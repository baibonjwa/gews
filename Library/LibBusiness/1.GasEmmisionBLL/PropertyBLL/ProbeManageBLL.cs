// ******************************************************************
// 概  述：探头信息管理业务逻辑
// 作  者：伍鑫
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

namespace LibBusiness
{
    public class ProbeManageBLL
    {
        /// <summary>
        /// 获取全部【探头信息】
        /// </summary>
        /// <returns>全部【探头信息】</returns>
        public static DataSet selectAllProbeManageInfo()
        {
            string sqlStr = "SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取【探头信息】
        /// </summary>
        /// <returns>【探头信息】</returns>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        public static DataSet selectProbeManageInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + ProbeManageDbConstNames.PROBE_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + ProbeManageDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【探头信息】登录
        /// </summary>
        /// <param name="probeEntity">【探头实体】</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertProbeManageInfo(Probe probeEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + ProbeManageDbConstNames.PROBE_ID);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_NAME);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_TYPE_ID);
            // 2014/5/29 add by wuxin Start
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_TYPE_DISPLAY_NAME);
            // 2014/5/29 add by wuxin End
            sqlStr.Append(", " + ProbeManageDbConstNames.TUNNEL_ID);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_X);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_Y);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_Z);
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_DESCRIPTION);
            sqlStr.Append(", " + ProbeManageDbConstNames.IS_MOVE);
            sqlStr.Append(", " + ProbeManageDbConstNames.FAR_FROM_FRONTAL);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + probeEntity.ProbeId + "'");
            sqlStr.Append(", '" + probeEntity.ProbeName + "'");
            sqlStr.Append(", '" + probeEntity.ProbeType + "'");
            // 2014/5/29 add by wuxin Start
            sqlStr.Append(", '" + probeEntity.ProbeTypeDisplayName + "'");
            // 2014/5/29 add by wuxin End
            sqlStr.Append(", '" + probeEntity.Tunnel + "'");
            sqlStr.Append(", '" + probeEntity.ProbeLocationX + "'");
            sqlStr.Append(", '" + probeEntity.ProbeLocationY + "'");
            sqlStr.Append(", '" + probeEntity.ProbeLocationZ + "'");
            sqlStr.Append(", '" + probeEntity.ProbeDescription + "'");
            sqlStr.Append(", '" + probeEntity.IsMove + "'");
            sqlStr.Append(", '" + probeEntity.FarFromFrontal + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 判断【探头编号】是否存在
        /// </summary>
        /// <param name="strProbeId">【探头编号】</param>
        /// <returns>存在与否：true存在，false不存在</returns>
        public static bool isProbeIdExist(string strProbeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_ID + " = '" + strProbeId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 判断【探头名称】是否存在
        /// </summary>
        /// <param name="strProbeName">【探头名称】</param>
        /// <returns>存在与否：true存在，false不存在</returns>
        public static bool isProbeNameExist(string strProbeName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_NAME + " = '" + strProbeName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 获取指定【巷道】下，指定【探头类型】的探头信息
        /// </summary>
        /// <param name="iProbeTypeId">【探头类型】</param>
        /// <param name="iTunnelId">【巷道】</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbeByProbeTypeIdAndTunnelId(int iProbeTypeId, int iTunnelId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_TYPE_ID + " = " + iProbeTypeId);
            sqlStr.Append(" AND ");
            sqlStr.Append(ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return ds;
        }

        /// <summary>
        /// 根据巷道编号，获取全部探头信息
        /// </summary>
        /// <param name="tunnelID">巷道编号</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbeManageInfoByTunnelID(int iTunnelID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelID);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 根据巷道编号，获取全部探头信息
        /// </summary>
        /// <param name="tunnelID">巷道编号</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbesByTunnelID(int iTunnelID)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT [PROBE_ID],[PROBE_NAME], CASE [PROBE_TYPE_ID]"+
                " WHEN 0 THEN 'T0' " +
                " WHEN 1 THEN 'T1' " +
                " WHEN 2 THEN 'T2' " +
                " WHEN 3 THEN 'T3' " +
                " WHEN 4 THEN 'T4' " +
                " WHEN 5 THEN 'T5' " +
                " WHEN 6 THEN 'T6' " +
                " WHEN 7 THEN 'T7' " +
                " WHEN 8 THEN 'T8' " +
                " END AS [PROBE_TYPE]" +
                ",[PROBE_DESCRIPTION] FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelID);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 根据巷道编号和探头类型编号,获取全部探头信息
        /// </summary>
        /// <param name="tunnelID">巷道编号</param>
        /// <param name="iProbeTypeId">探头类型编号</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbeManageInfoByTunnelIDAndProbeType(int iTunnelID, int iProbeTypeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.TUNNEL_ID + " = " + iTunnelID);
            sqlStr.Append(" AND ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_TYPE_ID + " = " + iProbeTypeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 重置探头类型
        /// </summary>
        /// <param name="iProbeId">【探头编号】</param>
        /// <returns>成功与否：true，false</returns>
        public static bool resetProbeTypeInOneTunnel(string iProbeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_TYPE_ID + " = NULL");
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_ID + " = '" + iProbeId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 根据【探头】编号，获取该【探头】的详细信息
        /// </summary>
        /// <param name="probeId">【探头】编号</param>
        /// <returns>【探头】信息</returns>
        public static DataSet selectProbeManageInfoByProbeId(string strProbeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_ID + " = '" + strProbeId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 根据探头名称，获取该探头的详细信息
        /// </summary>
        /// <param name="probeName">探头名称</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbeManageInfoByProbeName(string strProbeName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_NAME + " = '" + strProbeName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【探头信息】修改
        /// </summary>
        /// <param name="probeEntity">【探头实体】</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateProbeManageInfo(Probe probeEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + ProbeManageDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + ProbeManageDbConstNames.PROBE_NAME + " = '" + probeEntity.ProbeName + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_TYPE_ID + " = '" + probeEntity.ProbeType + "'");
            // 2014/5/29 add by wuxin Start
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_TYPE_DISPLAY_NAME + " = '" + probeEntity.ProbeTypeDisplayName + "'");
            // 2014/5/29 add by wuxin End
            sqlStr.Append(", " + ProbeManageDbConstNames.TUNNEL_ID + " = '" + probeEntity.Tunnel + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_X + " = '" + probeEntity.ProbeLocationX + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_Y + " = '" + probeEntity.ProbeLocationY + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_LOCATION_Z + " = '" + probeEntity.ProbeLocationZ + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.PROBE_DESCRIPTION + " = '" + probeEntity.ProbeDescription + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.IS_MOVE + " = '" + probeEntity.IsMove + "'");
            sqlStr.Append(", " + ProbeManageDbConstNames.FAR_FROM_FRONTAL + " = '" + probeEntity.FarFromFrontal + "'");
            sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_ID + " = '" + probeEntity.ProbeId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【探头信息】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteProbeManageInfo(string[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + ProbeManageDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + ProbeManageDbConstNames.PROBE_ID + " = '" + iPkIdxsArr[i] + "'");

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }
    }
}
