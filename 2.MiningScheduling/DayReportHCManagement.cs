// ******************************************************************
// 概  述：回采日报管理
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
using LibCommon;
using LibBusiness;
using LibEntity;
using System.Threading;
using LibCommonControl;
using LibSocket;
using ESRI.ArcGIS.Carto;
using LibCommonForm;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;

namespace _2.MiningScheduling
{
    /// <summary>
    /// 回采进尺管理
    /// </summary>
    public partial class DayReportHCManagement : BaseForm
    {
        #region *******变量声明******
        /**分页用数据行数**/
        private int _iRecordCount = 0;
        /**当前页数据行数**/
        int _rowsCount = 0;
        /**选择行数**/
        int _checkCount = 0;
        /**表头冻结行数**/
        int _rowDetailStartIndex = 3;

        /**修改行号（修改时重新设置焦点用）**/
        int _tmpRowIndex = 0;

        /**接分页查询数据**/
        DataSet _ds = new DataSet();
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;
        /**开始日期**/
        DateTime dtFrom;
        /**结束日期**/
        DateTime dtTo;
        int tmpInt = 0;

        const int C_CHOOSE_BUTTON = 0;      // 选择按钮
        const int C_TEAM_NAME = 1;      // 对别名称
        const int C_WORKING_FACE_NAME = 2;      // 工作面名称
        const int C_WORK_TIME = 3;     // 班次
        const int C_WORK_STYLE = 4;     // 工作制式
        const int C_WORK_CONTENT = 5;      // 工作内容
        const int C_WORK_PROGRESS = 6;      // 进尺
        const int C_DATE = 7;      // 日期
        const int C_SUBMITTER = 8;      // 填报人
        const int C_COMMENTS = 9;      // 备注

        // 隐藏列
        const int C_TUNNEL_ID = 10;     // 巷道id
        const int C_WORKING_FACE_ID = 11;     // id
        //****************************************

        FarPoint.Win.Spread.Cells cells = null;
        #endregion

        #region ******初始化******
        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportHCManagement(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            //分页初始化
            dataPager1.FrmChild_EventHandler += new DataPager.FrmChild_DelegateHandler(FrmParent_EventHandler);

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpDayReportHC, LibCommon.Const_MS.DAY_REPORT_HC_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[] { 1,
            2,
            3,
            4,
            5,
            8,
            9 };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportHC, _filterColunmIdxs);

            cells = this.fpDayReportHC.Sheets[0].Cells;
        }

