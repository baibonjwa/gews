// ******************************************************************
// 概  述：预警结果表实体
// 作  者：伍鑫
// 创建日期：2013/12/25
// 版本号：1.0
// ******************************************************************

using System;
using System.Collections;
using LibCommon;

namespace LibEntity
{
    public class PreWarningResultTable
    {
        /// <summary>
        ///     预警日期
        /// </summary>
        public string PreWarningDate { get; set; }

        /// <summary>
        ///     预警时间
        /// </summary>
        public string PreWarningTime { get; set; }

        /// <summary>
        ///     打印时间
        /// </summary>
        public string PrintTime { get; set; }

        /// <summary>
        ///     打印人
        /// </summary>
        public string PrintPerson { get; set; }

        /// <summary>
        ///     序号
        /// </summary>
        public string SequenceNumber { get; set; }

        /// <summary>
        ///     预警结果明细
        ///     数组大小应该为" + (Const_WM.GetRuleTypeConstStrings().Length+1);其中第一个元素为综合预警结果，其他元素为各规则类别对应的结果，顺序与界面中各指标类型顺序一致！
        /// </summary>
        public PreWarningResultTableUnit[] PreWarningResultArr { get; set; }


        public static PreWarningResultTable Convert2WarningResultTableEntity(PreWarningResult[] results)
        {
            var tblEntRet = new PreWarningResultTable();
            if (results == null || results.Length <= 0)
            {
                return null;
            }

            //瓦斯数据预警结果
            //地质构造数据预警结果
            //煤层赋存
            //通风
            //采掘影响
            //瓦斯数据预警结果
            //瓦斯数据预警结果


            //综合预警结果
            var comprehensiveResultOutOfLimit = new PreWarningReasonsByType(); //超限综合预警结果
            var comprehensiveResultOutburst = new PreWarningReasonsByType(); //突出综合预警结果
            comprehensiveResultOutOfLimit.WarningLevel = WARNING_LEVEL_RESULT.NODATA;
            comprehensiveResultOutburst.WarningLevel = WARNING_LEVEL_RESULT.NODATA;

            #region 各种规则类别的超限和突出预警结果

            var htResultsOutOfLimit = new Hashtable(); //超限预警结果
            var htResultOutburst = new Hashtable(); //突出预警结果         

            string[] types = Const_WM.GetRuleTypeConstStrings();
            int nTypes = types.Length;
            for (int i = 0; i < nTypes; i++)
            {
                htResultsOutOfLimit.Add(types[i], new PreWarningReasonsByType());
                htResultOutburst.Add(types[i], new PreWarningReasonsByType());
            }

            int nResultCnt = results.Length;
            for (int i = 0; i < nResultCnt; i++)
            {
                PreWarningResult curPreWarningResultEnt = results[i];

                //获取规则编码对应的预警级别
                WARNING_LEVEL_RESULT curRulesEntWarningLevel =
                    curPreWarningResultEnt.PreWarningRulesEntity.GetRuleWarningLevel();

                //单条预警原因
                var tmpWarningReasonUnit = new PreWarningReasonUnit();
                //获取最终显示给用户的规则描述
                tmpWarningReasonUnit.Notes =
                    curPreWarningResultEnt.PreWarningRulesEntity.GetRuleDescriptionWithoutAdditionalCharacter();
                //在预警原因后面添加时间，以区分重复规则描述
                tmpWarningReasonUnit.Notes += "(";
                tmpWarningReasonUnit.Notes +=
                    curPreWarningResultEnt.WarningDataCommonInfoEnt.Date.ToString("yyyy/MM/dd hh:mm:ss");
                tmpWarningReasonUnit.Notes += ")";

                //超限预警
                if (curPreWarningResultEnt.PreWarningRulesEntity.WarningType == Const_WM.WARNING_TYPE_OUT_OF_LIMIT)
                {
                    //超限预警当前规则类别的预警原因
                    //引用类型实体改值会反馈至其他实体
                    var outOfLimitCurTypeWarningReason =
                        ((PreWarningReasonsByType)
                            htResultsOutOfLimit[curPreWarningResultEnt.PreWarningRulesEntity.RuleType]);

                    //超限预警最终预警级别
                    if (outOfLimitCurTypeWarningReason.WarningLevel < curRulesEntWarningLevel)
                    {
                        outOfLimitCurTypeWarningReason.WarningLevel = curRulesEntWarningLevel;
                    }

                    if (comprehensiveResultOutOfLimit.WarningLevel < curRulesEntWarningLevel)
                    {
                        comprehensiveResultOutOfLimit.WarningLevel = curRulesEntWarningLevel;
                    }

                    tmpWarningReasonUnit.WarningLevelResult = curRulesEntWarningLevel;

                    //将预警原因添加至原因列表
                    outOfLimitCurTypeWarningReason.LstWarningResultDetails.Add(tmpWarningReasonUnit);
                }
                    //突出预警
                else if (results[i].PreWarningRulesEntity.WarningType == Const_WM.WARNING_TYPE_GAS_OUTBURST)
                {
                    //超限预警当前规则类别的预警原因
                    //引用类型实体改值会反馈至其他实体
                    var outburstCurTypeWarningReason =
                        ((PreWarningReasonsByType)
                            htResultOutburst[curPreWarningResultEnt.PreWarningRulesEntity.RuleType]);

                    //超限预警最终预警级别
                    if (outburstCurTypeWarningReason.WarningLevel < curRulesEntWarningLevel)
                    {
                        outburstCurTypeWarningReason.WarningLevel = curRulesEntWarningLevel;
                    }
                    if (comprehensiveResultOutburst.WarningLevel < curRulesEntWarningLevel)
                    {
                        comprehensiveResultOutburst.WarningLevel = curRulesEntWarningLevel;
                    }

                    tmpWarningReasonUnit.WarningLevelResult = curRulesEntWarningLevel;

                    //将预警原因添加至原因列表
                    outburstCurTypeWarningReason.LstWarningResultDetails.Add(tmpWarningReasonUnit);
                }
                else
                {
                    Alert.alert("未定义预警类型：" + results[i].PreWarningRulesEntity.WarningType);
                }
            }

            #endregion

            tblEntRet.PreWarningDate = DateTime.Now.ToString("yyyy/MM/dd");
            tblEntRet.PreWarningResultArr = new[]
            {
                Convert2WarningResultTablePart(comprehensiveResultOutburst, comprehensiveResultOutOfLimit), //综合预警结果
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_GAS), //瓦斯
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit,
                    Const_WM.RULE_TYPE_GEOLOGIC_STRUCTURE), //地质构造
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_COAL_OCURRENCE),
                //"煤层赋存";
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_VENTILATION),
                //"通风";
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_MINING_INFLUENCE),
                //"采掘影响";
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_SOLUTION),
                // "防突措施";
                //Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_PREDICTION),// "日常预测";
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_MANAGE_FACTOR),
                // "管理因素";
                Parse2WarningResultTablePart(htResultOutburst, htResultsOutOfLimit, Const_WM.RULE_TYPE_OTHERS) //"其他";
            };

            //综合预警结果
            string[] ruleTyps = Const_WM.GetRuleTypeConstStrings();
            int nTypesResult = ruleTyps.Length;
            string finalOutburstStr = "";
            string finalOutOfLimitStr = "";
            for (int i = 0; i < nTypesResult; i++)
            {
                //突出
                finalOutburstStr += ruleTyps[i];
                finalOutburstStr += Const_WM.WARNING_REASON_COLON;
                finalOutburstStr += tblEntRet.PreWarningResultArr[i + 1].OutburstPreWarning;
                if (i != nTypesResult - 1)
                {
                    finalOutburstStr += Const_WM.WARNING_REASON_SEPERATOR_SEMICOLON;
                    finalOutburstStr += Const_WM.WARNING_REASON_SEPERATOR_RETURN;
                }
                else
                {
                    finalOutburstStr += Const_WM.WARNING_REASON_END;
                }
                //超限
                finalOutOfLimitStr += ruleTyps[i];
                finalOutOfLimitStr += Const_WM.WARNING_REASON_COLON;
                finalOutOfLimitStr += tblEntRet.PreWarningResultArr[i + 1].UltralimitPreWarning;
                if (i != nTypesResult - 1)
                {
                    finalOutOfLimitStr += Const_WM.WARNING_REASON_SEPERATOR_SEMICOLON;
                    finalOutOfLimitStr += Const_WM.WARNING_REASON_SEPERATOR_RETURN;
                }
                else
                {
                    finalOutOfLimitStr += Const_WM.WARNING_REASON_END;
                }
            }

            //综合预警结果
            var comprehensiveResult = new PreWarningResultTableUnit();
            comprehensiveResult.OutburstPreWarning =
                PreWarningResultTableUnit.ConvertWarningLevel2UserStr(comprehensiveResultOutburst.WarningLevel);
            comprehensiveResult.OutburstPreWarningEX = finalOutburstStr;
            comprehensiveResult.UltralimitPreWarning =
                PreWarningResultTableUnit.ConvertWarningLevel2UserStr(comprehensiveResultOutOfLimit.WarningLevel);
            comprehensiveResult.UltralimitPreWarningEX = finalOutOfLimitStr;
            tblEntRet.PreWarningResultArr[0] = comprehensiveResult;

            tblEntRet.PreWarningTime = DateTime.Now.ToString("hh:mm:ss");
            return tblEntRet;
        }

        private static PreWarningResultTableUnit Parse2WarningResultTablePart(Hashtable htOutburst,
            Hashtable htOutOfLimit, string ruleType)
        {
            var ret = new PreWarningResultTableUnit();
            //突出
            var reasonByTypeOutburst = (PreWarningReasonsByType) htOutburst[ruleType];
            //超限
            var reasonByTypeOutOfLimit = (PreWarningReasonsByType) htOutOfLimit[ruleType];
            ret = Convert2WarningResultTablePart(reasonByTypeOutburst, reasonByTypeOutOfLimit);
            return ret;
        }

        private static PreWarningResultTableUnit Convert2WarningResultTablePart(
            PreWarningReasonsByType reasonByTypeOutburst, PreWarningReasonsByType reasonByTypeOutOfLimit)
        {
            var ret = new PreWarningResultTableUnit();
            //突出
            ret.OutburstPreWarning =
                PreWarningResultTableUnit.ConvertWarningLevel2UserStr(reasonByTypeOutburst.WarningLevel);
            int nCntOutBurst = reasonByTypeOutburst.LstWarningResultDetails.Count;
            ret.OutburstPreWarningEX =
                PreWarningResultTableUnit.ConvertWarningDetails2UserStr(
                    reasonByTypeOutburst.LstWarningResultDetails);
            //超限
            ret.UltralimitPreWarning =
                PreWarningResultTableUnit.ConvertWarningLevel2UserStr(reasonByTypeOutOfLimit.WarningLevel);
            ret.UltralimitPreWarningEX =
                PreWarningResultTableUnit.ConvertWarningDetails2UserStr(
                    reasonByTypeOutOfLimit.LstWarningResultDetails);
            return ret;
        }
    }
}