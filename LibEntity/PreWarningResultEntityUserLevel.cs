// ******************************************************************
// 概  述：预警结果实体（显示给用户的）
// 作  者：杨小颖  
// 创建日期：2014/02/15
// 版本号：1.0
// ******************************************************************
using System;
using LibCommon;
using System.Collections;
using System.Collections.Generic;


namespace LibEntity
{

    /// <summary>
    /// 预警结果实体
    /// 目前只存储预警规则实体，日后扩展时再添加
    /// </summary>
    public class PreWarningResultEntityUserLevel
    {
        /// <summary>
        /// 巷道ID
        /// </summary>
        private int _tunnelID;

        /// <summary>
        /// 巷道ID
        /// </summary>
        public int TunnelID
        {
            get { return _tunnelID; }
            set { _tunnelID = value; }
        }

        /// <summary>
        /// 预警日期
        /// </summary>
        private string _preWarningDate;

        /// <summary>
        /// 预警日期
        /// </summary>
        public string PreWarningDate
        {
            get { return _preWarningDate; }
            set { _preWarningDate = value; }
        }

        /// <summary>
        /// 预警时间
        /// </summary>
        private string _preWarningTime;

        /// <summary>
        /// 预警时间
        /// </summary>
        public string PreWarningTime
        {
            get { return _preWarningTime; }
            set { _preWarningTime = value; }
        }

        //综合预警结果
        private PreWarningResultEntityUserLevelUnit _comprehensiveResult = new PreWarningResultEntityUserLevelUnit();
        public PreWarningResultEntityUserLevelUnit ComprehensiveResult
        {
            get { return _comprehensiveResult; }
            set { _comprehensiveResult = value; }
        }

        //各项指标预警结果
        private List<PreWarningResultEntityUserLevelUnit> _lstWarningResultUserLevelUnit = new List<PreWarningResultEntityUserLevelUnit>();
        public List<PreWarningResultEntityUserLevelUnit> LstWarningResultUserLevelUnit
        {
            get { return _lstWarningResultUserLevelUnit; }
            set { _lstWarningResultUserLevelUnit = value; }
        }

        /// <summary>
        /// 将预警原因解析为显示给用户看的预警结果（单个） 
        /// </summary>
        /// <param name="htOutburst">包含各项规则类别的突出预警结果哈希表</param>
        /// <param name="htOutOfLimit">包含各项规则类别的超限预警结果哈希表</param>
        /// <param name="ruleType">规则类别</param>
        /// <returns>当前规则类别对应的超限和突出预警结果</returns>
        static private PreWarningResultEntityUserLevelUnit Parse2WarningResultUserLevelUnit(Hashtable htOutburst, Hashtable htOutOfLimit, string ruleType)
        {
            PreWarningResultEntityUserLevelUnit ret = new PreWarningResultEntityUserLevelUnit();
            //突出
            PreWarningReasonsByType reasonByTypeOutburst = (PreWarningReasonsByType)htOutburst[ruleType];
            //超限
            PreWarningReasonsByType reasonByTypeOutOfLimit = (PreWarningReasonsByType)htOutOfLimit[ruleType];
            ret = Convert2WarningResultUserLevelUnit(reasonByTypeOutburst, reasonByTypeOutOfLimit);
            //规则类别
            ret.RuleType = ruleType;
            return ret;
        }

        static private PreWarningResultEntityUserLevelUnit Convert2WarningResultUserLevelUnit(PreWarningReasonsByType reasonByTypeOutburst, PreWarningReasonsByType reasonByTypeOutOfLimit)
        {
            PreWarningResultEntityUserLevelUnit ret = new PreWarningResultEntityUserLevelUnit();
            //突出
            ret.OutburstResult = PreWarningResultEntityUserLevelUnit.ConvertWarningLevel2UserStr(reasonByTypeOutburst.WarningLevel);
            ret.OutburstNotes = PreWarningResultEntityUserLevelUnit.ConvertWarningDetails2UserStr(reasonByTypeOutburst.LstWarningResultDetails);
            //超限
            ret.OutOfLimitResult = PreWarningResultEntityUserLevelUnit.ConvertWarningLevel2UserStr(reasonByTypeOutOfLimit.WarningLevel);
            ret.OutOfLimitNotes = PreWarningResultEntityUserLevelUnit.ConvertWarningDetails2UserStr(reasonByTypeOutOfLimit.LstWarningResultDetails);
            return ret;
        }

