// ******************************************************************
// 概  述：井下数据通风业务逻辑
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
    public class VentilationBLL
    {
        /// <summary>
        /// 获取全部通风信息
        /// </summary>
        /// <returns>全部通风信息</returns>
        public static DataSet selectVentilationInfo()
        {
            string sqlStr = "SELECT * FROM " + VentilationDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取全部通风信息
        /// </summary>
        /// <returns>全部通风信息</returns>
        public static DataSet selectVentilationInfoWithCondition(int iTunnelId, string startTime, string endTime)
        {
            string sqlStr = "SELECT * FROM " + VentilationDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + VentilationDbConstNames.TUNNEL_ID + " = " + iTunnelId;
            sqlStr += " AND " + VentilationDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取某巷道通风信息
        /// </summary>
        /// <returns>通风信息</returns>
        public static VentilationInfo selectVentilationInfo(int id)
        {
            string sqlStr = "SELECT * FROM " + VentilationDbConstNames.TABLE_NAME + " WHERE " + VentilationDbConstNames.ID + " = " + id;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            VentilationInfo vEntity = new VentilationInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                vEntity.Id = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.ID].ToString());
                vEntity.Tunnel.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.TUNNEL_ID].ToString());
                string str = ds.Tables[0].Rows[0][VentilationDbConstNames.FAULTAGE_AREA].ToString();
                vEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][VentilationDbConstNames.COORDINATE_X].ToString());
                vEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][VentilationDbConstNames.COORDINATE_Y].ToString());
                vEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][VentilationDbConstNames.COORDINATE_Z].ToString());
                vEntity.WorkStyle = ds.Tables[0].Rows[0][GasDataDbConstNames.WORK_STYLE].ToString();
                vEntity.WorkTime = ds.Tables[0].Rows[0][GasDataDbConstNames.WORK_TIME].ToString();
                vEntity.TeamName = ds.Tables[0].Rows[0][GasDataDbConstNames.TEAM_NAME].ToString();
                vEntity.Submitter = ds.Tables[0].Rows[0][GasDataDbConstNames.SUBMITTER].ToString();
                vEntity.Datetime = Convert.ToDateTime(ds.Tables[0].Rows[0][VentilationDbConstNames.DATETIME].ToString());
                vEntity.IsNoWindArea = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.IS_NO_WIND_AREA].ToString());
                vEntity.IsLightWindArea = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.IS_LIGHT_WIND_AREA].ToString());
                vEntity.IsReturnWindArea = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.IS_RETURN_WIND_AREA].ToString());
                vEntity.IsFollowRule = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.IS_FOLLOW_RULE].ToString());
                vEntity.IsSmall = Convert.ToInt32(ds.Tables[0].Rows[0][VentilationDbConstNames.IS_SMALL].ToString());
                vEntity.FaultageArea = ds.Tables[0].Rows[0][VentilationDbConstNames.FAULTAGE_AREA].ToString() == "" ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[0][VentilationDbConstNames.FAULTAGE_AREA].ToString());
                vEntity.AirFlow = ds.Tables[0].Rows[0][VentilationDbConstNames.AIR_FLOW].ToString() == "" ? 0.0 : Convert.ToDouble(ds.Tables[0].Rows[0][VentilationDbConstNames.AIR_FLOW].ToString());
            }
            return vEntity;
        }

        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectVentilationInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + VentilationDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + VentilationDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectVentilationInfoWithCondition(int iStartIndex, int iEndIndex, int iTunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + VentilationDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + VentilationDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + VentilationDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sb.Append(" AND " + VentilationDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");
            sb.Append(" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }


        /// <summary>
        /// 插入通风信息
        /// </summary>
        /// <param name="m">工作面瓦斯涌出动态特征井下数据实体</param>
        /// <param name="viEntity">通风实体</param>
        /// <returns>通风信息</returns>
        public static bool insertVentilationInfo(VentilationInfo viEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + VentilationDbConstNames.TABLE_NAME + "(" + PreWarningDataCommonBLL.sqlFront() + "," + VentilationDbConstNames.IS_NO_WIND_AREA + "," + VentilationDbConstNames.IS_LIGHT_WIND_AREA + "," + VentilationDbConstNames.IS_RETURN_WIND_AREA + "," + VentilationDbConstNames.IS_SMALL + "," + VentilationDbConstNames.IS_FOLLOW_RULE + "," + VentilationDbConstNames.FAULTAGE_AREA + "," + VentilationDbConstNames.AIR_FLOW + ") VALUES(");
            sb.Append(PreWarningDataCommonBLL.sqlBack(viEntity) + ",");
            sb.Append(viEntity.IsNoWindArea + ",");
            sb.Append(viEntity.IsLightWindArea + ",");
            sb.Append(viEntity.IsReturnWindArea + ",");
            sb.Append(viEntity.IsSmall + ",");
            sb.Append(viEntity.IsFollowRule + ",");
            sb.Append(viEntity.FaultageArea + ",");
            sb.Append(viEntity.AirFlow + ")");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改通风信息
        /// </summary>
        /// <param name="m">工作面瓦斯涌出动态特征井下数据实体</param>
        /// <param name="viEntity">通风实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateVentilationInfo(VentilationInfo viEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + VentilationDbConstNames.TABLE_NAME + " SET " + VentilationDbConstNames.TUNNEL_ID + "=");
            sb.Append(viEntity.Tunnel.TunnelID + "," + VentilationDbConstNames.COORDINATE_X + "=");
            sb.Append(viEntity.CoordinateX + "," + VentilationDbConstNames.COORDINATE_Y + "=");
            sb.Append(viEntity.CoordinateY + "," + VentilationDbConstNames.COORDINATE_Z + "=");
            sb.Append(viEntity.CoordinateZ + "," + VentilationDbConstNames.DATETIME + "='");
            sb.Append(viEntity.Datetime + "'," + VentilationDbConstNames.IS_NO_WIND_AREA + "=");
            sb.Append(viEntity.IsNoWindArea + "," + VentilationDbConstNames.IS_LIGHT_WIND_AREA + "=");
            sb.Append(viEntity.IsLightWindArea + "," + VentilationDbConstNames.IS_RETURN_WIND_AREA + "=");
            sb.Append(viEntity.IsReturnWindArea + "," + VentilationDbConstNames.IS_SMALL + "=");
            sb.Append(viEntity.IsSmall + "," + VentilationDbConstNames.IS_FOLLOW_RULE + "=");
            sb.Append(viEntity.IsFollowRule + "," + VentilationDbConstNames.FAULTAGE_AREA + "=");
            sb.Append(viEntity.FaultageArea + "," + VentilationDbConstNames.AIR_FLOW + "=");
            sb.Append(viEntity.AirFlow + " WHERE " + VentilationDbConstNames.ID + "=");
            sb.Append(viEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除通风信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteVentilationInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + VentilationDbConstNames.TABLE_NAME + " WHERE " + VentilationDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
