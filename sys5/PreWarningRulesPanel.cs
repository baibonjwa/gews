// ******************************************************************
// 概  述：预警规则管理
// 作  者：杨小颖  
// 创建日期：2013/12/13
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;
using LibXPorperty;
using LibBusiness;
using System.Collections;
using LibCommon;

namespace _5.WarningManagement
{
    public partial class PreWarningRulesPanel : Form
    {
        //预警类型初始过滤条件
        private RULE_TYPE_WARNING_TYPE_FILTER _initWarningTypeFilter = RULE_TYPE_WARNING_TYPE_FILTER.ALL;
        private Color _inColor = Color.LightGreen;
        private Color _outColor = Color.White;
        //数据行数
        private int _dataCnt = 0;
        private const int DATA_START_IDX = 1;
        private int DATA_COLUMN_CNT = 9;//可用数据的列数 MARK FIELD

        private PreWarningRules _srcEnt = null;
        private XProps _props = new XProps();
        //规则ID
        XProp _ruleCodeId = new XProp();
        //规则编码
        XProp _ruleCodeProp = new XProp();
        //规则类别
        XProp _ruleType = new XProp();
        //预警类型
        XProp _warningType = new XProp();
        //预警级别
        XProp _warningLevel = new XProp();
        //适用位置
        XProp _suitableLocation = new XProp();
        XProp _paramCnt = new XProp();
        //指标类型
        XProp _indicatorType = new XProp();
        //比较符
        XProp _operator = new XProp();//MARK FIELD
        //修改日期
        XProp _dateProp = new XProp();
        List<XProp> _paramProp = new List<XProp>();
        //备注
        XProp _remarks = new XProp();
        //组合规则中绑定的单一规则
        XProp _multiRulesBindingSingleRule = new XProp();
        //该规则是否是组合规则
        XProp _IsMultiRules = new XProp();

        const string CATEGORY_BASICINFO_TITLE = "1.基本信息";
        const string CATEGORY_RULE_PARAMS_TITLE = "2.规则参数";
        const string CATEGORY_OPERATOR_TYPE_TITLE = "3.计算方法";
        const string CATEGORY_INDICATORR_TYPE_TITLE = "4.指标类型";
        const string CATEGORY_OTHERS_TITLE = "5.其他";
        const string CATEGORY_REMARKS_TITLE = "6.备注";
        const string CATEGORY_MULTIRULES_BINDING_SINGLERULE_TITLE = "6.组合规则";//MAKR FIELD

        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;

        /// <summary>
        /// 设置初始加载时预警类型过滤条件，Panel显示时会根据过滤条件自动加载相应的规则，默认加载全部规则
        /// </summary>
        /// <param name="initFilter">过滤条件</param>
        public void SetInitWarningTypeFilter(RULE_TYPE_WARNING_TYPE_FILTER initFilter)
        {
            _initWarningTypeFilter = initFilter;
        }

