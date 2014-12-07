// ******************************************************************
// 概  述：回采巷道信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
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
using LibBusiness;
using LibCommon;
using LibCommonControl;
using GIS.HdProc;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace _3.GeologyMeasure
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TunnelHCManagement : BaseForm
    {
        //****************变量声明***************
        private int _iRecordCount = 0;
        int _rowsCount = 0;      //数据行数
        int _checkCount = 0;     //选择行数
        int _rowDetailStartIndex = 4;
        int _tmpRowIndex = 0;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        WorkingFace tunnelHCEntity = new WorkingFace();
        DataSet _ds = new DataSet();
        int tmpInt = 0;

        //各列索引
        const int COLUMN_INDEX_CHOOSE_BUTTON = 0;      // 选择按钮
        const int COLUMN_INDEX_MINE_NAME = 1;      // 矿井名称
        const int COLUMN_INDEX_HORIZONTAL_NAME = 2;      // 水平名称
        const int COLUMN_INDEX_MINING_AREA_NAME = 3;      // 采区名称
        const int COLUMN_INDEX_WORKING_FACE_NAME = 4;      // 工作面名称
        const int COLUMN_INDEX_TUNNEL_ZY = 5;      // 巷道名称
        const int COLUMN_INDEX_TUNNEL_FY = 6;      // 巷道名称
        const int COLUMN_INDEX_TUNNEL_QY = 7;      // 巷道名称
        const int COLUMN_INDEX_TUNNEL_OTHER = 8;      // 巷道名称
        const int COLUMN_INDEX_TEAM_NAME = 9;      // 对别名称
        const int COLUMN_INDEX_START_DATE = 10;      // 开始时间
        const int COLUMN_INDEX_IS_FINISHED = 11;      // 是否结束
        const int COLUMN_INDEX_STOP_DATE = 12;      // 结束时间
        const int COLUMN_INDEX_WORK_STYLE = 13;     // 工作制式
        const int COLUMN_INDEX_WORK_TIME = 14;     // 班次
        const int COLUMN_INDEX_TUNNEL_ID = 15;     // 巷道id
        const int COLUMN_INDEX_WORKING_FACE_ID = 16;     // id
        //****************************************

        FarPoint.Win.Spread.Cells cells = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public TunnelHCManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //分页委托
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_HC_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpTunnelHCInfo, LibCommon.Const_GM.TUNNEL_HC_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                11,
                12,
                13
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelHCInfo, _filterColunmIdxs);

            cells = this.fpTunnelHCInfo.Sheets[0].Cells;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelHCManagement_Load(object sender, EventArgs e)
        {
            this.bindFpTunnelHCInfo();
        }

        /// <summary>
        /// 分页委托
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            bindFpTunnelHCInfo();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpTunnelHCInfo()
        {
            //清空Farpoint
            FarPointOperate.farPointClear(fpTunnelHCInfo, _rowDetailStartIndex, _rowsCount);

            _checkCount = 0;

            chkSelAll.Checked = false;
            // ※分页必须
            _iRecordCount = TunnelInfoBLL.selectTunnelHCCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            _ds = TunnelInfoBLL.selectTunnelHC(iStartIndex, iEndIndex);
            _rowsCount = _ds.Tables[0].Rows.Count;
            //重绘Farpoint
            FarPointOperate.farPointReAdd(fpTunnelHCInfo, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //取消三选
                ckbxcell.ThreeState = false;

                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = _ds.Tables[0].Rows[i];
                    int workingFaceId = Convert.ToInt32(dr[WorkingFaceDbConstNames.WORKINGFACE_ID]);
                    WorkingFace entity = BasicInfoManager.getInstance().getWorkingFaceById(workingFaceId);

                    entity.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(entity.WorkingFaceID));

                    Tunnel tunnelZY = null, tunnelFY = null, tunnelQY = null;
                    string otherTunnel = "";
                    foreach (Tunnel tunnel in entity.tunnelSet)
                    {
                        if (tunnel.TunnelType == TunnelTypeEnum.STOPING_ZY)
                            tunnelZY = tunnel;//主运顺槽
                        else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_FY)
                            tunnelFY = tunnel;//辅运顺槽
                        else if (tunnel.TunnelType == TunnelTypeEnum.STOPING_QY)
                            tunnelQY = tunnel;//开切眼
                        else
                        {
                            if (tunnel.TunnelType == TunnelTypeEnum.STOPING_OTHER)
                            {
                                otherTunnel += " " + tunnel.TunnelName; //其他关联巷道
                            }
                        }
                    }


                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_CHOOSE_BUTTON].CellType = ckbxcell;

                    //矿井名称
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_MINE_NAME].Text = entity.MiningArea.Horizontal.Mine.MineName;
                    //水平
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_HORIZONTAL_NAME].Text = entity.MiningArea.Horizontal.HorizontalName;
                    //采区
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_MINING_AREA_NAME].Text = entity.MiningArea.MiningAreaName;
                    //工作面
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_NAME].Text = entity.WorkingFaceName;
                    //主运顺槽
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_ZY].Text = tunnelZY != null ? tunnelZY.TunnelName : "";
                    //辅运顺槽
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_FY].Text = tunnelFY != null ? tunnelFY.TunnelName : "";
                    //开切眼
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_QY].Text = tunnelQY != null ? tunnelQY.TunnelName : "";
                    //其他关联
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_OTHER].Text = otherTunnel;
                    //队别
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TEAM_NAME].Text = BasicInfoManager.getInstance().getTeamNameById(entity.TeamNameID);

                    //开工日期
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_START_DATE].Text = string.Format("{0:YYYY-MM-dd}", entity.StartDate);

                    //是否回采完毕
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_IS_FINISHED].Text = entity.IsFinish == Const.FINISHED ? Const.MSG_YES : Const.MSG_NO;
                    //停工日期
                    if (entity.IsFinish == Const.FINISHED)
                    {
                        cells[_rowDetailStartIndex + i, COLUMN_INDEX_STOP_DATE].Text = string.Format("{0:YYYY-MM-dd}", entity.StopDate);
                    }

                    //工作制式
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORK_STYLE].Text = entity.WorkStyle;
                    //班次
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORK_TIME].Text = entity.WorkTime;

                    // 隐藏列，
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_TUNNEL_ID].Text = "1";
                    cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Text = entity.WorkingFaceID.ToString();
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpStopLineInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                if (fpChk.Checked)
                {
                    _checkCount++;
                }
                else
                {
                    _checkCount--;
                }
            }
            if (_checkCount == _rowsCount)
            {
                chkSelAll.Checked = true;
            }
            else
            {
                chkSelAll.Checked = false;
            }
            setButtenEnable();
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            if (_checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            if (_checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 为变量tunnelHCEntity赋值
        /// </summary>
        private void setTunnelHCEntityValue()
        {
            int searchCount = _rowsCount;
            int rowDetailStartIndex = 4;
            for (int i = 0; i < _rowsCount; i++)
            {

                if (cells[rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[rowDetailStartIndex + i, 0].Value == true)
                {
                    if (!String.IsNullOrEmpty(cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Text))
                        tunnelHCEntity = BasicInfoManager.getInstance().getWorkingFaceById(
                            Convert.ToInt32(cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Text));
                }
            }
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //TunnelHCEntering tunnelHCForm = new TunnelHCEntering();
            TunnelHCEntering tunnelHCForm = new TunnelHCEntering(this.MainForm);

            if (DialogResult.OK == tunnelHCForm.ShowDialog())
            {
                bindFpTunnelHCInfo();
                this.dataPager1.btnLastPage_Click(sender, e);
                FarPointOperate.farPointFocusSetAdd(fpTunnelHCInfo, _rowDetailStartIndex, _rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setTunnelHCEntityValue();
            TunnelHCEntering tunnelHCForm = new TunnelHCEntering(tunnelHCEntity, this.MainForm);
            if (DialogResult.OK == tunnelHCForm.ShowDialog())
            {
                bindFpTunnelHCInfo();
                FarPointOperate.farPointFocusSetChange(fpTunnelHCInfo, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                int searchCount = _rowsCount;
                bool bResult = false;
                for (int i = 0; i < _rowsCount; i++)
                {
                    DataRow dr = _ds.Tables[0].Rows[i];
                    _tmpRowIndex = this.fpTunnelHCInfo.Sheets[0].ActiveRowIndex;
                    //遍历“选择”是否选中
                    if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        tunnelHCEntity = BasicInfoManager.getInstance().getWorkingFaceById(Convert.ToInt32(cells[_rowDetailStartIndex + i, COLUMN_INDEX_WORKING_FACE_ID].Value));

                        //删除回采巷道信息
                        bResult = WorkingFaceBLL.deleteHCWorkingFace(tunnelHCEntity);
                    }
                }
                if (bResult)
                {
                    Tunnel tunnelZY = null;
                    Tunnel tunnelFY = null;
                    Tunnel tunnelQY = null;

                    HashSet<Tunnel> tSet = tunnelHCEntity.tunnelSet;
                    foreach (Tunnel entity in tSet)
                    {
                        if (entity.TunnelType == TunnelTypeEnum.STOPING_ZY) //主运
                            tunnelZY = entity;
                        if (entity.TunnelType == TunnelTypeEnum.STOPING_FY) // 辅运
                            tunnelFY = entity;
                        if (entity.TunnelType == TunnelTypeEnum.STOPING_QY) // 切眼
                            tunnelQY = entity;
                    }

                    //TODO:删除后事件
                    //将图层中对应的信息删除
                    DelHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId);
                    //删除工作面中对应的回采信息
                    /////Mark

                }
                bindFpTunnelHCInfo();
                FarPointOperate.farPointFocusSetDel(fpTunnelHCInfo, _tmpRowIndex);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 回采删除信息
        /// </summary>
        /// <param name="hd1"></param>
        /// <param name="hd2"></param>
        /// <param name="qy"></param>
        private void DelHcjc(int hd1, int hd2, int qy)
        {
            Dictionary<string, string> hdids = new Dictionary<string, string>();
            hdids["HdId"] = hd1.ToString() + "_" + hd2.ToString();
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
            if (selcjqs.Count > 0)
            {
                for (int i = 0; i < selcjqs.Count; i++)
                {
                    IFeature fea = selcjqs[i].Item1 as IFeature;
                    Global.commonclss.DelFeature(Global.hcqlyr, fea);
                }
            }
        }
        private string[] SplitString(string p)
        {
            string[] sArray = new string[10];

            if ((p != null) && (p != ""))
            {
                sArray = p.Split(',');
            }

            return sArray;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            //farpoint有数据时
            if (_rowsCount > 0)
            {
                //遍历数据
                for (int i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        this.fpTunnelHCInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        this.fpTunnelHCInfo.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            bindFpTunnelHCInfo();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpTunnelHCInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpTunnelHCInfo, 0);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpTunnelHCInfo, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelHCInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpTunnelHCInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelHCInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpTunnelHCInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion
    }
}
