// ******************************************************************
// 概  述："规则编码与参数"单元
// 作  者：杨小颖  
// 创建日期：2013/12/23
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections;


namespace LibEntity
{
   
    public class RuleInfo
    {
        private int _ruleId;

        public RuleInfo() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        public RuleInfo(int ruleId)
        {
            _ruleId = ruleId;
        }

        //规则编码字符串
        private string _ruleIdsStr = "";

        public string RuleCodesStr
        {
            get { return _ruleIdsStr; }
            set { _ruleIdsStr = value; }
        }

        /// <summary>
        /// 规则ID
        /// </summary>
        public int Id
        {
            get { return _ruleId; }
            set { _ruleId = value; }
        }

        //////规则编码
        //private string _ruleCode;

        //public string RuleCode
        //{
        //    get { return _ruleCode; }
        //    set { _ruleCode = value; }
        //}

        //预警参数（用户自定义的）
        private Hashtable _preWarningParams = null;

        public Hashtable PreWarningParams
        {
            get { return _preWarningParams; }
            set { _preWarningParams = value; }
        }

        //所有规则编码对应的所有参数字符串
        private string _paramsInfoStr = "";

        public string ParamsInfoStr
        {
            get { return _paramsInfoStr; }
            set { _paramsInfoStr = value; }
        }
    }
}
