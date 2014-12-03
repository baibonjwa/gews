// ******************************************************************
// 概  述：S值业务逻辑
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class SValueBLL
    {
        /// <summary>
        /// 查询所有S值
        /// </summary>
        /// <returns></returns>
        public static DataSet selectValueS()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + SValueDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 获取最大分组ID
        /// </summary>
        /// <returns></returns>
        public static int selectMaxGroupID()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT DISTINCT(MAX(" + SValueDbConstNames.VALUE_S_ID + ")) FROM " + SValueDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            int count = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
            }
            return count;
        }
        /// <summary>
        /// 返回某S值
        /// </summary>
        /// <param name="SValueEntityID">S值ID</param>
        /// <returns>S值实体</returns>
        public static SValueEntity selectValueSByID(int ID)
        {
            SValueEntity sValueEntity = new SValueEntity();
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + SValueDbConstNames.TABLE_NAME + " WHERE " + SValueDbConstNames.ID + " = " + ID;
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    sValueEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[0][SValueDbConstNames.ID]);
                    sValueEntity.SValueID = Convert.ToInt32(ds.Tables[0].Rows[0][SValueDbConstNames.VALUE_S_ID]);
                    sValueEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.COORDINATE_X]);
                    sValueEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.COORDINATE_Y]);
                    sValueEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.COORDINATE_Z]);
                    sValueEntity.ValueSg = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.VALUE_SG]);
                    sValueEntity.ValueSv = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.VALUE_SV]);
                    sValueEntity.Valueq = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.VALUE_Q]);
                    sValueEntity.BoreholeDeep = Convert.ToDouble(ds.Tables[0].Rows[0][SValueDbConstNames.BOREHOLE_DEEP]);
                    sValueEntity.Time = Convert.ToDateTime(ds.Tables[0].Rows[0][SValueDbConstNames.TIME].ToString());
                    sValueEntity.TypeInTime = Convert.ToDateTime(ds.Tables[0].Rows[0][SValueDbConstNames.TYPE_IN_TIME].ToString());
                    sValueEntity.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[0][SValueDbConstNames.TUNNEL_ID]);

                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return sValueEntity;

        }

        /// <summary>
        /// 返回某组s值
        /// </summary>
        /// <param name="k1ValueID">K1分组ID</param>
        /// <returns>K1实体</returns>
        public static SValueEntity[] selectValueSBySValueID(int SValueID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + SValueDbConstNames.TABLE_NAME + " WHERE " + SValueDbConstNames.VALUE_S_ID + " = " + SValueID;
            DataSet ds = db.ReturnDS(sql);
            SValueEntity[] sEntity = new SValueEntity[ds.Tables[0].Rows.Count];
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        SValueEntity sValueEntity = new SValueEntity();
                        sValueEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[i][SValueDbConstNames.ID]);
                        sValueEntity.SValueID = Convert.ToInt32(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_S_ID]);
                        sValueEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_X]);
                        sValueEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_Y]);
                        sValueEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.COORDINATE_Z]);
                        sValueEntity.ValueSg = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_SG]);
                        sValueEntity.ValueSv = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_SV]);
                        sValueEntity.Valueq = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.VALUE_Q]);
                        sValueEntity.BoreholeDeep = Convert.ToDouble(ds.Tables[0].Rows[i][SValueDbConstNames.BOREHOLE_DEEP]);
                        sValueEntity.Time = Convert.ToDateTime(ds.Tables[0].Rows[i][SValueDbConstNames.TIME].ToString());
                        sValueEntity.TypeInTime = Convert.ToDateTime(ds.Tables[0].Rows[i][SValueDbConstNames.TYPE_IN_TIME].ToString());
                        sValueEntity.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[i][SValueDbConstNames.TUNNEL_ID]);
                        sEntity[i] = sValueEntity;

                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
            return sEntity;
        }

        /// <summary>
        /// 分页用查询S值
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectValueSEntity(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + SValueDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + SValueDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
        /// <summary>
        /// 添加ValueS
        /// </summary>
        /// <param name="sValueEntity"></param>
        /// <returns></returns>
        public static bool insertValueS(SValueEntity sValueEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + SValueDbConstNames.TABLE_NAME + " (");
            sb.Append(SValueDbConstNames.VALUE_S_ID + ",");
            sb.Append(SValueDbConstNames.COORDINATE_X + ",");
            sb.Append(SValueDbConstNames.COORDINATE_Y + ",");
            sb.Append(SValueDbConstNames.COORDINATE_Z + ",");
            sb.Append(SValueDbConstNames.VALUE_SG + ",");
            sb.Append(SValueDbConstNames.VALUE_SV + ",");
            sb.Append(SValueDbConstNames.VALUE_Q + ",");
            sb.Append(SValueDbConstNames.BOREHOLE_DEEP + ",");
            sb.Append(SValueDbConstNames.TIME + ",");
            sb.Append(SValueDbConstNames.TYPE_IN_TIME + ",");
            sb.Append(SValueDbConstNames.TUNNEL_ID + ") VALUES (");
            sb.Append(sValueEntity.SValueID + ",");
            sb.Append(sValueEntity.CoordinateX + ",");
            sb.Append(sValueEntity.CoordinateY + ",");
            sb.Append(sValueEntity.CoordinateZ + ",");
            sb.Append(sValueEntity.ValueSg + ",");
            sb.Append(sValueEntity.ValueSv + ",");
            sb.Append(sValueEntity.Valueq + ",");
            sb.Append(sValueEntity.BoreholeDeep + ",'");
            sb.Append(sValueEntity.Time + "','");
            sb.Append(sValueEntity.TypeInTime + "',");
            sb.Append(sValueEntity.TunnelID + ")");

            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }
        /// <summary>
        /// 修改ValueS
        /// </summary>
        /// <param name="sValueEntity"></param>
        /// <returns></returns>
        public static bool updateValueS(SValueEntity sValueEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + SValueDbConstNames.TABLE_NAME + " SET ");
            sb.Append(SValueDbConstNames.COORDINATE_X + " = " + sValueEntity.CoordinateX + ",");
            sb.Append(SValueDbConstNames.COORDINATE_Y + " = " + sValueEntity.CoordinateY + ",");
            sb.Append(SValueDbConstNames.COORDINATE_Z + " = " + sValueEntity.CoordinateZ + ",");
            sb.Append(SValueDbConstNames.VALUE_SG + " = " + sValueEntity.ValueSg + ",");
            sb.Append(SValueDbConstNames.VALUE_SV + " = " + sValueEntity.ValueSv + ",");
            sb.Append(SValueDbConstNames.VALUE_Q + " = " + sValueEntity.Valueq + ",");
            sb.Append(SValueDbConstNames.BOREHOLE_DEEP + " = " + sValueEntity.BoreholeDeep + ",");
            sb.Append(SValueDbConstNames.TIME + " = '" + sValueEntity.Time + "',");
            sb.Append(SValueDbConstNames.TYPE_IN_TIME + " = '" + sValueEntity.TypeInTime + "',");
            sb.Append(SValueDbConstNames.TUNNEL_ID + " = " + sValueEntity.TunnelID + " WHERE ");
            sb.Append(SValueDbConstNames.ID + " = " + sValueEntity.ID);

            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }
        /// <summary>
        /// 删除ValueS
        /// </summary>
        /// <param name="sValueEntity"></param>
        /// <returns></returns>
        public static bool deleteValueS(SValueEntity sValueEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "DELETE FROM " + SValueDbConstNames.TABLE_NAME + " WHERE " + SValueDbConstNames.ID + " = " + sValueEntity.ID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}