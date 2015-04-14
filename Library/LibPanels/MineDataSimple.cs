using System;
using System.Globalization;
using System.Windows.Forms;
using LibBusiness;
using LibBusiness.CommonBLL;
using LibCommon;
using LibEntity;
using LibSocket;

namespace LibPanels
{
    public partial class MineDataSimple : Form
    {
        //******定义变量***********
        private readonly GasInfoEntering _gasData = new GasInfoEntering(); //瓦斯
        private readonly GeologicStructureInfoEntering _geologicStructure = new GeologicStructureInfoEntering();
        private readonly ManagementInfoEntering _management = new ManagementInfoEntering(); //管理
        private readonly CoalExistenceInfoEntering _coalExistenceInfo = new CoalExistenceInfoEntering(); //煤层赋存
        private readonly VentilationInfoEntering _ventilationInfo = new VentilationInfoEntering(); //通风

        private readonly object _obj = null;
        private CoalExistence _ceEntity = new CoalExistence(); //煤层赋存实体
        private GasData _gdEntity = new GasData(); //瓦斯实体
        private GeologicStructure _geologicStructureEntity = new GeologicStructure();
        private Management _mEntity = new Management(); //管理实体
        private MineData _mineDataEntity = new MineData();
        private Ventilation _viEntity = new Ventilation(); //通风实体
        private const int FormHeight = 247;

        //*************************

        private Tunnel Tunnel { get; set; }
        private Team Team { get; set; }
        private String Submitter { get; set; }

        private MineData MineData { get; set; }

        public MineDataSimple()
        {
            InitializeComponent();
        }

        public MineDataSimple(MineData obj)
        {
            InitializeComponent();
            MineData = obj;
        }

        public MineDataSimple(Tunnel tunnel, Team team, String submitter)
        {
            InitializeComponent();
            Tunnel = tunnel;
            Team = team;
            Submitter = submitter;
        }

