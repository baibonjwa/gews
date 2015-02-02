using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LibEntity;
using LibDatabase;

namespace LibBusiness
{
    public class WireInfoBLL
    {

        /// <summary>
        /// 导线信息修改
        /// </summary>
        /// <param name="wireEntity">导线实体</param>
        /// <param name="tunnelID">巷道编号</param>
        /// <returns>成功与否：true,false</returns>
        public static bool updateWireInfo(Wire wireEntity, int tunnelID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WireInfoDbConstNames.TABLE_NAME + " SET " + WireInfoDbConstNames.TUNNEL_ID + " ='");
            sqlStr.Append(wireEntity.Tunnel + "'," + WireInfoDbConstNames.WIRE_NAME + " = '");
            sqlStr.Append(wireEntity.WireName + "'," + WireInfoDbConstNames.WIRE_LEVEL + " = '");
            sqlStr.Append(wireEntity.WireLevel + "'," + WireInfoDbConstNames.MEASURE_DATE + " = '");
            sqlStr.Append(wireEntity.MeasureDate + "'," + WireInfoDbConstNames.VOBSERVER + " ='");
            sqlStr.Append(wireEntity.Vobserver + "'," + WireInfoDbConstNames.COUNTER + " ='");
            sqlStr.Append(wireEntity.Counter + "'," + WireInfoDbConstNames.COUNT_DATE + " ='");
            sqlStr.Append(wireEntity.CountDate + "'," + WireInfoDbConstNames.CHECKER + " ='");
            sqlStr.Append(wireEntity.Checker + "'," + WireInfoDbConstNames.CHECK_DATE + " ='");
            //sqlStr.Append(wireEntity.CheckDate+ "' WHERE " + WireInfoDbConstNames.TUNNEL_ID + "=");
            //sqlStr.Append(tunnelID);
            //Fixed by Yanger_xy 2014.05.29
            sqlStr.Append(wireEntity.CheckDate + "' WHERE " + WireInfoDbConstNames.ID + "=");
            sqlStr.Append(wireEntity.WireInfoId);

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
        /// 返回绑定某巷道的导线信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        public static DataSet selectAllWireInfo(Tunnel tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelId;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 分页用获取所有导线信息
        /// </summary>
        /// <param name="iStartIndex">起始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <returns>导线信息</returns>
        public static DataSet selectAllWireInfo(int iStartIndex, int iEndIndex)
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
    }
}
