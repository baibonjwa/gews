// ******************************************************************
// 概  述：预警规则实体
// 作  者：杨小颖  
// 创建日期：2013/12/10
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using LibCommon;


namespace LibEntity
{
    /// <summary>
    /// 预警规则实体，Excel原始预警规则表中数据格式要求：
    /// 
    /// 参数值全部能够转换为double，定性指标参数值为0或1
    /// 
    /// </summary>

    public class PreWarningRulesEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        public PreWarningRulesEntity(int ruleId)
        {
            _ruleId = ruleId;
        }

        public PreWarningRulesEntity()
        {
        }

        //规则ID（唯一标识）
        private int _ruleId = 0;

        public int RuleId
        {
            get { return _ruleId; }
            set { _ruleId = value; }
        }

        //规则编码，唯一标识（规则编码不会有重复内容）
        private string _ruleCode;

        public string RuleCode
        {
            get { return _ruleCode; }
            set { _ruleCode = value; }
        }
        //规则类别
        private string _ruleType;

        public string RuleType
        {
            get { return _ruleType; }
            set { _ruleType = value; }
        }
        //预警类型
        private string _warningType;

        public string WarningType
        {
            get { return _warningType; }
            set { _warningType = value; }
        }
        //预警级别
        private string _warningLevel;

        public string WarningLevel
        {
            get { return _warningLevel; }
            set { _warningLevel = value; }
        }
        //适用位置
        private string _suitableLocation;

        public string SuitableLocation
        {
            get { return _suitableLocation; }
            set { _suitableLocation = value; }
        }
        //规则描述
        private string _ruleDescription;
        public string RuleDescription
        {
            get { return _ruleDescription; }
            set { _ruleDescription = value; }
        }

        //指标类型
        string _indicatorType;//定性指标、定量指标

        public string IndicatorType
        {
            get { return _indicatorType; }
            set { _indicatorType = value; }
        }


        //比较符(数据库中读取出来的原始比较符字符串，多个比较符间用逗号分隔)
        private string _operator;

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        //修改日期
        private DateTime _modifyDate;

        public DateTime ModifyDate
        {
            get { return _modifyDate; }
            set { _modifyDate = value; }
        }

        //备注
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        // 绑定数据库表
        private string _bindingTableName;
        public string BindingTableName
        {
            get { return _bindingTableName; }
            set { _bindingTableName = value; }
        }

        // 绑定数据库表字段
        private string _bindingColumnName;
        public string BindingColumnName
        {
            get { return _bindingColumnName; }
            set { _bindingColumnName = value; }
        }

        // 字段使用方式
        private string _useType;
        public string UseType
        {
            get { return _useType; }
            set { _useType = value; }
        }

        //组合规则绑定的单一规则编码
        private string _strBindingSingleRuleName = null;
        public string StrBindingSingleRuleName
        {
            get { return _strBindingSingleRuleName; }
            set
            {
                if (value != "")
                {
                    _strBindingSingleRuleName = value;
                }
            }
        }

        //判断是否属于组合规则
        private bool _iIsMultiRules = false;
        public bool IsMultiRules
        {
            get { return _iIsMultiRules; }
            set { _iIsMultiRules = value; }
        }


        /// <summary>
        /// 返回组合规则中绑定的单一规则组成的数组
        /// </summary>
        /// <returns></returns>
        public string[] ListBindingSingleRuleName()
        {
            string[] strs = null;
            if (_strBindingSingleRuleName!=null)
            {
                strs = _strBindingSingleRuleName.Split(LibCommon.Const_WM.OPERATOR_SEPERATOR);
            }
            return strs;
        }


        /// <summary>
        /// 解析一条预警规则的规则描述，获取对应的参数名和参数值
        /// </summary>
        /// <param name="strSrc">规则描述</param>
        /// <returns>规则描述对应的参数名和参数值，返回值为null时，说明该规则为定性指标</returns>
        static public Hashtable ParseRuleDescriptionOfOneRuleId(string strSrc)
        {
            string[] paramResult = StringSplitor.ParseRuleDescriptionParams(strSrc, Const_WM.PARAM_START_SEPERATOR, Const_WM.PARAM_END_SEPERATOR);
            string[] valResult = StringSplitor.ParseRuleDescriptionParams(strSrc, Const_WM.VALUE_START_SEPERATOR, Const_WM.VALUE_END_SEPERATOR);

            if (paramResult != null && valResult != null)
            {
                if (paramResult.Length != valResult.Length)//参数和值不匹配！
                {
                    Alert.alert("解析预警规则参数和对应值出错:规则参数和值不匹配！");
                    return null;
                }
            }
            else
            {
                return null;
            }
            Hashtable htRet = new Hashtable();
            int n = paramResult.Length;
            for (int i = 0; i < n; i++)
            {
                htRet.Add(paramResult[i], valResult[i]);
            }
            return htRet;
        }
        /// <summary>
        /// 获取预警参数和对应的值
        /// </summary>
        /// <returns>没有值则返回null</returns>
        public Hashtable GetWarningParamsAndValues()
        {
            return ParseRuleDescriptionOfOneRuleId(RuleDescription);
        }

        /// <summary>
        /// 获取预警规则对应的标准值
        /// </summary>
        /// <returns></returns>
        public string GetStandardWarningValueStrs()
        {
            string ret = "";
            Hashtable ht = GetWarningParamsAndValues();
            //定量指标
            if (ht != null)
            {
                int cnt = ht.Count;
                int idx = 0;
                foreach (string par in ht.Keys)
                {
                    ret += ht[par].ToString();
                    if (idx != cnt - 1)
                    {
                        ret += Const_WM.OPERATOR_SEPERATOR;
                    }
                    idx++;
                }
            }
            else//定性指标
            {
                ret = "1";
            }
            return ret;
        }
        /// <summary>
        /// 获取规则编码和预警参数信息
        /// </summary>
        /// <returns></returns>
        public RuleInfo GetRuleCodeAndParamsInfo()
        {
            RuleInfo ret = new RuleInfo(this.RuleId);
            Hashtable htParam = ParseRuleDescriptionOfOneRuleId(RuleDescription);
            ret.PreWarningParams = htParam;
            return ret;
        }

