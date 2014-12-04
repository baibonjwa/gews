using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibCommonForm;
using LibEntity;
using LibCommon;
using LibPanels;
using LibSocket;
using LibBusiness.CommonBLL;
using LibCommonControl;

namespace LibPanels
{
    public partial class MineDataSimple
    {
        //******定义变量***********
        VentilationInfoEntering ventilationInfoEntering = new VentilationInfoEntering();      //通风
        public CoalExistenceInfoEntering coalExistence = new CoalExistenceInfoEntering();                          //煤层赋存
        GasInfoEntering gasData = new GasInfoEntering();                          //瓦斯
        UsualForecast usualForecast = new UsualForecast();              //日常预测
        ManagementInfoEntering management = new ManagementInfoEntering();                    //管理
        Tunnel tunnelEntity = new Tunnel();                 //巷道信息实体
        VentilationInfoEntity viEntity = new VentilationInfoEntity();   //通风实体
        CoalExistenceEntity ceEntity = new CoalExistenceEntity();       //煤层赋存实体
        GasDataEntity gdEntity = new GasDataEntity();       //瓦斯实体
        UsualForecastEntity ufEntity = new UsualForecastEntity();       //日常预测实体
        ManagementEntity mEntity = new ManagementEntity();  //管理实体
        MineDataEntity mineDataEntity = new MineDataEntity();
        GeologicStructureInfoEntering geologicStructure = new GeologicStructureInfoEntering();
        GeologicStructureEntity geologicStructureEntity = new GeologicStructureEntity();
        int[] arr = new int[5];
        object obj = null;
        int formHeight = 247;

        //*************************

        public MineDataSimple(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();
            selectTunnelSimple1.TunnelNameChanged += InheritTunnelNameChanged;

            addInfo();

            // 设置班次名称
            setWorkTimeName();

        }

        private void InheritTunnelNameChanged(object sender, TunnelEventArgs e)
        {
            Tunnel entTunnel = BasicInfoManager.getInstance().getTunnelByID(selectTunnelSimple1.ITunnelId);
            WorkingFace entWorkingFace =
                BasicInfoManager.getInstance().getWorkingFaceById(entTunnel.WorkingFace.WorkingFaceID); //WorkingFaceBLL.selectWorkingFaceInfoByID(entTunnel.WorkingFace.WorkingFaceID);
            txtCoordinateX.Text = entWorkingFace.Coordinate.X.ToString();
            txtCoordinateY.Text = entWorkingFace.Coordinate.Y.ToString();
            txtCoordinateZ.Text = entWorkingFace.Coordinate.Z.ToString();
        }

        /// <summary>
        /// 添加时初始化
        /// </summary>
        private void addInfo()
        {
            this.bindTeamInfo();
            this.bindWorkTimeFirstTime();

            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }

            // CommentByWuxin-2014/05/27
            //cboWorkTime.Text = workTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
        }

