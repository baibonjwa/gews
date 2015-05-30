// ******************************************************************
// 概  述：回采巷道业务逻辑
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
using LibCommon;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class TunnelHCBLLNew
    {
        /// <summary>
        /// 返回巷道是否作为回采巷道
        /// </summary>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>巷道是否作为回采巷道？true:false</returns>
        public static bool isTunnelUsedAsHC(TunnelEntity tunnelEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + TunnelHCDbConstNames.TABLE_NAME + 
                " WHERE " + TunnelHCDbConstNames.TUNNEL_ID1 + " = " + tunnelEntity.TunnelID + 
                " OR " + TunnelHCDbConstNames.TUNNEL_ID2 + " = " + tunnelEntity.TunnelID + 
                " OR " + TunnelHCDbConstNames.TUNNEL_ID3 + " = " + tunnelEntity.TunnelID;
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
        /// 插入掘进巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool insertTunnelHC(TunnelHCEntityNew tunnelHCEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + TunnelHCDbConstNames.TABLE_NAME + " (" + TunnelHCDbConstNames.TUNNEL_ID1 + "," + TunnelHCDbConstNames.TUNNEL_ID2 + "," + TunnelHCDbConstNames.TUNNEL_ID3 + "," + TunnelHCDbConstNames.TUNNEL_ID + "," + TunnelHCDbConstNames.TEAM_NAME_ID + "," + TunnelHCDbConstNames.START_DATE + "," + TunnelHCDbConstNames.IS_FINISH + "," + TunnelHCDbConstNames.STOP_DATE + "," + TunnelHCDbConstNames.WORK_STYLE + "," + TunnelHCDbConstNames.WORK_TIME + ") VALUES(");
            sb.Append(tunnelHCEntity.TunnelID_ZY + ",");
            sb.Append(tunnelHCEntity.TunnelID_FY + ",");
            sb.Append(tunnelHCEntity.TunnelID_KQY + ",'");
            sb.Append(tunnelHCEntity.TunnelID + "','");
            sb.Append(tunnelHCEntity.TeamNameID + "','");
            sb.Append(tunnelHCEntity.StartDate + "',");
            sb.Append(tunnelHCEntity.IsFinish + ",'");
            sb.Append(tunnelHCEntity.StopDate + "','");
            sb.Append(tunnelHCEntity.WorkStyle + "','");
            sb.Append(tunnelHCEntity.WorkTime + "')");

            bool bResult = db.OperateDB(sb.ToString());
            setTunnelAsHC(tunnelHCEntity.TunnelID_ZY);
            setTunnelAsHC(tunnelHCEntity.TunnelID_FY);
            setTunnelAsHC(tunnelHCEntity.TunnelID_KQY);
            //其它巷道
            string[] sArray = new string[10];
            if (tunnelHCEntity.TunnelID != null)
            {
                sArray = tunnelHCEntity.TunnelID.Split(',');
            }
            foreach (string i in sArray)
            {
                int iTunnelID = Convert.ToInt16(i);
                setTunnelAsHC(iTunnelID);
            }
            return bResult;
        }

        /// <summary>
        /// 修改掘进巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool updateTunnelHC(TunnelHCEntityNew tunnelHCEntity)
        {
            clearTunnelTypeOfHC(tunnelHCEntity.ID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + TunnelHCDbConstNames.TABLE_NAME + " SET " + TunnelHCDbConstNames.TUNNEL_ID1 + " = ");
            sb.Append(tunnelHCEntity.TunnelID_ZY + "," + TunnelHCDbConstNames.TUNNEL_ID2 + " = ");
            sb.Append(tunnelHCEntity.TunnelID_FY + "," + TunnelHCDbConstNames.TUNNEL_ID3 + " = ");
            sb.Append(tunnelHCEntity.TunnelID_KQY + "," + TunnelHCDbConstNames.TUNNEL_ID + " = '");
            sb.Append(tunnelHCEntity.TunnelID + "'," + TunnelHCDbConstNames.TEAM_NAME_ID + " = '");
            sb.Append(tunnelHCEntity.TeamNameID + "'," + TunnelHCDbConstNames.START_DATE + "='");
            sb.Append(tunnelHCEntity.StartDate + "'," + TunnelHCDbConstNames.IS_FINISH + "=");
            sb.Append(tunnelHCEntity.IsFinish + "," + TunnelHCDbConstNames.STOP_DATE + "='");
            sb.Append(tunnelHCEntity.StopDate + "'," + TunnelHCDbConstNames.WORK_STYLE + "='");
            sb.Append(tunnelHCEntity.WorkStyle + "'," + TunnelHCDbConstNames.WORK_TIME + "='");
            sb.Append(tunnelHCEntity.WorkTime + "' WHERE " + TunnelHCDbConstNames.ID + "=");
            sb.Append(tunnelHCEntity.ID);
            bool bResult = db.OperateDB(sb.ToString());
            setTunnelAsHC(tunnelHCEntity.TunnelID_ZY);
            setTunnelAsHC(tunnelHCEntity.TunnelID_FY);
            setTunnelAsHC(tunnelHCEntity.TunnelID_KQY);
            //其他巷道
            string[] sArray = new string[10];
            if (tunnelHCEntity.TunnelID != null)
            {
                sArray = tunnelHCEntity.TunnelID.Split(',');
            }
            foreach (string i in sArray)
            {
                if ((i != "")&&(i != null))
                {
                    int iTunnelID = Convert.ToInt16(i);
                    setTunnelAsHC(iTunnelID);
                }
            }
            return bResult;
        }

        public static void clearTunnelTypeOfHC(int tunnelHCID)
        {
            TunnelHCEntity tunnelHCEntity = selectTunnelHC(tunnelHCID);
            if (tunnelHCEntity != null)
            {
                TunnelInfoBLL.clearTunnelType(tunnelHCEntity.TunnelID_ZY);
                TunnelInfoBLL.clearTunnelType(tunnelHCEntity.TunnelID_FY);
                TunnelInfoBLL.clearTunnelType(tunnelHCEntity.TunnelID_KQY);
            }
        }

        /// <summary>
        /// 设置巷道类型为回采巷道
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns></returns>
        public static bool setTunnelAsHC(int tunnelID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME + " SET " + TunnelInfoDbConstNames.TUNNEL_TYPE + " = '" + TunnelHCDbConstNames.TUNNEL_TYPE + "' WHERE " + TunnelInfoDbConstNames.ID + " = " + tunnelID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 删除回采巷道数据
        /// </summary>
        /// <returns></returns>
        public static bool deleteTunnelHC(TunnelHCEntity tunnelHCEntity)
        {
            
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + TunnelHCDbConstNames.TABLE_NAME + " WHERE " + TunnelHCDbConstNames.ID + " = " + tunnelHCEntity.ID;
            bool bResult = db.OperateDB(sql);
            if (bResult)
            {
                clearTunnelTypeOfHC(tunnelHCEntity.ID);
            }
            return bResult;
        }
        /// <summary>
        /// 查询回采巷道数据
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHCDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查询回采巷道数据
        /// </summary>
        /// <returns></returns>
        public static TunnelHCEntity selectTunnelHCByID3(int tunnelId)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHCDbConstNames.TABLE_NAME+" WHERE "+TunnelHCDbConstNames.TUNNEL_ID3+"="+tunnelId;
            DataSet ds = db.ReturnDS(sql);
            TunnelHCEntity tunnelHCEntity=new TunnelHCEntity();
            if(ds.Tables[0].Rows.Count>0)
            {
                try
                {
                    tunnelHCEntity.ID = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.ID];
                    tunnelHCEntity.TunnelID_ZY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID1];
                    tunnelHCEntity.TunnelID_FY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID2];
                    tunnelHCEntity.TunnelID_KQY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID3];
                    tunnelHCEntity.TeamNameID = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TEAM_NAME_ID];
                    tunnelHCEntity.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHCDbConstNames.START_DATE]);
                    tunnelHCEntity.IsFinish = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.IS_FINISH];
                    tunnelHCEntity.StopDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHCDbConstNames.STOP_DATE]);
                    tunnelHCEntity.WorkStyle = ds.Tables[0].Rows[0][TunnelHCDbConstNames.WORK_STYLE].ToString();
                    tunnelHCEntity.WorkTime = ds.Tables[0].Rows[0][TunnelHCDbConstNames.WORK_TIME].ToString();
                }
                catch
                {
                    return null;
                }
            }
            return tunnelHCEntity;
        }

        /// <summary>
        /// 返回某条回采巷道信息
        /// </summary>
        /// <param name="tunnelHCID">回采巷道ID</param>
        /// <returns>回采巷道实体</returns>
        public static TunnelHCEntity selectTunnelHC(int tunnelHCID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelHCDbConstNames.TABLE_NAME + " WHERE " + TunnelHCDbConstNames.ID + " = " + tunnelHCID;
            DataSet ds = db.ReturnDS(sql);
            TunnelHCEntity tunnelHCEntity = new TunnelHCEntity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    tunnelHCEntity.ID = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.ID];
                    tunnelHCEntity.TunnelID_ZY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID1];
                    tunnelHCEntity.TunnelID_FY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID2];
                    tunnelHCEntity.TunnelID_KQY = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TUNNEL_ID3];
                    tunnelHCEntity.TeamNameID = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.TEAM_NAME_ID];
                    tunnelHCEntity.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHCDbConstNames.START_DATE]);
                    tunnelHCEntity.IsFinish = (int)ds.Tables[0].Rows[0][TunnelHCDbConstNames.IS_FINISH];
                    tunnelHCEntity.StopDate = Convert.ToDateTime(ds.Tables[0].Rows[0][TunnelHCDbConstNames.STOP_DATE]);
                    tunnelHCEntity.WorkStyle = ds.Tables[0].Rows[0][TunnelHCDbConstNames.WORK_STYLE].ToString();
                    tunnelHCEntity.WorkTime = ds.Tables[0].Rows[0][TunnelHCDbConstNames.WORK_TIME].ToString();
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
            return tunnelHCEntity;
        }

        /// <summary>
        /// 分页用返回回采巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelHCDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TunnelHCDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
