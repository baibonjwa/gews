// ******************************************************************
// 概  述：大断层业务逻辑
// 作  者：伍鑫
// 创建日期：2013/11/30
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class BigFaultageBLL
    {
        /// <summary>
        /// 获取全部大断层信息
        /// </summary>
        /// <returns>全部大断层信息</returns>
        public static DataSet selectAllBigFaultageInfo()
        {
            string sqlStr = "SELECT * FROM " + BigFaultageDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取大断层信息
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectBigFaultageInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + BigFaultageDbConstNames.FAULTAGE_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + BigFaultageDbConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 大断层信息登录
        /// </summary>
        /// <param name="bigFaultageEntity">断层实体</param>
        /// <returns>成功与否：true，false</returns>
        //public static bool insertBigFaultageInfo(BigFaultageEntity bigFaultageEntity)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("INSERT INTO " + BigFaultageDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" (" + BigFaultageDbConstNames.FAULTAGE_NAME);
        //    sqlStr.Append(", " + BigFaultageDbConstNames.TYPE);
        //    sqlStr.Append(", " + BigFaultageDbConstNames.EXPOSE_POINTS);
        //    sqlStr.Append(", " + BigFaultageDbConstNames.BID);
        //    sqlStr.Append(" )");
        //    sqlStr.Append(" VALUES (");
        //    sqlStr.Append("  '" + bigFaultageEntity.FaultageName + "'");
        //    sqlStr.Append(", '" + bigFaultageEntity.Type + "'");
        //    sqlStr.Append(", '" + bigFaultageEntity.ExposePoints + "'");
        //    sqlStr.Append(", '" + bigFaultageEntity.BindingId + "'");
        //    sqlStr.Append(" )");

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    bool bResult = db.OperateDB(sqlStr.ToString());
        //    return bResult;
        //}
        public static bool insertBigFaultageInfo(BigFaultageEntity bigFaultageEntity, List<BigFaultagePointEntity> pointList)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("BEGIN ");
            sqlStr.Append("declare @ReturnID int ");

            sqlStr.Append("INSERT INTO " + BigFaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + BigFaultageDbConstNames.FAULTAGE_NAME);
            sqlStr.Append(", " + BigFaultageDbConstNames.TYPE);
            sqlStr.Append(", " + BigFaultageDbConstNames.GAP);
            sqlStr.Append(", " + BigFaultageDbConstNames.ANGLE);
            sqlStr.Append(", " + BigFaultageDbConstNames.TREND);
            sqlStr.Append(", " + BigFaultageDbConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + bigFaultageEntity.FaultageName + "'");
            sqlStr.Append(", '" + bigFaultageEntity.Type + "'");
            sqlStr.Append(", '" + bigFaultageEntity.Gap + "'");
            sqlStr.Append(", '" + bigFaultageEntity.Angle + "'");
            sqlStr.Append(", '" + bigFaultageEntity.Trend + "'");
            sqlStr.Append(", '" + bigFaultageEntity.BindingId + "'");
            sqlStr.Append(" );SET @ReturnID = @@IDENTITY;");

            foreach (var i in pointList)
            {
                sqlStr.Append("INSERT INTO " + BigFaultagePointDbConstNames.TABLE_NAME);
                sqlStr.Append(" (" + BigFaultagePointDbConstNames.UPORDOWN);
                sqlStr.Append(", " + BigFaultagePointDbConstNames.COORDINATE_X);
                sqlStr.Append(", " + BigFaultagePointDbConstNames.COORDINATE_Y);
                sqlStr.Append(", " + BigFaultagePointDbConstNames.COORDINATE_Z);
                sqlStr.Append(", " + BigFaultagePointDbConstNames.BIG_FAULTAGE_ID);
                sqlStr.Append(", " + BigFaultagePointDbConstNames.BINDINGID);
                sqlStr.Append(" )");
                sqlStr.Append(" VALUES (");
                sqlStr.Append("  '" + i.UpOrDown + "'");
                sqlStr.Append(", '" + i.CoordinateX + "'");
                sqlStr.Append(", '" + i.CoordinateY + "'");
                sqlStr.Append(", '" + i.CoordinateZ + "'");
                sqlStr.Append(", @ReturnID");
                sqlStr.Append(", '" + i.Bid + "'");
                sqlStr.Append(" )");
            }
            sqlStr.Append(" END");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());

            return bResult;
        }


        /// <summary>
        /// 判断断层名称是否存在
        /// </summary>
        /// <param name="strFaultageName">断层名称</param>
        /// <returns>成功与否：true，false</returns>
        public static bool isFaultageNameExist(string strFaultageName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + BigFaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_NAME + " = '" + strFaultageName + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 大断层信息删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteBigFaultageInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("BEGIN ");
                sqlStr.Append("DELETE FROM " + BigFaultagePointDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + BigFaultagePointDbConstNames.BIG_FAULTAGE_ID + " = " + iPkIdxsArr[i] + ";");
                sqlStr.Append("DELETE FROM " + BigFaultageDbConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = " + iPkIdxsArr[i]);
                sqlStr.Append(" END ");
                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            }
            db.Close();

            return bResult;
        }

        /// <summary>
        /// 通过<推断断层>编号，获取<推断断层>信息
        /// </summary>
        /// <param name="iBigFaultageId"><推断断层>编号</param>
        /// <returns><推断断层>信息</returns>
        public static DataSet selectBigFaultageInfoByBigFaultageId(int iBigFaultageId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + BigFaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = " + iBigFaultageId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<推断断层>名称获取<推断断层>编号
        /// </summary>
        /// <param name="name"><推断断层>名称</param>
        /// <param name="ret"><推断断层>编号</param>
        /// <returns></returns>
        public static bool selectBigFaultageIdByBigFaultageName(string name, out int ret)
        {
            ret = -1;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + BigFaultageDbConstNames.FAULTAGE_ID + " FROM " + BigFaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_NAME + " = '" + name + "'");

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

        public static void selectBigfaultageId(string id, BigFaultageEntity bigFaultageEntity, List<BigFaultagePointEntity> pointList)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + BigFaultageDbConstNames.TABLE_NAME + " WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = " + id);

            DataTable dtFaultage = db.ReturnDS(sqlStr.ToString()).Tables[0];

            if (dtFaultage.Rows.Count > 0)
            {
                bigFaultageEntity.FaultageId = Convert.ToInt32(dtFaultage.Rows[0][BigFaultageDbConstNames.FAULTAGE_ID]);
                bigFaultageEntity.FaultageName = dtFaultage.Rows[0][BigFaultageDbConstNames.FAULTAGE_NAME].ToString();
                bigFaultageEntity.Gap = dtFaultage.Rows[0][BigFaultageDbConstNames.GAP].ToString();
                bigFaultageEntity.Trend = dtFaultage.Rows[0][BigFaultageDbConstNames.TREND].ToString();
                bigFaultageEntity.Type = dtFaultage.Rows[0][BigFaultageDbConstNames.TYPE].ToString();
                bigFaultageEntity.Angle = dtFaultage.Rows[0][BigFaultageDbConstNames.ANGLE].ToString();
                bigFaultageEntity.BindingId = dtFaultage.Rows[0][BigFaultageDbConstNames.BID].ToString();
            }

            StringBuilder sqlStr2 = new StringBuilder();
            sqlStr2.Append("SELECT * FROM " + BigFaultagePointDbConstNames.TABLE_NAME + " WHERE " + BigFaultagePointDbConstNames.BIG_FAULTAGE_ID + " = " + id);

            DataTable dtFaultagePoint = db.ReturnDS(sqlStr2.ToString()).Tables[0];

            for (int i = 0; i < dtFaultagePoint.Rows.Count; i++)
            {
                BigFaultagePointEntity point = new BigFaultagePointEntity();
                point.CoordinateX = Convert.ToDouble(dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.COORDINATE_X]);
                point.CoordinateY = Convert.ToDouble(dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.COORDINATE_Y]);
                point.CoordinateZ = Convert.ToDouble(dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.COORDINATE_Z]);
                point.Bid = dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.COORDINATE_Z].ToString();
                point.UpOrDown = dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.UPORDOWN].ToString();
                point.Id = Convert.ToInt32(dtFaultagePoint.Rows[i][BigFaultagePointDbConstNames.ID]);

                pointList.Add(point);
            }


            //sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_NAME + " = '" + name + "'");
        }


        /// <summary>
        /// 20140506 lyf
        /// 通过<推断断层>编号获取<推断断层>绑定ID
        /// </summary>
        /// <param name="id"><推断断层>编号</param>
        /// <param name="bid"><推断断层>绑定ID</param>
        /// <returns></returns>
        //public static bool selectBigFaultageBIDByBigFaultageID(int id, out string bid)
        //{
        //    bid = "";
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT " + BigFaultageDbConstNames.BID + " FROM " + BigFaultageDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = '" + id + "'");

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    try
        //    {
        //        bid = ds.Tables[0].Rows[0][0].ToString();
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        /// <summary>
        /// <推断断层>信息修改
        /// </summary>
        /// <param name="bigFaultageEntity"><推断断层>实体</param>
        /// <returns>成功与否：true，false</returns>
        //public static bool updateBigFaultageInfo(BigFaultageEntity bigFaultageEntity)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("UPDATE " + BigFaultageDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" SET");
        //    sqlStr.Append("  " + BigFaultageDbConstNames.FAULTAGE_NAME + " = '" + bigFaultageEntity.FaultageName + "'");
        //    sqlStr.Append(", " + BigFaultageDbConstNames.TYPE + " = '" + bigFaultageEntity.Type + "'");
        //    sqlStr.Append(", " + BigFaultageDbConstNames.EXPOSE_POINTS + " = '" + bigFaultageEntity.ExposePoints + "'");
        //    sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = " + bigFaultageEntity.FaultageId);

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    bool bResult = db.OperateDB(sqlStr.ToString());
        //    return bResult;
        //}
        public static bool updateBigFaultageInfo(BigFaultageEntity bigFaultageEntity, List<BigFaultagePointEntity> pointList)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("BEGIN ");
            sqlStr.Append("UPDATE " + BigFaultageDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + BigFaultageDbConstNames.FAULTAGE_NAME + " = '" + bigFaultageEntity.FaultageName + "'");
            sqlStr.Append(", " + BigFaultageDbConstNames.TYPE + " = '" + bigFaultageEntity.Type + "'");
            sqlStr.Append(", " + BigFaultageDbConstNames.GAP + " = '" + bigFaultageEntity.Gap + "'");
            sqlStr.Append(", " + BigFaultageDbConstNames.ANGLE + " = '" + bigFaultageEntity.Angle + "'");
            sqlStr.Append(", " + BigFaultageDbConstNames.TREND + " = '" + bigFaultageEntity.Trend + "'");
            sqlStr.Append(", " + BigFaultageDbConstNames.BID + " = '" + bigFaultageEntity.BindingId + "'");
            sqlStr.Append(" WHERE " + BigFaultageDbConstNames.FAULTAGE_ID + " = " + bigFaultageEntity.FaultageId);

            foreach (var i in pointList)
            {
                sqlStr.Append("UPDATE " + BigFaultagePointDbConstNames.TABLE_NAME);
                sqlStr.Append(" SET");
                sqlStr.Append("  " + BigFaultagePointDbConstNames.COORDINATE_X + " = '" + i.CoordinateX + "',");
                sqlStr.Append("  " + BigFaultagePointDbConstNames.COORDINATE_Y + " = '" + i.CoordinateY + "',");
                sqlStr.Append("  " + BigFaultagePointDbConstNames.COORDINATE_Z + " = '" + i.CoordinateZ + "',");
                sqlStr.Append("  " + BigFaultagePointDbConstNames.UPORDOWN + " = '" + i.UpOrDown + "'");
                sqlStr.Append(" WHERE " + BigFaultagePointDbConstNames.ID + " = " + i.Id);
            }

            sqlStr.Append(" END ");


            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

    }
}
