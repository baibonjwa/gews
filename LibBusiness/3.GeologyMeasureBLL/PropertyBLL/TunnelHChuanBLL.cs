using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class TunnelHChuanBLL
    {
        /// <summary>
        /// 插入横川数据
        /// </summary>
        /// <returns></returns>
        public static int insertTunnelHChuan(TunnelHChuanEntity tunnelHChuanEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + TunnelHChuanDbConstNames.TABLE_NAME + " (" +
                TunnelHChuanDbConstNames.NAME_HCHUAN + "," + TunnelHChuanDbConstNames.TUNNEL_ID1 + "," +
                TunnelHChuanDbConstNames.TUNNEL_ID2 + "," + TunnelHChuanDbConstNames.TUNNEL_X1 + "," +
                TunnelHChuanDbConstNames.TUNNEL_Y1 + "," + TunnelHChuanDbConstNames.TUNNEL_Z1 + "," +
                TunnelHChuanDbConstNames.TUNNEL_X2 + "," + TunnelHChuanDbConstNames.TUNNEL_Y2 + "," +
                TunnelHChuanDbConstNames.TUNNEL_Z2 + "," +
                TunnelHChuanDbConstNames.TUNNEL_AZIMUTH + "," +
                WorkingFaceDbConstNames.TEAM_NAME_ID + "," + WorkingFaceDbConstNames.START_DATE + "," + WorkingFaceDbConstNames.IS_FINISH + ","
                + WorkingFaceDbConstNames.STOP_DATE + "," + WorkingFaceDbConstNames.WORK_STYLE + "," +
                WorkingFaceDbConstNames.WORK_TIME + "," + TunnelHChuanDbConstNames.TUNNEL_STATE + "," + TunnelHChuanDbConstNames.TUNNEL_WIDTH + ") VALUES(");
            sb.Append("'");
            sb.Append(tunnelHChuanEntity.NameHChuan + "',");
            sb.Append(tunnelHChuanEntity.TunnelID1 + ",");
            sb.Append(tunnelHChuanEntity.TunnelID2 + ",");
            sb.Append(tunnelHChuanEntity.X_1 + ",");
            sb.Append(tunnelHChuanEntity.Y_1 + ",");
            sb.Append(tunnelHChuanEntity.Z_1 + ",");
            sb.Append(tunnelHChuanEntity.X_2 + ",");
            sb.Append(tunnelHChuanEntity.Y_2 + ",");
            sb.Append(tunnelHChuanEntity.Z_2 + ",");
            sb.Append(tunnelHChuanEntity.Azimuth + ",");
            sb.Append(tunnelHChuanEntity.TeamNameID + ",'");
            sb.Append(tunnelHChuanEntity.StartDate + "',");
            sb.Append(tunnelHChuanEntity.IsFinish + ",'");
            sb.Append(tunnelHChuanEntity.StopDate + "','");
            sb.Append(tunnelHChuanEntity.WorkStyle + "','");
            sb.Append(tunnelHChuanEntity.WorkTime + "','");
            sb.Append(tunnelHChuanEntity.State + "','");
            sb.Append(tunnelHChuanEntity.Width + "');");
            sb.Append("SELECT @@IDENTITY;");

            var ds = db.ReturnDS(sb.ToString());
            int id = -1;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var objId = ds.Tables[0].Rows[0][0];

                int.TryParse(objId.ToString(), out id);
            }

            //bool bResult = db.OperateDB(sb.ToString());

            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID1);
            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID2);

            return id;
        }

        private static bool setTunnelAsHChuan(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " + TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + TunnelHChuanDbConstNames.TUNNEL_TYPE + "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除回采巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool deleteTunnelHChuan(TunnelHChuanEntity tunnelHChuanEntity)
        {

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + TunnelHChuanDbConstNames.TABLE_NAME + " WHERE " + TunnelHChuanDbConstNames.ID + " = " + tunnelHChuanEntity.ID;
            bool bResult = db.OperateDB(sql);
            if (bResult)
            {
                clearTunnelTypeOfHChuan(tunnelHChuanEntity.ID);
            }
            return bResult;
        }

        /// <summary>
        /// 修改掘进巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool updateTunnelHChuan(TunnelHChuanEntity tunnelHChuanEntity)
        {
            clearTunnelTypeOfHChuan(tunnelHChuanEntity.ID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + TunnelHChuanDbConstNames.TABLE_NAME + " SET " + TunnelHChuanDbConstNames.NAME_HCHUAN + " = '");
            sb.Append(tunnelHChuanEntity.NameHChuan + "'," + TunnelHChuanDbConstNames.TUNNEL_ID1 + " = ");
            sb.Append(tunnelHChuanEntity.TunnelID1 + "," + TunnelHChuanDbConstNames.TUNNEL_ID2 + " = ");
            sb.Append(tunnelHChuanEntity.TunnelID2 + "," + TunnelHChuanDbConstNames.TUNNEL_X1 + " = ");
            sb.Append(tunnelHChuanEntity.X_1 + "," + TunnelHChuanDbConstNames.TUNNEL_Y1 + " = ");
            sb.Append(tunnelHChuanEntity.Y_1 + "," + TunnelHChuanDbConstNames.TUNNEL_Z1 + " = ");
            sb.Append(tunnelHChuanEntity.Z_1 + "," + TunnelHChuanDbConstNames.TUNNEL_X2 + " = ");
            sb.Append(tunnelHChuanEntity.X_2 + "," + TunnelHChuanDbConstNames.TUNNEL_Y2 + " = ");
            sb.Append(tunnelHChuanEntity.Y_2 + "," + TunnelHChuanDbConstNames.TUNNEL_Z2 + " = ");
            sb.Append(tunnelHChuanEntity.Z_2 + "," + TunnelHChuanDbConstNames.TUNNEL_AZIMUTH + " = ");
            sb.Append(tunnelHChuanEntity.Azimuth + "," + TunnelHChuanDbConstNames.TEAM_NAME_ID + " = ");
            sb.Append(tunnelHChuanEntity.TeamNameID + "," + TunnelHChuanDbConstNames.START_DATE + "='");
            sb.Append(tunnelHChuanEntity.StartDate + "'," + TunnelHChuanDbConstNames.IS_FINISH + "=");
            sb.Append(tunnelHChuanEntity.IsFinish + "," + TunnelHChuanDbConstNames.STOP_DATE + "='");
            sb.Append(tunnelHChuanEntity.StopDate + "'," + TunnelHChuanDbConstNames.WORK_STYLE + "='");
            sb.Append(tunnelHChuanEntity.WorkStyle + "'," + TunnelHChuanDbConstNames.WORK_TIME + "='");
            sb.Append(tunnelHChuanEntity.WorkTime + "'," + TunnelHChuanDbConstNames.TUNNEL_STATE + " = '");
            sb.Append(tunnelHChuanEntity.State + "'," + TunnelHChuanDbConstNames.TUNNEL_WIDTH + " = '");
            sb.Append(tunnelHChuanEntity.Width + "' WHERE " + TunnelHChuanDbConstNames.ID + "=");
            sb.Append(tunnelHChuanEntity.ID);

            bool bResult = db.OperateDB(sb.ToString());
            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID1);
            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID2);

            return bResult;
        }

        private static void clearTunnelTypeOfHChuan(int tunnelHChuanID)
        {
            TunnelHChuanEntity tunnelHChuanEntity = selectTunnelHChuan(tunnelHChuanID);
            if (tunnelHChuanEntity != null)
            {
                TunnelInfoBLL.clearTunnelType(tunnelHChuanEntity.TunnelID1);
                TunnelInfoBLL.clearTunnelType(tunnelHChuanEntity.TunnelID2);
            }
        }
        /// <summary>
        /// 查询回采巷道数据
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHChuan()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHChuanDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }
        private static TunnelHChuanEntity selectTunnelHChuan(int tunnelHChuanID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHChuanDbConstNames.TABLE_NAME + " WHERE " + TunnelHChuanDbConstNames.ID + " = " + tunnelHChuanID;
            DataSet ds = db.ReturnDS(sql);
            TunnelHChuanEntity tunnelHChuanEntity = new TunnelHChuanEntity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    tunnelHChuanEntity.ID = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.ID];
                    tunnelHChuanEntity.TunnelID1 = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_ID1];
                    tunnelHChuanEntity.TunnelID2 = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_ID2];
                    tunnelHChuanEntity.X_1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_X1];
                    tunnelHChuanEntity.Y_1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Y1];
                    tunnelHChuanEntity.Z_1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Z1];
                    tunnelHChuanEntity.X_2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_X2];
                    tunnelHChuanEntity.Y_2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Y2];
                    tunnelHChuanEntity.Z_2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Z2];
                    tunnelHChuanEntity.Azimuth = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH];
                    tunnelHChuanEntity.TeamNameID = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TEAM_NAME_ID];
                    tunnelHChuanEntity.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.START_DATE]);
                    tunnelHChuanEntity.IsFinish = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.IS_FINISH];
                    tunnelHChuanEntity.StopDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.STOP_DATE]);
                    tunnelHChuanEntity.WorkStyle = ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.WORK_STYLE].ToString();
                    tunnelHChuanEntity.WorkTime = ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.WORK_TIME].ToString();
                    tunnelHChuanEntity.State = ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_STATE].ToString();
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
            return tunnelHChuanEntity;
        }

        /// <summary>
        /// 分页用返回回采巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHChuan(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelHChuanDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TunnelHChuanDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
