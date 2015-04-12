using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using GIS.Warning;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibSocket;
using _5.WarningManagement;

namespace sys5
{
    public partial class PreWarningLastedResultQuery : Form
    {
        //数据行起始位置
        private const int FROZEN_ROW_COUNT = 5;
        //数据列数
        private const int COLUMN_COUNT = 50; //20
        //各列索引
        private const int COLUMN_INDEX_WORKFACE_NAME = 0; // 工作面名称
        private const int COLUMN_INDEX_DATETIME_SHIFT = 1; // 日期班次
        private const int COLUMN_INDEX_WANRING_RESULT_OVERLIMIT = 2; // 超限预警结果
        private const int COLUMN_INDEX_WANRING_RESULT_OUTBURST = 3; // 突出预警结果
        private const int COLUMN_INDEX_OVERLIMIT_GAS = 4; // 超限瓦斯
        private const int COLUMN_INDEX_OUTBURST_GAS = 5; // 突出瓦斯
        private const int COLUMN_INDEX_OVERLIMIT_COAL = 6; // 超限煤层赋存
        private const int COLUMN_INDEX_OUTBURST_COAL = 7; // 突出煤层赋存
        private const int COLUMN_INDEX_OVERLIMIT_GEOLOGY = 8; // 超限地质构造
        private const int COLUMN_INDEX_OUTBURST_GEOLOGY = 9; // 突出地质构造
        private const int COLUMN_INDEX_OVERLIMIT_VENTILATION = 10; // 超限通风
        private const int COLUMN_INDEX_OUTBURST_VENTILATION = 11; // 突出通风 
        private const int COLUMN_INDEX_OVERLIMIT_MANAGEMENT = 12; // 超限管理
        private const int COLUMN_INDEX_OUTBURST_MANAGEMENT = 13; // 突出管理 
        private const int COLUMN_INDEX_DETAIL_INFO_BUTTON = 14; // 详细信息按钮 
        // 下面几列是隐藏起来的
        private const int COLUMN_INDEX_HIDE_OVERLIMIT_WARNING_ID = 15; // Overlimit Warning ID
        private const int COLUMN_INDEX_HIDE_OUTBURST_WARNING_ID = 16; // Outburst Warning ID
        private const int COLUMN_INDEX_HIDE_TUNNEL_ID = 17; // Tunnel ID
        private const int COLUMN_INDEX_HIDE_DATETIME = 18; // 日期时间
        private const int COLUMN_INDEX_HIDE_SHIFT = 19; // 班次
        private const int COLUMN_INDEX_HIDE_OVERLIMIT_HANDLE_STATUS = 20; // Overlimit 处理状态
        private const int COLUMN_INDEX_HIDE_OUTBURST_HANDLE_STATUS = 21; // Outburst 处理状态

        private static List<LibEntity.PreWarningResultQuery> _warningRecord =
            new List<LibEntity.PreWarningResultQuery>();

        //传出结构体，更改时请查找（设置传出值）
        private static readonly EarlyWarningResult OutInfo = new EarlyWarningResult();
        //查询的最新结果
        private List<LibEntity.PreWarningResultQuery> _ents1 = new List<LibEntity.PreWarningResultQuery>();
        private List<LibEntity.PreWarningResultQuery> _ents2 = new List<LibEntity.PreWarningResultQuery>();
        //定义提醒语音字符串
        private string _strPreWarningTxt = "";
        public FlashWarningPoints FlashGis = new FlashWarningPoints();
        private readonly MySpeech _speaker = MySpeech.Instance();
        private readonly Cells cells;

        /// <summary>
        ///     构造函数
        /// </summary>
        public PreWarningLastedResultQuery()
        {
            InitializeComponent();

            cells = _fpPreWaringLastedValue.ActiveSheet.Cells;

            PreWarningLastedResultQueryBLL.loadTunnelNames();

            //定义fp显示的行列数
            _fpPreWaringLastedValue.ActiveSheet.Rows.Count = FROZEN_ROW_COUNT;
            _fpPreWaringLastedValue.ActiveSheet.Columns.Count = COLUMN_COUNT;

            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_WM.LASTED_PREWARNING_RESULT_QUERY);

