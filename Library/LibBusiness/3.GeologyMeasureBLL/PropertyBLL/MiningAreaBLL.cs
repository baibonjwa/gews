using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDatabase;
using System.Data;
using LibEntity;

namespace LibBusiness
{
    public class MiningAreaBLL
    {


        /// <summary>
        /// 通过<水平编号>，获取该<水平>下所有<采区>信息
        /// </summary>
        /// <returns><采区>信息</returns>
        //public static DataSet selectMiningAreaInfoByHorizontalId(int iHorizontalId)
        //{
        //    StringBuilder sqlStr = new StringBuilder();
        //    sqlStr.Append("SELECT * FROM " + MiningAreaDbConstNames.TABLE_NAME);
        //    sqlStr.Append(" WHERE " + MiningAreaDbConstNames.HORIZONTAL_ID + " = " + iHorizontalId);

        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    DataSet ds = db.ReturnDS(sqlStr.ToString());
        //    return ds;
        //}

        /// <summary>
        /// 条件过滤巷道
        /// </summary>
        /// <param name="tunnelFilterRules">过滤规则</param>
        /// <param name="iWorkingFaceID">工作面ID</param>
        /// <returns>过滤后巷道信息</returns>
        //public static DataSet selectWorkingFaceListByMiningAreaId(int iMiningAreaID)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
        //    sb.Append(" WHERE " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + iMiningAreaID);
        //    ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
        //    DataSet ds = db.ReturnDS(sb.ToString());
        //    return ds;
        //}
    }
}
