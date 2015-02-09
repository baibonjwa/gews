// ******************************************************************
// 概  述：工作制式业务逻辑
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibDatabase;
using LibEntity;
using LibCommon;

namespace LibBusiness
{
    public class WorkTimeBLL
    {

        /// <summary>
        /// 获取班次
        /// </summary>
        /// <param name="workTime">制式</param>
        /// <returns>班次</returns>
        //public static DataSet returnWorkTime(string workTime)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
        //    string sql = "";
        //    if (workTime == Const_MS.WORK_TIME_38)
        //    {
        //        sql = "SELECT  * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = " + Const_MS.WORK_GROUP_ID_38;
        //    }
        //    if (workTime == Const_MS.WORK_TIME_46)
        //    {
        //        sql = "SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = " + Const_MS.WORK_GROUP_ID_46;
        //    }
        //    DataSet ds = db.ReturnDS(sql);
        //    return ds;
        //}

        /// <summary>
        /// 设置默认工作制式
        /// </summary>
        /// <param name="workTime">工作制式名</param>
        /// <returns>是否</returns>
        public static bool setDefaultWorkTime(string workTime)
        {
            int defaultWorkTimeGroupID;
            if (workTime == Const_MS.WORK_TIME_38)
            {
                defaultWorkTimeGroupID = Const_MS.WORK_GROUP_ID_38;
            }
            else
            {
                defaultWorkTimeGroupID = Const_MS.WORK_GROUP_ID_46;
            }
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + WorkTimeDefaultDbConstNames.TABLE_NAME;
            db.Open();
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sql = "UPDATE " + WorkTimeDefaultDbConstNames.TABLE_NAME + " SET " + WorkTimeDefaultDbConstNames.DEFAULT_WORK_TIME_GROUP_ID + " = " + defaultWorkTimeGroupID;
            }
            else
            {
                sql = "INSERT INTO " + WorkTimeDefaultDbConstNames.TABLE_NAME + " (" + WorkTimeDefaultDbConstNames.DEFAULT_WORK_TIME_GROUP_ID + ") VALUES (" + defaultWorkTimeGroupID + ")";
            }
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            db.Close();
            return bResult;
        }
        /// <summary>
        /// 获取默认工作制式
        /// </summary>
        /// <returns>工作制式(三八制：四六制)</returns>
        public static string getDefaultWorkTime()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + WorkTimeDefaultDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][WorkTimeDefaultDbConstNames.DEFAULT_WORK_TIME_GROUP_ID]) == 1)
                {
                    return Const_MS.WORK_TIME_38;
                }
                else
                {
                    return Const_MS.WORK_TIME_46;
                }
            }
            else
            {
                return Const_MS.WORK_TIME_46;
            }
        }
    }
}
