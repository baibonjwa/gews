// ******************************************************************
// 概  述：预警结果解析实体
// 作  者：秦凯  
// 创建日期：2014/3/14
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// 存放一条混合规则  W[W1(ID1|V1,ID2|V2)/W2(ID1|V1,ID2|V2)];W1(-999|V1,ID2|V2);
    /// </summary>
    public class PreWarningAnalysisResultEnt
    {
        //复合规则编码
        private string _strMultiRuleCodeID = "";
        /// <summary>
        /// 获取、设置规则编码
        /// </summary>
        public string MultiRuleCodeID
        {
            get { return _strMultiRuleCodeID; }
            set { _strMultiRuleCodeID = value; }
        }

        //存放多条单一规则编码
        private List<SingleRuleResultEnt> entRuleValues = new List<SingleRuleResultEnt>();
        /// <summary>
        /// 设置、获取多条单一规则编码
        /// </summary>
        public List<SingleRuleResultEnt> RuleValues
        {
            get { return entRuleValues; }
            set { entRuleValues = value; }
        }

        /// <summary>
        /// 判断是否属于单一规则编码
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public bool IsSingleRule()
        {
            return this.MultiRuleCodeID == "" ? true : false;
        }

        /// <summary>
        /// 返回单一规则预警结果实体,注意检测返回值是否null
        /// </summary>
        /// <param name="ent">注意参数ent是否是null</param>
        /// <returns></returns>
        public SingleRuleResultEnt GetSingleRulesResultEnt()
        {
            SingleRuleResultEnt returnEnt = null;
            if (this.MultiRuleCodeID == "")
            {
                List<SingleRuleResultEnt> values = this.RuleValues;
                if (values.Count==1)
                {
                    returnEnt = values[0];
                }                
            }
            return returnEnt;
        }

        /// <summary>
        /// 转换复合规则为单一规则预警结果实体数组,注意检测返回值是否null
        /// </summary>
        /// <param name="ent">注意参数ent是否是null</param>
        /// <returns></returns>
        public List<SingleRuleResultEnt> parseMulti2SingleRules()
        {
            List<SingleRuleResultEnt> returnEnt = null;
            if (this.MultiRuleCodeID != "")
            {
                returnEnt = new List<SingleRuleResultEnt>();
                returnEnt = this.RuleValues;
            }
            return returnEnt;
        }
    }

    /// <summary>
    /// 存放一条单一预警规则  “单一预警规则”：W1(ID1|V1,ID2|V2);
    /// </summary>
    public class SingleRuleResultEnt
    {
        //规则编码
        private string _strSingleRuleCodeID = "";
        /// <summary>
        /// 获取、设置规则编码
        /// </summary>
        public string SingleRuleCodeID
        {
            get { return _strSingleRuleCodeID; }
            set { _strSingleRuleCodeID = value; }
        }

        //存放项与值
        private List<Pair> entValues = new List<Pair>();
        /// <summary>
        /// 获取设置项与值
        /// </summary>
        public List<Pair> EntValues
        {
            get { return entValues; }
            set { entValues = value; }
        }
    }

    /// <summary>
    /// 预警详细信息中，预警规则对应的预警结果的Id与Value：ID1|V1
    /// </summary>
    public class Pair
    {
        //项
        private string _strId = "";
        /// <summary>
        /// 导致预警的数据库记录ID
        /// </summary>
        public string RecordId
        {
            get { return _strId; }
            set { _strId = value; }
        }
        //值
        private string _strValue = "";
        /// <summary>
        /// 预警计算结果值
        /// </summary>
        public string Value
        {
            get { return _strValue; }
            set { _strValue = value; }
        }
    }
}
