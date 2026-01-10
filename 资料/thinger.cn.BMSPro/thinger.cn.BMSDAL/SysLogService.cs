
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
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.cn.BMSModels;

namespace thinger.cn.BMSDAL
{
    public class SysLogService
    {
        public bool InsertSysLog(SysLog log)
        {
            string sql = "Insert into SysLog(LogTime,LogInfo,LogState,LogType) values(@LogTime,@LogInfo,@LogState,@LogType)";

            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@LogTime",log.LogTime),
                new SQLiteParameter("@LogInfo",log.LogInfo),
                new SQLiteParameter("@LogState",log.LogState),
                new SQLiteParameter("@LogType",log.LogType),
            };

            return SQLiteHelper.Update(sql, param) == 1;

        }


        public DataTable GetSysLogByTime(string start, string end, string LogType)
        {

            string sql = string.Empty;

            SQLiteParameter[] param;

            if (LogType.Length == 0)
            {
                sql = "Select LogTime,LogInfo,LogState,LogType from SysLog where LogTime between @Start and @End";

                param = new SQLiteParameter[]
                {
                new SQLiteParameter("@Start",start),
                new SQLiteParameter("@End",end),
                 };
            }
            else
            {
                sql = "Select LogTime,LogInfo,LogState,LogType from SysLog where LogTime between @Start and @End and LogType==@LogType";

                param = new SQLiteParameter[]
                 {
                new SQLiteParameter("@Start",start),
                new SQLiteParameter("@End",end),
                new SQLiteParameter("@LogType",LogType),
                  };
            }

            DataSet ds = SQLiteHelper.GetDataSet(sql, param);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

        }

        public DataTable GetSysLogByCount(int count)
        {

            string sql = "Select LogTime,LogInfo,LogState,LogType from SysLog order by LogTime DESC Limit @Count";

            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@Count",count),
             };

            DataSet ds = SQLiteHelper.GetDataSet(sql, param);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }

        }

        public bool DeleteSysLog()
        {
            string sql = "Delete from SysLog";

            return SQLiteHelper.Update(sql) > 0;

        }

    }
}
