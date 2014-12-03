// ******************************************************************
// 概  述：瓦斯涌出量点业务逻辑
// 作  者：伍鑫
// 创建日期：2013/12/08
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
    public class GasGushQuantityBLL
    {
        /// <summary>
        /// 【瓦斯涌出量点】登录
        /// </summary>
        /// <param name="gasGushQuantityEntity">【瓦斯涌出量点】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertGasGushQuantityInfo(GasGushQuantityEntity gasGushQuantityEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + GasGushQuantityDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + GasGushQuantityDbConstNames.X);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.Y);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.Z);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.ABSOLUTE_GAS_GUSH_QUANTITY);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.RELATIVE_GAS_GUSH_QUANTITY);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.WORKING_FACE_DAY_OUTPUT);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.STOPE_DATE);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.TUNNEL_ID);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.COAL_SEAMS_ID);
            sqlStr.Append(", " + GasGushQuantityDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + gasGushQuantityEntity.CoordinateX + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.CoordinateY + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.AbsoluteGasGushQuantity + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.RelativeGasGushQuantity + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.WorkingFaceDayOutput + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.StopeDate + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.TunnelID + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.CoalSeamsId + "'");
            sqlStr.Append(", '" + gasGushQuantityEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 获取【瓦斯涌出量点】
        /// </summary>
        /// <returns>【瓦斯涌出量点】</returns>
        public static DataSet selectAllGasGushQuantityInfo()
        {
            string sqlStr = "SELECT * FROM " + GasGushQuantityDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取【瓦斯涌出量点】
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectGasGushQuantityInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + GasGushQuantityDbConstNames.ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + GasGushQuantityDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过主键，获取【瓦斯涌出量点】
        /// </summary>
        /// <returns>【瓦斯涌出量点】/returns>
        public static DataSet selectGasGushQuantityInfoByPK(int iPrimaryKey)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasGushQuantityDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + GasGushQuantityDbConstNames.ID + " = " + iPrimaryKey);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【瓦斯涌出量点】修改
        /// </summary>
        /// <param name="gasGushQuantityEntity">【瓦斯涌出量点】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateGasGushQuantityInfo(GasGushQuantityEntity gasGushQuantityEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + GasGushQuantityDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + GasGushQuantityDbConstNames.X + " = '" + gasGushQuantityEntity.CoordinateX + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.Y + " = '" + gasGushQuantityEntity.CoordinateY + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.Z + " = '" + gasGushQuantityEntity.CoordinateZ + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.ABSOLUTE_GAS_GUSH_QUANTITY + " = '" + gasGushQuantityEntity.AbsoluteGasGushQuantity + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.RELATIVE_GAS_GUSH_QUANTITY + " = '" + gasGushQuantityEntity.RelativeGasGushQuantity + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.WORKING_FACE_DAY_OUTPUT + " = '" + gasGushQuantityEntity.WorkingFaceDayOutput + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.STOPE_DATE + " = '" + gasGushQuantityEntity.StopeDate + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.TUNNEL_ID + " = '" + gasGushQuantityEntity.TunnelID + "'");
            sqlStr.Append(", " + GasGushQuantityDbConstNames.COAL_SEAMS_ID + " = '" + gasGushQuantityEntity.CoalSeamsId + "'");
            sqlStr.Append(" WHERE " + GasGushQuantityDbConstNames.ID + " = " + gasGushQuantityEntity.PrimaryKey);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【瓦斯涌出量点】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteGasGushQuantityInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + GasGushQuantityDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + GasGushQuantityDbConstNames.ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 将数据库中瓦斯涌出量（绝对涌出量）点数据存至文本文件
        /// </summary>
        /// <param name="strExportPointPath"></param>
        public static void gasGushQuantityPointToTxt(string strExportPointPath, int coalSeamID)
        {
            SaveDBXyz2Txt.WriteXYZ(strExportPointPath, GasGushQuantityDbConstNames.TABLE_NAME, GasGushQuantityDbConstNames.X, GasGushQuantityDbConstNames.Y, GasGushQuantityDbConstNames.ABSOLUTE_GAS_GUSH_QUANTITY, coalSeamID);
        }
    }
}
