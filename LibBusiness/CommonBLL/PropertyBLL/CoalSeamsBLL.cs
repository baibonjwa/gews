// ******************************************************************
// 概  述：煤层业务逻辑
// 作  者：伍鑫
// 创建日期：2014/03/04
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDatabase;
using System.Data;
using LibEntity;

namespace LibBusiness
{
    public class CoalSeamsBLL
    {
        /// <summary>
        /// 获取全部煤层信息
        /// </summary>
        /// <returns>全部煤层信息</returns>
        public static DataSet selectAllCoalSeamsInfo()
        {
            string sqlStr = "SELECT * FROM " + CoalSeamsDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// <煤层>信息登录
        /// </summary>
        /// <param name="coalSeamsEntity"><煤层>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertCoalSeamsInfo(CoalSeamsEntity coalSeamsEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + CoalSeamsDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + CoalSeamsDbConstNames.COAL_SEAMS_NAME);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + coalSeamsEntity.CoalSeamsName + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <煤层>信息修改
        /// </summary>
        /// <param name="mineEntity"><煤层>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateCoalSeamsInfo(CoalSeamsEntity coalSeamsEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + CoalSeamsDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + CoalSeamsDbConstNames.COAL_SEAMS_NAME + " = '" + coalSeamsEntity.CoalSeamsName + "'");
            sqlStr.Append(" WHERE " + CoalSeamsDbConstNames.COAL_SEAMS_ID + " = " + coalSeamsEntity.CoalSeamsId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <煤层>信息删除
        /// </summary>
        /// <param name="iCoalSeamsId">删除数据主键</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteCoalSeamsInfo(int iCoalSeamsId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + CoalSeamsDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + CoalSeamsDbConstNames.COAL_SEAMS_ID + " = " + iCoalSeamsId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