            //隐藏5列，Columns[_iColumnCount - 1]记录突出预警记录的ID，Columns[_iColumnCount - 2]记录超限预警记录的ID
            _fpPreWaringLastedValue.ActiveSheet.Columns[COLUMN_COUNT - 1].Visible = false;
            _fpPreWaringLastedValue.ActiveSheet.Columns[COLUMN_COUNT - 2].Visible = false;
            _fpPreWaringLastedValue.ActiveSheet.Columns[COLUMN_COUNT - 3].Visible = false;
            _fpPreWaringLastedValue.ActiveSheet.Columns[COLUMN_COUNT - 4].Visible = false;
            _fpPreWaringLastedValue.ActiveSheet.Columns[COLUMN_COUNT - 5].Visible = false;

            _warningRecord = PreWarningLastedResultQueryBLL.QueryHoldWarningResult();
        }

        /// <summary>
        ///     获取传出的值
        /// </summary>
        /// <returns></returns>
        public static EarlyWarningResult GetOutInfo()
        {
            return OutInfo;
        }

        /// <summary>
        ///     加载预警数据
        /// </summary>
        public void LoadValue(string strTime)
        {
            // 所有绿色预警
            _ents1 = PreWarningLastedResultQueryBLL.QueryLastedPreWarningResult(strTime);
            // 所有红色和黄色预警
            _ents2 = PreWarningLastedResultQueryBLL.QueryHoldWarningResult();

            //List<PreWarningResultEntity>     LibBusiness.PreWarningLastedResultQueryBLL.MergePreWarningInfo(_ents1);
            if (_ents2.Count > _warningRecord.Count)
            {
                var ds = UserInformationDetailsManagementBLL.GetNeedSendMessageUsers();

                foreach (var i in _ents2)
                {
                    var sign = true;
                    foreach (var j in _warningRecord)
                    {
                        if (i.TunnelID == j.TunnelID)
                        {
                            sign = false;
                        }
                    }
                    if (sign)
                    {
                        var outBrustStr = "";
                        var overLimitStr = "";
                        if (i.OutBrustWarningResult.WarningResult == 0)
                        {
                            outBrustStr = "红色突出预警";
                        }
                        else if (i.OutBrustWarningResult.WarningResult == 1)
                        {
                            outBrustStr = "黄色突出预警";
                        }
                        if (i.OverLimitWarningResult.WarningResult == 0)
                        {
                            overLimitStr = "红色超限预警";
                        }
                        else if (i.OverLimitWarningResult.WarningResult == 0)
                        {
                            overLimitStr = "黄色超限预警";
                        }
                        var message = i.TunelName + " " + i.DateTime + " " + i.Date_Shift + " " + outBrustStr + " " +
                                      overLimitStr;

                        var TypeStr = "";
                        var CopyRightToCOMStr = "";
                        var CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
                        short port = 11;
                        var isture = SetShortMessage.Sms_Connection(CopyRightStr, port, Convert.ToInt16(9600), TypeStr,
                            CopyRightToCOMStr);
                        if (isture == 1)
                        {
                            for (var k = 0; k < ds.Tables[0].Rows.Count; k++)
                            {
                                var number =
                                    ds.Tables[0].Rows[k][UserInformationDetailsManagementDbConstNames.USER_PHONENUMBER]
                                        .ToString();
                                SetShortMessage.Sms_Send(number, message);
                            }
                        }
                    }
                }
                _warningRecord = new List<LibEntity.PreWarningResultQuery>(_ents2);
            }
            else
            {
                _warningRecord = new List<LibEntity.PreWarningResultQuery>(_ents2);
            }


            //var workingfaceEntNormal = new List<PreWarningResultQueryWithWorkingfaceEnt>();
            //var workingfaceEntWarning = new List<PreWarningResultQueryWithWorkingfaceEnt>();

            //workingfaceEntNormal = PreWarningLastedResultQueryBLL.MergePreWarningInfo(_ents1);
            //workingfaceEntWarning = PreWarningLastedResultQueryBLL.MergePreWarningInfo(_ents2);

            #region 删除垃圾数据

            while (_fpPreWaringLastedValue.ActiveSheet.Rows.Count > FROZEN_ROW_COUNT)
            {
                _fpPreWaringLastedValue.ActiveSheet.Rows.Remove(FROZEN_ROW_COUNT, 1);
            }
            //提醒语音字符串清空
            _strPreWarningTxt = "";

            #endregion

            #region 添加标题数据

            //计算时间
            cells[1, 5].Value = strTime;
            //管理员
            cells[1, 1].Value = CurrentUser.CurLoginUserInfo.LoginName;
            cells[1, 1].Locked = false;
            //添加计算机名称
            //this._fpPreWaringLastedValue.ActiveSheet.Cells[1, 6].Value = GetComputerName();
            //编号
            cells[1, 9].Value = DateTime.Now.ToShortDateString();
            cells[1, 9].Locked = false;
            //其他
            cells[1, 12].Value = "";

            #endregion

            //清空提示文档
            try
            {
                var swnull = new StreamWriter(Application.StartupPath + "\\NoticeTxt.txt", false, Encoding.UTF8);
                swnull.Write(_strPreWarningTxt);
                swnull.Close();
            }
            catch (Exception ex)
            {
                Alert.alert(ex.ToString());
            }

            var valueCount = _ents2.Count;
            for (var i = 0; i < valueCount; i++)
            {
                addRow2Fp(FROZEN_ROW_COUNT + i, _ents2[i]);
            }

            valueCount = _ents1.Count;
            var iTmpRowCount = FROZEN_ROW_COUNT + _ents2.Count;

            for (var i = 0; i < valueCount; i++)
            {
                addRow2Fp(iTmpRowCount + i, _ents1[i]);
            }

            try
            {
                _speaker.Stop();
                if (_strPreWarningTxt == "")
                {
                    return;
                }
                var sw = new StreamWriter(Application.StartupPath + "\\NoticeTxt.txt", false, Encoding.UTF8);
                sw.Write(_strPreWarningTxt);
                sw.Close();

                //启动语音报警程序
                if (_chkAddPreWarningVoice.Checked)
                {
                    var thread = new Thread(SpeakerThreadFunction);
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                Alert.alert(ex.ToString());
            }
        }

        /// <summary>
        ///     向farpoint spread表单中添加一行数据.
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="entity"></param>
        private void addRow2Fp(int rowIdx, LibEntity.PreWarningResultQuery entity)
        {
            _fpPreWaringLastedValue.ActiveSheet.Rows.Add(rowIdx, 1);
            _fpPreWaringLastedValue.ActiveSheet.Rows[rowIdx].Height = 40;
            _fpPreWaringLastedValue.ActiveSheet.Rows[rowIdx].Locked = true;

            cells[rowIdx, COLUMN_INDEX_WORKFACE_NAME].Value = entity.TunelName;
            cells[rowIdx, COLUMN_INDEX_DATETIME_SHIFT].Value = entity.DateTime + " " + entity.Date_Shift;
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_WANRING_RESULT_OVERLIMIT],
                entity.OverLimitWarningResult.WarningResult);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_WANRING_RESULT_OUTBURST],
                entity.OutBrustWarningResult.WarningResult);

            //瓦斯
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_GAS], entity.OverLimitWarningResult.Gas);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_GAS], entity.OutBrustWarningResult.Gas);

            //煤层
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_COAL], entity.OverLimitWarningResult.Coal);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_COAL], entity.OutBrustWarningResult.Coal);
            //地质
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_GEOLOGY], entity.OverLimitWarningResult.Geology);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_GEOLOGY], entity.OutBrustWarningResult.Geology);
            //通风
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_VENTILATION],
                entity.OverLimitWarningResult.Ventilation);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_VENTILATION], entity.OutBrustWarningResult.Ventilation);
            //管理
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_MANAGEMENT], entity.OverLimitWarningResult.Management);
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_MANAGEMENT], entity.OutBrustWarningResult.Management);

            var buttonCell = new ButtonCellType();
            buttonCell.Text = "···";
            if (entity.OverLimitWarningResult.WarningResult > 1 && entity.OutBrustWarningResult.WarningResult > 1)
            {
                cells[rowIdx, COLUMN_INDEX_DETAIL_INFO_BUTTON].Locked = true;
                buttonCell.Text = "-";
            }
            cells[rowIdx, COLUMN_INDEX_DETAIL_INFO_BUTTON].CellType = buttonCell;

            if (_chkAddPreWarningVoice.Checked)
            {
                if (entity.OverLimitWarningResult.WarningResult < (int) WarningResult.GREEN ||
                    entity.OutBrustWarningResult.WarningResult < (int) WarningResult.GREEN)
                {
                    AddPreWarningTxt(entity.TunelName, entity.OverLimitWarningResult.WarningResult,
                        entity.OutBrustWarningResult.WarningResult);
                }
            }

            //以下内容设置了隐藏，界面上无法看到
            //超限预警记录的ID  列号15
            cells[rowIdx, COLUMN_INDEX_HIDE_OVERLIMIT_WARNING_ID].Value = entity.OverLimitWarningResult.ID;
            //突出预警记录的ID  列号16
            cells[rowIdx, COLUMN_INDEX_HIDE_OUTBURST_WARNING_ID].Value = entity.OutBrustWarningResult.ID;
            //巷道ID  列号17                                            
            cells[rowIdx, COLUMN_INDEX_HIDE_TUNNEL_ID].Value = entity.TunnelID;
            //日期  列号18                                              
            cells[rowIdx, COLUMN_INDEX_HIDE_DATETIME].Value = entity.DateTime;
            //班次  列号19                                              
            cells[rowIdx, COLUMN_INDEX_HIDE_SHIFT].Value = entity.Date_Shift;
            //  列号20                                              
            cells[rowIdx, COLUMN_INDEX_HIDE_OVERLIMIT_HANDLE_STATUS].Value = entity.OverLimitWarningResult.HandleStatus;
            //班次  列号21                                              
            cells[rowIdx, COLUMN_INDEX_HIDE_OUTBURST_HANDLE_STATUS].Value = entity.OutBrustWarningResult.HandleStatus;
        }

        //private void addRow2Fp(int rowIdx, PreWarningResultQueryWithWorkingfaceEnt entity)
        //{
        //    this._fpPreWaringLastedValue.ActiveSheet.Rows.Add(rowIdx, 1);
        //    this._fpPreWaringLastedValue.ActiveSheet.Rows[rowIdx].Height = 40;
        //    this._fpPreWaringLastedValue.ActiveSheet.Rows[rowIdx].Locked = true;

        //    this.cells[rowIdx, COLUMN_INDEX_WORKFACE_NAME].Value = entity.WorkingfaceName;
        //    this.cells[rowIdx, COLUMN_INDEX_DATETIME_SHIFT].Value = entity.DateTime + " " + entity.Date_Shift;
        //    FpUtil.setCellImg(this.cells[rowIdx, COLUMN_INDEX_WANRING_RESULT_OVERLIMIT],
        //        entity.OverLimitWarningResult.WarningResult);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_WANRING_RESULT_OUTBURST],
        //        entity.OutBrustWarningResult.WarningResult);

        //    //瓦斯
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_GAS], entity.OverLimitWarningResult.Gas);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_GAS], entity.OutBrustWarningResult.Gas);

        //    //煤层
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_COAL], entity.OverLimitWarningResult.Coal);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_COAL], entity.OutBrustWarningResult.Coal);
        //    //地质
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_GEOLOGY], entity.OverLimitWarningResult.Geology);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_GEOLOGY], entity.OutBrustWarningResult.Geology);
        //    //通风
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_VENTILATION], entity.OverLimitWarningResult.Ventilation);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_VENTILATION], entity.OutBrustWarningResult.Ventilation);
        //    //管理
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OVERLIMIT_MANAGEMENT], entity.OverLimitWarningResult.Management);
        //    FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_OUTBURST_MANAGEMENT], entity.OutBrustWarningResult.Management);

        //    FarPoint.Win.Spread.CellType.ButtonCellType buttonCell = new FarPoint.Win.Spread.CellType.ButtonCellType();
        //    buttonCell.Text = "···";
        //    if (entity.OverLimitWarningResult.WarningResult > 1 && entity.OutBrustWarningResult.WarningResult > 1)
        //    {
        //        cells[rowIdx, COLUMN_INDEX_DETAIL_INFO_BUTTON].Locked = true;
        //        buttonCell.Text = "-";
        //    }
        //    cells[rowIdx, COLUMN_INDEX_DETAIL_INFO_BUTTON].CellType = buttonCell;

        //    if (_chkAddPreWarningVoice.Checked)
        //    {
        //        if (entity.OverLimitWarningResult.WarningResult < (int)LibCommon.WarningResult.GREEN ||
        //            entity.OutBrustWarningResult.WarningResult < (int)LibCommon.WarningResult.GREEN)
        //        {
        //            AddPreWarningTxt(entity.WorkingfaceName, entity.OverLimitWarningResult.WarningResult, entity.OutBrustWarningResult.WarningResult);
        //        }
        //    }

        //    //以下内容设置了隐藏，界面上无法看到
        //    //超限预警记录的ID  列号15
        //    cells[rowIdx, COLUMN_INDEX_HIDE_OVERLIMIT_WARNING_ID].Value = entity.OverLimitWarningResult.ID;
        //    //突出预警记录的ID  列号16
        //    cells[rowIdx, COLUMN_INDEX_HIDE_OUTBURST_WARNING_ID].Value = entity.OutBrustWarningResult.ID;
        //    //巷道ID  列号17                                            
        //    cells[rowIdx, COLUMN_INDEX_HIDE_TUNNEL_ID].Value = entity.Tunnel;
        //    //日期  列号18                                              
        //    cells[rowIdx, COLUMN_INDEX_HIDE_DATETIME].Value = entity.DateTime;
        //    //班次  列号19                                              
        //    cells[rowIdx, COLUMN_INDEX_HIDE_SHIFT].Value = entity.Date_Shift;
        //    //  列号20                                              
        //    cells[rowIdx, COLUMN_INDEX_HIDE_OVERLIMIT_HANDLE_STATUS].Value = entity.OverLimitWarningResult.HandleStatus;
        //    //班次  列号21                                              
        //    cells[rowIdx, COLUMN_INDEX_HIDE_OUTBURST_HANDLE_STATUS].Value = entity.OutBrustWarningResult.HandleStatus;
        //}

        /// <summary>
        ///     语音提示文本
        /// </summary>
        /// <param name="strPreWarningTxt"></param>
        /// <param name="tunnelName"></param>
        /// <param name="overLimitResult"></param>
        /// <param name="OutBurstResult"></param>
        private void AddPreWarningTxt(string tunnelName, int OverLimitResult, int OutBurstResult)
        {
            //添加巷道名称
            _strPreWarningTxt += ChangeTunnelName(tunnelName) + new string(' ', 6);

            if (OverLimitResult < (int) WarningResult.GREEN)
            {
                _strPreWarningTxt += ChangePreWarningIntValueToString(OverLimitResult) + "超限  预警" + new string(' ', 6);
            }

            if (OutBurstResult < (int) WarningResult.GREEN)
            {
                _strPreWarningTxt += ChangePreWarningIntValueToString(OutBurstResult) + "突出  预警" + new string(' ', 15);
            }

            //添加换行
            _strPreWarningTxt += "\r\n";
        }

        /// <summary>
        ///     将数据库中的预警信息转化成汉字形式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ChangePreWarningIntValueToString(int value)
        {
            var returnValue = "正常" + new string(' ', 3);
            switch (value)
            {
                case 0:
                    returnValue = "红色" + new string(' ', 3);
                    break;
                case 1:
                    returnValue = "黄色" + new string(' ', 3);
                    break;
            }
            return returnValue;
        }

        /// <summary>
        ///     转换巷道名称。将巷道名称98611转换成9 8 6 1 1 ，便于读取时不读成 “玖万捌仟陆佰壹拾壹”
        /// </summary>
        /// <param name="oldName"></param>
        /// <returns></returns>
        private string ChangeTunnelName(string oldName)
        {
            var returnStr = "";
            var array = oldName.ToCharArray();
            foreach (var c in array)
            {
                if (char.IsDigit(c))
                {
                    returnStr += c + " ";
                }
                else
                {
                    returnStr += c;
                }
            }
            return returnStr;
        }

        /// <summary>
        ///     获取计算机名称
        /// </summary>
        /// <returns></returns>
        private string GetComputerName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(_fpPreWaringLastedValue, 0);
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpPreWaringLastedValue, false))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        ///     设置传出值，并启动详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fpPreWaringLastedValue_CellClick(object sender, CellClickEventArgs e)
        {
            //当选择行的数据是预警结果属于正常时，锁定单元格。检测到单元格锁定时，返回，屏蔽窗体弹出
            if (cells[e.Row, e.Column].Locked)
            {
                return;
            }

            if (e.Row < 5)
            {
                return;
            }

            // 第14列是预警结果详细信息
            if (e.Column == 14)
            {
                // 第0列是工作面名称
                var workface = cells[e.Row, COLUMN_INDEX_WORKFACE_NAME].Text;
                // 第17列是巷道Id
                var tunnelId = cells[e.Row, COLUMN_INDEX_HIDE_TUNNEL_ID].Text;
                // 第18列是日期时间
                var dateTime = cells[e.Row, COLUMN_INDEX_HIDE_DATETIME].Text;
                // 第19列是班次
                var shift = cells[e.Row, COLUMN_INDEX_HIDE_SHIFT].Text;

                // 预警id
                var warningIdList = new List<string>();
                warningIdList = PreWarningLastedResultQueryBLL.GetWarningIdListWithTunnelId(tunnelId);
                //warningIdList.Add(cells[e.Row, COLUMN_INDEX_HIDE_OVERLIMIT_WARNING_ID].Text);
                //warningIdList.Add(cells[e.Row, COLUMN_INDEX_HIDE_OUTBURST_WARNING_ID].Text);

                if (workface == "" || dateTime == "" || shift == "" || tunnelId == "")
                {
                    return;
                }

                // 处理状态
                var sHandleStatus = cells[e.Row, COLUMN_INDEX_HIDE_OUTBURST_HANDLE_STATUS].Text;

                //设置传出值
                //工作面名称
                OutInfo.WarkingFaceName = workface;
                //日期
                OutInfo.DateTime = dateTime;
                //班次
                OutInfo.DateShift = shift;
                //巷道ID
                OutInfo.TunnelId = tunnelId;

                OutInfo.WarningIdList = warningIdList;

                var pwrdq = new PreWarningResultDetailsQuery(OutInfo, false);
                pwrdq.ShowDialog();
                //pwrdq.Show();//不能用Show，否则点击详细信息按钮时会弹回至实时预警
            }
        }

        /// <summary>
        ///     语音进程的开启与关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _chkAddPreWarningVoice_CheckedChanged(object sender, EventArgs e)
        {
            if (!_chkAddPreWarningVoice.Checked)
            {
                _speaker.Stop();
            }
        }

        public void SpeakerThreadFunction()
        {
            try
            {
                // do any background work
                _speaker.Speak();
            }
            catch (Exception ex)
            {
                // log errors
            }
        }

        private void PreWarningLastedResultQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainFormWm.GetLatestWarningResultInstance().Hide();
            e.Cancel = true;
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            //设置最新预警结果窗体为null
            MainFormWm.CleanLatestWarningResult();

            Close();
            Dispose();
            GC.Collect();
        }

        /// <summary>
        ///     确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnOK_Click(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnCancle_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void PreWarningLastedResultQuery_Load(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
            var msg = new SocketMessage(COMMAND_ID.UPDATE_WARNING_RESULT, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //LoadValue("");
            //SocketMessage msg = new SocketMessage(COMMAND_ID.REGISTER_WARNING_RESULT_NOTIFICATION_ALL, DateTime.Now);
            //SendMsg2Server(msg);
        }
    }
}