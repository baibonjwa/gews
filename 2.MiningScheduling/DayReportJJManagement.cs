// ******************************************************************
// 概  述：掘进日报管理
// 作  者：宋英杰
// 日  期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
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
using GIS.HdProc;

namespace _2.MiningScheduling
{
    public partial class DayReportJJManagement : BaseForm
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

        FarPoint.Win.Spread.Cells cells = null;

        //各列索引
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

        const int C_TUNNEL_ID = 10;     // 巷道id
        const int C_WORKING_FACE_ID = 11;     // id
        //BID列序号
        int _bidindex = 12;
        /** 保存所有用户选中的行的索引 **/
        private Hashtable _htSelIdxs = new Hashtable();
        #endregion

        #region ******初始化******
        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportJJManagement(MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            //分页初始化
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.DAY_REPORT_JJ_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpDayReportJJ, LibCommon.Const_MS.DAY_REPORT_JJ_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[] { 1, 2, 3, 4, 5, 8 };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportJJ, _filterColunmIdxs);

            cells = this.fpDayReportJJ.Sheets[0].Cells;
        }

        public DayReportJJManagement(MainFrm mainFrm, String title, String title2)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            //分页初始化
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, title);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpDayReportJJ, LibCommon.Const_MS.DAY_REPORT_JJ_FARPOINT_TITLE, _rowDetailStartIndex);

            _filterColunmIdxs = new int[] { 1, 2, 3, 4, 5, 8 };

            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportJJ, _filterColunmIdxs);
        }
        /// <summary>
        /// 调用委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            //绑定数据
            bindDayReportJJ();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DayReportJJManagement_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = Convert.ToDateTime(Const_MS.DAY_REPORT_HC_DEFAULT_TIME);
            dtpTo.Value = DateTime.Now;
            bindDayReportJJ();
            chkSearch.Checked = false;
        }
        #endregion

        #region ******farpoint操作******
        /// <summary>
        /// 绑定farpoint数据
        /// </summary>
        private void bindDayReportJJ()
        {
            //farpoint清空（必须）
            FarPointOperate.farPointClear(fpDayReportJJ, _rowDetailStartIndex, _rowsCount);

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
                _iRecordCount = DayReportJJBLL.selectDayReportJJInfo(dtFrom, dtTo).Tables[0].Rows.Count;
            }
            else
            {
                 _iRecordCount = DayReportJJBLL.selectDayReportJJInfo().Tables[0].Rows.Count;
            }

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            //分页用数据
            if (chkSearch.Checked == true)
            {
                //按时间查询
                _ds = DayReportJJBLL.selectDayReportJJInfo(iStartIndex, iEndIndex, dtFrom, dtTo);
            }
            else
            {
                //查询全部
                _ds = DayReportJJBLL.selectDayReportJJInfo(iStartIndex, iEndIndex);
            }

            //数据行数
            _rowsCount = _ds.Tables[0].Rows.Count;

            //farpoint重新绘制
            FarPointOperate.farPointReAdd(fpDayReportJJ, _rowDetailStartIndex, _rowsCount);

            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //取消checkbox三选
                ckbxcell.ThreeState = false;

                //绑定farpoint数据
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = _ds.Tables[0].Rows[i];

                    // 选择按钮
                    cells[_rowDetailStartIndex + i, C_CHOOSE_BUTTON].CellType = ckbxcell;

                    //队别名称
                    if (int.TryParse(dr[DayReportJJDbConstNames.TEAM_NAME_ID].ToString(), out tmpInt))
                    {
                        cells[_rowDetailStartIndex + i, C_TEAM_NAME].Text =
                            BasicInfoManager.getInstance().getTeamNameById(tmpInt);
                        tmpInt = 0;
                    }
                    //工作面名称
                    if (int.TryParse(dr[DayReportJJDbConstNames.WORKINGFACE_ID].ToString(), out tmpInt))
                    {
                        cells[_rowDetailStartIndex + i, C_WORKING_FACE_NAME].Text =
                            BasicInfoManager.getInstance().getWorkingFaceById(tmpInt).WorkingFaceName;
                        tmpInt = 0;
                    }

                    //班次
                    cells[_rowDetailStartIndex + i, C_WORK_TIME].Text = dr[DayReportJJDbConstNames.WORK_TIME].ToString();
                    //工作制式
                    cells[_rowDetailStartIndex + i, C_WORK_STYLE].Text = dr[DayReportJJDbConstNames.WORK_TIME_SYTLE].ToString();
                    //工作内容
                    cells[_rowDetailStartIndex + i, C_WORK_CONTENT].Text = dr[DayReportJJDbConstNames.WORK_INFO].ToString();
                    //进尺
                    cells[_rowDetailStartIndex + i, C_WORK_PROGRESS].Text = dr[DayReportJJDbConstNames.JIN_CHI].ToString();
                    //日期
                    cells[_rowDetailStartIndex + i, C_DATE].Text = dr[DayReportJJDbConstNames.DATETIME].ToString().Substring(0, dr[DayReportJJDbConstNames.DATETIME].ToString().IndexOf(' '));
                    //填报人
                    cells[_rowDetailStartIndex + i, C_SUBMITTER].Text = dr[DayReportJJDbConstNames.SUBMITTER].ToString();
                    //备注
                    cells[_rowDetailStartIndex + i, C_COMMENTS].Text = dr[DayReportJJDbConstNames.OTHER].ToString();
                    //bid
                    cells[_rowDetailStartIndex + i, _bidindex].Text = dr[DayReportJJDbConstNames.BINDINGID].ToString();
                    fpDayReportJJ.Sheets[0].Columns[_bidindex].Visible = false;
                }
                //设置按钮可操作性
                setButtenEnable();
            }
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDayReportJJ_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                //记录选中行数
                if (fpChk.Checked)
                {
                    // 保存索引号
                    if (!_htSelIdxs.Contains(e.Row))
                    {
                        _htSelIdxs.Add(e.Row, true);

                        // 点击每条记录知道全部选中的情况下，全选/全不选checkbox设为选中
                        if (_htSelIdxs.Count == _checkCount)
                        {
                            // 全选/全不选checkbox设为选中
                            this.chkSelectAll.Checked = true;
                        }
                    }
                    _checkCount++;
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    this.chkSelectAll.Checked = false;

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
        /// 为变量dayReportJJEntity赋值
        /// </summary>
        private DayReportJJ setDayReportJJEntityValue()
        {
            DayReportJJ eReturn = null;
            for (int i = 0; i < _rowsCount; i++)
            {
                if (cells[_rowDetailStartIndex + i, 0].Value != null && (bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    _tmpRowIndex = _rowDetailStartIndex + i;
                    eReturn = new DayReportJJ();
                    DataRow dr = _ds.Tables[0].Rows[i];

                    //掘进ID
                    eReturn.ID = (int)dr[DayReportJJDbConstNames.ID];
                    //队别ID
                    eReturn.TeamNameID = (int)dr[DayReportJJDbConstNames.TEAM_NAME_ID];
                    //工作面ID
                    eReturn.WorkingFaceID = Convert.ToInt32(dr[DayReportJJDbConstNames.WORKINGFACE_ID]);
                    //班次
                    eReturn.WorkTime = cells[_rowDetailStartIndex + i, C_WORK_TIME].Text;
                    //工作制式
                    eReturn.WorkTimeStyle = cells[_rowDetailStartIndex + i, C_WORK_STYLE].Text;
                    //工作内容
                    eReturn.WorkInfo = cells[_rowDetailStartIndex + i, C_WORK_CONTENT].Text;
                    //进尺
                    eReturn.JinChi = Convert.ToDouble(cells[_rowDetailStartIndex + i, C_WORK_PROGRESS].Text);
                    //日期
                    string datetime = cells[_rowDetailStartIndex + i, C_DATE].Text;
                    eReturn.DateTime = DateTime.Parse(datetime);
                    //填报人
                    eReturn.Submitter = cells[_rowDetailStartIndex + i, C_SUBMITTER].Text;
                    //备注
                    eReturn.Other = cells[_rowDetailStartIndex + i, C_COMMENTS].Text;
                    //BID
                    eReturn.BindingID = dr[DayReportJJDbConstNames.BINDINGID].ToString();
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
            DayReportJJEntering dayReportJJForm = new DayReportJJEntering(this.MainForm);
            if (DialogResult.OK == dayReportJJForm.ShowDialog())
            {
                //绑定数据
                bindDayReportJJ();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);
                //添加后重设farpoint焦点
                FarPointOperate.farPointFocusSetAdd(fpDayReportJJ, _rowDetailStartIndex, _rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //回采日报实体赋值
            DayReportJJ entity = setDayReportJJEntityValue();

            WorkingFace ent = BasicInfoManager.getInstance().getWorkingFaceById(entity.WorkingFaceID);
            /**自定义控件用巷道信息数组**/
            int[] _arr = new int[5];
            _arr[0] = ent.MiningArea.Horizontal.Mine.MineId;
            _arr[1] = ent.MiningArea.Horizontal.HorizontalId;
            _arr[2] = ent.MiningArea.MiningAreaId;
            _arr[3] = ent.WorkingFaceID;

            DayReportJJEntering dayReportJJForm = new DayReportJJEntering(_arr, entity, this.MainForm);
            if (DialogResult.OK == dayReportJJForm.ShowDialog())
            {
                //绑定数据
                bindDayReportJJ();
                //修改后重设Farpoint焦点
                FarPointOperate.farPointFocusSetChange(fpDayReportJJ, _tmpRowIndex);
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
                bool bResult = false;

                IFeatureLayer featureLayer = GetJjjcFeatureLayer();

                //获取当前farpoint选中焦点
                _tmpRowIndex = fpDayReportJJ.Sheets[0].ActiveRowIndex;
                for (int i = 0; i < _rowsCount; i++)
                {
                    //选择为null时，该选择框没有被选择过,与未选中同样效果
                    if (cells[_rowDetailStartIndex + i, 0].Value != null)
                    {
                        //选择框被选择
                        if ((bool)cells[_rowDetailStartIndex + i, 0].Value == true)
                        {
                            DataRow dr = _ds.Tables[0].Rows[i];
                            DayReportJJ entity = new DayReportJJ();
                            //获取掘进ID
                            entity.ID = (int)dr[DayReportJJDbConstNames.ID];
                            entity.WorkingFaceID = (int)dr[DayReportJJDbConstNames.WORKINGFACE_ID];
                            entity.BindingID = dr[DayReportJJDbConstNames.BINDINGID].ToString();

                            // 掘进工作面，只有一条巷道
                            Tunnel tEntity = BasicInfoManager.getInstance().getTunnelListByWorkingFaceId(entity.WorkingFaceID)[0];

                            //删除操作
                            bResult = DayReportJJBLL.deleteDayReportJJInfo(entity);
                            if (bResult)
                            {
                                DelJJCD(tEntity.TunnelId.ToString(), entity.BindingID,entity.WorkingFaceID);

                                // 向server端发送更新预警数据
                                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(entity.WorkingFaceID,
                                    Const.INVALID_ID,
                                    DayReportJJDbConstNames.TABLE_NAME, OPERATION_TYPE.DELETE, DateTime.Now);
                                this.MainForm.SendMsg2Server(msg);
                            }
                        }
                    }
                }
                //删除成功
                if (bResult)
                {
                    //绑定数据
                    bindDayReportJJ();
                    //删除后重设Farpoint焦点
                    FarPointOperate.farPointFocusSetDel(fpDayReportJJ, _tmpRowIndex);
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
        /// 删除掘进进尺日报对应的地图信息
        /// </summary>
        /// <param name="hdid">巷道ID</param>
        /// <param name="bid">绑定ID</param>
        private void DelJJCD(string hdid, string bid,int workingfaceid)
        {
            Global.cons.DelJJCD(hdid, bid);
            //计算地质构造距离
            string sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + hdid + "'";
            Dictionary<string,string> dics=new Dictionary<string,string>();
            dics.Add(GIS.GIS_Const.FIELD_HDID,hdid);
            List<Tuple<ESRI.ArcGIS.Geodatabase.IFeature, ESRI.ArcGIS.Geometry.IGeometry, Dictionary<string, string>>> objs= Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdlyr, dics);
            if(objs.Count>0)
            {
                ESRI.ArcGIS.Geometry.IPointCollection poly0 = objs[0].Item2 as ESRI.ArcGIS.Geometry.IPointCollection;
                ESRI.ArcGIS.Geometry.IPointCollection poly1 = objs[1].Item2 as ESRI.ArcGIS.Geometry.IPointCollection;
                ESRI.ArcGIS.Geometry.IPoint pline = new ESRI.ArcGIS.Geometry.PointClass();
                if (poly0.get_Point(0).X - poly0.get_Point(1).X > 0)//向右掘进
                {
                    pline.X=(poly0.get_Point(0).X+poly0.get_Point(3).X)/2;
                    pline.Y=(poly0.get_Point(0).Y+poly0.get_Point(3).Y)/2;
                }
                else//向左掘进
                {
                    pline.X=(poly0.get_Point(1).X+poly0.get_Point(2).X)/2;
                    pline.Y=(poly0.get_Point(1).Y+poly0.get_Point(2).Y)/2;
                }
                //查询地质构造信息
                List<int> hdids=new List<int>();
                hdids.Add(Convert.ToInt32(hdid));
                Dictionary<string, List<GeoStruct>> dzxlist = Global.commonclss.GetStructsInfosNew(pline, hdids);
                GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingfaceid);//删除工作面ID对应的地质构造信息
                
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        GeologySpace geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkSpaceID = workingfaceid;
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
        /// 获取掘进进尺图层
        /// </summary>
        /// <returns></returns>
        private IFeatureLayer GetJjjcFeatureLayer()
        {
            //找到图层
            IMap map = GIS.Common.DataEditCommon.g_pMap;
            string layerName = GIS.LayerNames.DUG_FOOTAGE;//“掘进进尺”图层
            GIS.Common.DrawSpecialCommon drawSpecialCom = new GIS.Common.DrawSpecialCommon();
            IFeatureLayer featureLayer = drawSpecialCom.GetFeatureLayerByName(layerName);

            if (featureLayer == null)
            {
                //MessageBox.Show("没有找到" + layerName + "图层，将不能绘制掘进进尺线。", "提示", MessageBoxButtons.OK);
                return null;
            }

            return featureLayer;
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
            this.bindDayReportJJ();
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
            if (FileExport.fileExport(fpDayReportJJ, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
            //未保存直接返回
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
            FilePrint.CommonPrint(fpDayReportJJ, 0);
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
                        if (!_htSelIdxs.Contains(_rowDetailStartIndex + i))
                        {
                            _htSelIdxs.Add(_rowDetailStartIndex + i, true);
                        }
                        fpDayReportJJ.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        _htSelIdxs.Remove(_rowDetailStartIndex + i);
                        fpDayReportJJ.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
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
            bindDayReportJJ();
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
                bindDayReportJJ();
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
                bindDayReportJJ();
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpDayReportJJ, _filterColunmIdxs);
            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportJJ, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpDayReportJJ.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportJJ, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpDayReportJJ, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion


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
        /// 图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            if (iSelIdxsArr == null)
            {
                MessageBox.Show("未选中数据行！");
                return;
            }
            string bid = "";
            ILayer pLayer = GIS.Common.DataEditCommon.GetLayerByName(GIS.Common.DataEditCommon.g_pMap, GIS.LayerNames.LAYER_ALIAS_MR_TUNNEL_FD);
            if (pLayer == null)
            {
                MessageBox.Show("未发现掘进进尺图层！");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            string str = "";
            for (int i = 0; i < iSelIdxsArr.Length; i++)
            {
                bid = this.fpDayReportJJ.Sheets[0].Cells[iSelIdxsArr[i], _bidindex].Text.Trim();
                if (bid != "")
                {
                    if (i == 0)
                        str = "bid='" + bid + "'";
                    else
                        str += " or bid='" + bid + "'";
                }
            }
            List<ESRI.ArcGIS.Geodatabase.IFeature> list = GIS.MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                GIS.MyMapHelp.Jump(GIS.MyMapHelp.GetGeoFromFeature(list));
            }
            else
            {
                Alert.alert("图元丢失");
            }
        }
    }
}
