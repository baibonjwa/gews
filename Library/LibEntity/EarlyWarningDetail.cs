using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_EARLY_WARNING_DETAIL")]
    public class EarlyWarningDetail : ActiveRecordBase<EarlyWarningDetail>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public string Id { get; set; }

        [BelongsTo("WARNING_ID")]
        public EarlyWarningResult EarlyWarningResult { get; set; }

        [BelongsTo("RULE_ID")]
        public PreWarningRules PreWarningRules { get; set; }

        public string DataId { get; set; }

        public string HandleStatus { get; set; }

        public string DateTime { get; set; }

        public string WarningType { get; set; }

        public string WarningLevel { get; set; }

        public string Threshold { get; set; }

        public string ActualValue { get; set; }

        public string Actions { get; set; }

        public string ActionsPerson { get; set; }

        public string ActionsDateTime { get; set; }

        public string Comments { get; set; }

        public string CommentsPerson { get; set; }

        public string CommentsDateTime { get; set; }

        public string LiftPerson { get; set; }

        public string LiftDateTime { get; set; }

        public string RuleId { get; set; }

    }
}
