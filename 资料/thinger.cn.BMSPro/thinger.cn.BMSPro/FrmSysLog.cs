
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
using thinger.cn.BMSDAL;
using xktNodeSettings.Node.Variable;
using Timer = System.Windows.Forms.Timer;

namespace thinger.cn.BMSPro
{
    public partial class FrmSysLog : Form
    {
        public FrmSysLog()
        {
            InitializeComponent();

            this.dpt_End.Value = DateTime.Now;
            this.dpt_Start.Value = DateTime.Now.AddHours(-1.0);

        }

        private SysLogService SysLogService = new SysLogService();

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (this.dgv_Log.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XLS文件(*.xls)|*.xls|All Files|*.*";//设置保存文件的类型

                sfd.FileName = "Report";
                sfd.DefaultExt = "xls";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (NiceExcelSaveAndRead.SaveToExcelNew(sfd.FileName, this.dgv_Log))
                    {
                        MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "Excel导出成功！":"Excel Export Successfully", CommonMethods.myLang == Language.Chinese ? "Excel导出":"Excel Export");
                    }
                    else
                    {
                        MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "Excel导出失败！" : "Excel Export Fail", CommonMethods.myLang == Language.Chinese ? "Excel导出" : "Excel Export");
                    }
                }
            }
            else
            {
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "当前无数据，无法进行Excel导出！" : "No Data , Please Check ", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
            }
        }

        private void btn_Recently50_Click(object sender, EventArgs e)
        {
           DataTable dt = SysLogService.GetSysLogByCount(50);

            if (dt != null && dt.Rows.Count > 0)
            {
                this.dgv_Log.DataSource = null;
                this.dgv_Log.DataSource = dt;
            }
            else
            {
                this.dgv_Log.DataSource = null;
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "未查询到数据，请检查！" : "No Data ", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
            }
        }

        private void btn_Recently100_Click(object sender, EventArgs e)
        {
            DataTable dt = SysLogService.GetSysLogByCount(100);

            if (dt != null && dt.Rows.Count > 0)
            {
                this.dgv_Log.DataSource = null;
                this.dgv_Log.DataSource = dt;
            }
            else
            {
                this.dgv_Log.DataSource = null;
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "未查询到数据，请检查！" : "No Data ", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
            }
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            //对于参数进行一定的判断

            DateTime t1 = this.dpt_Start.Value;
            DateTime t2 = this.dpt_End.Value;

            TimeSpan ts = t1 - t2;

            if (ts.TotalSeconds>=0)
            {
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "开始时间不能大于结束时间！" : "Start Time Can not Over Stop Time", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
                return;
            }


            DataTable dt = SysLogService.GetSysLogByTime(this.dpt_Start.Text, this.dpt_End.Text, this.cmb_LogType.SelectedIndex==0?"":this.cmb_LogType.SelectedIndex==1?"系统日志":"系统报警");

            if (dt != null && dt.Rows.Count > 0)
            {
                this.dgv_Log.DataSource = null;
                this.dgv_Log.DataSource = dt;
            }
            else
            {
                this.dgv_Log.DataSource = null;
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "未查询到数据，请检查！" : "No Data ", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "确定要清除数据，该操作不可恢复？" : "Are you sure to clear data?", CommonMethods.myLang == Language.Chinese ? "清除数据" : "Tips");

            if (dr == DialogResult.OK)
            {
                if (SysLogService.DeleteSysLog())
                {
                    MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "清除数据成功！" : "Clear Successfully", CommonMethods.myLang == Language.Chinese ? "清除数据" : "Tips");
                    this.dgv_Log.DataSource = null;

                }
                else
                {
                    MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "清除数据失败！" : "Clear Fail", CommonMethods.myLang == Language.Chinese ? "清除数据" : "Tips");
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.dgv_Log.Rows.Count > 0)
            {
                PrintDGV.Print_DataGridView(this.dgv_Log);
            }
            else
            {
                MessageBox.Show(CommonMethods.myLang == Language.Chinese ? "当前无数据，无法进行打印！" : "No Data , Please Check ", CommonMethods.myLang == Language.Chinese ? "查询提示" : "Tips");
            }
        }
    }
}
