using Newtonsoft.Json;
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

namespace thinger.cn.BMSHelper
{
    public class JSONHelper
    {
        /// <summary>
        /// 实体对象转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string EntityToJSON<T>(T x)
        {
            string result = string.Empty;

            try
            {
                result = JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;

        }

        /// <summary>
        /// JSON字符串转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JSONToEntity<T>(string json)
        {
            T t = default(T);
            try
            {
                t = (T)JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch (Exception)
            {
                t = default(T);
            }

            return t;
        }

    }
}