        /// <summary>
        /// 调用委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            //绑定数据
            bindDayReportHC();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayReportHCManagement_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = Convert.ToDateTime(Const_MS.DAY_REPORT_HC_DEFAULT_TIME);
            dtpTo.Value = DateTime.Now;
            bindDayReportHC();
            chkSearch.Checked = false;
        }
        #endregion

        #region ******farpoint操作******
        /// <summary>
        /// 绑定farpoint数据
        /// </summary>
        private void bindDayReportHC()
        {
            //farpoint清空（必须）
            FarPointOperate.farPointClear(fpDayReportHC, _rowDetailStartIndex, _rowsCount);

            //全选checkbox取消选择（必须）
            chkSelectAll.Checked = false;

            //清空选择记数（必须)
            _checkCount = 0;

            //开始时间
            dtFrom = dtpFrom.Value;
            //结束时间
            dtTo = dtpTo.Value;

            // ※分页必须
            if (chkSearch.Checked == true)
            {
                _iRecordCount = DayReportHCBLL.selectDayReportHCInfo(dtFrom, dtTo).Tables[0].Rows.Count;
            }
            else
            {
                _iRecordCount = DayReportHCBLL.selectDayReportHCInfo().Tables[0].Rows.Count;
            }

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            //分页用数据
            if (chkSearch.Checked == true)
            {
                //按时间查询
                _ds = DayReportHCBLL.selectDayReportHCInfo(iStartIndex, iEndIndex, dtFrom, dtTo);
            }
            else
            {
                //查询全部
                _ds = DayReportHCBLL.selectDayReportHCInfo(iStartIndex, iEndIndex);
            }

            //数据行数
            _rowsCount = _ds.Tables[0].Rows.Count;

            //farpoint重新绘制
            FarPointOperate.farPointReAdd(fpDayReportHC, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //取消checkbox三选
                ckbxcell.ThreeState = false;

                //绑定farpoint数据
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = _ds.Tables[0].Rows[i];
                    //选择
                    cells[_rowDetailStartIndex + i, C_CHOOSE_BUTTON].CellType = ckbxcell;

                    //队别名称
                    if (int.TryParse(dr[DayReportHCDbConstNames.TEAM_NAME_ID].ToString(), out tmpInt))
                    {
                        cells[_rowDetailStartIndex + i, C_TEAM_NAME].Text = BasicInfoManager.getInstance().getTeamNameById(tmpInt);
                        tmpInt = 0;
                    }
                    //工作面名称
                    if (int.TryParse(dr[DayReportHCDbConstNames.WORKINGFACE_ID].ToString(), out tmpInt))
                    {
                        cells[_rowDetailStartIndex + i, C_WORKING_FACE_NAME].Text =
                            BasicInfoManager.getInstance().getWorkingFaceById(tmpInt).WorkingFaceName;
                        tmpInt = 0;
                    }

                    //班次
                    cells[_rowDetailStartIndex + i, C_WORK_TIME].Text = dr[DayReportHCDbConstNames.WORK_TIME].ToString();
                    //工作制式
                    cells[_rowDetailStartIndex + i, C_WORK_STYLE].Text = dr[DayReportHCDbConstNames.WORK_TIME_SYTLE].ToString();
                    //工作内容
                    cells[_rowDetailStartIndex + i, C_WORK_CONTENT].Text = dr[DayReportHCDbConstNames.WORK_INFO].ToString();
                    //进尺
                    cells[_rowDetailStartIndex + i, C_WORK_PROGRESS].Text = dr[DayReportHCDbConstNames.JIN_CHI].ToString();
                    //日期
                    cells[_rowDetailStartIndex + i, C_DATE].Text = dr[DayReportHCDbConstNames.DATETIME].ToString().Substring(0, dr[DayReportHCDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //填报人
                    cells[_rowDetailStartIndex + i, C_SUBMITTER].Text = dr[DayReportHCDbConstNames.SUBMITTER].ToString();
                    //备注
                    cells[_rowDetailStartIndex + i, C_COMMENTS].Text = dr[DayReportHCDbConstNames.OTHER].ToString();

                    // 隐藏列
                    // 工作面ID
                    cells[_rowDetailStartIndex + i, C_WORKING_FACE_ID].Text = dr[DayReportHCDbConstNames.WORKINGFACE_ID].ToString();
                }
                //设置按钮可操作性
                setButtenEnable();
            }

            //System.Threading.Tasks.Parallel

        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDayReportHC_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                //记录选中行数
                if (fpChk.Checked)
                {
                    _checkCount++;
                }
                else
                {
                    _checkCount--;
                }
            }
            //控制全选框的状态
            if (_checkCount == _rowsCount)
            {
                chkSelectAll.Checked = true;
            }
            else
            {
                chkSelectAll.Checked = false;
            }
            //设置按钮可操作性
            setButtenEnable();
        }
        #endregion

        #region ******实体赋值******
        /// <summary>
        /// 为变量dayReportHCEntity赋值
        /// </summary>
        private DayReportHCEntity setDayReportHCEntityValue()
        {
            DayReportHCEntity eReturn = null;

            for (int i = 0; i < _rowsCount; i++)
            {
                if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                    (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    _tmpRowIndex = _rowDetailStartIndex + i;
                    DataRow dr = _ds.Tables[0].Rows[i];
                    eReturn = new DayReportHCEntity();

                    //掘进ID
                    eReturn.ID = (int)dr[DayReportHCDbConstNames.ID];
                    //队别名称
                    eReturn.TeamNameID = (int)dr[DayReportHCDbConstNames.TEAM_NAME_ID];
                    //工作面ID
                    eReturn.WorkingFaceID = Convert.ToInt32(dr[DayReportHCDbConstNames.WORKINGFACE_ID]);
                    //班次
                    eReturn.WorkTime = cells[_rowDetailStartIndex + i, C_WORK_TIME].Text;
                    //工作制式
                    eReturn.WorkTimeStyle = cells[_rowDetailStartIndex + i, C_WORK_STYLE].Text;
                    //工作内容
                    eReturn.WorkInfo = cells[_rowDetailStartIndex + i, C_WORK_CONTENT].Text;
                    //进尺
                    eReturn.JinChi = Convert.ToDouble(cells[_rowDetailStartIndex + i, C_WORK_PROGRESS].Text);
                    //日期
                    eReturn.DateTime = DateTime.Parse(cells[_rowDetailStartIndex + i, C_DATE].Text);
                    //填报人
                    eReturn.Submitter = cells[_rowDetailStartIndex + i, C_SUBMITTER].Text;
                    //备注
                    eReturn.Other = cells[_rowDetailStartIndex + i, C_COMMENTS].Text;
                    //BID
                    eReturn.BindingID = dr[DayReportHCDbConstNames.BINDINGID].ToString();
                    break;
                }
            }

            return eReturn;
        }
        #endregion

        #region ******按钮点击事件******
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            DayReportHCEntering dayReportHCForm = new DayReportHCEntering(this.MainForm);
            if (DialogResult.OK == dayReportHCForm.ShowDialog())
            {
                //绑定数据
                bindDayReportHC();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);
                //添加后重设farpoint焦点
                FarPointOperate.farPointFocusSetAdd(fpDayReportHC, _rowDetailStartIndex, _rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            DayReportHCEntity entity = setDayReportHCEntityValue();

            if (null == entity)
            {
                Alert.alert("没有选择需要修改的信息。");
                return;
            }

            WorkingFaceEntity ent = BasicInfoManager.getInstance().getWorkingFaceById(entity.WorkingFaceID);
            /**自定义控件用巷道信息数组**/
            int[] _arr = new int[5];
            _arr[0] = ent.MiningArea.Horizontal.Mine.MineId;
            _arr[1] = ent.MiningArea.Horizontal.HorizontalId;
            _arr[2] = ent.MiningArea.MiningAreaId;
            _arr[3] = ent.WorkingFaceID;
            DayReportHCEntering dayReportHCForm = new DayReportHCEntering(_arr, entity, this.MainForm);
            if (DialogResult.OK == dayReportHCForm.ShowDialog())
            {
                //绑定数据
                bindDayReportHC();
                //修改后重设Farpoint焦点
                FarPointOperate.farPointFocusSetChange(fpDayReportHC, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            //确认删除
            bool result = Alert.confirm(Const.DEL_CONFIRM_MSG);

            if (result == true)
            {
                bool bResult = true;

                //获取当前farpoint选中焦点
                _tmpRowIndex = fpDayReportHC.Sheets[0].ActiveRowIndex;
                for (int i = 0; i < _rowsCount; i++)
                {
                    DataRow dr = _ds.Tables[0].Rows[i];
                    //选择为null时，该选择框没有被选择过,与未选中同样效果
                    //选择框被选择
                    if (cells[_rowDetailStartIndex + i, 0].Value != null &&
                        (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        DayReportHCEntity entity = new DayReportHCEntity();
                        //获取掘进ID
                        entity.ID = (int)dr[DayReportHCDbConstNames.ID];
                        entity.WorkingFaceID = Convert.ToInt32(dr[DayReportHCDbConstNames.WORKINGFACE_ID]);
                        entity.BindingID = dr[DayReportHCDbConstNames.BINDINGID].ToString();

                        // 回采面对象
                        WorkingFaceEntity hjEntity = BasicInfoManager.getInstance().getWorkingFaceById(entity.WorkingFaceID);
                        if (hjEntity != null)
                            hjEntity.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(hjEntity.WorkingFaceID));
                        Dictionary<TunnelTypeEnum, TunnelEntity> tDict = TunnelUtils.getTunnelDict(hjEntity);

                        if (tDict.Count > 0)
                        {
                            TunnelEntity tunnelZY = tDict[TunnelTypeEnum.STOPING_ZY];
                            TunnelEntity tunnelFY = tDict[TunnelTypeEnum.STOPING_FY];
                            TunnelEntity tunnelQY = tDict[TunnelTypeEnum.STOPING_QY];
                            // 删除GIS图形上的回采进尺
                            DelHcjc(tunnelZY.TunnelID, tunnelFY.TunnelID, tunnelQY.TunnelID, entity.BindingID, hjEntity, tunnelZY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid);
                        }

                        // 从数据库中删除对应的进尺信息
                        bResult &= DayReportHCBLL.deleteDayReportHCInfo(entity);
                        if (bResult == true)
                        {
                            BasicInfoManager.getInstance().refreshWorkingFaceInfo(hjEntity);

                            #region 通知服务器预警数据已更新
                            UpdateWarningDataMsg msg = new UpdateWarningDataMsg(entity.WorkingFaceID,
                                Const.INVALID_ID,
                                DayReportHCDbConstNames.TABLE_NAME, OPERATION_TYPE.DELETE, DateTime.Now);
                            this.MainForm.SendMsg2Server(msg);
                            #endregion
                        }
                    }
                }
                //删除成功
                if (bResult)
                {
                    //绑定数据
                    bindDayReportHC();
                    //删除后重设Farpoint焦点
                    FarPointOperate.farPointFocusSetDel(fpDayReportHC, _tmpRowIndex);
                }
                //删除失败
                else
                {
                    Alert.alert(Const_MS.MSG_DELETE_FAILURE);
                }
            }
            return;
        }

        /// <summary>
        /// 删除GIS回采进尺图形信息
        /// </summary>
        /// <param name="hd1">主运顺槽id</param>
        /// <param name="hd2">辅运顺槽id</param>
        /// <param name="qy">切眼id</param>
        /// <param name="bid">回采进尺的BindingID</param>
        /// <param name="wfEntity">回采面实体</param>
        private void DelHcjc(int hd1, int hd2, int qy, string bid, WorkingFaceEntity wfEntity, double zywid, double fywid, double qywid)
        {
            //删除对应的回采进尺图形和数据表中的记录信息
            Dictionary<string, IPoint> results = Global.cons.DelHCCD(hd1.ToString(), hd2.ToString(), qy.ToString(), bid, zywid, fywid, Global.searchlen);
            if (results == null)
                return;

            //更新当前回采进尺后的回采进尺记录表信息
            int count = results.Keys.Count;
            int index = 0;
            IPoint posnew = null;
            foreach (string key in results.Keys)
            {
                double x = results[key].X;
                double y = results[key].Y;
                double z = results[key].Z;
                wfEntity.Coordinate = new CoordinateEntity(x, y, z);
                LibBusiness.WorkingFaceBLL.updateWorkingfaceXYZ(wfEntity);

                index += 1;
                if (index == count - 1)
                {
                    posnew = new PointClass();
                    posnew.X = x;
                    posnew.Y = y;
                    posnew.Z = z;
                }
            }
            //更新回采进尺表，将isdel设置0
            LibEntity.DayReportHCEntity entity = new DayReportHCEntity();
            entity.BindingID = bid;
            entity.IsDel = 0;
            LibBusiness.DayReportHCBLL.updateDayResportHCInfoByBID(entity);

            //更新地质构造表中的信息
            if (posnew == null)
                return;
            List<int> hd_ids = new List<int>();
            hd_ids.Add(hd1);
            hd_ids.Add(hd2);
            hd_ids.Add(qy);
            Dictionary<string, List<GeoStruct>> dzxlist = Global.commonclss.GetStructsInfos(posnew, hd_ids);
            if (dzxlist.Count > 0)
            {
                GeologySpaceBLL.deleteGeologySpaceEntityInfos(wfEntity.WorkingFaceID);//删除工作面ID对应的地质构造信息
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    string geo_type = key;
                    for (int j = 0; j < geoinfos.Count; j++)
                    {
                        GeoStruct tmp = geoinfos[j];

                        GeologySpaceEntity geologyspaceEntity = new GeologySpaceEntity();
                        geologyspaceEntity.WorkSpaceID = wfEntity.WorkingFaceID;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);
                        geologyspaceEntity.TectonicID = tmp.geoinfos[GIS.GIS_Const.FIELD_BID].ToString();
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.onDateTime = DateTime.Now.ToShortDateString();

                        GeologySpaceBLL.insertGeologySpaceEntityInfo(geologyspaceEntity);
                    }
                }
            }
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            this.bindDayReportHC();
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 确认按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpDayReportHC, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
            return;
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //打印
            FilePrint.CommonPrint(fpDayReportHC, 0);
        }
        #endregion

        #region ******设置按钮可操作性******
        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            //修改只在选中一条时可用
            if (_checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            //删除在选中条数大于0时可用
            if (_checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }
        #endregion

        #region ******选择框事件******
        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_Click(object sender, EventArgs e)
        {
            //farpoint有数据时
            if (_rowsCount > 0)
            {
                //遍历数据
                for (int i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelectAll.Checked)
                    {
                        fpDayReportHC.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        fpDayReportHC.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// 按时间查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            //绑定数据
            bindDayReportHC();
        }
        #endregion

        #region ******时间选择事件******
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (chkSearch.Checked == true)
            {
                bindDayReportHC();
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (chkSearch.Checked == true)
            {
                bindDayReportHC();
            }
        }
        #endregion

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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportHC, _filterColunmIdxs);
            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportHC, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpDayReportHC.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportHC, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportHC, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion
    }
}
