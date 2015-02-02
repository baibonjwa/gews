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
        /// 返回绑定某巷道的导线信息
        /// </summary>
        /// <param name="tunnelEntity"></param>
        /// <returns></returns>
        //public static DataSet selectAllWireInfo(Tunnel tunnelEntity)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    string sql = "SELECT * FROM " + WireInfoDbConstNames.TABLE_NAME + " WHERE " + WireInfoDbConstNames.TUNNEL_ID + " = " + tunnelEntity.TunnelId;
        //    DataSet ds = db.ReturnDS(sql);
        //    return ds;
        //}

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
