// ******************************************************************
// 概  述：井下数据瓦斯业务逻辑
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
    public class GasDataBLL
    {
        /// <summary>
        /// 获取全部瓦斯数据信息
        /// </summary>
        /// <returns>瓦斯数据信息</returns>
        public static DataSet selectGasData()
        {
            string sqlStr = "SELECT * FROM " + GasDataDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取全部瓦斯数据信息
        /// </summary>
        /// <returns>瓦斯数据信息</returns>
        public static DataSet selectGasDataWithCondition(int iTunnelId, string startTime, string endTime)
        {
            string sqlStr = "SELECT * FROM " + GasDataDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + GasDataDbConstNames.TUNNEL_ID + " = " + iTunnelId;
            sqlStr += " AND " + GasDataDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'";
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
        public static DataSet selectGasData(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + GasDataDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + GasDataDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用查询带条件
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <param name="iTunnelId">巷道ID</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>显示部分数据</returns>
        public static DataSet selectGasDataWithCondition(int iStartIndex, int iEndIndex, int iTunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + GasDataDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + GasDataDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + GasDataDbConstNames.TUNNEL_ID + " = " + iTunnelId);
            sb.Append(" AND " + GasDataDbConstNames.DATETIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");
            sb.Append(" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append(" AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }


        /// <summary>
        /// 插入瓦斯信息
        /// </summary>
        /// <param name="gdEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>瓦斯信息</returns>
        public static bool insertGasDataInfo(GasDataEntity gdEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + GasDataDbConstNames.TABLE_NAME + " (");
            sb.Append(PreWarningDataCommonBLL.sqlFront() + ",");
            sb.Append(GasDataDbConstNames.POWER_FALIURE + ",");
            sb.Append(GasDataDbConstNames.DRILL_TIMES + ",");
            sb.Append(GasDataDbConstNames.GAS_TIMES + ",");
            sb.Append(GasDataDbConstNames.TEMP_DOWN_TIMES + ",");
            sb.Append(GasDataDbConstNames.COAL_BANG_TIMES + ",");
            sb.Append(GasDataDbConstNames.CRATER_TIMES + ",");
            sb.Append(GasDataDbConstNames.STOPER_TIMES + ",");
            sb.Append(GasDataDbConstNames.GAS_THICKNESS + ") VALUES(");
            sb.Append(PreWarningDataCommonBLL.sqlBack(gdEntity) + ",");
            sb.Append(gdEntity.PowerFailure + ",");
            sb.Append(gdEntity.DrillTimes + ",");
            sb.Append(gdEntity.GasTimes + ",");
            sb.Append(gdEntity.TempDownTimes + ",");
            sb.Append(gdEntity.CoalBangTimes + ",");
            sb.Append(gdEntity.CraterTimes + ",");
            sb.Append(gdEntity.StoperTimes + ",");
            sb.Append(gdEntity.GasThickness + ")");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改瓦斯信息
        /// </summary>
        /// <param name="gdEntity">工作面动态防突井下数据实体</param>
        /// <param name="tunnelEntity">巷道实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateGasDataInfo(GasDataEntity gdEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + GasDataDbConstNames.TABLE_NAME + " SET " + PreWarningDataCommonBLL.sqlUpdate(gdEntity) + "," + GasDataDbConstNames.POWER_FALIURE + "=");
            sb.Append(gdEntity.PowerFailure + "," + GasDataDbConstNames.DRILL_TIMES + "=");
            sb.Append(gdEntity.DrillTimes + "," + GasDataDbConstNames.GAS_TIMES + "=");
            sb.Append(gdEntity.GasTimes + "," + GasDataDbConstNames.TEMP_DOWN_TIMES + "=");
            sb.Append(gdEntity.TempDownTimes + "," + GasDataDbConstNames.COAL_BANG_TIMES + "=");
            sb.Append(gdEntity.CoalBangTimes + "," + GasDataDbConstNames.CRATER_TIMES + "=");
            sb.Append(gdEntity.CraterTimes + "," + GasDataDbConstNames.STOPER_TIMES + "=");
            sb.Append(gdEntity.StoperTimes + "," + GasDataDbConstNames.GAS_THICKNESS + "=");
            sb.Append(gdEntity.GasThickness + "WHERE " + GasDataDbConstNames.ID + "=");
            sb.Append(gdEntity.Id);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除瓦斯信息
        /// </summary>
        /// <param name="id">信息编号</param>
        /// <returns>是否删除成功?true:false</returns>
        public static bool deleteGasDataInfo(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + GasDataDbConstNames.TABLE_NAME + " WHERE " + GasDataDbConstNames.ID + " = " + id;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
