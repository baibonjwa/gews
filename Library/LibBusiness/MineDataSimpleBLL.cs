// ******************************************************************
// 概  述：根据系统当前时间、工作制式设计班次
// 作  者：伍鑫
// 创建日期：2014/05/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibBusiness.CommonBLL.DbConstNames;
using LibDatabase;

namespace LibBusiness
{
    public class MineDataSimpleBLL
    {
        /// <summary>
        /// 根据班次及系统当前时间获取系统当前时间对应的工作班次
        /// </summary>
        /// <param name="workTimeGroupId">班次ID</param>
        /// <param name="sysDateTime">系统时间</param>
        /// <returns>系统当前时间对应的工作班次</returns>
        public static string
            selectWorkTimeNameByWorkTimeGroupIdAndSysTime(int workTimeGroupId,
            string sysDateTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT " +
                MineDataSimpleDbConstNames.WORK_TIME_NAME);
            strSql.Append(" FROM " +
                MineDataSimpleDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(MineDataSimpleDbConstNames.WORK_TIME_GROUP_ID +
                " = " + workTimeGroupId);
            strSql.Append(" AND ");
            strSql.Append("'" + sysDateTime + "'" + " <= " +
                MineDataSimpleDbConstNames.WORK_TIME_TO);
            strSql.Append(" AND ");
            strSql.Append("'" + sysDateTime + "'" + " >= " +
                MineDataSimpleDbConstNames.WORK_TIME_FROM);

            ManageDataBase db = new
                ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return null;
        }
    }
}
