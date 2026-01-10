
using thinger.cn.BMSPro.Properties;
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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.cn.BMSModels;

namespace thinger.cn.BMSPro
{
    public partial class FrmCommSet : Form
    {
        public FrmCommSet(CommSet set)
        {
            InitializeComponent();

            InitialPort();

            this.cmb_Baud.DataSource = new string[] { "4800", "9600", "19200", "38400" };

            for (int i = 1; i < 11; i++)
            {
                this.cmb_DevAddress.Items.Add(i.ToString());
            }


            //读取配置
            this.cmb_Port.Text = set.PortName;
            this.cmb_Baud.Text = set.BaudRate.ToString();
            this.cmb_DevAddress.Text = set.DevAdd.ToString();
            this.txt_SleepTime.Text = set.SleepTime.ToString();
        }

        public SaveDefaultSettingDelegate SaveDefaultSetting;

        private void InitialPort()
        {
            string[] PortList = SerialPort.GetPortNames();

            if (PortList.Length > 0)
            {
                this.cmb_Port.DataSource = PortList;
            }
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            CommSet set = new CommSet();
            try
            {
                set.PortName = this.cmb_Port.Text;
                set.BaudRate = Convert.ToInt32(this.cmb_Baud.Text);
                set.DevAdd = Convert.ToInt32(this.cmb_DevAddress.Text);
                set.SleepTime = Convert.ToInt32(this.txt_SleepTime.Text);
            }
            catch (Exception)
            {
                CommonMethods.AddLog(1, "请检查数据格式是否正确",false,false);
                return;
            }

            SaveDefaultSetting(set);

            MessageBox.Show("配置成功，重启后生效", "配置成功");

        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            CommonMethods.isRestartExit = true;

            DialogResult dr = MessageBox.Show("是否确定要重启系统？", "重启系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                Application.Exit();

                Thread.Sleep(500);

                Process.Start(Assembly.GetExecutingAssembly().Location);
            }
        }

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
                            DriveInfo[] s = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    InitialPort();
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
                            InitialPort();
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
    }
}
