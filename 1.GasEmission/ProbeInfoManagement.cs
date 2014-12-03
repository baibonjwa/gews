// ******************************************************************
// 概  述：探头数据管理
// 作  者：伍鑫
// 创建日期：2013/12/01
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
using LibBusiness;
using LibCommon;
using System.Collections;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using Steema.TeeChart.Styles;

namespace _1.GasEmission
{
    public partial class ProbeInfoManagement : BaseForm
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 4;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        /** 检索件数**/
        private int _iRowCount = 0;
        /** 主键index **/
        private int _primaryKeyIndex = 1;
        /** 需要过滤的列索引 **/
        private int[] _filterColunmIdxs = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProbeInfoManagement(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            //分配用户权限
            if (CurrentUserEnt._curLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }

            // 设置窗体默认属性
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GE.MANAGE_PROBE_INFO);

            // 设置Farpoint默认属性
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(this.fpProbeInfo, Const_GE.MANAGE_PROBE_INFO, _iRowDetailStartIndex);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            // 加载探头信息
            loadProbeInfo();

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                3,
                8,
                9,
                10,
                11,
                12,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpProbeInfo, _filterColunmIdxs);
            #endregion
        }

        /// <summary>
        /// 调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 分页控件与Farpoint过滤绑定问题
            FarpointFilterSetter.ClearFpFilter(this.fpProbeInfo);

            // 加载探头信息
            loadProbeInfo();
        }

        /// <summary>
        /// 加载探头信息
        /// </summary>
        private void loadProbeInfo()
        {
            // 修改按钮设为不可用（必须实装）
            this.btnUpdate.Enabled = false;
            // 删除按钮设为不可用（必须实装）
            this.btnDelete.Enabled = false;
            // 全选/全不选checkbox设为未选中（必须实装）
            this.chkSelAll.Checked = false;

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            // 删除farpoint明细部（必须实装）
            // 解决修改、删除某条数据后，重新load的时候，选择列checkbox不恢复成默认（不选择）的BUG
            // 解决删除全部数据后，再添加一行，报错的BUG
            if (this.fpProbeInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
            {
                this.fpProbeInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iRowCount);
            }
            else
            {
                _iRowCount = 0;
            }

            // 获取全部数据件数（必须实装）
            int iRecordCount = ProbeManageBLL.selectAllProbeManageInfo().Tables[0].Rows.Count;

            // 调用分页控件初始化方法（必须实装）
            dataPager1.PageControlInit(iRecordCount);

            // 获取要检索数据的开始位置和结束位置 （必须实装）
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 获取开始位置和结束位置之间的数据（必须实装）
            // 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
            DataSet ds = ProbeManageBLL.selectProbeManageInfoForPage(iStartIndex, iEndIndex);

            // 当前检索件数（必须实装）
            int iSelCnt = ds.Tables[0].Rows.Count;

            // 重新设定farpoint显示行数 （必须实装）
            this.fpProbeInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + iSelCnt;

            // 检索件数 > 0 的场合
            if (iSelCnt > 0)
            {
                // 当前检索件数（必须实装）
                this._iRowCount = iSelCnt;

                // 循环结果集
                for (int i = 0; i < iSelCnt; i++)
                {
                    int index = 0;
                    // 选择
                    FarPoint.Win.Spread.CellType.CheckBoxCellType objCheckCell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].CellType = objCheckCell;

                    // 探头编号
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_ID].ToString();
                    // 探头名称
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_NAME].ToString();

                    // 2014/05/29 del by wuxin Start
                    // 探头类型
                    //this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    //int iProbeTypeId = 0;
                    //if (int.TryParse(ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_TYPE_ID].ToString(), out iProbeTypeId))
                    //{
                    //    DataSet dsProbeType = ProbeTypeBLL.selectProbeTypeInfoByProbeTypeId(iProbeTypeId);
                    //    if (dsProbeType.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text = dsProbeType.Tables[0].Rows[0][ProbeTypeDbConstNames.PROBE_TYPE_NAME].ToString();
                    //    }
                    //}
                    // 2014/05/29 del by wuxin End

                    // 探头显示名称
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_TYPE_DISPLAY_NAME].ToString();

                    // 探头位置坐标X
                    string strProbeLocationX = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_LOCATION_X].ToString();
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = (strProbeLocationX == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationX);

                    // 探头位置坐标Y
                    string strProbeLocationY = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_LOCATION_Y].ToString();
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = (strProbeLocationY == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationY);

                    // 探头位置坐标Z
                    string strProbeLocationZ = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_LOCATION_Z].ToString();
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = (strProbeLocationZ == Const.DOUBLE_DEFAULT_VALUE ? "" : strProbeLocationZ);

                    // 探头描述
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ProbeManageDbConstNames.PROBE_DESCRIPTION].ToString();

                    // 巷道信息
                    // 矿井名称
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 水平
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 采区
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 工作面
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 巷道名称
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";

                    int iTunnelID = 0;
                    if (int.TryParse(ds.Tables[0].Rows[i][GasGushQuantityDbConstNames.TUNNEL_ID].ToString(), out iTunnelID))
                    {
                        TunnelEntity tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(iTunnelID);
                        if (tunnelEntity != null)
                        {
                            // 矿井名称
                            this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 4].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineName;//.MineName;
                            // 水平
                            this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 3].Text = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalName;//.HorizontalName;
                            // 采区
                            this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 2].Text =
                                tunnelEntity.WorkingFace.MiningArea.MiningAreaName;//MiningAreaName;
                            // 工作面
                            this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 1].Text =
                                tunnelEntity.WorkingFace.WorkingFaceName;//WorkingFaceName;
                            // 巷道名称
                            this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text = tunnelEntity.TunnelName;
                        }
                    }

                    // 是否自动位移
                    if (ds.Tables[0].Rows[i][ProbeManageDbConstNames.IS_MOVE].ToString() == "1")
                    {
                        this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "是";
                        // 距迎头距离
                        this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][ProbeManageDbConstNames.FAR_FROM_FRONTAL].ToString();
                    }
                    else
                    {
                        this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "否";
                        // 距迎头距离
                        this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    }


                }
            }
        }

        /// <summary>
        /// 添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProbeInfoEntering probeInfoEnteringForm = new ProbeInfoEntering(this.MainForm);
            if (DialogResult.OK == probeInfoEnteringForm.ShowDialog())
            {
                // 加载探头信息
                loadProbeInfo();
                // 跳转到尾页（必须实装）
                this.dataPager1.btnLastPage_Click(sender, e);
                // #TODO:暂时添加成功后不设计焦点，因为探头编号是可以输入的，无法根据其进行排序定位。
                //// 设置farpoint焦点（必须实装）
                //this.fpProbeInfo.Sheets[0].SetActiveCell(this.fpProbeInfo.Sheets[0].Rows.Count, 0);
            }
        }

        /// <summary>
        /// 修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            // 获取编号（主键）
            string strPrimaryKey = this.fpProbeInfo.Sheets[0].Cells[iSelIdxsArr[0], _primaryKeyIndex].Text;

            ProbeInfoEntering probeInfoEnteringForm = new ProbeInfoEntering(strPrimaryKey, this.MainForm);
            if (DialogResult.OK == probeInfoEnteringForm.ShowDialog())
            {
                // 加载探头信息
                loadProbeInfo();

                // 设置farpoint焦点（必须实装）
                this.fpProbeInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            }
        }

        /// <summary>
        /// 删除（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GE.DEL_CONFIRM_MSG))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                // 存放主键的数组
                string[] iPkIdxsArr = new string[iSelIdxsArr.Length];

                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string iPk = this.fpProbeInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKeyIndex].Text;
                    iPkIdxsArr[i] = iPk;
                }

                // 探头数据删除
                bool bResult = ProbeManageBLL.deleteProbeManageInfo(iPkIdxsArr);

                // 删除成功的场合
                if (bResult)
                {
                    // 加载探头信息
                    loadProbeInfo();

                    // 设置farpoint焦点（必须实装）
                    this.fpProbeInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
                }
            }
        }

        /// <summary>
        /// 获取farpoint中选中的所有行（必须实装）
        /// </summary>
        /// <returns>注意，返回值可能是null，null则代表一个也没选中</returns>
        private int[] GetSelIdxs()
        {
            if (this._htSelIdxs.Count == 0)
            {
                return null;
            }
            int[] retArr = new int[this._htSelIdxs.Count];
            this._htSelIdxs.Keys.CopyTo(retArr, 0);
            return retArr;
        }

        /// <summary>
        /// farpoint的ButtonClicked事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpProbeInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                // 判断是否被选中
                if (fpChk.Checked)
                {
                    // 保存索引号
                    if (!_htSelIdxs.Contains(e.Row))
                    {
                        _htSelIdxs.Add(e.Row, true);

                        // 点击每条记录知道全部选中的情况下，全选/全不选checkbox设为选中
                        if (_htSelIdxs.Count == _iRowCount)
                        {
                            // 全选/全不选checkbox设为选中
                            this.chkSelAll.Checked = true;
                        }
                    }
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    this.chkSelAll.Checked = false;
                }

                // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
                this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
                // 删除按钮
                this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        /// 全选/全不选checkbox的click事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            // 全不选的情况下
            if (_htSelIdxs.Count == _iRowCount)
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细的checkbox设为未选中
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将存有选中项目的数组清空
                    _htSelIdxs.Remove(_iRowDetailStartIndex + i);
                }
                // 删除按钮设为不可用
                this.btnDelete.Enabled = false;
            }
            // 全选的情况下
            else
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细设为全选中
                    this.fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                    if (!_htSelIdxs.Contains(_iRowDetailStartIndex + i))
                    {
                        _htSelIdxs.Add(_iRowDetailStartIndex + i, true);
                    }
                }
                // 删除按钮设为可用
                this.btnDelete.Enabled = true;
            }

            // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
            this.btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
            // 删除按钮
            this.btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
        }

        /// <summary>
        /// farpointFilter1的OnCheckFilterChanged方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpProbeInfo, _filterColunmIdxs);

            }
            //未选中时，根据用户自定义的颜色进行分类显示
            else
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpProbeInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        /// <summary>
        /// 清空过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpProbeInfo.ActiveSheet.RowFilter.ResetFilter();
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpProbeInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        /// 根据新的颜色值设置自动隐藏过滤条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpProbeInfo, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
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

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpProbeInfo, true))
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
            FilePrint.CommonPrint(this.fpProbeInfo, 0);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载探头信息
            loadProbeInfo();
        }

        private void btnProbeImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Desktop";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] strs = System.IO.File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
                string type = "";
                for (int i = 1; i < strs.Length; i++)
                {
                    string[] line = strs[i].Split(',');
                    ProbeManageEntity probe = new ProbeManageEntity();
                    probe.ProbeMeasureType = Convert.ToInt16(line[0]);
                    probe.ProbeId = line[1].Substring(3);
                    probe.ProbeDescription = line[2];
                    probe.ProbeTypeDisplayName = line[3];
                    probe.ProbeMeasureType = Convert.ToInt16(line[4]);
                    probe.ProbeUseType = line[5];
                    probe.Unit = line[6];

                    ProbeManageBLL.insertProbeManageInfo(probe);
                }
            }
        }
    }
}
