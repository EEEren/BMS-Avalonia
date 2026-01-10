
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.cn.BMSHelper;
using thinger.cn.BMSModels;
using xktNodeSettings.Node.Variable;
using Timer = System.Windows.Forms.Timer;

namespace thinger.cn.BMSPro
{
    public partial class FrmProtect : Form
    {
        public FrmProtect()
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
            this.Load += FrmProtect_Load;

            this.FormClosing += FrmProtect_FormClosing;

            EventHandler(this.Panel_Param1);
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

        private void FrmProtect_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormChange?.Invoke(FormName.保护参数, false);

            this.updateTimer.Enabled = false;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (CommonMethods.ModbusRTUList[0].IsConnected)
            {
                UpdateInfo(this);
            }
        }

        private void FrmProtect_Load(object sender, EventArgs e)
        {
            this.FormChange?.Invoke(FormName.保护参数, true);
        }

        public FormChangeDelegate FormChange;

        private Timer updateTimer;

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
        private List<ParamSet> ConfigList = new List<ParamSet>();
        private void btn_ImportConfig_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Multiselect = false;
                fileDialog.Filter = "CFG文件|*.cfg";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ConfigList = GetConfigInfo(fileDialog.FileName);
                }
            }
        }

        #region  获取通讯信息
        /// <summary>
        /// 获取通讯信息
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<ParamSet> GetConfigInfo(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string str = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return JSONHelper.JSONToEntity<List<ParamSet>>(str);
        }

        #endregion

        #region 一键更新
        /// <summary>
        /// 一键更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OneKeyUpdate_Click(object sender, EventArgs e)
        {
            if (CommonMethods.ModbusRTUList[0].IsConnected)
            {
                if (ConfigList.Count > 0)
                {
                    bool res = false;
                    this.pic_process.Image = Properties.Resources.load;
                    this.pic_process.Visible = true;
                    Task task1 = new Task(() =>
                    {
                        res = UpdateConfig();
                    }, TaskCreationOptions.LongRunning);

                    //任务2
                    Task task2 = task1.ContinueWith((task) =>
                    {
                        Invoke(new Action(() =>
                        {
                            if (res)
                            {
                                this.pic_process.Image = Properties.Resources.done;
                            }
                            else
                            {
                                this.pic_process.Image = Properties.Resources.error;
                            }
                        }));


                    });
                    Task task3 = task2.ContinueWith((task) =>
                    {
                        Thread.Sleep(2000);
                        Invoke(new Action(() =>
                        {
                            this.pic_process.Visible = false;
                            ConfigList.Clear();
                        }));
                    });

                    task1.Start();
                }
                else
                {
                    MessageBox.Show("请先导入正确的配置文件", "更新配置");
                }
            }
            else
            {
                MessageBox.Show("设备未连接", "更新配置");
            }
        }

        private bool UpdateConfig()
        {
            bool res = true;
            foreach (var var in ConfigList)
            {
                res = res & CommonMethods.CommonWrite(var.VarName, var.VarValue);
                Thread.Sleep(10);
            }
            return true;
        }

        #endregion

        #region 导出配置
        List<ParamSet> exportList = new List<ParamSet>();
        private void btn_Export_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CFG文件|*.cfg";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    exportList = new List<ParamSet>();

                    GetExportVar(this.Panel_Param1);

                    if (exportList.Count == 0)
                    {
                        MessageBox.Show("导出配置文件失败：未查询到变量", "导出配置");
                        return;
                    }

                    if (SaveConfig(JSONHelper.EntityToJSON(exportList), dialog.FileName))
                    {
                        MessageBox.Show("导出配置文件成功！", "导出配置");
                    }
                    else
                    {
                        MessageBox.Show("导出配置文件失败！", "导出配置");
                    }
                }
            }
        }

        private void GetExportVar(Control ctrl)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is Label lb)
                {
                    if (lb.Tag != null && lb.Tag.ToString().Length > 0)
                    {
                        string tag = lb.Tag.ToString();
                        string des = string.Empty;
                        if (CommonMethods.CurrentRTUVarList.ContainsKey(tag))
                        {
                            des = CommonMethods.CurrentRTUVarList[tag].Description;
                        }

                        exportList.Add(new ParamSet()
                        {
                            VarName = tag,
                            VarDesc = des,
                            VarValue = lb.Text.Trim()
                        });
                    }
                }

                if (item.HasChildren)
                {
                    GetExportVar(item);
                }
            }
        }

        private bool SaveConfig(string info, string path)
        {
            FileStream fs;
            StreamWriter sw;
            try
            {
                //保存协议信息
                fs = new FileStream(path, FileMode.Create);
                sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(info);
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        #endregion

    }
}
