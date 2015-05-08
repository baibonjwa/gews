using System.Text;
using LibEntity;
using LibDatabase;
using System.Data;
using LibCommon;

namespace LibBusiness
{
    public class GeologySpaceBll
    {

        /// <summary>
        /// 处理地质构造信息
        /// </summary>
        /// <param name="geologySpaceEntity"></param>
        /// <returns></returns>
        public static bool ProcGeologySpaceEntityInfo(GeologySpace 
            geologySpaceEntity)
        {
            var db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            var sb = new StringBuilder();
            sb.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;");
            sb.Append("BEGIN TRANSACTION;");
            sb.Append("IF EXISTS( SELECT * FROM T_GEOLOGY_SPACE WHERE ");
            sb.Append(GeologySpaceDbConstNames.WORKFACE_ID + "=" + 
                geologySpaceEntity.WorkingFace);
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_ID + "='" 
                + geologySpaceEntity.TectonicId + "'");
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_TYPE + 
                "=" + geologySpaceEntity.TectonicType+")");
            sb.Append(" BEGIN ");
            sb.Append(" UPDATE " + GeologySpaceDbConstNames.TABLE_NAME + 
                " SET ");
            sb.Append(GeologySpaceDbConstNames.TECTONIC_DISTANCE + "=" + 
                geologySpaceEntity.Distance);
            sb.Append("," + GeologySpaceDbConstNames.DATE_TIME + "=" + 
                geologySpaceEntity.OnDateTime);
            sb.Append(" WHERE " + GeologySpaceDbConstNames.WORKFACE_ID + 
                "=" + geologySpaceEntity.WorkingFace);
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_ID + "='" 
                + geologySpaceEntity.TectonicId + "'");
            sb.Append(" AND " + GeologySpaceDbConstNames.TECTONIC_TYPE + 
                "=" + geologySpaceEntity.TectonicType + ";");
            sb.Append(" END ");
            sb.Append(" ELSE ");
            sb.Append(" BEGIN ");
            sb.Append("INSERT INTO " + GeologySpaceDbConstNames.TABLE_NAME 
                + "(" + GeologySpaceDbConstNames.WORKFACE_ID + ","
                + GeologySpaceDbConstNames.TECTONIC_ID + "," + 
                    GeologySpaceDbConstNames.TECTONIC_TYPE + ","
                + GeologySpaceDbConstNames.TECTONIC_DISTANCE + "," + 
                    GeologySpaceDbConstNames.DATE_TIME + ")VALUES(");
            sb.Append(geologySpaceEntity.WorkingFace + ",'" + 
                geologySpaceEntity.TectonicId + "'," + 
                geologySpaceEntity.TectonicType + "," +
                geologySpaceEntity.Distance + ",'" + 
                    geologySpaceEntity.OnDateTime + "');");
            sb.Append(" END ");
            sb.Append("COMMIT TRANSACTION;");

            Log.Debug("[Geology_Distance]" + sb);

            var bres = db.OperateDB(sb.ToString());
            return bres;
        }

        /// <summary>
        /// 删除指定工作面的地质构造信息
        /// </summary>
        /// <param name="workingFaceId">工作面ID</param>
        /// <returns></returns>
        public static bool DeleteGeologySpaceEntityInfos(int workingFaceId)
        {
            var db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            var sql = "DELETE FROM " + GeologySpaceDbConstNames.TABLE_NAME 
                + " WHERE " +
                GeologySpaceDbConstNames.WORKFACE_ID+"="+workingFaceId;
            var bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
