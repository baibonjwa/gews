// ******************************************************************
// 概  述：揭露断层业务逻辑
// 作  者：伍鑫
// 创建日期：2013/11/27
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
    public class FaultageBLL
    {
        /// <summary>
        /// 根据一行数据获取断层实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Faultage GetFaultageEnt(DataRow dr)
        {
            Faultage ret = null;
            ret = new Faultage();

            //断层ID
            ret.FaultageId = int.Parse(dr[FaultageDbConstNames.FAULTAGE_ID].ToString());
            //断层名称
            ret.FaultageName = dr[FaultageDbConstNames.FAULTAGE_NAME].ToString();
            //落差
            ret.Gap = dr[FaultageDbConstNames.GAP].ToString();
            //倾角
            ret.Angle = Convert.ToDouble(dr[FaultageDbConstNames.ANGLE].ToString());
            //类型
            ret.Type = dr[FaultageDbConstNames.TYPE].ToString();
            //走向
            ret.Trend = dr[FaultageDbConstNames.TREND].ToString();
            //断距
            ret.Separation = dr[FaultageDbConstNames.SEPARATION].ToString();
            //X坐标
            ret.CoordinateX = Convert.ToDouble(dr[FaultageDbConstNames.X].ToString());
            //Y坐标
            ret.CoordinateY = Convert.ToDouble(dr[FaultageDbConstNames.Y].ToString());
            //Z坐标
            ret.CoordinateZ = Convert.ToDouble(dr[FaultageDbConstNames.Z].ToString());
            //BID
            ret.BindingId = dr[FaultageDbConstNames.BID].ToString();

            return ret;
        }

        /// <summary>
        /// 获取全部【揭露断层】
        /// </summary>
        /// <returns>全部【揭露断层】</returns>
        public static DataSet selectAllFaultageInfo()
        {
            string sqlStr = "SELECT * FROM " + FaultageDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取【揭露断层】
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectFaultageInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + FaultageDbConstNames.FAULTAGE_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + FaultageDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过【揭露断层】编号，获取【揭露断层】
        /// </summary>
        /// <param name="faultageId">【揭露断层】编号</param>
        /// <returns>【揭露断层】</returns>
        public static DataSet selectFaultageInfoByFaultageId(int faultageId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_ID + " = " + faultageId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 20140429 lyf
        /// 通过【揭露断层】编号，获取【揭露断层】的绑定ID
        /// </summary>
        /// <param name="faultageId">【揭露断层】编号</param>
        /// <returns>【揭露断层】绑定ID</returns>
        public static string selectFaultageBIDByFaultageId(int faultageId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + FaultageDbConstNames.BID + " FROM " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_ID + " = " + faultageId);

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
        /// 根据【揭露断层】名称，获取该【揭露断层】的详细信息
        /// </summary>
        /// <param name="faultageName">【揭露断层】名称</param>
        /// <returns>【揭露断层】</returns>
        public static DataSet selectFaultageInfoByFaultageName(string faultageName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_NAME + " = '" + faultageName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【揭露断层】登录
        /// </summary>
        /// <param name="faultageEntity">【揭露断层】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertFaultageInfo(Faultage faultageEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + FaultageDbConstNames.FAULTAGE_NAME);
            sqlStr.Append(", " + FaultageDbConstNames.GAP);
            sqlStr.Append(", " + FaultageDbConstNames.ANGLE);
            sqlStr.Append(", " + FaultageDbConstNames.TYPE);
            sqlStr.Append(", " + FaultageDbConstNames.TREND);
            sqlStr.Append(", " + FaultageDbConstNames.SEPARATION);
            sqlStr.Append(", " + FaultageDbConstNames.X);
            sqlStr.Append(", " + FaultageDbConstNames.Y);
            sqlStr.Append(", " + FaultageDbConstNames.Z);
            sqlStr.Append(", " + FaultageDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + faultageEntity.FaultageName + "'");
            sqlStr.Append(", '" + faultageEntity.Gap + "'");
            sqlStr.Append(", '" + faultageEntity.Angle + "'");
            sqlStr.Append(", '" + faultageEntity.Type + "'");
            sqlStr.Append(", '" + faultageEntity.Trend + "'");
            sqlStr.Append(", '" + faultageEntity.Separation + "'");
            sqlStr.Append(", '" + faultageEntity.CoordinateX + "'");
            sqlStr.Append(", '" + faultageEntity.CoordinateY + "'");
            sqlStr.Append(", '" + faultageEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + faultageEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 判断【揭露断层】名称是否存在
        /// </summary>
        /// <param name="strFaultageName">【揭露断层】名称</param>
        /// <returns>成功与否：true，false</returns>
        public static bool isFaultageNameExist(string strFaultageName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_NAME + " = '" + strFaultageName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 【揭露断层】修改
        /// </summary>
        /// <param name="faultageEntity">【揭露断层】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateFaultageInfo(Faultage faultageEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + FaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + FaultageDbConstNames.FAULTAGE_NAME + " = '" + faultageEntity.FaultageName + "'");
            sqlStr.Append(", " + FaultageDbConstNames.GAP + " = '" + faultageEntity.Gap + "'");
            sqlStr.Append(", " + FaultageDbConstNames.ANGLE + " = '" + faultageEntity.Angle + "'");
            sqlStr.Append(", " + FaultageDbConstNames.TYPE + " = '" + faultageEntity.Type + "'");
            sqlStr.Append(", " + FaultageDbConstNames.TREND + " = '" + faultageEntity.Trend + "'");
            sqlStr.Append(", " + FaultageDbConstNames.SEPARATION + " = '" + faultageEntity.Separation + "'");
            sqlStr.Append(", " + FaultageDbConstNames.X + " = '" + faultageEntity.CoordinateX + "'");
            sqlStr.Append(", " + FaultageDbConstNames.Y + " = '" + faultageEntity.CoordinateY + "'");
            sqlStr.Append(", " + FaultageDbConstNames.Z + " = '" + faultageEntity.CoordinateZ + "'");
            sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_ID + " = '" + faultageEntity.FaultageId + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【揭露断层】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteFaultageInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + FaultageDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + FaultageDbConstNames.FAULTAGE_ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }
    }
}
