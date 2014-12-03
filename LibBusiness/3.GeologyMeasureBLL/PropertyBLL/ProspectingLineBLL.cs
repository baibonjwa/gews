// ******************************************************************
// 概  述：勘探线业务逻辑
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class ProspectingLineBLL
    {
        /// <summary>
        /// 获取全部<勘探线>信息
        /// </summary>
        /// <returns>全部<勘探线>信息</returns>
        public static DataSet selectAllProspectingLineInfo()
        {
            string sqlStr = "SELECT * FROM " + ProspectingLineDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取<勘探线>信息(分页控件用)
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns><勘探线>信息</returns>
        public static DataSet selectProspectingLineInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + ProspectingLineDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// <勘探线>信息登录
        /// </summary>
        /// <param name="prospectingLineEntity"><勘探线>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertProspectingLineInfo(ProspectingLineEntity prospectingLineEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + ProspectingLineDbConstNames.PROSPECTING_LINE_NAME);
            sqlStr.Append(", " + ProspectingLineDbConstNames.PROSPECTING_BOREHOLE);
            sqlStr.Append(", " + ProspectingLineDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + prospectingLineEntity.ProspectingLineName + "'");
            sqlStr.Append(", '" + prospectingLineEntity.ProspectingBorehole + "'");
            sqlStr.Append(", '" + prospectingLineEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 判断<勘探线>名称是否存在
        /// </summary>
        /// <param name="strProspectingLineName"><勘探线>名称</param>
        /// <returns>成功与否：true，false</returns>
        public static bool isProspectingLineNameExist(string strProspectingLineName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_NAME + " = '" + strProspectingLineName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// <勘探线>信息删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteProspectingLineInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + ProspectingLineDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 通过<勘探线>编号，获取<勘探线>信息
        /// </summary>
        /// <param name="iProspectingLineId"><勘探线>编号</param>
        /// <returns><勘探线>信息</returns>
        public static DataSet selectProspectingLineInfoByProspectingLineId(int iProspectingLineId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + " = " + iProspectingLineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<勘探线>名称获取<勘探线>编号
        /// </summary>
        /// <param name="name"><勘探线>名称</param>
        /// <param name="ret"><勘探线>编号</param>
        /// <returns></returns>
        public static bool selectProspectingLineIdByProspectingLineName(string name, out int ret)
        {
            ret = -1;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + " FROM " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_NAME + " = '" + name + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            try
            {
                ret = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 20140507 lyf
        /// 通过<勘探线>ID获取<勘探线>绑定ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public static bool selectProspectingLineBIDByProspectingLineID(int id, out string bid)
        {
            bid = "";
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + ProspectingLineDbConstNames.BID + " FROM " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + " = '" + id + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            try
            {
                bid = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// <勘探线>信息修改
        /// </summary>
        /// <param name="prospectingLineEntity"><勘探线>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateProspectingLineInfo(ProspectingLineEntity prospectingLineEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + ProspectingLineDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + ProspectingLineDbConstNames.PROSPECTING_LINE_NAME + " = '" + prospectingLineEntity.ProspectingLineName + "'");
            sqlStr.Append(", " + ProspectingLineDbConstNames.PROSPECTING_BOREHOLE + " = '" + prospectingLineEntity.ProspectingBorehole + "'");
            sqlStr.Append(" WHERE " + ProspectingLineDbConstNames.PROSPECTING_LINE_ID + " = " + prospectingLineEntity.ProspectingLineId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