        /// <summary>
        /// 最终所有的预警结果（显示给用户的）
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        static public PreWarningResultEntityUserLevel Convert2WarningResultUserLevel(PreWarningResultEntity[] results)
        {
            PreWarningResultEntityUserLevel ret = new PreWarningResultEntityUserLevel();

            if (results == null || results.Length <= 0)
            {
                return null;
            }

            //获取巷道ID
            int tunnelID = results[0].WarningDataCommonInfoEnt.TunnelID;
            ret.TunnelID = tunnelID;

            //瓦斯数据预警结果
            //地质构造数据预警结果
            //煤层赋存
            //通风
            //采掘影响
            //瓦斯数据预警结果
            //瓦斯数据预警结果


            //综合预警结果
            PreWarningReasonsByType comprehensiveResultOutOfLimit = new PreWarningReasonsByType();//超限综合预警结果
            PreWarningReasonsByType comprehensiveResultOutburst = new PreWarningReasonsByType();//突出综合预警结果
            comprehensiveResultOutOfLimit.WarningLevel = WARNING_LEVEL_RESULT.NODATA;
            comprehensiveResultOutburst.WarningLevel = WARNING_LEVEL_RESULT.NODATA;

            #region 各种规则类别的超限和突出预警结果
            Hashtable htResultsOutOfLimit = new Hashtable();//超限预警结果
            Hashtable htResultOutburst = new Hashtable();//突出预警结果         
            
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
                PreWarningResultEntity curPreWarningResultEnt = results[i];

                //获取规则编码对应的预警级别
                WARNING_LEVEL_RESULT curRulesEntWarningLevel = curPreWarningResultEnt.PreWarningRulesEntity.GetRuleWarningLevel();

                //单条预警原因
                PreWarningReasonUnit tmpWarningReasonUnit = new PreWarningReasonUnit();
                //获取最终显示给用户的规则描述
                tmpWarningReasonUnit.Notes = curPreWarningResultEnt.PreWarningRulesEntity.GetRuleDescriptionWithoutAdditionalCharacter();
                //在预警原因后面添加时间，以区分重复规则描述
                tmpWarningReasonUnit.Notes += "(";
                tmpWarningReasonUnit.Notes += curPreWarningResultEnt.WarningDataCommonInfoEnt.Date.ToString("yyyy/MM/dd hh:mm:ss");
                tmpWarningReasonUnit.Notes += ")";

                //超限预警
                if (curPreWarningResultEnt.PreWarningRulesEntity.WarningType == Const_WM.WARNING_TYPE_OUT_OF_LIMIT)
                {
                    //超限预警当前规则类别的预警原因
                    //引用类型实体改值会反馈至其他实体
                    PreWarningReasonsByType outOfLimitCurTypeWarningReason = ((PreWarningReasonsByType)htResultsOutOfLimit[curPreWarningResultEnt.PreWarningRulesEntity.RuleType]);

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
                    PreWarningReasonsByType outburstCurTypeWarningReason = ((PreWarningReasonsByType)htResultOutburst[curPreWarningResultEnt.PreWarningRulesEntity.RuleType]);

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
            //预警日期
            ret.PreWarningDate = DateTime.Now.ToString("yyyy/MM/dd");
            //各规则类别的预警结果
            foreach (string ruleType in htResultOutburst.Keys)
            {
              PreWarningResultEntityUserLevelUnit unit =  PreWarningResultEntityUserLevel.Parse2WarningResultUserLevelUnit(htResultOutburst, htResultsOutOfLimit, ruleType);
              ret.LstWarningResultUserLevelUnit.Add(unit);
            }  

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
                //finalOutburstStr += tblEntRet.PreWarningResultArr[i + 1].OutburstPreWarning;
                finalOutburstStr += ret.LstWarningResultUserLevelUnit[i].OutburstResult;
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
                finalOutOfLimitStr += ret.LstWarningResultUserLevelUnit[i].OutOfLimitResult;
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
            PreWarningResultEntityUserLevelUnit comprehensiveResult = new PreWarningResultEntityUserLevelUnit();
            comprehensiveResult.OutburstResult = PreWarningResultEntityUserLevelUnit.ConvertWarningLevel2UserStr(comprehensiveResultOutburst.WarningLevel);
            comprehensiveResult.OutburstNotes = finalOutburstStr;
            comprehensiveResult.OutOfLimitResult = PreWarningResultEntityUserLevelUnit.ConvertWarningLevel2UserStr(comprehensiveResultOutOfLimit.WarningLevel);
            comprehensiveResult.OutOfLimitNotes = finalOutOfLimitStr;
            comprehensiveResult.RuleType = "综合预警结果";//该文字并无大用
            ret.ComprehensiveResult = comprehensiveResult;

            //预警时间
            ret.PreWarningTime = DateTime.Now.ToString("hh:mm:ss");
            return ret;
        }
    }
}
