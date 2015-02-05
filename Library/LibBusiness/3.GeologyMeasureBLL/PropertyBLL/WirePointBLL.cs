// ******************************************************************
// 概  述：导线点业务逻辑
// 作  者：宋英杰
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;
using System.Data.SqlClient;
using LibCommon;

namespace LibBusiness
{
    public class WirePointBLL
    {
        /// <summary>
        /// 导线点信息登录
        /// </summary>
        /// <param name="wirePointEntity">导线点实体</param>
        /// <returns>成功与否：true，false</returns>
        //public static bool insertWirePointInfo(WirePoint wirePointEntity)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

        //    PValue[] pValue = new PValue[1];
        //    pValue[0] = new PValue("WirePointName", wirePointEntity.WirePointId);

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("INSERT INTO " + WirePointDbConstNames.TABLE_NAME + " (" +
        //        WirePointDbConstNames.WIRE_POINT_NAME + "," +
        //        WirePointDbConstNames.COORDINATE_X + "," +
        //        WirePointDbConstNames.COORDINATE_Y + "," +
        //        WirePointDbConstNames.COORDINATE_Z + "," +
        //        WirePointDbConstNames.DISTANCE_FROM_THE_LEFT + "," +
        //        WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT + "," +
        //        WirePointDbConstNames.DISTANCE_FROM_TOP + "," +
        //        WirePointDbConstNames.DISTANCE_FROM_BOTTOM + "," +
        //        WirePointDbConstNames.WIRE_INFO_ID + "," +
        //        WirePointDbConstNames.BINDINGID + ")");
        //    sb.Append("VALUES (");
        //    sb.Append("@WirePointName,'");
        //    sb.Append(wirePointEntity.CoordinateX + "','");
        //    sb.Append(wirePointEntity.CoordinateY + "','");
        //    sb.Append(wirePointEntity.CoordinateZ + "','");
        //    sb.Append(wirePointEntity.LeftDis + "','");
        //    sb.Append(wirePointEntity.RightDis + "','");
        //    sb.Append(wirePointEntity.TopDis + "','");
        //    sb.Append(wirePointEntity.BottomDis + "','");
        //    sb.Append(wirePointEntity.Wire + "','");
        //    sb.Append(wirePointEntity.BindingId + "')");

        //    bool bResult = db.ExecuteDataSet(sb.ToString(), pValue);
        //    return bResult;

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wirePointEntity"></param>
        /// <param name="wireEntity"></param>
        /// <param name="wirePointID"></param>
        /// <returns></returns>
        //public static bool updateWirePointInfo(WirePoint wirePointEntity, Wire wireEntity)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

        //    PValue[] pValue = new PValue[1];
        //    pValue[0] = new PValue("WirePointName", wirePointEntity.WirePointId);

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("UPDATE " + WirePointDbConstNames.TABLE_NAME + " SET ");
        //    sb.Append(WirePointDbConstNames.WIRE_POINT_NAME + " = ");
        //    sb.Append("@WirePointName," + WirePointDbConstNames.COORDINATE_X + " =");
        //    sb.Append(wirePointEntity.CoordinateX + "," + WirePointDbConstNames.COORDINATE_Y + "=");
        //    sb.Append(wirePointEntity.CoordinateY + "," + WirePointDbConstNames.COORDINATE_Z + "=");
        //    sb.Append(wirePointEntity.CoordinateZ + "," + WirePointDbConstNames.DISTANCE_FROM_THE_LEFT + "=");
        //    sb.Append(wirePointEntity.LeftDis + "," + WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT + "=");
        //    sb.Append(wirePointEntity.RightDis + "," + WirePointDbConstNames.DISTANCE_FROM_TOP + "=");
        //    sb.Append(wirePointEntity.TopDis + "," + WirePointDbConstNames.DISTANCE_FROM_BOTTOM + "=");
        //    sb.Append(wirePointEntity.BottomDis + " WHERE " + WirePointDbConstNames.ID + "=");
        //    sb.Append(wirePointEntity.WirePointId);

        //    bool bResult = db.ExecuteDataSet(sb.ToString(), pValue);
        //    return bResult;
        //}
        /// <summary>
        /// 删除导线点信息
        /// </summary>
        /// <param name="wirePointEntity"></param>
        /// <returns></returns>
        public static bool deleteWirePointInfo(WirePoint wirePointEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " ='" + wirePointEntity.WirePointId + "'";
            bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除巷道上的所有导线点
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static bool deleteWirePointInfo(Wire wireEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " ='" + wireEntity.WireId + "'";
            bResult = db.OperateDB(sql);
            return bResult;
        }
        /// <summary>
        /// 删除导线上的所有导线点
        /// </summary>
        /// <param name="wireInfoID">导线ID</param>
        /// <returns>是否成功删除？true:false</returns>
        public static bool deleteWirePointInfo(int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " ='" + wireInfoID + "'";
            bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 查询导线点信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static WirePoint selectWirePointInfoByWirePointId(int id)
        {
            string sqlStr = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " = " + id;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            WirePoint wirePointEntity = new WirePoint();
            if (ds.Tables[0].Rows.Count > 0)
            {
                wirePointEntity.WirePointId = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.ID]);
                wirePointEntity.WirePointName = ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_POINT_NAME].ToString();
                wirePointEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_X]);
                wirePointEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Y]);
                wirePointEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Z]);
                wirePointEntity.LeftDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_LEFT]);
                wirePointEntity.RightDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT]);
                wirePointEntity.TopDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_TOP]);
                wirePointEntity.BottomDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_BOTTOM]);
                wirePointEntity.Wire.WireId = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_INFO_ID]);
            }
            return wirePointEntity;
        }
        /// <summary>
        /// 返回所有导线点信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static DataSet selectAllWirePointInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 返回绑定某导线的导线点信息
        /// </summary>
        /// <param name="wireInfoID"></param>
        /// <returns></returns>
        public static DataSet selectAllWirePointInfo(int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " = " + wireInfoID;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 返回某导线点信息
        /// </summary>
        /// <param name="wirePointInfoID"></param>
        /// <returns>wirePointEntity</returns>
        public static WirePoint returnWirePointInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " = " + id;
            DataSet ds = db.ReturnDS(sql);
            WirePoint wirePointEntity = new WirePoint();
            if (ds.Tables[0].Rows.Count > 0)
            {
                wirePointEntity.WirePointId = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.ID]);
                wirePointEntity.WirePointName = ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_POINT_NAME].ToString();
                wirePointEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_X]);
                wirePointEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Y]);
                wirePointEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Z]);
                wirePointEntity.LeftDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_LEFT]);
                wirePointEntity.RightDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT]);
                wirePointEntity.TopDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_TOP]);
                wirePointEntity.BottomDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_BOTTOM]);
                wirePointEntity.BindingId = ds.Tables[0].Rows[0][WirePointDbConstNames.BINDINGID].ToString();
            }
            return wirePointEntity;
        }
        /// <summary>
        /// 返回绑定某巷道的导线点信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static DataSet selectAllWirePointInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelId;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        public static bool isWirePointNameExist(string wirePointName)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_POINT_NAME + " = " + wirePointName;
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 分页用获取所有导线点信息
        /// </summary>
        /// <param name="iStartIndex">起始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <returns>导线点信息</returns>
        public static DataSet selectAllWirePointInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + WirePointDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + WirePointDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
