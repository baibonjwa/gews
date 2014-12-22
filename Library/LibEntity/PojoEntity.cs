using System.Collections.Generic;

namespace LibEntity
{
    public class PojoWarningDetail
    {
        public string Id { get; set; }

        public string Actions { get; set; }

        public string ActionsDateTime { get; set; }

        public string ActionsPerson { get; set; }

        public string Comments { get; set; }

        public string CommentsDateTime { get; set; }

        public string CommentsPerson { get; set; }

        public string LiftPerson { get; set; }

        public string LiftDateTime { get; set; }

        public string RuleId { get; set; }

        public string DataId { get; set; }
    }

    public class PojoWarning
    {
        private readonly List<PojoWarningDetail> details = new List<PojoWarningDetail>();
        public string WarningId { get; set; }

        public List<PojoWarningDetail> WarningDetails
        {
            get { return details; }
        }

        public void addDetail(PojoWarningDetail warningDetail)
        {
            details.Add(warningDetail);
        }
    }

    public class PojoWarningContainer
    {
        private readonly List<PojoWarning> warningList = new List<PojoWarning>();
        public string TunnelId { get; set; }

        public List<PojoWarning> WarningList
        {
            get { return warningList; }
        }

        public void addWarning(PojoWarning warning)
        {
            warningList.Add(warning);
        }
    }
}