// ******************************************************************
// 概  述：瓦斯压力点业务逻辑
// 作  者：伍鑫
// 创建日期：2013/12/07
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
    public class GasPressureBLL
    {
        /// <summary>
        /// 【瓦斯压力点】信息登录
        /// </summary>
        /// <param name="gasPressureEntity">瓦斯压力实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertGasPressureInfo(GasPressure gasPressureEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + GasPressureDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + GasPressureDbConstNames.X);
            sqlStr.Append(", " + GasPressureDbConstNames.Y);
            sqlStr.Append(", " + GasPressureDbConstNames.Z);
            sqlStr.Append(", " + GasPressureDbConstNames.DEPTH);
            sqlStr.Append(", " + GasPressureDbConstNames.GAS_PRESSURE_VALUE);
            sqlStr.Append(", " + GasPressureDbConstNames.MEASURE_DATE_TIME);
            sqlStr.Append(", " + GasPressureDbConstNames.TUNNEL_ID);
            sqlStr.Append(", " + GasPressureDbConstNames.COAL_SEAMS_ID);
            sqlStr.Append(", " + GasPressureDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + gasPressureEntity.CoordinateX + "'");
            sqlStr.Append(", '" + gasPressureEntity.CoordinateY + "'");
            sqlStr.Append(", '" + gasPressureEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + gasPressureEntity.Depth + "'");
            sqlStr.Append(", '" + gasPressureEntity.GasPressureValue + "'");
            sqlStr.Append(", '" + gasPressureEntity.MeasureDateTime + "'");
            sqlStr.Append(", '" + gasPressureEntity.Tunnel + "'");
            sqlStr.Append(", '" + gasPressureEntity.CoalSeams + "'");
            sqlStr.Append(", '" + gasPressureEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 获取全部【瓦斯压力点】
        /// </summary>
        /// <returns>【瓦斯压力点】</returns>
        public static DataSet selectAllGasPressureInfo()
        {
            string sqlStr = "SELECT * FROM " + GasPressureDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取【瓦斯压力点】
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectGasPressureInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + GasPressureDbConstNames.ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + GasPressureDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过主键，获取【瓦斯压力点】
        /// </summary>
        /// <returns>【瓦斯压力点】</returns>
        public static DataSet selectGasPressureInfoByPK(int iPrimaryKey)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasPressureDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + GasPressureDbConstNames.ID + " = " + iPrimaryKey);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【瓦斯压力点】修改
        /// </summary>
        /// <param name="gasPressureEntity">【瓦斯压力点】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateGasPressureInfo(GasPressure gasPressureEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + GasPressureDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + GasPressureDbConstNames.X + " = '" + gasPressureEntity.CoordinateX + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.Y + " = '" + gasPressureEntity.CoordinateY + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.Z + " = '" + gasPressureEntity.CoordinateZ + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.DEPTH + " = '" + gasPressureEntity.Depth + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.GAS_PRESSURE_VALUE + " = '" + gasPressureEntity.GasPressureValue + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.MEASURE_DATE_TIME + " = '" + gasPressureEntity.MeasureDateTime + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.TUNNEL_ID + " = '" + gasPressureEntity.Tunnel + "'");
            sqlStr.Append(", " + GasPressureDbConstNames.COAL_SEAMS_ID + " = '" + gasPressureEntity.CoalSeams + "'");
            sqlStr.Append(" WHERE " + GasPressureDbConstNames.ID + " = " + gasPressureEntity.PrimaryKey);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 【瓦斯压力点】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteGasPressureInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + GasPressureDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + GasPressureDbConstNames.ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 将数据库中瓦斯压力点数据存至文本文件
        /// </summary>
        public static void gasPressurePointToTxt(string strExportPointPath, int coalSeamID)
        {
            SaveDBXyz2Txt.WriteXYZ(strExportPointPath, GasPressureDbConstNames.TABLE_NAME, GasPressureDbConstNames.X, GasPressureDbConstNames.Y, GasPressureDbConstNames.GAS_PRESSURE_VALUE, coalSeamID);
        }
    }
}
