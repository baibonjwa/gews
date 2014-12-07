// ******************************************************************
// 概  述：井下数据煤层赋存业务逻辑
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

namespace LibBusiness
{
    public class CoalExistenceBLL
    {
        /// <summary>
        /// 获取全部煤层赋存信息
        /// </summary>
        /// <returns>全部煤岐赋存信息</returns>
        public static DataSet selectCoalExistence()
        {
            string sqlStr = "SELECT * FROM " + CoalExistenceDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }


        /// <summary>
        /// 获取全部煤层赋存信息
        /// </summary>
        /// <returns>全部煤岐赋存信息</returns>
        public static DataSet selectCoalExistenceWithCondition(int iTunnelId, string startTime, string endTime)
        {
            string sqlStr = "SELECT * FROM " + CoalExistenceDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + CoalExistenceDbConstNames.TUNNEL_ID + " = " + iTunnelId;
            sqlStr += " AND " + CoalExistenceDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取全部煤层赋存信息
        /// </summary>
        /// <returns>全部煤岐赋存信息</returns>
        public static DataSet selectCoalExistence(int id)
        {
            string sqlStr = "SELECT * FROM " + CoalExistenceDbConstNames.TABLE_NAME + " WHERE " + CoalExistenceDbConstNames.ID + " = " + id;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            CoalExistence cEntity = new CoalExistence();
            if (ds.Tables[0].Rows.Count > 0)
            {
                cEntity.Id = Convert.ToInt32(ds.Tables[0].Rows[0][CoalExistenceDbConstNames.ID].ToString());
                cEntity.Tunnel.TunnelId = Convert.ToInt32(ds.Tables[0].Rows[0][CoalExistenceDbConstNames.TUNNEL_ID].ToString());
                cEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][CoalExistenceDbConstNames.COORDINATE_X].ToString());
                cEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][CoalExistenceDbConstNames.COORDINATE_Y].ToString());
                cEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][CoalExistenceDbConstNames.COORDINATE_Z].ToString());
            }
            return ds;
        }

        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectCoalExistence(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + CoalExistenceDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + CoalExistenceDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用查询
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectCoalExistenceWithCondition(int iStartIndex, int iEndIndex, int iTunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + CoalExistenceDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + CoalExistenceDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + CoalExistenceDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sb.Append(" AND " + CoalExistenceDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");
            sb.Append(" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 修改煤层赋存信息
        /// </summary>
        /// <param name="m">工作面瓦斯涌出动态特征井下数据实体</param>
        /// <param name="ceEntity">煤层赋存实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateCoalExistence(CoalExistence ceEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + CoalExistenceDbConstNames.TABLE_NAME + " SET " + PreWarningDataCommonBLL.sqlUpdate(ceEntity) + ",");
            sb.Append(CoalExistenceDbConstNames.IS_LEVEL_DISORDER + "=");
            sb.Append(ceEntity.IsLevelDisorder + "," + CoalExistenceDbConstNames.IS_LEVEL_CHANGE + "=");
            sb.Append(ceEntity.IsLevelChange + "," + CoalExistenceDbConstNames.COAL_THICK_CHANGE + "=");
            sb.Append(ceEntity.CoalThickChange + "," + CoalExistenceDbConstNames.TECTONIC_COAL_THICK + "=");
            sb.Append(ceEntity.TectonicCoalThick + "," + CoalExistenceDbConstNames.COAL_DISTORY_LEVEL + "='");
            sb.Append(ceEntity.CoalDistoryLevel + "'," + CoalExistenceDbConstNames.IS_TOWARDS_CHANGE + "=");
            sb.Append(ceEntity.IsTowardsChange + " WHERE " + CoalExistenceDbConstNames.ID + "=");
            sb.Append(ceEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除煤层赋存信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteCoalExistence(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + CoalExistenceDbConstNames.TABLE_NAME + " WHERE " + CoalExistenceDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
