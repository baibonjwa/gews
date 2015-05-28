using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_EARLY_WARNING_DETAIL", Lazy = false)]
    public class EarlyWarningDetail : ActiveRecordBase<EarlyWarningDetail>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public string Id { get; set; }

        [BelongsTo("WARNING_ID")]
        public EarlyWarningResult EarlyWarningResult { get; set; }

        [BelongsTo("RULE_ID")]
        public PreWarningRules PreWarningRules { get; set; }

        [Property("DATA_ID")]
        public string DataId { get; set; }

        [Property("THRESHOLD")]
        public string Threshold { get; set; }

        [Property("ACTUAL_VALUE")]
        public string ActualValue { get; set; }

        [Property("ACTIONS")]
        public string Actions { get; set; }

        [Property("ACTIONS_PERSON")]
        public string ActionsPerson { get; set; }

        [Property("ACTIONS_DATE_TIME")]
        public DateTime? ActionsDateTime { get; set; }

        [Property("COMMENTS")]
        public string Comments { get; set; }

        [Property("COMMENTS_PERSON")]
        public string CommentsPerson { get; set; }

        [Property("COMMENTS_DATE_TIME")]
        public DateTime? CommentsDateTime { get; set; }

        [Property("LIFT_PERSON")]
        public string LiftPerson { get; set; }

        [Property("LIFT_DATE_TIME")]
        public DateTime? LiftDateTime { get; set; }

        public static EarlyWarningDetail[] FindAllByMutiCondition(int workingFaceId, DateTime dateTime, string shift,
            int warningResult, string warningType, string ruleType)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("EarlyWarningResult.Shift", shift)
                //Restrictions.Lt("EarlyWarningResult.HandleStatus", 3),
                //Restrictions.Eq("EarlyWarningResult.Tunnel.WorkingFace.WorkingFaceId", workingFaceId),
                //Restrictions.Eq("EarlyWarningResult.DateTime", dateTime),
                //Restrictions.Eq("EarlyWarningResult.Shift", shift),
                //Restrictions.Eq("EarlyWarningResult.WarningResult", warningResult),
                //Restrictions.Eq("PreWarningRules.WarningType", warningType),
                //Restrictions.Eq("PreWarningRules.RuleType", ruleType)
            };
            return FindAll(criterion);
        }

        public static EarlyWarningDetail[] FindAllByWarningId(int[] warningId)
        {
            var criterion = new Disjunction();
            foreach (var i in warningId)
            {
                criterion.Add(Restrictions.Eq("EarlyWarningResult.Id", i));
            }
            return FindAll(criterion);
        }

        public static EarlyWarningDetail[] FindAllByWarningId(string[] warningId)
        {
            var criterion = new Disjunction();
            foreach (var i in warningId.Where(i => !string.IsNullOrWhiteSpace(i)))
            {
                criterion.Add(Restrictions.Eq("EarlyWarningResult.Id", Convert.ToInt32(i)));
            }
            return FindAll(criterion);
        }

    }
}
