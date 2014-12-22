// ******************************************************************
// 概  述：停采线业务逻辑
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
using LibEntity;
using LibDatabase;
using System.Data;
using LibCommon;

namespace LibBusiness
{
    public class GeologySpaceBLL
    {
        /// <summary>
        /// 分页用返回停采区所有信息
        /// </summary>
        /// <returns>分页用停采区所有信息</returns>
        public static DataSet selectStopLineInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + StopLineDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + StopLineDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询停采线名称是否存在
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否存在？是true：否false</returns>
        public static bool selectStopLineName(string stopLineName)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + StopLineDbConstNames.TABLE_NAME + " WHERE " + StopLineDbConstNames.STOP_LINE_NAME + " = '" + stopLineName+"'";
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 处理地质构造信息
        /// </summary>
        /// <param name="geologySpaceEntity"></param>
        /// <returns></returns>
        public static bool ProcGeologySpaceEntityInfo(GeologySpace geologySpaceEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;");
            sb.Append("BEGIN TRANSACTION;");
            sb.Append("IF EXISTS( SELECT * FROM T_GEOLOGY_SPACE WHERE ");
            sb.Append(GeologySpaceDbConstNames.WORKFACE_ID + "=" + geologySpaceEntity.WorkingFace);
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_ID + "='" + geologySpaceEntity.TectonicId + "'");
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_TYPE + "=" + geologySpaceEntity.TectonicType+")");
            sb.Append(" BEGIN ");
            sb.Append(" UPDATE " + GeologySpaceDbConstNames.TABLE_NAME + " SET ");
            sb.Append(GeologySpaceDbConstNames.TECTONIC_DISTANCE + "=" + geologySpaceEntity.Distance);
            sb.Append("," + GeologySpaceDbConstNames.DATE_TIME + "=" + geologySpaceEntity.OnDateTime);
            sb.Append(" WHERE " + GeologySpaceDbConstNames.WORKFACE_ID + "=" + geologySpaceEntity.WorkingFace);
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_ID + "='" + geologySpaceEntity.TectonicId + "'");
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_TYPE + "=" + geologySpaceEntity.TectonicType + ";");
            sb.Append(" END ");
            sb.Append(" ELSE ");
            sb.Append(" BEGIN ");
            sb.Append("INSERT INTO " + GeologySpaceDbConstNames.TABLE_NAME + "(" + GeologySpaceDbConstNames.WORKFACE_ID + ","
                + GeologySpaceDbConstNames.TECTONIC_ID + "," + GeologySpaceDbConstNames.TECTONIC_TYPE + ","
                + GeologySpaceDbConstNames.TECTONIC_DISTANCE + "," + GeologySpaceDbConstNames.DATE_TIME + ")VALUES(");
            sb.Append(geologySpaceEntity.WorkingFace + ",'" + geologySpaceEntity.TectonicId + "'," + geologySpaceEntity.TectonicType + "," +
                geologySpaceEntity.Distance + ",'" + geologySpaceEntity.OnDateTime + "');");
            sb.Append(" END ");
            sb.Append("COMMIT TRANSACTION;");

            Log.Debug("[Geology_Distance]" + sb.ToString());

            bool bres = db.OperateDB(sb.ToString());
            return bres;
        }
        /// <summary>
        /// 添加停采线信息
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否成功添加?true:false</returns>
        public static bool insertGeologySpaceEntityInfo(GeologySpace geologySpaceEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + GeologySpaceDbConstNames.TABLE_NAME + " (");
            sb.Append(GeologySpaceDbConstNames.WORKFACE_ID + ", ");
            sb.Append(GeologySpaceDbConstNames.TECTONIC_ID + ", ");
            sb.Append(GeologySpaceDbConstNames.TECTONIC_DISTANCE + ", ");
            sb.Append(GeologySpaceDbConstNames.TECTONIC_TYPE + ", ");
            sb.Append(GeologySpaceDbConstNames.DATE_TIME);
            sb.Append(") VALUES (");
            sb.Append(geologySpaceEntity.WorkingFace + ",'");
            sb.Append(geologySpaceEntity.TectonicId + "',");
            sb.Append(geologySpaceEntity.Distance + ",");
            sb.Append(geologySpaceEntity.TectonicType + ",'");
            sb.Append(geologySpaceEntity.OnDateTime + "')");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 更改停采线信息
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateGeologySpaceEntityInfo(GeologySpace geologySpaceEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + GeologySpaceDbConstNames.TABLE_NAME + " SET " + GeologySpaceDbConstNames.TECTONIC_DISTANCE + " = '");
            sb.Append(geologySpaceEntity.Distance + " WHERE "+GeologySpaceDbConstNames.TECTONIC_ID+"="+geologySpaceEntity.TectonicId+
                " AND "+GeologySpaceDbConstNames.TECTONIC_TYPE+"="+geologySpaceEntity.TectonicType);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除停采线信息
        /// </summary>
        /// <param name="stoplineEntity">停采线实体</param>
        /// <returns>是否成功删除?true:false</returns>
        public static bool deleteGeologySpaceEntityInfo(GeologySpace geologySpaceEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + GeologySpaceDbConstNames.TABLE_NAME + " WHERE " +
                GeologySpaceDbConstNames.TECTONIC_ID + " =" + geologySpaceEntity.TectonicId+
                " AND "+GeologySpaceDbConstNames.TECTONIC_TYPE+"="+geologySpaceEntity.TectonicType;

            bool bResult = db.OperateDB(sql);
            return bResult;
        }
        /// <summary>
        /// 删除指定工作面的地质构造信息
        /// </summary>
        /// <param name="WorkingFaceID">工作面ID</param>
        /// <returns></returns>
        public static bool deleteGeologySpaceEntityInfos(int WorkingFaceID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + GeologySpaceDbConstNames.TABLE_NAME + " WHERE " +
                GeologySpaceDbConstNames.WORKFACE_ID+"="+WorkingFaceID.ToString();
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
