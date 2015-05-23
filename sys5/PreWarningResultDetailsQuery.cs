using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeScales.Http;
using CodeScales.Http.Common;
using CodeScales.Http.Entity;
using CodeScales.Http.Methods;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using LibBusiness;
using LibCommon;
using LibConfig;
using LibEntity;
using Newtonsoft.Json;
using _5.WarningManagement;

namespace sys5
{
    public partial class PreWarningResultDetailsQuery : Form
    {
        //定义初始时FarPoint行列数
        private const int COULMN_COUNT = 19;
        private const int FROZEN_ROW_COUNT = 2;
        //各列索引
        private const int COLUMN_INDEX_ID = 0; // 编号
        private const int COLUMN_INDEX_DATE_TIME = 1; // 时间
        private const int COLUMN_INDEX_WANRING_TYPE = 2; // 预警类型:突出，超限
        private const int COLUMN_INDEX_RULE_TYPE = 3; // 规则类型/预警依据：瓦斯，煤层赋存......
        private const int COLUMN_INDEX_WARNING_LEVEL = 4; // 预警级别/预警结果
        private const int COLUMN_INDEX_RULE_CODE = 5; // 规则编码
        private const int COLUMN_INDEX_RULE_DESCRIPTION = 6; // 规则描述
        private const int COLUMN_INDEX_STANDARD_VALUE = 7; // 标准值
        private const int COLUMN_INDEX_ACTUAL_VALUE = 8; // 实际值
        private const int COLUMN_INDEX_ACTIONS = 9; // 采取措施
        private const int COLUMN_INDEX_ACTIONS_PERSON = 10; // 解除措施
        private const int COLUMN_INDEX_ACTIONS_DATE_TIME = 11; // 措施添加日期
        private const int COLUMN_INDEX_COMMENTS = 12; // 措施评价. 
        private const int COLUMN_INDEX_COMMENTS_PERSON = 13; // 措施评价人. 
        private const int COLUMN_INDEX_COMMENTS_DATE_TIME = 14; // 措施评价时间. 
        private const int COLUMN_INDEX_LIFT_PERSON = 15; // 预警解除人
        private const int COLUMN_INDEX_LIFT_DATE_TIME = 16; // 预警解除时间
        // 下面几列是隐藏起来的
        private const int COLUMN_INDEX_HIDE_DATA_ID = 17; // 数据ID
        private const int COLUMN_INDEX_HIDE_WARNING_ID = 18; // 预警ID
        // 操作类型
        private const string OPERATION = "operation";
        private const int OPERATION_ACTION = 0; // 采取措施
        private const int OPERATION_COMMENTS = 1; // 措施评价
        private const int OPERATION_WARNING_LIFT = 2; // 预警解除
        public static List<String> ImgList = new List<string>();
        //设置背景颜色,背景颜色为白与RGB（200,250,160）交替设置
        private bool _bChangeBackClolor;
        // 全局变量
        private int _iRowCount = 2;
        //巷道ID
        private int _tunnelId = Const.INVALID_ID;
        private HttpClient client = new HttpClient();
        private ImgPreview imgPreview;
        private bool isDirty = false;
        private int sHandleStatus = 3; // 处理状态
        private UploadImg uploadImg;
        public String warningId;
        //UNHANDLED   (0, "未处理"),
        //ACCESSING   (1, "待评价"),
        //WAITING_LIFT(2, "待解除"),
        //HANDLED     (3, "评价通过,预警解除");

