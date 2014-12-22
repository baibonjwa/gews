// ******************************************************************
// 概  述：岩性业务逻辑
// 作  者：伍鑫
// 创建日期：2013/12/02
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDatabase;
using LibEntity;
using System.Data;

namespace LibBusiness
{
    public class LithologyBLL
    {
        /// <summary>
        /// 获取全部岩性信息
        /// </summary>
        /// <returns>全部岩性信息</returns>
        public static DataSet selectAllLithologyInfo()
        {
            string sqlStr = "SELECT * FROM " + LithologyDbConstNames.TABLE_NAME;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取某个岩性信息
        /// </summary>
        /// <returns>获取某个岩性信息</returns>
        public static DataSet selectLithologyInfoByLithologyId(int lithologyId)
        {
            string sqlStr = "SELECT * FROM " + LithologyDbConstNames.TABLE_NAME +
                " WHERE " + LithologyDbConstNames.LITHOLOGY_ID + " = " + lithologyId;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取某个岩性信息
        /// </summary>
        /// <returns>获取某个岩性信息</returns>
        public static DataSet selectLithologyInfoByLithologyName(string lithologyName)
        {
            string sqlStr = "SELECT * FROM " + LithologyDbConstNames.TABLE_NAME +
                " WHERE " + LithologyDbConstNames.LITHOLOGY_NAME + " = '" + lithologyName + "'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 获取煤层岩性信息
        /// </summary>
        /// <returns>获取某个岩性信息</returns>
        public static DataSet selectCoalInfo()
        {
            string sqlStr = "SELECT * FROM " + LithologyDbConstNames.TABLE_NAME +
                " WHERE " + LithologyDbConstNames.LITHOLOGY_NAME + " like '%煤%'";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }
    }
}
