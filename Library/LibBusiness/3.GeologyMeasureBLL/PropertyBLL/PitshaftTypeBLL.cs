// ******************************************************************
// 概  述：井筒类型业务逻辑
// 作  者：伍鑫
// 创建日期：2014/03/06
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
    public class PitshaftTypeBLL
    {
        /// <summary>
        /// 获取全部<井筒类型>信息
        /// </summary>
        /// <returns>全部<井筒类型>信息</returns>
        public static DataSet selectAllPitshaftTypeInfo()
        {
            string sqlStr = "SELECT * FROM " + PitshaftTypeDbConstNames.TABLE_NAME;

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr);
            return ds;
        }

        /// <summary>
        /// 通过【井筒类型】编号，获取【井筒类型】
        /// </summary>
        /// <param name="iPitshaftTypeId">【井筒类型】编号</param>
        /// <returns>【井筒类型】</returns>
        public static DataSet selectPitshaftTypeByPitshaftTypeId(int iPitshaftTypeId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + PitshaftTypeDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftTypeDbConstNames.PITSHAFT_TYPE_ID + " = " + iPitshaftTypeId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
    }
}