        /// <summary>
        ///     设置班次名称
        /// </summary>
        private void SetWorkTimeName()
        {
            string sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            var strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(rbtn38.Checked ? 1 : 2, sysDateTime);
            if (!string.IsNullOrEmpty(strWorkTimeName))
            {
                cboWorkTime.Text = strWorkTimeName;
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //通用信息
            _mineDataEntity.Tunnel = selectTunnelSimple1.SelectedTunnel;
            _mineDataEntity.CoordinateX = txtCoordinateX.Text == "" ? 0 : Convert.ToDouble(txtCoordinateX.Text);

            _mineDataEntity.CoordinateY = txtCoordinateY.Text == "" ? 0 : Convert.ToDouble(txtCoordinateY.Text);
            _mineDataEntity.CoordinateZ = txtCoordinateZ.Text == "" ? 0 : Convert.ToDouble(txtCoordinateZ.Text);
            _mineDataEntity.Datetime = dtpDateTime.Value;

            _mineDataEntity.WorkStyle = rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46;
            _mineDataEntity.WorkTime = cboWorkTime.Text;
            _mineDataEntity.TeamName = cboTeamName.Text;
            _mineDataEntity.Submitter = cboSubmitter.Text;

            var bResult = false;
            if (_ventilationInfo.WindowState != FormWindowState.Minimized)         //提交通风特有信息
            {
                bResult = SubmitV();
            }
            if (_coalExistenceInfo.WindowState != FormWindowState.Minimized)         //提交煤层赋存特有信息
            {
                bResult = SubmitC();
            }
            if (_gasData.WindowState != FormWindowState.Minimized)         //提交瓦斯特有信息
            {
                bResult = SubmitG();
            }
            if (_management.WindowState != FormWindowState.Minimized)         //提交管理特有信息
            {
                bResult = SubmitM();

            }
            if (_geologicStructure.WindowState != FormWindowState.Minimized)     //提交地质构造特有信息
            {
                bResult = SubmitGeologicStructure();
            }
            //关闭窗体
            if (!bResult) return;
            _ventilationInfo.Close();
            _coalExistenceInfo.Close();
            _gasData.Close();
            _management.Close();
            Close();
        }

        /// <summary>
        ///     提交通风特有信息
        /// </summary>
        private bool SubmitV()
        {
            //共通实体转化为通风实体
            _viEntity = _mineDataEntity.ChangeToVentilationInfoEntity();
            //是否有无风区域
            _viEntity.IsNoWindArea = _ventilationInfo.VentilationEntity.IsNoWindArea;
            //是否有微风区域
            _viEntity.IsLightWindArea = _ventilationInfo.VentilationEntity.IsLightWindArea;
            //是否有风流反向区域
            _viEntity.IsReturnWindArea = _ventilationInfo.VentilationEntity.IsReturnWindArea;
            //是否通风断面小于设计断面的2/3
            _viEntity.IsSmall = _ventilationInfo.VentilationEntity.IsSmall;
            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
            _viEntity.IsFollowRule = _ventilationInfo.VentilationEntity.IsFollowRule;

            _viEntity.FaultageArea = _ventilationInfo.VentilationEntity.FaultageArea;

            _viEntity.AirFlow = _ventilationInfo.VentilationEntity.AirFlow;

            bool bResult = false;
            if (Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
            {
                _viEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送添加通风信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    Ventilation.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送添加通风信息的Socket信息完成");
            }
            else if (Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                _viEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送修改通风信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    Ventilation.TableName, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送修改通风信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        ///     提交煤层赋存特有信息
        /// </summary>
        private bool SubmitC()
        {
            //共通实体转化为煤层赋存实体
            _ceEntity = _mineDataEntity.changeToCoalExistenceEntity();
            //是否层理紊乱
            _ceEntity.IsLevelDisorder = _coalExistenceInfo.coalExistenceEntity.IsLevelDisorder;
            //煤厚变化
            _ceEntity.CoalThickChange = _coalExistenceInfo.coalExistenceEntity.CoalThickChange;
            //软分层（构造煤）厚度
            _ceEntity.TectonicCoalThick = _coalExistenceInfo.coalExistenceEntity.TectonicCoalThick;
            //软分层（构造煤）层位是否发生变化
            _ceEntity.IsLevelChange = _coalExistenceInfo.coalExistenceEntity.IsLevelChange;
            //煤体破坏类型
            _ceEntity.CoalDistoryLevel = _coalExistenceInfo.coalExistenceEntity.CoalDistoryLevel;
            //是否煤层走向、倾角突然急剧变化
            _ceEntity.IsTowardsChange = _coalExistenceInfo.coalExistenceEntity.IsTowardsChange;
            //工作面煤层是否处于分叉、合层状态
            _ceEntity.IsCoalMerge = _coalExistenceInfo.coalExistenceEntity.IsCoalMerge;
            //煤层是否松软
            _ceEntity.IsCoalSoft = _coalExistenceInfo.coalExistenceEntity.IsCoalSoft;

            _ceEntity.Datetime = DateTime.Now;
            try
            {
                _ceEntity.SaveAndFlush();
                if (Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
                {
                    var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                        CoalExistence.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                    SocketUtil.SendMsg2Server(msg);
                }
                else if (Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
                {
                    var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                        CoalExistence.TableName, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                    SocketUtil.SendMsg2Server(msg);
                }
                return true;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.ToString());
                return false;
            }
        }



        /// <summary>
        ///     提交瓦斯特有信息
        /// </summary>
        private bool SubmitG()
        {
            _gdEntity = _mineDataEntity.ChangeToGasDataEntity();
            _gdEntity.PowerFailure = _gasData.GasDataEntity.PowerFailure;
            _gdEntity.DrillTimes = _gasData.GasDataEntity.DrillTimes;
            _gdEntity.GasTimes = _gasData.GasDataEntity.GasTimes;
            _gdEntity.TempDownTimes = _gasData.GasDataEntity.TempDownTimes;
            _gdEntity.CoalBangTimes = _gasData.GasDataEntity.CoalBangTimes;
            _gdEntity.CraterTimes = _gasData.GasDataEntity.CraterTimes;
            _gdEntity.StoperTimes = _gasData.GasDataEntity.StoperTimes;
            //瓦斯浓度
            _gdEntity.GasThickness = _gasData.GasDataEntity.GasThickness;
            bool bResult = false;
            if (Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                _gdEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送添加瓦斯信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    GasData.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送添加瓦斯信息的Socket信息完成");
            }
            else if (Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
            {
                Log.Debug("发送修改瓦斯信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    GasData.TableName, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                _gdEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送修改瓦斯信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        ///     提交管理特有信息
        /// </summary>
        private bool SubmitM()
        {
            _mEntity = _mineDataEntity.changeToManagementEntity();
            _mEntity.IsGasErrorNotReport = _management.managementEntity.IsGasErrorNotReport;
            _mEntity.IsWfNotReport = _management.managementEntity.IsWfNotReport;
            _mEntity.IsStrgasNotDoWell = _management.managementEntity.IsStrgasNotDoWell;
            _mEntity.IsRwmanagementNotDoWell = _management.managementEntity.IsRwmanagementNotDoWell;
            _mEntity.IsVfBrokenByPeople = _management.managementEntity.IsVfBrokenByPeople;
            _mEntity.IsElementPlaceNotGood = _management.managementEntity.IsElementPlaceNotGood;
            _mEntity.IsReporterFalseData = _management.managementEntity.IsReporterFalseData;
            _mEntity.IsDrillWrongBuild = _management.managementEntity.IsDrillWrongBuild;
            _mEntity.IsDrillNotDoWell = _management.managementEntity.IsDrillNotDoWell;
            _mEntity.IsOpNotDoWell = _management.managementEntity.IsOpNotDoWell;
            _mEntity.IsOpErrorNotReport = _management.managementEntity.IsOpErrorNotReport;
            _mEntity.IsPartWindSwitchError = _management.managementEntity.IsPartWindSwitchError;
            _mEntity.IsSafeCtrlUninstall = _management.managementEntity.IsSafeCtrlUninstall;
            _mEntity.IsCtrlStop = _management.managementEntity.IsCtrlStop;
            _mEntity.IsGasNotDowWell = _management.managementEntity.IsGasNotDowWell;
            _mEntity.IsMineNoChecker = _management.managementEntity.IsMineNoChecker;
            bool bResult = false;
            if (Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                _mEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送添加管理信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    Management.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送添加管理信息的Socket信息完成");
            }
            else if (Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
            {
                _mEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送修改管理信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                    Management.TableName, OPERATION_TYPE.UPDATE, _mEntity.Datetime);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送修改管理信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        ///     提交地质构造特有信息
        /// </summary>
        /// <returns></returns>
        private bool SubmitGeologicStructure()
        {
            _geologicStructureEntity = _mineDataEntity.changeToGeologicStructureEntity();
            _geologicStructureEntity.NoPlanStructure = _geologicStructure.geoligicStructureEntity.NoPlanStructure;
            _geologicStructureEntity.PassedStructureRuleInvalid =
                _geologicStructure.geoligicStructureEntity.PassedStructureRuleInvalid;
            _geologicStructureEntity.YellowRuleInvalid = _geologicStructure.geoligicStructureEntity.YellowRuleInvalid;
            _geologicStructureEntity.RoofBroken = _geologicStructure.geoligicStructureEntity.RoofBroken;
            _geologicStructureEntity.CoalSeamSoft = _geologicStructure.geoligicStructureEntity.CoalSeamSoft;
            _geologicStructureEntity.CoalSeamBranch = _geologicStructure.geoligicStructureEntity.CoalSeamBranch;
            _geologicStructureEntity.RoofChange = _geologicStructure.geoligicStructureEntity.RoofChange;
            _geologicStructureEntity.GangueDisappear = _geologicStructure.geoligicStructureEntity.GangueDisappear;
            _geologicStructureEntity.GangueLocationChange =
                _geologicStructure.geoligicStructureEntity.GangueLocationChange;
            bool bResult = false;
            if (Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                _geologicStructureEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送添加地址构造信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                     GeologicStructure.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送添加地址构造信息的Socket信息完成");
            }
            else if (Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
            {
                _geologicStructureEntity.SaveAndFlush();
                bResult = true;
                Log.Debug("发送修改地址构造信息的Socket信息");
                var msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelSimple1.SelectedTunnel.TunnelId,
                     GeologicStructure.TableName, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                SocketUtil.SendMsg2Server(msg);
                Log.Debug("发送修改地址构造信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>selectTunnelSimple1.SelectedTunnel.TunnelId
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            // 判断巷道信息是否选择
            //矿井名称
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道信息");
                return false;
            }
            //坐标X是否为数字
            if (txtCoordinateX.Text != "")
            {
                if (!LibCommon.Check.IsNumeric(txtCoordinateX, "坐标X"))
                {
                    return false;
                }
            }
            //坐标Y是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!LibCommon.Check.IsNumeric(txtCoordinateY, "坐标Y"))
                {
                    return false;
                }
            }
            //坐标Z是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!LibCommon.Check.IsNumeric(txtCoordinateY, "坐标Y"))
                {
                    return false;
                }
            }
            //煤层赋存特有检查
            if (_coalExistenceInfo.WindowState != FormWindowState.Minimized)
            {
                if (!_coalExistenceInfo.check())
                {
                    return false;
                }
            }
            //瓦斯数据特有检查
            if (_gasData.WindowState != FormWindowState.Minimized)
            {
                if (!_gasData.check())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     工作制式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn38.Checked)
            {
                cboWorkTime.Text = "";
                DataBindUtil.LoadWorkTime(cboWorkTime, Const_MS.WORK_GROUP_ID_38);
            }
            else
            {
                cboWorkTime.Text = "";
                DataBindUtil.LoadWorkTime(cboWorkTime, Const_MS.WORK_GROUP_ID_46);
            }

            // 设置班次名称
            SetWorkTimeName();
        }

        private void MineDataSimple_Load(object sender, EventArgs e)
        {
            DataBindUtil.LoadTeam(cboTeamName);
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);

            if (WorkingTimeDefault.FindFirst().DefaultWorkTimeGroupId == Const_MS.WORK_GROUP_ID_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            // 设置班次名称
            SetWorkTimeName();

            //窗体绑定到Panel中
            _ventilationInfo.MdiParent = this;
            _ventilationInfo.Parent = panel2;
            _coalExistenceInfo.MdiParent = this;
            _coalExistenceInfo.Parent = panel2;
            _gasData.MdiParent = this;
            _gasData.Parent = panel2;
            _management.MdiParent = this;
            _management.Parent = panel2;
            _geologicStructure.MdiParent = this;
            _geologicStructure.Parent = panel2;

            //panel2绑定窗体
            panel2.Controls.Add(_coalExistenceInfo);
            panel2.Controls.Add(_ventilationInfo);
            panel2.Controls.Add(_gasData);
            panel2.Controls.Add(_management);
            panel2.Controls.Add(_geologicStructure);

            if (Tunnel != null) selectTunnelSimple1.SetTunnel(Tunnel);
            if (Team != null) cboTeamName.SelectedText = Team.TeamName;
            if (MineData != null)
            {
                selectTunnelSimple1.SetTunnel(MineData.Tunnel);
                txtCoordinateX.Text = MineData.Tunnel.WorkingFace.CoordinateX.ToString(CultureInfo.InvariantCulture);
                txtCoordinateY.Text = MineData.Tunnel.WorkingFace.CoordinateY.ToString(CultureInfo.InvariantCulture);
                txtCoordinateZ.Text = MineData.Tunnel.WorkingFace.CoordinateZ.ToString(CultureInfo.InvariantCulture);

                if (MineData.WorkStyle == "三八制")
                {
                    rbtn38.Checked = true;
                    rbtn46.Checked = false;
                }
                else
                {
                    rbtn46.Checked = true;
                    rbtn38.Checked = false;
                }
                cboWorkTime.SelectedValue = MineData.WorkTime;
                cboTeamName.SelectedText = MineData.TeamName;
                cboSubmitter.SelectedText = MineData.Submitter;

                if (MineData is CoalExistence)
                {
                    var coalexistence = (CoalExistence)MineData;
                    _coalExistenceInfo.bindDefaultValue(coalexistence);
                    Height = FormHeight + _coalExistenceInfo.Height;
                    _coalExistenceInfo.WindowState = FormWindowState.Maximized;
                    _coalExistenceInfo.Show();
                    _coalExistenceInfo.Activate();
                }
                else if (MineData is GasData)
                {
                    var gasData = (GasData)MineData;
                    _gasData.bindDefaultValue(gasData);
                    Height = FormHeight + _gasData.Height;
                    _gasData.WindowState = FormWindowState.Maximized;
                    _gasData.Show();
                    _gasData.Activate();
                }
                else if (MineData is GeologicStructure)
                {
                    var geologicStructure = (GeologicStructure)MineData;
                    _geologicStructure.bindDefaultValue(geologicStructure);
                    Height = FormHeight + _geologicStructure.Height;
                    _geologicStructure.WindowState = FormWindowState.Maximized;
                    _geologicStructure.Show();
                    _geologicStructure.Activate();
                }
                else if (MineData is Ventilation)
                {
                    var ventilation = (Ventilation)MineData;
                    _ventilationInfo.bindDefaultValue(ventilation);
                    Height = FormHeight + _ventilationInfo.Height;
                    _ventilationInfo.WindowState = FormWindowState.Maximized;
                    _ventilationInfo.Show();
                    _ventilationInfo.Activate();
                }
                else if (MineData is Management)
                {
                    var management = (Management)MineData;
                    _management.bindDefaultValue(management);
                    Height = FormHeight + _management.Height;
                    _management.WindowState = FormWindowState.Maximized;
                    _management.Show();
                    _management.Activate();
                }
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(Submitter)) cboSubmitter.Text = Submitter;

                if (Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
                {
                    _viEntity = (Ventilation)_obj;
                }
                if (Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
                {
                    _ceEntity = (CoalExistence)_obj;
                }
                if (Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
                {
                    _gdEntity = (GasData)_obj;
                }
                if (Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
                {
                    _mEntity = (Management)_obj;
                }
                if (Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
                {
                    _geologicStructureEntity = (GeologicStructure)_obj;
                }

                //所有小窗体最小化
                //AllMin();
                //通风
                if (Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
                {
                    Height = FormHeight + _ventilationInfo.Height;
                    _ventilationInfo.WindowState = FormWindowState.Maximized;
                    _ventilationInfo.Show();
                    _ventilationInfo.Activate();
                }
                //煤层赋存
                if (Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
                {
                    Height = FormHeight + _coalExistenceInfo.Height;
                    _coalExistenceInfo.WindowState = FormWindowState.Maximized;
                    _coalExistenceInfo.Show();
                    _coalExistenceInfo.Activate();
                }
                //瓦斯
                if (Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
                {
                    Height = FormHeight + _gasData.Height;
                    _gasData.WindowState = FormWindowState.Maximized;
                    _gasData.Show();
                    _gasData.Activate();
                }
                //管理
                if (Text == new LibPanels(MineDataPanelName.Management).panelFormName)
                {
                    Height = FormHeight + _management.Height;
                    _management.WindowState = FormWindowState.Maximized;
                    _management.Show();
                    _management.Activate();
                }
                //地质构造
                if (Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
                {
                    Height = FormHeight + _geologicStructure.Height;
                    _geologicStructure.WindowState = FormWindowState.Maximized;
                    _geologicStructure.Show();
                    _geologicStructure.Activate();
                }

                //绑定通风修改初始信息
                if (Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
                {
                    Height = FormHeight + _ventilationInfo.Height;
                    ChangeMineCommonValue(_viEntity);

                    _ventilationInfo.VentilationEntity = _viEntity;

                    _ventilationInfo.bindDefaultValue(_viEntity);

                    _ventilationInfo.WindowState = FormWindowState.Maximized;
                    _ventilationInfo.Show();
                    _ventilationInfo.Activate();
                }

                //绑定煤层赋存修改初始信息
                if (Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
                {
                    Height = FormHeight + _coalExistenceInfo.Height;
                    ChangeMineCommonValue(_ceEntity);

                    _coalExistenceInfo.coalExistenceEntity = _ceEntity;

                    _coalExistenceInfo.bindDefaultValue(_ceEntity);

                    _coalExistenceInfo.WindowState = FormWindowState.Maximized;
                    _coalExistenceInfo.Show();
                    _coalExistenceInfo.Activate();
                }

                //绑定瓦斯修改初始信息
                if (Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
                {
                    Height = FormHeight + _gasData.Height;
                    ChangeMineCommonValue(_gdEntity);

                    _gasData.GasDataEntity = _gdEntity;
                    _gasData.bindDefaultValue(_gdEntity);

                    _gasData.WindowState = FormWindowState.Maximized;
                    _gasData.Show();
                    _gasData.Activate();
                }
                //绑定管理修改初始信息
                if (Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
                {
                    Height = FormHeight + _management.Height;
                    ChangeMineCommonValue(_mEntity);

                    _management.managementEntity = _mEntity;

                    _management.bindDefaultValue(_mEntity);

                    _management.WindowState = FormWindowState.Maximized;
                    _management.Show();
                    _management.Activate();
                }
                //绑定地质构造修改初始数据
                if (Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
                {
                    Height = FormHeight + _management.Height;
                    ChangeMineCommonValue(_geologicStructureEntity);

                    _geologicStructure.geoligicStructureEntity = _geologicStructureEntity;
                    _geologicStructure.bindDefaultValue(_geologicStructureEntity);

                    _geologicStructure.WindowState = FormWindowState.Maximized;
                    _geologicStructure.Show();
                    _geologicStructure.Activate();
                }
            }
        }

        /// <summary>
        ///     绑定井下数据通用信息
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeMineCommonValue(object obj)
        {
            _mineDataEntity = (MineData)obj;
            txtCoordinateX.Text = _mineDataEntity.CoordinateX.ToString(CultureInfo.InvariantCulture);
            txtCoordinateY.Text = _mineDataEntity.CoordinateY.ToString(CultureInfo.InvariantCulture);
            txtCoordinateZ.Text = _mineDataEntity.CoordinateZ.ToString(CultureInfo.InvariantCulture);

            if (_mineDataEntity.WorkStyle == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            if (_mineDataEntity.WorkStyle == Const_MS.WORK_TIME_46)
            {
                rbtn46.Checked = true;
            }
            cboWorkTime.Text = _mineDataEntity.WorkTime;
            cboTeamName.Text = _mineDataEntity.TeamName;
            cboSubmitter.Text = _mineDataEntity.Submitter;
            dtpDateTime.Value = _mineDataEntity.Datetime;
        }

        /// <summary>
        ///     所有的窗体都最小化
        /// </summary>
        //private void AllMin()
        //{
        //    _coalExistenceInfo.WindowState = FormWindowState.Minimized;
        //    _ventilationInfo.WindowState = FormWindowState.Minimized;
        //    _gasData.WindowState = FormWindowState.Minimized;
        //    _usualForecast.WindowState = FormWindowState.Minimized;
        //    _management.WindowState = FormWindowState.Minimized;
        //    _geologicStructure.WindowState = FormWindowState.Minimized;
        //}

        /// <summary>
        ///     队别选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
        }

        private void MineDataSimple_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}