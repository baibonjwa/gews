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
        /// <param name="wirePointInfoEntity">导线点实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertWirePointInfo(WirePointInfo wirePointInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

            PValue[] pValue = new PValue[1];
            pValue[0] = new PValue("WirePointName",wirePointInfoEntity.WirePointID);

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + WirePointDbConstNames.TABLE_NAME + " (" + 
                WirePointDbConstNames.WIRE_POINT_NAME + "," +
                WirePointDbConstNames.COORDINATE_X + "," +
                WirePointDbConstNames.COORDINATE_Y + "," +
                WirePointDbConstNames.COORDINATE_Z + "," +
                WirePointDbConstNames.DISTANCE_FROM_THE_LEFT + "," +
                WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT + "," +
                WirePointDbConstNames.DISTANCE_FROM_TOP + "," +
                WirePointDbConstNames.DISTANCE_FROM_BOTTOM + "," +
                WirePointDbConstNames.WIRE_INFO_ID + "," +
                WirePointDbConstNames.BINDINGID + ")");
            sb.Append("VALUES (");
            sb.Append("@WirePointName,'");
            sb.Append(wirePointInfoEntity.CoordinateX + "','");
            sb.Append(wirePointInfoEntity.CoordinateY + "','");
            sb.Append(wirePointInfoEntity.CoordinateZ + "','");
            sb.Append(wirePointInfoEntity.LeftDis + "','");
            sb.Append(wirePointInfoEntity.RightDis + "','");
            sb.Append(wirePointInfoEntity.TopDis + "','");
            sb.Append(wirePointInfoEntity.BottomDis + "','");
            sb.Append(wirePointInfoEntity.WireInfoID + "','");
            sb.Append(wirePointInfoEntity.BindingID + "')");

            bool bResult = db.ExecuteDataSet(sb.ToString(), pValue);
            return bResult;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wirePointInfoEntity"></param>
        /// <param name="wireInfoEntity"></param>
        /// <param name="wirePointID"></param>
        /// <returns></returns>
        public static bool updateWirePointInfo(WirePointInfo wirePointInfoEntity,WireInfo wireInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

            PValue[] pValue = new PValue[1];
            pValue[0] = new PValue("WirePointName", wirePointInfoEntity.WirePointID);

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + WirePointDbConstNames.TABLE_NAME + " SET ");
            sb.Append(WirePointDbConstNames.WIRE_POINT_NAME+" = ");
            sb.Append("@WirePointName," + WirePointDbConstNames.COORDINATE_X + " =");
            sb.Append(wirePointInfoEntity.CoordinateX + "," + WirePointDbConstNames.COORDINATE_Y + "=");
            sb.Append(wirePointInfoEntity.CoordinateY + "," + WirePointDbConstNames.COORDINATE_Z + "=");
            sb.Append(wirePointInfoEntity.CoordinateZ + "," + WirePointDbConstNames.DISTANCE_FROM_THE_LEFT + "=");
            sb.Append(wirePointInfoEntity.LeftDis + "," + WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT + "=");
            sb.Append(wirePointInfoEntity.RightDis + "," + WirePointDbConstNames.DISTANCE_FROM_TOP + "=");
            sb.Append(wirePointInfoEntity.TopDis + "," + WirePointDbConstNames.DISTANCE_FROM_BOTTOM + "=");
            sb.Append(wirePointInfoEntity.BottomDis + " WHERE " + WirePointDbConstNames.ID + "=");
            sb.Append(wirePointInfoEntity.ID);

            bool bResult = db.ExecuteDataSet(sb.ToString(), pValue);
            return bResult;
        }
        /// <summary>
        /// 删除导线点信息
        /// </summary>
        /// <param name="wirePointInfoEntity"></param>
        /// <returns></returns>
        public static bool deleteWirePointInfo(WirePointInfo wirePointInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " ='" + wirePointInfoEntity.ID + "'";
            bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除巷道上的所有导线点
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static bool deleteWirePointInfo(WireInfo wireInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " ='" + wireInfoEntity.WireInfoID + "'";
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
        public static WirePointInfo selectWirePointInfoByWirePointId(int id)
        {
            string sqlStr = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " = " + id;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            WirePointInfo wirePointInfoEntity = new WirePointInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                wirePointInfoEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.ID]);
                wirePointInfoEntity.WirePointID = ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_POINT_NAME].ToString();
                wirePointInfoEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_X]);
                wirePointInfoEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Y]);
                wirePointInfoEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Z]);
                wirePointInfoEntity.LeftDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_LEFT]);
                wirePointInfoEntity.RightDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT]);
                wirePointInfoEntity.TopDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_TOP]);
                wirePointInfoEntity.BottomDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_BOTTOM]);
                wirePointInfoEntity.WireInfoID = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_INFO_ID]);
            }
            return wirePointInfoEntity;
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
        /// <returns>wirePointInfoEntity</returns>
        public static WirePointInfo returnWirePointInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.ID + " = " + id;
            DataSet ds = db.ReturnDS(sql);
            WirePointInfo wirePointInfoEntity = new WirePointInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                wirePointInfoEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[0][WirePointDbConstNames.ID]);
                wirePointInfoEntity.WirePointID = ds.Tables[0].Rows[0][WirePointDbConstNames.WIRE_POINT_NAME].ToString();
                wirePointInfoEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_X]);
                wirePointInfoEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Y]);
                wirePointInfoEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.COORDINATE_Z]);
                wirePointInfoEntity.LeftDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_LEFT]);
                wirePointInfoEntity.RightDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT]);
                wirePointInfoEntity.TopDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_TOP]);
                wirePointInfoEntity.BottomDis = Convert.ToDouble(ds.Tables[0].Rows[0][WirePointDbConstNames.DISTANCE_FROM_BOTTOM]);
                wirePointInfoEntity.BindingID = ds.Tables[0].Rows[0][WirePointDbConstNames.BINDINGID].ToString();
            }
            return wirePointInfoEntity;
        }
        /// <summary>
        /// 返回绑定某巷道的导线点信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static DataSet selectAllWirePointInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelID;
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
