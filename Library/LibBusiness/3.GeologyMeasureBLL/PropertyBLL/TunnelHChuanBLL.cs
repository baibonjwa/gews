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
        public static int insertTunnelHChuan(TunnelHChuan tunnelHChuanEntity)
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
            sb.Append(tunnelHChuanEntity.TunnelId1 + ",");
            sb.Append(tunnelHChuanEntity.TunnelId2 + ",");
            sb.Append(tunnelHChuanEntity.X1 + ",");
            sb.Append(tunnelHChuanEntity.Y1 + ",");
            sb.Append(tunnelHChuanEntity.Z1 + ",");
            sb.Append(tunnelHChuanEntity.X2 + ",");
            sb.Append(tunnelHChuanEntity.Y2 + ",");
            sb.Append(tunnelHChuanEntity.Z2 + ",");
            sb.Append(tunnelHChuanEntity.Azimuth + ",");
            sb.Append(tunnelHChuanEntity.Team + ",'");
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
        public static bool deleteTunnelHChuan(TunnelHChuan tunnelHChuanEntity)
        {

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + TunnelHChuanDbConstNames.TABLE_NAME + " WHERE " + TunnelHChuanDbConstNames.ID + " = " + tunnelHChuanEntity.Id;
            bool bResult = db.OperateDB(sql);
            if (bResult)
            {
                clearTunnelTypeOfHChuan(tunnelHChuanEntity.Id);
            }
            return bResult;
        }

        /// <summary>
        /// 修改掘进巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool updateTunnelHChuan(TunnelHChuan tunnelHChuanEntity)
        {
            clearTunnelTypeOfHChuan(tunnelHChuanEntity.Id);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + TunnelHChuanDbConstNames.TABLE_NAME + " SET " + TunnelHChuanDbConstNames.NAME_HCHUAN + " = '");
            sb.Append(tunnelHChuanEntity.NameHChuan + "'," + TunnelHChuanDbConstNames.TUNNEL_ID1 + " = ");
            sb.Append(tunnelHChuanEntity.TunnelId1 + "," + TunnelHChuanDbConstNames.TUNNEL_ID2 + " = ");
            sb.Append(tunnelHChuanEntity.TunnelId2 + "," + TunnelHChuanDbConstNames.TUNNEL_X1 + " = ");
            sb.Append(tunnelHChuanEntity.X1 + "," + TunnelHChuanDbConstNames.TUNNEL_Y1 + " = ");
            sb.Append(tunnelHChuanEntity.Y1 + "," + TunnelHChuanDbConstNames.TUNNEL_Z1 + " = ");
            sb.Append(tunnelHChuanEntity.Z1 + "," + TunnelHChuanDbConstNames.TUNNEL_X2 + " = ");
            sb.Append(tunnelHChuanEntity.X2 + "," + TunnelHChuanDbConstNames.TUNNEL_Y2 + " = ");
            sb.Append(tunnelHChuanEntity.Y2 + "," + TunnelHChuanDbConstNames.TUNNEL_Z2 + " = ");
            sb.Append(tunnelHChuanEntity.Z2 + "," + TunnelHChuanDbConstNames.TUNNEL_AZIMUTH + " = ");
            sb.Append(tunnelHChuanEntity.Azimuth + "," + TunnelHChuanDbConstNames.TEAM_NAME_ID + " = ");
            sb.Append(tunnelHChuanEntity.Team + "," + TunnelHChuanDbConstNames.START_DATE + "='");
            sb.Append(tunnelHChuanEntity.StartDate + "'," + TunnelHChuanDbConstNames.IS_FINISH + "=");
            sb.Append(tunnelHChuanEntity.IsFinish + "," + TunnelHChuanDbConstNames.STOP_DATE + "='");
            sb.Append(tunnelHChuanEntity.StopDate + "'," + TunnelHChuanDbConstNames.WORK_STYLE + "='");
            sb.Append(tunnelHChuanEntity.WorkStyle + "'," + TunnelHChuanDbConstNames.WORK_TIME + "='");
            sb.Append(tunnelHChuanEntity.WorkTime + "'," + TunnelHChuanDbConstNames.TUNNEL_STATE + " = '");
            sb.Append(tunnelHChuanEntity.State + "'," + TunnelHChuanDbConstNames.TUNNEL_WIDTH + " = '");
            sb.Append(tunnelHChuanEntity.Width + "' WHERE " + TunnelHChuanDbConstNames.ID + "=");
            sb.Append(tunnelHChuanEntity.Id);

            bool bResult = db.OperateDB(sb.ToString());
            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID1);
            //setTunnelAsHChuan(tunnelHChuanEntity.TunnelID2);

            return bResult;
        }

        private static void clearTunnelTypeOfHChuan(int tunnelHChuanID)
        {
            TunnelHChuan tunnelHChuanEntity = selectTunnelHChuan(tunnelHChuanID);
            if (tunnelHChuanEntity != null)
            {
                var tunnel1 = Tunnel.Find(tunnelHChuanEntity.TunnelId1);
                var tunnel2 = Tunnel.Find(tunnelHChuanEntity.TunnelId2);
                tunnel1.TunnelType = TunnelTypeEnum.OTHER;
                tunnel2.TunnelType = TunnelTypeEnum.OTHER;
                tunnel1.Save();
                tunnel2.Save();
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
        private static TunnelHChuan selectTunnelHChuan(int tunnelHChuanID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHChuanDbConstNames.TABLE_NAME + " WHERE " + TunnelHChuanDbConstNames.ID + " = " + tunnelHChuanID;
            DataSet ds = db.ReturnDS(sql);
            TunnelHChuan tunnelHChuanEntity = new TunnelHChuan();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    tunnelHChuanEntity.Id = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.ID];
                    tunnelHChuanEntity.TunnelId1 = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_ID1];
                    tunnelHChuanEntity.TunnelId2 = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_ID2];
                    tunnelHChuanEntity.X1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_X1];
                    tunnelHChuanEntity.Y1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Y1];
                    tunnelHChuanEntity.Z1 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Z1];
                    tunnelHChuanEntity.X2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_X2];
                    tunnelHChuanEntity.Y2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Y2];
                    tunnelHChuanEntity.Z2 = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_Z2];
                    tunnelHChuanEntity.Azimuth = (double)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TUNNEL_AZIMUTH];
                    tunnelHChuanEntity.Team.TeamId = (int)ds.Tables[0].Rows[0][TunnelHChuanDbConstNames.TEAM_NAME_ID];
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
