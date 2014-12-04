// ******************************************************************
// 概  述：井下数据地质构造业务逻辑
// 作  者：宋英杰
// 创建日期：2014/03/25
// 版本号：1.0
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
    public class GeologicStructureBLL
    {
        /// <summary>
        /// 获取全部地质构造信息
        /// </summary>
        /// <returns>全部管理信息</returns>
        public static DataSet selectGeologicStructure()
        {
            string sqlStr = "SELECT * FROM " + GeologicStructureDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }


        /// <summary>
        /// 获取全部地质构造信息带条件
        /// </summary>
        /// <returns>全部管理信息</returns>
        public static DataSet selectGeologicStructureWithCondition(int iTunnelId, string startTime, string endTime)
        {
            string sqlStr = "SELECT * FROM " + GeologicStructureDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + GeologicStructureDbConstNames.TUNNEL_ID + " = " + iTunnelId;
            sqlStr += " AND " + GeologicStructureDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'";
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
        public static DataSet selectGeologicStructure(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + GeologicStructureDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + GeologicStructureDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用查询带条件
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <param name="iTunnelId">巷道Id</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>数据</returns>
        public static DataSet selectGeologicStructureWithCondition(int iStartIndex, int iEndIndex, int iTunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + GeologicStructureDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + GeologicStructureDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + GeologicStructureDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sb.Append(" AND " + GeologicStructureDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");
            sb.Append(" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append(" AND rowid <= " + iEndIndex);


            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 插入地质构造信息
        /// </summary>
        /// <param name="geologicStructureEntity">地质构造实体</param>
        /// <returns>是否成功插入？true:false</returns>
        public static bool insertGeologicStructure(GeologicStructure geologicStructureEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + GeologicStructureDbConstNames.TABLE_NAME + " (" + PreWarningDataCommonBLL.sqlFront() + "," + GeologicStructureDbConstNames.NO_PLAN_STRUCTURE + "," + GeologicStructureDbConstNames.PASSED_STRUCTURE_RULE_INVALID + "," + GeologicStructureDbConstNames.YELLOW_RULE_INVALID + "," + GeologicStructureDbConstNames.ROOF_BROKEN + "," + GeologicStructureDbConstNames.COAL_SEAM_SOFT + "," + GeologicStructureDbConstNames.COAL_SEAM_BRANCH + "," + GeologicStructureDbConstNames.ROOF_CHANGE + "," + GeologicStructureDbConstNames.GANGUE_DISAPPEAR + "," + GeologicStructureDbConstNames.GANGUE_LOCATION_CHANGE + ") VALUES(");
            sb.Append(PreWarningDataCommonBLL.sqlBack(geologicStructureEntity));
            sb.Append("," + geologicStructureEntity.NoPlanStructure + ",");
            sb.Append(geologicStructureEntity.PassedStructureRuleInvalid + ",");
            sb.Append(geologicStructureEntity.YellowRuleInvalid + ",");
            sb.Append(geologicStructureEntity.RoofBroken + ",");
            sb.Append(geologicStructureEntity.CoalSeamSoft + ",");
            sb.Append(geologicStructureEntity.CoalSeamBranch + ",");
            sb.Append(geologicStructureEntity.RoofChange + ",");
            sb.Append(geologicStructureEntity.GangueDisappear + ",");
            sb.Append(geologicStructureEntity.GangueLocationChange + ")");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改地质构造信息
        /// </summary>
        /// <param name="geologicStructureEntity">地质构造实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateGeologicStructure(GeologicStructure geologicStructureEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + GeologicStructureDbConstNames.TABLE_NAME + " SET " + PreWarningDataCommonBLL.sqlUpdate(geologicStructureEntity) + "," + GeologicStructureDbConstNames.NO_PLAN_STRUCTURE + "=");
            sb.Append(geologicStructureEntity.NoPlanStructure + "," + GeologicStructureDbConstNames.PASSED_STRUCTURE_RULE_INVALID + "=");
            sb.Append(geologicStructureEntity.PassedStructureRuleInvalid + "," + GeologicStructureDbConstNames.YELLOW_RULE_INVALID + "=");
            sb.Append(geologicStructureEntity.YellowRuleInvalid + "," + GeologicStructureDbConstNames.ROOF_BROKEN + "=");
            sb.Append(geologicStructureEntity.RoofBroken + "," + GeologicStructureDbConstNames.COAL_SEAM_SOFT + "=");
            sb.Append(geologicStructureEntity.CoalSeamSoft + "," + GeologicStructureDbConstNames.COAL_SEAM_BRANCH + "=");
            sb.Append(geologicStructureEntity.CoalSeamBranch + "," + GeologicStructureDbConstNames.ROOF_CHANGE + "=");
            sb.Append(geologicStructureEntity.RoofChange + "," + GeologicStructureDbConstNames.GANGUE_DISAPPEAR + "=");
            sb.Append(geologicStructureEntity.GangueDisappear + "," + GeologicStructureDbConstNames.GANGUE_LOCATION_CHANGE + "=");
            sb.Append(geologicStructureEntity.GangueLocationChange + " WHERE " + GeologicStructureDbConstNames.ID + "=");
            sb.Append(geologicStructureEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除地质构造信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteGeologicStructure(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + GeologicStructureDbConstNames.TABLE_NAME + " WHERE " + GeologicStructureDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
