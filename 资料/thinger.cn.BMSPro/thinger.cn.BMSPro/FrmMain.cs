using SeeSharpTools.JY.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using thinger.cn.BMSControl;
using thinger.cn.BMSDAL;
using thinger.cn.BMSHelper;
using thinger.cn.BMSModels;
using xktComm;
using xktComm.Common;
using xktControl;
using xktNodeSettings;
using xktNodeSettings.Node.Group;
using xktNodeSettings.Node.Modbus;
using LED = SeeSharpTools.JY.GUI.LED;

namespace thinger.cn.BMSPro
{
    /// <summary>
    /// 打开窗体的名称
    /// </summary>
    public enum FormName
    {
        保护参数,
        校准参数,
        硬件参数,
        固件更新
    }

    /// <summary>
    /// 多语言
    /// </summary>
    public enum Language
    {
        Chinese,
        English
    }


    //声明委托    再增加两个参数，第一个参数是否是报警信息，第二个参数表示是触发信号还是消除信号

    public delegate void AddLogDelegate(int index, string log, bool isAlarm, bool isAck);

    //声明委托    窗体切换委托
    public delegate void FormChangeDelegate(FormName formName, bool isEnter);

    //声明委托    保存配置
    public delegate void SaveDefaultSettingDelegate(CommSet set);

    //声明委托    保存存储配置
    public delegate void SaveStoreSettingDelegate(StoreSet set);

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            //系统相关初始化
            InitializeSystem();

