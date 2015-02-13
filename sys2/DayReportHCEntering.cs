using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.HdProc;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibCommonControl;
using LibCommonForm;
using LibEntity;
using LibPanels;
using LibSocket;
using TunnelDefaultSelect = LibBusiness.TunnelDefaultSelect;

namespace sys2
{
    public partial class DayReportHcEntering : BaseForm
    {
        #region ******变量声明******

        /**回采日报实体**/

        //各列索引
        private const int C_DATE = 0; // 选择日期
        private const int C_WORK_TIME = 1; // 班次
        private const int C_WORK_CONTENT = 2; // 工作内容
        private const int C_WORK_PROGRESS = 3; // 进尺
        private const int C_COMMENTS = 4; // 备注
        private Rectangle _Rectangle;
        private int[] _arr;
        private DayReportHc _dayReportHCEntity = new DayReportHc();
        private DateTimePicker dtp = new DateTimePicker(); //这里实例化一个DateTimePicker控件
        private Tunnel tunnelFY = null; // 辅运顺槽
        private Tunnel tunnelQY = null; // 切眼
        private Tunnel tunnelZY = null; // 主运
        private WorkingFace workingFace = null; // 工作面

        #endregion

        /// <summary>
        ///     构造方法
        /// </summary>
        public DayReportHcEntering()
        {
            InitializeComponent();

            dgrdvDayReportHC.Controls.Add(dtp); //把时间控件加入DataGridView
            dtp.Visible = false; //先不让它显示
            dtp.Format = DateTimePickerFormat.Custom; //设置日期格式为2010-08-05
            dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
            dgrdvDayReportHC.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };

            addInfo();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_ADD);
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="array">巷道编号数组</param>
        /// <param name="dayReportHCEntity">回采进尺日报实体</param>
        public DayReportHcEntering(DayReportHc dayReportHCEntity)
        {
            _dayReportHCEntity = dayReportHCEntity;

            updateWorkingFaceInfo(dayReportHCEntity.WorkingFace.WorkingFaceId);

            InitializeComponent();
            //修改初始化
            changeInfo();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_CHANGE);

            //this.selectWorkingFaceControl1.setCurSelectedID(_arr);

            dgrdvDayReportHC.AllowUserToAddRows = false;

