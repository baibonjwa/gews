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
using LibSocket;

namespace UnderTerminal
{
    public partial class MineDataSimple : Form
    {
        //******定义变量***********
        UnderMessageWindow mainWin;
        int tunnelId = -1;
        VentilationInfoEntering ventilationInfoEntering = new VentilationInfoEntering();      //通风
        public CoalExistenceInfoEntering coalExistence = new CoalExistenceInfoEntering();                          //煤层赋存
        GasInfoEntering gasData = new GasInfoEntering();                          //瓦斯
        UsualForecast usualForecast = new UsualForecast();              //日常预测
        ManagementInfoEntering management = new ManagementInfoEntering();                    //管理
        Tunnel tunnelEntity = new Tunnel();                 //巷道信息实体
        Ventilation viEntity = new Ventilation();   //通风实体
        CoalExistence ceEntity = new CoalExistence();       //煤层赋存实体
        GasData gdEntity = new GasData();       //瓦斯实体
        LibEntity.UsualForecast ufEntity = new LibEntity.UsualForecast();       //日常预测实体
        Management mEntity = new Management();  //管理实体
        MineData mineDataEntity = new MineData();
        GeologicStructureInfoEntering geologicStructure = new GeologicStructureInfoEntering();
        GeologicStructure geologicStructureEntity = new GeologicStructure();
        int[] arr = new int[5];
        object obj = null;
        int formHeight = 247;

        //*************************
        public MineDataSimple(int tunnelId, string tunnelName, UnderMessageWindow win)
        {
            InitializeComponent();
            this.tunnelId = tunnelId;
            //this.Text += "-" + tunnelName;
            Tunnel entTunnel = Tunnel.Find(tunnelId);
            WorkingFace entWorkingFace = WorkingFace.Find(entTunnel.WorkingFace.WorkingFaceId);
            txtCoordinateX.Text = entWorkingFace.CoordinateX.ToString();
            txtCoordinateY.Text = entWorkingFace.CoordinateY.ToString();
            txtCoordinateZ.Text = entWorkingFace.CoordinateZ.ToString();
            this.mainWin = win;
            addInfo();
        }

        /// <summary>
        /// 添加时初始化
        /// </summary>
        private void addInfo()
        {
            this.bindTeamInfo();
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
            //if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            //{
            //    rbtn38.Checked = true;
            //}
            //else
            //{
            //    rbtn46.Checked = true;
            //}
            cboWorkTime.Text = DataBindUtil.JudgeWorkTimeNow(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);

            if (!String.IsNullOrEmpty(mainWin.DefaultWorkStyle))
            {
                if (mainWin.DefaultWorkStyle == Const_MS.WORK_TIME_38)
                {
                    rbtn38.Checked = true;
                }
                else
                {
                    rbtn46.Checked = true;
                }
            }

            if (!String.IsNullOrEmpty(mainWin.DefaultWorkTime))
            {
                cboWorkTime.SelectedItem = mainWin.DefaultWorkTime;
            }

            if (!String.IsNullOrEmpty(mainWin.DefaultTeamName))
            {
                cboTeamName.SelectedItem = mainWin.DefaultTeamName;
            }

            if (!String.IsNullOrEmpty(mainWin.DefaultSubmitter))
            {
                cboSubmitter.SelectedItem = mainWin.DefaultSubmitter;
            }

        }

        private void changeInfo()
        {
            addInfo();
        }

        /// <summary>
        /// 绑定队别名称
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (!mainWin.OnLine)
            //{
            //    MessageBox.Show("系统处于离线状态，请重新和服务器进行连接");
            //    return;
            //}

