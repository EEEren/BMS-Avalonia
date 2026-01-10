using Avalonia.Xaml.Interactivity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public interface IModbusClient : IDisposable
    {
        void WriteRegister(byte slaveId, ushort startAddress, ushort[] value);

        ushort[] ReadRegisters(byte slaveId, ushort startAddress, ushort num);
    }

    public enum ModbusJobType
    {
        Write,  //写寄存器
        ReadAdHoc,  //临时读
        PollingReadCycle    //轮询读
    }

    public abstract class ModbusJobBase
    {
        public ModbusJobType Type { get; }

        public int Priority { get; } //优先级 数字越小优先级越高

        public TaskCompletionSource<object?> Tcs { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public CancellationToken Cancellation { get; }

        public ModbusJobBase(ModbusJobType type, int priority, CancellationToken ct)
        {
            Type = type;
            Priority = priority;
            Cancellation = ct;
        }

        internal void TryCacnel()
        {
            if(!Tcs.Task.IsCompleted) 
            {
                Tcs.TrySetCanceled();
            }
        }
    }

    public sealed class ModbusWriteJob : ModbusJobBase
    {
        public byte SlaveId { get; set; }
        public ushort StartAddress { get; set; }
        public ushort[] Values { get; set; }

        public bool ReadBackVerify { get; set; }

        public ushort? ReadBackLength { get; set; }

        public ModbusWriteJob(byte slaveId, ushort startAddress, ushort[] values, bool readBackVerify, ushort? readBackLength, int priority, CancellationToken ct) 
            : base (ModbusJobType.Write, priority, ct)
        {
            SlaveId = slaveId;
            StartAddress = startAddress;
            Values = values;
            ReadBackVerify = readBackVerify;
            ReadBackLength = readBackLength;

        }
    }


    public sealed class ModbusAdHocReadJob : ModbusJobBase
    {
        public byte SlaveId { get; set; }
        public ushort StartAddress { get; set; }
        public ushort Length { get; set; }

        public ModbusAdHocReadJob(byte slaveId, ushort startAddress, ushort length, int priority, CancellationToken ct)
            : base(ModbusJobType.ReadAdHoc, priority, ct)
        {
            SlaveId = slaveId;
            StartAddress = startAddress;
            Length = length;

        }
    }

    internal sealed class ModbusJobQueues
    {
        private readonly List<ConcurrentQueue<ModbusJobBase>> _priorityBuckets;

        public ModbusJobQueues(int maxPriority)
        {
            _priorityBuckets = Enumerable.Range(0, maxPriority + 1)
                .Select( _ => new ConcurrentQueue<ModbusJobBase>())
                .ToList();

        }

        public void Enqueue(ModbusJobBase job)
        {
            int p = Math.Clamp(job.Priority, 0, _priorityBuckets.Count - 1);
            _priorityBuckets[p].Enqueue(job);
        }

        public bool TryDequeue(out ModbusJobBase? job)
        {
            foreach(var q in _priorityBuckets)
            {
                if(q.TryDequeue(out job))
                {
                    return true;
                }    

            }

            job = null;
            return false;
        }

        public bool HasPendingHighPriorityWrites()
        {
            return !_priorityBuckets[0].IsEmpty;
        }

        public bool AnyJobs()
        {
            return _priorityBuckets.Any(q => !q.IsEmpty);
        }

    }

    public sealed class ModbusScheduler
    {
        #region 字段
        private readonly IModbusClient _client;
        private readonly CancellationTokenSource _cts = new();
        private readonly Thread _worker;
        private readonly ModbusJobQueues _queue = new(maxPriority: 2);
        private readonly AutoResetEvent _wakeup = new(false);

        // 固定轮询点位配置
        private readonly List<PollPoint> _pollPoints = new();
        private readonly TimeSpan _pollInterval;
        private DateTime _nextPollTime = DateTime.Now;

        //RTU 或 设备限制下的发送间隔
        private readonly TimeSpan _interRequestGap;
        #endregion

        #region 属性
        public List<ushort> FirstData { get; set; } = new();
        public List<ushort> LastData { get; set; } = new();

        #endregion


        public ModbusScheduler(IModbusClient client, IEnumerable<PollPoint> pollPoints, TimeSpan pollInterval, TimeSpan? interRequestGap = null)
        {
            _client = client;
            _pollPoints.AddRange(pollPoints);
            _pollInterval = pollInterval;
            _interRequestGap = interRequestGap ?? TimeSpan.FromMilliseconds(10);
            _worker = new Thread(Loop)
            {
                IsBackground = true,
                Name = "ModbusScheduler"
            };

            _worker.Start(); 
        }


        //轮询点位描述， 可扩展为不同的function code
        public record PollPoint(Byte SlaveId, ushort StartAddress, ushort Length);

        public Task<bool> WriteRegisterAsync(Byte slaveId, ushort startAddr, ushort[] values, 
            bool readBackVerify = false, ushort? readBackLength = null,
            CancellationToken ct =default)

        {
            var job = new ModbusWriteJob(slaveId, startAddr, values, readBackVerify, readBackLength, priority: 0, ct);
            _queue.Enqueue(job);
            _wakeup.Set();
            return job.Tcs.Task.ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    return false;
                }
                if (t.IsFaulted)
                {
                    throw t.Exception!.InnerException!;
                }
                return true;
            }, TaskScheduler.Default);

        }

        public Task<ushort[]> ReadHoldRegisterAsync(byte slaveId, ushort startAddr, ushort length,
            CancellationToken ct = default)
        {
            var job = new ModbusAdHocReadJob(slaveId, startAddr, length, priority: 1,ct);
            _queue.Enqueue(job);
            _wakeup.Set();
            return job.Tcs.Task.ContinueWith(t =>
            {
                if (t.IsCanceled)
                {
                    throw new TaskCanceledException();
                }
                if (t.IsFaulted)
                {
                    throw t.Exception!.InnerException!;
                }
                return (ushort[])t.Result!;
            }, TaskScheduler.Default);
        }


        private void Loop()
        {
            while(!_cts.IsCancellationRequested)
            {
                try
                {
                    
                    //先处理队列中高优先级的写/读
                    while (_queue.TryDequeue(out var job))
                    {
                        ExecuteJob(job);
                        if(_cts.IsCancellationRequested)
                        {
                            break;
                        }

                        SleepGap();
                    }

                    if(DateTime.Now >= _nextPollTime && !_queue.HasPendingHighPriorityWrites())
                    {
                        RunPollingCycle();
                        _nextPollTime = DateTime.Now + _pollInterval;
                    }

                    var waitMs = ComputeWaitMilliseconds();
                    _wakeup.WaitOne(waitMs);


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ModbusScheduler] Loop error: {ex}");
                }
            }
        }


        private void ExecuteJob(ModbusJobBase job)
        {
            if(job.Cancellation.IsCancellationRequested)
            {
                job.TryCacnel();
                return;
            }

            try
            {
                switch (job)
                {
                    case ModbusWriteJob w:
                        _client.WriteRegister(w.SlaveId, w.StartAddress, w.Values);
                        if (w.ReadBackVerify && w.ReadBackLength.HasValue && w.ReadBackLength.Value > 0)
                        {
                            Thread.Sleep(20);
                            var rb = _client.ReadRegisters(w.SlaveId, w.StartAddress, w.ReadBackLength.Value);
                            w.Tcs.TrySetResult(rb);
                        }
                        else
                        {
                            w.Tcs.TrySetResult(true);

                        }
                        break;
                    case ModbusAdHocReadJob r:
                        var data = _client.ReadRegisters(r.SlaveId, r.StartAddress, r.Length);
                        r.Tcs.TrySetResult(data);
                        break;
                    default:
                        job.Tcs.TrySetResult(null);
                        break;

                }
            }
            catch (Exception ex)
            {
                job.Tcs.TrySetResult(ex);
            }
                
        }


        private void RunPollingCycle()
        {
            foreach(var p in _pollPoints)
            {
                if(_queue.HasPendingHighPriorityWrites())
                {
                    break ;
                }

                try
                {
                    var data = _client.ReadRegisters(p.SlaveId, p.StartAddress, p.Length);
                    OnPollingData(p, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[RunPollingCycle] Read error SlaveId={p.SlaveId} Addr={p.StartAddress}: {ex.Message}");

                }
                SleepGap();
            }
        }

        private void OnPollingData(PollPoint p, ushort[] data)
        {
            if (p.StartAddress == 4096)
            {
                FirstData.Clear();
                FirstData.AddRange(data);
            }
            else if(p.StartAddress == 4191)
            {
                LastData.Clear();
                LastData.AddRange(data);
            }
        } 


        private int ComputeWaitMilliseconds()
        {
            if(_queue.AnyJobs())
            {
                return 0;

            }

            var diff = _nextPollTime - DateTime.Now;
            if(diff <= TimeSpan.Zero)
            {
                return 0;
            }

            var ms = (int)Math.Min(diff.TotalMilliseconds, 2000);
            return Math.Max(ms, 1);
        }

        public void SleepGap()
        {
            if(_interRequestGap > TimeSpan.Zero)
            {
                Thread.Sleep(_interRequestGap);
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _wakeup.Set();
            if(!_worker.Join(2000))
            {
                _worker.Interrupt();
            }
            _client.Dispose();
            _cts.Dispose();
            _wakeup.Dispose();
        }

    }


}
