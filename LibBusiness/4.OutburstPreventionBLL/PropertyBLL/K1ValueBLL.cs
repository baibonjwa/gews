// ******************************************************************
// 概  述：K1值业务逻辑
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
    public class K1ValueBLL
    {
        /// <summary>
        /// 查询所有K1值
        /// </summary>
        /// <returns></returns>
        public static DataSet selectValueK1()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT DISTINCT " + K1ValueDbConstNames.VALUE_K1_ID + " FROM " + K1ValueDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        public static DataSet selectValueK12(int tunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            db.Open();
            sb.Append("SELECT * FROM");
            sb.Append(" (SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid,* FROM ");
            sb.Append(" (SELECT ROW_NUMBER() OVER(PARTITION BY " + K1ValueDbConstNames.VALUE_K1_ID + " ORDER BY " + K1ValueDbConstNames.ID + ") AS V, * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.ID + " IN");
            sb.Append(" (SELECT MAX(" + K1ValueDbConstNames.ID + ") FROM " + K1ValueDbConstNames.TABLE_NAME + " A,(SELECT  MAX((" + K1ValueDbConstNames.VALUE_K1_DRY + "+" + K1ValueDbConstNames.VALUE_K1_WET + " + ABS(" + K1ValueDbConstNames.VALUE_K1_DRY + "-" + K1ValueDbConstNames.VALUE_K1_WET + "))/2) AS " + K1ValueDbConstNames.VALUE_K1_DRY + "," + K1ValueDbConstNames.VALUE_K1_ID + " FROM " + K1ValueDbConstNames.TABLE_NAME + " GROUP BY " + K1ValueDbConstNames.VALUE_K1_ID + ") B ");
            //此处的K1ValueDbConstNames.Value_K1_DRY并非是表中的字段名，而是自定名称的字段
            sb.Append(" WHERE A." + K1ValueDbConstNames.VALUE_K1_ID + " = B." + K1ValueDbConstNames.VALUE_K1_ID + " AND (A." + K1ValueDbConstNames.VALUE_K1_DRY + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + " OR A." + K1ValueDbConstNames.VALUE_K1_WET + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + ") GROUP BY A." + K1ValueDbConstNames.VALUE_K1_DRY + ",A." + K1ValueDbConstNames.VALUE_K1_ID + ")) AS TC");
            sb.Append(" WHERE V <= 1) AS TB WHERE ");
            sb.Append(K1ValueDbConstNames.TUNNEL_ID + " = " + tunnelId);
            sb.Append(" AND " + K1ValueDbConstNames.TIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");

            DataSet ds = db.ReturnDSNotOpenAndClose(sb.ToString());
            return ds;
        }
        /// <summary>
        /// 返回某K1值
        /// </summary>
        /// <param name="ID">K1主键ID</param>
        /// <returns>K1实体</returns>
        public static K1ValueEntity selectValueK1ByID(int ID)
        {
            K1ValueEntity k1ValueEntity = new K1ValueEntity();
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.ID + " = " + ID;
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    k1ValueEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.ID]);
                    k1ValueEntity.K1ValueID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_ID]);
                    k1ValueEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_X]);
                    k1ValueEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_Y]);
                    k1ValueEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_Z]);
                    k1ValueEntity.ValueK1Dry = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_DRY]);
                    k1ValueEntity.ValueK1Wet = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_WET]);
                    k1ValueEntity.Sg = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_SG]);
                    k1ValueEntity.Sv = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_SV]);
                    k1ValueEntity.Q = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_Q]);
                    k1ValueEntity.BoreholeDeep = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.BOREHOLE_DEEP]);
                    k1ValueEntity.Time = Convert.ToDateTime(ds.Tables[0].Rows[0][K1ValueDbConstNames.TIME].ToString());
                    k1ValueEntity.TypeInTime = Convert.ToDateTime(ds.Tables[0].Rows[0][K1ValueDbConstNames.TYPE_IN_TIME].ToString());
                    k1ValueEntity.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.TUNNEL_ID]);

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
            return k1ValueEntity;
        }

        /// <summary>
        /// 返回某组K1值
        /// </summary>
        /// <param name="k1ValueID">K1分组ID</param>
        /// <returns>K1实体</returns>
        public static K1ValueEntity[] selectValueK1ByK1ValueID(int k1ValueID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.VALUE_K1_ID + " = " + k1ValueID;
            DataSet ds = db.ReturnDS(sql);
            K1ValueEntity[] k1Entity = new K1ValueEntity[ds.Tables[0].Rows.Count];
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        K1ValueEntity k1ValueEntity = new K1ValueEntity();
                        k1ValueEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.ID]);
                        k1ValueEntity.K1ValueID = Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_ID]);
                        k1ValueEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_X]);
                        k1ValueEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_Y]);
                        k1ValueEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.COORDINATE_Z]);
                        k1ValueEntity.ValueK1Dry = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_DRY]);
                        k1ValueEntity.ValueK1Wet = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_K1_WET]);
                        k1ValueEntity.Sg = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_SG]);
                        k1ValueEntity.Sv = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_SV]);
                        k1ValueEntity.Q = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.VALUE_Q]);
                        k1ValueEntity.BoreholeDeep = Convert.ToDouble(ds.Tables[0].Rows[i][K1ValueDbConstNames.BOREHOLE_DEEP]);
                        k1ValueEntity.Time = Convert.ToDateTime(ds.Tables[0].Rows[i][K1ValueDbConstNames.TIME].ToString());
                        k1ValueEntity.TypeInTime = Convert.ToDateTime(ds.Tables[0].Rows[i][K1ValueDbConstNames.TYPE_IN_TIME].ToString());
                        k1ValueEntity.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[i][K1ValueDbConstNames.TUNNEL_ID]);
                        k1Entity[i] = k1ValueEntity;

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
            return k1Entity;
        }

        /// <summary>
        /// 返回某K1值
        /// </summary>
        /// <param name="tunnelID">巷道ID</param>
        /// <returns>K1实体</returns>
        public static K1ValueEntity selectValueK1ByTunnelID(int tunnelID)
        {
            K1ValueEntity k1ValueEntity = new K1ValueEntity();
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.TUNNEL_ID + " = " + tunnelID;
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {

                    k1ValueEntity.ID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.ID]);
                    k1ValueEntity.K1ValueID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_ID]);
                    k1ValueEntity.CoordinateX = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_X]);
                    k1ValueEntity.CoordinateY = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_Y]);
                    k1ValueEntity.CoordinateZ = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.COORDINATE_Z]);
                    k1ValueEntity.ValueK1Dry = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_DRY]);
                    k1ValueEntity.ValueK1Wet = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.VALUE_K1_WET]);
                    k1ValueEntity.BoreholeDeep = Convert.ToDouble(ds.Tables[0].Rows[0][K1ValueDbConstNames.BOREHOLE_DEEP]);
                    k1ValueEntity.Time = Convert.ToDateTime(ds.Tables[0].Rows[0][K1ValueDbConstNames.TIME].ToString());
                    k1ValueEntity.TypeInTime = Convert.ToDateTime(ds.Tables[0].Rows[0][K1ValueDbConstNames.TYPE_IN_TIME].ToString());
                    k1ValueEntity.TunnelID = Convert.ToInt32(ds.Tables[0].Rows[0][K1ValueDbConstNames.TUNNEL_ID]);

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
            return k1ValueEntity;
        }

        /// <summary>
        /// 分页用查询K1值
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectValueK1Entity(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            StringBuilder sc = new StringBuilder();
            db.Open();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE ID IN (SELECT MAX(ID) FROM T_VALUE_K1  A,(SELECT MAX(VALUE_K1_DRY) AS VALUE_K1_DRY,VALUE_K1_ID FROM T_VALUE_K1 GROUP BY VALUE_K1_ID)B WHERE A.VALUE_K1_ID = B.VALUE_K1_ID AND A.VALUE_K1_DRY = B. VALUE_K1_DRY GROUP BY A.VALUE_K1_DRY,A.VALUE_K1_ID)) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            sc.Append("SELECT * FROM ( ");
            sc.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid, * ");
            sc.Append("FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE ID IN (SELECT MAX(ID) FROM T_VALUE_K1  A,(SELECT MAX(VALUE_K1_WET) AS VALUE_K1_WET,VALUE_K1_ID FROM T_VALUE_K1 GROUP BY VALUE_K1_ID)B WHERE A.VALUE_K1_ID = B.VALUE_K1_ID AND A.VALUE_K1_WET = B. VALUE_K1_WET GROUP BY A.VALUE_K1_WET,A.VALUE_K1_ID)) AS TB ");
            sc.Append("WHERE rowid >= " + iStartIndex);
            sc.Append("AND rowid <= " + iEndIndex);

            DataSet ds = db.ReturnDSNotOpenAndClose(sb.ToString());
            DataSet ds2 = db.ReturnDSNotOpenAndClose(sc.ToString());
            ds.Merge(ds2);
            DataView dv = new DataView();
            dv.Table = ds.Tables[0];
            dv.Sort = "VALUE_K1_ID ASC";
            ds.Tables.Clear();
            ds.Tables.Add(dv.ToTable());
            db.Close();
            return ds;
        }

        public static DataSet selectValueK1Entity2(int iStartIndex, int iEndIndex, int tunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            db.Open();
            sb.Append("SELECT * FROM");
            sb.Append(" (SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid,* FROM ");
            sb.Append(" (SELECT ROW_NUMBER() OVER(PARTITION BY " + K1ValueDbConstNames.VALUE_K1_ID + " ORDER BY " + K1ValueDbConstNames.ID + ") AS V, * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.ID + " IN");
            sb.Append(" (SELECT MAX(" + K1ValueDbConstNames.ID + ") FROM " + K1ValueDbConstNames.TABLE_NAME + " A,(SELECT  MAX((" + K1ValueDbConstNames.VALUE_K1_DRY + "+" + K1ValueDbConstNames.VALUE_K1_WET + " + ABS(" + K1ValueDbConstNames.VALUE_K1_DRY + "-" + K1ValueDbConstNames.VALUE_K1_WET + "))/2) AS " + K1ValueDbConstNames.VALUE_K1_DRY + "," + K1ValueDbConstNames.VALUE_K1_ID + " FROM " + K1ValueDbConstNames.TABLE_NAME + " GROUP BY " + K1ValueDbConstNames.VALUE_K1_ID + ") B ");
            //此处的K1ValueDbConstNames.Value_K1_DRY并非是表中的字段名，而是自定名称的字段
            sb.Append(" WHERE A." + K1ValueDbConstNames.VALUE_K1_ID + " = B." + K1ValueDbConstNames.VALUE_K1_ID + " AND (A." + K1ValueDbConstNames.VALUE_K1_DRY + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + " OR A." + K1ValueDbConstNames.VALUE_K1_WET + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + ") GROUP BY A." + K1ValueDbConstNames.VALUE_K1_DRY + ",A." + K1ValueDbConstNames.VALUE_K1_ID + ")) AS TC");
            sb.Append(" WHERE V <= 1");
            sb.Append(" AND " + K1ValueDbConstNames.TUNNEL_ID + " = " + tunnelId);
            sb.Append(" AND " + K1ValueDbConstNames.TIME + " BETWEEN '" + startTime + "' AND '" + endTime + "' ) AS TB ");
            sb.Append(" WHERE rowid >=" + iStartIndex + " AND rowid <= " + iEndIndex);


            DataSet ds = db.ReturnDSNotOpenAndClose(sb.ToString());
            return ds;
        }

        public static DataSet selectValueK1Entity2(int tunnelId, string startTime, string endTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            db.Open();
            sb.Append("SELECT * FROM");
            sb.Append(" (SELECT ROW_NUMBER() OVER(PARTITION BY " + K1ValueDbConstNames.VALUE_K1_ID + " ORDER BY " + K1ValueDbConstNames.ID + ") AS V, * FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.ID + " IN");
            sb.Append(" (SELECT MAX(" + K1ValueDbConstNames.ID + ") FROM " + K1ValueDbConstNames.TABLE_NAME + " A,(SELECT  MAX((" + K1ValueDbConstNames.VALUE_K1_DRY + "+" + K1ValueDbConstNames.VALUE_K1_WET + " + ABS(" + K1ValueDbConstNames.VALUE_K1_DRY + "-" + K1ValueDbConstNames.VALUE_K1_WET + "))/2) AS " + K1ValueDbConstNames.VALUE_K1_DRY + "," + K1ValueDbConstNames.VALUE_K1_ID + " FROM " + K1ValueDbConstNames.TABLE_NAME + " GROUP BY " + K1ValueDbConstNames.VALUE_K1_ID + ") B ");
            //此处的K1ValueDbConstNames.Value_K1_DRY并非是表中的字段名，而是自定名称的字段
            sb.Append(" WHERE A." + K1ValueDbConstNames.VALUE_K1_ID + " = B." + K1ValueDbConstNames.VALUE_K1_ID + " AND (A." + K1ValueDbConstNames.VALUE_K1_DRY + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + " OR A." + K1ValueDbConstNames.VALUE_K1_WET + " = B." + K1ValueDbConstNames.VALUE_K1_DRY + ") GROUP BY A." + K1ValueDbConstNames.VALUE_K1_DRY + ",A." + K1ValueDbConstNames.VALUE_K1_ID + ")) AS TC");
            sb.Append(" WHERE " + K1ValueDbConstNames.TUNNEL_ID + " = " + tunnelId);
            sb.Append(" AND " + K1ValueDbConstNames.TIME + " BETWEEN '" + startTime + "' AND '" + endTime + "'");

            DataSet ds = db.ReturnDSNotOpenAndClose(sb.ToString());
            return ds;
        }
        //public static DataSet selectValueK1Entity(int iStartIndex, int iEndIndex)
        //{
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("SELECT * FROM ( ");


        //    return ds;
        //}

        /// <summary>
        /// 分页用查询K1值
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectValueK1EntityByCondition(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            StringBuilder sc = new StringBuilder();
            db.Open();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + K1ValueDbConstNames.TABLE_NAME + ") AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            //sb.Append(" AND TIME BETWEEN '" + strStartTime + "' AND '" + strEndTime + "'");

            //sc.Append("SELECT * FROM ( ");
            //sc.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid, * ");
            //sc.Append("FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE ID IN (SELECT MAX(ID) FROM T_VALUE_K1  A,(SELECT MAX(VALUE_K1_WET) AS VALUE_K1_WET,VALUE_K1_ID FROM T_VALUE_K1 GROUP BY VALUE_K1_ID)B WHERE A.VALUE_K1_ID = B.VALUE_K1_ID AND A.VALUE_K1_WET = B. VALUE_K1_WET GROUP BY A.VALUE_K1_WET,A.VALUE_K1_ID)) AS TB ");
            //sc.Append("WHERE rowid >= " + iStartIndex);
            //sc.Append(" AND rowid <= " + iEndIndex);
            //sc.Append(" AND strStartTime >= " + strStartTime);
            //sc.Append(" AND TIME BETWEEN " + strStartTime + " AND " + strEndTime);

            //if (iTunnelId != -1)
            //{
            //    sb.Append(" AND TUNNEL_ID = " + iTunnelId);
            //}

            DataSet ds = db.ReturnDSNotOpenAndClose(sb.ToString());
            //DataSet ds2 = db.ReturnDSNotOpenAndClose(sc.ToString());
            //ds.Merge(ds2);
            //DataView dv = new DataView();
            //dv.Table = ds.Tables[0];
            //dv.Sort = "VALUE_K1_ID ASC";
            //ds.Tables.Clear();
            //ds.Tables.Add(dv.ToTable());
            db.Close();
            return ds;
        }

        /// <summary>
        /// 分页用查询K1值
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public static DataSet selectValueK1EntityByConditionWithoutPage(int iTunnelId)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            //StringBuilder sc = new StringBuilder();
            //db.Open();
            sb.Append("SELECT * ");
            sb.Append("FROM " + K1ValueDbConstNames.TABLE_NAME);
            //sb.Append("WHERE TIME BETWEEN '" + strStartTime + "' AND '" + strEndTime + "'");

            //sc.Append("SELECT * FROM ( ");
            //sc.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + K1ValueDbConstNames.ID + ") AS rowid, * ");
            //sc.Append("FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE ID IN (SELECT MAX(ID) FROM T_VALUE_K1  A,(SELECT MAX(VALUE_K1_WET) AS VALUE_K1_WET,VALUE_K1_ID FROM T_VALUE_K1 GROUP BY VALUE_K1_ID)B WHERE A.VALUE_K1_ID = B.VALUE_K1_ID AND A.VALUE_K1_WET = B. VALUE_K1_WET GROUP BY A.VALUE_K1_WET,A.VALUE_K1_ID)) AS TB ");
            //sc.Append("WHERE rowid >= " + iStartIndex);
            //sc.Append(" AND rowid <= " + iEndIndex);
            //sc.Append(" AND strStartTime >= " + strStartTime);
            //sc.Append(" AND TIME BETWEEN " + strStartTime + " AND " + strEndTime);

            if (iTunnelId != -1)
            {
                sb.Append(" WHERE TUNNEL_ID = " + iTunnelId);
            }

            DataSet ds = db.ReturnDS(sb.ToString());
            //DataSet ds2 = db.ReturnDSNotOpenAndClose(sc.ToString());
            //ds.Merge(ds2);
            //DataView dv = new DataView();
            //dv.Table = ds.Tables[0];
            //dv.Sort = "VALUE_K1_ID ASC";
            //ds.Tables.Clear();
            //ds.Tables.Add(dv.ToTable());
            //db.Close();
            return ds;
            //ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            //string sql = "SELECT DISTINCT " + K1ValueDbConstNames.VALUE_K1_ID + " FROM " + K1ValueDbConstNames.TABLE_NAME;
            //DataSet ds = db.ReturnDS(sql);
            //return ds;
        }


        /// <summary>
        /// 返回valueK1分组数
        /// </summary>
        /// <returns></returns>
        public static int selectValueK1GroupCount()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "SELECT MAX(" + K1ValueDbConstNames.VALUE_K1_ID + ") FROM " + K1ValueDbConstNames.TABLE_NAME;
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
        /// 插入K1Value值
        /// </summary>
        /// <param name="k1ValueEntity">K1Value实体</param>
        /// <param name="count">K1Value分组数</param>
        /// <returns>是否成功插入?true:false</returns>
        public static bool insertValueK1(K1ValueEntity k1ValueEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + K1ValueDbConstNames.TABLE_NAME + " (");
            sb.Append(K1ValueDbConstNames.VALUE_K1_ID + ",");
            sb.Append(K1ValueDbConstNames.COORDINATE_X + ",");
            sb.Append(K1ValueDbConstNames.COORDINATE_Y + ",");
            sb.Append(K1ValueDbConstNames.COORDINATE_Z + ",");
            sb.Append(K1ValueDbConstNames.VALUE_K1_DRY + ",");
            sb.Append(K1ValueDbConstNames.VALUE_K1_WET + ",");
            sb.Append(K1ValueDbConstNames.VALUE_SG + ",");
            sb.Append(K1ValueDbConstNames.VALUE_SV + ",");
            sb.Append(K1ValueDbConstNames.VALUE_Q + ",");
            sb.Append(K1ValueDbConstNames.BOREHOLE_DEEP + ",");
            sb.Append(K1ValueDbConstNames.TIME + ",");
            sb.Append(K1ValueDbConstNames.TYPE_IN_TIME + ",");
            sb.Append(K1ValueDbConstNames.TUNNEL_ID + ") VALUES (");
            sb.Append(k1ValueEntity.K1ValueID + ",");
            sb.Append(k1ValueEntity.CoordinateX + ",");
            sb.Append(k1ValueEntity.CoordinateY + ",");
            sb.Append(k1ValueEntity.CoordinateZ + ",");
            sb.Append(k1ValueEntity.ValueK1Dry + ",");
            sb.Append(k1ValueEntity.ValueK1Wet + ",");
            sb.Append(k1ValueEntity.Sg + ",");
            sb.Append(k1ValueEntity.Sv + ",");
            sb.Append(k1ValueEntity.Q + ",");
            sb.Append(k1ValueEntity.BoreholeDeep + ",'");
            sb.Append(k1ValueEntity.Time + "','");
            sb.Append(k1ValueEntity.TypeInTime + "',");
            sb.Append(k1ValueEntity.TunnelID + ")");

            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改ValueK1
        /// </summary>
        /// <param name="k1ValueEntity"></param>
        /// <returns></returns>
        public static bool updateValueK1(K1ValueEntity k1ValueEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + K1ValueDbConstNames.TABLE_NAME + " SET ");
            sb.Append(K1ValueDbConstNames.COORDINATE_X + " = " + k1ValueEntity.CoordinateX + ",");
            sb.Append(K1ValueDbConstNames.COORDINATE_Y + " = " + k1ValueEntity.CoordinateY + ",");
            sb.Append(K1ValueDbConstNames.COORDINATE_Z + " = " + k1ValueEntity.CoordinateZ + ",");
            sb.Append(K1ValueDbConstNames.VALUE_K1_DRY + " = " + k1ValueEntity.ValueK1Dry + ",");
            sb.Append(K1ValueDbConstNames.VALUE_K1_WET + " = " + k1ValueEntity.ValueK1Wet + ",");
            sb.Append(K1ValueDbConstNames.VALUE_SG + " = " + k1ValueEntity.Sg + ",");
            sb.Append(K1ValueDbConstNames.VALUE_SV + " = " + k1ValueEntity.Sv + ",");
            sb.Append(K1ValueDbConstNames.VALUE_Q + " = " + k1ValueEntity.Q + ",");
            sb.Append(K1ValueDbConstNames.BOREHOLE_DEEP + " = " + k1ValueEntity.BoreholeDeep + ",");
            sb.Append(K1ValueDbConstNames.TIME + " = '" + k1ValueEntity.Time + "',");
            sb.Append(K1ValueDbConstNames.TYPE_IN_TIME + " = '" + k1ValueEntity.TypeInTime + "',");
            sb.Append(K1ValueDbConstNames.TUNNEL_ID + " = " + k1ValueEntity.TunnelID + " WHERE ");
            sb.Append(K1ValueDbConstNames.ID + " = " + k1ValueEntity.ID);

            bool bResult = db.OperateDB(sb.ToString());
            return bResult;

        }

        /// <summary>
        /// 删除K1值
        /// </summary>
        /// <param name="k1ValueEntity">K1实体</param>
        /// <param name="deleteType">删除方式：0为删除一条，1为删除该组所有数据</param>
        /// <returns></returns>
        public static bool deleteK1Value(K1ValueEntity k1ValueEntity, int deleteType)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            string sql = "";
            if (deleteType == 0)
            {
                sql = "DELETE FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.ID + " = " + k1ValueEntity.ID;
            }
            if (deleteType == 1)
            {
                sql = "DELETE FROM " + K1ValueDbConstNames.TABLE_NAME + " WHERE " + K1ValueDbConstNames.VALUE_K1_ID + " = " + k1ValueEntity.K1ValueID;
            }
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
