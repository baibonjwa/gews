// ******************************************************************
// 概  述：预警数据（井下数据）通用业务逻辑
// 作  者：杨小颖
// 创建日期：2014/02/15
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;

namespace LibBusiness
{
    public class PreWarningDataCommonBLL
    {
        /// <summary>
        /// 返回井下数据添加SQL语句前部拼接用字符串
        /// </summary>
        /// <returns></returns>
        public static string sqlFront()
        {
            string sql = PreWarningDataDbConstNames.TUNNEL_ID + ","
                + PreWarningDataDbConstNames.COORDINATE_X + ","
                + PreWarningDataDbConstNames.COORDINATE_Y + ","
                + PreWarningDataDbConstNames.COORDINATE_Z + ","
                + PreWarningDataDbConstNames.WORK_STYLE + ","
                + PreWarningDataDbConstNames.WORK_TIME + ","
                + PreWarningDataDbConstNames.TEAM_NAME + ","
                + PreWarningDataDbConstNames.SUBMITTER + ","
                + PreWarningDataDbConstNames.DATETIME;
            return sql;
        }
        /// <summary>
        /// 返回井下数据添加SQL语句后部拼接用字符串
        /// </summary>
        /// <param name="mdEntity"></param>
        /// <returns></returns>
        public static string sqlBack(MineData mdEntity)
        {
            string sql = mdEntity.Tunnel.TunnelID + "," +
                mdEntity.CoordinateX + "," +
                mdEntity.CoordinateY + "," +
                mdEntity.CoordinateZ + ",'" +
                mdEntity.WorkStyle + "','" +
                mdEntity.WorkTime + "','" +
                mdEntity.TeamName + "','" +
                mdEntity.Submitter + "','" +
                mdEntity.Datetime + "'";
            return sql;

        }
        /// <summary>
        /// 返回进下数据修改SQL语句拼接用字符串
        /// </summary>
        /// <param name="mdEntity"></param>
        /// <returns></returns>
        public static string sqlUpdate(MineData mdEntity)
        {
            string sql = PreWarningDataDbConstNames.TUNNEL_ID + "=" + mdEntity.Tunnel.TunnelID + "," +
                PreWarningDataDbConstNames.COORDINATE_X + "=" + mdEntity.CoordinateX + "," +
                PreWarningDataDbConstNames.COORDINATE_Y + "=" + mdEntity.CoordinateY + "," +
                PreWarningDataDbConstNames.COORDINATE_Z + "=" + mdEntity.CoordinateZ + "," +
                PreWarningDataDbConstNames.WORK_STYLE + "='" + mdEntity.WorkStyle + "'," +
                PreWarningDataDbConstNames.WORK_TIME + "='" + mdEntity.WorkTime + "'," +
                PreWarningDataDbConstNames.TEAM_NAME + "='" + mdEntity.TeamName + "'," +
                PreWarningDataDbConstNames.SUBMITTER + "='" + mdEntity.Submitter + "'," +
                PreWarningDataDbConstNames.DATETIME + "='" + mdEntity.Datetime + "'";
            return sql;
        }
    }
}
