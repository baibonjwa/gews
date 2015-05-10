// ******************************************************************
// 概  述：预警结果解析结果数据逻辑
// 作  者：秦凯
// 创建日期：2014/03/16
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;

namespace LibBusiness
{
    public class PreWarningAnalysisResultBLL
    {
        //public static List<PreWarningAnalysisResult> 
        //    AnalysisPreWarningResult(string strPreWarningResult)
        //{            
        //    if (strPreWarningResult=="")
        //    {
        //        return null;
        //    }
        //    List<PreWarningAnalysisResult> MultiRuleEnts = new 
        //        List<PreWarningAnalysisResult>();
        //    string[] values = 
        //        strPreWarningResult.Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_MULTI);
        //    foreach (string value in values)
        //    {
        //         //包含“[”时，属于混合预警规则
        //        if 
        //            (value.Contains(LibCommon.Const_WM.PARAM_START_SEPERATOR))
        //        {
        //            //拆取复合编码的名称 multiValueNameAndValue[0]
        //            string[] multiValueNameAndValue = 
        //                value.Split(LibCommon.Const_WM.PARAM_START_SEPERATOR.ToCharArray()[0]);
        //            if (multiValueNameAndValue.Length == 2)
        //            {
        //                string[] multiValue = 
        //                    LibCommon.StringSplitor.ParseRuleDescriptionParams(value,
        //                    LibCommon.Const_WM.PARAM_START_SEPERATOR, 
        //                    LibCommon.Const_WM.PARAM_END_SEPERATOR);
                        
        //                //存放多条单一规则编码
        //                List<SingleRuleResultEnt> singleEnts = new 
        //                    List<SingleRuleResultEnt>();

        //                //      W1(ID1|V1,ID2|V2)/W2(ID1|V1,ID2|V2)]
        //                //      以“/”拆分，获得组合编码中的单一编码
        //                string[] strs = 
        //                    multiValue[0].Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_COMPLEX);
        //                foreach (string str in strs)
        //                {
        //                    //W1(ID1|V1,ID2|V2)
        //                    if 
        //                        (str.Contains(LibCommon.Const_WM.VALUE_START_SEPERATOR))
        //                    {
        //                        //单一编码名称rules[0]
        //                        string[] rules = 
        //                            str.Split(LibCommon.Const_WM.VALUE_START_SEPERATOR.ToCharArray()[0]);
        //                        if (rules.Length != 2)
        //                        {
        //                            continue;
        //                        }
        //                        string[] reasons = 
        //                            LibCommon.StringSplitor.ParseRuleDescriptionParams(str,
        //                            LibCommon.Const_WM.VALUE_START_SEPERATOR, 
        //                            LibCommon.Const_WM.VALUE_END_SEPERATOR);
        //                        if (reasons.Length > 0)
        //                        {
        //                            //定义存放原因与值的数组
        //                            List<Pair> valueEnts = new 
        //                                List<Pair>();
        //                            // 原因 打包  valueEnts
        //                            //以","拆分,拆分出不同的原因
        //                            string[] reasonMulti = 
        //                                reasons[0].Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_SINGLE);

        //                            //清空，避免重复添加
        //                            valueEnts.Clear();
        //                            foreach (string reasonOne in 
        //                                reasonMulti)
        //                            {
        //                                //拆分原因与值，拆分符"|"
        //                                string[] strReasonAndResult = 
        //                                    reasonOne.Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_REASONS);
        //                                if (strReasonAndResult.Length == 2)
        //                                {
        //                                    //定义原因与值实体，赋值后，存放于数组valueEnts
        //                                    Pair valueEnt = new Pair();
        //                                    valueEnt.RecordId = 
        //                                        strReasonAndResult[0];
        //                                    valueEnt.Value = 
        //                                        strReasonAndResult[1];
        //                                    valueEnts.Add(valueEnt);
        //                                }
        //                            }

