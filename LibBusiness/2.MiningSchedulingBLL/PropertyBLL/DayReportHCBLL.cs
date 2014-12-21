// ******************************************************************
// 概  述：回采进尺日报业务逻辑
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
    public class DayReportHCBLL
    {
        #region ******SELECT******
        /// <summary>
        /// 查询所有回采进尺信息
        /// </summary>
        /// <returns>回采进尺信息</returns>
        public static DataSet selectDayReportHCInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查询时间段内回采进尺信息
        /// </summary>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public static DataSet selectDayReportHCInfo(DateTime dtFrom, DateTime dtTo)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " >= '" + dtFrom.ToString());
            sb.Append("' AND ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " < '" + dtTo.ToString() + "'");
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用回采进尺信息查询
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectDayReportHCInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + DayReportHCDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + DayReportHCDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用按时间查询
        /// </summary>
        /// <param name="iStartIndex">开始索引</param>
        /// <param name="iEndIndex">结束索引</param>
        /// <param name="dtFrom">开始日期</param>
        /// <param name="dtTo">结束日期</param>
        /// <returns></returns>
        public static DataSet selectDayReportHCInfo(int iStartIndex, int iEndIndex, DateTime dtFrom, DateTime dtTo)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + DayReportHCDbConstNames.ID + ") AS rowid, * ");
            sb.Append(" FROM " + DayReportHCDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " >= '" + dtFrom.ToString());
            sb.Append("' AND ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " < '" + dtTo.ToString() + "') AS TB ");
            sb.Append(" WHERE rowid >= " + iStartIndex);
            sb.Append(" AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 通过巷道ID查询回采日报
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns>回采日报实体</returns>
        public static DayReportHc selectDayReportHCByWorkFaceID(int workFaceID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + workFaceID;
            DataSet ds = db.ReturnDS(sql);
            DayReportHc dayReportHCEntity = new DayReportHc();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dayReportHCEntity.Id = Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.ID].ToString());
                dayReportHCEntity.TeamInfo = TeamInfo.FindById(Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.TEAM_NAME_ID].ToString()));
                dayReportHCEntity.WorkingFace.WorkingFaceID = Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORKINGFACE_ID].ToString());
                dayReportHCEntity.WorkTime = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_TIME].ToString();
                dayReportHCEntity.WorkTimeStyle = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_TIME_SYTLE].ToString();
                dayReportHCEntity.WorkInfo = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_INFO].ToString();
                dayReportHCEntity.JinChi = Convert.ToDouble(ds.Tables[0].Rows[0][DayReportHCDbConstNames.JIN_CHI].ToString());
                dayReportHCEntity.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0][DayReportHCDbConstNames.DATETIME].ToString());
                dayReportHCEntity.Submitter = ds.Tables[0].Rows[0][DayReportHCDbConstNames.SUBMITTER].ToString();
                dayReportHCEntity.Other = ds.Tables[0].Rows[0][DayReportHCDbConstNames.OTHER].ToString();
            }
            return dayReportHCEntity;
        }

        /// <summary>
        /// 所选巷道是否已在回采进尺日报中被选择
        /// </summary>
        /// <param name="tunnelEntity">巷道实体（必须包含巷道必填条件）</param>
        /// <returns>是否已在回采进尺日报中被选择？true:false</returns>
        public static bool isExistDayReportHCInfo(int workingFaceId)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + workingFaceId;
            DataSet ds = db.ReturnDS(sql);
            bool bResult;
            if (ds.Tables[0].Rows.Count > 0)
            {
                bResult = false;
            }
            else
            {
                bResult = true;
            }
            return bResult;
        }

        /// <summary>
        /// 返回最新一条数据距切眼距离
        /// </summary> 
        /// <param name="workfaceId">工作面Id</param>
        /// <returns></returns>
        public static double returnMaxRowOpenOffCutDistance(int workfaceId)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT " + DayReportHCDbConstNames.OPEN_OFF_CUT_DISTANCE);
            sb.Append(" FROM " + DayReportHCDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportHCDbConstNames.ID + " = ");
            sb.Append("(SELECT MAX(" + DayReportHCDbConstNames.ID + ")");
            sb.Append(" FROM ");
            sb.Append(DayReportHCDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportHCDbConstNames.WORKINGFACE_ID + " = ");
            sb.Append(workfaceId + ")");
            DataSet ds = db.ReturnDS(sb.ToString());
            double lastOpenOffCutDistance = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                lastOpenOffCutDistance = Convert.ToDouble(ds.Tables[0].Rows[0][DayReportHCDbConstNames.OPEN_OFF_CUT_DISTANCE].ToString());
            }
            return lastOpenOffCutDistance;
        }
        #endregion ******SELECT******

        #region ******INSERT******
        /// <summary>
        /// 插入回采进尺信息
        /// </summary>
        /// <param name="dayReportHCEntity">回采进尺实体</param>
        /// <returns>是否成功插入？true:false</returns>
        public static bool insertDayReportHCInfo(DayReportHc dayReportHCEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + DayReportHCDbConstNames.TABLE_NAME + " (");
            sqlStr.Append(DayReportHCDbConstNames.TEAM_NAME_ID + ", ");
            sqlStr.Append(DayReportHCDbConstNames.WORKINGFACE_ID + ", ");
            sqlStr.Append(DayReportHCDbConstNames.WORK_TIME + ", ");
            sqlStr.Append(DayReportHCDbConstNames.WORK_TIME_SYTLE + ", ");
            sqlStr.Append(DayReportHCDbConstNames.WORK_INFO + ", ");
            sqlStr.Append(DayReportHCDbConstNames.JIN_CHI + ",");
            sqlStr.Append(DayReportHCDbConstNames.OPEN_OFF_CUT_DISTANCE + ",");
            sqlStr.Append(DayReportHCDbConstNames.DATETIME + ", ");
            sqlStr.Append(DayReportHCDbConstNames.SUBMITTER + ", ");
            sqlStr.Append(DayReportHCDbConstNames.OTHER + ", ");
            sqlStr.Append(DayReportHCDbConstNames.BINDINGID);
            sqlStr.Append(") VALUES ('");
            sqlStr.Append(dayReportHCEntity.TeamInfo + "',");
            sqlStr.Append(dayReportHCEntity.WorkingFace + ",'");
            sqlStr.Append(dayReportHCEntity.WorkTime + "','");
            sqlStr.Append(dayReportHCEntity.WorkTimeStyle + "','");
            sqlStr.Append(dayReportHCEntity.WorkInfo + "',");
            sqlStr.Append(dayReportHCEntity.JinChi + ",");
            sqlStr.Append(0 + ",'");
            sqlStr.Append(dayReportHCEntity.DateTime + "','");
            sqlStr.Append(dayReportHCEntity.Submitter + "','");
            sqlStr.Append(dayReportHCEntity.Other + "','");
            sqlStr.Append(dayReportHCEntity.BindingId + "')");
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
        #endregion ******INSERT******

        #region ******UPDATE******
        /// <summary>
        /// 修改回采进尺信息
        /// </summary>
        /// <param name="dayReportHCEntity">回采进尺实体</param>
        /// <returns>是否成功修改？true:false</returns>
        public static bool updateDayReportHCInfo(DayReportHc dayReportHCEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + DayReportHCDbConstNames.TABLE_NAME + " SET " + DayReportHCDbConstNames.TEAM_NAME_ID + "='");
            sqlStr.Append(dayReportHCEntity.TeamInfo + "'," + DayReportHCDbConstNames.WORKINGFACE_ID + " = ");
            sqlStr.Append(dayReportHCEntity.WorkingFace + "," + DayReportHCDbConstNames.WORK_TIME + " = '");
            sqlStr.Append(dayReportHCEntity.WorkTime + "'," + DayReportHCDbConstNames.WORK_TIME_SYTLE + "= '");
            sqlStr.Append(dayReportHCEntity.WorkTimeStyle + "'," + DayReportHCDbConstNames.WORK_INFO + " ='");
            sqlStr.Append(dayReportHCEntity.WorkInfo + "'," + DayReportHCDbConstNames.JIN_CHI + " = " + dayReportHCEntity.JinChi + ",");
            sqlStr.Append(DayReportHCDbConstNames.DATETIME + " = '");
            sqlStr.Append(dayReportHCEntity.DateTime + "'," + DayReportHCDbConstNames.SUBMITTER + " = '");
            sqlStr.Append(dayReportHCEntity.Submitter + "'," + DayReportHCDbConstNames.OTHER + " = '");
            sqlStr.Append(dayReportHCEntity.Other + "' WHERE " + DayReportHCDbConstNames.ID + " = ");
            sqlStr.Append(dayReportHCEntity.Id);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dayReportHCEntity"></param>
        /// <returns></returns>
        public static bool updateDayResportHCInfoByBID(DayReportHc dayReportHCEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("UPDATE " + DayReportHCDbConstNames.TABLE_NAME + " SET ISDEL=");
            sqlstr.Append(dayReportHCEntity.IsDel + " WHERE " + DayReportHCDbConstNames.BINDINGID + "='" + dayReportHCEntity.BindingId + "'");
            bool bResult = db.OperateDB(sqlstr.ToString());
            return bResult;

        }
        #endregion ******UPDATE******

        #region ******DELETE******
        /// <summary>
        /// 删除回采进尺信息
        /// </summary>
        /// <param name="dayReportHCEntity">回采进尺实体</param>
        /// <returns>是否成功删除？true:false</returns>
        public static bool deleteDayReportHCInfo(DayReportHc dayReportHCEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.ID + " =" + dayReportHCEntity.Id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
        #endregion ******DELETE******
    }
}
