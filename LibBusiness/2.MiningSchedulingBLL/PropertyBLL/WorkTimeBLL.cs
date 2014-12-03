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
        /// 登入工作时间
        /// </summary>
        /// <param name="ds">工作时间</param>
        /// <returns>是否成功登入：true,false</returns>
        public static bool insertWorkTime(DataSet ds)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            bool bResult = true;
            if (ds.Tables[0].Rows[0][WorkTimeDbConstNames.WORK_TIME_ID].ToString() != "")
            {
                //Alert.alert(ds.Tables[0].Rows[0]["WORK_TIME_ID"].ToString());
                db.Open();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("UPDATE " + WorkTimeDbConstNames.TABLE_NAME + " SET " + WorkTimeDbConstNames.WORK_TIME_NAME + " = '");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString() + "'," + WorkTimeDbConstNames.WORK_TIME_FROM + " ='");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString() + "'," + WorkTimeDbConstNames.WORK_TIME_TO + " ='");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString() + "' WHERE " + WorkTimeDbConstNames.WORK_TIME_ID + " =");
                    sqlStr.Append(Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_ID]) + " AND " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " =");
                    sqlStr.Append((int)(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID]));
                    bool bbResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
                    bResult = (bResult && bbResult);
                }
                db.Close();
            }
            else
            {
                db.Open();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("INSERT INTO " + WorkTimeDbConstNames.TABLE_NAME + " (" + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + "," + WorkTimeDbConstNames.WORK_TIME_NAME + "," + WorkTimeDbConstNames.WORK_TIME_FROM + "," + WorkTimeDbConstNames.WORK_TIME_TO + ") VALUES (");
                    sqlStr.Append((int)(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_GROUP_ID]) + ",'");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString() + "','");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString() + "','");
                    sqlStr.Append(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString() + "')");
                    bool bbResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
                    bResult = (bResult && bbResult);
                }
                db.Close();
            }
            return bResult;
        }

        /// <summary>
        /// 返回工作时间信息
        /// </summary>
        /// <returns>工作时间</returns>
        public static DataSet returnWorkTime38DS()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " =1");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 返回工作时间信息
        /// </summary>
        /// <returns>工作时间</returns>
        public static DataSet returnWorkTime46DS()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " =2");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 检查是否存在38制工作时间
        /// </summary>
        /// <returns>检查结果:true,false</returns>
        public static bool isWorkTime38Exist()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = 1");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows.Count) > 0 ? true : false;
        }

        /// <summary>
        /// 检查是否存在46制工作时间
        /// </summary>
        /// <returns>检查结果:true,false</returns>
        public static bool isWorkTime46Exist()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = 2");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows.Count) > 0 ? true : false;
        }

        /// <summary>
        /// 获取班次
        /// </summary>
        /// <param name="workTime">制式</param>
        /// <returns>班次</returns>
        public static DataSet returnWorkTime(string workTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "";
            if (workTime == Const_MS.WORK_TIME_38)
            {
                sql = "SELECT  * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = " + Const_MS.WORK_GROUP_ID_38;
            }
            if (workTime == Const_MS.WORK_TIME_46)
            {
                sql = "SELECT * FROM  " + WorkTimeDbConstNames.TABLE_NAME + " WHERE  " + WorkTimeDbConstNames.WORK_TIME_GROUP_ID + " = " + Const_MS.WORK_GROUP_ID_46;
            }
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }
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

        /// <summary>
        /// 清空班次表及默认班次表数据
        /// </summary>
        public static void truncateWorkTime()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            db.Open();
            string sql = "TRUNCATE TABLE " + WorkTimeDbConstNames.TABLE_NAME;
            db.OperateDBNotOpenAndClose(sql);
            sql = "TRUNCATE TABLE "+WorkTimeDbConstNames.DEFAULT_WORK_TIME_TABLE_NAME;
            db.OperateDBNotOpenAndClose(sql);
            db.Close();
        }
    }
}
