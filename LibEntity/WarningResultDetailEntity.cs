using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class WarningResultDetailEntity
    {
        // 应当对应数T_EARLY_WARNING_DETAIL据库表中的主键ID
        private string _sId;
        public string Id
        {
            get { return this._sId; }
            set { this._sId = value; }
        }

        private string _sWarningId;
        public string WarningId
        {
            get { return this._sWarningId; }
            set { this._sWarningId = value; }
        }

        private string _sHandleStatus;
        public string HandleStatus
        {
            get { return this._sHandleStatus; }
            set { this._sHandleStatus = value; }
        }

        private string _sDateTime;
        public string DateTime
        {
            get { return this._sDateTime; }
            set { this._sDateTime = value; }
        }

        private string _sWarningType;
        public string WarningType
        {
            get { return this._sWarningType; }
            set { this._sWarningType = value; }
        }

        private string _sWarningLevel;
        public string WarningLevel
        {
            get { return this._sWarningLevel; }
            set { this._sWarningLevel = value; }
        }

        private string _sRuleCode;
        public string RuleCode
        {
            get { return this._sRuleCode; }
            set { this._sRuleCode = value; }
        }

        // 规则类型：瓦斯，地质构造，管理因素，煤层赋存，通风，其他
        private string _sRuleType;
        public string RuleType
        {
            get { return this._sRuleType; }
            set { this._sRuleType = value; }
        }

        private string _sRuleDescription;
        public string RuleDescription
        {
            get { return this._sRuleDescription; }
            set { this._sRuleDescription = value; }
        }

        private string _sThreshold;
        public string Threshold
        {
            get { return this._sThreshold; }
            set { this._sThreshold = value; }
        }

        private string _sActualValue;
        public string ActualValue
        {
            get { return this._sActualValue; }
            set { this._sActualValue = value; }
        }

        private string _sActions;  // 措施
        public string Actions
        {
            get { return this._sActions; }
            set { this._sActions = value; }
        }

        private string _sActionsPerson;  // 措施录入人
        public string ActionsPerson
        {
            get { return this._sActionsPerson; }
            set { this._sActionsPerson = value; }
        }

        private string _sActionsDateTime; // 措施录入日期
        public string ActionsDateTime
        {
            get { return this._sActionsDateTime; }
            set { this._sActionsDateTime = value; }
        }

        private string _sComments;  // 措施评价
        public string Comments
        {
            get { return this._sComments; }
            set { this._sComments = value; }
        }

        private string _sCommentsPerson;  // 评价人
        public string CommentsPerson
        {
            get { return this._sCommentsPerson; }
            set { this._sCommentsPerson = value; }
        }

        private string _sCommentsDateTime;  // 评价日期
        public string CommentsDateTime
        {
            get { return this._sCommentsDateTime; }
            set { this._sCommentsDateTime = value; }
        }

        private string _sLiftPerson;  // 预警解除人
        public string LiftPerson
        {
            get { return this._sLiftPerson; }
            set { this._sLiftPerson = value; }
        }

        private string _sLiftDateTIme; // 预警解除日期
        public string LiftDateTime
        {
            get { return this._sLiftDateTIme; }
            set { this._sLiftDateTIme = value; }
        }

        public string RuleId { get; set; }

        public string DataId { get; set; }
    }
}
