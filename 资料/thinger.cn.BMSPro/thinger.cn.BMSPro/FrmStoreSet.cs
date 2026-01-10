
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
    public partial class FrmStoreSet : Form
    {
        public FrmStoreSet(StoreSet set)
        {
            InitializeComponent();

            //读取配置
            this.txt_StorePath.Text = set.StorePath;
            this.chk_UseDefault.Checked = set.UseDefault;
            this.num_StoreTime.Text = set.StoreTime.ToString();
        }

        public SaveStoreSettingDelegate SaveDefaultSetting;

        public StoreSet set = new StoreSet();

        private void btn_Set_Click(object sender, EventArgs e)
        {
            try
            {
                set.StorePath = this.txt_StorePath.Text.Length==0?Application.StartupPath:this.txt_StorePath.Text;
                set.UseDefault = this.chk_UseDefault.Checked;
                set.StoreTime = Convert.ToInt32(this.num_StoreTime.Text);
            }
            catch (Exception)
            {
                CommonMethods.AddLog(1, "请检查数据格式是否正确",false,false);
                return;
            }

            SaveDefaultSetting(set);

            MessageBox.Show("配置成功，立即生效", "配置成功");

            this.DialogResult = DialogResult.OK;

            this.Close();

        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.txt_StorePath.Text = path.SelectedPath;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
