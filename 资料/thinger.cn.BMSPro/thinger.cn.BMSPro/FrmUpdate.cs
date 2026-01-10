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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xktComm;

namespace thinger.cn.BMSPro
{
    public partial class FrmUpdate : Form
    {
        public FrmUpdate()
        {
            InitializeComponent();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            this.Load += FrmUpdate_Load;

            this.FormClosing += FrmUpdate_FormClosing;
        }

        private void FrmUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormChange?.Invoke(FormName.固件更新, false);
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            this.FormChange?.Invoke(FormName.固件更新, true);
        }

        public FormChangeDelegate FormChange;

        private byte[] data;
        private void btn_Import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Multiselect = false;
                fileDialog.Filter = "BIN文件|*.bin";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    data = AuthGetFileData(fileDialog.FileName);

                    if (data != null && data.Length > 0)
                    {
                        this.pic_Import.Visible = true;
                    }
                    else
                    {
                        this.pic_Import.Image = Properties.Resources.error;
                        this.pic_Import.Visible = true;
                    }
                }
            }
        }

        private byte[] AuthGetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void btn_BootLoad_Click(object sender, EventArgs e)
        {
            if (CommonMethods.ModbusRTUList[0].IsConnected && data != null && data.Length > 0)
            {
                Task task1 = new Task(() =>
                {
                    UpdateProcess();
                }, TaskCreationOptions.LongRunning);

                task1.Start();
            }
            else
            {
                MessageBox.Show("当前未连接设备或未导入BIN文件，请检查！", "固件升级");
                return;
            }
        }

        /// <summary>
        /// 更新过程
        /// </summary>
        private void UpdateProcess()
        {
            DateTime t1 = DateTime.Now;

            int slaveID = CommonMethods.ModbusRTUList[0].ModbusRTUGroupList[0].SlaveID;

            //第一步：复位【次数定义为5】
            for (int i = 0; i < 6; i++)
            {
                //第五次连接时，直接返回false
                if (i == 5)
                {
                    Invoke(new Action(() =>
                    {
                        this.pic_Reset.Image = Properties.Resources.error;
                        this.pic_Reset.Visible = true;
                    }));
                    return;

                }
                if (CommonMethods.ModbusRTUList[0].modrtu.PreSetSingleReg(slaveID, 12288, 90))
                {
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    continue;
                }

            }
            Invoke(new Action(() =>
            {
                this.pic_Reset.Visible = true;
            }));

            //第二步：握手【时间定义为20s】
            for (int i = 0; i < 11; i++)
            {
                if (i == 10)
                {
                    Invoke(new Action(() =>
                    {
                        this.pic_Hand.Image = Properties.Resources.error;
                        this.pic_Hand.Visible = true;
                    }));
                    return;
                }
                byte[] res = CommonMethods.ModbusRTUList[0].modrtu.ReadKeepReg(slaveID, 65520, 1);

                if (res != null)
                {
                    if (res.Length == 2)
                    {
                        int result = ShortLib.GetShortFromByteArray(res);

                        if (result != 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }

            Invoke(new Action(() =>
            {
                this.pic_Hand.Visible = true;

            }));

            //第三步：更新
            Invoke(new Action(() =>
            {
                this.pic_Update.Image = Properties.Resources.loop;
                this.pic_Update.Visible = true;

            }));
            //确定更新次数
            int timer = data.Length / 128 + 1;

            for (int i = 0; i < timer; i++)
            {
                byte[] setByteArray = new byte[128];

                if (i == timer - 1)
                {
                    byte[] b1 = ByteArrayLib.GetByteArray(data, 128 * i, data.Length - 128 * i);
                    for (int j = 0; j < b1.Length; j++)
                    {
                        setByteArray[j] = b1[j];
                    }
                }
                else
                {
                    setByteArray = ByteArrayLib.GetByteArray(data, 128 * i, 128);
                }
                int address = 61440 + i;

                if (CommonMethods.ModbusRTUList[0].modrtu.PreSetMultiReg(slaveID, address, setByteArray) == false)
                {
                    Invoke(new Action(() =>
                    {
                        this.pic_Update.Image = Properties.Resources.error;
                    }));
                    return;
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        float f1 = Convert.ToSingle(i);
                        float f2 = Convert.ToSingle(timer - 1);
                        float value = (f1 / f2) * 100.0f;
                        int val = Convert.ToInt32(value);
                        this.lbl_Process.Text = val.ToString() + " %";
                        this.pe_Process.Value = val;
                        this.lbl_ExcuteTime.Text = DateDiffToString(ExecDateDiff(DateTime.Now, t1));

                    }));
                }
            }

            Invoke(new Action(() =>
            {
                this.pic_Update.Image = Properties.Resources.done;

            }));

            //第四步：跳转
            if (CommonMethods.ModbusRTUList[0].modrtu.PreSetSingleReg(slaveID, 65522, 91))
            {
                Invoke(new Action(() =>
                {
                    this.pic_Transfer.Visible = true;

                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    this.pic_Transfer.Image = Properties.Resources.error;
                    this.pic_Transfer.Visible = true;

                }));
                return;
            }

        }
        #region 时间处理
        private int ExecDateDiff(DateTime t1, DateTime t2)
        {
            TimeSpan ts1 = new TimeSpan(t1.Ticks);
            TimeSpan ts2 = new TimeSpan(t2.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            return Convert.ToInt32(ts3.TotalSeconds);
        }

        private string DateDiffToString(int Second)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(Second));

            return AutoAddZero(ts.Hours) + "：" + AutoAddZero(ts.Minutes) + "：" + AutoAddZero(ts.Seconds);
        }

        private string AutoAddZero(int val)
        {
            if (val >= 0 && val < 10)
            {
                return "0" + val.ToString();
            }
            else
            {
                return val.ToString();
            }
        }
        #endregion

    }
}
