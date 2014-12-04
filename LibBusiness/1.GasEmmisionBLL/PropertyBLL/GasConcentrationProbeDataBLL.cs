// ******************************************************************
// 概  述：瓦斯浓度探头数据业务逻辑
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class GasConcentrationProbeDataBLL
    {
        /// <summary>
        /// 【瓦斯浓度探头数据】登录
        /// </summary>
        /// <param name="gasConcentrationProbeDataEntity">【瓦斯浓度探头数据】实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertGasConcentrationProbeData(GasConcentrationProbeData gasConcentrationProbeDataEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + GasConcentrationProbeDataDbConstNames.PROBE_ID);
            sqlStr.Append(", " + GasConcentrationProbeDataDbConstNames.PROBE_VALUE);
            sqlStr.Append(", " + GasConcentrationProbeDataDbConstNames.RECORD_TIME);
            sqlStr.Append(", " + GasConcentrationProbeDataDbConstNames.RECORD_TYPE);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + gasConcentrationProbeDataEntity.ProbeId + "'");
            sqlStr.Append(", '" + gasConcentrationProbeDataEntity.ProbeValue + "'");
            sqlStr.Append(", '" + gasConcentrationProbeDataEntity.RecordTime + "'");
            sqlStr.Append(", '" + gasConcentrationProbeDataEntity.RecordType + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
        // TODO:使用待定
        ///// <summary>
        ///// 获取全部【瓦斯浓度探头数据】
        ///// </summary>
        ///// <returns>全部【瓦斯浓度探头数据】</returns>
        //public static DataSet selectAllGasConcentrationProbeData()
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}45

        /// 根据探头编号和开始结束时间，获取特定探头和特定时间段内的【瓦斯浓度探头数据】
        /// </summary>
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <param name="dtEndTime">结束时间</param>
        /// <returns>特定探头和特定时间段内的【瓦斯浓度探头数据】</returns>
        public static DataSet selectAllGasConcentrationProbeDataByProbeIdAndTime(string strProbeId, DateTime dtStartTime, DateTime dtEndTime)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + dtEndTime + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// 根据探头编号和开始结束时间，获取特定探头和特定时间段内的【瓦斯浓度探头数据】
        /// </summary>
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <returns>特定探头和特定时间段内的【瓦斯浓度探头数据】</returns>
        public static DataSet selectAllGasConcentrationProbeDataByProbeIdAndStartTime(string strProbeId, DateTime dtStartTime)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        // TODO:使用待定
        ///// <summary>
        ///// 获取【瓦斯浓度探头数据】
        ///// </summary>
        ///// <returns>【瓦斯浓度探头数据】</returns>
        ///// <param name="iStartIndex">开始位</param>
        ///// <param name="iEndIndex">结束位</param>
        //public static DataSet selectGasConcentrationProbeDataForPage(int iStartIndex, int iEndIndex)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT * FROM (");
        //    sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + "," + GasConcentrationProbeDataDbConstNames.PROBE_ID+ ") AS rowid, *");
        //    sqlStr.Append(" FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME + ") AS TB");
        //    sqlStr.Append(" WHERE rowid >= " + iStartIndex);
        //    sqlStr.Append(" AND rowid <= " + iEndIndex);

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}

        /// <summary>
        /// 获取【瓦斯浓度探头数据】
        /// </summary>
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <param name="dtEndTime">结束时间</param>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns>【瓦斯浓度探头数据】</returns>
        public static DataSet selectGasConcentrationProbeDataForPageByProbeIdAndTime(string strProbeId, DateTime dtStartTime, DateTime dtEndTime, int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append("SELECT ROW_NUMBER() OVER (ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + ",A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + ") AS rowid,");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + ", ");
            sqlStr.Append("B." + ProbeManageDbConstNames.PROBE_ID + ", ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_VALUE + ", ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + ", ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TYPE + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_NAME + ", ");
            sqlStr.Append("B." + ProbeManageDbConstNames.PROBE_TYPE_ID + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_TYPE_DISPLAY_NAME + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_MEASURE_TYPE + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_USE_TYPE + ", ");
            sqlStr.Append("B." + ProbeManageDbConstNames.TUNNEL_ID + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_LOCATION_X + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_LOCATION_Y + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_LOCATION_Z + ", ");
            sqlStr.Append(ProbeManageDbConstNames.PROBE_DESCRIPTION + ", ");
            sqlStr.Append(ProbeManageDbConstNames.IS_MOVE + ", ");
            sqlStr.Append(ProbeManageDbConstNames.FAR_FROM_FRONTAL + ", ");
            sqlStr.Append(ProbeTypeDbConstNames.PROBE_TYPE_NAME + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.TUNNEL_NAME + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.SUPPORT_PATTERN + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.LITHOLOGY_ID + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.SECTION_TYPE + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.PARAM + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.DESIGNLENGTH + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.TUNNEL_TYPE + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.RULE_IDS + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.BINDINGID + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.PRE_WARNING_PARAMS + ", ");
            sqlStr.Append("E." + TunnelInfoDbConstNames.WORKINGFACE_ID + ", ");
            sqlStr.Append(TunnelInfoDbConstNames.COAL_OR_STONE + ", ");
            sqlStr.Append(WorkingFaceDbConstNames.WORKINGFACE_NAME + ", ");
            sqlStr.Append("F." + MiningAreaDbConstNames.MININGAREA_ID + ", ");
            sqlStr.Append(MiningAreaDbConstNames.MININGAREA_NAME + ", ");
            sqlStr.Append(HorizontalDbConstNames.HORIZONTAL_NAME + ", ");
            sqlStr.Append("H." + MineDbConstNames.MINE_ID + ", ");
            sqlStr.Append(MineDbConstNames.MINE_NAME);
            sqlStr.Append(" FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME + " AS A," + ProbeManageDbConstNames.TABLE_NAME + " AS B," + ProbeTypeDbConstNames.TABLE_NAME + " AS C," + TunnelInfoDbConstNames.TABLE_NAME + " AS D," + WorkingFaceDbConstNames.TABLE_NAME + " AS E," + MiningAreaDbConstNames.TABLE_NAME + " AS F," + HorizontalDbConstNames.TABLE_NAME + " AS G," + MineDbConstNames.TABLE_NAME + " AS H");
            sqlStr.Append(" WHERE ");
            sqlStr.Append(" A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + "B." + ProbeManageDbConstNames.PROBE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" B." + ProbeManageDbConstNames.PROBE_TYPE_ID + " = C." + ProbeTypeDbConstNames.PROBE_TYPE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" B." + ProbeManageDbConstNames.TUNNEL_ID + " = D." + TunnelInfoDbConstNames.ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" D." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = E." + WorkingFaceDbConstNames.WORKINGFACE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" E." + WorkingFaceDbConstNames.MININGAREA_ID + " = F." + MiningAreaDbConstNames.MININGAREA_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" F." + MiningAreaDbConstNames.HORIZONTAL_ID + " = G." + HorizontalDbConstNames.HORIZONTAL_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" G." + HorizontalDbConstNames.MINE_ID + " = H." + MineDbConstNames.MINE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + dtEndTime + "'");
            sqlStr.Append(" ) AS TB ");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【瓦斯浓度探头数据】删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteGasConcentrationProbeData(List<string[]> pkIdxsArrList)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < pkIdxsArrList.Count; i++)
            {
                string[] strArr = pkIdxsArrList[i];

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" DELETE FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE ");
                sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " = " + strArr[0]);
                sqlStr.Append(" AND ");
                sqlStr.Append(GasConcentrationProbeDataDbConstNames.PROBE_ID + " = '" + strArr[1] + "'");

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 获取指定探头的最新实时数据
        /// </summary>
        /// <param name="iProbeId"></param>
        /// <returns></returns>
        public static DataSet getNewRealData(string iProbeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME + " A ");
            sqlStr.Append("WHERE ");
            sqlStr.Append("A." + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " = ( ");
            sqlStr.Append("SELECT MAX(B." + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + ") ");
            sqlStr.Append("FROM ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.TABLE_NAME + " B ");
            sqlStr.Append("WHERE ");
            sqlStr.Append("B." + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = '" + iProbeId + "'");
            sqlStr.Append(" ) ");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 根据瓦斯浓度数据编号，获取全部瓦斯浓度数据
        /// </summary>
        /// <param name="iProbeDataId">瓦斯浓度数据编号</param>
        /// <returns>瓦斯浓度数据</returns>
        public static DataSet selectProbeDataByProbeDataId(int iProbeDataId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " = " + iProbeDataId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 【瓦斯浓度数据】修改
        /// </summary>
        /// <param name="gasConcentrationProbeDataEntity">【瓦斯浓度数据实体】</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateGasConcentrationProbeData(GasConcentrationProbeData gasConcentrationProbeDataEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + GasConcentrationProbeDataDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = '" + gasConcentrationProbeDataEntity.ProbeId + "'");
            sqlStr.Append(", " + GasConcentrationProbeDataDbConstNames.PROBE_VALUE + " = '" + gasConcentrationProbeDataEntity.ProbeValue + "'");
            sqlStr.Append(", " + GasConcentrationProbeDataDbConstNames.RECORD_TIME + " = '" + gasConcentrationProbeDataEntity.RecordTime + "'");
            sqlStr.Append(" WHERE " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + " = " + gasConcentrationProbeDataEntity.ProbeDataId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 计算坏数据
        /// </summary>
        /// <param name="strProbeId">探头编号</param>
        /// <param name="dtStartTime">开始时间</param>
        /// <param name="dtEndTime">结束时间</param>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns>【瓦斯浓度探头数据】</returns>
        public static DataSet selectGasConcentrationProbeDataForPageByProbeIdAndTimeAndBad(string strProbeId, DateTime dtStartTime, DateTime dtEndTime, double dBadDataThreshold)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT ROW_NUMBER() OVER (ORDER BY " + GasConcentrationProbeDataDbConstNames.PROBE_DATA_ID + ",A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + ") AS rowid,*");
            sqlStr.Append("FROM " + GasConcentrationProbeDataDbConstNames.TABLE_NAME + " AS A," + ProbeManageDbConstNames.TABLE_NAME + " AS B," + ProbeTypeDbConstNames.TABLE_NAME + " AS C," + TunnelInfoDbConstNames.TABLE_NAME + " AS D," + WorkingFaceDbConstNames.TABLE_NAME + " AS E," + MiningAreaDbConstNames.TABLE_NAME + " AS F," + HorizontalDbConstNames.TABLE_NAME + " AS G," + MineDbConstNames.TABLE_NAME + " AS H");
            sqlStr.Append(" WHERE ");
            sqlStr.Append(" A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + " = " + "B." + ProbeManageDbConstNames.PROBE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" B." + ProbeManageDbConstNames.PROBE_TYPE_ID + " = C." + ProbeTypeDbConstNames.PROBE_TYPE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" B." + ProbeManageDbConstNames.TUNNEL_ID + " = D." + TunnelInfoDbConstNames.ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" D." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = E." + WorkingFaceDbConstNames.WORKINGFACE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" E." + WorkingFaceDbConstNames.MININGAREA_ID + " = F." + MiningAreaDbConstNames.MININGAREA_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" F." + MiningAreaDbConstNames.HORIZONTAL_ID + " = G." + HorizontalDbConstNames.HORIZONTAL_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" G." + HorizontalDbConstNames.MINE_ID + " = H." + MineDbConstNames.MINE_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" A." + GasConcentrationProbeDataDbConstNames.PROBE_ID + " ='" + strProbeId + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " >= '" + dtStartTime + "'");
            sqlStr.Append(" AND ");
            sqlStr.Append(GasConcentrationProbeDataDbConstNames.RECORD_TIME + " <= '" + dtEndTime + "'");
            //sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            //sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            ds.Tables[0].Columns.Add("PreviouData");
            ds.Tables[0].Columns.Add("Difference", typeof(Double));
            for (int i = 10; i < ds.Tables[0].Rows.Count - 10; i++)
            {
                //ds.Tables[0].Rows[i]["PreviouData"] = 
                double previousData = 0;
                double followingData = 0;
                for (int j = 1; j < 10; j++)
                {
                    previousData += Convert.ToDouble(ds.Tables[0].Rows[i - j][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]);
                }
                for (int j = 1; j < 10; j++)
                {
                    followingData += Convert.ToDouble(ds.Tables[0].Rows[i + j][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]);
                }
                ds.Tables[0].Rows[i]["Difference"] = Math.Abs(((previousData / 10 + followingData / 10) / 2) -
                                                     Convert.ToDouble(ds.Tables[0].Rows[i]["PROBE_VALUE"]));

                //ds.Tables[0].Rows[i]["Difference"] = Math.Abs(Convert.ToDouble(ds.Tables[0].Rows[i - 1][GasConcentrationProbeDataDbConstNames.PROBE_VALUE]) - Convert.ToDouble(ds.Tables[0].Rows[i]["PROBE_VALUE"]));
            }
            ds.Tables[0].DefaultView.RowFilter = "Difference >= " + dBadDataThreshold;
            DataSet dsClone = new DataSet();
            dsClone.Tables.Add(ds.Tables[0].DefaultView.ToTable());

            return dsClone;
        }
    }
}
