// ******************************************************************
// 概  述：导线业务逻辑
// 作  者：宋英杰
// 创建日期：2013/11/29
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibEntity;
using LibDatabase;
using System.Windows.Forms;

namespace LibBusiness
{
    public class WireInfoBLL
    {
        public bool isWirePointExist(string wirePointID,int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM "+WirePointDbConstNames.TABLE_NAME+" WHERE "+WirePointDbConstNames.WIRE_INFO_ID+"="+wireInfoID;
            DataSet ds = db.ReturnDS(sql);
            bool bResult = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][WirePointDbConstNames.WIRE_POINT_NAME].ToString() == wirePointID)
                    {
                        bResult =  true;
                    }
                }                
            }
            return bResult;
        }

        /// <summary>
        /// 导线信息登录
        /// </summary>
        /// <param name="wireInfoEntity">导线实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertWireInfo(WireInfo wireInfoEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + WireInfoDbConstNames.TABLE_NAME + " (" + 
                WireInfoDbConstNames.WIRE_NAME + "," + 
                WireInfoDbConstNames.WIRE_LEVEL + "," + 
                WireInfoDbConstNames.MEASURE_DATE + "," + 
                WireInfoDbConstNames.VOBSERVER + "," +
                WireInfoDbConstNames.COUNTER + "," +
                WireInfoDbConstNames.COUNT_DATE + "," +
                WireInfoDbConstNames.CHECKER + "," +
                WireInfoDbConstNames.CHECK_DATE + "," + 
                WireInfoDbConstNames.TUNNEL_ID + ")");
            sqlStr.Append("VALUES ('");
            sqlStr.Append(wireInfoEntity.WireName + "','");
            sqlStr.Append(wireInfoEntity.WireLevel + "','");
            sqlStr.Append(wireInfoEntity.MeasureDate + "','");
            sqlStr.Append(wireInfoEntity.Vobserver + "','");
            sqlStr.Append(wireInfoEntity.Counter + "','");
            sqlStr.Append(wireInfoEntity.CountDate + "','");
            sqlStr.Append(wireInfoEntity.Checker + "','");
            sqlStr.Append(wireInfoEntity.CheckDate + "','");
            sqlStr.Append(wireInfoEntity.Tunnel+"')" );

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 导线信息修改
        /// </summary>
        /// <param name="wireInfoEntity">导线实体</param>
        /// <param name="tunnelID">巷道编号</param>
        /// <returns>成功与否：true,false</returns>
        public static bool updateWireInfo(WireInfo wireInfoEntity,int tunnelID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WireInfoDbConstNames.TABLE_NAME + " SET " + WireInfoDbConstNames.TUNNEL_ID + " ='");
            sqlStr.Append(wireInfoEntity.Tunnel + "'," + WireInfoDbConstNames.WIRE_NAME + " = '");
            sqlStr.Append(wireInfoEntity.WireName + "',"  + WireInfoDbConstNames.WIRE_LEVEL + " = '");
            sqlStr.Append(wireInfoEntity.WireLevel + "'," + WireInfoDbConstNames.MEASURE_DATE + " = '");
            sqlStr.Append(wireInfoEntity.MeasureDate + "'," + WireInfoDbConstNames.VOBSERVER + " ='");
            sqlStr.Append(wireInfoEntity.Vobserver + "'," + WireInfoDbConstNames.COUNTER + " ='");
            sqlStr.Append(wireInfoEntity.Counter + "'," + WireInfoDbConstNames.COUNT_DATE + " ='");
            sqlStr.Append(wireInfoEntity.CountDate + "'," + WireInfoDbConstNames.CHECKER + " ='");
            sqlStr.Append(wireInfoEntity.Checker + "'," + WireInfoDbConstNames.CHECK_DATE + " ='");
            //sqlStr.Append(wireInfoEntity.CheckDate+ "' WHERE " + WireInfoDbConstNames.TUNNEL_ID + "=");
            //sqlStr.Append(tunnelID);
            //Fixed by Yanger_xy 2014.05.29
            sqlStr.Append(wireInfoEntity.CheckDate + "' WHERE " + WireInfoDbConstNames.ID + "=");
            sqlStr.Append(wireInfoEntity.WireInfoId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// combobox添加新项
        /// </summary>
        /// <param name="selectedRow">列名</param>
        /// <param name="tableName">表名</param>
        /// <param name="paraCondition">查询条件集合</param>
        /// <param name="paraResult">条件对应结果集合</param>
        /// <returns></returns>
        public static DataSet cboItemAdd(string selectedRow, string tableName, List<string> paraCondition, List<string> paraResult)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT DISTINCT ");
            //sqlStr.Append("SELECT  ");//BY YANGER_XY 不知道对其他功能是否存在影响，此处需要进行测试检查！MARK
            sqlStr.Append(selectedRow + " FROM ");
            sqlStr.Append(tableName + " WHERE 0=0");
            if (paraCondition != null)
            {
                for (int i = 0; i < paraCondition.Count; i++)
                {
                    if (paraCondition[i] != null && paraCondition[i] != "")
                    {
                        sqlStr.Append(" and " + paraCondition[i] + "= '" + paraResult[i] + "'");
                    }
                }
            }
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
        /// <summary>
        /// 返回所有导线信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectAllWireInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }
        /// <summary>
        /// 用导线ID查询巷道ID
        /// </summary>
        /// <param name="wireInfoID"></param>
        /// <returns></returns>
        public static int selectTunnelIDByWireInfoID(int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.ID + " = " + wireInfoID;
            db.Open();
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            int tunnelID = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                tunnelID = Convert.ToInt32(ds.Tables[0].Rows[0][WireInfoDbConstNames.TUNNEL_ID]);
            }
            return tunnelID;
        }
        /// <summary>
        /// 返回某导线信息
        /// </summary>
        /// <param name="wireInfoID">导线ID</param>
        /// <returns>导线信息</returns>
        public static WireInfo selectAllWireInfo(int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.ID + " = " + wireInfoID;
            DataSet ds = db.ReturnDS(sql);
            
            WireInfo wireInfoEntity = new WireInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    wireInfoEntity.WireInfoId = wireInfoID;
                    wireInfoEntity.WireName = ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME].ToString();
                    wireInfoEntity.WireLevel = ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_LEVEL].ToString();
                    wireInfoEntity.Vobserver = ds.Tables[0].Rows[0][WireInfoDbConstNames.VOBSERVER].ToString();
                    wireInfoEntity.MeasureDate = ds.Tables[0].Rows[0][WireInfoDbConstNames.MEASURE_DATE].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[0][WireInfoDbConstNames.MEASURE_DATE].ToString()) : DateTime.Now;
                    wireInfoEntity.Counter = ds.Tables[0].Rows[0][WireInfoDbConstNames.COUNTER].ToString();
                    wireInfoEntity.CountDate = ds.Tables[0].Rows[0][WireInfoDbConstNames.COUNT_DATE].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[0][WireInfoDbConstNames.COUNT_DATE].ToString()) : DateTime.Now;
                    wireInfoEntity.Checker = ds.Tables[0].Rows[0][WireInfoDbConstNames.CHECKER].ToString();
                    wireInfoEntity.CheckDate = ds.Tables[0].Rows[0][WireInfoDbConstNames.CHECK_DATE].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[0][WireInfoDbConstNames.CHECK_DATE].ToString()) : DateTime.Now;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            return wireInfoEntity;
        }
        /// <summary>
        /// 返回绑定某巷道的导线信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static DataSet selectAllWireInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME+" WHERE "+WireInfoDbConstNames.TUNNEL_ID+" = "+tunnelEntity.TunnelId;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 分页用获取所有导线信息
        /// </summary>
        /// <param name="iStartIndex">起始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <returns>导线信息</returns>
        public static DataSet selectAllWireInfo(int iStartIndex,int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + WireInfoDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + WireInfoDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 返回同一导线下所有导线点信息
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static DataSet selectWirePointByWireInfoID(int wireInfoID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.WIRE_INFO_ID + " = " + wireInfoID;
            DataSet ds = db.ReturnDS(sql);

            return ds;
        }

        /// <summary>
        /// 返回坐标
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static string[] wirePointCoordiante(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " + WireInfoDbConstNames.WIRE_NAME + " FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelID;
            db.Open();
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            string wireName = ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME].ToString();
            string sql1 = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.TUNNEL_ID + " = '" + tunnelID + "'";
            DataSet dsWirePoint = db.ReturnDSNotOpenAndClose(sql1);
            int rowCount = dsWirePoint.Tables[0].Rows.Count;
            string[] wirePoint = new string[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                string coordinat = dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.COORDINATE_X].ToString() + "," + dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.COORDINATE_Y].ToString() + "," + dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.COORDINATE_Z].ToString();
                wirePoint[i] = coordinat;
            }
            db.Close();
            return wirePoint;
        }

        /// <summary>
        /// 返回距左帮距离
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static string[] wirePointDistanceLeft(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " + WireInfoDbConstNames.WIRE_NAME + " FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelID;
            db.Open();
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            string wireName = ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME].ToString();
            string sql1 = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.TUNNEL_ID + " = '" + tunnelID + "'";
            DataSet dsWirePoint = db.ReturnDSNotOpenAndClose(sql1);
            int rowCount = dsWirePoint.Tables[0].Rows.Count;
            string[] wirePoint = new string[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                string left = dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.DISTANCE_FROM_THE_LEFT].ToString();
                wirePoint[i] = left;
            }
            db.Close();
            return wirePoint;
        }
        /// <summary>
        /// 返回距右帮距离
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public static string[] wirePointDistanceRight(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " + WireInfoDbConstNames.WIRE_NAME + " FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelID;
            db.Open();
            DataSet ds = db.ReturnDSNotOpenAndClose(sql);
            string wireName = ds.Tables[0].Rows[0][WireInfoDbConstNames.WIRE_NAME].ToString();
            string sql1 = "SELECT * FROM " + WirePointDbConstNames.TABLE_NAME + " WHERE " + WirePointDbConstNames.TUNNEL_ID + " = '" + tunnelID + "'";
            DataSet dsWirePoint = db.ReturnDSNotOpenAndClose(sql1);
            int rowCount = dsWirePoint.Tables[0].Rows.Count;
            string[] wirePoint = new string[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                string right = dsWirePoint.Tables[0].Rows[i][WirePointDbConstNames.DISTANCE_FROM_THE_RIGHT].ToString();
                wirePoint[i] = right;
            }
            db.Close();
            return wirePoint;
        }
        
        /// <summary>
        /// 删除导线信息
        /// </summary>
        /// <param name="wirePointInfoEntity"></param>
        /// <returns></returns>
        public static bool deleteWireInfo(WireInfo wireInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = false;
            string sql = "DELETE FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.ID + " ='" + wireInfoEntity.WireInfoId + "'";
            bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
