// ******************************************************************
// 概  述：预警信息结果查询
// 作  者：秦凯
// 创建日期：2014/03/23
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibEntity;
using LibCommon;
using LibCommonControl;
using System.Drawing;

namespace _5.WarningManagement
{
    public partial class PreWarningResultQuery : Form
    {
        // Summary of the query, 每个班次的查询总结
        FarPoint.Win.Spread.Cells summaryCells = null;
        // 每个总结的简要信息
        FarPoint.Win.Spread.Cells infoCells = null;
        //详细信息的数据标题行
        const int _iDetailsRowHeaderCount = 2;

        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;

        // 传出值
        private string sOutWorkface = null; // 工作面名称
        private string sOutTunnelId = null; // Tunnel Id
        private string sOutDate = null; // 日期
        private string sOutShift = null; // 班次
        private string sOutWarningResult = null; // 红色/黄色/绿色
        private string sOutWarningType = null; // 突出/超限
        private string sOutWarningItem = null; // 瓦斯/地质构造/通风/管理/煤层赋存

        //数据列数
        const int COLUMN_COUNT = 18;
        //Mark By QinKai
        #region 数据列索引
        const int COLUMN_TUNNEL_NAME = 0;
        const int COLUMN_DATE_TIME = 1;
        const int COLUMN_DATE_SHIFT = 2;
        const int COLUMN_WARNING_RESULT_OVERLIMIT = 3;
        const int COLUMN_WARNING_RESULT_OUTBURST = 4;
        const int _iOverLimitGas = 5;
        const int _iOverLimitCoal = 6;
        const int _iOverLimitGeology = 7;
        const int _iOverLimitVentilation = 8;
        const int _iOverLimitManagement = 9;
        const int _iOverLimitOther = 10;
        const int _iOutBurstGas = 11;
        const int _iOutBurstCoal = 12;
        const int _iOutBurstGeology = 13;
        const int _iOutBurstVentilation = 14;
        const int _iOutBurstManagement = 15;
        const int _iOutBurstOther = 16;

        const int COLUMN_TUNNEL_ID = 17;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public PreWarningResultQuery()
        {
            InitializeComponent();
            summaryCells = this._fpTunelInfo.ActiveSheet.Cells;
            infoCells = this._fpPreWarningDetials.ActiveSheet.Cells;

            //定义Farpoint列数
            this._fpTunelInfo.ActiveSheet.ColumnCount = COLUMN_COUNT;
            //设置窗体格式和窗体名称
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.Const_WM.PREWARNING_RESULT_QUERY);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                0,
                1,
                2
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this._fpTunelInfo, _filterColunmIdxs);
            #endregion
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this._fpTunelInfo, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this._fpTunelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this._fpTunelInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this._fpTunelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this._fpTunelInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion

