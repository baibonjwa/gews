// ******************************************************************
// 概  述：井下数据管理业务逻辑
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

namespace LibBusiness
{
    public class ManagementBLL
    {
        /// <summary>
        /// 获取全部管理信息
        /// </summary>
        /// <returns>全部管理信息</returns>
        public static DataSet selectManagement()
        {
            string sqlStr = "SELECT * FROM " + ManagementDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取全部管理信息
        /// </summary>
        /// <returns>全部管理信息</returns>
        public static DataSet selectManagementWithCondition(int iTunnelId, string startTime, string endTime)
        {
            string sqlStr = "SELECT * FROM " + ManagementDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + GasDataDbConstNames.TUNNEL_ID + " = " + iTunnelId;
            sqlStr += " AND " + GasDataDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'";

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectManagement(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + ManagementDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + ManagementDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }


        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectManagementWithCondition(int iStartIndex, int iEndIndex, int iTunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + ManagementDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + ManagementDbConstNames.TABLE_NAME);

            sb.Append(" WHERE " + GeologicStructureDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sb.Append(" AND " + GeologicStructureDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");

            sb.Append(" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }


        /// <summary>
        /// 插入管理信息
        /// </summary>
        /// <param name="mEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>管理信息</returns>
        public static bool insertManagementInfo(Management mEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + ManagementDbConstNames.TABLE_NAME + " (");
            sb.Append(PreWarningDataCommonBLL.sqlFront() + ",");
            sb.Append(ManagementDbConstNames.IS_GAS_ERROR_NOT_REPORT + ",");
            sb.Append(ManagementDbConstNames.IS_WF_NOT_REPORT + ",");
            sb.Append(ManagementDbConstNames.IS_STRGAS_NOT_DO_WELL + ",");
            sb.Append(ManagementDbConstNames.IS_RWMANAGEMENT_NOT_DO_WELL + ",");
            sb.Append(ManagementDbConstNames.IS_VF_BROKEN_BY_PEOPLE + ",");
            sb.Append(ManagementDbConstNames.IS_ELEMENT_PLACE_NOT_GOOD + ",");
            sb.Append(ManagementDbConstNames.IS_REPORTER_FALSE_DATA + ",");
            sb.Append(ManagementDbConstNames.IS_DRILL_WRONG_BUILD + ",");
            sb.Append(ManagementDbConstNames.IS_DRILL_NOT_DO_WELL + ",");
            sb.Append(ManagementDbConstNames.IS_OP_NOT_DO_WELL + ",");
            sb.Append(ManagementDbConstNames.IS_OP_ERROR_NOT_REPORT + ",");
            sb.Append(ManagementDbConstNames.IS_PART_WIND_SWITCH_ERROR + ",");
            sb.Append(ManagementDbConstNames.IS_SAFE_CTRL_UNINSTALL + ",");
            sb.Append(ManagementDbConstNames.IS_CTRL_STOP + ",");
            sb.Append(ManagementDbConstNames.IS_GAS_NOT_DO_WELL + ",");
            sb.Append(ManagementDbConstNames.IS_MINE_NO_CHECKER + ") VALUES(");
            sb.Append(PreWarningDataCommonBLL.sqlBack(mEntity));
            sb.Append("," + mEntity.IsGasErrorNotReport + ",");
            sb.Append(mEntity.IsWFNotReport + ",");
            sb.Append(mEntity.IsStrgasNotDoWell + ",");
            sb.Append(mEntity.IsRwmanagementNotDoWell + ",");
            sb.Append(mEntity.IsVFBrokenByPeople + ",");
            sb.Append(mEntity.IsElementPlaceNotGood + ",");
            sb.Append(mEntity.IsReporterFalseData + ",");
            sb.Append(mEntity.IsDrillWrongBuild + ",");
            sb.Append(mEntity.IsDrillNotDoWell + ",");
            sb.Append(mEntity.IsOPNotDoWell + ",");
            sb.Append(mEntity.IsOPErrorNotReport + ",");
            sb.Append(mEntity.IsPartWindSwitchError + ",");
            sb.Append(mEntity.IsSafeCtrlUninstall + ",");
            sb.Append(mEntity.IsCtrlStop + ",");
            sb.Append(mEntity.IsGasNotDowWell + ",");
            sb.Append(mEntity.IsMineNoChecker + ")");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改管理信息
        /// </summary>
        /// <param name="mEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateManagementInfo(Management mEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + ManagementDbConstNames.TABLE_NAME + " SET " + ManagementDbConstNames.TUNNEL_ID + "=");
            sb.Append(mEntity.Tunnel.TunnelId + "," + ManagementDbConstNames.X + "=");
            sb.Append(mEntity.CoordinateX + "," + ManagementDbConstNames.Y + "=");
            sb.Append(mEntity.CoordinateY + "," + ManagementDbConstNames.Z + "=");
            sb.Append(mEntity.CoordinateZ + "," + ManagementDbConstNames.DATETIME + "='");
            sb.Append(mEntity.Datetime + "'," + ManagementDbConstNames.IS_GAS_ERROR_NOT_REPORT + "=");
            sb.Append(mEntity.IsGasErrorNotReport + "," + ManagementDbConstNames.IS_WF_NOT_REPORT + "=");
            sb.Append(mEntity.IsWFNotReport + "," + ManagementDbConstNames.IS_STRGAS_NOT_DO_WELL + "=");
            sb.Append(mEntity.IsStrgasNotDoWell + "," + ManagementDbConstNames.IS_RWMANAGEMENT_NOT_DO_WELL + "=");
            sb.Append(mEntity.IsRwmanagementNotDoWell + "," + ManagementDbConstNames.IS_VF_BROKEN_BY_PEOPLE + "=");
            sb.Append(mEntity.IsVFBrokenByPeople + "," + ManagementDbConstNames.IS_ELEMENT_PLACE_NOT_GOOD + "=");
            sb.Append(mEntity.IsElementPlaceNotGood + "," + ManagementDbConstNames.IS_REPORTER_FALSE_DATA + "=");
            sb.Append(mEntity.IsReporterFalseData + "," + ManagementDbConstNames.IS_DRILL_WRONG_BUILD + "=");
            sb.Append(mEntity.IsDrillWrongBuild + "," + ManagementDbConstNames.IS_DRILL_NOT_DO_WELL + "=");
            sb.Append(mEntity.IsDrillNotDoWell + "," + ManagementDbConstNames.IS_OP_NOT_DO_WELL + "=");
            sb.Append(mEntity.IsOPNotDoWell + "," + ManagementDbConstNames.IS_OP_ERROR_NOT_REPORT + "=");
            sb.Append(mEntity.IsOPErrorNotReport + "," + ManagementDbConstNames.IS_PART_WIND_SWITCH_ERROR + "=");
            sb.Append(mEntity.IsPartWindSwitchError + "," + ManagementDbConstNames.IS_SAFE_CTRL_UNINSTALL + "=");
            sb.Append(mEntity.IsSafeCtrlUninstall + "," + ManagementDbConstNames.IS_CTRL_STOP + "=");
            sb.Append(mEntity.IsCtrlStop + "," + ManagementDbConstNames.IS_GAS_NOT_DO_WELL + "=");
            sb.Append(mEntity.IsGasNotDowWell + "," + ManagementDbConstNames.IS_MINE_NO_CHECKER + "=");
            sb.Append(mEntity.IsMineNoChecker + " WHERE " + ManagementDbConstNames.ID + "=");
            sb.Append(mEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除管理信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteManagementInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + ManagementDbConstNames.TABLE_NAME + " WHERE " + ManagementDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
