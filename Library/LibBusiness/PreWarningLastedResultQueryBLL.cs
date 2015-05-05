using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBusiness.CommonBLL;
using LibEntity;
using LibDatabase;
using System.Data;
using LibCommon;

namespace LibBusiness
{
    public class PreWarningLastedResultQueryBLL
    {
        //public static List<String> GetWarningIdListWithTunnelId(string
        //    tunnelId)
        //{
        //    List<String> results = new List<string>();
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM ");
        //    strSql.Append(PreWarningResultViewDbConstNames.VIEW_NAME);
        //    strSql.Append(" WHERE ");
        //    strSql.Append(PreWarningResultDBConstNames.WARNING_RESULT);
        //    strSql.Append(" < 2 ");
        //    strSql.Append(" AND " +
        //        PreWarningResultDBConstNames.HANDLE_STATUS + " < 3"); // 3指 HANDLED
        //    strSql.Append(" AND WARNING_STATUS = 1");
        //    strSql.Append(" AND ");
        //    strSql.Append(TunnelInfoDbConstNames.ID + " = '" + tunnelId +
        //        "'");

        //    ManageDataBase db = new
        //        ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    DataTable dt = db.ReturnDS(strSql.ToString()).Tables[0];

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        results.Add(dt.Rows[i][PreWarningResultDBConstNames.ID].ToString());
        //    }

        //    return results;
        //}
    }
}