        //                            //单条预警规则拆分完毕，包含预警规则编码，原因与值的数组
        //                            SingleRuleResultEnt singleEnt = new 
        //                                SingleRuleResultEnt();
        //                            singleEnt.SingleRuleCodeID = rules[0];
        //                            singleEnt.EntValues = valueEnts;

        //                            //存放一条单一规则进入数组
        //                            singleEnts.Add(singleEnt);
        //                        }

        //                    }
        //                }
        //                //单条规则,将单条规则亦写成数组形式，便于与复合规则统一：array[0]
        //                PreWarningAnalysisResult MultiRuleEnt = new 
        //                    PreWarningAnalysisResult();
        //                MultiRuleEnt.MultiRuleCodeID = 
        //                    multiValueNameAndValue[0];
        //                MultiRuleEnt.RuleValues = singleEnts;

        //                //存放多条组合编码
        //                MultiRuleEnts.Add(MultiRuleEnt);
        //            }//endif检查是否是符合条件的规则编码

        //            continue;
        //        }//endIf检查编码类型

        //        //包含“（”时，属于单条预警规则
        //        if 
        //            (value.Contains(LibCommon.Const_WM.VALUE_START_SEPERATOR))
        //        {
        //            string[] rules = 
        //                value.Split(LibCommon.Const_WM.VALUE_START_SEPERATOR.ToCharArray()[0]);
        //            if (rules.Length == 2)
        //            {
        //                //存放多条单一规则编码
        //                List<SingleRuleResultEnt> singleEnts = new 
        //                    List<SingleRuleResultEnt>();

        //                string[] reasons = 
        //                    LibCommon.StringSplitor.ParseRuleDescriptionParams(value,
        //                    LibCommon.Const_WM.VALUE_START_SEPERATOR, 
        //                    LibCommon.Const_WM.VALUE_END_SEPERATOR);
        //                if (reasons.Length > 0)
        //                {
        //                    //以","拆分,拆分出不同的原因
        //                    string[] reasonMulti = 
        //                        reasons[0].Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_SINGLE);

        //                    //定义存放原因与值的数组
        //                    List<Pair> valueEnts = new List<Pair>();
        //                    foreach (string reasonOne in reasonMulti)
        //                    {
        //                        //拆分原因与值，拆分符"|"
        //                        string[] strReasonAndResult = 
        //                            reasonOne.Split(LibCommon.Const_WM.PRE_WARNING_RULE_CODE_AND_PARAMS_SEPERATOR_REASONS);
        //                        if (strReasonAndResult.Length == 2)
        //                        {
        //                            //定义原因与值实体，赋值后，存放于数组valueEnts
        //                            Pair valueEnt = new Pair();
        //                            valueEnt.RecordId = 
        //                                strReasonAndResult[0];
        //                            valueEnt.Value = strReasonAndResult[1];
        //                            valueEnts.Add(valueEnt);
        //                        }
        //                    }

        //                    //单条预警规则拆分完毕，包含预警规则编码，原因与值的数组
        //                    SingleRuleResultEnt singleEnt = new 
        //                        SingleRuleResultEnt();
        //                    singleEnt.SingleRuleCodeID = rules[0];
        //                    singleEnt.EntValues = valueEnts;

        //                    //存放一条单一规则进入数组
        //                    singleEnts.Add(singleEnt);

        //                    //单条规则,将单条规则亦写成数组形式，便于与复合规则统一：array[0]
        //                    PreWarningAnalysisResult MultiRuleEnt = new 
        //                        PreWarningAnalysisResult();
        //                    MultiRuleEnt.MultiRuleCodeID = "";
        //                    MultiRuleEnt.RuleValues = singleEnts;

        //                    //存放多条组合编码
        //                    MultiRuleEnts.Add(MultiRuleEnt);
        //                }
        //            }
        //        }  
        //    }//EndForeach
        //    return MultiRuleEnts;
        //}
    }
}
