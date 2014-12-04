// ******************************************************************
// 概  述：钻孔岩性业务逻辑
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
    public class BoreholeLithologyBLL
    {
        /// <summary>
        /// 钻孔岩性信息登录
        /// </summary>
        /// <param name="boreholeLithologyEntity">钻孔岩性实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertBoreholeLithologyInfo(BoreholeLithology boreholeLithologyEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + BoreholeLithologyDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + BoreholeLithologyDbConstNames.BOREHOLE_ID);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.LITHOLOGY_ID);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.FLOOR_ELEVATION);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.THICKNESS);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.COAL_SEAMS_NAME);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.COORDINATE_X);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.COORDINATE_Y);
            sqlStr.Append(", " + BoreholeLithologyDbConstNames.COORDINATE_Z);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + boreholeLithologyEntity.BoreholeId + "'");
            sqlStr.Append(", '" + boreholeLithologyEntity.LithologyId + "'");
            sqlStr.Append(",  " + boreholeLithologyEntity.FloorElevation + "");
            sqlStr.Append(",  " + boreholeLithologyEntity.Thickness + "");
            sqlStr.Append(", '" + boreholeLithologyEntity.CoalSeamsName + "'");
            sqlStr.Append(",  " + boreholeLithologyEntity.CoordinateX + "");
            sqlStr.Append(",  " + boreholeLithologyEntity.CoordinateY + "");
            sqlStr.Append(",  " + boreholeLithologyEntity.CoordinateZ + "");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 钻孔岩性信息查询
        /// </summary>
        /// <param name="boreholeId">钻孔编号</param>
        /// <returns>钻孔岩性信息</returns>
        public static DataSet selectBoreholeLithologyInfoByBoreholeId(int iBoreholeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM");
            sqlStr.Append("  " + BoreholeLithologyDbConstNames.TABLE_NAME + " TB");
            sqlStr.Append(", " + LithologyDbConstNames.TABLE_NAME + " TL");
            sqlStr.Append(" WHERE ");
            sqlStr.Append(" TB." + BoreholeLithologyDbConstNames.LITHOLOGY_ID);
            sqlStr.Append(" = ");
            sqlStr.Append(" TL." + LithologyDbConstNames.LITHOLOGY_ID);
            sqlStr.Append(" AND ");
            sqlStr.Append(" TB." + BoreholeLithologyDbConstNames.BOREHOLE_ID);
            sqlStr.Append(" = ");
            sqlStr.Append(iBoreholeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 钻孔岩性信息删除
        /// </summary>
        /// <param name="boreholeId">钻孔编号</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteBoreholeLithologyInfoByBoreholeId(int iBoreholeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + BoreholeLithologyDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + BoreholeLithologyDbConstNames.BOREHOLE_ID + " = " + iBoreholeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
