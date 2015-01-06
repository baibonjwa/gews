
using System;
using System.Text;
using System.Data;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class DayReportJJBLL
    {

        /// <summary>
        /// 查询掘进管理信息
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static DataSet selectDayReportJJInfo(int workingFaceID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + DayReportJJDbConstNames.TABLE_NAME +
                " WHERE " + DayReportJJDbConstNames.WORKINGFACE_ID + " = " + workingFaceID;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查询时间段内掘进进尺信息
        /// </summary>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public static DataSet selectDayReportJJInfo(DateTime dtFrom, DateTime dtTo)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + DayReportJJDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportJJDbConstNames.DATETIME + " >= '" + dtFrom.ToString());
            sb.Append("' AND ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " < '" + dtTo.ToString() + "'");
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 所选巷道是否已在掘进进尺日报中被选择
        /// </summary>
        /// <param name="tunnelEntity">巷道实体（必须包含巷道必填条件）</param>
        /// <returns>是否已在掘进进尺日报中被选择？true:false</returns>
        public static bool selectDayReportJJInfoTunnelInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.Append(DayReportJJDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportJJDbConstNames.WORKINGFACE_ID + " =  " + tunnelEntity.TunnelId);
            DataSet ds = db.ReturnDS(sb.ToString());
            bool bResult;
            if (ds.Tables[0].Rows.Count > 0)
            {
                bResult = true;
            }
            else
            {
                bResult = false;
            }
            return bResult;
        }

        /// <summary>
        /// 分页用掘进进尺信息查询
        /// </summary>
        /// <param name="iStartIndex">开始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <returns>查询所有数据</returns>
        public static DataSet selectDayReportJJInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + DayReportJJDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + DayReportJJDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            //string sql = "SELECT * FROM T_DAYREPORT_JJ";
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用按时间查询
        /// </summary>
        /// <param name="iStartIndex">开始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <param name="dtFrom">开始时间</param>
        /// <param name="dtTo">结束时间</param>
        /// <returns>时间段内查询所有数据</returns>
        public static DataSet selectDayReportJJInfo(int iStartIndex, int iEndIndex, DateTime dtFrom, DateTime dtTo)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + DayReportJJDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + DayReportJJDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportJJDbConstNames.DATETIME + " >= '" + dtFrom.ToString());
            sb.Append("' AND ");
            sb.Append(DayReportHCDbConstNames.DATETIME + " < '" + dtTo.ToString() + "') AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 删除回采进尺日报信息
        /// </summary>
        /// <param name="dayReportJJEntity">回采进尺日报实体(含主键)</param>
        /// <returns></returns>
        //public static bool deleteDayReportJJInfo(DayReportJj dayReportJJEntity)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
        //    string sql = "DELETE FROM " + DayReportJJDbConstNames.TABLE_NAME + " WHERE " + DayReportJJDbConstNames.ID + " =" + dayReportJJEntity.Id;
        //    bool bResult = db.OperateDB(sql);
        //    return bResult;
        //}

        /// <summary>
        /// 返回最新一条数据的距参考导线点距离
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static DayReportJj returnMaxRowDistanceFromWirepoint(int tunnelID)
        {
            DayReportJj dayReportJJEntity = new DayReportJj();
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + DayReportJJDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportJJDbConstNames.ID + " = ");
            sb.Append("(SELECT MAX(" + DayReportJJDbConstNames.ID + ")");
            sb.Append(" FROM ");
            sb.Append(DayReportJJDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(DayReportJJDbConstNames.WORKINGFACE_ID + " = ");
            sb.Append(tunnelID + ")");
            DataSet ds = db.ReturnDS(sb.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                int tmpInt = 0;
                double tmpDouble = 0;
                if (int.TryParse(ds.Tables[0].Rows[0][DayReportJJDbConstNames.CONSULT_WIREPOINT_ID].ToString(), out tmpInt))
                {
                    dayReportJJEntity.ConsultWirepoint = tmpInt;
                }
                if (double.TryParse(ds.Tables[0].Rows[0][DayReportJJDbConstNames.DISTANCE_FROM_WIREPOINT].ToString(), out tmpDouble))
                {
                    dayReportJJEntity.DistanceFromWirepoint = tmpDouble;
                }
            }
            return dayReportJJEntity;
        }
    }
}
