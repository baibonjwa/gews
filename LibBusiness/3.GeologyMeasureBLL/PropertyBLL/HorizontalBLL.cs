// ******************************************************************
// 概  述：水平业务逻辑
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：V1.0
// 版本信息：
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
    public class HorizontalBLL
    {
        /// <summary>
        /// 获取所有<水平>信息
        /// </summary>
        /// <returns><水平>信息</returns>
        public static DataSet selectAllHorizontalInfo()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + HorizontalDbConstNames.TABLE_NAME);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// 通过<水平编号>，获取该<水平>信息
        /// </summary>
        /// <param name="iHorizontalId"><水平编号></param>
        /// <returns><矿井>信息</returns>
        public static DataSet selectHorizontalInfoByHorizontalId(int iHorizontalId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + HorizontalDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + HorizontalDbConstNames.HORIZONTAL_ID + " = " + iHorizontalId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<矿井编号>，获取该<矿井>下所有<水平>信息
        /// </summary>
        /// <returns><水平>信息</returns>
        public static DataSet selectHorizontalInfoByMineId(int iMineId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + HorizontalDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + HorizontalDbConstNames.MINE_ID + " = " + iMineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 水平信息登录
        /// </summary>
        /// <param name="horizontalEntity">水平实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertHorizontalInfo(HorizontalEntity horizontalEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + HorizontalDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + HorizontalDbConstNames.HORIZONTAL_NAME);
            sqlStr.Append(", " + HorizontalDbConstNames.MINE_ID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + horizontalEntity.HorizontalName + "'");
            sqlStr.Append(", '" + horizontalEntity.Mine.MineId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 水平信息修改
        /// </summary>
        /// <param name="horizontalEntity">水平实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateHorizontalInfo(HorizontalEntity horizontalEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + HorizontalDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + HorizontalDbConstNames.HORIZONTAL_NAME + " = '" + horizontalEntity.HorizontalName + "'");
            sqlStr.Append(", " + HorizontalDbConstNames.MINE_ID + " = '" + horizontalEntity.Mine.MineId + "'");
            sqlStr.Append(" WHERE " + HorizontalDbConstNames.HORIZONTAL_ID + " = " + horizontalEntity.HorizontalId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 水平信息删除
        /// </summary>
        /// <param name="ihorizontalId">删除数据主键</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteHorizontalInfo(int ihorizontalId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + HorizontalDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + HorizontalDbConstNames.HORIZONTAL_ID + " = " + ihorizontalId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
