using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibBusiness;

namespace UnderTerminal
{
    class Utils
    {
        /// <summary>
        /// 返回当前时间对应的班次名
        /// </summary>
        /// <param name="workStyle">工作制式名</param>
        /// <returns>班次名</returns>
        public static string returnSysWorkTime(string workStyle)
        {
            //获取班次
            DataSet ds = WorkTimeBLL.returnWorkTime(workStyle);
            //小时
            int hour = DateTime.Now.Hour;
            string workTime = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //对比小时
                if (hour > Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString().Remove(2)) && hour <= Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString().Remove(2)))
                {
                    //获取当前时间对应班次
                    workTime = ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                }
            }
            return workTime;
        }
    }
}