        private EarlyWarningDetail[] warningResultDetails;
        //需要过滤的列索引
        private readonly int[] _filterColunmIdxs;
        //其他窗体传入信息
        private readonly Cells cells;
        private readonly bool isHistory;
        private readonly string restPort = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_REST_PORT);
        private readonly string serverIp = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_SERVER_IP);

        /// <summary>
        /// </summary>
        /// <param name="sOutTunnelId">Tunnel WirePointName</param>
        /// <param name="sOutWorkface">Work Face Name</param>
        /// <param name="sOutDate">Date</param>
        /// <param name="sOutShift">班次</param>
        /// <param name="sOutWarningResult">预警结果</param>
        /// <param name="sOutWarningType">预警类型</param>
        /// <param name="sOutWarningItem">预警项目：瓦斯/地质构造/煤层赋存/通风/管理</param>
        /// <param name="isHistory"></param>
        public PreWarningResultDetailsQuery(WorkingFace workingFace, DateTime dateTime, string shift,
            int warningResult, string warningType,
            string ruleType, bool isHistory)
        {
            InitializeComponent();

            cells = _fpPreWarningResultDetials.ActiveSheet.Cells;

            this.isHistory = isHistory;

            #region Farpoint自动过滤功能

            //初始化需要过滤功能的列
            _filterColunmIdxs = new[]
            {
                3
            };
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(_fpPreWarningResultDetials, _filterColunmIdxs);

            #endregion

            //设置窗体的样式及显示名称
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this,
                Const_WM.PREWARNING_RESULT_DETAILS_QUERY);

            //定义Farpoint的行列数
            _fpPreWarningResultDetials.ActiveSheet.Rows.Count = FROZEN_ROW_COUNT;
            _fpPreWarningResultDetials.ActiveSheet.Columns.Count = COULMN_COUNT;

            // 加载预警结果详细信息。
            //LoadWarningResultDetail(sOutTunnelId,
            //            sOutWorkface, sOutDate, sOutShift,
            //            sOutWarningResult, sOutWarningType, sOutWarningItem);
            LoadWarningResultDetail(workingFace.WorkingFaceId, dateTime, shift,
                warningResult, warningType, ruleType);


            _txtDateShift.Text = shift; // 班次
            _txtDateTime.Text = dateTime.ToString("yyyy-MM-dd"); // 日期时间
            _txtWorkingFace.Text = workingFace.WorkingFaceName; // 工作面

            _fpPreWarningResultDetials.Focus();
            _fpPreWarningResultDetials.SetCursor(CursorType.Normal, Cursors.Hand);

            // 隐藏下面的几列
            _fpPreWarningResultDetials.ActiveSheet.Columns[COLUMN_INDEX_HIDE_DATA_ID].Visible = false;
            _fpPreWarningResultDetials.ActiveSheet.Columns[COLUMN_INDEX_HIDE_WARNING_ID].Visible = false;

            // 历史信息和实时信息都要显示在这一个页面中，因此需要设置某些空间的Enabled/Visible属性。
            btnDoIt.Visible = !isHistory;
            _btnUploadImg.Visible = !isHistory;
            _btnDeleteImg.Visible = !isHistory;
            RefreshImgListFromDb();
        }

        /// <summary>
        /// </summary>
        /// <param name="inInfo">传入值</param>
        /// <param name="isHistory">是历史数据还是实时数据</param>
        public PreWarningResultDetailsQuery(dynamic inInfo, bool isHistory)
        {
            InitializeComponent();
            _tunnelId = inInfo.TunnelId;
            cells = _fpPreWarningResultDetials.ActiveSheet.Cells;

            this.isHistory = isHistory;

            #region Farpoint自动过滤功能

            //初始化需要过滤功能的列
            _filterColunmIdxs = new[]
            {
                3
            };
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(_fpPreWarningResultDetials, _filterColunmIdxs);

            #endregion

            //设置窗体的样式及显示名称
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this,
                Const_WM.PREWARNING_RESULT_DETAILS_QUERY);

            //定义Farpoint的行列数
            _fpPreWarningResultDetials.ActiveSheet.Rows.Count = FROZEN_ROW_COUNT;
            _fpPreWarningResultDetials.ActiveSheet.Columns.Count = COULMN_COUNT;

            // 加载预警结果详细信息。
            LoadWarningResultDetail(inInfo.WarningIdList);

            _txtDateShift.Text = inInfo.DateShift; // 班次
            _txtDateTime.Text = inInfo.DateTime; // 日期时间
            _txtWorkingFace.Text = inInfo.WarkingFaceName; // 工作面

            _fpPreWarningResultDetials.Focus();
            _fpPreWarningResultDetials.SetCursor(CursorType.Normal, Cursors.Hand);

            // 隐藏下面的几列
            _fpPreWarningResultDetials.ActiveSheet.Columns[COLUMN_INDEX_HIDE_DATA_ID].Visible = false;
            _fpPreWarningResultDetials.ActiveSheet.Columns[COLUMN_INDEX_HIDE_WARNING_ID].Visible = false;

            // 历史信息和实时信息都要显示在这一个页面中，因此需要设置某些空间的Enabled/Visible属性。
            btnDoIt.Visible = !isHistory;
            RefreshImgListFromDb();
        }

        private string GetReadableHandleStatus(int status)
        {
            switch (status)
            {
                case 0:
                    btnDoIt.Text = @"提交措施";
                    return "未处理, 请采取措施";
                case 1:
                    btnDoIt.Text = @"提交评价";
                    return "待评价";
                case 2:
                    // 隐藏提交按钮
                    btnDoIt.Text = @"解除预警";
                    return "评价通过,待解除";
                case 3:
                    // 隐藏提交按钮
                    btnDoIt.Visible = false;
                    return "预警解除完毕";
            }
            return "";
        }

        private int getWarningLevel(string sWarningLevel)
        {
            switch (sWarningLevel)
            {
                case "红色预警":
                    return 0;
                case "黄色预警":
                    return 1;
            }
            return 2;
        }

        /// <summary>
        ///     向指定的FpSpread中添加一行
        /// </summary>
        /// <param name="details"></param>
        private void addOneRowToFpSpread(int rowIdx, EarlyWarningDetail details, FpSpread fp)
        {
            fp.ActiveSheet.Rows.Add(rowIdx, 1);
            fp.ActiveSheet.Rows[rowIdx].Height = 30;
            fp.ActiveSheet.Rows[rowIdx].Locked = true;

            sHandleStatus = details.EarlyWarningResult.HandleStatus;
            lblStatus.Text = GetReadableHandleStatus(sHandleStatus); // 处理状态

            // 编号合并信息
            var idSpanInfo = new SpanCell(fp) { RowIdx = rowIdx, ColIdx = COLUMN_INDEX_ID, SpanColCnt = 1 };
            idSpanInfo.setText((rowIdx - 1).ToString());

            // 时间合并信息
            var dateSpanInfo = new SpanCell(fp) { RowIdx = rowIdx, ColIdx = COLUMN_INDEX_DATE_TIME, SpanColCnt = 1 };

            dateSpanInfo.setText(details.EarlyWarningResult.DateTime.ToString("yyyy-MM-dd"));

            //预警类型合并信息
            var warningTypeSpanInfo = new SpanCell(fp)
            {
                RowIdx = rowIdx,
                ColIdx = COLUMN_INDEX_WANRING_TYPE,
                SpanColCnt = 1
            };
            //超限/突出
            if (details.EarlyWarningResult.WarningType == 0)
            {
                warningTypeSpanInfo.setText(WarningTypeCHN.超限预警.ToString());
            }
            if (details.EarlyWarningResult.WarningType == 1)
            {
                warningTypeSpanInfo.setText(WarningTypeCHN.突出预警.ToString());
            }

            // 规则类型/预警依据：瓦斯，煤层赋存......
            var ruleTypeSpanInfo = new SpanCell(fp) { RowIdx = rowIdx, ColIdx = COLUMN_INDEX_RULE_TYPE, SpanColCnt = 1 };
            ruleTypeSpanInfo.setText(details.PreWarningRules.RuleType);

            // 预警级别/预警结果
            FpUtil.setCellImg(cells[rowIdx, COLUMN_INDEX_WARNING_LEVEL], getWarningLevel(details.PreWarningRules.WarningLevel));

            //预警编号单元格样式
            cells[rowIdx, COLUMN_INDEX_ID].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_ID].VerticalAlignment = CellVerticalAlignment.Center;

            //预警类型单元格样式
            cells[rowIdx, COLUMN_INDEX_WANRING_TYPE].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_WANRING_TYPE].VerticalAlignment = CellVerticalAlignment.Center;

            //预警依据单元格样式
            cells[rowIdx, COLUMN_INDEX_RULE_TYPE].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_RULE_TYPE].VerticalAlignment = CellVerticalAlignment.Center;

            // 规则编码
            cells[rowIdx, COLUMN_INDEX_RULE_CODE].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_RULE_CODE].Locked = true;
            cells[rowIdx, COLUMN_INDEX_RULE_CODE].Text = details.PreWarningRules.RuleCode;
            cells[rowIdx, COLUMN_INDEX_RULE_CODE].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_RULE_CODE].VerticalAlignment = CellVerticalAlignment.Center;

            // 时间
            cells[rowIdx, COLUMN_INDEX_DATE_TIME].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_DATE_TIME].Locked = true;
            cells[rowIdx, COLUMN_INDEX_DATE_TIME].Text = details.EarlyWarningResult.DateTime.ToString("yyyy-MM-dd");
            cells[rowIdx, COLUMN_INDEX_DATE_TIME].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_DATE_TIME].VerticalAlignment = CellVerticalAlignment.Center;

            //规则描述
            cells[rowIdx, COLUMN_INDEX_RULE_DESCRIPTION].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_RULE_DESCRIPTION].Locked = true;
            cells[rowIdx, COLUMN_INDEX_RULE_DESCRIPTION].Text = details.PreWarningRules.RuleDescription;
            cells[rowIdx, COLUMN_INDEX_RULE_DESCRIPTION].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_RULE_DESCRIPTION].VerticalAlignment = CellVerticalAlignment.Center;

            //标准值
            cells[rowIdx, COLUMN_INDEX_STANDARD_VALUE].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_STANDARD_VALUE].Locked = true;
            cells[rowIdx, COLUMN_INDEX_STANDARD_VALUE].Text = details.Threshold;
            cells[rowIdx, COLUMN_INDEX_STANDARD_VALUE].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_STANDARD_VALUE].VerticalAlignment = CellVerticalAlignment.Center;

            //实际值
            cells[rowIdx, COLUMN_INDEX_ACTUAL_VALUE].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_ACTUAL_VALUE].Locked = true;
            cells[rowIdx, COLUMN_INDEX_ACTUAL_VALUE].Text = details.ActualValue;
            cells[rowIdx, COLUMN_INDEX_ACTUAL_VALUE].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_ACTUAL_VALUE].VerticalAlignment = CellVerticalAlignment.Center;

            // 解除措施
            cells[rowIdx, COLUMN_INDEX_ACTIONS].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_ACTIONS].Locked = isHistory;
            cells[rowIdx, COLUMN_INDEX_ACTIONS].Text = details.Actions;
            cells[rowIdx, COLUMN_INDEX_ACTIONS].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_ACTIONS].VerticalAlignment = CellVerticalAlignment.Center;

            // 解除措施录入人
            cells[rowIdx, COLUMN_INDEX_ACTIONS_PERSON].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_ACTIONS_PERSON].Locked = true; //this.isHistory;
            cells[rowIdx, COLUMN_INDEX_ACTIONS_PERSON].Text = details.ActionsPerson;
            cells[rowIdx, COLUMN_INDEX_ACTIONS_PERSON].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_ACTIONS_PERSON].VerticalAlignment = CellVerticalAlignment.Center;

            // 解除措施录入时间
            cells[rowIdx, COLUMN_INDEX_ACTIONS_DATE_TIME].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_ACTIONS_DATE_TIME].Locked = true; //this.isHistory;
            cells[rowIdx, COLUMN_INDEX_ACTIONS_DATE_TIME].Text = details.ActionsDateTime.ToString();
            cells[rowIdx, COLUMN_INDEX_ACTIONS_DATE_TIME].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_ACTIONS_DATE_TIME].VerticalAlignment = CellVerticalAlignment.Center;

            // 措施评价
            cells[rowIdx, COLUMN_INDEX_COMMENTS].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_COMMENTS].Locked = isHistory;
            cells[rowIdx, COLUMN_INDEX_COMMENTS].Text = details.Comments;
            cells[rowIdx, COLUMN_INDEX_COMMENTS].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_COMMENTS].VerticalAlignment = CellVerticalAlignment.Center;

            // 措施评价人
            cells[rowIdx, COLUMN_INDEX_COMMENTS_PERSON].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_COMMENTS_PERSON].Locked = true; //this.isHistory;
            cells[rowIdx, COLUMN_INDEX_COMMENTS_PERSON].Text = details.CommentsPerson;
            cells[rowIdx, COLUMN_INDEX_COMMENTS_PERSON].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_COMMENTS_PERSON].VerticalAlignment = CellVerticalAlignment.Center;

            // 措施评价日期
            cells[rowIdx, COLUMN_INDEX_COMMENTS_DATE_TIME].CellType = new TextCellType();
            cells[rowIdx, COLUMN_INDEX_COMMENTS_DATE_TIME].Locked = true; //this.isHistory;
            cells[rowIdx, COLUMN_INDEX_COMMENTS_DATE_TIME].Text = details.CommentsDateTime.ToString();
            cells[rowIdx, COLUMN_INDEX_COMMENTS_DATE_TIME].HorizontalAlignment = CellHorizontalAlignment.Center;
            cells[rowIdx, COLUMN_INDEX_COMMENTS_DATE_TIME].VerticalAlignment = CellVerticalAlignment.Center;

            // DataId--此列隐藏
            cells[rowIdx, COLUMN_INDEX_HIDE_DATA_ID].Text = details.Id;
            cells[rowIdx, COLUMN_INDEX_HIDE_WARNING_ID].Text = details.EarlyWarningResult.Id.ToString();

            ////合并预警类型, 设置预警依据       
            //warningTypeSpanInfo.applyCellsSpan();
            //warningTypeSpanInfo.setText(entity.WarningType);

            ////合并时间          
            //dateSpanInfo.SpanRowCnt = warningTypeSpanInfo.SpanRowCnt;
            //dateSpanInfo.applyCellsSpan();


            ////合并编号      
            //idSpanInfo.SpanRowCnt = warningTypeSpanInfo.SpanRowCnt;
            //idSpanInfo.applyCellsSpan();
            ////设置编号
            //idSpanInfo.setText(entity.WirePointName.ToString());

            //背景色设置为淡绿
            //设置背景颜色,采取白与RGB（200, 250, 160）交替设置
            if (!_bChangeBackClolor)
            {
                for (var iRow = idSpanInfo.RowIdx; iRow < idSpanInfo.RowIdx + idSpanInfo.SpanRowCnt; iRow++)
                {
                    fp.ActiveSheet.Rows[iRow].BackColor = Color.FromArgb(200, 250, 160);
                }
                fp.ActiveSheet.Rows[rowIdx].BackColor = Color.FromArgb(200, 250, 160);
                _bChangeBackClolor = true;
            }
            else
            {
                _bChangeBackClolor = false;
            }
        }

        /// <summary>
        ///     加载预警详细信息
        /// </summary>
        private void LoadWarningResultDetail(int workingFaceId, DateTime dateTime, string shift,
            int warningResult, string warningType,
            string ruleType)
        {
            //测试代码耗时
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _iRowCount = 2;

            // 删除垃圾数据
            while (_fpPreWarningResultDetials.ActiveSheet.Rows.Count > FROZEN_ROW_COUNT)
            {
                _fpPreWarningResultDetials.ActiveSheet.Rows.Remove(FROZEN_ROW_COUNT, 1);
            }

            warningResultDetails = EarlyWarningDetail.FindAllByProperty("EarlyWarningResult.Gas", 1);
            warningResultDetails = EarlyWarningDetail.FindAllByMutiCondition(workingFaceId,
                dateTime, shift, warningResult, warningType, ruleType);
            // 向farpoint spread中添加数据。
            foreach (var t in warningResultDetails)
            {
                addOneRowToFpSpread(_iRowCount++, t, _fpPreWarningResultDetials);
            }

            //设置焦点
            _fpPreWarningResultDetials.ActiveSheet.SetActiveCell(0, 0);


            var list = new List<string>();

            warningId = "";
            foreach (var t in warningResultDetails.Where(t => !list.Contains(t.EarlyWarningResult.Id.ToString())))
            {
                list.Add(t.EarlyWarningResult.Id.ToString());
            }

            foreach (var t in list)
            {
                warningId += t + ",";
            }
            if (warningId.Length > 0)
                warningId = warningId.Substring(0, warningId.Length - 1);
            //测试代码耗时
            stopwatch.Stop();
            var timespan = stopwatch.Elapsed;
            double hours = timespan.Hours;
            double minutes = timespan.Minutes;
            double seconds = timespan.Seconds;
            double milliseconds = timespan.Milliseconds;
            var timetext = "查询耗时：" + hours + "小时" + minutes + "分" + seconds + "秒" + milliseconds + "毫秒";
        }

        /// <summary>
        ///     加载预警详细信息
        /// </summary>
        /// <param name="warningIdList">对应预警信息表中的Id(T_EARLY_WARNING_RESULT的主键)</param>
        private void LoadWarningResultDetail(int[] warningIdList)
        {
            //测试代码耗时
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _iRowCount = 2;


            // 删除垃圾数据
            while (_fpPreWarningResultDetials.ActiveSheet.Rows.Count > FROZEN_ROW_COUNT)
            {
                _fpPreWarningResultDetials.ActiveSheet.Rows.Remove(FROZEN_ROW_COUNT, 1);
            }

            //提取传入值
            //var date = _inInfo.DateTime;
            //var date_shift = _inInfo.DateShift;
            //var tunnelID = _inInfo.TunnelId;

            warningResultDetails = EarlyWarningDetail.FindAllByWarningId(warningIdList);
            // 向farpoint spread中添加数据。
            foreach (var t in warningResultDetails)
            {
                addOneRowToFpSpread(_iRowCount++, t, _fpPreWarningResultDetials);
            }

            //设置焦点
            _fpPreWarningResultDetials.ActiveSheet.SetActiveCell(0, 0);

            //测试代码耗时
            stopwatch.Stop();
            var timespan = stopwatch.Elapsed;
            double hours = timespan.Hours;
            double minutes = timespan.Minutes;
            double seconds = timespan.Seconds;
            double milliseconds = timespan.Milliseconds;
            var timetext = "查询耗时：" + hours + "小时" + minutes + "分" + seconds + "秒" + milliseconds + "毫秒";
        }

        /// <summary>
        ///     获取预警结果的详细信息
        /// </summary>
        /// <param name="warningId">预警结果Id</param>
        /// <returns>预警结果详细信息列表</returns>
        /// <summary>
        ///     获取历史预警结果的详细信息
        /// </summary>
        /// <param name="warningId">预警结果Id</param>
        ///// <returns>预警结果详细信息列表</returns>
        //public List<WarningResultDetailEntity> getHistoryWarningResultDetails(string sTunnelId,
        //                string sDate, string sShift,
        //                string sWarningResult, string sWarningType,
        //                string sWarningItem)
        //{
        //    HttpPost postMethod = new HttpPost(new Uri("http://" + serverIp + ":" + restPort + "/rest/warnings/history"));
        //    List<NameValuePair> nameValuePairList = new List<NameValuePair>();
        //    nameValuePairList.Add(new NameValuePair("Tunnel", sTunnelId));
        //    nameValuePairList.Add(new NameValuePair("Date", sDate));
        //    nameValuePairList.Add(new NameValuePair("Shift", sShift));        //班次
        //    nameValuePairList.Add(new NameValuePair("WarningResult", sWarningResult)); // 预警结果
        //    nameValuePairList.Add(new NameValuePair("WarningType", sWarningType));  // 预警类型
        //    nameValuePairList.Add(new NameValuePair("WarningItem", sWarningItem));   // 

        //    UrlEncodedFormEntity formEntity = new UrlEncodedFormEntity(nameValuePairList, Encoding.UTF8);
        //    postMethod.Entity = formEntity;
        //    HttpResponse response = client.Execute(postMethod);

        //    int responseCode = response.ResponseCode;
        //    if (responseCode < 400)
        //    {
        //        string content = EntityUtils.ToString(response.Entity);

        //        List<WarningResultDetailEntity> entityList =
        //            (List<WarningResultDetailEntity>)Newtonsoft.Json.JsonConvert.DeserializeObject(content, typeof(List<WarningResultDetailEntity>));
        //        return entityList;
        //    }

        //    return null;
        //}
        //public List<EarlyWarningResult> getHistoryWarningResultDetails(string sWorkingface,
        //    string sDate, string sShift,
        //    string sWarningResult, string sWarningType,
        //    string sWarningItem)
        //{
        //    return PreWarningDetailsBLL.getHistoryWarningResultDetails(sWorkingface, sDate, sShift, sWarningResult,
        //        sWarningType, sWarningItem);
        //}

        //public List<WarningResultDetail> getHistoryWarningResultDetails(string warningId)
        //{
        //    return PreWarningDetailsBLL.getHistoryWarningResultDetails(warningId);
        //}

        private bool isEmptyString(String str)
        {
            if (string.IsNullOrEmpty(str) || str == "")
                return true;
            return false;
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        // 采取措施
        [Authorize(Roles = "管理员,通风管理员")]
        private void doActions()
        {
            //PojoWarningContainer container = new PojoWarningContainer();
            //container.Tunnel = _inInfo.Tunnel;

            //Dictionary<string, PojoWarning> warnings = new Dictionary<string, PojoWarning>();

            //for (int i = FROZEN_ROW_COUNT; i < this._fpPreWarningResultDetials.ActiveSheet.RowCount; i++)
            //{
            //    PojoWarningDetail detail = new PojoWarningDetail();
            //    detail.WirePointName = cells[i, COLUMN_INDEX_HIDE_DATA_ID].Text;
            //    detail.Comments = cells[i, COLUMN_INDEX_COMMENTS].Text;
            //    detail.Actions = cells[i, COLUMN_INDEX_ACTIONS].Text;
            //    detail.ActionsPerson = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_CURRENT_USER);
            //    detail.ActionsDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    detail.RuleId = warningResultDetails.Find(u => u.WirePointName == detail.WirePointName).RuleId;
            //    detail.DataId = warningResultDetails.Find(u => u.WirePointName == detail.WirePointName).DataId;

            //    string warningId = cells[i, COLUMN_INDEX_HIDE_WARNING_ID].Text;

            //    PojoWarning warning = null;
            //    if (warnings.ContainsKey(warningId))
            //        warning = warnings[warningId];
            //    else
            //    {
            //        warning = new PojoWarning();
            //        warning.WarningId = warningId;
            //        warnings.Add(warningId, warning);
            //    }

            //    warning.addDetail(detail);

            //    if (isEmptyString(detail.Actions))
            //    {
            //        MessageBox.Show("请填写每一条规则的预警解除措施。");
            //        return;
            //    }
            //}

            //foreach (KeyValuePair<string, PojoWarning> pair in warnings)
            //{
            //    container.addWarning(pair.Value);
            //}

            //doSubmit(container, "措施待评价");

            //Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));

            doSubmit("Actions", "请填写每一条规则的预警解除措施。", "评价成功，预警待解除");
        }

        // 措施评价 
        private void doComments()
        {
            doSubmit("Comments", "请填写每一条的措施评价", "评价成功，预警待解除");
            //Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));
        }

        // 预警解除
        private void doWarningLift()
        {
            doSubmit("WarningLift", "请填写每一条的预警解除措施以及措施评价", "预警解除");

            //PojoWarningContainer container = new PojoWarningContainer();
            //container.Tunnel = _inInfo.Tunnel;


            //Dictionary<string, PojoWarning> warnings = new Dictionary<string, PojoWarning>();

            //for (int i = FROZEN_ROW_COUNT; i < this._fpPreWarningResultDetials.ActiveSheet.RowCount; i++)
            //{
            //    PojoWarningDetail detail = new PojoWarningDetail();
            //    detail.WirePointName = cells[i, COLUMN_INDEX_HIDE_DATA_ID].Text;
            //    detail.Comments = cells[i, COLUMN_INDEX_COMMENTS].Text; // 措施评价
            //    detail.Actions = cells[i, COLUMN_INDEX_ACTIONS].Text; // 措施
            //    detail.LiftPerson = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_CURRENT_USER);// 解除人
            //    detail.LiftDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    detail.RuleId = warningResultDetails.Find(u => u.WirePointName == detail.WirePointName).RuleId;
            //    detail.DataId = warningResultDetails.Find(u => u.WirePointName == detail.WirePointName).DataId;

            //    string warningId = cells[i, COLUMN_INDEX_HIDE_WARNING_ID].Text;

            //    PojoWarning warning = null;
            //    if (warnings.ContainsKey(warningId))
            //        warning = warnings[warningId];
            //    else
            //    {
            //        warning = new PojoWarning();
            //        warning.WarningId = warningId;
            //        warnings.Add(warningId, warning);
            //    }

            //    warning.addDetail(detail);
            //}

            //foreach (KeyValuePair<string, PojoWarning> pair in warnings)
            //{
            //    container.addWarning(pair.Value);
            //}

            //doSubmit(container, "预警解除");
        }

        // 解除预警
        private void btnLiftWarning_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"确实要执行此项操作吗？", "预警处理",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (sHandleStatus == OPERATION_ACTION)
                    doActions(); //提交解除措施
                else if (sHandleStatus == OPERATION_COMMENTS)
                    doComments(); // 提交措施评价
                else if (sHandleStatus == OPERATION_WARNING_LIFT)
                    doWarningLift(); // 提交措施评价
            }
        }

        /// <summary>
        ///     获取各类别预警原因
        ///     目前需要将该数组中的顺序与枚举中的顺序完全一致，否则结果将不正确！！
        /// </summary>
        /// <param name="ents"></param>
        /// <returns></returns>
        private ReasonAndRule[] PreWarningAnalysisResultEntForItems(List<PreWarningAnalysisResult> ents)
        {
            //定义返回值数组
            var returnEnts = new ReasonAndRule[5];

            //定义五个数组，分别对应瓦斯、煤层、地质、通风、管理
            var gas = new ReasonAndRule(new List<PreWarningAnalysisResult>());
            var coal = new ReasonAndRule(new List<PreWarningAnalysisResult>());
            var geology = new ReasonAndRule(new List<PreWarningAnalysisResult>());
            var venlation = new ReasonAndRule(new List<PreWarningAnalysisResult>());
            var management = new ReasonAndRule(new List<PreWarningAnalysisResult>());

            foreach (var ent in ents)
            {
                var ruleEnt = new PreWarningRules();
                if (ent.IsSingleRule())
                {
                    var singleRule = ent.GetSingleRulesResultEnt();
                    ruleEnt = PreWarningRules.Find(singleRule.SingleRuleCodeID);
                }
                else
                {
                    ruleEnt = PreWarningRules.Find(ent.MultiRuleCodeID);
                }

                if (ruleEnt.RuleType == WarningReasonItems.瓦斯.ToString())
                {
                    gas.Reason.Add(ent);
                }
                if (ruleEnt.RuleType == WarningReasonItems.煤层赋存.ToString())
                {
                    coal.Reason.Add(ent);
                }
                if (ruleEnt.RuleType == WarningReasonItems.地质构造.ToString())
                {
                    geology.Reason.Add(ent);
                }
                if (ruleEnt.RuleType == WarningReasonItems.通风.ToString())
                {
                    venlation.Reason.Add(ent);
                }
                if (ruleEnt.RuleType == WarningReasonItems.管理因素.ToString())
                {
                    management.Reason.Add(ent);
                }
            }
            //返回数组赋值，与枚举对应
            returnEnts[(int)WarningReasonItems.瓦斯] = gas;
            returnEnts[(int)WarningReasonItems.煤层赋存] = coal;
            returnEnts[(int)WarningReasonItems.地质构造] = geology;
            returnEnts[(int)WarningReasonItems.通风] = venlation;
            returnEnts[(int)WarningReasonItems.管理因素] = management;

            return returnEnts;
        }

        /// <summary>
        ///     设置自动过滤
        /// </summary>
        /// <param name="warningItem"></param>
        private void FillFarpointCellsWithWarningTypeFilter(string warningItem)
        {
            const int warningTypeFilterIdx = 3;
            _fpPreWarningResultDetials.ActiveSheet.AutoFilterColumn(warningTypeFilterIdx, warningItem, 0);
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpPreWarningResultDetials, false))
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
            FilePrint.CommonPrint(_fpPreWarningResultDetials, 0);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public String NextImg()
        {
            if (_imgList.SelectedIndex < _imgList.Items.Count - 1)
                _imgList.SelectedIndex++;
            return _imgList.SelectedItem.ToString();
        }

        public String PreviousImg()
        {
            if (_imgList.SelectedIndex > 0)
                _imgList.SelectedIndex--;
            return _imgList.SelectedItem.ToString();
        }

        public void RefreshImgList()
        {
            _imgList.Items.Clear();
            for (var i = 0; i < ImgList.Count; i++)
            {
                _imgList.Items.Add(ImgList[i]);
            }
        }

        public void RefreshImgList(string warningId)
        {
        }

        public void RefreshImgListFromDb()
        {
            GetWarningId();
            ImgList = WarningImgBLL.GetFilsNameListWithWarningId(warningId);
            RefreshImgList();
        }

        private void _btnUploadImg_Click(object sender, EventArgs e)
        {
            if (uploadImg == null || uploadImg.IsDisposed)
            {
                uploadImg = new UploadImg(this);
            }
            uploadImg.Show();
            uploadImg.Focus();
        }

        private void _btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (_imgList.SelectedItem == null)
            {
                Alert.alert("请选择要删除的图片");
            }
            else
            {
                ImgList.Remove(_imgList.SelectedItem.ToString());
                WarningImgBLL.DeleteWarningImgWithWarningIdAndFileName(warningId, _imgList.SelectedItem.ToString());
                RefreshImgList();
            }
        }

        private void _imgList_DoubleClick(object sender, EventArgs e)
        {
            if (imgPreview == null || imgPreview.IsDisposed)
            {
                if (_imgList.SelectedItem != null)
                {
                    imgPreview = new ImgPreview(_imgList.SelectedItem.ToString(), this);
                    imgPreview.Show();
                    imgPreview.Focus();
                }
            }
        }

        private void GetWarningId()
        {
            //if (String.IsNullOrEmpty(warningId))
            //{
            //    if (_inInfo != null)
            //    {
            //        for (var i = 0; i < _inInfo.WarningIdList.Count; i++)
            //        {
            //            warningId += _inInfo.WarningIdList[i] + ",";
            //        }
            //        if (!String.IsNullOrEmpty(warningId))
            //        {
            //            warningId = warningId.Substring(0, warningId.Length - 1);
            //        }
            //    }
            //}
            //for (int i = FROZEN_ROW_COUNT; i < this._fpPreWarningResultDetials.ActiveSheet.RowCount; i++)
            //{
            //    warningId += cells[i, COLUMN_INDEX_HIDE_WARNING_ID].Text + ",";
            //}
            //warningId = warningId.Substring(0, warningId.Length - 1);
        }

        public byte[] GetBytesByImagePath(string strFile)
        {
            byte[] photo_byte = null;
            using (var fs = new FileStream(strFile, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    photo_byte = br.ReadBytes((int)fs.Length);
                }
            }
            return photo_byte;
        }

        private void _fpPreWarningResultDetials_CellClick(object sender, CellClickEventArgs e)
        {
            var str =
                _fpPreWarningResultDetials.ActiveSheet.Cells[e.Row, COLUMN_INDEX_HIDE_WARNING_ID].Value == null
                    ? ""
                    : _fpPreWarningResultDetials.ActiveSheet.Cells[e.Row, COLUMN_INDEX_HIDE_WARNING_ID].Value
                        .ToString();
            if (!String.IsNullOrEmpty(str))
            {
                ImgList = WarningImgBLL.GetFilsNameListWithWarningId(str);
                RefreshImgList();
            }
        }

        private void doSubmit(String submitType, String emptyString, String statusString)
        {
            var container = new PojoWarningContainer();
            //container.TunnelId = _inInfo.TunnelId;


            var warnings = new Dictionary<string, PojoWarning>();

            for (var i = FROZEN_ROW_COUNT; i < _fpPreWarningResultDetials.ActiveSheet.RowCount; i++)
            {
                var detail = new PojoWarningDetail { Id = cells[i, COLUMN_INDEX_HIDE_DATA_ID].Text };


                switch (submitType)
                {
                    case "Actions":
                        {
                            detail.Actions = cells[i, COLUMN_INDEX_ACTIONS].Text;
                            detail.ActionsPerson = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_CURRENT_USER);
                            detail.ActionsDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        }
                    case "Comments":
                        {
                            detail.Actions = cells[i, COLUMN_INDEX_ACTIONS].Text;
                            detail.Comments = cells[i, COLUMN_INDEX_COMMENTS].Text;
                            detail.CommentsPerson = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_CURRENT_USER);
                            detail.CommentsDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        }
                    case "WarningLift":
                        {
                            detail.Actions = cells[i, COLUMN_INDEX_ACTIONS].Text;
                            detail.Comments = cells[i, COLUMN_INDEX_COMMENTS].Text;
                            detail.LiftPerson = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_CURRENT_USER);
                            detail.LiftDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        }
                }
                var warningss = warningResultDetails.ToList();
                detail.RuleId = warningss.Find(u => u.Id == detail.Id).PreWarningRules.RuleId.ToString();
                detail.DataId = warningss.Find(u => u.Id == detail.Id).DataId;


                var warningId = cells[i, COLUMN_INDEX_HIDE_WARNING_ID].Text;

                PojoWarning warning = null;
                if (warnings.ContainsKey(warningId))
                    warning = warnings[warningId];
                else
                {
                    warning = new PojoWarning { WarningId = warningId };
                    warnings.Add(warningId, warning);
                }

                warning.addDetail(detail);

                switch (submitType)
                {
                    case "Actions":
                        {
                            if (isEmptyString(detail.Actions))
                            {
                                MessageBox.Show(emptyString);
                                return;
                            }
                            break;
                        }
                    case "Comments":
                        {
                            if (isEmptyString(detail.Comments))
                            {
                                MessageBox.Show(emptyString);
                                return;
                            }
                            break;
                        }
                    case "WarningLift":
                        {
                            break;
                        }
                }
            }

            foreach (var pair in warnings)
            {
                container.addWarning(pair.Value);
            }

            var client1 = new HttpClient();
            var postMethod =
                new HttpPost(
                    new Uri("http://" + serverIp + ":" + restPort + "/rest/tunnels/" + _tunnelId + "/warninglift")
                    );
            var nameValuePairList = new List<NameValuePair>();
            switch (submitType)
            {
                case "Actions":
                    {
                        nameValuePairList.Add(new NameValuePair(OPERATION, OPERATION_ACTION.ToString())); // 采取措施
                        break;
                    }
                case "Comments":
                    {
                        nameValuePairList.Add(new NameValuePair(OPERATION, OPERATION_COMMENTS.ToString())); // 采取措施
                        break;
                    }
                case "WarningLift":
                    {
                        nameValuePairList.Add(new NameValuePair(OPERATION, OPERATION_WARNING_LIFT.ToString())); // 采取措施
                        break;
                    }
            }
            //nameValuePairList.Add(new NameValuePair(OPERATION, OPERATION_ACTION)); // 采取措施
            container.TunnelId = _tunnelId.ToString();
            var jsonStr = JsonConvert.SerializeObject(container); // 措施详细描述
            nameValuePairList.Add(new NameValuePair(Const.JSON_MSG, jsonStr));
            var formEntity = new UrlEncodedFormEntity(nameValuePairList, Encoding.UTF8);
            postMethod.Entity = formEntity;

            var response = client1.Execute(postMethod);

            var responseCode = response.ResponseCode;
            if (responseCode < 400)
            {
                MessageBox.Show("提交成功！" + Encoding.UTF8.GetString(response.Entity.Content));
                lblStatus.Text = statusString;
                btnDoIt.Enabled = false;
            }
            else
            {
                MessageBox.Show("提交失败,请检查是否和服务器正常连接！");
            }
        }

        /// <summary>
        ///     定义结构，盛放预警原因及规则
        /// </summary>
        private struct ReasonAndRule
        {
            private List<PreWarningAnalysisResult> _reason;

            public ReasonAndRule(List<PreWarningAnalysisResult> reason)
            {
                _reason = reason;
            }

            /// <summary>
            ///     预警原因
            /// </summary>
            public List<PreWarningAnalysisResult> Reason
            {
                get { return _reason; }
                set { _reason = value; }
            }
        }
    }
}