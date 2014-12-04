// ******************************************************************
// 概  述：回采日报添加修改
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
using LibBusiness;
using LibEntity;
using LibCommon;
using LibPanels;
using LibCommonControl;
using LibSocket;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using LibBusiness.CommonBLL;
using LibCommonForm;
using GIS.HdProc;

namespace _2.MiningScheduling
{
    public partial class DayReportHCEntering : BaseForm
    {
        #region ******变量声明******
        /**回采日报实体**/
        DayReportHC _dayReportHCEntity = new DayReportHC();
        /**巷道关联矿井等信息ID集合**/
        int[] _arr;

        DateTimePicker dtp = new DateTimePicker();   //这里实例化一个DateTimePicker控件
        Rectangle _Rectangle;

        WorkingFace workingFace = null; // 工作面
        Tunnel tunnelZY = null;  // 主运
        Tunnel tunnelFY = null;  // 辅运顺槽
        Tunnel tunnelQY = null; // 切眼

        //各列索引
        const int C_DATE = 0;     // 选择日期
        const int C_WORK_TIME = 1;     // 班次
        const int C_WORK_CONTENT = 2;     // 工作内容
        const int C_WORK_PROGRESS = 3;     // 进尺
        const int C_COMMENTS = 4;     // 备注

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public DayReportHCEntering(MainFrm frm)
        {
            InitializeComponent();

            dgrdvDayReportHC.Controls.Add(dtp);   //把时间控件加入DataGridView
            dtp.Visible = false;   //先不让它显示
            dtp.Format = DateTimePickerFormat.Custom;   //设置日期格式为2010-08-05
            dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
            this.dgrdvDayReportHC.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };

            this.MainForm = frm;

            addInfo();
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_ADD);

            //自定义控件初始化
            //WorkingFaceSelectEntity workingFaceSelectEntity = WorkingFaceSelect.SelectWorkingFace(DayReportHCDbConstNames.TABLE_NAME);
            //if (workingFaceSelectEntity != null)
            //{
            //    _arr = new int[5];
            //    _arr[0] = workingFaceSelectEntity.MineID;
            //    _arr[1] = workingFaceSelectEntity.HorizontalID;
            //    _arr[2] = workingFaceSelectEntity.MiningAreaID;

            //    this.selectWorkingFaceControl1.setCurSelectedID(_arr);
            //}
            //else
            //{
            //this.selectWorkingFaceControl1.loadMineName();


