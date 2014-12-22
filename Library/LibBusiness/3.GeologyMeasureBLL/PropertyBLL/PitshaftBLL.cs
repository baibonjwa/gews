// ******************************************************************
// 概  述：井筒业务逻辑
// 作  者：伍鑫
// 创建日期：2014/03/06
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
    public class PitshaftBLL
    {
        /// <summary>
        /// 获取全部<井筒>信息
        /// </summary>
        /// <returns>全部<井筒>信息</returns>
        public static DataSet selectAllPitshaftInfo()
        {
            string sqlStr = "SELECT * FROM " + PitshaftDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取【井筒】
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectPitshaftInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + PitshaftDbConstNames.PITSHAFT_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + PitshaftDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 判断<井筒>名称是否存在
        /// </summary>
        /// <param name="strPitshaftName"><井筒>名称</param>
        /// <returns>成功与否：true，false</returns>
        public static bool isPitshaftNameExist(string strPitshaftName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_NAME + " = '" + strPitshaftName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 根据<井筒>名称，获取该<井筒>的详细信息
        /// </summary>
        /// <param name="faultageName"><井筒>名称</param>
        /// <returns><井筒>信息</returns>
        public static DataSet selectPitshaftInfoByPitshaftName(string strPitshaftName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_NAME + " = '" + strPitshaftName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }


        /// <summary>
        /// 20140428 lyf
        /// 根据<井筒>名称，获取该<井筒>的绑定ID
        /// </summary>
        /// <param name="faultageName"><井筒>名称</param>
        /// <returns><井筒>绑定ID</returns>
        public static string selectPitshaftInfoBIDByPitshaftName(string strPitshaftName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + PitshaftDbConstNames.BID + " FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_NAME + " = '" + strPitshaftName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            string sBID = "";
            try
            {
                sBID = ds.Tables[0].Rows[0][0].ToString();
                return sBID;
            }
            catch
            {
                return sBID;
            }
        }

        /// <summary>
        /// 20140428 lyf
        /// 通过井筒主键（井筒编号），获取井筒绑定ID
        /// </summary>
        /// <param name="iPkIdxs">井筒主键</param>
        /// <returns></returns>
        public static string selectPitshaftInfoBIDByPitshaftPK(int iPkIdxs)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + PitshaftDbConstNames.BID + " FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = '" + iPkIdxs + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            string sBID = "";
            try
            {
                sBID = ds.Tables[0].Rows[0][0].ToString();
                return sBID;
            }
            catch
            {
                return sBID;
            }
        }


        /// <summary>
        /// <井筒>信息登录
        /// </summary>
        /// <param name="pitshaftEntity"><井筒>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertPitshaftInfo(Pitshaft pitshaftEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + PitshaftDbConstNames.PITSHAFT_NAME);
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_TYPE_ID);
            sqlStr.Append(", " + PitshaftDbConstNames.WELLHEAD_ELEVATION);
            sqlStr.Append(", " + PitshaftDbConstNames.WELLBOTTOM_ELEVATION);
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_COORDINATE_X);
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_COORDINATE_Y);
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_X);
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_Y);
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_Z);
            sqlStr.Append(", " + PitshaftDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + pitshaftEntity.PitshaftName + "'");
            sqlStr.Append(", '" + pitshaftEntity.PitshaftTypeId + "'");
            sqlStr.Append(", '" + pitshaftEntity.WellheadElevation + "'");
            sqlStr.Append(", '" + pitshaftEntity.WellbottomElevation + "'");
            sqlStr.Append(", '" + pitshaftEntity.PitshaftCoordinateX + "'");
            sqlStr.Append(", '" + pitshaftEntity.PitshaftCoordinateY + "'");
            sqlStr.Append(", '" + pitshaftEntity.FigureCoordinateX + "'");
            sqlStr.Append(", '" + pitshaftEntity.FigureCoordinateY + "'");
            sqlStr.Append(", '" + pitshaftEntity.FigureCoordinateZ + "'");
            sqlStr.Append(", '" + pitshaftEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <井筒>信息修改
        /// </summary>
        /// <param name="pitshaftEntity"><井筒>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updatePitshaftInfo(Pitshaft pitshaftEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + PitshaftDbConstNames.PITSHAFT_NAME + " = '" + pitshaftEntity.PitshaftName + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_TYPE_ID + " = '" + pitshaftEntity.PitshaftTypeId + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.WELLHEAD_ELEVATION + " = '" + pitshaftEntity.WellheadElevation + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.WELLBOTTOM_ELEVATION + " = '" + pitshaftEntity.WellbottomElevation + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_COORDINATE_X + " = '" + pitshaftEntity.PitshaftCoordinateX + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.PITSHAFT_COORDINATE_Y + " = '" + pitshaftEntity.PitshaftCoordinateY + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_X + " = '" + pitshaftEntity.PitshaftCoordinateX + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_Y + " = '" + pitshaftEntity.FigureCoordinateY + "'");
            sqlStr.Append(", " + PitshaftDbConstNames.FIGURE_COORDINATE_Z + " = '" + pitshaftEntity.FigureCoordinateZ + "'");
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = " + pitshaftEntity.PitshaftId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【井筒】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deletePitshaftInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + PitshaftDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 通过【井筒】编号，获取【井筒】
        /// </summary>
        /// <param name="faultageId">【揭井筒露断层】编号</param>
        /// <returns>【井筒】</returns>
        public static DataSet selectPitshaftInfoByPitshaftId(int iPitshaftId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = " + iPitshaftId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
    }
}