            this.Load += FrmMain_Load;
        }

        #region 系统相关初始化
        private void InitializeSystem()
        {
            //绑定委托
            CommonMethods.AddLog = MyAddLog;

            //设置listView
            this.lstInfo.Columns[1].Width = this.lstInfo.ClientSize.Width - this.lstInfo.Columns[0].Width - 20;

            //初始化定时器
            this.updateTimer = new System.Windows.Forms.Timer();
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;

            //数据存储定时器
            storeTimer = new System.Timers.Timer(1);
            storeTimer.AutoReset = true;
            storeTimer.Elapsed += StoreTimer_Elapsed;

            //读取配置信息
            this.set = LoadDefaultSetting();
            this.storeSet = LoadSaveSetting();

            if (this.set == null)
            {
                CommonMethods.AddLog(1, IsChinese ? "通信配置信息读取失败" : "Setting Read Error", false, false);
                return;
            }
        }
        #endregion

        #region 实时更新
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (IsChinese)
            {
                this.tsl_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));
            }
            else
            {
                this.tsl_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("en-US"));
            }

            if (CommonMethods.ModbusRTUList.Count == 1)
            {
                if (IsChinese)
                {
                    this.tsl_CommState.Text = CommonMethods.ModbusRTUList[0].ConnectState ? "通信正常" : "通信中断";
                }
                else
                {
                    this.tsl_CommState.Text = CommonMethods.ModbusRTUList[0].ConnectState ? "Comm OK" : "Comm NG";

                }

                if (CommonMethods.ModbusRTUList[0].ConnectState)
                {
                    UpdateInfo(this);
        
                    //获取最大最小串数
                    if (CommonMethods.CurrentRTUValue.ContainsKey("VmaxminNo"))
                    {
                        ushort val = Convert.ToUInt16(CommonMethods.CurrentRTUValue["VmaxminNo"]);

                        byte[] value = BitConverter.GetBytes(val);

                        int max = value[1];
                        int min = value[0];

                        foreach (Control control in pnl_Voltage.Controls)
                        {
                            if (control is CellVoltage cv)
                            {
                                if (cv.Index == max)
                                {
                                    cv.VoltageClass = VoltageClass.Max;
                                }
                                else if (cv.Index == min)
                                {
                                    cv.VoltageClass = VoltageClass.Min;
                                }
                                else
                                {
                                    cv.VoltageClass = VoltageClass.Normal;
                                }
                            }
                        }

                    }
                }
            }
        }

        private void UpdateInfo(Control ctl)
        {
            foreach (Control item in ctl.Controls)
            {
                if (item is Label lbl)
                {
                    if (lbl.Name == "lbl_AverageVol")
                    {
                        if (CommonMethods.CurrentRTUValue.ContainsKey("Voltage") && CommonMethods.CurrentRTUValue.ContainsKey("Cell_Num"))
                        {
                            float voltage = float.Parse(CommonMethods.CurrentRTUValue["Voltage"].ToString());
                            int chuan = int.Parse(CommonMethods.CurrentRTUValue["Cell_Num"].ToString());

                            if (chuan != CurrentBatteryCount)
                            {
                                Invoke(new Action(() =>
                                {
                                    AddCellVoltage(chuan);

                                    CurrentBatteryCount = chuan;
                                }));
                            }
                            chuan = chuan == 0 ? 1 : chuan;
                            lbl.Text = Convert.ToInt32(voltage / chuan * 1000.0f).ToString();
                        }
                    }

                    if (lbl.Name == "lbl_DiffVol")
                    {
                        if (CommonMethods.CurrentRTUValue.ContainsKey("Vmax") && CommonMethods.CurrentRTUValue.ContainsKey("Vmin"))
                        {
                            float max = float.Parse(CommonMethods.CurrentRTUValue["Vmax"].ToString());
                            float min = float.Parse(CommonMethods.CurrentRTUValue["Vmin"].ToString());
                            lbl.Text = (max - min).ToString();
                        }
                    }

                    if (lbl.Name == "lbl_Temp")
                    {
                        StringBuilder sb = new StringBuilder();

                        for (int i = 1; i < 4; i++)
                        {
                            if (CommonMethods.CurrentRTUValue.ContainsKey("Temp" + i.ToString()))
                            {
                                sb.Append(CommonMethods.CurrentRTUValue["Temp" + i.ToString()] + " ");
                            }
                        }

                        lbl.Text = sb.ToString().Trim();
                    }

                    if (lbl.Name == "lbl_SysState")
                    {
                        if (CommonMethods.CurrentRTUValue.ContainsKey("PackStatus"))
                        {
                            lbl.Text = GetSysState(Convert.ToInt16(CommonMethods.CurrentRTUValue["PackStatus"].ToString()));
                        }
                    }

                    if (lbl.Tag != null && lbl.Tag.ToString().Length > 0)
                    {
                        string var = lbl.Tag.ToString();

                        if (var.StartsWith("Shut_Down"))
                        {
                            if (CommonMethods.CurrentRTUValue.ContainsKey(var))
                            {
                                short val = Convert.ToInt16(CommonMethods.CurrentRTUValue[var].ToString());
                                lbl.Text = GetReason(val).Length == 0 ? IsChinese ? "无保护" : "No-Protect" : GetReason(val);
                            }
                        }
                        else if (CommonMethods.CurrentRTUValue.ContainsKey(var))
                        {
                            lbl.Text = CommonMethods.CurrentRTUValue[var].ToString();
                        }
                    }

                }

                else if (item is xktGauge gauge)
                {
                    if (gauge.Tag != null && gauge.Tag.ToString().Length > 0)
                    {
                        string var = gauge.Tag.ToString();
                        if (CommonMethods.CurrentRTUValue.ContainsKey(var))
                        {
                            gauge.CurrentValue = Convert.ToSingle(CommonMethods.CurrentRTUValue[var].ToString());
                        }
                    }
                }

                else if (item is LED led)
                {
                    if (led.Tag != null && led.Tag.ToString().Length > 0)
                    {
                        string var = led.Tag.ToString();
                        if (CommonMethods.CurrentRTUValue.ContainsKey(var))
                        {
                            led.Value = CommonMethods.CurrentRTUValue[var].ToString() == "1" || CommonMethods.CurrentRTUValue[var].ToString().ToLower() == "true";
                        }
                    }
                }

                else if (item is CellVoltage cellVoltage)
                {
                    if (cellVoltage.Tag != null && cellVoltage.Tag.ToString().Length > 0)
                    {
                        string var = cellVoltage.Tag.ToString();
                        if (CommonMethods.CurrentRTUValue.ContainsKey(var))
                        {
                            cellVoltage.CellVolValue = CommonMethods.CurrentRTUValue[var].ToString();
                        }
                    }
                }

                else if (item is ToolStrip ts)
                {
                    if (CommonMethods.CurrentRTUValue.ContainsKey("Madeinfo_SoftVer"))
                    {
                        string val = CommonMethods.CurrentRTUValue["Madeinfo_SoftVer"].ToString();

                        if (val.Length == 3)
                        {
                            this.lbl_Version.Text = "V" + val.Substring(0, 1) + "." + val.Substring(1, 1) + "." + val.Substring(2, 1);
                        }
                        else
                        {
                            this.lbl_Version.Text = "V0.0.0";
                        }
                    }
                }

                else if (item.HasChildren)
                {
                    UpdateInfo(item);
                }
            }

       
        }

        private string GetReason(short Value)
        {
            string result = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                if (BitLib.GetBitFromShort(Value, i))
                {
                    if (CommonMethods.myLang == Language.Chinese)
                    {
                        result += ReasonList[i] + " ";
                    }
                    else
                    {
                        result += ReasonEnglishList[i] + " ";
                    }
                }
            }
            return result;
        }

        private string GetSysState(short Value)
        {
            if (BitLib.GetBitFromShort(Value, 0))
            {
                return CommonMethods.myLang == Language.Chinese ? "放电使能" : "DisCharge-E";
            }
            else if (BitLib.GetBitFromShort(Value, 1))
            {
                return CommonMethods.myLang == Language.Chinese ? "充电使能" : "Charge-E";
            }
            else if (BitLib.GetBitFromShort(Value, 6))
            {
                return CommonMethods.myLang == Language.Chinese ? "放电状态" : "DisCharging";
            }
            if (BitLib.GetBitFromShort(Value, 7))
            {
                return CommonMethods.myLang == Language.Chinese ? "充电状态" : "Charging";
            }
            else
            {
                return CommonMethods.myLang == Language.Chinese ? "正常状态" : "Normal";
            }
        }

        #endregion

        #region 动态添加电池控件

        private void AddCellVoltage(int Count)
        {
            if (Count > 0)
            {
                this.pnl_Voltage.Controls.Clear();

                int RowCount = Count / 6 == 0 ? Count / 6 : Count / 6 + 1;

                for (int i = 0; i < RowCount; i++)
                {
                    //对于每行，行基本坐标
                    int x = 16;
                    int y = 41;

                    //行间距
                    int rowgap = 79;
                    int cellgap = 60;

                    //该行的数量
                    int count = i == RowCount - 1 ? Count - 6 * i : 6;

                    for (int j = 0; j < count; j++)
                    {
                        CellVoltage cv = new CellVoltage();
                        cv.Index = 6 * i + j + 1;
                        cv.Size = new Size(70, 54);
                        cv.Location = new Point(x + rowgap * j, y + cellgap * i);
                        cv.Tag = "VCell" + cv.Index;
                        cv.MyLang = (BMSControl.Language)CommonMethods.myLang;
                        this.pnl_Voltage.Controls.Add(cv);
                    }
                }
            }
        }
        #endregion

        #region 变量属性

        //配置文件路径
        private string xmlPath = Application.StartupPath + "\\Settings\\Settings.xml";

        private string SystemPath = Application.StartupPath + "\\Settings\\System.ini";

        //系统当前时间
        private string CurrentTime
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); }
        }

        //初始化读取通信组索引
        private ArrayList arraylist = new ArrayList() { 0, 2 };

        //固件更新状态
        private bool isBootLoading = false;

        //更新定时器
        private System.Windows.Forms.Timer updateTimer;

        //配置对象
        Properties.Settings DefaultSetting = new Properties.Settings();

        //配置信息
        private CommSet set = new CommSet();

        //存储信息
        private StoreSet storeSet = new StoreSet();

        //电池默认串数
        private int CurrentBatteryCount = 24;

        //是否中文
        private bool IsChinese
        {
            get { return CommonMethods.myLang == Language.Chinese; }
            set { CommonMethods.myLang = value ? Language.Chinese : Language.English; }
        }

        //原因集合
        private Dictionary<int, string> ReasonList = new Dictionary<int, string>();

        private Dictionary<int, string> ReasonEnglishList = new Dictionary<int, string>();

        //定时器
        private System.Timers.Timer storeTimer;

        //存储路径
        private string AutoStorePath = string.Empty;

        //标题
        private List<string> HeadList = new List<string>();

        #endregion

        #region 窗体初始化

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //第一步：读取配置信息
            try
            {
                CommonMethods.ModbusRTUList = ModbusRTUCFG.LoadXmlFile(xmlPath);
            }
            catch (Exception ex)
            {
                CommonMethods.AddLog(1, IsChinese ? "配置文件读取失败：" + ex.Message : "Config Read Error", false, false);
                return;
            }

            CommonMethods.AddLog(0, IsChinese ? "配置文件读取成功" : "Config Read Success", false, false);


            //第二步：初始化相关信息
            InitialInfo();


            //第三步：通用数据解析及连接
            ConnectAndComm();
        }

        #endregion

        #region 初始化相关信息
        private void InitialInfo()
        {
            //更改通信信息
            if (CommonMethods.ModbusRTUList.Count != 1)
            {
                CommonMethods.AddLog(1, "配置文件信息错误，设备数量不为1", false, false);
                return;
            }

            AddCellVoltage(CurrentBatteryCount);

            CommonMethods.ModbusRTUList[0].PortNum = set.PortName;
            CommonMethods.ModbusRTUList[0].Paud = set.BaudRate;
            CommonMethods.ModbusRTUList[0].SleepTime = set.SleepTime;
            foreach (var item in CommonMethods.ModbusRTUList[0].ModbusRTUGroupList)
            {
                item.SlaveID = set.DevAdd;

                foreach (var var in item.varList)
                {
                    if (!CommonMethods.CurrentRTUValue.ContainsKey(var.Name))
                    {
                        CommonMethods.CurrentRTUValue.Add(var.Name, "0");
                        CommonMethods.CurrentRTUVarList.Add(var.Name, var);
                    }
                }
            }

            ReasonList.Add(0, "短路保护");
            ReasonList.Add(1, "压差保护");
            ReasonList.Add(2, "放电二级过流保护");
            ReasonList.Add(3, "充电过流保护");
            ReasonList.Add(4, "放电过流保护");
            ReasonList.Add(5, "总压过压保护");
            ReasonList.Add(6, "总压欠压保护");
            ReasonList.Add(7, "单体过压保护");
            ReasonList.Add(8, "单体欠压保护");
            ReasonList.Add(9, "充电高温保护");
            ReasonList.Add(10, "充电低温保护");
            ReasonList.Add(11, "放电高温保护");
            ReasonList.Add(12, "放电低温保护");
            ReasonList.Add(13, "未知原因保护");
            ReasonList.Add(14, "未知原因保护");
            ReasonList.Add(15, "未知原因保护");

            ReasonEnglishList.Add(0, "ShortProtect");
            ReasonEnglishList.Add(1, "VolDiffProtect");
            ReasonEnglishList.Add(2, "D-OverCurrent2");
            ReasonEnglishList.Add(3, "C-OverCurrent");
            ReasonEnglishList.Add(4, "D-OverCurrent");
            ReasonEnglishList.Add(5, "T-OverVoltage");
            ReasonEnglishList.Add(6, "T-UnderVoltage");
            ReasonEnglishList.Add(7, "S-OverVoltage");
            ReasonEnglishList.Add(8, "S-UnderVoltage");
            ReasonEnglishList.Add(9, "C-OverTemp");
            ReasonEnglishList.Add(10, "C-UnderTemp");
            ReasonEnglishList.Add(11, "D-OverTemp");
            ReasonEnglishList.Add(12, "D-UnderTemp");
            ReasonEnglishList.Add(13, "Unknown");
            ReasonEnglishList.Add(14, "Unknown");
            ReasonEnglishList.Add(15, "Unknown");



            HeadList.AddRange(new string[] {
                 "日期时间",
                 "电压(V)",
                 "电流(A)",
                 "SOC",
                 "SOH",
                 "满充容量(AH)",
                 "剩余容量(AH)",
                 "循环次数(次)",
                 "Cell 1",
                 "Cell 2",
                 "Cell 3",
                 "Cell 4",
                 "Cell 5",
                 "Cell 6",
                 "Cell 7",
                 "Cell 8",
                 "Cell 9",
                 "Cell 10",
                 "Cell 11",
                 "Cell 12",
                 "Cell 13",
                 "Cell 14",
                 "Cell 15",
                 "Cell 16",
                 "Cell 17",
                 "Cell 18",
                 "Cell 19",
                 "Cell 20",
                 "Cell 21",
                 "Cell 22",
                 "Cell 23",
                 "Cell 24",
                 "单体最高(mv)",
                 "单体最低(mv)",
                 "单体平均(mv)",
                 "单体压差(mv)",
                 "温度1(℃)",
                 "温度2(℃)",
                 "温度3(℃)",
                 "保护信息",
                 "保护原因"

            });

            //中英文
            //语言初始化
            if (IsChinese)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

                ApplyResource(this);

                this.tsm_Chinese.Checked = true;
                this.tsm_English.Checked = false;

            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

                ApplyResource(this);

                this.tsm_Chinese.Checked = false;
                this.tsm_English.Checked = true;
            }


        }
        #endregion

        #region 通用数据解析及连接

        private void ConnectAndComm()
        {
            if (CommonMethods.ModbusRTUList.Count > 0)
            {
                foreach (var item in CommonMethods.ModbusRTUList)
                {
                    if (item.IsActive)
                    {
                        //开启多线程
                        item.cts = new CancellationTokenSource();

                        Task.Run(() => GetModbusRTUValue(item), item.cts.Token);
                    }
                }
            }
        }

        /// <summary>
        /// ModbusRTU通用数据读取及解析
        /// </summary>
        /// <param name="item"></param>
        private void GetModbusRTUValue(NodeModbusRTU Nodemodrtu)
        {
            while (!Nodemodrtu.cts.IsCancellationRequested)
            {
                if (Nodemodrtu.IsConnected)
                {
                    if (isBootLoading)
                    {
                        Thread.Sleep(10);
                    }
                    else
                    {
                        lock (arraylist.SyncRoot)
                        {
                            for (int i = 0; i < arraylist.Count; i++)
                            {
                                if (int.TryParse(arraylist[i].ToString(), out int index))
                                {
                                    if (Nodemodrtu.ModbusRTUGroupList.Count > index)
                                    {
                                        Nodemodrtu.ConnectState = GetGroupValue(Nodemodrtu.ModbusRTUGroupList[index], Nodemodrtu);

                                        if (Nodemodrtu.ConnectState)
                                        {
                                            if (Nodemodrtu.ErrorTimes > 0)
                                            {
                                                Nodemodrtu.ErrorTimes = 0;
                                                MyAddLog(0, IsChinese ? "BMS系统通信恢复" : "BMS Comm Recovery", false, false);
                                            }
                                        }
                                        else
                                        {
                                            Nodemodrtu.ErrorTimes++;
                                            MyAddLog(1, IsChinese ? "BMS系统通信中断" : "BMS Comm Interrupt", false, false);

                                            if (Nodemodrtu.ErrorTimes >= Nodemodrtu.MaxErrorTimes)
                                            {
                                                if (SerialPort.GetPortNames().Contains(Nodemodrtu.PortNum))
                                                {
                                                    Thread.Sleep(10);
                                                    continue;
                                                }
                                                else
                                                {
                                                    Nodemodrtu.IsConnected = false;
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!Nodemodrtu.FirstConnect)
                    {
                        Thread.Sleep(Nodemodrtu.ReConnectTime);
                        Nodemodrtu.modrtu?.DisConnect();
                    }

                    //初始化通信对象
                    Nodemodrtu.modrtu = new ModbusRtu();
                    //设置SleepTime
                    Nodemodrtu.modrtu.SleepTime = Nodemodrtu.SleepTime;
                    //设置数据格式
                    Nodemodrtu.modrtu.DataFormat = Nodemodrtu.DataFormat;
                    //建立连接
                    Nodemodrtu.IsConnected = Nodemodrtu.modrtu.Connect(Nodemodrtu.Paud, Nodemodrtu.PortNum, int.Parse(Nodemodrtu.DataBits), Nodemodrtu.Parity, Nodemodrtu.StopBits);

                    if (Nodemodrtu.FirstConnect)
                    {
                        MyAddLog(Nodemodrtu.IsConnected ? 0 : 1, Nodemodrtu.IsConnected ? IsChinese ? "BMS系统连接成功" : "BMS Connect OK" : IsChinese ? "BMS系统连接失败" : "BMS Connect NG", false, false);
                    }
                    else
                    {
                        MyAddLog(Nodemodrtu.IsConnected ? 0 : 1, Nodemodrtu.IsConnected ? IsChinese ? "BMS系统重连成功" : "BMS ReConn OK" : IsChinese ? "BMS系统重连失败" : "BMS ReConn NG", false, false);
                    }

                    Nodemodrtu.FirstConnect = false;
                }
            }
        }

        private bool GetGroupValue(ModbusRTUGroup gp, NodeModbusRTU Nodemodrtu)
        {
            if (gp.IsActive)
            {
                byte[] res = null;
                if (gp.StoreArea == ModbusStoreArea.输入线圈 || gp.StoreArea == ModbusStoreArea.输出线圈)
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStoreArea.输出线圈:
                            res = Nodemodrtu.modrtu.ReadOutputStatus(gp.SlaveID, gp.Start, gp.Length);
                            break;
                        case ModbusStoreArea.输入线圈:
                            res = Nodemodrtu.modrtu.ReadInputStatus(gp.SlaveID, gp.Start, gp.Length);
                            break;
                        default:
                            break;
                    }

                    if (res != null && res.Length == (gp.Length % 8 == 0 ? gp.Length / 8 : gp.Length / 8 + 1))
                    {
                        //表示读取到数据，这里就接着去解析数据
                        foreach (var var in gp.varList)
                        {
                            //地址验证
                            if (VerifyModbusAddress(var.VarAddress, true, out int start, out int offset))
                            {
                                start -= gp.Start;
                                switch (var.VarType)
                                {
                                    case DataType.Bool:
                                        //字节数组转换成布尔数组
                                        var.Value = BitLib.GetBitArrayFromByteArray(res, false)[start];
                                        break;
                                    default:
                                        break;
                                }

                                if (CommonMethods.CurrentRTUValue.ContainsKey(var.Name))
                                {
                                    CommonMethods.CurrentRTUValue[var.Name] = var.Value;
                                }
                                else
                                {
                                    CommonMethods.CurrentRTUValue.Add(var.Name, var.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;

                    }
                }
                else
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStoreArea.保持寄存器:
                            res = Nodemodrtu.modrtu.ReadKeepReg(gp.SlaveID, gp.Start, gp.Length);
                            break;
                        case ModbusStoreArea.输入寄存器:
                            res = Nodemodrtu.modrtu.ReadInputReg(gp.SlaveID, gp.Start, gp.Length);
                            break;
                        default:
                            break;
                    }
                    if (res != null && res.Length == gp.Length * 2)
                    {
                        //表示读取到数据，这里就接着去解析数据
                        foreach (var var in gp.varList)
                        {
                            //地址验证
                            if (VerifyModbusAddress(var.VarAddress, false, out int start, out int offset))
                            {
                                start -= gp.Start;

                                start *= 2;

                                switch (var.VarType)
                                {
                                    case DataType.Bool:
                                        //先拿到2个字节  根据2个字节中的偏移解析
                                        var.Value = BitLib.GetBitFrom2ByteArray(res, start, offset);
                                        break;
                                    case DataType.Byte:
                                        var.Value = ByteLib.GetByteFromByteArray(res, start);
                                        break;
                                    case DataType.Short:
                                        var.Value = ShortLib.GetShortFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.UShort:
                                        var.Value = UShortLib.GetUShortFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.Int:
                                        var.Value = IntLib.GetIntFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.UInt:
                                        var.Value = UIntLib.GetUIntFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.Float:
                                        var.Value = FloatLib.GetFloatFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.Double:
                                        var.Value = DoubleLib.GetDoubleFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.Long:
                                        var.Value = LongLib.GetLongFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.ULong:
                                        var.Value = ULongLib.GetULongFromByteArray(res, start, Nodemodrtu.DataFormat);
                                        break;
                                    case DataType.String:
                                        var.Value = StringLib.GetStringFromByteArray(res, start, offset);
                                        break;
                                    default:
                                        break;
                                }

                                //数据转换
                                var.Value = MigrationLib.GetMigrationValue(var.Value, var.Scale, var.Offset);

                                if (CommonMethods.CurrentRTUValue.ContainsKey(var.Name))
                                {
                                    CommonMethods.CurrentRTUValue[var.Name] = var.Value;
                                }
                                else
                                {
                                    CommonMethods.CurrentRTUValue.Add(var.Name, var.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证地址
        /// </summary>
        /// <param name="address">地址值</param>
        /// <param name="isBit">是否为线圈存储区</param>
        /// <param name="start">返回开始地址</param>
        /// <param name="offset">返回偏移地址</param>
        /// <returns></returns>
        private bool VerifyModbusAddress(string address, bool isBit, out int start, out int offset)
        {
            address = address.Replace(" ", "");
            if (isBit)
            {
                offset = 0;
                return int.TryParse(address, out start);
            }
            else
            {
                if (address.Contains('.'))
                {
                    string[] result = address.Split('.');

                    if (result.Length == 2)
                    {
                        bool value = true;
                        int res = 0;
                        value &= int.TryParse(result[0], out res);
                        start = res;

                        value &= int.TryParse(result[1], out res);
                        offset = res;

                        return value;
                    }
                    else
                    {
                        start = offset = 0;
                        return false;
                    }
                }
                else
                {
                    offset = 0;
                    return int.TryParse(address, out start);
                }
            }
        }


        #endregion

        #region 写入日志委托方法
        private void MyAddLog(int index, string log, bool isAlarm, bool isAck)
        {
            if (this.lstInfo.InvokeRequired)
            {
                this.lstInfo.Invoke(new Action(() =>
                {
                    AddLogMethod(index, log, isAlarm, isAck);
                }));

            }
            else
            {
                AddLogMethod(index, log, isAlarm, isAck);
            }
        }

        private List<string> AlarmInfoList = new List<string>();

        private string GetAlarmInfo(string log)
        {
            if (log.Contains('|'))
            {
                return log.Split('|')[IsChinese ? 0 : 1];
            }
            else
            {
                return log;
            }
        }

        private void AddLogMethod(int index, string log, bool isAlarm, bool isAck)
        {
            ListViewItem lst = new ListViewItem("    " + CurrentTime, index);

            string str = string.Empty;

            if (isAlarm)
            {
                string info = GetAlarmInfo(log);
                str = isAck ? info + "  ACK" : info + "  UNACK";

                if (isAck)
                {
                    if (!AlarmInfoList.Contains(info))
                    {
                        AlarmInfoList.Add(info);
                    }
                }
                else
                {
                    if (AlarmInfoList.Contains(info))
                    {
                        AlarmInfoList.Remove(info);
                    }
                }
            }
            else
            {
                str = log;
            }

            lst.SubItems.Add(str);

            RefreshAlarm();

            //Log4net
            LogHelper.Info(str);

            //保证最新的日志在最上面
            this.lstInfo.Items.Insert(0, lst);

            //存储到数据库
            //new SysLogService().InsertSysLog(new SysLog()
            //{
            //    LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    LogInfo = isAlarm ? GetAlarmInfo(log) : log,
            //    LogState = isAlarm ? isAck ? "ACK" : "UNACK" : "无",
            //    LogType = isAlarm ? "系统报警" : "系统日志"
            //});

        }


        private void RefreshAlarm()
        {
            if (AlarmInfoList.Count == 0)
            {
                this.lbl_Info.Visible = true;
                this.lbl_ScrollAlarm.Visible = false;
                this.lbl_Info.Text = CommonMethods.myLang == Language.Chinese ? "当前无系统报警" : "System No Errors";
                return;
            }
            if (AlarmInfoList.Count == 1)
            {
                this.lbl_Info.Visible = true;
                this.lbl_ScrollAlarm.Visible = false;
                this.lbl_Info.Text = AlarmInfoList[0];
                return;
            }
            if (AlarmInfoList.Count > 1)
            {
                this.lbl_Info.Visible = false;
                this.lbl_ScrollAlarm.Visible = true;
                string msg = string.Empty;

                foreach (var item in AlarmInfoList)
                {
                    msg += item + "     ";
                }
                this.lbl_ScrollAlarm.Text = msg;
                return;
            }
        }

        #endregion

        #region 窗体切换委托方法

        private void FormChangeMethod(FormName formName, bool isEnter)
        {
            switch (formName)
            {
                case FormName.保护参数:

                    if (isEnter)
                    {
                        arraylist.Add(3);
                        arraylist.Add(6);
                    }
                    else
                    {
                        arraylist.Remove(3);
                        arraylist.Remove(6);
                    }

                    break;
                case FormName.校准参数:
                    if (isEnter)
                    {
                        arraylist.Add(5);
                    }
                    else
                    {
                        arraylist.Remove(5);
                    }
                    break;
                case FormName.硬件参数:
                    if (isEnter)
                    {
                        arraylist.Add(4);
                        arraylist.Add(7);
                    }
                    else
                    {
                        arraylist.Remove(4);
                        arraylist.Remove(7);
                    }
                    break;
                case FormName.固件更新:
                    if (isEnter)
                    {
                        isBootLoading = true;
                    }
                    else
                    {
                        isBootLoading = false;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 读取及写入配置信息

        private CommSet LoadDefaultSetting()
        {
            CommSet set = new CommSet();
            try
            {
                set.PortName = DefaultSetting.PortName;

                set.BaudRate = Convert.ToInt32(DefaultSetting.BaudRate);

                set.DevAdd = Convert.ToInt32(DefaultSetting.DevAddress);

                set.SleepTime = Convert.ToInt32(DefaultSetting.SleepTime);
            }
            catch (Exception)
            {
                return null;
            }
            return set;
        }

        private StoreSet LoadSaveSetting()
        {
            StoreSet set = new StoreSet();
            try
            {
                set.StorePath = DefaultSetting.StorePath;

                set.UseDefault = DefaultSetting.UseDefault == "1";

                set.StoreTime = Convert.ToInt32(DefaultSetting.StoreTime);
            }
            catch (Exception)
            {
                return null;
            }
            return set;
        }

        private void SaveDefaultSetting(CommSet set)
        {
            DefaultSetting.PortName = set.PortName;
            DefaultSetting.BaudRate = set.BaudRate.ToString();
            DefaultSetting.DevAddress = set.DevAdd.ToString();
            DefaultSetting.SleepTime = set.SleepTime.ToString();
            DefaultSetting.Save();
        }

        private void SaveStoreSetting(StoreSet set)
        {
            DefaultSetting.StorePath = set.StorePath;
            DefaultSetting.UseDefault = set.UseDefault ? "1" : "0";
            DefaultSetting.StoreTime = set.StoreTime.ToString();
            DefaultSetting.Save();
        }

        #endregion

        #region 窗体操作
        private void tsb_ProtectParam_Click(object sender, EventArgs e)
        {
            OpenForm("保护参数");
        }

        private void tsb_CalibrationParam_Click(object sender, EventArgs e)
        {
            OpenForm("校准参数");
        }

        private void tsb_HardwareConfig_Click(object sender, EventArgs e)
        {
            OpenForm("硬件参数");
        }

        private void tsb_HardwareUpdate_Click(object sender, EventArgs e)
        {
            OpenForm("固件更新");
        }

        private void tsb_SystemLog_Click(object sender, EventArgs e)
        {
            OpenForm("系统日志");
        }

        private void OpenForm(string formName)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item.Text == formName)
                {
                    item.Activate();
                    return;
                }
            }

            switch (formName)
            {
                case "保护参数":
                    new FrmProtect() { FormChange = this.FormChangeMethod }.Show();
                    break;
                case "校准参数":
                    new FrmCalibration() { FormChange = this.FormChangeMethod }.Show();
                    break;
                case "硬件参数":
                    new FrmHardware() { FormChange = this.FormChangeMethod }.Show();
                    break;
                case "固件更新":
                    new FrmUpdate() { FormChange = this.FormChangeMethod }.Show();
                    break;
                case "系统日志":
                    new FrmSysLog().Show();
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region 事件方法
        private void tsb_Restart_Click(object sender, EventArgs e)
        {
            CommonMethods.isRestartExit = true;

            DialogResult dr = MessageBox.Show("是否确定要重启系统？", "重启系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                Application.Exit();

                Thread.Sleep(500);

                Process.Start(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                CommonMethods.isRestartExit = false;
            }

        }

        private void tsm_CommSet_Click(object sender, EventArgs e)
        {
            FrmCommSet objFrm = new FrmCommSet(set);

            objFrm.SaveDefaultSetting = this.SaveDefaultSetting;

            objFrm.ShowDialog();
        }

        private void tsm_StoreSet_Click(object sender, EventArgs e)
        {
            FrmStoreSet objFrm = new FrmStoreSet(storeSet);

            objFrm.SaveDefaultSetting = this.SaveStoreSetting;

            DialogResult dr = objFrm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.storeSet = objFrm.set;

            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CommonMethods.isRestartExit)
            {
                DialogResult dr = MessageBox.Show("你确定要退出系统吗？", "退出系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }
        }

        private void tsm_Restart_Click(object sender, EventArgs e)
        {
            this.tsb_Restart_Click(null, null);
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsm_Exit_Click(object sender, EventArgs e)
        {
            this.tsb_Exit_Click(null, null);
        }
        #endregion

        #region 检测USB插拔

        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;
        public const int DBT_CONFIGCHANGED = 0x0018;
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;
        public const int DBT_DEVNODES_CHANGED = 0x0007;
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        case DBT_DEVICEARRIVAL:
                            CommonMethods.AddLog(0, IsChinese ? "检测USB插入电脑" : "USB Device In", false, false);
                            DriveInfo[] s = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    break;
                                }
                            }
                            break;
                        case DBT_CONFIGCHANGECANCELED:
                            break;
                        case DBT_CONFIGCHANGED:
                            break;
                        case DBT_CUSTOMEVENT:
                            break;
                        case DBT_DEVICEQUERYREMOVE:
                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:
                            break;
                        case DBT_DEVICEREMOVECOMPLETE:
                            CommonMethods.AddLog(0, IsChinese ? "检测USB拔出电脑" : "USB Device Out", false, false);
                            break;
                        case DBT_DEVICEREMOVEPENDING:
                            break;
                        case DBT_DEVICETYPESPECIFIC:
                            break;
                        case DBT_DEVNODES_CHANGED:
                            break;
                        case DBT_QUERYCHANGECONFIG:
                            break;
                        case DBT_USERDEFINED:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            base.WndProc(ref m);
        }





        #endregion

        #region 开启记录
        private void tsm_RecordStart_Click(object sender, EventArgs e)
        {
            if (tsm_RecordStop.Checked)
            {
                if (CommonMethods.ModbusRTUList[0].IsConnected)
                {
                    if (this.storeSet.UseDefault)
                    {
                        this.tsm_RecordStart.Checked = true;
                        this.tsm_RecordStop.Checked = false;
                        AutoStorePath = this.storeSet.StorePath + "\\" + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";
                        if (!Directory.Exists(this.storeSet.StorePath))
                        {
                            AutoStorePath = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";
                        }
                    }
                    else
                    {
                        using (SaveFileDialog dialog = new SaveFileDialog())
                        {
                            dialog.Title = "请选择要保存的文件路径";
                            dialog.Filter = "csv文件|*.csv";
                            dialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                this.tsm_RecordStart.Checked = true;
                                this.tsm_RecordStop.Checked = false;
                                AutoStorePath = dialog.FileName;
                            }
                        }
                    }
                    CommonMethods.AddLog(0, "开始记录，记录周期：" + this.storeSet.StoreTime + "秒", false, false);
                    storeTimer.Start();
                }
                else
                {
                    CommonMethods.AddLog(1, "开始记录失败：设备连接中断", false, false);
                }
            }
        }

        private void tsm_RecordStop_Click(object sender, EventArgs e)
        {
            if (tsm_RecordStart.Checked)
            {
                CommonMethods.AddLog(0, "停止记录，记录周期：" + this.storeSet.StoreTime + "秒", false, false);
                storeTimer.Stop();
                this.tsm_RecordStart.Checked = false;
                this.tsm_RecordStop.Checked = true;

                DialogResult dr = MessageBox.Show("是否立即打开记录的CSV文件？", "打开记录", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.OK)
                {
                    Process.Start(AutoStorePath);
                }

            }
        }
        private void StoreTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.storeTimer.Interval == 1)
            {
                this.storeTimer.Interval = this.storeSet.StoreTime * 1000;
            }

            if (CommonMethods.ModbusRTUList[0].IsConnected)
            {
                List<string> StrList = new List<string>();
                StrList.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms"));
                StrList.Add(CommonMethods.CurrentRTUValue["Voltage"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["CurCadc"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["RSOC"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["HSOC"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["FCC"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["RC"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["CycleCount"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell1"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell2"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell3"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell4"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell5"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell6"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell7"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell8"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell9"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell10"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell11"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell12"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell13"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell14"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell15"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell16"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell17"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell18"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell19"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell20"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell21"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell22"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell23"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["VCell24"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["Vmax"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["Vmin"].ToString());

                float voltage = float.Parse(CommonMethods.CurrentRTUValue["Voltage"].ToString());
                float chuan = float.Parse(CommonMethods.CurrentRTUValue["Cell_Num"].ToString());
                StrList.Add((voltage / chuan * 1000.0f).ToString());
                float max = float.Parse(CommonMethods.CurrentRTUValue["Vmax"].ToString());
                float min = float.Parse(CommonMethods.CurrentRTUValue["Vmin"].ToString());
                StrList.Add((max - min).ToString());


                StrList.Add(CommonMethods.CurrentRTUValue["Temp1"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["Temp2"].ToString());
                StrList.Add(CommonMethods.CurrentRTUValue["Temp3"].ToString());

                short value = Convert.ToInt16(CommonMethods.CurrentRTUValue["ALARM"].ToString());
                StrList.Add(GetAllStatus(value));
                string reason = string.Empty;
                reason += CommonMethods.CurrentRTUValue["Shut_Down1"].ToString() + " ";
                reason += CommonMethods.CurrentRTUValue["Shut_Down2"].ToString() + " ";
                reason += CommonMethods.CurrentRTUValue["Shut_Down3"].ToString() + " ";
                reason += CommonMethods.CurrentRTUValue["Shut_Down4"].ToString() + " ";
                reason += CommonMethods.CurrentRTUValue["Shut_Down5"].ToString() + " ";
                StrList.Add(reason);
                WriteToCSV(AutoStorePath, StrList);
            }
            else
            {
                MyAddLog(1, "写入记录停止：设备连接中断", false, false);
                storeTimer.Stop();
                Invoke(new Action(() =>
                {
                    this.tsm_RecordStart.Checked = false;
                    this.tsm_RecordStop.Checked = true;
                }));
            }
        }
        private string GetAllStatus(short Value)
        {

            string result = string.Empty;
            for (int i = 0; i < 16; i++)
            {

                result += BitLib.GetBitFromShort(Value, i) ? "1" : "0" + " ";
            }

            return result;
        }

        private void WriteToCSV(string path, List<string> dataList)
        {
            StreamWriter sw;

            try
            {
                if (!File.Exists(path))
                {
                    //创建写入器
                    sw = new StreamWriter(path, false, Encoding.Default);
                    //写入第一行
                    sw.WriteLine("开始记录时间" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms"), Encoding.Default);
                    //写入第二行

                    sw.WriteLine(string.Join(",", HeadList), Encoding.Default);
                }
                else
                {
                    sw = new StreamWriter(path, true, Encoding.Default);

                }

                sw.WriteLine(string.Join(",", dataList.ToArray()), Encoding.Default);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MyAddLog(1, "写入记录停止：" + ex.Message, false, false);
                storeTimer.Stop();
                Invoke(new Action(() =>
                {
                    this.tsm_RecordStart.Checked = false;
                    this.tsm_RecordStop.Checked = true;

                }));
            }
        }

        #endregion

        #region 中英文切换

        private void tsm_Chinese_Click(object sender, EventArgs e)
        {
            if (this.tsm_English.Checked)
            {
                this.tsm_Chinese.Checked = true;
                this.tsm_English.Checked = false;
                IsChinese = true;

                IniConfigHelper.WriteIniData("当前语言", "语言名称", "中文", SystemPath);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

                ApplyResource(this);
            }
        }

        private void tsm_English_Click(object sender, EventArgs e)
        {
            if (this.tsm_Chinese.Checked)
            {
                this.tsm_Chinese.Checked = false;
                this.tsm_English.Checked = true;
                IsChinese = false;

                IniConfigHelper.WriteIniData("当前语言", "语言名称", "英文", SystemPath);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

                ApplyResource(this);
            }
        }


        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMain));

        // 遍历控件，并根据资源文件替换相应属性
        private void ApplyResource(Control control)
        {
            foreach (Control ctl in control.Controls)
            {
                if (ctl is MenuStrip ms)
                {
                    foreach (ToolStripItem tsi in ms.Items)
                    {
                        resources.ApplyResources(tsi, tsi.Name);
                        ToolStripMenuItem tsmi = tsi as ToolStripMenuItem;
                        foreach (ToolStripItem item in tsmi.DropDownItems)
                        {
                            resources.ApplyResources(item, item.Name);
                        }
                    }
                }
                else if (ctl is ToolStrip ts)
                {
                    foreach (ToolStripItem tsi in ts.Items)
                    {
                        resources.ApplyResources(tsi, tsi.Name);
                        ToolStripDropDownButton tsmi = tsi as ToolStripDropDownButton;
                    }
                }
                else if (ctl is xktPlateHead ph)
                {
                    resources.ApplyResources(ph, ph.Name);
                    ApplyResource(ph);
                }

                else if (ctl is CellVoltage cv)
                {
                    cv.MyLang = (BMSControl.Language)CommonMethods.myLang;
                }

                else if (ctl.HasChildren)
                {
                    ApplyResource(ctl);
                }
                else
                {
                    resources.ApplyResources(ctl, ctl.Name);
                }

            }
            this.ResumeLayout(false);
            this.PerformLayout();
            resources.ApplyResources(this, "$this");
        }

        #endregion
    }
}
