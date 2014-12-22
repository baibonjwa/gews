// ******************************************************************
// 概  述：瓦斯含量点业务逻辑
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
using LibDatabase;
using LibEntity;
using System.Data;
using System.IO;

namespace LibBusiness
{
    public class GasContentBLL
    {
        /// <summary>
        /// 【瓦斯含量点】登录
        /// </summary>
        /// <param name="gasContentEntity">【瓦斯含量点】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertGasContentInfo(GasContent gasContentEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + GasContentDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + GasContentDbConstNames.X);
            sqlStr.Append(", " + GasContentDbConstNames.Y);
            sqlStr.Append(", " + GasContentDbConstNames.Z);
            sqlStr.Append(", " + GasContentDbConstNames.DEPTH);
            sqlStr.Append(", " + GasContentDbConstNames.GAS_CONTENT_VALUE);
            sqlStr.Append(", " + GasContentDbConstNames.MEASURE_DATE_TIME);
            sqlStr.Append(", " + GasContentDbConstNames.TUNNEL_ID);
            sqlStr.Append(", " + GasContentDbConstNames.COAL_SEAMS_ID);
            sqlStr.Append(", " + GasContentDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + gasContentEntity.CoordinateX + "'");
            sqlStr.Append(", '" + gasContentEntity.CoordinateY + "'");
            sqlStr.Append(", '" + gasContentEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + gasContentEntity.Depth + "'");
            sqlStr.Append(", '" + gasContentEntity.GasContentValue + "'");
            sqlStr.Append(", '" + gasContentEntity.MeasureDateTime.ToString("yyyy-MM-dd hh:mm:ss") + "'");
            sqlStr.Append(", '" + gasContentEntity.Tunnel + "'");
            sqlStr.Append(", '" + gasContentEntity.CoalSeams + "'");
            sqlStr.Append(", '" + gasContentEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 获取全部【瓦斯含量点】
        /// </summary>
        /// <returns>【瓦斯含量点】</returns>
        public static DataSet selectAllGasContentInfo()
        {
            string sqlStr = "SELECT * FROM " + GasContentDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取【瓦斯含量点】
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectGasContentInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + GasContentDbConstNames.ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + GasContentDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过主键，获取【瓦斯含量点】
        /// </summary>
        /// <returns>【瓦斯含量点】/returns>
        public static DataSet selectGasContentInfoByPK(int iPrimaryKey)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasContentDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + GasContentDbConstNames.ID + " = " + iPrimaryKey);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【瓦斯含量点】修改
        /// </summary>
        /// <param name="gasContentEntity">【瓦斯含量点】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateGasContentInfo(GasContent gasContentEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + GasContentDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + GasContentDbConstNames.X + " = '" + gasContentEntity.CoordinateX + "'");
            sqlStr.Append(", " + GasContentDbConstNames.Y + " = '" + gasContentEntity.CoordinateY + "'");
            sqlStr.Append(", " + GasContentDbConstNames.Z + " = '" + gasContentEntity.CoordinateZ + "'");
            sqlStr.Append(", " + GasContentDbConstNames.DEPTH + " = '" + gasContentEntity.Depth + "'");
            sqlStr.Append(", " + GasContentDbConstNames.GAS_CONTENT_VALUE + " = '" + gasContentEntity.GasContentValue + "'");
            sqlStr.Append(", " + GasContentDbConstNames.MEASURE_DATE_TIME + " = '" + gasContentEntity.MeasureDateTime + "'");
            sqlStr.Append(", " + GasContentDbConstNames.TUNNEL_ID + " = '" + gasContentEntity.Tunnel + "'");
            sqlStr.Append(", " + GasContentDbConstNames.COAL_SEAMS_ID + " = '" + gasContentEntity.CoalSeams + "'");
            sqlStr.Append(" WHERE " + GasContentDbConstNames.ID + " = '" + gasContentEntity.PrimaryKey + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【瓦斯含量点】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteGasContentInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + GasContentDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + GasContentDbConstNames.ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 将数据库中瓦斯含量点数据存至文本文件
        /// </summary>
        /// <param name="strExportPointPath">保存路径</param>
        public static void gasValuePointToTxt(string strExportPointPath, int coalSeamID)
        {
            SaveDBXyz2Txt.WriteXYZ(strExportPointPath, GasContentDbConstNames.TABLE_NAME, GasContentDbConstNames.X, GasContentDbConstNames.Y, GasContentDbConstNames.GAS_CONTENT_VALUE, coalSeamID);
        }
    }
}