        void InitXPorps()
        {
            #region 基本信息
            _ruleCodeId.Category = CATEGORY_BASICINFO_TITLE;
            _ruleCodeId.Name = "ID";
            _ruleCodeId.Description = "预警规则ID";
            _ruleCodeId.ReadOnly = true;
            _ruleCodeId.ProType = typeof(int);
            _ruleCodeId.Visible = false;
            _ruleCodeId.Converter = null;
            _props.Add(_ruleCodeId);

            //规则编码
            _ruleCodeProp.Category = CATEGORY_BASICINFO_TITLE;
            _ruleCodeProp.Name = "规则编码";
            _ruleCodeProp.Description = "预警规则唯一标识，规则编码不能重复";
            _ruleCodeProp.ReadOnly = true;
            _ruleCodeProp.ProType = typeof(string);
            _ruleCodeProp.Visible = true;
            _ruleCodeProp.Converter = null;
            _props.Add(_ruleCodeProp);
            //规则类别
            _ruleType.Category = CATEGORY_BASICINFO_TITLE;
            _ruleType.Name = "规则类别";
            _ruleType.Description = "瓦斯、煤层赋存、通风、采掘影响、防突措施、日常预测、管理因素、其他";
            _ruleType.ReadOnly = true;
            _ruleType.ProType = typeof(string);
            _ruleType.Visible = true;
            _ruleType.Converter = null;
            _props.Add(_ruleType);
            //预警类型
            _warningType.Category = CATEGORY_BASICINFO_TITLE;
            _warningType.Name = "预警类型";
            _warningType.Description = "趋势预警、状态预警";
            _warningType.ReadOnly = true;
            _warningType.ProType = typeof(string);
            _warningType.Visible = true;
            _warningType.Converter = null;
            _props.Add(_warningType);

            //预警级别
            _warningLevel.Category = CATEGORY_BASICINFO_TITLE;
            _warningLevel.Name = "预警级别";
            _warningLevel.Description = "红色预警、黄色预警";
            _warningLevel.ReadOnly = true;
            _warningLevel.ProType = typeof(string);
            _warningLevel.Visible = true;
            _warningLevel.Converter = null;
            _props.Add(_warningLevel);

            //适用位置
            _suitableLocation.Category = CATEGORY_BASICINFO_TITLE;
            _suitableLocation.Name = "适用位置";
            _suitableLocation.Description = "掘进工作面、回采工作面、掘进和回采工作面、整个矿井";
            _suitableLocation.ReadOnly = true;
            _suitableLocation.ProType = typeof(string);
            _suitableLocation.Visible = true;
            _suitableLocation.Converter = null;
            _props.Add(_suitableLocation);
            #endregion

            #region 规则参数
            //参数个数
            _paramCnt.Category = CATEGORY_RULE_PARAMS_TITLE;
            _paramCnt.Name = "参数个数";
            _paramCnt.Description = "预警规则对应的参数个数";
            _paramCnt.ReadOnly = true;
            _paramCnt.ProType = typeof(string);
            _paramCnt.Visible = true;
            _paramCnt.Converter = null;
            _props.Add(_paramCnt);

            //比较符
            _operator.Category = CATEGORY_OPERATOR_TYPE_TITLE;//MARK FILED
            _operator.Name = "比较符";
            _operator.Description = "＞,≥,＜,≤,＝,≠；多个比较符用逗号分隔，比较符个数与参数一一对应";
            _operator.ReadOnly = true;
            _operator.ProType = typeof(string);
            _operator.Visible = true;
            _operator.Converter = null;
            _props.Add(_operator);
            #endregion

            #region 指标类型
            _indicatorType.Category = CATEGORY_INDICATORR_TYPE_TITLE;
            _indicatorType.Name = "指标类型";
            _indicatorType.Description = "定性指标、定量指标";
            _indicatorType.ReadOnly = true;
            _indicatorType.ProType = typeof(string);
            _indicatorType.Visible = true;
            _indicatorType.Converter = null;
            _props.Add(_indicatorType);
            #endregion

            #region 其他
            //修改时间
            _dateProp.Category = CATEGORY_OTHERS_TITLE;
            _dateProp.Name = "修改时间";
            _dateProp.Description = "预警规则最近修改时间";
            _dateProp.ReadOnly = true;
            _dateProp.ProType = typeof(string);
            _dateProp.Visible = true;
            _dateProp.Converter = null;
            _props.Add(_dateProp);

            //备注
            _remarks.Category = CATEGORY_OTHERS_TITLE;
            _remarks.Name = "备注";
            _remarks.Description = "备注信息";
            _remarks.ReadOnly = true;
            _remarks.ProType = typeof(string);
            _remarks.Visible = true;
            _remarks.Converter = null;
            _props.Add(_remarks);
            #endregion

            #region 组合规则中绑定的单一规则
            _IsMultiRules.Category = CATEGORY_MULTIRULES_BINDING_SINGLERULE_TITLE;
            _IsMultiRules.Name = "是否是组合规则";
            _IsMultiRules.Description = "该规则是否属于组合规则编码";
            _IsMultiRules.ReadOnly = true;
            _IsMultiRules.ProType = typeof(string);
            _IsMultiRules.Visible = true;
            _IsMultiRules.Converter = null;
            _props.Add(_IsMultiRules);

            _multiRulesBindingSingleRule.Category = CATEGORY_MULTIRULES_BINDING_SINGLERULE_TITLE;
            _multiRulesBindingSingleRule.Name = "相关规则";
            _multiRulesBindingSingleRule.Description = "组合规则中绑定的单一规则编码";
            _multiRulesBindingSingleRule.ReadOnly = true;
            _multiRulesBindingSingleRule.ProType = typeof(string);
            _multiRulesBindingSingleRule.Visible = true;
            _multiRulesBindingSingleRule.Converter = null;
            _props.Add(_multiRulesBindingSingleRule);
            #endregion
        }

