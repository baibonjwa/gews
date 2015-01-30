// ******************************************************************
// 概  述：井筒业务逻辑
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
    public class PitshaftBLL
    {




        /// <summary>
        /// 20140428 lyf
        /// 通过井筒主键（井筒编号），获取井筒绑定ID
        /// </summary>
        /// <param name="iPkIdxs">井筒主键</param>
        /// <returns></returns>
        public static string selectPitshaftInfoBIDByPitshaftPK(int iPkIdxs)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + PitshaftDbConstNames.BID + " FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = '" + iPkIdxs + "'");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            string sBID = "";
            try
            {
                sBID = ds.Tables[0].Rows[0][0].ToString();
                return sBID;
            }
            catch
            {
                return sBID;
            }
        }


        /// <summary>
        /// 通过【井筒】编号，获取【井筒】
        /// </summary>
        /// <param name="faultageId">【揭井筒露断层】编号</param>
        /// <returns>【井筒】</returns>
        public static DataSet selectPitshaftInfoByPitshaftId(int iPitshaftId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + PitshaftDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + PitshaftDbConstNames.PITSHAFT_ID + " = " + iPitshaftId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
    }
}
