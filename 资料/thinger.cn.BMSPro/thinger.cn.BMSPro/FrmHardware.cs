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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thinger.cn.BMSPro
{
    public partial class FrmHardware : Form
    {
        public FrmHardware()
        {
            InitializeComponent();

            InitializeEvent();

            this.updateTimer = new Timer();
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;
        }

        private void InitializeEvent()
        {
            this.Load += FrmHardware_Load;

            this.FormClosing += FrmHardware_FormClosing;

            EventHandler(this.panel_CommParam);
            EventHandler(this.panel_DeviceParam);
            EventHandler(this.panel_SerialParam);
        }

        private void EventHandler(Control ctl)
        {
            foreach (Control item in ctl.Controls)
            {
                if (item is Label lbl)
                {
                    if (lbl.Tag != null && lbl.Tag.ToString().Length > 0)
                    {
                        string tag = lbl.Tag.ToString();
                        if (CommonMethods.CurrentRTUValue.ContainsKey(tag))
                        {
                            lbl.DoubleClick += Lbl_DoubleClick;
                        }
                    }
                }

                else if (item.HasChildren)
                {
                    EventHandler(item);
                }
            }
        }

        private void Lbl_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                if (lbl.Tag != null && lbl.Tag.ToString().Length > 0)
                {
                    if (CommonMethods.ModbusRTUList[0].IsConnected)
                    {
                        new FrmParamSet(lbl.Tag.ToString()).ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("参数修改失败，设备未连接！", "参数修改");
                    }
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (CommonMethods.ModbusRTUList[0].IsConnected)
            {
                UpdateInfo(this);
            }
        }

        private void UpdateInfo(Control ctl)
        {
            foreach (Control item in ctl.Controls)
            {
                if (item is Label lbl)
                {
                    if (lbl.Tag != null && lbl.Tag.ToString().Length > 0)
                    {
                        string tag = lbl.Tag.ToString();
                        if (CommonMethods.CurrentRTUValue.ContainsKey(tag))
                        {
                            lbl.Text = CommonMethods.CurrentRTUValue[tag].ToString();
                        }
                    }
                }

                else if (item.HasChildren)
                {
                    UpdateInfo(item);
                }
            }
        }


        private void FrmHardware_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormChange?.Invoke(FormName.硬件参数, false);

            this.updateTimer.Enabled = false;
        }

        private void FrmHardware_Load(object sender, EventArgs e)
        {
            this.FormChange?.Invoke(FormName.硬件参数, true);
        }

        public FormChangeDelegate FormChange;

        private Timer updateTimer;

    }
}
