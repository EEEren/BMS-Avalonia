using System;

namespace AvaloniaApplication1.Models;

public class FaultInfo
{
    public string DateTime { get; set; } // 故障时间
    
    /*
    public string TemperatureState { get; set; } = "正常";    //温度状态
    public string CurrentState { get; set; } = "正常";    //电流状态
    public string VoltageState { get; set; } = "正常";    //电压状态
    
    public string TempertureInfo { get; set; }  //温度异常信息
    
    public string CurrentInfo { get; set; } //电流异常信息
    
    public string VoltageInfo { get; set; } //电压异常信息
    */

    public string FaultType { get; set; } = "正常";
    
    public int SystemTotalVoltage { get; set; }  //系统总电压
    public int SystemCurrent { get; set; }  //系统电流
    public int SOC { get; set; }    //显示SOC
    public int MaxCellVoltage { get; set; } //最高单体电压
    public int MinCellVoltage { get; set; } //最低单体电压
    public int MaxTemperature { get; set; } //最高温度
    public int MinTemperature { get; set; } //最低温度

}