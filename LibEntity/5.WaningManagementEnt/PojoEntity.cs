using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class PojoWarningDetail
    {
        private string _sId;
        public string Id
        {
            get { return this._sId; }
            set { this._sId = value; }
        }

        private string _sActions;
        public string Actions
        {
            get { return this._sActions; }
            set { this._sActions = value; }
        }

        private string _sActionsDateTime;
        public string ActionsDateTime
        {
            get { return this._sActionsDateTime; }
            set { this._sActionsDateTime = value; }
        }

        private string _sActionsPerson;
        public string ActionsPerson
        {
            get { return this._sActionsPerson; }
            set { this._sActionsPerson = value; }
        }

        private string _sComments;
        public string Comments
        {
            get { return this._sComments; }
            set { this._sComments = value; }
        }

        private string _sCommentsDateTime;
        public string CommentsDateTime
        {
            get { return this._sCommentsDateTime; }
            set { this._sCommentsDateTime = value; }
        }

        private string _sCommentsPerson;
        public string CommentsPerson
        {
            get { return this._sCommentsPerson; }
            set { this._sCommentsPerson = value; }
        }

        private string _sLiftPerson;
        public string LiftPerson
        {
            get { return this._sLiftPerson; }
            set { this._sLiftPerson = value; }
        }

        private string _sLiftDateTime;
        public string LiftDateTime
        {
            get { return this._sLiftDateTime; }
            set { this._sLiftDateTime = value; }
        }

        public string RuleId { get; set; }

        public string DataId { get; set; }
    }

    public class PojoWarning
    {
        private string _sWarningId;
        public string WarningId
        {
            get { return this._sWarningId; }
            set { this._sWarningId = value; }
        }

        List<PojoWarningDetail> details = new List<PojoWarningDetail>();
        public List<PojoWarningDetail> WarningDetails
        {
            get { return this.details; }
        }

        public void addDetail(PojoWarningDetail warningDetail)
        {
            this.details.Add(warningDetail);
        }
    }

    public class PojoWarningContainer
    {
        private string _sTunnelId;
        public string TunnelId
        {
            get { return this._sTunnelId; }
            set { this._sTunnelId = value; }
        }


        List<PojoWarning> warningList = new List<PojoWarning>();
        public List<PojoWarning> WarningList
        {
            get { return this.warningList; }
        }

        public void addWarning(PojoWarning warning)
        {
            this.warningList.Add(warning);
        }
    }
}
