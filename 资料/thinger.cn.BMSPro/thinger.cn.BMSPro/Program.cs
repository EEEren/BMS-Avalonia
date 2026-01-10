
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.cn.BMSDAL;
using thinger.cn.BMSHelper;

namespace thinger.cn.BMSPro
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //获取欲启动进程名
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 2)
            {

                MessageBox.Show("BMS锂电池管理软件已经运行！", "系统运行", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
            else
            {
                //获取语言

                if (File.Exists(Application.StartupPath + "\\Settings\\System.ini"))
                {
                    CommonMethods.myLang = IniConfigHelper.ReadIniData("当前语言", "语言名称", "", Application.StartupPath + "\\Settings\\System.ini") == "中文" ? Language.Chinese : Language.English;
                }

                SQLiteService.SetConStr("Data Source=" + Application.StartupPath + "\\Database\\AXEBMSDataBase;Pooling=true;FailIfMissing=false");

                Application.Run(new FrmMain());
            }
        }
    }
}