        /// <summary>
        /// 调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            LoadTunelInformation();
        }

        /// <summary>
        /// 窗体登陆时加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreWarningResultQuery_Load(object sender, EventArgs e)
        {
            #region 设定默认的时间
            this._dtpStartTime.ValueChanged -= this._dtpStartTime_ValueChanged;
            this._dtpEndTime.ValueChanged -= this._dtpStartTime_ValueChanged;
            _dtpEndTime.Value = DateTime.Now;
            _dtpStartTime.Value = DateTime.Now.AddDays(-1);
            this._dtpStartTime.ValueChanged += this._dtpStartTime_ValueChanged;
            this._dtpEndTime.ValueChanged += this._dtpStartTime_ValueChanged;
            #endregion

            //加载预警类型
            //AddWarningType();
            var ds = WorkingFaceBLL.selectAllWorkingFace();
            _cbxSelWorkSurface.Items.Add("全部");
            _cbxSelWorkSurface.SelectedIndex = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                _cbxSelWorkSurface.Items.Add(ds.Tables[0].Rows[i][WorkingFaceDbConstNames.WORKINGFACE_NAME]);
            }

            //设置farpoint显示行列数
            this._fpTunelInfo.ActiveSheet.RowCount = 0;
            for (int i = 5; i < COLUMN_COUNT; i++)
            {
                this._fpTunelInfo.ActiveSheet.Columns[i].Visible = false;
            }

            //Mark By QinKai，若添加新的预警依据项，需要更改的内容
            //加载预警依据的类别
            string[] WaringItemTypeNames = Enum.GetNames(typeof(LibCommon.WarningReasonItems));
            cbWarningType.SelectedIndex = 0;

            for (int index = 0; index < WaringItemTypeNames.Length; index++)
            {
                infoCells[_iDetailsRowHeaderCount + index, 0].Text = WaringItemTypeNames[index];
                infoCells[WaringItemTypeNames.Length + _iDetailsRowHeaderCount * 2 + index, 0].Text = WaringItemTypeNames[index];
            }
        }


        /// <summary>
        /// 查询符合时间段的预警信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            //获取选择的时间范围
            string dateStartYear = _dtpStartTime.Value.Year.ToString();
            string dateStartMonth = _dtpStartTime.Value.Month < 10 ? "0" + _dtpStartTime.Value.Month.ToString() : _dtpStartTime.Value.Month.ToString();
            string dateStartDay = _dtpStartTime.Value.Day < 10 ? "0" + _dtpStartTime.Value.Day.ToString() : _dtpStartTime.Value.Day.ToString();
            string dateEndYear = _dtpEndTime.Value.Year.ToString();
            string dateEndMonth = _dtpEndTime.Value.Month < 10 ? "0" + _dtpEndTime.Value.Month.ToString() : _dtpEndTime.Value.Month.ToString();
            string dateEndDay = _dtpEndTime.Value.Day < 10 ? "0" + _dtpEndTime.Value.Day.ToString() : _dtpEndTime.Value.Day.ToString();

            //自行转换成数据库中合适的类型
            string dateStart = dateStartYear + "-" + dateStartMonth + "-" + dateStartDay + " 00:00:00";
            string dateEnd = dateEndYear + "-" + dateEndMonth + "-" + dateEndDay + " 23:59:59";
            //根据日期查询结果
            //if (_cbxSelWorkSurface.SelectedItem != null)
            //{
            string workingFace = _cbxSelWorkSurface.SelectedItem.ToString() == "全部"
                ? ""
                : _cbxSelWorkSurface.SelectedItem.ToString();
            PreWarningResultQueryBLL.PreWarningResultSort(dateStart, dateEnd, _tsProgressBar, workingFace, cbWarningType.SelectedItem.ToString());
            //调取填充Farpoint的事件
            LoadTunelInformation();
            //}
            //else
            //{
            //    Alert.alert("请先选择工作面");
            //}



        }

        /// <summary>
        /// 控制切换右侧详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fpTunelInfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int rowIndex = 0;
            if (e != null)
            {
                rowIndex = e.Range.Row;
            }

            //巷道名称
            _txtWorkingFaceName.Text = summaryCells[rowIndex, 0].Text.ToString();
            //日期班次
            _txtPreWarningDateAndShift.Text = summaryCells[rowIndex, 1].Text.ToString() + " " + summaryCells[rowIndex, 2].Text.ToString();
            try
            {
                //超限预警
                _picOverLimitResult.Image = (Image)summaryCells[rowIndex, 3].Value;
                //突出预警
                _picOurburstResult.Image = (Image)summaryCells[rowIndex, 4].Value;

                FarPoint.Win.Spread.CellType.ImageCellType imageCell = new FarPoint.Win.Spread.CellType.ImageCellType();
                imageCell.Style = FarPoint.Win.RenderStyle.Normal;

                //Mark By QinKai，若添加新的预警依据项，需要更改的内容
                FarPoint.Win.Spread.CellType.ButtonCellType buttonCell = new FarPoint.Win.Spread.CellType.ButtonCellType();
                buttonCell.Text = "详细信息";
                //超限预警
                //瓦斯
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].Value = summaryCells[rowIndex, _iOverLimitGas].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 2].CellType = buttonCell;
                //煤层
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].Value = summaryCells[rowIndex, _iOverLimitCoal].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 2].CellType = buttonCell;
                //地质
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].Value = summaryCells[rowIndex, _iOverLimitGeology].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 2].CellType = buttonCell;
                //通风
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].Value = summaryCells[rowIndex, _iOverLimitVentilation].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 2].CellType = buttonCell;
                //管理
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].Value = summaryCells[rowIndex, _iOverLimitManagement].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 2].CellType = buttonCell;
                //其他
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 1].Value = summaryCells[rowIndex, _iOverLimitOther].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 2].CellType = buttonCell;

                //预警依据类型的数量
                int warningItemTypeCount = Enum.GetNames(typeof(LibCommon.WarningReasonItems)).Length;
                //超限预警
                //瓦斯
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].Value = (summaryCells[rowIndex, _iOutBurstGas]).Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 2].CellType = buttonCell;
                //煤层
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].Value = summaryCells[rowIndex, _iOutBurstCoal].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 2].CellType = buttonCell;
                //地质
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].Value = summaryCells[rowIndex, _iOutBurstGeology].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 2].CellType = buttonCell;
                //通风
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].Value = summaryCells[rowIndex, _iOutBurstVentilation].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 2].CellType = buttonCell;
                //管理
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].Value = summaryCells[rowIndex, _iOutBurstManagement].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 2].CellType = buttonCell;
                //其他
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 1].CellType = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 1].Value = summaryCells[rowIndex, _iOutBurstOther].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 2].CellType = buttonCell;
            }
            catch (System.Exception ex)
            {
                Alert.alert(ex.ToString());
            }

            //设置传出值
            //工作面名称
            sOutWorkface = _txtWorkingFaceName.Text;
            //日期
            sOutDate = summaryCells[rowIndex, 1].Text.ToString();
            //班次                   
            sOutShift = summaryCells[rowIndex, 2].Text.ToString();
            //巷道ID                 
            sOutTunnelId = summaryCells[rowIndex, COLUMN_TUNNEL_ID].Text.ToString();
        }

        /// <summary>
        /// 加载巷道信息
        /// </summary>
        private void LoadTunelInformation()
        {
            //设置控件是否启用
            dataPager1.Enabled = true;

            //实例化分页控件
            //维护查询结果的记录
            //List<PreWarningHistoryResultEnt> historyResultEnt = PreWarningResultQueryBLL.GetSortedPreWarningData();
            //int iRecordCount = historyResultEnt == null ? 0 : historyResultEnt.Count;
            int iRecordCount = PreWarningResultQueryBLL.GetPreWarningDataCount();

            dataPager1.PageControlInit(iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            //根据分页控件选择数据
            List<PreWarningHistoryResultEnt> _ents = PreWarningResultQueryBLL.GetSortedPreWarningData(iStartIndex, iEndIndex);
            #region 删除垃圾数据
            while (_fpTunelInfo.ActiveSheet.Rows.Count > 0)
            {
                _fpTunelInfo.ActiveSheet.Rows.Remove(0, 1);
            }
            #endregion
            if (_ents == null)
            {
                return;
            }
            int iSelCnt = _ents.Count;

            #region 删除垃圾数据
            while (_fpTunelInfo.ActiveSheet.Rows.Count > 0)
            {
                _fpTunelInfo.ActiveSheet.Rows.Remove(0, 1);
            }
            #endregion

            for (int i = 0; i < iSelCnt; i++)
            {
                //添加新的行
                this._fpTunelInfo.ActiveSheet.Rows.Add(i, 1);
                this._fpTunelInfo.ActiveSheet.Rows[i].Height = 30;
                this._fpTunelInfo.ActiveSheet.Rows[i].Locked = true;

                //巷道名称
                summaryCells[i, COLUMN_TUNNEL_NAME].Value = _ents[i].TunelName;
                summaryCells[i, COLUMN_TUNNEL_NAME].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                summaryCells[i, COLUMN_TUNNEL_NAME].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //日期
                summaryCells[i, COLUMN_DATE_TIME].Value = _ents[i].DateTime.Split(' ')[0];
                summaryCells[i, COLUMN_DATE_TIME].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                summaryCells[i, COLUMN_DATE_TIME].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //班次
                summaryCells[i, COLUMN_DATE_SHIFT].Value = _ents[i].Date_Shift;
                summaryCells[i, COLUMN_DATE_SHIFT].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                summaryCells[i, COLUMN_DATE_SHIFT].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //超限预警
                FpUtil.setCellImg(summaryCells[i, COLUMN_WARNING_RESULT_OVERLIMIT], _ents[i].OverLimitWarningResult.WarningResult);
                //突出预警
                FpUtil.setCellImg(summaryCells[i, COLUMN_WARNING_RESULT_OUTBURST], _ents[i].OutBrustWarningResult.WarningResult);

                //瓦斯
                FpUtil.setCellImg(summaryCells[i, _iOverLimitGas], _ents[i].OverLimitWarningResult.Gas);
                //煤层
                FpUtil.setCellImg(summaryCells[i, _iOverLimitCoal], _ents[i].OverLimitWarningResult.Coal);
                //地质
                FpUtil.setCellImg(summaryCells[i, _iOverLimitGeology], _ents[i].OverLimitWarningResult.Geology);
                //通风
                FpUtil.setCellImg(summaryCells[i, _iOverLimitVentilation], _ents[i].OverLimitWarningResult.Ventilation);
                //管理
                FpUtil.setCellImg(summaryCells[i, _iOverLimitManagement], _ents[i].OverLimitWarningResult.Management);
                //其他
                FpUtil.setCellImg(summaryCells[i, _iOverLimitManagement], _ents[i].OverLimitWarningResult.Other);

                //瓦斯
                FpUtil.setCellImg(summaryCells[i, _iOutBurstGas], _ents[i].OutBrustWarningResult.Gas);
                //煤层
                FpUtil.setCellImg(summaryCells[i, _iOutBurstCoal], _ents[i].OutBrustWarningResult.Coal);
                //地质
                FpUtil.setCellImg(summaryCells[i, _iOutBurstGeology], _ents[i].OutBrustWarningResult.Geology);
                //通风
                FpUtil.setCellImg(summaryCells[i, _iOutBurstVentilation], _ents[i].OutBrustWarningResult.Ventilation);
                //管理
                FpUtil.setCellImg(summaryCells[i, _iOutBurstManagement], _ents[i].OutBrustWarningResult.Management);
                //其他
                FpUtil.setCellImg(summaryCells[i, _iOverLimitManagement], _ents[i].OutBrustWarningResult.Other);

                //记录巷道ID
                summaryCells[i, COLUMN_TUNNEL_ID].Text = _ents[i].TunnelID.ToString().Trim();
            }
            //设置焦点
            this._fpTunelInfo.ActiveSheet.SetActiveCell(0, 0);
            //传入默认值，使得右侧详细信息随之改变
            _fpTunelInfo_SelectionChanged(null, null);

        }

        /// <summary>
        /// 转换数据库中列名为中文显示
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ConvertDatabaseColumnNameToDisPlayColumnsName(string name)
        {
            switch (name)
            {
                case "TUNNEL_NAME":
                    return "巷道名称";
                case "date_shift":
                    return "班次";
                case "data_time":
                    return "日期";
                case "warning_type":
                    return "预警类型";
                case "warning_result":
                    return "预警结果";
            }
            return "";
        }

        /// <summary>
        /// 添加巷道名称
        /// </summary>
        /// <param name="ents"></param>
        private void AddTunelNames()
        {
            _cbxSelWorkSurface.Items.Clear();
            _cbxSelWorkSurface.Items.Add("<全部>");

        }

        /// <summary>
        /// 添加预警类型
        /// </summary>
        //private void AddWarningType()
        //{
        //    _cbxWarningType.Items.Clear();
        //    _cbxWarningType.Items.Add("<全部>");
        //    _cbxWarningType.Items.Add(LibCommon.WarningResultCHN.红色预警.ToString());
        //    _cbxWarningType.Items.Add(LibCommon.WarningResultCHN.黄色预警.ToString());
        //    _cbxWarningType.Items.Add(LibCommon.WarningResultCHN.正常.ToString());
        //    _cbxWarningType.SelectedIndex = 0;
        //}

        /// <summary>
        /// 测试用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            LoadTunelInformation();
        }

        /// <summary>
        /// 确保起始时间小于终止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (_dtpStartTime.Value >= _dtpEndTime.Value)
            {
                _dtpStartTime.Value = _dtpEndTime.Value.AddDays(-1);
                Alert.alert(LibCommon.Const_WM.WRONG_DATETIME, LibCommon.Const.NOTES);
                return;
            }
        }

        /// <summary>
        /// 确保起始时间小于终止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (_dtpStartTime.Value >= _dtpEndTime.Value)
            {
                _dtpEndTime.Value = _dtpStartTime.Value.AddDays(+1);
                Alert.alert(LibCommon.Const_WM.WRONG_DATETIME, LibCommon.Const.NOTES);
                return;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpTunelInfo, false))
            {
                Alert.alert(LibCommon.Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(_fpTunelInfo, 0);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 预警结果日报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDayPreWarning_Click(object sender, EventArgs e)
        {

        }

        //预警结果详细信息按钮触发事件
        private void _fpPreWarningDetials_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 2)
            {
                int rowIndex = 0;
                if (e != null)
                {
                    rowIndex = e.Row;
                }
                else
                {
                    return;
                }
                if (_fpPreWarningDetials.ActiveSheet.Cells[rowIndex, 1].Value == null)
                {
                    return;
                }

                string warningtype = "";
                string warningitem = "";

                //取出选择的项对应的类型
                //枚举中的项个数
                int warningItemCount = Enum.GetNames(typeof(LibCommon.WarningReasonItems)).Length;

                #region 取出选择的项对应的值
                if (rowIndex >= _iDetailsRowHeaderCount && rowIndex <= _iDetailsRowHeaderCount + warningItemCount)
                {
                    warningtype = LibCommon.WarningTypeCHN.超限预警.ToString();
                }
                else if (rowIndex >= 2 * _iDetailsRowHeaderCount + warningItemCount && rowIndex <= 2 * _iDetailsRowHeaderCount + 2 * warningItemCount)
                {
                    warningtype = LibCommon.WarningTypeCHN.突出预警.ToString();
                }
                if (rowIndex == (int)LibCommon.WarningReasonItems.瓦斯 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.瓦斯 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.瓦斯.ToString();
                }
                else if (rowIndex == (int)LibCommon.WarningReasonItems.煤层赋存 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.煤层赋存 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.煤层赋存.ToString();
                }
                else if (rowIndex == (int)LibCommon.WarningReasonItems.地质构造 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.地质构造 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.地质构造.ToString();
                }
                else if (rowIndex == (int)LibCommon.WarningReasonItems.通风 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.通风 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.通风.ToString();
                }
                else if (rowIndex == (int)LibCommon.WarningReasonItems.管理因素 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.管理因素 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.管理因素.ToString();
                }
                else if (rowIndex == (int)LibCommon.WarningReasonItems.其他 + _iDetailsRowHeaderCount || rowIndex == (int)LibCommon.WarningReasonItems.其他 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = LibCommon.WarningReasonItems.其他.ToString();
                }
                #endregion

                //设置传出值

                //预警类型
                sOutWarningType = warningtype;
                //预警依据
                sOutWarningItem = warningitem;

                PreWarningResultDetailsQuery pwrdq = new PreWarningResultDetailsQuery("-1", sOutWorkface, sOutDate, sOutShift, sOutWarningResult, sOutWarningType, sOutWarningItem, true);
                pwrdq.ShowDialog();
            }
        }
    }
}