        /// <summary>
        /// 设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToString("HH:mm:ss");

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
                cboWorkTime.Text = strWorkTimeName;
            }
        }

        private void changeInfo()
        {
            addInfo();
        }

        // CommentByWuxin-2014/05/27
        ///// <summary>
        ///// 返回班次名
        ///// </summary>
        ///// <param name="workStyle">工作制式名</param>
        ///// <returns>班次名</returns>
        //public static string workTime(string workStyle)
        //{
        //    DataSet ds = WorkTimeBLL.returnWorkTime(workStyle);
        //    int hour = DateTime.Now.Hour;
        //    string workTime = "";
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        if (hour > Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString().Remove(2)) && hour <= Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString().Remove(2)))
        //        {
        //            workTime = ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
        //        }
        //    }
        //    return workTime;
        //}

        /// <summary>
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            DataSet ds = TeamBLL.selectTeamInfo();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboTeamName.Items.Add(ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_NAME].ToString());
            }
        }

        /// <summary>
        /// 绑定班次
        /// </summary>
        private void bindWorkTimeFirstTime()
        {
            DataSet dsWorkTime;
            if (rbtn38.Checked)
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
            }
            else
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
            }
            for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
            {
                cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
            }
        }

        //绑定填报人
        private void bindTeamMember()
        {
            cboSubmitter.Items.Clear();
            cboSubmitter.Text = "";
            DataSet ds = TeamBLL.selectTeamInfoByTeamName(cboTeamName.Text);
            string teamLeader = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_LEADER].ToString();
            string[] teamMember = ds.Tables[0].Rows[0][TeamDbConstNames.TEAM_MEMBER].ToString().Split(',');
            cboSubmitter.Items.Add(teamLeader);
            for (int i = 0; i < teamMember.Length; i++)
            {
                cboSubmitter.Items.Add(teamMember[i]);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //通用信息
            mineDataEntity.Tunnel.TunnelID = this.selectTunnelSimple1.ITunnelId;
            if (txtCoordinateX.Text == "")
            {
                mineDataEntity.CoordinateX = 0;
            }
            else
            {
                mineDataEntity.CoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            }

            if (txtCoordinateY.Text == "")
            {
                mineDataEntity.CoordinateY = 0;
            }
            else
            {
                mineDataEntity.CoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            }
            if (txtCoordinateZ.Text == "")
            {
                mineDataEntity.CoordinateZ = 0;
            }
            else
            {
                mineDataEntity.CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            }

            mineDataEntity.Datetime = dtpDateTime.Value;

            if (rbtn38.Checked)
            {
                mineDataEntity.WorkStyle = Const_MS.WORK_TIME_38;
            }
            else
            {
                mineDataEntity.WorkStyle = Const_MS.WORK_TIME_46;
            }
            mineDataEntity.WorkTime = cboWorkTime.Text;
            mineDataEntity.TeamName = cboTeamName.Text;
            mineDataEntity.Submitter = cboSubmitter.Text;

            bool bResult = false;
            if (ventilationInfoEntering.WindowState != FormWindowState.Minimized)         //提交通风特有信息
            {
                bResult = submitV();
            }
            if (coalExistence.WindowState != FormWindowState.Minimized)         //提交煤层赋存特有信息
            {
                bResult = submitC();
            }
            if (gasData.WindowState != FormWindowState.Minimized)         //提交瓦斯特有信息
            {
                bResult = submitG();
            }
            if (usualForecast.WindowState != FormWindowState.Minimized)         //提交日常预测特有信息
            {
                bResult = submitU();
            }
            if (management.WindowState != FormWindowState.Minimized)         //提交管理特有信息
            {
                bResult = submitM();

            }
            if (geologicStructure.WindowState != FormWindowState.Minimized) // 提交地质构造信息
            {
                bResult = submitGeologicStructure();
            }

            if (bResult)
            {
                Alert.noteMsg("提交成功!");

                ventilationInfoEntering.Close();
                coalExistence.Close();
                gasData.Close();
                usualForecast.Close();
                management.Close();

                this.Close();
            }
        }

        /// <summary>
        /// 提交通风特有信息
        /// </summary>
        private bool submitV()
        {
            //共通实体转化为通风实体
            viEntity = mineDataEntity.ChangeToVentilationInfoEntity();
            //是否有无风区域
            viEntity.IsNoWindArea = ventilationInfoEntering.ventilationInfoEntity.IsNoWindArea;
            //是否有微风区域
            viEntity.IsLightWindArea = ventilationInfoEntering.ventilationInfoEntity.IsLightWindArea;
            //是否有风流反向区域
            viEntity.IsReturnWindArea = ventilationInfoEntering.ventilationInfoEntity.IsReturnWindArea;
            //是否通风断面小于设计断面的2/3
            viEntity.IsSmall = ventilationInfoEntering.ventilationInfoEntity.IsSmall;
            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
            viEntity.IsFollowRule = ventilationInfoEntering.ventilationInfoEntity.IsFollowRule;

            viEntity.FaultageArea = ventilationInfoEntering.ventilationInfoEntity.FaultageArea;

            viEntity.AirFlow = ventilationInfoEntering.ventilationInfoEntity.AirFlow;

            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
            {
                bResult = VentilationBLL.insertVentilationInfo(viEntity);
                Log.Debug("发送添加通风信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    VentilationDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送添加通风信息的Socket信息完成");
            }
            else if (this.Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                bResult = VentilationBLL.updateVentilationInfo(viEntity);
                Log.Debug("发送修改通风信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    VentilationDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送修改通风信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        /// 提交煤层赋存特有信息
        /// </summary>
        private bool submitC()
        {
            ceEntity = mineDataEntity.changeToCoalExistenceEntity();
            ceEntity.IsLevelDisorder = coalExistence.coalExistenceEntity.IsLevelDisorder;
            ceEntity.CoalThickChange = coalExistence.coalExistenceEntity.CoalThickChange;
            ceEntity.TectonicCoalThick = coalExistence.coalExistenceEntity.TectonicCoalThick;
            ceEntity.IsLevelChange = coalExistence.coalExistenceEntity.IsLevelChange;
            ceEntity.CoalDistoryLevel = coalExistence.coalExistenceEntity.CoalDistoryLevel;
            ceEntity.IsTowardsChange = coalExistence.coalExistenceEntity.IsTowardsChange;
            ceEntity.IsCoalMerge = coalExistence.coalExistenceEntity.IsCoalMerge;
            ceEntity.IsCoalSoft = coalExistence.coalExistenceEntity.IsCoalSoft;


            bool bResult = false;
            //if (this.Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
            //{
            //    //bResult = CoalExistenceBLL.insertCoalExistence(ceEntity);

            //    Log.Debug("发送添加煤层赋存信息的Socket信息");
            //    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
            //        CoalExistenceDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
            //    this.MainForm.SendMsg2Server(msg);
            //    Log.Debug("发送添加煤层赋存信息的Socket信息完成");
            //}
            //else if (this.Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
            //{
            //    bResult = CoalExistenceBLL.updateCoalExistence(ceEntity);
            //    Log.Debug("发送修改煤层赋存信息的Socket信息");
            //    UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
            //         CoalExistenceDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
            //    this.MainForm.SendMsg2Server(msg);
            //    Log.Debug("发送修改煤层赋存信息的Socket信息完成");
            //}
            try
            {
                ceEntity.SaveAndFlush();
                return true;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 提交瓦斯特有信息
        /// </summary>
        private bool submitG()
        {
            gdEntity = mineDataEntity.ChangeToGasDataEntity();
            gdEntity.PowerFailure = gasData.gasDataEntity.PowerFailure;
            gdEntity.DrillTimes = gasData.gasDataEntity.DrillTimes;
            gdEntity.GasTimes = gasData.gasDataEntity.GasTimes;
            gdEntity.TempDownTimes = gasData.gasDataEntity.TempDownTimes;
            gdEntity.CoalBangTimes = gasData.gasDataEntity.CoalBangTimes;
            gdEntity.CraterTimes = gasData.gasDataEntity.CraterTimes;
            gdEntity.StoperTimes = gasData.gasDataEntity.StoperTimes;
            //瓦斯浓度
            gdEntity.GasThickness = gasData.gasDataEntity.GasThickness;
            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                bResult = GasDataBLL.insertGasDataInfo(gdEntity);
                Log.Debug("发送添加瓦斯信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    GasDataDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送添加瓦斯信息的Socket信息完成");
            }
            else if (this.Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
            {
                Log.Debug("发送修改瓦斯信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                     GasDataDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                bResult = GasDataBLL.updateGasDataInfo(gdEntity);
                Log.Debug("发送修改瓦斯信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        /// 提交日常预测特有信息
        /// </summary>
        private bool submitU()
        {
            ufEntity = mineDataEntity.changeToUsualForecastEntity();
            ufEntity.IsRoofDown = usualForecast.isRoofDown;
            ufEntity.IsSupportBroken = usualForecast.isSupportBroken;
            ufEntity.IsCoalWallDrop = usualForecast.isCoalWallDrop;
            ufEntity.IsPartRoolFall = usualForecast.isPartRoolFall;
            ufEntity.IsBigRoofFall = usualForecast.isBigRoofFall;
            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast).panelFormName)
            {
                bResult = UsualForecastBLL.insertUsualForecastInfo(ufEntity);

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    UsualForecastDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
            }
            else if (this.Text == new LibPanels(MineDataPanelName.UsualForecast_Change).panelFormName)
            {
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    UsualForecastDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                bResult = UsualForecastBLL.updateUsualForecastInfo(ufEntity);
            }
            return bResult;
        }

        /// <summary>
        /// 提交管理特有信息
        /// </summary>
        private bool submitM()
        {
            mEntity = mineDataEntity.changeToManagementEntity();
            mEntity.IsGasErrorNotReport = management.managementEntity.IsGasErrorNotReport;
            mEntity.IsWFNotReport = management.managementEntity.IsWFNotReport;
            mEntity.IsStrgasNotDoWell = management.managementEntity.IsStrgasNotDoWell;
            mEntity.IsRwmanagementNotDoWell = management.managementEntity.IsRwmanagementNotDoWell;
            mEntity.IsVFBrokenByPeople = management.managementEntity.IsVFBrokenByPeople;
            mEntity.IsElementPlaceNotGood = management.managementEntity.IsElementPlaceNotGood;
            mEntity.IsReporterFalseData = management.managementEntity.IsReporterFalseData;
            mEntity.IsDrillWrongBuild = management.managementEntity.IsDrillWrongBuild;
            mEntity.IsDrillNotDoWell = management.managementEntity.IsDrillNotDoWell;
            mEntity.IsOPNotDoWell = management.managementEntity.IsOPNotDoWell;
            mEntity.IsOPErrorNotReport = management.managementEntity.IsOPErrorNotReport;
            mEntity.IsPartWindSwitchError = management.managementEntity.IsPartWindSwitchError;
            mEntity.IsSafeCtrlUninstall = management.managementEntity.IsSafeCtrlUninstall;
            mEntity.IsCtrlStop = management.managementEntity.IsCtrlStop;
            mEntity.IsGasNotDowWell = management.managementEntity.IsGasNotDowWell;
            mEntity.IsMineNoChecker = management.managementEntity.IsMineNoChecker;
            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {

                bResult = ManagementBLL.insertManagementInfo(mEntity);
                Log.Debug("发送添加管理信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    ManagementDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送添加管理信息的Socket信息完成");
            }
            else if (this.Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
            {
                bResult = ManagementBLL.updateManagementInfo(mEntity);
                Log.Debug("发送修改管理信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    ManagementDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, mEntity.Datetime);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送修改管理信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        /// 提交地质构造特有信息
        /// </summary>
        /// <returns></returns>
        private bool submitGeologicStructure()
        {
            geologicStructureEntity = mineDataEntity.changeToGeologicStructureEntity();
            geologicStructureEntity.NoPlanStructure = geologicStructure.geoligicStructureEntity.NoPlanStructure;
            geologicStructureEntity.PassedStructureRuleInvalid = geologicStructure.geoligicStructureEntity.PassedStructureRuleInvalid;
            geologicStructureEntity.YellowRuleInvalid = geologicStructure.geoligicStructureEntity.YellowRuleInvalid;
            geologicStructureEntity.RoofBroken = geologicStructure.geoligicStructureEntity.RoofBroken;
            geologicStructureEntity.CoalSeamSoft = geologicStructure.geoligicStructureEntity.CoalSeamSoft;
            geologicStructureEntity.CoalSeamBranch = geologicStructure.geoligicStructureEntity.CoalSeamBranch;
            geologicStructureEntity.RoofChange = geologicStructure.geoligicStructureEntity.RoofChange;
            geologicStructureEntity.GangueDisappear = geologicStructure.geoligicStructureEntity.GangueDisappear;
            geologicStructureEntity.GangueLocationChange = geologicStructure.geoligicStructureEntity.GangueLocationChange;
            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                bResult = GeologicStructureBLL.insertGeologicStructure(geologicStructureEntity);
                Log.Debug("发送添加地址构造信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    GeologicStructureDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送添加地址构造信息的Socket信息完成");
            }
            else if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
            {
                bResult = GeologicStructureBLL.updateGeologicStructure(geologicStructureEntity);
                Log.Debug("发送修改地址构造信息的Socket信息");
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, this.selectTunnelSimple1.ITunnelId,
                    GeologicStructureDbConstNames.TABLE_NAME, OPERATION_TYPE.UPDATE, dtpDateTime.Value);
                this.MainForm.SendMsg2Server(msg);
                Log.Debug("发送修改地址构造信息的Socket信息完成");
            }
            return bResult;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            // 判断巷道信息是否选择
            //矿井名称
            if (this.selectTunnelSimple1.ITunnelId == -1)
            {
                Alert.alert("请选择巷道信息");
                return false;
            }
            //坐标X是否为数字
            if (txtCoordinateX.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateX, "坐标X"))
                {
                    return false;
                }
            }
            //坐标Y是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateY, "坐标Y"))
                {
                    return false;
                }
            }
            //坐标Z是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateY, "坐标Y"))
                {
                    return false;
                }
            }
            //煤层赋存特有检查
            if (coalExistence.WindowState != FormWindowState.Minimized)
            {
                if (!coalExistence.check())
                {
                    return false;
                }
            }
            //瓦斯数据特有检查
            if (gasData.WindowState != FormWindowState.Minimized)
            {
                if (!gasData.check())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 工作制式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn38.Checked)
            {

                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }
            else
            {
                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }

            // 设置班次名称
            setWorkTimeName();
        }

        private void MineDataSimple_Load(object sender, EventArgs e)
        {
            //窗体绑定到Panel中
            ventilationInfoEntering.MdiParent = this;
            ventilationInfoEntering.Parent = panel2;
            coalExistence.MdiParent = this;
            coalExistence.Parent = panel2;
            gasData.MdiParent = this;
            gasData.Parent = panel2;
            usualForecast.MdiParent = this;
            usualForecast.Parent = panel2;
            management.MdiParent = this;
            management.Parent = panel2;
            geologicStructure.MdiParent = this;
            geologicStructure.Parent = panel2;

            //panel2绑定窗体
            panel2.Controls.Add(coalExistence);
            panel2.Controls.Add(ventilationInfoEntering);
            panel2.Controls.Add(gasData);
            panel2.Controls.Add(usualForecast);
            panel2.Controls.Add(management);
            panel2.Controls.Add(geologicStructure);

            if (this.Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                viEntity = (VentilationInfoEntity)obj;
            }
            if (this.Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
            {
                ceEntity = (CoalExistenceEntity)obj;
            }
            if (this.Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
            {
                gdEntity = (GasDataEntity)obj;
            }
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast_Change).panelFormName)
            {
                ufEntity = (UsualForecastEntity)obj;
            }
            if (this.Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
            {
                mEntity = (ManagementEntity)obj;
            }
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
            {
                geologicStructureEntity = (GeologicStructureEntity)obj;
            }

            //所有小窗体最小化
            AllMin();
            //通风
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
            {
                this.Height = formHeight + ventilationInfoEntering.Height;
                ventilationInfoEntering.WindowState = FormWindowState.Maximized;
                ventilationInfoEntering.Show();
                ventilationInfoEntering.Activate();
            }
            //煤层赋存
            if (this.Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
            {
                this.Height = formHeight + coalExistence.Height;
                coalExistence.WindowState = FormWindowState.Maximized;
                coalExistence.Show();
                coalExistence.Activate();
            }
            //瓦斯
            if (this.Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                this.Height = formHeight + gasData.Height;
                gasData.WindowState = FormWindowState.Maximized;
                gasData.Show();
                gasData.Activate();
            }
            //日常预测
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast).panelFormName)
            {
                this.Height = formHeight + usualForecast.Height;
                usualForecast.WindowState = FormWindowState.Maximized;
                usualForecast.Show();
                usualForecast.Activate();
            }
            //管理
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                this.Height = formHeight + management.Height;
                management.WindowState = FormWindowState.Maximized;
                management.Show();
                management.Activate();
            }
            //地质构造
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                this.Height = formHeight + geologicStructure.Height;
                geologicStructure.WindowState = FormWindowState.Maximized;
                geologicStructure.Show();
                geologicStructure.Activate();
            }

            //绑定通风修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                this.Height = formHeight + ventilationInfoEntering.Height;
                changeMineCommonValue(viEntity);

                ventilationInfoEntering.ventilationInfoEntity = viEntity;

                ventilationInfoEntering.bindDefaultValue(viEntity);

                ventilationInfoEntering.WindowState = FormWindowState.Maximized;
                ventilationInfoEntering.Show();
                ventilationInfoEntering.Activate();
            }

            //绑定煤层赋存修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName)
            {
                this.Height = formHeight + coalExistence.Height;
                changeMineCommonValue(ceEntity);

                coalExistence.coalExistenceEntity = ceEntity;

                coalExistence.bindDefaultValue(ceEntity);

                coalExistence.WindowState = FormWindowState.Maximized;
                coalExistence.Show();
                coalExistence.Activate();
            }

            //绑定瓦斯修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
            {
                this.Height = formHeight + gasData.Height;
                changeMineCommonValue(gdEntity);

                gasData.gasDataEntity = gdEntity;
                gasData.bindDefaultValue(gdEntity);

                gasData.WindowState = FormWindowState.Maximized;
                gasData.Show();
                gasData.Activate();
            }
            //绑定日常预测修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast_Change).panelFormName)
            {
                this.Height = formHeight + usualForecast.Height;
                changeMineCommonValue(ufEntity);

                usualForecast.isRoofDown = ufEntity.IsRoofDown;
                usualForecast.isSupportBroken = ufEntity.IsSupportBroken;
                usualForecast.isCoalWallDrop = ufEntity.IsCoalWallDrop;
                usualForecast.isPartRoolFall = ufEntity.IsPartRoolFall;
                usualForecast.isBigRoofFall = ufEntity.IsBigRoofFall;
                usualForecast.bindDefaultValue();

                usualForecast.WindowState = FormWindowState.Maximized;
                usualForecast.Show();
                usualForecast.Activate();
            }
            //绑定管理修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
            {
                this.Height = formHeight + management.Height;
                changeMineCommonValue(mEntity);

                management.managementEntity = mEntity;

                management.bindDefaultValue(mEntity);

                management.WindowState = FormWindowState.Maximized;
                management.Show();
                management.Activate();
            }
            //绑定地质构造修改初始数据
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
            {
                this.Height = formHeight + management.Height;
                changeMineCommonValue(geologicStructureEntity);

                geologicStructure.geoligicStructureEntity = geologicStructureEntity;
                geologicStructure.bindDefaultValue(geologicStructureEntity);

                geologicStructure.WindowState = FormWindowState.Maximized;
                geologicStructure.Show();
                geologicStructure.Activate();
            }
        }

        /// <summary>
        /// 绑定井下数据通用信息
        /// </summary>
        /// <param name="obj"></param>
        private void changeMineCommonValue(object obj)
        {
            mineDataEntity = (MineDataEntity)obj;
            txtCoordinateX.Text = mineDataEntity.CoordinateX.ToString();
            txtCoordinateY.Text = mineDataEntity.CoordinateY.ToString();
            txtCoordinateZ.Text = mineDataEntity.CoordinateZ.ToString();

            if (mineDataEntity.WorkStyle == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            if (mineDataEntity.WorkStyle == Const_MS.WORK_TIME_46)
            {
                rbtn46.Checked = true;
            }
            cboWorkTime.Text = mineDataEntity.WorkTime;
            cboTeamName.Text = mineDataEntity.TeamName;
            cboSubmitter.Text = mineDataEntity.Submitter;
            dtpDateTime.Value = mineDataEntity.Datetime;
        }

        /// <summary>
        /// 所有的窗体都最小化
        /// </summary>
        private void AllMin()
        {
            coalExistence.WindowState = FormWindowState.Minimized;
            ventilationInfoEntering.WindowState = FormWindowState.Minimized;
            gasData.WindowState = FormWindowState.Minimized;
            usualForecast.WindowState = FormWindowState.Minimized;
            management.WindowState = FormWindowState.Minimized;
            geologicStructure.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 队别选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindTeamMember();
        }

        private void MineDataSimple_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
