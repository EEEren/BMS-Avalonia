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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xktControl;

namespace thinger.cn.BMSPro
{
    public partial class FrmCalibration : Form
    {
        public FrmCalibration()
        {
            InitializeComponent();

            InitializeEvent();

            this.updateTimer = new System.Windows.Forms.Timer();
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;


            //中英文
            //语言初始化
            if (IsChinese)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

                ApplyResource(this);


            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

                ApplyResource(this);
            }
        }


        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmCalibration));

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



        //是否中文
        private bool IsChinese
        {
            get { return CommonMethods.myLang == Language.Chinese; }
            set { CommonMethods.myLang = value ? Language.Chinese : Language.English; }
        }

        private void InitializeEvent()
        {
            this.Load += FrmCalibration_Load;

            this.FormClosing += FrmCalibration_FormClosing;

            EventHandler(this.Panel_CurrentCalibration);
            EventHandler(this.Panel_EnergyCalibration);
            EventHandler(this.Panel_SOCCalibration);
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

        private void FrmCalibration_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormChange?.Invoke(FormName.校准参数, false);

            this.updateTimer.Enabled = false;
        }

        private void FrmCalibration_Load(object sender, EventArgs e)
        {
            this.FormChange?.Invoke(FormName.校准参数, true);
        }

        public FormChangeDelegate FormChange;


        private System.Windows.Forms.Timer updateTimer;

    }
}
