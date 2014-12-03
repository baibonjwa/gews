// ******************************************************************
// 概  述：钻孔业务逻辑
// 作  者：伍鑫
// 创建日期：2013/11/26
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
    public class BoreholeBLL
    {
        /// <summary>
        /// 获取最大钻孔编号
        /// </summary>
        /// <returns>最大钻孔编号</returns>
        public static int getMaxBoreholeId()
        {
            string sqlStr = "SELECT MAX(" + BoreholeDBConstNames.BOREHOLE_ID + ") FROM " + BoreholeDBConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds.Tables[0].Rows[0][0].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// 钻孔信息登录
        /// </summary>
        /// <param name="boreholeEntity">钻孔实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertBoreholeInfo(BoreholeEntity boreholeEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" (" + BoreholeDBConstNames.BOREHOLE_ID);
            sqlStr.Append(", " + BoreholeDBConstNames.BOREHOLE_NUMBER);
            sqlStr.Append(", " + BoreholeDBConstNames.GROUND_ELEVATION);
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_X);
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_Y);
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_Z);
            sqlStr.Append(", " + BoreholeDBConstNames.COAL_SEAMS_TEXTURE);
            sqlStr.Append(", " + BoreholeDBConstNames.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + boreholeEntity.BoreholeId + "'");
            sqlStr.Append(", '" + boreholeEntity.BoreholeNumber + "'");
            sqlStr.Append(", '" + boreholeEntity.GroundElevation + "'");
            sqlStr.Append(", '" + boreholeEntity.CoordinateX + "'");
            sqlStr.Append(", '" + boreholeEntity.CoordinateY + "'");
            sqlStr.Append(", '" + boreholeEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + boreholeEntity.CoalSeamsTexture + "'");
            sqlStr.Append(", '" + boreholeEntity.BindingId + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 判断孔号是否存在
        /// </summary>
        /// <param name="strBoreholeNumber">孔号</param>
        /// <returns>成功与否：true，false</returns>
        public static bool isBoreholeNumberExist(string strBoreholeNumber)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT COUNT(*) FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_NUMBER + " = '" + strBoreholeNumber + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return int.Parse(ds.Tables[0].Rows[0][0].ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 获取全部钻孔信息
        /// </summary>
        /// <returns>钻孔信息</returns>
        public static DataSet selectAllBoreholeInfo()
        {
            string sqlStr = "SELECT * FROM " + BoreholeDBConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        ///  获取钻孔信息
        /// </summary>
        /// <param name="iStartIndex">开始位</param>
        /// <param name="iEndIndex">结束位</param>
        /// <returns></returns>
        public static DataSet selectBoreholeInfoForPage(int iStartIndex, int iEndIndex)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM (");
            sqlStr.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + BoreholeDBConstNames.BOREHOLE_ID + ") AS rowid, *");
            sqlStr.Append(" FROM " + BoreholeDBConstNames.TABLE_NAME + ") AS TB");
            sqlStr.Append(" WHERE rowid >= " + iStartIndex);
            sqlStr.Append(" AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过孔号获取钻孔编号
        /// </summary>
        /// <param name="number"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static bool selectBoreholeIdByBoreholeNum(string number, out int ret)
        {
            ret = -1;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + BoreholeDBConstNames.BOREHOLE_ID + " FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_NUMBER + " = '" + number + "'");

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
        /// 20140426 lyf 
        /// 通过孔号获取钻孔绑定ID（BID）        
        /// </summary>
        /// <param name="number"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static bool selectBoreholeBIDByBoreholeNum(string number, out string ret)
        {
            ret = "";
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + BoreholeDBConstNames.BID + " FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_NUMBER + " = '" + number + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            try
            {
                ret = ds.Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 通过钻孔编号，获取钻孔信息
        /// </summary>
        /// <returns>钻孔信息</returns>
        public static DataSet selectBoreholeInfoByBoreholeId(int iBoreholeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_ID + " = " + iBoreholeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 20140507 lyf 
        /// 通过钻孔孔号（名称），获取钻孔信息
        /// </summary>
        /// <param name="sBoreholeName">钻孔孔号（名称）</param>
        /// <returns></returns>
        public static DataSet selectBoreholeInfoByBoreholeName(string sBoreholeName)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_NUMBER + " = '" + sBoreholeName + "'");   //zwy modify 20140527

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 20140507 lyf 
        /// 根据一行数据获取钻孔实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static BoreholeEntity GetBoreholeEntity(DataRow dr)
        {
            BoreholeEntity ret = null;
            ret = new BoreholeEntity();

            //钻孔编号
            ret.BoreholeId = int.Parse(dr[BoreholeDBConstNames.BOREHOLE_ID].ToString());

            //孔号(名称)
            ret.BoreholeNumber = dr[BoreholeDBConstNames.BOREHOLE_NUMBER].ToString();

            if (dr[BoreholeDBConstNames.GROUND_ELEVATION] != null)
            {
                //地面标高
                double dEle = 0.0;
                double.TryParse(dr[BoreholeDBConstNames.GROUND_ELEVATION].ToString(), out dEle);
                ret.GroundElevation = dEle;
                //ret.GroundElevation = Convert.ToDouble(dr[BoreholeDBConstNames.GROUND_ELEVATION].ToString());
            }

            //煤层结构
            if ( dr[BoreholeDBConstNames.COAL_SEAMS_TEXTURE] != null)
            {
                ret.CoalSeamsTexture = dr[BoreholeDBConstNames.COAL_SEAMS_TEXTURE].ToString();
            }

            //X坐标
            if (dr[BoreholeDBConstNames.COORDINATE_X] != null)
            {
                double dx = 0;
                double.TryParse(dr[BoreholeDBConstNames.COORDINATE_X].ToString(), out dx);
                ret.CoordinateX = dx;
            }

            //Y坐标
            if (dr[BoreholeDBConstNames.COORDINATE_Y] != null)
            {
                double dy = 0;
                double.TryParse(dr[BoreholeDBConstNames.COORDINATE_Y].ToString(), out dy);
                ret.CoordinateY = dy;
            }

            //Z坐标
            if (dr[BoreholeDBConstNames.COORDINATE_Z] != null)
            {
                double dz = 0;
                double.TryParse(dr[BoreholeDBConstNames.COORDINATE_Z].ToString(), out dz);
                ret.CoordinateZ = dz;
            }
            //ret.CoordinateZ = Convert.ToDouble(dr[BoreholeDBConstNames.COORDINATE_Z].ToString());

            //BID
            ret.BindingId = dr[BoreholeDBConstNames.BID].ToString();

            return ret;
        }

        /// <summary>
        /// 钻孔信息修改
        /// </summary>
        /// <param name="boreholeEntity">钻孔实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateBoreholeInfo(BoreholeEntity boreholeEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + BoreholeDBConstNames.BOREHOLE_NUMBER + " = '" + boreholeEntity.BoreholeNumber + "'");
            sqlStr.Append(", " + BoreholeDBConstNames.GROUND_ELEVATION + " = '" + boreholeEntity.GroundElevation + "'");
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_X + " = '" + boreholeEntity.CoordinateX + "'");
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_Y + " = '" + boreholeEntity.CoordinateY + "'");
            sqlStr.Append(", " + BoreholeDBConstNames.COORDINATE_Z + " = '" + boreholeEntity.CoordinateZ + "'");
            sqlStr.Append(", " + BoreholeDBConstNames.COAL_SEAMS_TEXTURE + " = '" + boreholeEntity.CoalSeamsTexture + "'");
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_ID + " = " + boreholeEntity.BoreholeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 通过钻孔编号，删除钻孔信息
        /// </summary>
        /// <param name="BoreholeId">钻孔编号</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteBoreholeInfoByBoreholeId(int iBoreholeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + BoreholeDBConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_ID + " = " + iBoreholeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 钻孔信息删除
        /// </summary>
        /// <param name="iPkIdxsArr">删除数据主键数组</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteBoreholeInfo(int[] iPkIdxsArr)
        {
            bool bResult = true;
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            // 批量删除
            for (int i = 0; i < iPkIdxsArr.Length; i++)
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("DELETE FROM " + BoreholeDBConstNames.TABLE_NAME);
                sqlStr.Append(" WHERE " + BoreholeDBConstNames.BOREHOLE_ID + " = " + iPkIdxsArr[i]);

                bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
                bResult = BoreholeLithologyBLL.deleteBoreholeLithologyInfoByBoreholeId(iPkIdxsArr[i]);
            }
            db.Close();

            return bResult;
        }
    }
}
