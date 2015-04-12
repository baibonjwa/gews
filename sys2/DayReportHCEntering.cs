using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.HdProc;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibEntity;
using LibEntity.Domain;
using LibPanels;
using LibSocket;

namespace sys2
{
    public partial class DayReportHcEntering : Form
    {
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
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_ADD);

            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
            dgrdvDayReportHC[2, 0].Value = "回采";
            //绑定队别名称
            DataBindUtil.LoadTeam(cboTeamName);
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
            SetWorkTimeName();
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        public DayReportHcEntering(DayReportHc dayReportHc)
        {
            InitializeComponent();
            _dayReportHc = dayReportHc;
            //修改初始化
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.DAY_REPORT_HC_CHANGE);

            //this.selectWorkingFaceControl1.setCurSelectedID(_arr);

            dgrdvDayReportHC.AllowUserToAddRows = false;

            dgrdvDayReportHC.Controls.Add(dtp); //把时间控件加入DataGridView
            dtp.Visible = false; //先不让它显示
            dtp.Format = DateTimePickerFormat.Custom; //设置日期格式为2010-08-05
            dtp.TextChanged += dtp_TextChange; //为时间控件加入事件dtp_TextChange
            dgrdvDayReportHC.DataError += delegate { };
        }

        /// <summary>
        ///     设置班次名称
        /// </summary>
        private void SetWorkTimeName()
        {
            var sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            var strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(
                rbtn38.Checked ? 1 : 2, sysDateTime);

            if (!string.IsNullOrEmpty(strWorkTimeName))
            {
                dgrdvDayReportHC[C_WORK_TIME, 0].Value = strWorkTimeName;
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
                var teamEntity = new Team {TeamId = Convert.ToInt32(cboTeamName.SelectedValue)};
                teamEntity = Team.Find(teamEntity.TeamId);
                teamInfoForm = new TeamInfoEntering(teamEntity);
            }

            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                DataBindUtil.LoadTeam(cboTeamName);
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
            var workingFaceHc = WorkingFaceHc.FindByWorkingFace(selectWorkingfaceSimple1.SelectedWorkingFace);
            if (workingFaceHc == null || workingFaceHc.TunnelZy == null || workingFaceHc.TunnelFy == null ||
                workingFaceHc.TunnelQy == null)
            {
                Alert.alert("所选工作面缺少主运、辅运或切眼巷道");
            }
            else
            {
                if (Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    insertDayReportHCInfo();
                }
                else if (Text == Const_MS.DAY_REPORT_HC_CHANGE)
                {
                    UpdateDayReportHcInfo();
                }
            }
        }

        /// <summary>
        ///     添加初始化时的回采进尺
        /// </summary>
        private void AddHcjc(int hd1, int hd2, int qy, double zywid, double fywid, double qywid, double hcjc, string bid)
        {
            var dics = new Dictionary<string, string>();
            IPoint prevPnt;
            var workingFace = selectWorkingfaceSimple1.SelectedWorkingFace;
            if (selectWorkingfaceSimple1.SelectedWorkingFace != null)
            {
                prevPnt = new PointClass
                {
                    X = workingFace.CoordinateX,
                    Y = workingFace.CoordinateY,
                    Z = workingFace.CoordinateZ
                };
            }
            else
            {
                Log.Error("[回采进尺]:工作面为空值!");
                throw new ArgumentException("[回采进尺]:工作面为空值!", "workingFace");
            }

            dics[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            var selcjqs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            var xh = 0;
            xh = selcjqs.Count == 0 ? 0 : Convert.ToInt32(selcjqs[0].Item3[GIS_Const.FIELD_XH]);
            dics[GIS_Const.FIELD_ID] = "0";
            dics[GIS_Const.FIELD_BS] = "0";
            dics[GIS_Const.FIELD_BID] = bid;
            dics[GIS_Const.FIELD_XH] = (xh + 1).ToString();
            IPoint pos = new PointClass();
            var dzxlist =
                Global.cons.DrawHDHC(hd1.ToString(), hd2.ToString(), qy.ToString(), hcjc, zywid, fywid, qywid, 1,
                    Global.searchlen, dics, true, prevPnt, out pos);
            // 更新工作面信息（预警点坐标）
            if (pos != null)
            {
                workingFace.SetCoordinate(pos.X, pos.Y, 0.0);
                workingFace.Save();
            }

            //更新地质构造表
            if (null != dzxlist && dzxlist.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                foreach (var key in dzxlist.Keys)
                {
                    var geoinfos = dzxlist[key];
                    var geo_type = key;
                    foreach (var tmp in geoinfos)
                    {
                        var geologyspaceEntity = new GeologySpace
                        {
                            WorkingFace = workingFace,
                            TectonicType = Convert.ToInt32(key),
                            TectonicId = tmp.geoinfos[GIS_Const.FIELD_BID],
                            Distance = tmp.dist,
                            OnDateTime = DateTime.Now.ToShortDateString()
                        };

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
            var workingFace = selectWorkingfaceSimple1.SelectedWorkingFace;
            IPoint prevPnt = new PointClass();
            prevPnt.X = workingFace.CoordinateX;
            prevPnt.Y = workingFace.CoordinateY;
            prevPnt.Z = workingFace.CoordinateZ;

            //已经存在回采进尺的，计算回采进尺点，保存到工作面表中，同时将绘制回采
            var dics = new Dictionary<string, string>();
            dics.Add(GIS_Const.FIELD_HDID, hd1 + "_" + hd2);
            dics.Add(GIS_Const.FIELD_BS, "0");
            dics.Add(GIS_Const.FIELD_BID, bid);
            var selcjqs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, dics);
            var results_pts = Global.cons.UpdateHCCD(hd1.ToString(), hd2.ToString(),
                qy.ToString(), bid, hcjc, zywid, fywid, qywid, Global.searchlen);
            if (results_pts == null) return;
            //更新当前修改的回采进尺对应的工作面信息表记录
            var index = 0;
            IPoint pnt = new PointClass();

            foreach (var key in results_pts.Keys)
            {
                workingFace.SetCoordinate(results_pts[key].X, results_pts[key].Y, results_pts[key].Z);
                workingFace.Save();
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
            var geostructsinfos = Global.commonclss.GetStructsInfos(pnt, hd_ids);
            if (geostructsinfos.Count > 0)
            {
                GeologySpaceBll.DeleteGeologySpaceEntityInfos(workingFace.WorkingFaceId); //删除工作面ID对应的地质构造信息
                foreach (var key in geostructsinfos.Keys)
                {
                    var geoinfos = geostructsinfos[key];
                    var geo_type = key;
                    for (var i = 0; i < geoinfos.Count; i++)
                    {
                        var tmp = geoinfos[i];

                        var geologyspaceEntity = new GeologySpace
                        {
                            WorkingFace = selectWorkingfaceSimple1.SelectedWorkingFace,
                            TectonicType = Convert.ToInt32(key),
                            TectonicId = tmp.geoinfos[GIS_Const.FIELD_BID],
                            Distance = tmp.dist,
                            OnDateTime = DateTime.Now.ToShortDateString()
                        };

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
            var workingFace = selectWorkingfaceSimple1.SelectedWorkingFace;
            var dayReportHCEntityList = new List<DayReportHc>();
            for (var i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                var dayReportHCEntity = new DayReportHc();
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                /**回采日报实体赋值**/
                //队别名称
                dayReportHCEntity.Team = (Team) cboTeamName.SelectedItem;
                //绑定回采面编号
                dayReportHCEntity.WorkingFace = selectWorkingfaceSimple1.SelectedWorkingFace;

                var cells = dgrdvDayReportHC.Rows[i].Cells;

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

            var bResult = false;

            //循环添加
            foreach (var dayReportHCEntity in dayReportHCEntityList)
            {
                // 在图中绘制回采进尺
                if (workingFace != null)
                {
                    var hcjc = dayReportHCEntity.JinChi;
                    var bid = dayReportHCEntity.BindingId;
                    var workingFaceHc = WorkingFaceHc.FindByWorkingFace(workingFace);
                    AddHcjc(workingFaceHc.TunnelZy.TunnelId, workingFaceHc.TunnelFy.TunnelId,
                        workingFaceHc.TunnelQy.TunnelId, workingFaceHc.TunnelZy.TunnelWid,
                        workingFaceHc.TunnelFy.TunnelWid, workingFaceHc.TunnelQy.TunnelWid,
                        hcjc, bid);
                    dayReportHCEntity.SaveAndFlush();
                    bResult = true;
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
            }
            else
            {
                // 工作面坐标已经改变，需要更新工作面信息
                Log.Debug("发送地址构造消息------开始");
                // 通知服务端回采进尺已经添加
                var msg = new UpdateWarningDataMsg(selectWorkingfaceSimple1.SelectedWorkingFace.WorkingFaceId,
                    Const.INVALID_ID,
                    DayReportHc.TableName, OPERATION_TYPE.ADD, DateTime.Now);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送地址构造消息------完成" + msg);
            }
        }

        /// <summary>
        ///     修改回采日报信息
        /// </summary>
        private void UpdateDayReportHcInfo()
        {
            _dayReportHc.WorkingFace.WorkingFaceId = selectWorkingfaceSimple1.SelectedWorkingFace.WorkingFaceId;
            //队别名称
            _dayReportHc.Team.TeamId = Convert.ToInt32(cboTeamName.SelectedValue);
            //日期
            //_dayReportHCEntity.DateTime = dtpDate.Value;
            //填报人
            _dayReportHc.Submitter = cboSubmitter.Text;
            //工作制式
            if (rbtn38.Checked)
            {
                _dayReportHc.WorkTimeStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                _dayReportHc.WorkTimeStyle = rbtn46.Text;
            }

            var cells = dgrdvDayReportHC.Rows[0].Cells;

            //创建日期
            if (cells[C_DATE].Value != null)
            {
                _dayReportHc.DateTime = Convert.ToDateTime(cells[C_DATE].Value.ToString());
            }
            //班次
            if (cells[C_WORK_TIME].Value != null)
            {
                _dayReportHc.WorkTime = cells[C_WORK_TIME].Value.ToString();
            }

            //工作内容
            if (cells[C_WORK_CONTENT].Value != null)
            {
                _dayReportHc.WorkInfo = cells[C_WORK_CONTENT].Value.ToString();
            }

            //回采进尺
            if (cells[C_WORK_PROGRESS].Value != null)
            {
                _dayReportHc.JinChi = Convert.ToDouble(cells[C_WORK_PROGRESS].Value);
            }

            //备注
            if (cells[C_COMMENTS].Value != null)
            {
                _dayReportHc.Remarks = cells[C_COMMENTS].Value.ToString();
            }

            //提交修改
            _dayReportHc.SaveAndFlush();
            var bResult = true;

            //绘制回采进尺图形
            var hcjc = _dayReportHc.JinChi;
            var bid = _dayReportHc.BindingId;
            var workingFace = selectWorkingfaceSimple1.SelectedWorkingFace;
            var workingFaceHc = WorkingFaceHc.FindByWorkingFace(workingFace);
            UpdateHcjc(workingFaceHc.TunnelZy.TunnelId, workingFaceHc.TunnelFy.TunnelId, workingFaceHc.TunnelQy.TunnelId,
                hcjc, bid, workingFaceHc.TunnelZy.TunnelWid,
                workingFaceHc.TunnelFy.TunnelWid, workingFaceHc.TunnelQy.TunnelWid);


            // 通知服务器数据已经修改
            var msg = new UpdateWarningDataMsg(workingFace.WorkingFaceId, workingFaceHc.TunnelQy.TunnelId,
                DayReportHc.TableName, OPERATION_TYPE.UPDATE, DateTime.Now);
            SocketUtil.SendMsg2Server(msg);
        }

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
            SetWorkTimeName();
            for (var i = 0; i < dgrdvDayReportHC.RowCount; i++)
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
            if (selectWorkingfaceSimple1.SelectedWorkingFace == null)
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
                //添加时判断为未录入进尺
                if (Text == Const_MS.DAY_REPORT_HC_ADD)
                {
                    Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_MS.JC + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //修改时
                var bResult = false;
                //为空返回false，不数据时跳出循环
                for (var i = 0; i < dgrdvDayReportHC.ColumnCount; i++)
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

            for (var i = 0; i < dgrdvDayReportHC.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvDayReportHC.RowCount - 1)
                {
                    break;
                }

                var cells = dgrdvDayReportHC.Rows[i].Cells;

                var cell = cells[C_DATE] as DataGridViewTextBoxCell;
                //工作面是否选择
                cell = cells[C_WORK_PROGRESS] as DataGridViewTextBoxCell;
                //进尺为空
                if (cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_NOT_NULL + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //进尺不为数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_MS.JC + Const.MSG_MUST_NUMBER + Const_MS.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
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
                                dgrdvDayReportHC[e.ColumnIndex, e.RowIndex].Value = _dayReportHc.JinChi;
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
            for (var i = 0; i < cboTeamName.Items.Count; i++)
            {
                if (cboTeamName.Text == cboTeamName.GetItemText(cboTeamName.Items[i]))
                {
                    DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
                    break;
                }
                cboSubmitter.Items.Clear();
                cboSubmitter.Text = "";
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
            dgrdvDayReportHC.Rows[dgrdvDayReportHC.CurrentCell.RowIndex].Cells[C_DATE].Value = dtp.Text;
            //时间控件选择时间时，就把时间赋给所在的单元格
        }

        private void dgrdvDayReportHC_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //日期自动填充
            if (e.ColumnIndex == C_DATE)
            {
                //datetimepicker控件位置大小
                var rect = dgrdvDayReportHC.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
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
        private readonly DateTimePicker dtp = new DateTimePicker(); //这里实例化一个DateTimePicker控件

        private readonly DayReportHc _dayReportHc;

        #endregion
    }
}