// ******************************************************************
// 概  述：采区业务逻辑
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
    public class MiningAreaBLL
    {
        /// <summary>
        /// 获取所有<采区>信息
        /// </summary>
        /// <returns><采区>信息</returns>
        public static DataSet selectAllMiningAreaInfo()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + MiningAreaDbConstNames.TABLE_NAME);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<水平编号>，获取该<水平>下所有<采区>信息
        /// </summary>
        /// <returns><采区>信息</returns>
        public static DataSet selectMiningAreaInfoByHorizontalId(int iHorizontalId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + MiningAreaDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MiningAreaDbConstNames.HORIZONTAL_ID + " = " + iHorizontalId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<采区编号>，获取<采区>信息
        /// </summary>
        /// <returns><采区>信息</returns>
        public static DataSet selectMiningAreaInfoByMiningAreaId(int iMiningAreaId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + MiningAreaDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MiningAreaDbConstNames.MININGAREA_ID + " = " + iMiningAreaId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 采区信息登录
        /// </summary>
        /// <param name="miningAreaEntity">采区实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertMiningAreaInfo(string miningAreaName, int horizontalId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + MiningAreaDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + MiningAreaDbConstNames.MININGAREA_NAME);
            sqlStr.Append(", " + MiningAreaDbConstNames.HORIZONTAL_ID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + miningAreaName + "'");
            sqlStr.Append(", '" + horizontalId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 采区信息修改
        /// </summary>
        /// <param name="miningAreaEntity">采区实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateMiningAreaInfo(int miningAreaId, string miningAreaName, int horizontalId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + MiningAreaDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + MiningAreaDbConstNames.MININGAREA_NAME + " = '" + miningAreaName + "'");
            sqlStr.Append(", " + MiningAreaDbConstNames.HORIZONTAL_ID + " = '" + horizontalId + "'");
            sqlStr.Append(" WHERE " + MiningAreaDbConstNames.MININGAREA_ID + " = " + miningAreaId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 采区信息删除
        /// </summary>
        /// <param name="iMiningAreaId">删除数据主键</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteMiningAreaInfo(int iMiningAreaId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + MiningAreaDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + MiningAreaDbConstNames.MININGAREA_ID + " = " + iMiningAreaId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 条件过滤巷道
        /// </summary>
        /// <param name="tunnelFilterRules">过滤规则</param>
        /// <param name="iWorkingFaceID">工作面ID</param>
        /// <returns>过滤后巷道信息</returns>
        public static DataSet selectWorkingFaceListByMiningAreaId(int iMiningAreaID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + iMiningAreaID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
