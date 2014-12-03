// ******************************************************************
// 概  述：矿井业务逻辑
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
    public class MineBLL
    {
        /// <summary>
        /// 获取全部矿井信息
        /// </summary>
        /// <returns>全部矿井信息</returns>
        public static DataSet selectAllMineInfo()
        {
            string sqlStr = "SELECT * FROM " + MineDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 通过<矿井编号>，获取该<矿井>信息
        /// </summary>
        /// <returns><矿井>信息</returns>
        public static DataSet selectMineInfoByMineId(int iMineId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + MineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MineDbConstNames.MINE_ID + " = " + iMineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 判断矿井名称是否重复
        /// </summary>
        /// <returns><矿井>信息</returns>
        public static bool isMineNameExistInfoByMineName(string sMineName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + MineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MineDbConstNames.MINE_NAME + " = '" + sMineName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());

            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 矿井信息登录
        /// </summary>
        /// <param name="mineEntity">矿井实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertMineInfo(MineEntity mineEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + MineDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + MineDbConstNames.MINE_NAME);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + mineEntity.MineName + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 矿井信息修改
        /// </summary>
        /// <param name="mineEntity">矿井实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateMineInfo(MineEntity mineEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + MineDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + MineDbConstNames.MINE_NAME + " = '" + mineEntity.MineName + "'");
            sqlStr.Append(" WHERE " + MineDbConstNames.MINE_ID + " = " + mineEntity.MineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 矿井信息删除
        /// </summary>
        /// <param name="iMineId">删除数据主键</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteMineInfo(int iMineId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + MineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MineDbConstNames.MINE_ID + " = " + iMineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
