// ******************************************************************
// 概  述：探头类型业务逻辑
// 作  者：伍鑫
// 创建日期：2014/03/01
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

namespace LibBusiness
{
    public class ProbeTypeBLL
    {
        /// <summary>
        /// 获取全部<探头类型>信息
        /// </summary>
        /// <returns>全部探头类型信息</returns>
        public static DataSet selectAllProbeTypeInfo()
        {
            string sqlStr = "SELECT * FROM " + ProbeTypeDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 根据<探头类型>编号，获取该<探头类型>的详细信息
        /// </summary>
        /// <param name="probeId"><探头类型>编号</param>
        /// <returns>探头信息</returns>
        public static DataSet selectProbeTypeInfoByProbeTypeId(int iProbeTypeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + ProbeTypeDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + ProbeTypeDbConstNames.PROBE_TYPE_ID + " = " + iProbeTypeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
    }
}