            //注册事件 
        }

        private void NameChangeEvent(object sender, WorkingFaceEventArgs e)
        {
            updateWorkingFaceInfo(this.selectWorkingfaceSimple1.IWorkingfaceId);
        }

        private void updateWorkingFaceInfo(int workingFaceId)
        {
            if (workingFaceId != Const.INVALID_ID)
            {
                workingFace = BasicInfoManager.getInstance().getWorkingFaceById(workingFaceId);

                workingFace.tunnelSet =
                    BasicInfoManager.getInstance()
                        .getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(workingFace.WorkingFaceID));
                Dictionary<TunnelTypeEnum, Tunnel> tList = TunnelUtils.getTunnelDict(workingFace);
                if (tList.Count == 0)
                    return;
                if (tList.Count >= 3)
                {
                    tunnelZY = tList[TunnelTypeEnum.STOPING_ZY];
                    tunnelFY = tList[TunnelTypeEnum.STOPING_FY];
                    tunnelQY = tList[TunnelTypeEnum.STOPING_QY];
                }

                if (null == workingFace)
                {
                    Log.Debug("[添加回采进尺]：工作面信息为空， workingFaceId=" + workingFaceId);
                    Alert.alert("[添加回采进尺]：工作面信息为空， workingFaceId=" + workingFaceId);
                    return;
                }
            }


        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="array">巷道编号数组</param>
        /// <param name="dayReportHCEntity">回采进尺日报实体</param>
        public DayReportHCEntering(int[] array, DayReportHC dayReportHCEntity, MainFrm frm)
        {
            _arr = array;
            this.MainForm = frm;

            this._dayReportHCEntity = dayReportHCEntity;

            updateWorkingFaceInfo(dayReportHCEntity.WorkingFaceID);

            InitializeComponent();
            //修改初始化
            changeInfo();
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_CHANGE);

            //this.selectWorkingFaceControl1.setCurSelectedID(_arr);

            dgrdvDayReportHC.AllowUserToAddRows = false;

            dgrdvDayReportHC.Controls.Add(dtp);   //把时间控件加入DataGridView
            dtp.Visible = false;   //先不让它显示
            dtp.Format = DateTimePickerFormat.Custom;   //设置日期格式为2010-08-05
            dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
            this.dgrdvDayReportHC.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };
        }

        private void DayReportHCEntering_Load(object sender, EventArgs e)
        {
            this.selectWorkingfaceSimple1.WorkingfaceNameChanged += NameChangeEvent;
            if (workingFace != null)
            {
                WorkingfaceSimple ws = new WorkingfaceSimple(workingFace.WorkingFaceID, workingFace.WorkingFaceName,
                    workingFace.WorkingfaceTypeEnum);
                this.selectWorkingfaceSimple1.SelectTunnelItemWithoutHistory(ws);



                workingFace.tunnelSet = BasicInfoManager.getInstance().getTunnelSetByDataSet(TunnelInfoBLL.selectTunnelByWorkingFaceId(workingFace.WorkingFaceID));
                Dictionary<TunnelTypeEnum, Tunnel> tList = TunnelUtils.getTunnelDict(workingFace);
                if (tList.Count >= 3)
                {
                    tunnelZY = tList[TunnelTypeEnum.STOPING_ZY];
                    tunnelFY = tList[TunnelTypeEnum.STOPING_FY];
                    tunnelQY = tList[TunnelTypeEnum.STOPING_QY];
                }
            }
        }

        /// <summary>
        /// 委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                if (dgrdvDayReportHC[3, i].Value != null)
                {
                    DataGridViewCellEventArgs dgvce = new DataGridViewCellEventArgs(2, i);
                    dgrdvDayReportHC_CellEndEdit(sender, dgvce);
                }
            }
        }

        /// <summary>
        /// 添加时加载初始化设置
        /// </summary>
        private void addInfo()
        {
            dgrdvDayReportHC[2, 0].Value = "回采";
            //绑定队别名称
            this.bindTeamInfo();
            ////初始化班次
            //this.bindWorkTimeFirstTime();
            //设置为默认工作制式
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            ////设置班次为当前时间对应的班次
            //dgrdvDayReportHC[0, 0].Value = WorkTime.returnSysWorkTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToLongTimeString();
            if (this.rbtn38.Checked == true)
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(1, sysDateTime);
            }
            else
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(2, sysDateTime);
            }

            if (strWorkTimeName != null && strWorkTimeName != "")
            {
                dgrdvDayReportHC[C_WORK_TIME, 0].Value = strWorkTimeName;
            }
        }

        /// <summary>
        /// 修改时加载初始化设置
        /// </summary>
        private void changeInfo()
        {
            //绑定默认信息
            addInfo();
            //绑定修改数据
            bindInfo();
        }

        /// <summary>
        /// datagridview绑定信息
        /// </summary>
        private void bindInfo()
        {
            //工作制式
            if (_dayReportHCEntity.WorkTimeStyle == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            if (_dayReportHCEntity.WorkTimeStyle == Const_MS.WORK_TIME_46)
            {
                rbtn46.Checked = true;
            }

            //队别
            cboTeamName.SelectedValue = _dayReportHCEntity.TeamNameID;

            //绑定队别成员
            this.bindTeamMember();

            //填报人
            cboSubmitter.Text = _dayReportHCEntity.Submitter;

            dgrdvDayReportHC.Rows.Add();
            dgrdvDayReportHC[C_DATE, 0].Value = Convert.ToString(_dayReportHCEntity.DateTime);
            dgrdvDayReportHC[C_WORK_TIME, 0].Value = _dayReportHCEntity.WorkTime;
            dgrdvDayReportHC[C_WORK_CONTENT, 0].Value = _dayReportHCEntity.WorkInfo;
            dgrdvDayReportHC[C_WORK_PROGRESS, 0].Value = _dayReportHCEntity.JinChi;
            dgrdvDayReportHC[C_COMMENTS, 0].Value = _dayReportHCEntity.Other;
        }

        /// <summary>
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.DataSource = null;

            DataSet ds = TeamBLL.selectTeamInfo();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //cboTeamName.Items.Add(ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_NAME].ToString());
                cboTeamName.DataSource = ds.Tables[0];
                cboTeamName.DisplayMember = TeamDbConstNames.TEAM_NAME;
                cboTeamName.ValueMember = TeamDbConstNames.ID;
                cboTeamName.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 绑定填报人
        /// </summary>
        private void bindTeamMember()
        {
            //清空填报人
            cboSubmitter.Items.Clear();
            cboSubmitter.Text = "";

            //获取队别成员姓名
            DataSet ds = TeamBLL.selectTeamInfoByTeamName(cboTeamName.Text);
            string teamLeader;
            string[] teamMember;
            if (ds.Tables[0].Rows.Count > 0)
            {
                //队长
                teamLeader = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_LEADER].ToString();
                //队员
                teamMember = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_MEMBER].ToString().Split(',');
                cboSubmitter.Items.Add(teamLeader);
                for (int i = 0; i < teamMember.Length; i++)
                {
                    cboSubmitter.Items.Add(teamMember[i]);
                }
            }
        }

        /// <summary>
        /// 添加队别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTeamInfo_Click(object sender, EventArgs e)
        {
            TeamInfoEntering teamInfoForm;
            if (cboTeamName.Text == "")
            {
                teamInfoForm = new TeamInfoEntering();
            }
            else
            {
                TeamInfo teamInfoEntity = new TeamInfo();
                teamInfoEntity.TeamID = Convert.ToInt32(cboTeamName.SelectedValue);
                teamInfoEntity = TeamBLL.selectTeamInfoByID(teamInfoEntity.TeamID);
                teamInfoForm = new TeamInfoEntering(teamInfoEntity);
            }

            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                this.bindTeamInfo();
                cboTeamName.Text = teamInfoForm.returnTeamName();
                DataSet ds = new DataSet();
                ds = TeamBLL.selectTeamInfoByTeamName(teamInfoForm.returnTeamName());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cboSubmitter.Items.Add(ds.Tables[0].Rows[0].ToString());
                    cboSubmitter.Items.RemoveAt(cboSubmitter.Items.Count - 1);
                    cboSubmitter.Text = cboSubmitter.Items[0].ToString();
                }
            }
        }

        /// <summary>
        /// 队别选择事件（根据队别绑定队员）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.bindTeamMember();
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
        /// 提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            if (tunnelZY == null || tunnelFY == null || tunnelQY == null)
            {
                Alert.alert("所选工作面缺少主运、辅运或切眼巷道");
                return;
            }
            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
            {
                LibBusiness.TunnelDefaultSelect.InsertDefaultTunnel(DayReportHCDbConstNames.TABLE_NAME, selectWorkingfaceSimple1.IWorkingfaceId);
                insertDayReportHCInfo();
            }
            else if (this.Text == Const_MS.DAY_REPORT_HC_CHANGE)
            {
                DayReportHC oldDayReportHCEntity = _dayReportHCEntity; //修改前实体
                LibBusiness.TunnelDefaultSelect.UpdateDefaultTunnel(DayReportHCDbConstNames.TABLE_NAME, selectWorkingfaceSimple1.IWorkingfaceId);
                updateDayReportHCInfo();

                DayReportHC newDayReportHCEntity = _dayReportHCEntity; //修改后实体
            }
        }

        /// <summary>
        /// 添加初始化时的回采进尺
        /// </summary>
        private void AddHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid, double hcjc, string bid)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            IPoint prevPnt = null;

            workingFace = WorkingFaceBLL.selectWorkingFaceInfoByWksId(workingFace.WorkingFaceID);

            if (workingFace != null)
            {
                if (workingFace.Coordinate.X != 0 && workingFace.Coordinate.Y != 0)
                {
                    prevPnt = new PointClass();
                    prevPnt.X = workingFace.Coordinate.X;
                    prevPnt.Y = workingFace.Coordinate.Y;
                    prevPnt.Z = workingFace.Coordinate.Z;
                }
            }
            else
            {
                Log.Error("[回采进尺]:工作面为空值!");
                throw new ArgumentException("[回采进尺]:工作面为空值!", "workingFace");
            }

            dics[GIS.GIS_Const.FIELD_HDID] = hd1.ToString() + "_" + hd2.ToString();
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            int xh = 0;
            if (selcjqs.Count == 0)
                xh = 0;
            else
                xh = Convert.ToInt32(selcjqs[0].Item3[GIS.GIS_Const.FIELD_XH]);
            dics[GIS.GIS_Const.FIELD_ID] = "0";
            dics[GIS.GIS_Const.FIELD_BS] = "0";
            dics[GIS.GIS_Const.FIELD_BID] = bid;
            dics[GIS.GIS_Const.FIELD_XH] = (xh + 1).ToString();
            IPoint pos = new PointClass();
            Dictionary<string, List<GeoStruct>> dzxlist =
                Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(), qy.ToString(), hcjc, zywid, fywid, qywid, 1, Global.searchlen, dics, true, prevPnt, out pos);

            // 更新工作面信息（预警点坐标）
            if (pos != null)
            {
                workingFace.Coordinate = new Coordinate(pos.X, pos.Y, 0.0);
                LibBusiness.WorkingFaceBLL.updateWorkingfaceXYZ(workingFace);
            }

            //更新地质构造表
            if (null != dzxlist && dzxlist.Count > 0)
            {
                GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingFace.WorkingFaceID);//删除工作面ID对应的地质构造信息
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        GeologySpace geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkSpaceID = workingFace.WorkingFaceID;
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
        /// 修改回采进尺面上显示信息
        /// </summary>
        private void UpdateHcjc(int hd1, int hd2, int qy, double hcjc, string bid, double zywid, double fywid, double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            IPoint prevPnt = new PointClass();
            prevPnt.X = workingFace.Coordinate.X;
            prevPnt.Y = workingFace.Coordinate.Y;
            prevPnt.Z = workingFace.Coordinate.Z;

            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add(GIS.GIS_Const.FIELD_HDID, hd1.ToString() + "_" + hd2.ToString());
            dics.Add(GIS.GIS_Const.FIELD_BS, "0");
            dics.Add(GIS.GIS_Const.FIELD_BID, bid);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            Dictionary<string, IPoint> results_pts = Global.cons.UpdateHCCD(hd1.ToString(), hd2.ToString(), qy.ToString(), bid, hcjc, zywid, fywid, qywid, Global.searchlen);
            if (results_pts == null) return;
            //更新当前修改的回采进尺对应的工作面信息表记录
            int index = 0;
            IPoint pnt = new PointClass();

            foreach (string key in results_pts.Keys)
            {
                workingFace.Coordinate = new Coordinate(results_pts[key].X, results_pts[key].Y, results_pts[key].Z);

                LibBusiness.WorkingFaceBLL.updateWorkingfaceXYZ(workingFace);
                if (index == results_pts.Count - 1)
                {
                    pnt.X = results_pts[key].X;
                    pnt.Y = results_pts[key].Y;
                    pnt.Z = results_pts[key].Z;
                }

                index++;
            }

            //
            if (results_pts.Count == 0)
                return;

            // 地质构造距离
            List<int> hd_ids = new List<int>();
            hd_ids.Add(hd1);
            hd_ids.Add(hd2);
            hd_ids.Add(qy);
            Dictionary<string, List<GeoStruct>> geostructsinfos = Global.commonclss.GetStructsInfos(pnt, hd_ids);
            if (geostructsinfos.Count > 0)
            {
                GeologySpaceBLL.deleteGeologySpaceEntityInfos(workingFace.WorkingFaceID);//删除工作面ID对应的地质构造信息
                foreach (string key in geostructsinfos.Keys)
                {
                    List<GeoStruct> geoinfos = geostructsinfos[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        GeologySpace geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkSpaceID = selectWorkingfaceSimple1.IWorkingfaceId;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);

                        geologyspaceEntity.TectonicID = tmp.geoinfos[GIS.GIS_Const.FIELD_BID];
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.onDateTime = DateTime.Now.ToShortDateString();

                        GeologySpaceBLL.insertGeologySpaceEntityInfo(geologyspaceEntity);
                    }
                }
            }
        }

        /// <summary>
        /// 添加回采日报
        /// </summary>
        private void insertDayReportHCInfo()
        {
            List<DayReportHC> dayReportHCEntityList = new List<DayReportHC>();
            for (int i = 0; i < this.dgrdvDayReportHC.RowCount; i++)
            {
                DayReportHC dayReportHCEntity = new DayReportHC();
                // 最后一行为空行时，跳出循环
                if (i == this.dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                /**回采日报实体赋值**/
                //队别名称
                dayReportHCEntity.TeamNameID = Convert.ToInt32(cboTeamName.SelectedValue);
                //绑定回采面编号
                dayReportHCEntity.WorkingFaceID = selectWorkingfaceSimple1.IWorkingfaceId;

                DataGridViewCellCollection cells = this.dgrdvDayReportHC.Rows[i].Cells;

                //日期
                if (cells[C_DATE].Value != null)
                {
                    dayReportHCEntity.DateTime = Convert.ToDateTime(cells[C_DATE].Value.ToString());
                }
                //填报人
                dayReportHCEntity.Submitter = cboSubmitter.Text;
                //工作制式
                if (rbtn38.Checked)
                {
                    dayReportHCEntity.WorkTimeStyle = rbtn38.Text;
                }
                if (rbtn46.Checked)
                {
                    dayReportHCEntity.WorkTimeStyle = rbtn46.Text;
                }
                //班次
                if (cells[C_WORK_TIME].Value != null)
                {
                    dayReportHCEntity.WorkTime = cells[C_WORK_TIME].Value.ToString();
                }

                //工作内容
                if (cells[C_WORK_CONTENT].Value != null)
                {
                    dayReportHCEntity.WorkInfo = cells[C_WORK_CONTENT].Value.ToString();
                }

                //回采进尺
                if (cells[C_WORK_PROGRESS].Value != null)
                {
                    dayReportHCEntity.JinChi = Convert.ToDouble(cells[C_WORK_PROGRESS].Value);
                }

                //备注
                if (cells[C_COMMENTS].Value != null)
                {
                    dayReportHCEntity.Other = cells[C_COMMENTS].Value.ToString();
                }
                //BID
                dayReportHCEntity.BindingID = IDGenerator.NewBindingID();

                //添加到dayReportHCEntityList中
                dayReportHCEntityList.Add(dayReportHCEntity);
            }

            bool bResult = false;

            //循环添加
            foreach (DayReportHC dayReportHCEntity in dayReportHCEntityList)
            {
                //添加回采进尺日报
                bResult = DayReportHCBLL.insertDayReportHCInfo(dayReportHCEntity);

                // 在图中绘制回采进尺
                if (workingFace != null)
                {
                    double hcjc = dayReportHCEntity.JinChi;
                    string bid = dayReportHCEntity.BindingID;

                    AddHcjc(tunnelZY.TunnelID, tunnelFY.TunnelID, tunnelQY.TunnelID, tunnelZY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid,
                        hcjc, bid);
                }
                else
                {
                    Log.Fatal("[添加回采日报]：工作面实体为空值。");
                }
            }

            //添加失败
            if (!bResult)
            {
                Alert.alert(Const_MS.MSG_UPDATE_FAILURE);
                return;
            }
            else
            {
                // 工作面坐标已经改变，需要更新工作面信息
                Log.Debug("发送地址构造消息------开始");
                BasicInfoManager.getInstance().refreshWorkingFaceInfo(workingFace);

                // 通知服务端回采进尺已经添加
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(selectWorkingfaceSimple1.IWorkingfaceId,
                    Const.INVALID_ID,
                    DayReportHCDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, DateTime.Now);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送地址构造消息------完成" + msg.ToString());
            }
        }

        /// <summary>
        /// 修改回采日报信息
        /// </summary>
        private void updateDayReportHCInfo()
        {
            //绑定回采面编号
            _dayReportHCEntity.WorkingFaceID = selectWorkingfaceSimple1.IWorkingfaceId;
            //队别名称
            _dayReportHCEntity.TeamNameID = Convert.ToInt32(cboTeamName.SelectedValue);
            //日期
            //_dayReportHCEntity.DateTime = dtpDate.Value;
            //填报人
            _dayReportHCEntity.Submitter = cboSubmitter.Text;
            //工作制式
            if (rbtn38.Checked)
            {
                _dayReportHCEntity.WorkTimeStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                _dayReportHCEntity.WorkTimeStyle = rbtn46.Text;
            }

            DataGridViewCellCollection cells = this.dgrdvDayReportHC.Rows[0].Cells;

            //创建日期
            if (cells[C_DATE].Value != null)
            {
                _dayReportHCEntity.DateTime = Convert.ToDateTime(cells[C_DATE].Value.ToString());
            }
            //班次
            if (cells[C_WORK_TIME].Value != null)
            {
                _dayReportHCEntity.WorkTime = cells[C_WORK_TIME].Value.ToString();
            }

            //工作内容
            if (cells[C_WORK_CONTENT].Value != null)
            {
                _dayReportHCEntity.WorkInfo = cells[C_WORK_CONTENT].Value.ToString();
            }

            //回采进尺
            if (cells[C_WORK_PROGRESS].Value != null)
            {
                _dayReportHCEntity.JinChi = Convert.ToDouble(cells[C_WORK_PROGRESS].Value);
            }

            //备注
            if (cells[C_COMMENTS].Value != null)
            {
                _dayReportHCEntity.Other = cells[C_COMMENTS].Value.ToString();
            }

            //提交修改
            bool bResult = DayReportHCBLL.updateDayReportHCInfo(_dayReportHCEntity);

            //绘制回采进尺图形
            double hcjc = _dayReportHCEntity.JinChi;
            string bid = _dayReportHCEntity.BindingID;

            UpdateHcjc(tunnelZY.TunnelID, tunnelFY.TunnelID, tunnelQY.TunnelID, hcjc, bid, tunnelZY.TunnelWid, tunnelFY.TunnelWid, tunnelQY.TunnelWid);

            //修改成功
            if (bResult)
            {
                // 工作面坐标修改，需要更新工作面信息
                BasicInfoManager.getInstance().refreshWorkingFaceInfo(workingFace);

                // 通知服务器数据已经修改
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(workingFace.WorkingFaceID, tunnelQY.TunnelID,
                    DayReportHCDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, DateTime.Now);
                this.MainForm.SendMsg2Server(msg);
            }
        }

        ///// <summary>
        ///// 初始化班次
        ///// </summary>
        //private void bindWorkTimeFirstTime()
        //{
        //    DataSet dsWorkTime;
        //    //获取三八制班次
        //    if (rbtn38.Checked)
        //    {
        //        dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
        //    }
        //    //获取四六制班次
        //    else
        //    {
        //        dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
        //    }
        //    //向combobox里插入数据
        //    for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
        //    {
        //        cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
        //    }
        //}

        /// <summary>
        /// 三八制选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            //选择三八制
            if (rbtn38.Checked)
            {
                cboWorkTime.Items.Clear();
                //清空班次下拉框中选项

                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
                for (int j = 0; j < dsWorkTime.Tables[0].Rows.Count; j++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[j][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
                //for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
                //{
                //    //清空班次
                //    dgrdvDayReportHC[0, i].Value = "";

                //    if (i == 0)
                //    {
                //        dgrdvDayReportHC[0, 0].Value = WorkTime.returnSysWorkTime(rbtn38.Text);
                //    }
                //    else
                //    {
                //        dgrdvDayReportHC[0, i].Value = null;
                //    }
                //}
            }
            //选择四六制
            else
            {
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
                for (int j = 0; j < dsWorkTime.Tables[0].Rows.Count; j++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[j][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
                //for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
                //{
                //    dgrdvDayReportHC[0, i].Value = "";
                //    if (i == 0)
                //    {
                //        dgrdvDayReportHC[0, 0].Value = WorkTime.returnSysWorkTime(rbtn46.Text);
                //    }
                //    else
                //    {
                //        dgrdvDayReportHC[0, i].Value = null;
                //    }
                //}
            }
            // 设置班次名称
            setWorkTimeName();

            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                dgrdvDayReportHC[0, i].Value = dgrdvDayReportHC[0, 0].Value;
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //工作面是否选择
            if (selectWorkingfaceSimple1.IWorkingfaceId == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_MS.WORKINGFACE + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }

            //队别为空
            if (Validator.IsEmpty(this.cboTeamName.Text))
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.TEAM_NAME + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //队别名称是否有特殊字符
            if (Validator.checkSpecialCharacters(cboTeamName.Text))
            {
                Alert.alert(Const_MS.TEAM_NAME + Const.MSG_SP_CHAR + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //填报人是否有特殊字符
            if (Validator.checkSpecialCharacters(cboSubmitter.Text))
            {
                Alert.alert(Const_MS.SUBMITTER + Const.MSG_SP_CHAR + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //datagridview验证
            //只有一条数据时
            if (this.dgrdvDayReportHC.Rows.Count - 1 == 0)
            {
                //工作面是否选择
                if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    if (!WorkingFaceBLL.CheckIsExist(selectWorkingfaceSimple1.IWorkingfaceId, Convert.ToString(dgrdvDayReportHC[1, 0].Value),
                        Convert.ToDateTime(dgrdvDayReportHC[0, 0].Value)))
                    {
                        Alert.alert(Const_MS.WORKINGFACEEXIST + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                }
                //添加时判断为未录入进尺
                if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //修改时
                else
                {
                    bool bResult = false;
                    //为空返回false，不数据时跳出循环
                    for (int i = 0; i < dgrdvDayReportHC.ColumnCount; i++)
                    {
                        if (dgrdvDayReportHC[3, i].Value == null)
                        {
                            bResult = false;
                        }
                        else
                        {
                            bResult = true;
                            break;
                        }
                    }
                    if (!bResult)
                    {
                        Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                        return bResult;
                    }
                }
            }

            for (int i = 0; i < this.dgrdvDayReportHC.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                DataGridViewCellCollection cells = dgrdvDayReportHC.Rows[i].Cells;

                DataGridViewTextBoxCell cell = cells[C_DATE] as DataGridViewTextBoxCell;
                //工作面是否选择
                if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    if (!WorkingFaceBLL.CheckIsExist(selectWorkingfaceSimple1.IWorkingfaceId, dgrdvDayReportHC[1, i].Value.ToString(),
                        Convert.ToDateTime(dgrdvDayReportHC[0, i].Value)))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_MS.DAY_REPORT_HC_EXIST + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }

                cell = cells[C_WORK_PROGRESS] as DataGridViewTextBoxCell;
                //进尺为空
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_NOT_NULL + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //进尺不为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                else
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                cell = cells[C_COMMENTS] as DataGridViewTextBoxCell;
                //备注不能含特殊字符
                if (cell.Value != null)
                {
                    if (Validator.checkSpecialCharacters(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const_MS.OTHER + Const.MSG_SP_CHAR + Const_MS.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                    else
                    {
                        cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    }
                }
            }

            //验证成功
            return true;
        }

        /// <summary>
        /// 进尺输入计算距切眼距离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvDayReportHC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //第一行时
            if (e.RowIndex == 0)
            {
                //进尺单元格
                if (e.ColumnIndex == C_WORK_PROGRESS)
                {
                    //非空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        //验证输入是否为数字
                        if (!Validator.IsNumeric(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString()))
                        {
                            Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                            //错误处理
                            //添加时处理方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = null;
                            }
                            //修改时处理方式
                            else
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = _dayReportHCEntity.JinChi;
                            }
                        }
                    }
                }
            }
            //非第一行时
            else
            {
                //进尺单元格
                if (e.ColumnIndex == C_WORK_PROGRESS)
                {
                    //非空
                    if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        //验证输入是否为数字
                        if (!Validator.IsNumeric(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString()))
                        {
                            Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                            //错误处理
                            //添加时处理方式
                            if (this.Text == Const_MS.DAY_REPORT_HC_ADD)
                            {
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = null;
                            }
                        }
                    }
                }
            }
        }

        private void cboTeamName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cboTeamName.Items.Count; i++)
            {
                if (cboTeamName.Text == cboTeamName.GetItemText(cboTeamName.Items[i]))
                {
                    bindTeamMember();
                    break;
                }
                else
                {
                    cboSubmitter.Items.Clear();
                    cboSubmitter.Text = "";
                }
            }
        }

        private void dgrdvDayReportHC_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgrdvDayReportHC[C_WORK_CONTENT, e.RowIndex].Value = "回采";
            dgrdvDayReportHC[C_WORK_CONTENT, e.RowIndex].Value = dgrdvDayReportHC[C_WORK_CONTENT, 0].Value;
        }

        private void dgrdvDayReportHC_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrdvDayReportHC[C_DATE, e.RowIndex].Value == null &&
                dgrdvDayReportHC[C_WORK_PROGRESS, e.RowIndex].Value == null &&
                dgrdvDayReportHC[C_WORK_TIME, e.RowIndex].Value == null)
            {
                dgrdvDayReportHC[C_WORK_CONTENT, e.RowIndex].Value = null;
            }
        }

        private void dgrdvDayReportHC_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgrdvDayReportHC.EndEdit();
        }

        /*************时间控件选择时间时****************/
        private void dtp_TextChange(object sender, EventArgs e)
        {
            dgrdvDayReportHC.Rows[dgrdvDayReportHC.CurrentCell.RowIndex].Cells[C_DATE].Value = dtp.Text.ToString();   //时间控件选择时间时，就把时间赋给所在的单元格
        }

        /****************单元格被单击，判断是否是放时间控件的那一列*******************/
        //private void dgrdvDayReportHC_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == C_DATE)
        //    {
        //        _Rectangle = dgrdvDayReportHC.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //得到所在单元格位置和大小
        //        dtp.Size = new Size(_Rectangle.Width, _Rectangle.Height); //把单元格大小赋给时间控件
        //        dtp.Location = new System.Drawing.Point(_Rectangle.X, _Rectangle.Y); //把单元格位置赋给时间控件
        //        dtp.Visible = true;   //可以显示控件了
        //    }
        //    else
        //        dtp.Visible = false;
        //}

        /***********当列的宽度变化时，时间控件先隐藏起来，不然单元格变大时间控件无法跟着变大哦***********/
        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }

        /***********滚动条滚动时，单元格位置发生变化，也得隐藏时间控件，不然时间控件位置不动就乱了********/
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            dtp.Visible = false;
        }

        private void dgrdvDayReportHC_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //日期自动填充
            if (e.ColumnIndex == C_DATE)
            {
                //datetimepicker控件位置大小
                Rectangle rect = dgrdvDayReportHC.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                dtp.Visible = true;
                dtp.Top = rect.Top;
                dtp.Left = rect.Left;
                dtp.Height = rect.Height;
                dtp.Width = rect.Width;
                //datetimepicker赋值
                if (dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    dtp.Value = Convert.ToDateTime(dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value.ToString());
                }
                //默认填充系统时间
                else
                {
                    dtp.Value = DateTime.Now;
                    dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = dtp.Text;
                }
            }
        }

    }
}