            DialogResult result = MessageBox.Show(@"确认提交", "井下终端录入系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }

            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            //通用信息
            mineDataEntity.Tunnel.TunnelId = this.tunnelId;
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
            viEntity.IsNoWindArea = ventilationInfoEntering.VentilationEntity.IsNoWindArea;
            //是否有微风区域
            viEntity.IsLightWindArea = ventilationInfoEntering.VentilationEntity.IsLightWindArea;
            //是否有风流反向区域
            viEntity.IsReturnWindArea = ventilationInfoEntering.VentilationEntity.IsReturnWindArea;
            //是否通风断面小于设计断面的2/3
            viEntity.IsSmall = ventilationInfoEntering.VentilationEntity.IsSmall;
            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
            viEntity.IsFollowRule = ventilationInfoEntering.VentilationEntity.IsFollowRule;

            viEntity.FaultageArea = ventilationInfoEntering.VentilationEntity.FaultageArea;

            viEntity.AirFlow = ventilationInfoEntering.VentilationEntity.AirFlow;

            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
            {
                viEntity.SaveAndFlush();
                bResult = true;

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                    Ventilation.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
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
            if (Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
            {
                ceEntity.SaveAndFlush();

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                   CoalExistence.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
                bResult = true;
            }

            return bResult;
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
                gdEntity.SaveAndFlush();
                bResult = true;

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                    GasData.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
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

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                    UsualForecastDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
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
            mEntity.IsWfNotReport = management.managementEntity.IsWfNotReport;
            mEntity.IsStrgasNotDoWell = management.managementEntity.IsStrgasNotDoWell;
            mEntity.IsRwmanagementNotDoWell = management.managementEntity.IsRwmanagementNotDoWell;
            mEntity.IsVfBrokenByPeople = management.managementEntity.IsVfBrokenByPeople;
            mEntity.IsElementPlaceNotGood = management.managementEntity.IsElementPlaceNotGood;
            mEntity.IsReporterFalseData = management.managementEntity.IsReporterFalseData;
            mEntity.IsDrillWrongBuild = management.managementEntity.IsDrillWrongBuild;
            mEntity.IsDrillNotDoWell = management.managementEntity.IsDrillNotDoWell;
            mEntity.IsOpNotDoWell = management.managementEntity.IsOpNotDoWell;
            mEntity.IsOpErrorNotReport = management.managementEntity.IsOpErrorNotReport;
            mEntity.IsPartWindSwitchError = management.managementEntity.IsPartWindSwitchError;
            mEntity.IsSafeCtrlUninstall = management.managementEntity.IsSafeCtrlUninstall;
            mEntity.IsCtrlStop = management.managementEntity.IsCtrlStop;
            mEntity.IsGasNotDowWell = management.managementEntity.IsGasNotDowWell;
            mEntity.IsMineNoChecker = management.managementEntity.IsMineNoChecker;
            bool bResult = false;
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                mEntity.SaveAndFlush();
                bResult = true;
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                    Management.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
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
                geologicStructureEntity.SaveAndFlush();
                bResult = true;

                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(this.mainWin.workingfaceId, this.tunnelId,
                  GeologicStructure.TableName, OPERATION_TYPE.ADD, dtpDateTime.Value);
                mainWin.SendMsg2Server(msg);
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
            if (this.tunnelId <= 0)
            {
                Alert.alert("请选择巷道");
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
            DataBindUtil.LoadWorkTime(cboWorkTime,
                rbtn38.Checked ? Const_MS.WORK_GROUP_ID_38 : Const_MS.WORK_GROUP_ID_46);
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

            //所有小窗体最小化
            AllMin();
            //通风
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation).panelFormName)
            {
                //this.Height = formHeight + ventilationInfoEntering.Height;
                ventilationInfoEntering.WindowState = FormWindowState.Maximized;
                ventilationInfoEntering.Show();
                ventilationInfoEntering.Activate();
            }
            //煤层赋存
            if (this.Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
            {
                //this.Height = formHeight + coalExistence.Height;
                coalExistence.WindowState = FormWindowState.Maximized;
                coalExistence.Show();
                coalExistence.Activate();
            }
            //瓦斯
            if (this.Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                //this.Height = formHeight + gasData.Height;
                gasData.WindowState = FormWindowState.Maximized;
                gasData.Show();
                gasData.Activate();
            }
            //日常预测
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast).panelFormName)
            {
                //this.Height = formHeight + usualForecast.Height;
                usualForecast.WindowState = FormWindowState.Maximized;
                usualForecast.Show();
                usualForecast.Activate();
            }
            //管理
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                //this.Height = formHeight + management.Height;
                management.WindowState = FormWindowState.Maximized;
                management.Show();
                management.Activate();
            }
            //地质构造
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                //this.Height = formHeight + geologicStructure.Height;
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
            mineDataEntity = (MineData)obj;
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
            DataBindUtil.LoadTeamMemberByTeamName(cboSubmitter, cboTeamName.Text);
        }

        private void MineDataSimple_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
