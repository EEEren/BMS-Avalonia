using Avalonia.Threading;
using AvaloniaApplication1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Modbus.Device;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using NModbus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    #region 字段

    private static SerialPort port; //串口
    [ObservableProperty]
    private string? _portName;  //串口名
    [ObservableProperty]
    private int baudRate = 115200;  //波特率
    private Parity parity;  //校验位
    private int dataBits;   //数据为
    private StopBits stopBits;  //停止位
    private Timer _Timer;   //定时器，定时获取从站状态

    [ObservableProperty]
    private int slaveAddr = 1;  //从站地址
    
    [ObservableProperty]
    private TimeSpan  _timeSpan = DateTime.Now.TimeOfDay;   //时间控件   

    [ObservableProperty]
    private string _cellVoltageStatus = "正常";
    
    [ObservableProperty]
    private string _cellEqualization = "均衡停止";
    // 电芯电压状态
    public int CellVoltageStatusFlag;
    // 电芯均衡状态
    public int CellEqualizationFlag;
    
    private int timeoutCnt = 0; //超时次数

    private ModbusRTUClient modbus; //ModusRTU客户端

    private ModbusScheduler scheduler;  //Modbuus调度器

    private ModbusScheduler.PollPoint[] pollPoint;



    #region 单体电压
    // 0号单体电压
    [ObservableProperty]
    private int _monomerVoltage0;
    // 1号单体电压
    [ObservableProperty]
    private int _monomerVoltage1;
    // 2号单体电压
    [ObservableProperty]
    private int _monomerVoltage2;
    // 3号单体电压
    [ObservableProperty]
    private int _monomerVoltage3;
    // 4号单体电压
    [ObservableProperty]
    private int _monomerVoltage4;
    // 5号单体电压
    [ObservableProperty]
    private int _monomerVoltage5;
    // 6号单体电压
    [ObservableProperty]
    private int _monomerVoltage6;
    // 7号单体电压
    [ObservableProperty]
    private int _monomerVoltage7;
    // 8号单体电压
    [ObservableProperty]
    private int _monomerVoltage8;
    // 9号单体电压
    [ObservableProperty]
    private int _monomerVoltage9;
    // 10号单体电压
    [ObservableProperty]
    private int _monomerVoltage10;
    // 11号单体电压
    [ObservableProperty]
    private int _monomerVoltage11;
    // 12号单体电压
    [ObservableProperty]
    private int _monomerVoltage12;
    // 13号单体电压
    [ObservableProperty]
    private int _monomerVoltage13;
    // 14号单体电压  预留
    [ObservableProperty]
    private int _monomerVoltage14;
    // 15号单体电压  预留
    [ObservableProperty]
    private int _monomerVoltage15;
    
    //0-15号单体序号 预留  暂不启用
    
    #endregion

    #region 温度
    [ObservableProperty]
    private string _temperatureStatus = "正常";
    
    // 温度状态
    [ObservableProperty] 
    private int _temperatureStatusFlag;
    // 0号温度状态
    [ObservableProperty] 
    private int _temperatureStatus0; //获取寄存器值后-40
                        
    // 1号温度状态
    [ObservableProperty] 
    private int _temperatureStatus1; //获取寄存器值后-40
    // 2号温度状态
    [ObservableProperty] 
    private int _temperatureStatus2; //获取寄存器值后-40
    // 3-15温度状态  预留
    
    // 0号温度序号
    [ObservableProperty] 
    private int _temperatureIndex0;
    // 1号温度序号
    [ObservableProperty] 
    private int _temperatureIndex1;
    // 2号温度序号
    [ObservableProperty] 
    private int _temperatureIndex2;
    // 3-15温度序号  预留
    

    #endregion
    
    // 电池电流
    [ObservableProperty] 
    private double _batteryCurrent; //获取寄存器值后先*0.1再-16000
    // 累加总电压值
    [ObservableProperty] 
    private double _accumulatedTotalVoltage; //获取寄存器值后*0.1
    // 母线总电压值
    [ObservableProperty] 
    private double _totalBusVoltage; //获取寄存器值后*0.1
    // 采集温度数量
    [ObservableProperty] 
    private int _temperaturesCollectedCount;
    // 采集电压数量
    [ObservableProperty] 
    private int _voltagesCollectedCount;
    
    // 环境温度
    [ObservableProperty] 
    private int environmentalTemperature;   //获取寄存器值后-40
    
    // 充电MOS温度 预留
    // 放电MOS温度 预留
    
    // BMS_SOC
    [ObservableProperty] 
    private double _bMS_SOC;  //获取寄存器值后*0.1
    // BMS_SOH
    [ObservableProperty] 
    private double bMS_SOH;   //获取寄存器值后*0.1
    
    // 标称容量
    [ObservableProperty] 
    private double _nominalCapacity; //获取寄存器值后*0.1
    // 实际容量
    [ObservableProperty] 
    private double _practicalCapacity;   //获取寄存器值后*0.1
    // 运行状态
    [ObservableProperty] 
    private int _runStatus; 
    
    // 最高单体电压序号
    [ObservableProperty] 
    private int _highestIndividualVoltageIndex;
    // 最低单体电压序号
    [ObservableProperty] 
    private int _minimumIndividualVoltageIndex;
    
    // 最高单体电压
    [ObservableProperty] 
    private int _highestUnitVoltage;
    // 最低单体电压
    [ObservableProperty] 
    private int _minimumUnitVoltage;
    
    // 最高温度序号
    [ObservableProperty] 
    private int _maximumTemperatureIndex;
    // 最低温度序号
    [ObservableProperty] 
    private int _minimumTemperatureIndex;
    // 最高温度
    [ObservableProperty] 
    private int _maximumTemperature; //获取寄存器值后-40
    // 最低温度
    [ObservableProperty] 
    private int _minimumTemperature; //获取寄存器值后-40
    
    
    // 充电MOS闭合标识  预留
    // 放电MOS闭合标识  预留
    // 限流MOS闭合标识  预留
    
    [ObservableProperty]
    private string _thresholdFaultAlarmtop1 = "无警告";
    // 阈值类故障告警top1
    private int ThresholdFaultAlarmtop1Flag;
    [ObservableProperty]
    private string _thresholdFaultAlarmtop2 = "无警告";
    // 阈值类故障告警top2
    private int ThresholdFaultAlarmtop2Flag; 
    [ObservableProperty]
    private string _booleanFaultAlarm = "正常";
    // 布尔类告警
    private int BooleanFaultAlarmFlag;
    
    // 日志记录条数
    [ObservableProperty] 
    private int _logCount;


    // 软件版本号
    [ObservableProperty]
    private string _softwareVersion = string.Empty;
    // 休眠状态
    [ObservableProperty] 
    private int _dormantState;
    // 休眠倒计时  预留
    
    // 硬件版本号
    [ObservableProperty]
    private string _hardwareVersion = string.Empty;
    
    // 电池ID序列号
    [ObservableProperty]
    private string _batteryIDSerialNumber = string.Empty;
    // 连接/断开电池
    [ObservableProperty] 
    private int _baterryConnetionStatus;
    
    #region 电池组状态
    
    // 循环次数
    [ObservableProperty] 
    private int _loopCount;
    // 过温次数
    [ObservableProperty] 
    private int _overTemperatureCount;
    // 过放次数
    [ObservableProperty] 
    private int _overReleaseCount;
    // 过流次数
    [ObservableProperty] 
    private int _overcurrentsCount;
    // 过充次数
    [ObservableProperty] 
    private int _overchargesCount;
    [ObservableProperty]
    private string _flightControlProtocol = "新微克";
    // 飞控协议配置
    private int FlightControlProtocolFlag;
    
    #endregion

    [ObservableProperty]
    private string _btnSwitchSerialPortText = "打开串口"; 
    
    [ObservableProperty]
    private bool _isSerialEnabled = false;  //串口是否打开

    [ObservableProperty] 
    private string _flightControlProtocolType = string.Empty;   //修改飞控协议
    #endregion

    private DateTime _seletedDate; //日历控件绑定数据

    [ObservableProperty] 
    private string _batteryCapacity;    //设置电池电量

    public DateTime SeletedDate
    {
        get => _seletedDate;
        set
        {
            _seletedDate = value;

        }
    }


    #region 属性

    // 界面上需要显示的内容
    //串口名称
    [ObservableProperty] 
    private ObservableCollection<string> _serialList = new ObservableCollection<string>();

    //波特率
    [ObservableProperty]
    private ObservableCollection<int> _baudRateList = new ObservableCollection<int>()
    {
        9600, 19200, 38400, 57600, 115200
    };

    
    
    //从站地址
    
    //延时间隔
    [ObservableProperty]
    private int _delayInterval = 100;
    
    public record LogEntry(string Date, string Message);
    
    [ObservableProperty]
    private ObservableCollection<LogEntry> _logEntries = new ObservableCollection<LogEntry>();
    
    [ObservableProperty]
    private ObservableCollection<FaultInfo> allFaultInfos = new ObservableCollection<FaultInfo>();  //所有的故障信息
    
    [ObservableProperty]
    private ObservableCollection<FaultInfo> filterFaultInfos = new ObservableCollection<FaultInfo>();  //所有的故障信息

    #endregion
    
    
    #region 构造函数
    public MainWindowViewModel()
    {
        //定时器初始化
        //_Timer = new Timer(GetStatus, null, Timeout.Infinite, Timeout.Infinite);

        AddLog("系统启动");
        //获取连接电脑的串口
        GetSerialPort();

        //添加轮询读所需要的寄存器
        pollPoint = new ModbusScheduler.PollPoint[]
        {
            new ModbusScheduler.PollPoint(1, 4096, 120), //寄存器0x1000
            new ModbusScheduler.PollPoint(1, 4191, 41), //寄存器0x105F
        };

    }
    
    #endregion
    
    #region 方法
    
    /// <summary>
    /// 连接串口
    /// </summary>
    public bool Connect()
    {
        try
        {
            //设置串口参数
            port = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One);
            if (!port.IsOpen)
            {
                port.Open(); //打开串口
            }

            //创建ModbusRtu实例
            modbus = new ModbusRTUClient(port);

            AddLog($"连接串口{PortName}成功");
            IsSerialEnabled = true;
            return true;
        }
        catch (Exception e)
        {
            IsSerialEnabled = false;
            AddWarnLog($"连接串口{PortName}失败, {e.Message}", e);
            return false;
        }
    
    }
    
    /// <summary>
    /// 断开串口
    /// </summary>
    public bool Disconnect()
    {
        try
        {
            modbus.Dispose();
            //关闭串口
            Task.Run(() =>
            {
                Task.Delay(300);
                port.Close();

            });
            IsSerialEnabled = false;
            
            return true;
        }
        catch (Exception ex)
        {
            AddWarnLog($"关闭串口{PortName}失败, {ex.Message}", ex);

            return false;
        }
    }
    
    /// <summary>
    /// 获取从站信息
    /// </summary>
    private async void GetStatus()
    {
        if(port == null || !port.IsOpen || !IsSerialEnabled || modbus == null)
        {
            return;
        }

        
        try
        {

            #region 前半部分寄存器

            /* 使用调度器进行读取，不再使用定时器
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var part1 = await master.ReadHoldingRegistersAsync((byte)SlaveAddr, 0x1000, 120); //读前94个寄存器
            sw.Stop();
            AddLog($"前120寄存器的数据是{data}");
            AddLog($"读 120 个寄存器数据共耗时 {sw.ElapsedMilliseconds} ms");
             */

            var part1 = scheduler.FirstData;
            string data = string.Join(Environment.NewLine,
                part1.Select((v, i) => $"[{i}] = {v}"));

            CellVoltageStatusFlag = part1[0]; //电芯电压状态
            CellVoltageStatus = CellVoltageStatusFlag == 0 ? "正常" : "异常";
            CellEqualizationFlag = part1[1]; //电芯均衡状态
            CellEqualization = CellEqualizationFlag == 0 ? "均衡停止" : "均衡开启";

            MonomerVoltage0 = part1[2]; // 0号单体电压 
            MonomerVoltage1 = part1[3]; // 1号单体电压
            MonomerVoltage2 = part1[4]; // 2号单体电压
            MonomerVoltage3 = part1[5]; // 3号单体电压
            MonomerVoltage4 = part1[6]; // 4号单体电压
            MonomerVoltage5 = part1[7]; // 5号单体电压
            MonomerVoltage6 = part1[8]; // 6号单体电压
            MonomerVoltage7 = part1[9]; // 7号单体电压
            MonomerVoltage8 = part1[10]; // 8号单体电压
            MonomerVoltage9 = part1[11]; // 9号单体电压
            MonomerVoltage10 = part1[12]; // 10号单体电压
            MonomerVoltage11 = part1[13]; // 11号单体电压
            MonomerVoltage12 = part1[14]; // 12号单体电压
            MonomerVoltage13 = part1[15]; // 13号单体电压
            // MonomerVoltage14 = part1[16];    // 14号单体电压  预留
            // MonomerVoltage15 = part1[17];    // 15号单体电压  预留

            //0-15号单体序号 预留  暂不启用
            // CellVoltageStatus = part1[18];
            // CellVoltageStatus = part1[19];
            // CellVoltageStatus = part1[20];
            // CellVoltageStatus = part1[21];
            // CellVoltageStatus = part1[22];
            // CellVoltageStatus = part1[23];
            // CellVoltageStatus = part1[24];
            // CellVoltageStatus = part1[25];
            // CellVoltageStatus = part1[26];
            // CellVoltageStatus = part1[27];
            // CellVoltageStatus = part1[28];
            // CellVoltageStatus = part1[29];
            // CellVoltageStatus = part1[30];
            // CellVoltageStatus = part1[31];
            // CellVoltageStatus = part1[32];
            // CellVoltageStatus = part1[33];

            TemperatureStatusFlag = part1[34] - 40; // 温度状态
            TemperatureStatus = TemperatureStatusFlag == 0 ? "正常" : "异常";

            TemperatureStatus0 = part1[35] - 40; // 0号温度状态
            TemperatureStatus1 = part1[36] - 40; // 1号温度状态
            TemperatureStatus2 = part1[37] - 40; // 2号温度状态
            // 3-15温度状态  预留
            // CellVoltageStatus = part1[38];
            // CellVoltageStatus = part1[39];
            // CellVoltageStatus = part1[40];
            // CellVoltageStatus = part1[41];
            // CellVoltageStatus = part1[42];
            // CellVoltageStatus = part1[43];
            // CellVoltageStatus = part1[44];
            // CellVoltageStatus = part1[45];
            // CellVoltageStatus = part1[46];
            // CellVoltageStatus = part1[47];
            // CellVoltageStatus = part1[48];
            // CellVoltageStatus = part1[49];
            // CellVoltageStatus = part1[50];

            TemperatureIndex0 = part1[51]; // 0号温度序号
            TemperatureIndex1 = part1[52]; // 1号温度序号
            TemperatureIndex2 = part1[53]; // 2号温度序号
            // 3-15温度序号  预留
            // CellVoltageStatus = part1[54];
            // CellVoltageStatus = part1[55];
            // CellVoltageStatus = part1[56];
            // CellVoltageStatus = part1[57];
            // CellVoltageStatus = part1[58];
            // CellVoltageStatus = part1[59];
            // CellVoltageStatus = part1[60];
            // CellVoltageStatus = part1[61];
            // CellVoltageStatus = part1[62];
            // CellVoltageStatus = part1[63];
            // CellVoltageStatus = part1[64];
            // CellVoltageStatus = part1[65];
            // CellVoltageStatus = part1[66];

            BatteryCurrent = (part1[67] - 16000) * 0.1; // 电池电流
            AccumulatedTotalVoltage = part1[68] * 0.1; // 累加总电压值
            TotalBusVoltage = part1[69] * 0.1; // 母线总电压值
            TemperaturesCollectedCount = part1[70]; // 采集温度数量
            VoltagesCollectedCount = part1[71]; // 采集电压数量
            EnvironmentalTemperature = part1[72] - 40; // 环境温度

            // 充电MOS温度 预留
            // CellVoltageStatus = part1[73];
            // 放电MOS温度 预留
            // CellVoltageStatus = part1[74];

            BMS_SOC = part1[75] * 0.1; // BMS_SOC
            BMS_SOH = part1[76] * 0.1; // BMS_SOH
            NominalCapacity = part1[77] * 0.1; // 标称容量
            PracticalCapacity = part1[78] * 0.1; // 实际容量
            RunStatus = part1[79]; // 运行状态
            HighestIndividualVoltageIndex = part1[80]; // 最高单体电压序号
            MinimumIndividualVoltageIndex = part1[81]; // 最低单体电压序号
            HighestUnitVoltage = part1[82]; // 最高单体电压
            MinimumUnitVoltage = part1[83]; // 最低单体电压
            MaximumTemperatureIndex = part1[84]; // 最高温度序号
            MinimumTemperatureIndex = part1[85]; // 最低温度序号
            MaximumTemperature = part1[86] - 40; // 最高温度
            MinimumTemperature = part1[87] - 40; // 最低温度

            // 充电MOS闭合标识  预留
            // CellVoltageStatus = part1[88];
            // 放电MOS闭合标识  预留
            // CellVoltageStatus = part1[89];
            // 限流MOS闭合标识  预留
            // CellVoltageStatus = part1[90];

            ThresholdFaultAlarmtop1Flag = part1[91]; // 阈值类故障告警top1
            switch (ThresholdFaultAlarmtop1Flag)
            {
                case 0:
                    ThresholdFaultAlarmtop1 = "无告警";
                    break;
                case 1:
                    ThresholdFaultAlarmtop1 = "一级告警";
                    break;
                case 2:
                    ThresholdFaultAlarmtop1 = "二级告警";
                    break;
                case 3:
                    ThresholdFaultAlarmtop1 = "三级告警";
                    break;
                default:
                    ThresholdFaultAlarmtop1 = "其他";
                    break;
            }

            ThresholdFaultAlarmtop2Flag = part1[92]; // 阈值类故障告警top2
            switch (ThresholdFaultAlarmtop2Flag)
            {
                case 0:
                    ThresholdFaultAlarmtop2 = "无告警";
                    break;
                case 1:
                    ThresholdFaultAlarmtop2 = "一级告警";
                    break;
                case 2:
                    ThresholdFaultAlarmtop2 = "二级告警";
                    break;
                case 3:
                    ThresholdFaultAlarmtop2 = "三级告警";
                    break;
                default:
                    ThresholdFaultAlarmtop2 = "其他";
                    break;
            }

            BooleanFaultAlarmFlag = part1[93]; // 布尔类告警
            BooleanFaultAlarm = BooleanFaultAlarmFlag == 0 ? "正常" : "故障";
            LogCount = part1[94]; // 日志记录条数

            // SoftwareVersion = DECToHEX(part1.Skip(94).Take(8).ToArray());
            // HardwareVersion = DECToHEX(part1.Skip(104).Take(8).ToArray());
            // BatteryIDSerialNumber = DECToHEX(part1.Skip(112).Take(12).ToArray());
            #endregion


            #region 后半部分寄存器

            /* 使用调度器进行读取，不再使用定时器

            sw = System.Diagnostics.Stopwatch.StartNew();
            var part2 = await master.ReadHoldingRegistersAsync((byte)SlaveAddr, 0x105F, 41); //读后41个寄存器
            sw.Stop();
            data = string.Join(Environment.NewLine,
                part2.Select((v, i) => $"[{i}] = {v}"));
            AddLog($"后41个寄存器的数据是{data}");

            AddLog($"读 41 个寄存器数据共耗时 {sw.ElapsedMilliseconds} ms");
            */

            var part2 = scheduler.LastData;
            //  软件版本号由0~7组成
            // AddLog($"读取寄存器软件版本号{part2.Take(8).ToArray()}");
            // SoftwareVersion = DECToHEX(part2.Take(8).ToArray());
            // AddLog($"转换后的软件版本号{SoftwareVersion}");
            // DormantState = part2[8];
            // // 休眠倒计时  预留
            // // DormantState = part2[9];
            // //  硬件版本号由10~17组成
            // AddLog($"读取寄存器软件版本号{part2.Skip(10).Take(8).ToArray()}");
            // HardwareVersion = DECToHEX(part2.Skip(10).Take(8).ToArray());
            // AddLog($"转换后的软件版本号{HardwareVersion}");
            //
            // //  电池序列号由18~29组成
            // BatteryIDSerialNumber = DECToHEX(part2.Skip(18).Take(12).ToArray());

            // 预留29~33

            BaterryConnetionStatus = part2[34];
            LoopCount = part2[35];
            OverTemperatureCount = part2[36];
            OverReleaseCount = part2[37];
            OvercurrentsCount = part2[38];
            OverchargesCount = part2[39];
            FlightControlProtocolFlag = part2[40];
            switch (FlightControlProtocolFlag)
            {
                case 0:
                    FlightControlProtocol = "新微克";
                    break;
                case 1:
                    FlightControlProtocol = "博鹰";
                    break;
                case 2:
                    FlightControlProtocol = "正方";
                    break;
                default:
                    FlightControlProtocol = "其他";
                    break;
            }
            #endregion

        }
        catch(NullReferenceException ex)
        {

        }
        catch (TimeoutException e)
        {
            AddLog("获取从站状态超时");
        }
        catch (Exception ex)
        {
            AddWarnLog($"获取从站状态失败,{ex.Message}", ex);
        }
    }


    private async void GetVersionInfo()
    {
        if(port == null || !port.IsOpen || !IsSerialEnabled || modbus == null)
        {
            return;
        }

        
        try
        {

            /* 使用调度器进行读取，不再使用定时器
             
            var sw = System.Diagnostics.Stopwatch.StartNew();
            sw = System.Diagnostics.Stopwatch.StartNew();
            var part2 = await master.ReadHoldingRegistersAsync((byte)SlaveAddr, 0x105F, 41); //读后41个寄存器
            sw.Stop(); sw.Stop();
            string data = string.Join(Environment.NewLine,
                part2.Select((v, i) => $"[{i}] = {v}"));
            AddLog($"后41个寄存器的数据是{data}");
            AddLog($"读 41 个寄存器数据共耗时 {sw.ElapsedMilliseconds} ms");
            */


            var part2 = scheduler.LastData;

            //  软件版本号由0~7组成
            AddLog($"读取寄存器硬件版本号{string.Join(",", part2.Take(8).ToArray())}");
            SoftwareVersion = NummberToLetter(part2.Take(8).ToArray());
            AddLog($"转换后的硬件件版本号{SoftwareVersion}");
            DormantState = part2[8];
            // 休眠倒计时  预留
            // DormantState = part2[9];
            //  硬件版本号由10~17组成
            AddLog($"读取寄存器软件版本号{string.Join(",", part2.Skip(10).Take(8).ToArray())}");
            HardwareVersion = NummberToLetter(part2.Skip(10).Take(8).ToArray());
            AddLog($"转换后的软件版本号{HardwareVersion}");
            
            //  电池序列号由18~29组成
            AddLog($"读取寄存器电池ID{string.Join(",", part2.Skip(18).Take(12).ToArray())}");
            BatteryIDSerialNumber = NummberToLetter(part2.Skip(18).Take(12).ToArray());
            AddLog($"转换后的电池ID{HardwareVersion}");

            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        

    }

    /// <summary>
    /// 获取本地的串口
    /// </summary>
    [RelayCommand]
    public void GetSerialPort()
    {
        AddLog("获取本地串口");
        // 清除本地串口数据
        SerialList.Clear();
        // 获取本地串口数据
        var localSerial = SerialPort.GetPortNames();
        //给localSerial排序
        Array.Sort(localSerial);
        // 添加本地串口到SerialList
        foreach (var serial in localSerial)
        {
            SerialList.Add(serial);
        }
        
        //设置第一个串口为默认串口
        PortName = SerialList.FirstOrDefault();
    }
    
    
    /// <summary>
    /// 按钮打开串口，计时器开启
    /// </summary>
    [RelayCommand]
    public void OpenSerialPort()
    {
        //连接串口
        var res = Connect();

        /* 使用调度器进行读取，不再使用定时器
        //定时器启动，获取从站状态
        if (res)
        {
            AddLog("开启定时任务"); 
            _Timer.Change(DelayInterval, DelayInterval);
        }
        */

        scheduler = new ModbusScheduler(modbus, pollPoint,
            pollInterval: TimeSpan.FromSeconds(2),
            interRequestGap: TimeSpan.FromMilliseconds(2));
    
    }

    [RelayCommand]
    public void SwitchSerialPort()
    {
        if (IsSerialEnabled)
        {
            AddLog("断开电池");
            var res = Disconnect();
            if (res)
            {
                BtnSwitchSerialPortText = "打开串口";
                //AddLog("取消定时任务");
                //_Timer?.Change(Timeout.Infinite, Timeout.Infinite);
                
            }

        }
        else
        {
            //连接串口
            AddLog("连接电池");
            var res = Connect();
            //定时器启动，获取从站状态
            if (res)
            {
                BtnSwitchSerialPortText = "关闭串口";
                //AddLog("开启定时任务");
                //AddLog($"定时器间隔{DelayInterval}");
                ////GetVersionInfo();
                //_Timer.Change(DelayInterval, DelayInterval);
                scheduler = new ModbusScheduler(modbus, pollPoint, pollInterval: TimeSpan.FromSeconds(2), interRequestGap: TimeSpan.FromMilliseconds(2));
            }
        }
    }

    /// <summary>
    /// 按钮：写入时间
    /// </summary>
    [RelayCommand]
    public async Task WriteDate()
    {
        if(port == null || !port.IsOpen || !IsSerialEnabled || modbus == null)
        {
            await MessageBoxManager.GetMessageBoxStandard("提示", "串口未连接", ButtonEnum.Ok).ShowAsync();

            return;
        }

        
        try
        {
            if(SeletedDate == null || _seletedDate.ToString() == "0001/1/1 0:00:00")
            {
                SeletedDate = DateTime.Now;
            }
            if (SeletedDate is { } day)
            {
                AddLog($"写入时间{SeletedDate}-{TimeSpan}");
                //获取界面上所选的日期和时间
                var week = ISOWeek.GetWeekOfYear(day);
                ushort[] date = new ushort[]
                {
                    (ushort)day.Year,
                    (ushort)day.Month,
                    (ushort)day.Day,
                    (ushort)week,
                    (ushort)TimeSpan.Hours,
                    (ushort)TimeSpan.Minutes
                };

                //将年，月，周，日，时间，分钟写入寄存器
                var writeTask = scheduler.WriteRegisterAsync((byte)SlaveAddr,  4232, date);
                var res = await writeTask;
                if (res == true)
                {
                    AddLog($"写入时间成功");

                }
                else
                {
                    AddLog($"向寄存器0x1088写入{date}失败, 写入时间{SeletedDate}-{TimeSpan}失败");

                }

                /* 使用调度器进行读取，不再使用定时器
                //将年，月，周，日，时间，分钟写入寄存器
                await master.WriteMultipleRegistersAsync((byte)SlaveAddr, 0x1088, date);
                */

            }
        }
        catch (TimeoutException)
        {
            AddLog($"写入时间超时");
        }
        catch (Exception e)
        {
            AddWarnLog($"写入时间{SeletedDate}-{TimeSpan}失败, {e.Message}", e);
        }
    }

    /// <summary>
    /// 按钮：写入飞控协议
    /// </summary>
    [RelayCommand]
    public async Task ModifyFightControl()
    {
        if (port == null || !port.IsOpen || modbus == null)
        {
            await MessageBoxManager.GetMessageBoxStandard("提示", "串口未连接", ButtonEnum.Ok).ShowAsync();
            return;
        }

        
        try
        {
            if (port == null || !port.IsOpen)
            {
                await MessageBoxManager.GetMessageBoxStandard("提示", "串口未连接", ButtonEnum.Ok).ShowAsync();
                return;
            }
            if (!string.IsNullOrEmpty(FlightControlProtocolType)) //判断飞控协议是否填值
            {
                int fcpt = int.Parse(FlightControlProtocolType);
                ushort[] data = new ushort[]
                {
                    (ushort)(fcpt & 0xFFFF),
                    (ushort)(fcpt >> 16)
                };

                var writeTask = scheduler.WriteRegisterAsync((byte)SlaveAddr, 4231, data);
                var res = await writeTask;
                if(res == true)
                {
                    AddLog($"飞控协议修改成功");

                }
                else
                {
                    AddLog($"向寄存器0x1087写入{fcpt}失败, 飞控协议修改失败");

                }
                /* 使用调度器进行读取，不再使用定时器
                await master.WriteSingleRegisterAsync((byte)SlaveAddr, 0x1087,
                    (ushort)int.Parse(FlightControlProtocolType));
                */
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("提示", "未填写飞控协议类型", ButtonEnum.Ok).ShowAsync();
                AddLog($"未填写飞控协议类型");
            }
        }
        catch (TimeoutException)
        {
            AddLog($"修改飞控协议超时");
        }
        catch (Exception e)
        {
            AddWarnLog($"修改飞控协议{FlightControlProtocolType}失败,{e.Message}", e);
        }
        

    }
    

    /// <summary>
    /// 设置电池容量
    /// </summary>
    [RelayCommand]
    public async Task SetBatteryCapacity()
    {
        if (port == null || !port.IsOpen)
        {
            await MessageBoxManager.GetMessageBoxStandard("提示", "串口未连接", ButtonEnum.Ok).ShowAsync();
            return;
        }

        try
        {
            if (!string.IsNullOrEmpty(FlightControlProtocolType)) //判断飞控协议是否填值
            {
                /* 使用调度器进行读取，不再使用定时器
                await master.WriteSingleRegisterAsync((byte)SlaveAddr, 0x1087,
                    (ushort)int.Parse(BatteryCapacity));
                */
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("提示", "未填写电池容量", ButtonEnum.Ok).ShowAsync();

                AddLog($"未填写飞控协议类型");
            }
        }
        catch (TimeoutException)
        {
            AddLog($"设置电池容量超时");
        }
        catch (Exception e)
        {
            AddWarnLog($"设置电池容量{FlightControlProtocolType}失败,{e.Message}", e);
        }
        
    }

    [RelayCommand]
    public async Task GetFaultInfo()
    {
        if(port == null || !port.IsOpen || !IsSerialEnabled || modbus == null)
        {
            return;
        }


        
        try
        {
            /* 使用调度器进行读取，不再使用定时器
            var res = await master.ReadHoldingRegistersAsync((byte)SlaveAddr, 0xc000, 10);
            */

            var readTask = scheduler.ReadHoldRegisterAsync((byte)SlaveAddr, 49152, 10);
            var res = await readTask;

            FaultInfo item = new FaultInfo();
            string year = res[2].ToString();
            string month = (res[3] >> 8).ToString();
            string week = (res[3] & 0xFF).ToString();
            string day = (res[4] >> 8).ToString();
            string hour = (res[4] & 0xFF).ToString();
            string minutes = (res[5] >> 8).ToString();
            string second = (res[5] & 0xFF).ToString();
            item.DateTime = $"{year}年{month}月{week}周{day}日:{hour}时{minutes}分{second}秒";
            ushort state = res[0];
            switch (state)
            {
                case 0:
                    item.TemperatureState = "正常";
                    item.CurrentState = "正常";
                    item.VoltageState = "正常";
                    item.FaultType = "正常";
                    break;
                case 1:
                    item.TemperatureState = "正常";
                    item.CurrentState = "正常";
                    item.VoltageState = $"{res[1]}mV";
                    item.FaultType = "充电过压异常";
                    break;
                case 2:
                    item.TemperatureState = "正常";
                    item.CurrentState = "正常";
                    item.VoltageState = $"{res[1]}mV";
                    item.FaultType = "充电过压恢复";
                    break;
                case 3:
                    item.TemperatureState = "正常";
                    item.FaultType = "充电过流异常";
                    item.VoltageState = "正常";
                    item.CurrentState = $"{res[1]}mA";
                    break;
                case 4:
                    item.TemperatureState = "正常";
                    item.FaultType = "充电过流恢复";
                    item.VoltageState = "正常";
                    item.CurrentState = $"{res[1]}mA";

                    break;
                case 5:
                    item.FaultType = "充电高温异常";
                    item.CurrentState = "正常";
                    item.VoltageState = "正常";
                    item.TemperatureState = $"{res[1]}℃";
                    break;
                case 6:
                    item.FaultType = "充电高温恢复";
                    item.CurrentState = "正常";
                    item.VoltageState = "正常";
                    item.TemperatureState = $"{res[1]}℃";
                    break;
                case 11:
                    item.FaultType = "放电过压异常";
                    item.CurrentState = "正常";
                    item.VoltageState = $"{res[1]}mV";
                    item.TemperatureState = "正常";
                    break;
                case 12:
                    item.FaultType = "放电过压恢复";
                    item.CurrentState = "正常";
                    item.TemperatureState = "正常";
                    item.VoltageState = $"{res[1]}mV";
                    break;
                case 13:
                    item.FaultType = "放电过流异常";
                    item.CurrentState = $"{res[1]}mA";
                    item.VoltageState = "正常";
                    item.TemperatureState = "正常";

                    break;
                case 14:
                    item.FaultType = "放电过流恢复";
                    item.TemperatureState = "正常";
                    item.VoltageState = "正常";
                    item.CurrentState = $"{res[1]}mA";

                    break;
                case 15:
                    item.FaultType = $"放电高温异常";
                    item.CurrentState = "正常";
                    item.VoltageState = "正常";
                    item.TemperatureState = $"{res[1]}℃";
                    break;
                case 16:
                    item.FaultType = "放电高温恢复";
                    item.CurrentState = "正常";
                    item.VoltageState = "正常";
                    item.TemperatureState = $"{res[1]}℃";
                    break;

            }
            
            allFaultInfos.Add(item);
        }
        catch (TimeoutException)
        {
            AddLog($"获取故障信息超时");
        }
        catch (Exception e)
        {
            AddWarnLog($"获取故障信息{FlightControlProtocolType}失败,{e.Message}", e);
        }
    }

    private void RefreshFaultInfo()
    {
        if(SeletedDate == null)
        {
            SeletedDate = DateTime.Now;
        }

        string date = SeletedDate.ToString("yyyyMMdd");
        FilterFaultInfos = AllFaultInfos.Where(f => f.DateTime.Contains(date)) as ObservableCollection<FaultInfo>;
    }


    /// <summary>
    /// 添加日志
    /// </summary>
    /// <param name="log">传入的日志</param>
    private void AddLog(string log)
    {
        if (string.IsNullOrEmpty(log))
        {
            return;
        }

        Dispatcher.UIThread.Post(() =>
        {
            LogEntry item = new LogEntry(
                Date: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Message: log
            );
            // LogEntries.Insert(0, item);
            LogEntries.Add(item);
        });
        //写入日志到本地
        Log.Information(log);
    }

    private void AddWarnLog(string log, Exception ex)
    {
        if (string.IsNullOrEmpty(log))
        {
            return;
        }
        Dispatcher.UIThread.Post(() =>
        {
            LogEntry item = new LogEntry(
                Date: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Message: log
            );
            // LogEntries.Insert(0, item);
            LogEntries.Add(item);
        });
        //写入日志到本地
        Log.Warning($"{ex}");
    }
    
    /// <summary>
    /// 十进制转十六进制
    /// </summary>
    /// <param name="value">十进制数数组</param>
    /// <returns>十六进制字符串</returns>
    private string DECToHEX(ushort[] value)
    {
        string result = string.Empty;
        foreach (var item in value)
        {
            string hex = item.ToString("X");
            result += hex;
        }
        return result;
    }

    /// <summary>
    /// 将int类型十进制数据转换成十六进制ushort[]
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private ushort[] DECToHEX(int value)
    {
        ushort[] words = new ushort[2];

        // 方法：位运算
        words[0] = (ushort)((value >> 16) & 0xFFFF); // 高 16 位
        words[1] = (ushort)(value & 0xFFFF);    
        return words;
    }

    private string NummberToLetter(ushort[] data)
    {
        // 一个 ushort = 高 8 位 + 低 8 位
        byte[] bytes = data.SelectMany(u => new[] { (byte)(u >> 8), (byte)(u & 0xFF) })
            .ToArray();
        string txt = Encoding.ASCII.GetString(bytes);
        return txt;
    }
    
    
    #endregion
    
}