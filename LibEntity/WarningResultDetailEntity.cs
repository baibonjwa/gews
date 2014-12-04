namespace LibEntity
{
    public class WarningResultDetailEntity
    {
        // 应当对应数T_EARLY_WARNING_DETAIL据库表中的主键ID
        public string Id { get; set; }

        public string WarningId { get; set; }

        public string HandleStatus { get; set; }

        public string DateTime { get; set; }

        public string WarningType { get; set; }

        public string WarningLevel { get; set; }

        public string RuleCode { get; set; }

        // 规则类型：瓦斯，地质构造，管理因素，煤层赋存，通风，其他
        public string RuleType { get; set; }

        public string RuleDescription { get; set; }

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

        public string DataId { get; set; }
    }
}