            dgrdvDayReportHC.Controls.Add(dtp); //把时间控件加入DataGridView
            dtp.Visible = false; //先不让它显示
            dtp.Format = DateTimePickerFormat.Custom; //设置日期格式为2010-08-05
            dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
            dgrdvDayReportHC.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };
        }

        private void NameChangeEvent(object sender, WorkingFaceEventArgs e)
        {
            updateWorkingFaceInfo(selectWorkingfaceSimple1.IWorkingfaceId);
        }

        private void updateWorkingFaceInfo(int workingFaceId)
        {
            if (workingFaceId != Const.INVALID_ID)
            {
                workingFace = WorkingFace.Find(workingFaceId);

                tunnelZY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
                tunnelFY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
                tunnelQY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);

                if (null == workingFace)
                {
                    Log.Debug("[添加回采进尺]：工作面信息为空， workingFaceId=" + workingFaceId);
                    Alert.alert("[添加回采进尺]：工作面信息为空， workingFaceId=" + workingFaceId);
                    return;
                }
            }
        }

        private void DayReportHCEntering_Load(object sender, EventArgs e)
        {
            selectWorkingfaceSimple1.WorkingfaceNameChanged += NameChangeEvent;
            if (workingFace != null)
            {
                var ws = new WorkingfaceSimple(workingFace.WorkingFaceId, workingFace.WorkingFaceName,
                    workingFace.WorkingfaceTypeEnum);
                selectWorkingfaceSimple1.SelectTunnelItemWithoutHistory(ws);


                tunnelZY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
                tunnelFY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
                tunnelQY = workingFace.Tunnels.First(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);
            }
        }

        /// <summary>
        ///     委托事件
        /// </summary>
        /// <param name="sender"></param>
        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                if (dgrdvDayReportHC[3, i].Value != null)
                {
                    var dgvce = new DataGridViewCellEventArgs(2, i);
                    dgrdvDayReportHC_CellEndEdit(sender, dgvce);
                }
            }
        }

        /// <summary>
        ///     添加时加载初始化设置
        /// </summary>
        private void addInfo()
        {
            dgrdvDayReportHC[2, 0].Value = "回采";
            //绑定队别名称
            bindTeamInfo();
            ////初始化班次
            //this.bindWorkTimeFirstTime();
            //设置为默认工作制式
            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
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
        ///     设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToLongTimeString();
            if (rbtn38.Checked == true)
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
        ///     修改时加载初始化设置
        /// </summary>
        private void changeInfo()
        {
            //绑定默认信息
            addInfo();
            //绑定修改数据
            bindInfo();
        }

        /// <summary>
        ///     datagridview绑定信息
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
            cboTeamName.SelectedValue = _dayReportHCEntity.Team;

            //绑定队别成员
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);

            //填报人
            cboSubmitter.Text = _dayReportHCEntity.Submitter;

            dgrdvDayReportHC.Rows.Add();
            dgrdvDayReportHC[C_DATE, 0].Value = Convert.ToString(_dayReportHCEntity.DateTime);
            dgrdvDayReportHC[C_WORK_TIME, 0].Value = _dayReportHCEntity.WorkTime;
            dgrdvDayReportHC[C_WORK_CONTENT, 0].Value = _dayReportHCEntity.WorkInfo;
            dgrdvDayReportHC[C_WORK_PROGRESS, 0].Value = _dayReportHCEntity.JinChi;
            dgrdvDayReportHC[C_COMMENTS, 0].Value = _dayReportHCEntity.Remarks;
        }

        /// <summary>
        ///     绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            Team[] team = Team.FindAll();
            foreach (Team t in team)
            {
                cboTeamName.Items.Add(t.TeamName);
            }
        }


        /// <summary>
        ///     添加队别
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
                var teamEntity = new Team();
                teamEntity.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
                teamEntity = Team.Find(teamEntity.TeamId);
                teamInfoForm = new TeamInfoEntering(teamEntity);
            }

            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                bindTeamInfo();
                cboTeamName.Text = teamInfoForm.returnTeamName();
                DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, teamInfoForm.returnTeamName());
            }
        }

        /// <summary>
        ///     队别选择事件（根据队别绑定队员）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.bindTeamMember();
        }

        /// <summary>
        ///     取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     提交按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
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
            if (Text == Const_MS.DAY_REPORT_HC_ADD)
            {
                TunnelDefaultSelect.InsertDefaultTunnel(DayReportHc.TableName, selectWorkingfaceSimple1.IWorkingfaceId);
                insertDayReportHCInfo();
            }
            else if (Text == Const_MS.DAY_REPORT_HC_CHANGE)
            {
                DayReportHc oldDayReportHCEntity = _dayReportHCEntity; //修改前实体
                TunnelDefaultSelect.UpdateDefaultTunnel(DayReportHc.TableName, selectWorkingfaceSimple1.IWorkingfaceId);
                updateDayReportHCInfo();

                DayReportHc newDayReportHCEntity = _dayReportHCEntity; //修改后实体
            }
        }

        /// <summary>
        ///     添加初始化时的回采进尺
        /// </summary>
        private void AddHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid, double hcjc, string bid)
        {
            var dics = new Dictionary<string, string>();
            IPoint prevPnt = null;

            workingFace = WorkingFaceBLL.selectWorkingFaceInfoByWksId(workingFace.WorkingFaceId);

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

            dics[GIS_Const.FIELD_HDID] = hd1.ToString() + "_" + hd2.ToString();
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            int xh = 0;
            if (selcjqs.Count == 0)
                xh = 0;
            else
                xh = Convert.ToInt32(selcjqs[0].Item3[GIS_Const.FIELD_XH]);
            dics[GIS_Const.FIELD_ID] = "0";
            dics[GIS_Const.FIELD_BS] = "0";
            dics[GIS_Const.FIELD_BID] = bid;
            dics[GIS_Const.FIELD_XH] = (xh + 1).ToString();
            IPoint pos = new PointClass();
            Dictionary<string, List<GeoStruct>> dzxlist =
                Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(), qy.ToString(), hcjc, zywid, fywid, qywid, 1,
                    Global.searchlen, dics, true, prevPnt, out pos);

            // 更新工作面信息（预警点坐标）
            if (pos != null)
            {
                workingFace.Coordinate = new Coordinate(pos.X, pos.Y, 0.0);
                WorkingFaceBLL.updateWorkingfaceXYZ(workingFace);
            }

            //更新地质构造表
            if (null != dzxlist && dzxlist.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                foreach (string key in dzxlist.Keys)
                {
                    List<GeoStruct> geoinfos = dzxlist[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        var geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkingFace = workingFace;
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);
                        geologyspaceEntity.TectonicId = tmp.geoinfos[GIS_Const.FIELD_BID].ToString();
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.OnDateTime = DateTime.Now.ToShortDateString();

                        geologyspaceEntity.Save();
                    }
                }
            }
        }

        /// <summary>
        ///     修改回采进尺面上显示信息
        /// </summary>
        private void UpdateHcjc(int hd1, int hd2, int qy, double hcjc, string bid, double zywid, double fywid,
            double qywid)
        {
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            IPoint prevPnt = new PointClass();
            prevPnt.X = workingFace.Coordinate.X;
            prevPnt.Y = workingFace.Coordinate.Y;
            prevPnt.Z = workingFace.Coordinate.Z;

            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            var dics = new Dictionary<string, string>();
            dics.Add(GIS_Const.FIELD_HDID, hd1.ToString() + "_" + hd2.ToString());
            dics.Add(GIS_Const.FIELD_BS, "0");
            dics.Add(GIS_Const.FIELD_BID, bid);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            Dictionary<string, IPoint> results_pts = Global.cons.UpdateHCCD(hd1.ToString(), hd2.ToString(),
                qy.ToString(), bid, hcjc, zywid, fywid, qywid, Global.searchlen);
            if (results_pts == null) return;
            //更新当前修改的回采进尺对应的工作面信息表记录
            int index = 0;
            IPoint pnt = new PointClass();

            foreach (string key in results_pts.Keys)
            {
                workingFace.Coordinate = new Coordinate(results_pts[key].X, results_pts[key].Y, results_pts[key].Z);

                WorkingFaceBLL.updateWorkingfaceXYZ(workingFace);
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
            var hd_ids = new List<int>();
            hd_ids.Add(hd1);
            hd_ids.Add(hd2);
            hd_ids.Add(qy);
            Dictionary<string, List<GeoStruct>> geostructsinfos = Global.commonclss.GetStructsInfos(pnt, hd_ids);
            if (geostructsinfos.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                foreach (string key in geostructsinfos.Keys)
                {
                    List<GeoStruct> geoinfos = geostructsinfos[key];
                    string geo_type = key;
                    for (int i = 0; i < geoinfos.Count; i++)
                    {
                        GeoStruct tmp = geoinfos[i];

                        var geologyspaceEntity = new GeologySpace();
                        geologyspaceEntity.WorkingFace =
                            BasicInfoManager.getInstance().getWorkingFaceById(selectWorkingfaceSimple1.IWorkingfaceId);
                        geologyspaceEntity.TectonicType = Convert.ToInt32(key);

                        geologyspaceEntity.TectonicId = tmp.geoinfos[GIS_Const.FIELD_BID];
                        geologyspaceEntity.Distance = tmp.dist;
                        geologyspaceEntity.OnDateTime = DateTime.Now.ToShortDateString();

                        geologyspaceEntity.Save();
                    }
                }
            }
        }

        /// <summary>
        ///     添加回采日报
        /// </summary>
        private void insertDayReportHCInfo()
        {
            var dayReportHCEntityList = new List<DayReportHc>();
            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                var dayReportHCEntity = new DayReportHc();
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                /**回采日报实体赋值**/
                //队别名称
                dayReportHCEntity.Team = Team.Find(cboTeamName.SelectedValue);
                //绑定回采面编号
                dayReportHCEntity.WorkingFace.WorkingFaceId = selectWorkingfaceSimple1.IWorkingfaceId;

                DataGridViewCellCollection cells = dgrdvDayReportHC.Rows[i].Cells;

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
                    dayReportHCEntity.Remarks = cells[C_COMMENTS].Value.ToString();
                }
                //BID
                dayReportHCEntity.BindingId = IDGenerator.NewBindingID();

                //添加到dayReportHCEntityList中
                dayReportHCEntityList.Add(dayReportHCEntity);
            }

            bool bResult = false;

            //循环添加
            foreach (DayReportHc dayReportHCEntity in dayReportHCEntityList)
            {
                //添加回采进尺日报
                dayReportHCEntity.SaveAndFlush();
                bResult = true;

                // 在图中绘制回采进尺
                if (workingFace != null)
                {
                    double hcjc = dayReportHCEntity.JinChi;
                    string bid = dayReportHCEntity.BindingId;

                    AddHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId, tunnelZY.TunnelWid,
                        tunnelFY.TunnelWid, tunnelQY.TunnelWid,
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
                var msg = new UpdateWarningDataMsg(selectWorkingfaceSimple1.IWorkingfaceId,
                    Const.INVALID_ID,
                    DayReportHc.TableName, OPERATION_TYPE.ADD, DateTime.Now);
                MainForm.SendMsg2Server(msg);
                Log.Debug("发送地址构造消息------完成" + msg.ToString());
            }
        }

        /// <summary>
        ///     修改回采日报信息
        /// </summary>
        private void updateDayReportHCInfo()
        {
            //绑定回采面编号
            _dayReportHCEntity.WorkingFace.WorkingFaceId = selectWorkingfaceSimple1.IWorkingfaceId;
            //队别名称
            _dayReportHCEntity.Team.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
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

            DataGridViewCellCollection cells = dgrdvDayReportHC.Rows[0].Cells;

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
                _dayReportHCEntity.Remarks = cells[C_COMMENTS].Value.ToString();
            }

            //提交修改
            _dayReportHCEntity.SaveAndFlush();
            bool bResult = true;

            //绘制回采进尺图形
            double hcjc = _dayReportHCEntity.JinChi;
            string bid = _dayReportHCEntity.BindingId;

            UpdateHcjc(tunnelZY.TunnelId, tunnelFY.TunnelId, tunnelQY.TunnelId, hcjc, bid, tunnelZY.TunnelWid,
                tunnelFY.TunnelWid, tunnelQY.TunnelWid);

            //修改成功
            if (bResult)
            {
                // 工作面坐标修改，需要更新工作面信息
                BasicInfoManager.getInstance().refreshWorkingFaceInfo(workingFace);

                // 通知服务器数据已经修改
                var msg = new UpdateWarningDataMsg(workingFace.WorkingFaceId, tunnelQY.TunnelId,
                    DayReportHc.TableName, OPERATION_TYPE.UPDATE, DateTime.Now);
                MainForm.SendMsg2Server(msg);
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
        ///     三八制选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            //选择三八制
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            // 设置班次名称
            setWorkTimeName();
            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                dgrdvDayReportHC[0, i].Value = dgrdvDayReportHC[0, 0].Value;
            }
        }

        /// <summary>
        ///     验证
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
            if (Validator.IsEmpty(cboTeamName.Text))
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
            if (dgrdvDayReportHC.Rows.Count - 1 == 0)
            {
                //工作面是否选择
                if (Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    if (
                        !WorkingFaceBLL.CheckIsExist(selectWorkingfaceSimple1.IWorkingfaceId,
                            Convert.ToString(dgrdvDayReportHC[1, 0].Value),
                            Convert.ToDateTime(dgrdvDayReportHC[0, 0].Value)))
                    {
                        Alert.alert(Const_MS.WORKINGFACEEXIST + Const.SIGN_EXCLAMATION_MARK);
                        return false;
                    }
                }
                //添加时判断为未录入进尺
                if (Text == Const_MS.DAY_REPORT_HC_ADD)
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

            for (int i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                DataGridViewCellCollection cells = dgrdvDayReportHC.Rows[i].Cells;

                var cell = cells[C_DATE] as DataGridViewTextBoxCell;
                //工作面是否选择
                if (Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    if (
                        !WorkingFaceBLL.CheckIsExist(selectWorkingfaceSimple1.IWorkingfaceId,
                            dgrdvDayReportHC[1, i].Value.ToString(),
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
        ///     进尺输入计算距切眼距离
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
                            if (Text == Const_MS.DAY_REPORT_HC_ADD)
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
                            if (Text == Const_MS.DAY_REPORT_HC_ADD)
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
                    DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
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
            dgrdvDayReportHC.Rows[dgrdvDayReportHC.CurrentCell.RowIndex].Cells[C_DATE].Value = dtp.Text.ToString();
            //时间控件选择时间时，就把时间赋给所在的单元格
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