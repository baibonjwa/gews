using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys5
{
    public partial class PreWarningResultQuery : Form
    {
        //详细信息的数据标题行
        private const int _iDetailsRowHeaderCount = 2;
        //数据列数
        private const int COLUMN_COUNT = 18;
        private string sOutDate; // 日期
        private string sOutShift; // 班次
        private string sOutWarningId; // Tunnel WirePointName
        private string sOutWarningItem; // 瓦斯/地质构造/通风/管理/煤层赋存
        private int sOutWarningType; // 突出/超限
        // 传出值
        private string sOutWorkface; // 工作面名称
        //需要过滤的列索引
        private readonly int[] _filterColunmIdxs;
        // 每个总结的简要信息
        private readonly Cells infoCells;
        private readonly int sOutWarningResult; // 红色/黄色/绿色
        // Summary of the query, 每个班次的查询总结
        private readonly Cells summaryCells;

        /// <summary>
        ///     构造函数
        /// </summary>
        public PreWarningResultQuery()
        {
            InitializeComponent();
            summaryCells = _fpTunelInfo.ActiveSheet.Cells;
            infoCells = _fpPreWarningDetials.ActiveSheet.Cells;

            //定义Farpoint列数
            _fpTunelInfo.ActiveSheet.ColumnCount = COLUMN_COUNT;
            //设置窗体格式和窗体名称
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_WM.PREWARNING_RESULT_QUERY);

            // 调用委托方法 （必须实装）
            //dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            #region Farpoint自动过滤功能

            //初始化需要过滤功能的列
            _filterColunmIdxs = new[]
            {
                0,
                1,
                2
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(_fpTunelInfo, _filterColunmIdxs);

            #endregion
        }

        /// <summary>
        ///     调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        //private void FrmParent_EventHandler(object sender)
        //{
        //    DoQuery();
        //}

        /// <summary>
        ///     窗体登陆时加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreWarningResultQuery_Load(object sender, EventArgs e)
        {
            #region 设定默认的时间

            _dtpStartTime.ValueChanged -= _dtpStartTime_ValueChanged;
            _dtpEndTime.ValueChanged -= _dtpStartTime_ValueChanged;
            _dtpEndTime.Value = DateTime.Now;
            _dtpStartTime.Value = DateTime.Now.AddDays(-1);
            _dtpStartTime.ValueChanged += _dtpStartTime_ValueChanged;
            _dtpEndTime.ValueChanged += _dtpStartTime_ValueChanged;

            #endregion

            //加载预警类型
            //AddWarningType();
            var workingFace = WorkingFace.FindAll();
            _cbxSelWorkSurface.Items.Add("全部");
            _cbxSelWorkSurface.SelectedIndex = 0;
            foreach (var t in workingFace)
            {
                _cbxSelWorkSurface.Items.Add(t.WorkingFaceName);
            }

            //设置farpoint显示行列数
            _fpTunelInfo.ActiveSheet.RowCount = 0;
            for (var i = 5; i < COLUMN_COUNT; i++)
            {
                _fpTunelInfo.ActiveSheet.Columns[i].Visible = false;
            }

            //Mark By QinKai，若添加新的预警依据项，需要更改的内容
            //加载预警依据的类别
            var WaringItemTypeNames = Enum.GetNames(typeof(WarningReasonItems));
            cbWarningType.SelectedIndex = 0;

            for (var index = 0; index < WaringItemTypeNames.Length; index++)
            {
                infoCells[_iDetailsRowHeaderCount + index, 0].Text = WaringItemTypeNames[index];
                infoCells[WaringItemTypeNames.Length + _iDetailsRowHeaderCount * 2 + index, 0].Text =
                    WaringItemTypeNames[index];
            }
        }

        /// <summary>
        ///     查询符合时间段的预警信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnQuery_Click(object sender, EventArgs e)
        {
            //获取选择的时间范围
            var dateStartYear = _dtpStartTime.Value.Year.ToString();
            var dateStartMonth = _dtpStartTime.Value.Month < 10
                ? "0" + _dtpStartTime.Value.Month
                : _dtpStartTime.Value.Month.ToString();
            var dateStartDay = _dtpStartTime.Value.Day < 10
                ? "0" + _dtpStartTime.Value.Day
                : _dtpStartTime.Value.Day.ToString();
            var dateEndYear = _dtpEndTime.Value.Year.ToString();
            var dateEndMonth = _dtpEndTime.Value.Month < 10
                ? "0" + _dtpEndTime.Value.Month
                : _dtpEndTime.Value.Month.ToString();
            var dateEndDay = _dtpEndTime.Value.Day < 10 ? "0" + _dtpEndTime.Value.Day : _dtpEndTime.Value.Day.ToString();

            //自行转换成数据库中合适的类型
            var dateStart = dateStartYear + "-" + dateStartMonth + "-" + dateStartDay + " 00:00:00";
            var dateEnd = dateEndYear + "-" + dateEndMonth + "-" + dateEndDay + " 23:59:59";

            //根据日期查询结果
            //if (_cbxSelWorkSurface.SelectedItem != null)
            //{
            var workingFace = _cbxSelWorkSurface.SelectedItem.ToString() == "全部"
                ? ""
                : _cbxSelWorkSurface.SelectedItem.ToString();

            //调取填充Farpoint的事件
            DoQuery(Convert.ToDateTime(dateStart), Convert.ToDateTime(dateEnd), workingFace);
        }

        /// <summary>
        ///     控制切换右侧详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fpTunelInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rowIndex = 0;
            if (e != null)
            {
                rowIndex = e.Range.Row;
            }

            //巷道名称
            _txtWorkingFaceName.Text = summaryCells[rowIndex, 0].Text;
            //日期班次
            _txtPreWarningDateAndShift.Text = summaryCells[rowIndex, 1].Text + " " + summaryCells[rowIndex, 2].Text;
            try
            {
                //超限预警
                _picOverLimitResult.Image = (Image)summaryCells[rowIndex, 3].Value;
                //突出预警
                _picOurburstResult.Image = (Image)summaryCells[rowIndex, 4].Value;

                var imageCell = new ImageCellType();
                imageCell.Style = RenderStyle.Normal;

                //Mark By QinKai，若添加新的预警依据项，需要更改的内容
                var buttonCell = new ButtonCellType();
                buttonCell.Text = "详细信息";
                //超限预警
                //瓦斯
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].Value =
                    summaryCells[rowIndex, _iOverLimitGas].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 2].CellType = buttonCell;
                //煤层
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].Value =
                    summaryCells[rowIndex, _iOverLimitCoal].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 2].CellType = buttonCell;
                //地质
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].Value =
                    summaryCells[rowIndex, _iOverLimitGeology].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 2].CellType = buttonCell;
                //通风
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].Value =
                    summaryCells[rowIndex, _iOverLimitVentilation].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 2].CellType = buttonCell;
                //管理
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].CellType = imageCell;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].Value =
                    summaryCells[rowIndex, _iOverLimitManagement].Value;
                infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 2].CellType = buttonCell;
                ////其他
                //infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 1].CellType = imageCell;
                //infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 1].Value =
                //    summaryCells[rowIndex, _iOverLimitOther].Value;
                //infoCells[_iDetailsRowHeaderCount + (int)WarningReasonItems.其他, 2].CellType = buttonCell;

                //预警依据类型的数量
                var warningItemTypeCount = Enum.GetNames(typeof(WarningReasonItems)).Length;
                //超限预警
                //瓦斯
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].CellType =
                    imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 1].Value =
                    (summaryCells[rowIndex, _iOutBurstGas]).Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.瓦斯, 2].CellType =
                    buttonCell;
                //煤层
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].CellType
                    = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 1].Value =
                    summaryCells[rowIndex, _iOutBurstCoal].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.煤层赋存, 2].CellType
                    = buttonCell;
                //地质
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].CellType
                    = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 1].Value =
                    summaryCells[rowIndex, _iOutBurstGeology].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.地质构造, 2].CellType
                    = buttonCell;
                //通风
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].CellType =
                    imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 1].Value =
                    summaryCells[rowIndex, _iOutBurstVentilation].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.通风, 2].CellType =
                    buttonCell;
                //管理
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].CellType
                    = imageCell;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 1].Value =
                    summaryCells[rowIndex, _iOutBurstManagement].Value;
                infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + (int)WarningReasonItems.管理因素, 2].CellType
                    = buttonCell;
                ////其他
                //infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 1].CellType =
                //    imageCell;
                //infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 1].Value =
                //    summaryCells[rowIndex, _iOutBurstOther].Value;
                //infoCells[warningItemTypeCount + 2 * _iDetailsRowHeaderCount + +(int)WarningReasonItems.其他, 2].CellType =
                //    buttonCell;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.ToString());
            }

            //设置传出值
            //工作面名称
            sOutWorkface = _txtWorkingFaceName.Text;
            //日期
            sOutDate = summaryCells[rowIndex, 1].Text;
            //班次                   
            sOutShift = summaryCells[rowIndex, 2].Text;
            //巷道ID                 
            sOutWarningId = summaryCells[rowIndex, COLUMN_WARNING_ID].Text;
        }

        /// <summary>
        ///     加载巷道信息
        /// </summary>
        private void DoQuery(DateTime startTime, DateTime endTime, string workingFace)
        {
            //设置控件是否启用
            dataPager1.Enabled = true;

            var warningList = EarlyWarningResult.FindAllByDateTimeAndWorkingfaceName(startTime, endTime, workingFace);

            var earlyWarningResultGroup =
                warningList.GroupBy(i => new { i.Tunnel.WorkingFace, i.Shift, i.DateTime.Date }).ToList();

            var iRecordCount = earlyWarningResultGroup.Count;
            dataPager1.PageControlInit(iRecordCount);
            var iStartIndex = dataPager1.getStartIndex();

            while (_fpTunelInfo.ActiveSheet.Rows.Count > 0)
            {
                _fpTunelInfo.ActiveSheet.Rows.Remove(0, 1);
            }


            //根据分页控件选择数据
            var warningOnePage = earlyWarningResultGroup.Skip(iStartIndex).Take(Convert.ToInt32(dataPager1.PageSize));

            int rowId = 0;

            foreach (var item in warningOnePage)
            {
                var first = item.First();
                var theWorstEwrOutBurst = new EarlyWarningResult();
                var theWorstEwrOverLimit = new EarlyWarningResult();
                string warningId = "";
                foreach (var j in item)
                {
                    switch (j.WarningType)
                    {
                        case (int)WarningType.OUTBURST:
                            theWorstEwrOutBurst.HandleStatus = j.HandleStatus;
                            if (j.WarningResult < theWorstEwrOutBurst.WarningResult)
                                theWorstEwrOutBurst.WarningResult = j.WarningResult;
                            if (j.Gas < theWorstEwrOutBurst.Gas) theWorstEwrOutBurst.Gas = j.Gas;
                            if (j.Coal < theWorstEwrOutBurst.Coal) theWorstEwrOutBurst.Coal = j.Coal;
                            if (j.Geology < theWorstEwrOutBurst.Geology) theWorstEwrOutBurst.Geology = j.Geology;
                            if (j.Ventilation < theWorstEwrOutBurst.Ventilation)
                                theWorstEwrOutBurst.Ventilation = j.Ventilation;
                            if (j.Management < theWorstEwrOutBurst.Management)
                                theWorstEwrOutBurst.Management = j.Management;
                            break;
                        case (int)WarningType.OVERLIMIT:
                            theWorstEwrOverLimit.HandleStatus = j.HandleStatus;
                            if (j.WarningResult < theWorstEwrOverLimit.WarningResult)
                                theWorstEwrOverLimit.WarningResult = j.WarningResult;
                            if (j.Gas < theWorstEwrOverLimit.Gas) theWorstEwrOverLimit.Gas = j.Gas;
                            if (j.Coal < theWorstEwrOverLimit.Coal) theWorstEwrOverLimit.Coal = j.Coal;
                            if (j.Geology < theWorstEwrOverLimit.Geology) theWorstEwrOverLimit.Geology = j.Geology;
                            if (j.Ventilation < theWorstEwrOverLimit.Ventilation)
                                theWorstEwrOverLimit.Ventilation = j.Ventilation;
                            if (j.Management < theWorstEwrOverLimit.Management)
                                theWorstEwrOverLimit.Management = j.Management;
                            break;
                    }
                    warningId += j.Id + ";";
                }


                _fpTunelInfo.ActiveSheet.Rows.Add(rowId, 1);
                _fpTunelInfo.ActiveSheet.Rows[rowId].Height = 30;
                _fpTunelInfo.ActiveSheet.Rows[rowId].Locked = true;

                //巷道名称
                summaryCells[rowId, COLUMN_TUNNEL_NAME].Value = first.Tunnel.WorkingFace.WorkingFaceName;
                summaryCells[rowId, COLUMN_TUNNEL_NAME].HorizontalAlignment = CellHorizontalAlignment.Center;
                summaryCells[rowId, COLUMN_TUNNEL_NAME].VerticalAlignment = CellVerticalAlignment.Center;
                //日期
                summaryCells[rowId, COLUMN_DATE_TIME].Value = first.DateTime.ToShortDateString();
                summaryCells[rowId, COLUMN_DATE_TIME].HorizontalAlignment = CellHorizontalAlignment.Center;
                summaryCells[rowId, COLUMN_DATE_TIME].VerticalAlignment = CellVerticalAlignment.Center;
                //班次
                summaryCells[rowId, COLUMN_DATE_SHIFT].Value = first.Shift;
                summaryCells[rowId, COLUMN_DATE_SHIFT].HorizontalAlignment = CellHorizontalAlignment.Center;
                summaryCells[rowId, COLUMN_DATE_SHIFT].VerticalAlignment = CellVerticalAlignment.Center;
                //超限预警
                FpUtil.setCellImg(summaryCells[rowId, COLUMN_WARNING_RESULT_OVERLIMIT],
                    theWorstEwrOverLimit.WarningResult);
                //突出预警
                FpUtil.setCellImg(summaryCells[rowId, COLUMN_WARNING_RESULT_OUTBURST],
                    theWorstEwrOutBurst.WarningResult);

                //瓦斯
                FpUtil.setCellImg(summaryCells[rowId, _iOverLimitGas], theWorstEwrOverLimit.Gas);
                //煤层
                FpUtil.setCellImg(summaryCells[rowId, _iOverLimitCoal], theWorstEwrOverLimit.Coal);
                //地质
                FpUtil.setCellImg(summaryCells[rowId, _iOverLimitGeology], theWorstEwrOverLimit.Geology);
                //通风
                FpUtil.setCellImg(summaryCells[rowId, _iOverLimitVentilation], theWorstEwrOverLimit.Ventilation);
                //管理
                FpUtil.setCellImg(summaryCells[rowId, _iOverLimitManagement], theWorstEwrOverLimit.Management);

                //瓦斯
                FpUtil.setCellImg(summaryCells[rowId, _iOutBurstGas], theWorstEwrOutBurst.Gas);
                //煤层
                FpUtil.setCellImg(summaryCells[rowId, _iOutBurstCoal], theWorstEwrOutBurst.Coal);
                //地质
                FpUtil.setCellImg(summaryCells[rowId, _iOutBurstGeology], theWorstEwrOutBurst.Geology);
                //通风
                FpUtil.setCellImg(summaryCells[rowId, _iOutBurstVentilation], theWorstEwrOutBurst.Ventilation);
                //管理
                FpUtil.setCellImg(summaryCells[rowId, _iOutBurstManagement], theWorstEwrOutBurst.Management);



                //记录巷道ID
                summaryCells[rowId, COLUMN_WARNING_ID].Text = warningId;
            }
            //设置焦点
            _fpTunelInfo.ActiveSheet.SetActiveCell(0, 0);
            //传入默认值，使得右侧详细信息随之改变
            _fpTunelInfo_SelectionChanged(null, null);



            //#endregion


        }

        /// <summary>
        ///     确保起始时间小于终止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (_dtpStartTime.Value >= _dtpEndTime.Value)
            {
                _dtpStartTime.Value = _dtpEndTime.Value.AddDays(-1);
                Alert.alert(Const_WM.WRONG_DATETIME, Const.NOTES);
            }
        }

        /// <summary>
        ///     确保起始时间小于终止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (_dtpStartTime.Value >= _dtpEndTime.Value)
            {
                _dtpEndTime.Value = _dtpStartTime.Value.AddDays(+1);
                Alert.alert(Const_WM.WRONG_DATETIME, Const.NOTES);
            }
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpTunelInfo, false))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(_fpTunelInfo, 0);
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     预警结果日报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDayPreWarning_Click(object sender, EventArgs e)
        {
        }

        //预警结果详细信息按钮触发事件
        private void _fpPreWarningDetials_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == 2)
            {
                var rowIndex = 0;
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

                int warningtype = 0;
                var warningitem = "";

                //取出选择的项对应的类型
                //枚举中的项个数
                var warningItemCount = Enum.GetNames(typeof(WarningReasonItems)).Length;

                #region 取出选择的项对应的值

                if (rowIndex >= _iDetailsRowHeaderCount && rowIndex <= _iDetailsRowHeaderCount + warningItemCount)
                {
                    warningtype = Convert.ToInt32(WarningType.OVERLIMIT);
                }
                else if (rowIndex >= 2 * _iDetailsRowHeaderCount + warningItemCount &&
                         rowIndex <= 2 * _iDetailsRowHeaderCount + 2 * warningItemCount)
                {
                    warningtype = Convert.ToInt32(WarningType.OUTBURST);
                }
                if (rowIndex == (int)WarningReasonItems.瓦斯 + _iDetailsRowHeaderCount ||
                    rowIndex == (int)WarningReasonItems.瓦斯 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = WarningReasonItems.瓦斯.ToString();
                }
                else if (rowIndex == (int)WarningReasonItems.煤层赋存 + _iDetailsRowHeaderCount ||
                         rowIndex == (int)WarningReasonItems.煤层赋存 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = WarningReasonItems.煤层赋存.ToString();
                }
                else if (rowIndex == (int)WarningReasonItems.地质构造 + _iDetailsRowHeaderCount ||
                         rowIndex == (int)WarningReasonItems.地质构造 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = WarningReasonItems.地质构造.ToString();
                }
                else if (rowIndex == (int)WarningReasonItems.通风 + _iDetailsRowHeaderCount ||
                         rowIndex == (int)WarningReasonItems.通风 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = WarningReasonItems.通风.ToString();
                }
                else if (rowIndex == (int)WarningReasonItems.管理因素 + _iDetailsRowHeaderCount ||
                         rowIndex ==
                         (int)WarningReasonItems.管理因素 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                {
                    warningitem = WarningReasonItems.管理因素.ToString();
                }
                //else if (rowIndex == (int)WarningReasonItems.其他 + _iDetailsRowHeaderCount ||
                //         rowIndex ==
                //         (int)WarningReasonItems.其他 + _iDetailsRowHeaderCount * 2 + warningItemCount)
                //{
                //    warningitem = WarningReasonItems.其他.ToString();
                //}

                #endregion

                //设置传出值

                //预警类型
                sOutWarningType = warningtype;


                //预警依据
                sOutWarningItem = warningitem;

                var workingFace = WorkingFace.FindByWorkingFaceName(sOutWorkface);
                var pwrdq = new PreWarningResultDetailsQuery(sOutWarningId, workingFace, Convert.ToDateTime(sOutDate), sOutShift,
                    sOutWarningType, sOutWarningItem, true);
                pwrdq.ShowDialog();
            }
        }

        //Mark By QinKai

        #region 数据列索引

        private const int COLUMN_TUNNEL_NAME = 0;
        private const int COLUMN_DATE_TIME = 1;
        private const int COLUMN_DATE_SHIFT = 2;
        private const int COLUMN_WARNING_RESULT_OVERLIMIT = 3;
        private const int COLUMN_WARNING_RESULT_OUTBURST = 4;
        private const int _iOverLimitGas = 5;
        private const int _iOverLimitCoal = 6;
        private const int _iOverLimitGeology = 7;
        private const int _iOverLimitVentilation = 8;
        private const int _iOverLimitManagement = 9;
        private const int _iOverLimitOther = 10;
        private const int _iOutBurstGas = 11;
        private const int _iOutBurstCoal = 12;
        private const int _iOutBurstGeology = 13;
        private const int _iOutBurstVentilation = 14;
        private const int _iOutBurstManagement = 15;
        private const int _iOutBurstOther = 16;

        private const int COLUMN_WARNING_ID = 17;

        #endregion

        #region Farpoint自动过滤功能

        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            var chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(_fpTunelInfo, _filterColunmIdxs);
            }
            else //未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(_fpTunelInfo,
                    farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            _fpTunelInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(_fpTunelInfo,
                farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(_fpTunelInfo,
                farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        #endregion
    }
}