        /// <summary>
        /// 初始化筛选条件，并将最后一个（整个矿井）设置为当前选中状态
        /// </summary>
        private void InitFilterCombo()
        {
            //适用位置
            string[] locations = Const_WM.GetSuitableLocations();
            this.cmbFilterLocation.Items.AddRange(locations);
            //初始时不设置过滤选项，否则会自动调用SelectedIndexChange函数，导致farpoint重复加载
            //this.cmbFilterLocation.SelectedIndex = locations.Length - 1;

            //预警类型
            string[] warningTypes = Const_WM.GetWarningTypeConstStrings();
            this.cmbFilterWarningType.Items.AddRange(warningTypes);
            this.cmbFilterWarningType.Items.Add(Const_WM.WARNING_TYPE_FILTER_ALL);
            //初始时不设置过滤选项，否则会自动调用SelectedIndexChange函数，导致farpoint重复加载
            //this.cmbFilterWarningType.SelectedIndex = warningTypes.Length;

            //使用状态
        }

        public PreWarningRulesPanel()
        {
            InitializeComponent();
            toolStripFilter.Enabled = false;
            toolStripFilter.Visible = false;
            InitFilterCombo();

            FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpRules, "", 1);

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                7,
                8,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpRules, _filterColunmIdxs);
            #endregion
        }

        #region 属性窗口操作
        /// <summary>
        /// 设置属性窗口值
        /// </summary>
        /// <param name="ent"></param>
        public void SetPropertyGridEnt(PreWarningRules ent)
        {
            if (ent == null)
            {
                return;
            }
            _srcEnt = ent.DeepClone();

            _ruleCodeId.Value = ent.RuleId;
            _ruleCodeProp.Value = ent.RuleCode;
            _ruleType.Value = ent.RuleType;
            _warningType.Value = ent.WarningType;
            _warningLevel.Value = ent.WarningLevel;
            _suitableLocation.Value = ent.SuitableLocation;
            _indicatorType.Value = ent.IndicatorType;
            _operator.Value = ent.Operator;
            _dateProp.Value = ent.ModifyDate.ToString();
            _remarks.Value = ent.Remarks;//MARK FIELD
            _multiRulesBindingSingleRule.Value = ent.StrBindingSingleRuleName == null ? "" : ent.StrBindingSingleRuleName;
            _IsMultiRules.Value = ent.IsMultiRules == true ? "是" : "否";
            Hashtable htParams = ent.GetWarningParamsAndValues();
            int nParams = 0;
            if (htParams != null)
            {
                nParams = htParams.Count;
            }
            _paramCnt.Value = nParams.ToString();
            //移除先前的参数
            int preCnt = _paramProp.Count;
            for (int i = 0; i < preCnt; i++)
            {
                _props.Remove(_paramProp[i]);
            }
            _paramProp.Clear();

            //规则参数
            if (htParams != null)
            {
                foreach (string tmpKey in htParams.Keys)
                {
                    XProp prop = new XProp();
                    prop.Category = CATEGORY_RULE_PARAMS_TITLE;
                    prop.Name = tmpKey;
                    prop.Description = "规则参数" + tmpKey;
                    prop.ReadOnly = false;
                    prop.ProType = typeof(string);
                    prop.Visible = true;
                    prop.Converter = null;
                    prop.Value = htParams[tmpKey].ToString();
                    _props.Add(prop);
                    _paramProp.Add(prop);
                }
            }

            propertyGridRules.SelectedObject = _props;
        }

        private void ClearPropertyGridSelEnt()
        {
            propertyGridRules.SelectedObject = null;
        }

        /// <summary>
        /// 获取属性窗口值
        /// </summary>
        /// <returns></returns>
        public PreWarningRules GetEntFromProperyGrid()
        {
            PreWarningRules ret = new PreWarningRules(Convert.ToInt32(_ruleCodeId.Value));
            ret.ModifyDate = Convert.ToDateTime(_dateProp.Value.ToString());
            ret.RuleCode = _ruleCodeProp.Value.ToString();
            ret.RuleType = _ruleType.Value.ToString();
            ret.SuitableLocation = _suitableLocation.Value.ToString();
            ret.WarningLevel = _warningLevel.Value.ToString();
            ret.WarningType = _warningType.Value.ToString();
            ret.IndicatorType = _indicatorType.Value.ToString();
            ret.Operator = _operator.Value.ToString();
            ret.Remarks = _remarks.Value.ToString();
            ret.StrBindingSingleRuleName = _multiRulesBindingSingleRule.Value != null ? _multiRulesBindingSingleRule.Value.ToString() : "";//MARK FIELD
            Hashtable htNewParams = new Hashtable();
            int n = _paramProp.Count;
            for (int i = 0; i < n; i++)
            {
                htNewParams.Add(_paramProp[i].Name, _paramProp[i].Value.ToString());
            }
            //根据参数值更新规则描述
            ret.RuleDescription = _srcEnt.RuleDescription;
            if (!ret.UpdateRuleDescriptionByParams(htNewParams))
            {
                Alert.alert("更新预警规则参数失败！");
            }
            return ret;
        }
        #endregion

        #region Farpoint操作
        public void SetFarpointRowValues(int rowIdx, PreWarningRules ent)
        {
            //规则编码
            fpRules.Sheets[0].Cells[rowIdx, 1].Text = ent.RuleCode;
            //规则类别
            fpRules.Sheets[0].Cells[rowIdx, 2].Text = ent.RuleType;
            //预警类型
            fpRules.Sheets[0].Cells[rowIdx, 3].Text = ent.WarningType;
            //预警级别
            fpRules.Sheets[0].Cells[rowIdx, 4].Text = ent.WarningLevel;
            //适用位置
            fpRules.Sheets[0].Cells[rowIdx, 5].Text = ent.SuitableLocation;
            //规则描述
            fpRules.Sheets[0].Cells[rowIdx, 6].Text = ent.RuleDescription;
            //指标类型
            fpRules.Sheets[0].Cells[rowIdx, 7].Text = ent.IndicatorType;
            //比较符
            fpRules.Sheets[0].Cells[rowIdx, 8].Text = ent.Operator;
            //修改日期
            fpRules.Sheets[0].Cells[rowIdx, 9].Text = ent.ModifyDate.ToString();
            //备注
            fpRules.Sheets[0].Cells[rowIdx, 10].Text = ent.Remarks;
            //规则ID（内部识别，隐藏列）
            fpRules.Sheets[0].Cells[rowIdx, 11].Text = ent.RuleId.ToString();
            //绑定的单一规则编码
            fpRules.Sheets[0].Cells[rowIdx, 12].Text = ent.StrBindingSingleRuleName;
        }

        /// <summary>
        /// 从Farpoint获取预警规则参数
        /// </summary>
        /// <param name="rowIdx">farpoint所选行，注：此处未考虑rowIdx越界与无值的情况！</param>
        /// <returns></returns>
        public PreWarningRules GetEntityFromFarpointRow(int rowIdx)
        {
            PreWarningRules ret = new PreWarningRules();
            try
            {
                ret.RuleCode = fpRules.Sheets[0].Cells[rowIdx, 1].Value.ToString();
                ret.RuleType = fpRules.Sheets[0].Cells[rowIdx, 2].Value.ToString();
                ret.WarningType = fpRules.Sheets[0].Cells[rowIdx, 3].Value.ToString();
                ret.WarningLevel = fpRules.Sheets[0].Cells[rowIdx, 4].Value.ToString();
                ret.SuitableLocation = fpRules.Sheets[0].Cells[rowIdx, 5].Value.ToString();
                ret.RuleDescription = fpRules.Sheets[0].Cells[rowIdx, 6].Value.ToString();
                ret.IndicatorType = fpRules.Sheets[0].Cells[rowIdx, 7].Value.ToString();
                ret.Operator = fpRules.Sheets[0].Cells[rowIdx, 8].Value.ToString();//MARK FIELD
                ret.ModifyDate = Convert.ToDateTime(fpRules.Sheets[0].Cells[rowIdx, 9].Value);
                if (fpRules.Sheets[0].Cells[rowIdx, 10].Value != null)
                {
                    ret.Remarks = fpRules.Sheets[0].Cells[rowIdx, 10].Value.ToString();
                }
                else
                {
                    ret.Remarks = "";
                }
                ret.RuleId = int.Parse(fpRules.Sheets[0].Cells[rowIdx, 11].Value.ToString());
                if (fpRules.Sheets[0].Cells[rowIdx, 12].Value != null)
                {
                    ret.IsMultiRules = true;
                    ret.StrBindingSingleRuleName = fpRules.Sheets[0].Cells[rowIdx, 12].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message + "in function:" + "GetEntityFromFarpointRow");
            }
            return ret;
        }
        #endregion

        public void ClearFarpointCells()
        {
            fpRules.Sheets[0].Rows.Remove(DATA_START_IDX, _dataCnt);
        }

        private bool FillFarpointCellsWithWarningTypeFilter(RULE_TYPE_WARNING_TYPE_FILTER warningType)
        {
            DataSet ds = null;
            ds = PreWarningRulesBLL.selectAllWarningRules();
            string filterStr = Const_WM.WARNING_TYPE_OUT_OF_LIMIT;
            //设置过滤条件
            switch (warningType)
            {
                case RULE_TYPE_WARNING_TYPE_FILTER.OUT_OF_LIMIT://超限
                    filterStr = Const_WM.WARNING_TYPE_OUT_OF_LIMIT;
                    break;
                case RULE_TYPE_WARNING_TYPE_FILTER.OUTBURST://突出
                    filterStr = Const_WM.WARNING_TYPE_GAS_OUTBURST;
                    break;
                case RULE_TYPE_WARNING_TYPE_FILTER.ALL:
                default:
                    filterStr = Const.ALL_STRING;
                    break;
            }

            bool ret = FillFarpointCellsWithDataset(ds);
            //const int warningTypeFilterIdx = 3;
            //this.fpRules.ActiveSheet.AutoFilterColumn(warningTypeFilterIdx, filterStr, 0);
            return ret;
        }

        /// <summary>
        /// 加载全部预警规则
        /// </summary>
        public void FillFarpointCellsWithAllRules()
        {
            FillFarpointCellsWithLocationFilter(RULE_TYPE_LOCATION_FILTER.WHOLE);
        }

        /// <summary>
        /// 根据筛选条件显示预警规则
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <returns>成功返回ture，否则false</returns>
        private bool FillFarpointCellsWithLocationFilter(RULE_TYPE_LOCATION_FILTER filter)
        {
            DataSet ds = null;
            switch (filter)
            {
                case RULE_TYPE_LOCATION_FILTER.JUE_JIN_RULES://掘进工作面预警规则
                    ds = PreWarningRulesBLL.selectJueJinWarningRules();
                    break;
                case RULE_TYPE_LOCATION_FILTER.HUI_CAI_RULES://回采工作面预警规则
                    ds = PreWarningRulesBLL.selectHuiCaiWarningRules();
                    break;
                case RULE_TYPE_LOCATION_FILTER.JUE_JIN_HUI_CAI_COMMON://掘进和回采通用规则
                    ds = PreWarningRulesBLL.selectJueJinHuiCaiCommonRules();
                    break;
                case RULE_TYPE_LOCATION_FILTER.OTHERS://其他地点
                    ds = PreWarningRulesBLL.selectOthersRules();
                    break;
                case RULE_TYPE_LOCATION_FILTER.WHOLE://整个矿井
                    ds = PreWarningRulesBLL.selectAllWarningRules();
                    break;
                default:
                    break;
            }

            return FillFarpointCellsWithDataset(ds);

            //return false;
        }

        /// <summary>
        /// 只显示巷道绑定/不绑定的预警规则
        /// </summary>
        /// <returns></returns>
        public void FillFarpointCellsWithTunnelBindingRules(int tunnelID, bool select)
        {
            RuleInfo[] all = PreWarningRulesBLL.GetTunnelBindingRuleIdsAndParams(tunnelID);
            if (all == null)
            {
                ClearSelectedRules();
                return;
            }
            List<RuleInfo> lstRules = all.ToList<RuleInfo>();

            //遍历Farpoint所有规则
            for (int i = DATA_START_IDX; i < fpRules.Sheets[0].Rows.Count; i++)
            {
                int curRuleInFp = int.Parse(fpRules.Sheets[0].Cells[i, 11].Value.ToString());
                fpRules.Sheets[0].Cells[i, 0].Value = false;
                for (int j = 0; j < lstRules.Count; j++)
                {
                    if (select)
                    {
                        if (lstRules[j].Id == curRuleInFp)
                        {
                            fpRules.Sheets[0].Cells[i, 0].Value = true;
                            PreWarningRules ruleEnt = GetEntityFromFarpointRow(i);//使用该方法速度较快（不用读数据库），不能用：PreWarningRulesBLL.GetPreWarningRulesEntityByRuleCode(curRuleInFp)，速度慢！
                            ruleEnt.UpdateRuleDescriptionByParams(lstRules[j].PreWarningParams);
                            SetFarpointRowValues(i, ruleEnt);
                            lstRules.Remove(lstRules[j]);
                        }
                        else
                        {
                            fpRules.Sheets[0].Rows[i].Remove();
                        }
                    }
                    else
                    {
                    }
                }
            }

        }

        private bool FillFarpointCellsWithDataset(DataSet ds)
        {
            if (ds == null)
            {
                return false;
            }
            int cnt = ds.Tables[0].Rows.Count;
            if (cnt < 1)
            {
                return false;
            }
            _dataCnt = cnt;
            //根据farpoint中的行数动态添加应该添加的行
            //int nAddRows = cnt - fpRules.Sheets[0].Rows.Count + DATA_START_IDX;
            //if (nAddRows > 0)
            //{
            //    fpRules.Sheets[0].Rows.Add(fpRules.Sheets[0].Rows.Count, nAddRows);
            //}
            //根据数据条数重新设置Farpoint单元格行数
            fpRules.Sheets[0].Rows.Count = _dataCnt + DATA_START_IDX;
            //MARK FIELD
            for (int i = 0; i < cnt; i++)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType chkBox = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //选择
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 0].CellType = chkBox;
                //规则编码
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 1].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.RULE_CODE].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 1].Locked = true;
                //规则类别
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 2].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.RULE_TYPE].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 2].Locked = true;
                //预警类型
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 3].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.WARNING_TYPE].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 3].Locked = true;
                //预警级别
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 4].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.WARNING_LEVEL].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 4].Locked = true;
                //适用位置
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 5].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.SUITABLE_LOCATION].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 5].Locked = true;
                //规则描述
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 6].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.RULE_DESCRIPTION].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 6].Locked = true;
                //指标类型
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 7].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.INDICATOR_TYPE].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 7].Locked = true;
                //比较符
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 8].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.OPERATOR].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 8].Locked = true;
                //修改日期
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 9].Text = Convert.ToDateTime(ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.MODIFY_DATE]).ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 9].Locked = true;
                //备注
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 10].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.REMARKS].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 10].Locked = true;
                //规则ID
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 11].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.RULE_ID].ToString();
                //绑定的单一规则
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 12].Text = ds.Tables[0].Rows[i][PreWarningRulesDbConstNames.BINDING_SINGLERULES].ToString();
                fpRules.Sheets[0].Cells[DATA_START_IDX + i, 12].Locked = true;
            }
            LibAutoResizeFarpoint.AutoFitWidthAndHeight(fpRules, DATA_START_IDX, 1);
            return true;
        }


        private void PreWarningRulesManagement_Load(object sender, EventArgs e)
        {
            DATA_COLUMN_CNT = fpRules.Sheets[0].ColumnCount;
            if (!FillFarpointCellsWithWarningTypeFilter(_initWarningTypeFilter))
            {
                Alert.alert("无数据！");
                return;
            }

            InitXPorps();

            this.fpRules.ActiveSheet.Columns[12].Visible = false;
        }

        /// <summary>
        /// 检验用户点击的单元格是否为有效单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool IsCellDataAvailabe(int row, int col)
        {
            if (row >= DATA_START_IDX && row <= (DATA_START_IDX + _dataCnt))
            {
                if (col >= 0 && col < DATA_COLUMN_CNT - 1)//隐藏列（规则ID）不计算在内
                {
                    return true;
                }
            }
            return false;
        }

        private void fpRules_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //判断单击的单元格所在的行是否是有效区域
            if (!IsCellDataAvailabe(e.Row, e.Column))
            {
                return;
            }
            PreWarningRules ent = GetEntityFromFarpointRow(e.Row);

            SetPropertyGridEnt(ent);
        }

        /// <summary>
        /// 检验用户在PropertyGrid中输入的值是否有效
        /// </summary>
        /// <param name="vl"></param>
        /// <returns></returns>
        bool IsPropertyGridValueOK(object vl)
        {
            double numVl = 0;
            return double.TryParse(vl.ToString(), out numVl);
        }

        private void propertyGridRules_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (!IsPropertyGridValueOK(e.ChangedItem.Value))
            {
                Alert.alert("输入的预警参数值无效，预警参数未更改！");
                PreWarningRules ent = GetEntityFromFarpointRow(fpRules.Sheets[0].GetSelection(0).Row);
                SetPropertyGridEnt(ent);
                return;
            }
            UpdateRuleParams2RuleTable();
        }

        public void ApplyParamsValues()
        {
            int n = _paramProp.Count;
            for (int i = 0; i < n; i++)
            {
                if (!IsPropertyGridValueOK(_paramProp[i].Value))
                {
                    Alert.alert("输入的预警参数值无效，预警参数未更改！");
                    PreWarningRules ent = GetEntityFromFarpointRow(fpRules.Sheets[0].GetSelection(0).Row);
                    SetPropertyGridEnt(ent);
                    return;
                }
                UpdateRuleParams2RuleTable();
            }
        }

        /// <summary>
        /// 更新规则参数至预警规则表
        /// </summary>
        private void UpdateRuleParams2RuleTable()
        {
            PreWarningRules newEnt = GetEntFromProperyGrid();
            newEnt.ModifyDate = DateTime.Now;
            SetFarpointRowValues(fpRules.Sheets[0].ActiveRow.Index, newEnt);
            //更新至预警规则数据库表
            if (!PreWarningRulesBLL.updateWarningRulesInfo(newEnt))
            {
                Alert.alert("更新预警规则至数据库失败！");
            }
        }

        /// <summary>
        /// 更新巷道绑定的规则编码和参数信息
        /// </summary>
        /// <param name="tunnelID"></param>
        /// <returns></returns>
        public bool UpdateTunnelBindingRuleCodeAndParamsInfo(int tunnelID)
        {

            //遍历Farpoint中所有规则编码,获取选中规则编码及参数信息
            List<RuleInfo> lstParam = new List<RuleInfo>();
            int nRows = fpRules.Sheets[0].Rows.Count;
            try
            {
                for (int i = DATA_START_IDX; i < nRows; i++)
                {
                    if (fpRules.Sheets[0].Cells[i, 0] == null)
                    {
                        break;
                    }
                    else
                    {
                        if (bool.Parse(fpRules.Sheets[0].Cells[i, 0].Value.ToString()) == true)
                        {
                            RuleInfo oneInfo = GetEntityFromFarpointRow(i).GetRuleCodeAndParamsInfo();
                            lstParam.Add(oneInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return PreWarningRulesBLL.UpdateRuleIdsAndParams2TunnelTable(tunnelID, lstParam.ToArray());
            //return false;
        }

        /// <summary>
        /// 全选/不选所有规则
        /// </summary>
        /// <param name="select">是否全选</param>
        public void SelectAllRuleCodes(bool select)
        {
            //遍历Farpoint所有规则
            int rows = fpRules.Sheets[0].Rows.Count;
            for (int i = DATA_START_IDX; i < rows; i++)
            {
                fpRules.Sheets[0].Cells[i, 0].Value = select;
            }
        }


        /// <summary>
        /// 清除的时候需要同时清除PropertyGrid中的内容
        /// </summary>
        public void ClearSelectedRules()
        {
            ClearPropertyGridSelEnt();
            //遍历Farpoint所有规则
            int rows = fpRules.Sheets[0].Rows.Count;
            for (int i = DATA_START_IDX; i < rows; i++)
            {
                if (fpRules.Sheets[0].Cells[i, 11].Value != null)
                {
                    string curRuleInFp = fpRules.Sheets[0].Cells[i, 1].Value.ToString();
                    fpRules.Sheets[0].Cells[i, 0].Value = false;
                }
                //PreWarningRulesEntity ruleEnt = GetEntityFromFarpointRow(recordIdx); //PreWarningRulesBLL.GetPreWarningRulesEntityByRuleCode(curRuleInFp);
                //SetFarpointRowValues(recordIdx, ruleEnt);
            }
        }

        /// <summary>
        /// 根据巷道ID设置巷道使用的预警规则，并根据巷道的参数更新规则参数
        /// </summary>
        /// <param name="tunnelID"></param>
        public void SetTunnelSelectedRuleIdsAndUpdateParams(int tunnelID)
        {
            RuleInfo[] all = PreWarningRulesBLL.GetTunnelBindingRuleIdsAndParams(tunnelID);
            if (all == null)
            {
                ClearSelectedRules();
                return;
            }
            List<RuleInfo> lstRules = all.ToList<RuleInfo>();

            //遍历Farpoint所有规则
            int rows = fpRules.Sheets[0].Rows.Count;
            for (int i = DATA_START_IDX; i < rows; i++)
            {
                int curRuleInFp = int.Parse(fpRules.Sheets[0].Cells[i, 11].Value.ToString());
                fpRules.Sheets[0].Cells[i, 0].Value = false;
                for (int j = 0; j < lstRules.Count; j++)
                {
                    if (lstRules[j].Id == curRuleInFp)
                    {
                        fpRules.Sheets[0].Cells[i, 0].Value = true;
                        PreWarningRules ruleEnt = GetEntityFromFarpointRow(i);//使用该方法速度较快（不用读数据库），不能用：PreWarningRulesBLL.GetPreWarningRulesEntityByRuleCode(curRuleInFp)，速度慢！
                        ruleEnt.UpdateRuleDescriptionByParams(lstRules[j].PreWarningParams);
                        SetFarpointRowValues(i, ruleEnt);
                        lstRules.Remove(lstRules[j]);
                        break;
                    }
                }
            }

        }

        public bool returnExportInfo(string fileName)
        {
            bool bResult = FileExport.fileExport(fpRules, true);
            return bResult;
        }

        private void cmbFilterLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            RULE_TYPE_LOCATION_FILTER location = ConvertSuitableLocationFilterStr2Enum(cmbFilterLocation.Text);
            //清空先前单元格中的内容
            ClearFarpointCells();
            FillFarpointCellsWithLocationFilter(location);
        }

        private void cmbFilterWarningType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空先前单元格中的内容
            ClearFarpointCells();
            FillFarpointCellsWithWarningTypeFilter(ConvertWarningTypeFilterStr2Enum(cmbFilterWarningType.Text));
        }

        /// <summary>
        /// 将适用地点过滤字符转为枚举类型
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public RULE_TYPE_LOCATION_FILTER ConvertSuitableLocationFilterStr2Enum(string src)
        {
            RULE_TYPE_LOCATION_FILTER ret = RULE_TYPE_LOCATION_FILTER.WHOLE;
            if (src == Const_WM.SUITABLE_LOCATION_HUI_CAI)//回采
            {
                ret = RULE_TYPE_LOCATION_FILTER.HUI_CAI_RULES;
            }
            else if (src == Const_WM.SUITABLE_LOCATION_JUE_JIN)//掘进
            {
                ret = RULE_TYPE_LOCATION_FILTER.JUE_JIN_RULES;
            }
            else if (src == Const_WM.SUITABLE_LOCATION_JUE_JIN_HUI_CAI_COMMON)//掘进和回采通用
            {
                ret = RULE_TYPE_LOCATION_FILTER.JUE_JIN_HUI_CAI_COMMON;
            }
            else if (src == Const_WM.SUITABLE_LOCATION_OTHERS)//其他地点
            {
                ret = RULE_TYPE_LOCATION_FILTER.OTHERS;
            }
            else if (src == Const_WM.SUITABLE_LOCATION_WHOLE)
            {
                ret = RULE_TYPE_LOCATION_FILTER.WHOLE;//整个矿井（全部）
            }
            return ret;
        }

        /// <summary>
        /// 将预警类型过滤字符转为枚举类型
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public RULE_TYPE_WARNING_TYPE_FILTER ConvertWarningTypeFilterStr2Enum(string src)
        {
            RULE_TYPE_WARNING_TYPE_FILTER ret = RULE_TYPE_WARNING_TYPE_FILTER.ALL;
            if (src == Const_WM.WARNING_TYPE_OUT_OF_LIMIT)
            {
                ret = RULE_TYPE_WARNING_TYPE_FILTER.OUT_OF_LIMIT;//超限
            }
            else if (src == Const_WM.WARNING_TYPE_GAS_OUTBURST)
            {
                ret = RULE_TYPE_WARNING_TYPE_FILTER.OUTBURST;
            }
            return ret;
        }

        public void PrintRules()
        {
            FilePrint.CommonPrint(this.fpRules, 0);
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpRules, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpRules, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpRules.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpRules, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpRules, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion
    }

    public enum RULE_TYPE_WARNING_TYPE_FILTER
    {
        OUT_OF_LIMIT,//超限预警
        OUTBURST,//突出预警
        ALL,//所有
    }

    public enum RULE_TYPE_LOCATION_FILTER
    {
        //按适用位置筛选
        JUE_JIN_RULES,//只显示适用于掘进面预警规则 (仅适用于掘进+掘进和回采通用）
        HUI_CAI_RULES,//只显示适用于回采面预警规则 (仅适用于回采+掘进和回采通用）
        JUE_JIN_HUI_CAI_COMMON,//掘进和回采通用预警规则
        OTHERS,//其他地点

        WHOLE,//所有预警规则
    }
}
