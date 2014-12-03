// ******************************************************************
// 概  述：队别业务逻辑
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
using LibDatabase;
using LibEntity;
using LibCommon;

namespace LibBusiness
{
    public class TeamBLL
    {
        /// <summary>
        /// 登入队别信息
        /// </summary>
        /// <param name="teamInfoEntity">队别实体</param>
        /// <returns>队别信息</returns>
        public static bool insertTeamInfo(TeamInfoEntity teamInfoEntity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + TeamDbConstNames.TABLE_NAME + " (");
            sb.Append(TeamDbConstNames.TEAM_NAME + ",");
            sb.Append(TeamDbConstNames.TEAM_LEADER + ",");
            sb.Append(TeamDbConstNames.TEAM_MEMBER);
            sb.Append(") VALUES ('");
            sb.Append(teamInfoEntity.TeamName + "','");
            sb.Append(teamInfoEntity.TeamLeader +"','");
            sb.Append(teamInfoEntity.TeamMember + "')");
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 查询队别名称（去重）
        /// </summary>
        /// <returns>队别名</returns>
        public static DataSet selectTeamInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT DISTINCT * FROM " + TeamDbConstNames.TABLE_NAME);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 查询某巷道信息
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public static TeamInfoEntity selectTeamInfoByID(int teamID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TeamDbConstNames.TABLE_NAME + " WHERE " + TeamDbConstNames.ID + " = " + teamID;
            DataSet ds = db.ReturnDS(sql);
            TeamInfoEntity teamInfoEntity = new TeamInfoEntity();
            if (ds.Tables[0].Rows.Count > 0)
            {
                teamInfoEntity.TeamID = Convert.ToInt32(ds.Tables[0].Rows[0][TeamDbConstNames.ID].ToString());
                teamInfoEntity.TeamName = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_NAME].ToString();
                teamInfoEntity.TeamLeader = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_LEADER].ToString();
                teamInfoEntity.TeamMember = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_MEMBER].ToString();
            }
            return teamInfoEntity;
        }

        /// <summary>
        /// 分页用队别信息查询
        /// </summary>
        /// <param name="iStartIndex">开始编号</param>
        /// <param name="iEndIndex">结束编号</param>
        /// <returns>队别信息</returns>
        public static DataSet selectAllTeamInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TeamDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TeamDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            //string sql = "SELECT * FROM T_TEAM_INFO";
            //DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查找所有队别信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectAllTeamInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TeamDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 修改队别信息
        /// </summary>
        /// <param name="teamInfoEntity">队别实体</param>
        /// <returns></returns>
        public static bool updateTeamInfo(TeamInfoEntity teamInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + TeamDbConstNames.TABLE_NAME + " SET " + TeamDbConstNames.TEAM_NAME + " = '");
            sqlStr.Append(teamInfoEntity.TeamName + "'," + TeamDbConstNames.TEAM_LEADER + " ='");
            sqlStr.Append(teamInfoEntity.TeamLeader + "'," + TeamDbConstNames.TEAM_MEMBER + " ='");
            sqlStr.Append(teamInfoEntity.TeamMember + "' WHERE " + TeamDbConstNames.ID + " = ");
            sqlStr.Append(teamInfoEntity.TeamID);
            //Alert.alert(sqlStr.ToString());
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除队别信息
        /// </summary>
        /// <param name="teamInfoEntity">队别实体</param>
        /// <returns></returns>
        public static bool deleteTeamInfo(TeamInfoEntity teamInfoEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + TeamDbConstNames.TABLE_NAME + " WHERE " + TeamDbConstNames.ID + " =" + teamInfoEntity.TeamID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }

        /// <summary>
        /// 查询某队别下的队员信息
        /// </summary>
        /// <param name="teamName">队别名称</param>
        /// <returns></returns>
        public static DataSet selectTeamInfoByTeamName(string teamName)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + TeamDbConstNames.TABLE_NAME );
            sqlStr.Append(" WHERE ");
            sqlStr.Append(TeamDbConstNames.TEAM_NAME + " = '");
            sqlStr.Append(teamName + "'");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
    }
}
