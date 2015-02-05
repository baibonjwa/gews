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
        /// 通过巷道ID查询回采日报
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns>回采日报实体</returns>
        //public static DayReportHc selectDayReportHCByWorkFaceID(int workFaceID)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
        //    string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + workFaceID;
        //    DataSet ds = db.ReturnDS(sql);
        //    DayReportHc dayReportHCEntity = new DayReportHc();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dayReportHCEntity.WirePointName = Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.ID].ToString());
        //        dayReportHCEntity.Team = Team.FindById(Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.TEAM_NAME_ID].ToString()));
        //        dayReportHCEntity.WorkingFace.WorkingFaceID = Convert.ToInt32(ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORKINGFACE_ID].ToString());
        //        dayReportHCEntity.WorkTime = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_TIME].ToString();
        //        dayReportHCEntity.WorkTimeStyle = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_TIME_SYTLE].ToString();
        //        dayReportHCEntity.WorkInfo = ds.Tables[0].Rows[0][DayReportHCDbConstNames.WORK_INFO].ToString();
        //        dayReportHCEntity.JinChi = Convert.ToDouble(ds.Tables[0].Rows[0][DayReportHCDbConstNames.JIN_CHI].ToString());
        //        dayReportHCEntity.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0][DayReportHCDbConstNames.DATETIME].ToString());
        //        dayReportHCEntity.Submitter = ds.Tables[0].Rows[0][DayReportHCDbConstNames.SUBMITTER].ToString();
        //        dayReportHCEntity.Other = ds.Tables[0].Rows[0][DayReportHCDbConstNames.OTHER].ToString();
        //    }
        //    return dayReportHCEntity;
        //}

        /// <summary>
        /// 所选巷道是否已在回采进尺日报中被选择
        /// </summary>
        /// <param name="tunnelEntity">巷道实体（必须包含巷道必填条件）</param>
        /// <returns>是否已在回采进尺日报中被选择？true:false</returns>
        //public static bool isExistDayReportHCInfo(int workingFaceId)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + workingFaceId;
        //    DataSet ds = db.ReturnDS(sql);
        //    bool bResult;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        bResult = false;
        //    }
        //    else
        //    {
        //        bResult = true;
        //    }
        //    return bResult;
        //}

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
    }
}
