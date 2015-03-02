using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCommon;
using LibDatabase;
using LibEntity;
using System.Data;

namespace LibBusiness
{
    public class PreWarningDetailsBLL
    {
        public static List<WarningResultDetail> 
            getHistoryWarningResultDetails(string sWorkingface,
            string sDate, string sShift,
            string sWarningResult, string sWarningType,
            string sWarningItem)
        {
            string sqlStr = "SELECT * FROM " + 
                PreWarningDetailsViewDbConstName.VIEW_NAME;
            sqlStr += " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_NAME 
                + " = '" + sWorkingface + "'";
            if (!String.IsNullOrEmpty(sDate))
            {
                sqlStr += " AND ";
                sqlStr += PreWarningResultDBConstNames.DATA_TIME + 
                    " BETWEEN '" + sDate + " 00:00:00:000' AND '" + sDate + 
                    " 23:59:59:000'";
            }
            if (!String.IsNullOrEmpty(sShift))
            {
                sqlStr += " AND ";
                sqlStr += PreWarningResultDBConstNames.DATE_SHIFT + " = '" 
                    + sShift + "'";
            }
            if (!String.IsNullOrEmpty(sWarningResult))
            {
                sqlStr += " AND ";
                sqlStr += PreWarningResultDBConstNames.WARNING_RESULT + 
                    " = '" + sWarningResult + "'";
            }
            if (!String.IsNullOrEmpty(sWarningType))
            {
                sqlStr += " AND ";
                sqlStr += 
                    PreWarningDetailsViewDbConstName.WARNING_TYPE_RULES + 
                    " = '" + sWarningType + "'";
            }
            if (!String.IsNullOrEmpty(sWarningItem))
            {
                sqlStr += " AND ";
                sqlStr += PreWarningRulesDbConstNames.RULE_TYPE + " = '" + 
                    sWarningItem + "'";
            }

            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = db.ReturnDS(sqlStr).Tables[0];

            return getDetailsListByDataTable(dt);
        }

        public static List<WarningResultDetail> 
            getHistoryWarningResultDetails(string warningId)
        {
            string[] ids = warningId.Split(',');
            string sqlStr = "SELECT * FROM " + 
                PreWarningDetailsViewDbConstName.VIEW_NAME;
            sqlStr += " WHERE 1=0";
            for (int i = 0; i < ids.Length; i++)
            {
                sqlStr += " OR " + 
                    PreWarningDetailsViewDbConstName.WARNING_ID + "=" + ids[i];
            }

            ManageDataBase db = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = db.ReturnDS(sqlStr).Tables[0];
            return getDetailsListByDataTable(dt);
        }

        private static List<WarningResultDetail> 
            getDetailsListByDataTable(DataTable dt)
        {
            List<WarningResultDetail> list = new 
                List<WarningResultDetail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WarningResultDetail ent = new WarningResultDetail
                {
                    Id = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.ID].ToString(),
                    WarningId = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.WARNING_ID].ToString(),
                    HandleStatus = 
                        dt.Rows[i][PreWarningResultDBConstNames.HANDLE_STATUS].ToString(),
                    DateTime = 
                        dt.Rows[i][PreWarningResultDBConstNames.DATA_TIME].ToString(),
                    WarningType = 
                        dt.Rows[i][PreWarningResultDBConstNames.WARNING_TYPE].ToString(),
                    WarningLevel = 
                        dt.Rows[i][PreWarningRulesDbConstNames.WARNING_LEVEL].ToString(),
                    RuleCode = 
                        dt.Rows[i][PreWarningRulesDbConstNames.RULE_CODE].ToString(),
                    RuleType = 
                        dt.Rows[i][PreWarningRulesDbConstNames.RULE_TYPE].ToString(),
                    RuleDescription = 
                        dt.Rows[i][PreWarningRulesDbConstNames.RULE_DESCRIPTION].ToString(),
                    Threshold = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.THRESHOLD].ToString(),
                    ActualValue = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.ACTUAL_VALUE].ToString(),
                    Actions = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.ACTIONS].ToString(),
                    ActionsPerson = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.ACTIONS_PERSON].ToString(),
                    ActionsDateTime = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.ACTIONS_DATE_TIME].ToString(),
                    Comments = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.COMMENTS].ToString(),
                    CommentsPerson = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.COMMENTS_PERSON].ToString(),
                    CommentsDateTime = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.COMMENTS_DATE_TIME].ToString(),
                    LiftPerson = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.LIFT_PERSON].ToString(),
                    LiftDateTime = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.LIFT_DATE_TIME].ToString(),
                    RuleId = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.RULE_ID].ToString(),
                    DataId = 
                        dt.Rows[i][PreWarningDetailsViewDbConstName.DATA_ID].ToString()
                };
                list.Add(ent);
            }
            return list;
        }
    }
}
