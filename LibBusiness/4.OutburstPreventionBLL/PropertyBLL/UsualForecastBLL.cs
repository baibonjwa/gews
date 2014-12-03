// ******************************************************************
// 概  述：井下数据日常预测业务逻辑
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
//      日常预测不再使用
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class UsualForecastBLL
    {
        /// <summary>
        /// 获取日常预测信息
        /// </summary>
        /// <returns>全部日常预测信息</returns>
        public static DataSet selectUsualForecast()
        {
            string sqlStr = "SELECT * FROM "+UsualForecastDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }
        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectUsualForecast(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY "+UsualForecastDbConstNames.ID+") AS rowid, * ");
            sb.Append("FROM " + UsualForecastDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
        /// <summary>
        /// 插入日常预测信息
        /// </summary>
        /// <param name="ufEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>日常预测信息</returns>
        public static bool insertUsualForecastInfo(UsualForecastEntity ufEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + UsualForecastDbConstNames.TABLE_NAME + " (" + PreWarningDataCommonBLL.sqlFront()+ "," + UsualForecastDbConstNames.IS_ROOF_DOWN + "," + UsualForecastDbConstNames.IS_SUPPORT_BROKEN + "," + UsualForecastDbConstNames.IS_COAL_WALL_DROP + "," + UsualForecastDbConstNames.IS_PART_ROOF_FALL + "," + UsualForecastDbConstNames.IS_BIG_ROOF_FALL + ") VALUES(");
            sb.Append(PreWarningDataCommonBLL.sqlBack(ufEntity) + ",");
            sb.Append(ufEntity.IsRoofDown + ",");
            sb.Append(ufEntity.IsSupportBroken + ",");
            sb.Append(ufEntity.IsCoalWallDrop + ",");
            sb.Append(ufEntity.IsPartRoolFall + ",");
            sb.Append(ufEntity.IsBigRoofFall + ")");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改日常预测信息
        /// </summary>
        /// <param name="ufEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateUsualForecastInfo(UsualForecastEntity ufEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + UsualForecastDbConstNames.TABLE_NAME + " SET " + UsualForecastDbConstNames.TUNNEL_ID + "=");
            sb.Append(ufEntity.TunnelID + "," + UsualForecastDbConstNames.X + "=");
            sb.Append(ufEntity.CoordinateX + "," + UsualForecastDbConstNames.Y + "=");
            sb.Append(ufEntity.CoordinateY + "," + UsualForecastDbConstNames.Z + "=");
            sb.Append(ufEntity.CoordinateZ + "," + UsualForecastDbConstNames.DATETIME + "='");
            sb.Append(ufEntity.Datetime + "'," + UsualForecastDbConstNames.IS_ROOF_DOWN + "=");
            sb.Append(ufEntity.IsRoofDown + "," + UsualForecastDbConstNames.IS_SUPPORT_BROKEN + "=");
            sb.Append(ufEntity.IsSupportBroken + "," + UsualForecastDbConstNames.IS_COAL_WALL_DROP + "=");
            sb.Append(ufEntity.IsCoalWallDrop + "," + UsualForecastDbConstNames.IS_PART_ROOF_FALL + "=");
            sb.Append(ufEntity.IsPartRoolFall + "," + UsualForecastDbConstNames.IS_BIG_ROOF_FALL + "=");
            sb.Append(ufEntity.IsBigRoofFall + "WHERE " + UsualForecastDbConstNames.ID + "=");
            sb.Append(ufEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除日常预测信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteUsualForecastInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + UsualForecastDbConstNames.TABLE_NAME + " WHERE " + UsualForecastDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
