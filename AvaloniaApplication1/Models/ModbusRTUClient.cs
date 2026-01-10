using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models
{
    public class ModbusRTUClient : IModbusClient
    {

        #region 字段
        public static Modbus.Device.IModbusSerialMaster master;//引入的Modbus包

        #endregion

        #region 属性

        #endregion

        #region 构造
        public ModbusRTUClient(SerialPort port)
        {
            master = ModbusSerialMaster.CreateRtu(port); //RTU模式    
            master.Transport.ReadTimeout = 1000; //读超时
            master.Transport.WriteTimeout = 1000; //写超时
            master.Transport.Retries = 1; //尝试重复连接次数
            master.Transport.WaitToRetryMilliseconds = 200; //尝试重复连接间隔
        }
        #endregion

        #region 方法
        public void Dispose()
        {
            master.Dispose();
            Task.Run(() =>
            {
                Task.Delay(360);
                master = null;

            });
        }

        public ushort[] ReadRegisters(byte slaveId, ushort startAddress, ushort num)
        {
             return master.ReadHoldingRegisters((byte)slaveId, startAddress, num); //读前94个寄存器

        }

        public void WriteRegister(byte slaveId, ushort startAddress, ushort[] value)
        {
            master.WriteMultipleRegisters(slaveId, startAddress, value);
        }

        #endregion

    }
}
