using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibEntity;
using LibCommon;
using LibBusiness;
using LibDatabase;

namespace LibBusiness
{
    public class TunnelDefaultSelect
    {
        public static LibEntity.TunnelDefaultSelect 
            selectDefaultTunnel(string tableName)
        {
            string sql = "SELECT * FROM "+TunnelDefaultSelectDbConstNames.TABLE_NAME+
                " WHERE "+TunnelDefaultSelectDbConstNames.TABLE_NAME_USE+" = '"+
                tableName+"'";
            ManageDataBase db = new 
                ManageDataBase(LibDatabase.DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sql);
            LibEntity.TunnelDefaultSelect tunnelDefaultSelectEntity = new 
                LibEntity.TunnelDefaultSelect();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tunnelDefaultSelectEntity.TableName = 
                    ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.TABLE_NAME_USE].ToString();
                tunnelDefaultSelectEntity.MineID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.MINE_ID].ToString());
                tunnelDefaultSelectEntity.HorizontalID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.HORIZONTAL_ID].ToString());
                tunnelDefaultSelectEntity.MiningAreaID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.MINING_AREA_ID].ToString());
                tunnelDefaultSelectEntity.WorkingFaceID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.WORKING_FACE_ID].ToString());
                return tunnelDefaultSelectEntity;
            }
            return null;
        }

        /// <summary>
        /// 插入默认选择巷道
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tunnelID">巷道ID</param>
        public static bool InsertDefaultTunnel(string tableName, int 
            tunnelID)
        {
            //TunnelEntity tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelID);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("INSERT INTO " + TunnelDefaultSelectDbConstNames.TABLE_NAME+"(");
            //sb.Append(TunnelDefaultSelectDbConstNames.TABLE_NAME_USE + "," + TunnelDefaultSelectDbConstNames.MINE_ID + "," + TunnelDefaultSelectDbConstNames.HORIZONTAL_ID + "," + TunnelDefaultSelectDbConstNames.MINING_AREA_ID + "," + TunnelDefaultSelectDbConstNames.WORKING_FACE_ID);
            //sb.Append(") VALUES ('");
            //sb.Append(tableName + "',");
            //sb.Append(tunnelEntity.WorkingFace.BasicInfo.MineID + ",");
            //sb.Append(tunnelEntity.WorkingFace.BasicInfo.HorizontalID + ",");
            //sb.Append(tunnelEntity.WorkingFace.BasicInfo.MiningAreaID + ",");
            //sb.Append(tunnelEntity.WorkingFace.WorkingFaceID + ")");
            //ManageDataBase db = new ManageDataBase(LibDatabase.DATABASE_TYPE.GeologyMeasureDB);
            //bool bResult = db.OperateDB(sb.ToString());
            //return bResult;
            return false;
        }

        /// <summary>
        /// 修改默认选择巷道
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns></returns>
        public static bool UpdateDefaultTunnel(string tableName, int 
            tunnelID)
        {
            //DataSet ds = new DataSet();
            //TunnelEntity tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelID);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("UPDATE " + TunnelDefaultSelectDbConstNames.TABLE_NAME + " SET ");
            //sb.Append(TunnelDefaultSelectDbConstNames.MINE_ID          + " = " + tunnelEntity.WorkingFace.BasicInfo.MineID + ",");
            //sb.Append(TunnelDefaultSelectDbConstNames.HORIZONTAL_ID    + " = " + tunnelEntity.WorkingFace.BasicInfo.HorizontalID + ",");
            //sb.Append(TunnelDefaultSelectDbConstNames.MINING_AREA_ID   + " = " + tunnelEntity.WorkingFace.BasicInfo.MiningAreaID + ",");
            //sb.Append(TunnelDefaultSelectDbConstNames.WORKING_FACE_ID  + " = " + tunnelEntity.WorkingFace.WorkingFaceID);
            //sb.Append(" WHERE " + TunnelDefaultSelectDbConstNames.TABLE_NAME_USE + " = '" + tableName+"'");
            //ManageDataBase db = new ManageDataBase(LibDatabase.DATABASE_TYPE.GeologyMeasureDB);
            //bool bResult = db.OperateDB(sb.ToString());
            //return bResult;
            return false;
        }
    }
}
