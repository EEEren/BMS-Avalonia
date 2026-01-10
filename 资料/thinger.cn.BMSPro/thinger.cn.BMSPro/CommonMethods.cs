// ***********************************************************************
//    Assembly       : 新阁教育
//    Created          : 2020-11-11
// ***********************************************************************
//     Copyright by 新阁教育（天津星阁教育科技有限公司）
//     QQ：        2934008828（付老师）  
//     WeChat：thinger002（付老师）
//     公众号：   dotNet工控上位机
//     哔哩哔哩：dotNet工控上位机
//     知乎：      dotNet工控上位机
//     头条：      dotNet工控上位机
//     视频号：   dotNet工控上位机
//     版权所有，严禁传播
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xktComm;
using xktComm.Common;
using xktNodeSettings.Node.Modbus;
using xktNodeSettings.Node.Variable;

namespace thinger.cn.BMSPro
{
    public class CommonMethods
    {
        //ModbusRTU设备集合
        public static List<NodeModbusRTU> ModbusRTUList = new List<NodeModbusRTU>();

        //字典的键是变量名称，值是对应的变量值
        public static Dictionary<string, object> CurrentRTUValue = new Dictionary<string, object>();

        //字典的键是变量名称，值是对应的变量对象
        public static Dictionary<string, ModbusRTUVariable> CurrentRTUVarList = new Dictionary<string, ModbusRTUVariable>();

        //是否为重启标志位
        public static bool isRestartExit = true;

        //添加日志委托
        public static AddLogDelegate AddLog;

        //语言选择
        public static Language myLang = Language.Chinese;

        public static bool CommonWrite(string VarName, string value)
        {
            if (CurrentRTUVarList.ContainsKey(VarName))
            {
                //获取变量对象
                return CommonWrite(CurrentRTUVarList[VarName],value);
            }
            else
            {
                return false;
            }
        }

        public static bool CommonWrite(ModbusRTUVariable variable, string value)
        {
            return ModbusRTUList[0].modrtu.Write(variable.GroupID, variable.VarAddress, value, variable.VarType).IsSuccess;

        }
    }  
}