        public WARNING_LEVEL_RESULT GetRuleWarningLevel()
        {
            WARNING_LEVEL_RESULT ret = WARNING_LEVEL_RESULT.GREEN;
            if (WarningLevel == Const_WM.WARNING_LEVEL_STR_RED)
            {
                ret = WARNING_LEVEL_RESULT.RED;
            }
            else if (WarningLevel == Const_WM.WARNING_LEVEL_STR_YELLOW)
            {
                ret = WARNING_LEVEL_RESULT.YELLOW;
            }
            else
            {
                Alert.alert("未定义预警级别：" + WarningLevel);
                ret = WARNING_LEVEL_RESULT.NODATA;
            }

            return ret;
        }
        /// <summary>
        /// 解析比较符字符串
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private string[] ParseOperatorString(string src)
        {
            List<string> ret = new List<string>();
            string[] sps = src.Split(Const_WM.GetRuleOperatorSeperator());
            if (sps != null)
            {
                for (int i = 0; i < sps.Length; i++)
                {
                    ret.Add(sps[i]);
                }
            }
            if (ret.Count > 0)
            {
                return ret.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 获取解析后的比较符
        /// </summary>
        /// <returns></returns>
        public string[] GetParsedOperators()
        {
            return ParseOperatorString(this.Operator);
        }


        /// <summary>
        /// 根据新的规则参数值更新规则描述。
        /// 注：由于不同预警规则参数个数等可能不一样，此函数只适用于同一预警规则(规则描述相同)！
        /// </summary>
        /// <param name="newParams"></param>
        /// <returns>更新成功返回true，否则返回false</returns>
        public bool UpdateRuleDescriptionByParams(Hashtable newParams)
        {
            Hashtable htOld = GetWarningParamsAndValues();
            if (htOld == null)//没有参数
            {
                return true;
            }
            if (newParams.Count != htOld.Count)
            {
                //此处仅对参数个数进行了判断，并未对Key的值进行判断。
                return false;
            }
            foreach (string k in htOld.Keys)
            {
                string oldStr = Const_WM.PARAM_START_SEPERATOR + k + Const_WM.PARAM_END_SEPERATOR + Const_WM.VALUE_START_SEPERATOR + htOld[k].ToString() + Const_WM.VALUE_END_SEPERATOR;
                string newStr = Const_WM.PARAM_START_SEPERATOR + k + Const_WM.PARAM_END_SEPERATOR + Const_WM.VALUE_START_SEPERATOR + newParams[k].ToString() + Const_WM.VALUE_END_SEPERATOR;
                RuleDescription = RuleDescription.Replace(oldStr, newStr);
            }
            return true;
        }

        /// <summary>
        /// 获取移除参数名及默认值括号等额外字符后的规则描述
        /// </summary>
        /// <returns></returns>
        public string GetRuleDescriptionWithoutAdditionalCharacter()
        {
            string ret = RuleDescription;
            char[] removeChars = new char[] 
            { 
                char.Parse(Const_WM.PARAM_START_SEPERATOR),
                char.Parse(Const_WM.PARAM_END_SEPERATOR),
                char.Parse(Const_WM.VALUE_START_SEPERATOR),
                char.Parse(Const_WM.VALUE_END_SEPERATOR),
            };
            Hashtable htParams = ParseRuleDescriptionOfOneRuleId(RuleDescription);
            if (htParams != null)//定量指标，存在参数
            {
                foreach (string p in htParams.Keys)
                {
                    string v = htParams[p].ToString();
                    string oldParam = Const_WM.PARAM_START_SEPERATOR + p + Const_WM.PARAM_END_SEPERATOR;
                    //string oldVal = Const_WM.VALUE_START_SEPERATOR + v + Const_WM.VALUE_END_SEPERATOR;
                    string empty = "";
                    ret = ret.Replace(oldParam, empty);
                    //参数值保留，只移除括号
                    ret = ret.Replace(Const_WM.VALUE_START_SEPERATOR, empty);
                    ret = ret.Replace(Const_WM.VALUE_END_SEPERATOR, empty);
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取移除参数名及默认值括号等额外字符后的规则描述加该规则对应的标准预警结果
        /// </summary>
        /// <returns></returns>
        public string GetRuleDescriptionWithoutAdditionalCharacterPlusResult()
        {
            string des = GetRuleDescriptionWithoutAdditionalCharacter();
            des += Const_WM.RULE_DESCRIPTION_PLUS_INFO;
            des += this.WarningLevel;
            return des;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public PreWarningRulesEntity DeepClone()
        {
            //MARK FIELD
            PreWarningRulesEntity cpyEnt = new PreWarningRulesEntity(this.RuleId);
            cpyEnt.RuleCode = RuleCode;
            cpyEnt.RuleType = RuleType;
            cpyEnt.WarningLevel = WarningLevel;
            cpyEnt.WarningType = WarningType;
            cpyEnt.SuitableLocation = SuitableLocation;
            cpyEnt.RuleDescription = RuleDescription;
            cpyEnt.IndicatorType = IndicatorType;//指标类型
            cpyEnt.ModifyDate = ModifyDate;
            cpyEnt.Remarks = Remarks;
            cpyEnt.Operator = Operator;
            return cpyEnt;
        }
    }
}
