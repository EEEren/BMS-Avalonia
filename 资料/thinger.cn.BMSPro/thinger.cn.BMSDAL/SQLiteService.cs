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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.cn.BMSDAL
{
    public class SQLiteService
    {
        public static void SetConStr(string ConStr)
        {
            SQLiteHelper.ConStr = ConStr;
        }
    }
}
