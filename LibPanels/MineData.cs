// ******************************************************************
// 概  述：井下数据通用信息框架
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
using LibSocket;
using LibCommonControl;

namespace LibPanels
{
    public partial class MineData
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
        object obj = null; int formHeight = 410;

        //客户端
        public static ClientSocket _clientSocket = null;
        //*************************

        /// <summary>
        /// 构造方法
        /// </summary>
        public MineData(MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            InitializeComponent();

            this.selectTunnelUserControl1.init(mainFrm);
            selectTunnelUserControl1.loadMineName();

            addInfo();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="obj"></param>
        public MineData(object obj, MainFrm mainFrm)
        {
            this.MainForm = mainFrm;
            this.obj = obj;
            tunnelEntity.TunnelID = ((MineDataEntity)obj).Tunnel.TunnelID;
            //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            if (tunnelEntity == null)
            {
                InitializeComponent();
                selectTunnelUserControl1.loadMineName();
            }
            else
            {
                setArrValue(tunnelEntity.TunnelID);
                InitializeComponent();
                this.selectTunnelUserControl1.setCurSelectedID(arr);
            }
            addInfo();
        }

        /// <summary>
        /// 设置自定义控件用数组
        /// </summary>
        /// <param name="tunnelID"></param>
        private void setArrValue(int tunnelID)
        {
            //tunnelEntity.TunnelID = tunnelID;
            tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelID);
            //tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            arr[0] = tunnelEntity.WorkingFace.MiningArea.Horizontal.Mine.MineId;
            arr[1] = tunnelEntity.WorkingFace.MiningArea.Horizontal.HorizontalId;//HorizontalID;
            arr[2] = tunnelEntity.WorkingFace.MiningArea.MiningAreaId;//MiningAreaID;
            arr[3] = tunnelEntity.WorkingFace.WorkingFaceID;//WorkingFaceID;
            arr[4] = tunnelEntity.TunnelID;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineData_Load(object sender, EventArgs e)
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
                panel2.Height = ventilationInfoEntering.Height; ;
                ventilationInfoEntering.WindowState = FormWindowState.Maximized;
                ventilationInfoEntering.Show();
                ventilationInfoEntering.Activate();
            }

            //煤层赋存
            if (this.Text == new LibPanels(MineDataPanelName.CoalExistence).panelFormName)
            {
                this.Height = formHeight + coalExistence.Height;
                panel2.Height = coalExistence.Height; ;
                coalExistence.WindowState = FormWindowState.Maximized;
                coalExistence.Show();
                coalExistence.Activate();
            }

            //瓦斯
            if (this.Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                this.Height = formHeight + gasData.Height;
                panel2.Height = gasData.Height;
                gasData.WindowState = FormWindowState.Maximized;
                gasData.Show();
                gasData.Activate();
            }

            //日常预测
            if (this.Text == new LibPanels(MineDataPanelName.UsualForecast).panelFormName)
            {
                this.Height = formHeight + usualForecast.Height;
                panel2.Height = usualForecast.Height;
                usualForecast.WindowState = FormWindowState.Maximized;
                usualForecast.Show();
                usualForecast.Activate();
            }

            //管理
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                this.Height = formHeight + management.Height;
                panel2.Height = management.Height;
                management.WindowState = FormWindowState.Maximized;
                management.Show();
                management.Activate();
            }

            //地质构造
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                this.Height = formHeight + geologicStructure.Height;
                panel2.Height = geologicStructure.Height;
                geologicStructure.WindowState = FormWindowState.Maximized;
                geologicStructure.Show();
                geologicStructure.Activate();
            }

            //绑定通风修改初始信息
            if (this.Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                this.Height = formHeight + ventilationInfoEntering.Height;
                panel2.Height = ventilationInfoEntering.Height;
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
                panel2.Height = coalExistence.Height;
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
                panel2.Height = gasData.Height;
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
                panel2.Height = usualForecast.Height;
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
                panel2.Height = management.Height;
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
                this.Height = formHeight + geologicStructure.Height;
                panel2.Height = geologicStructure.Height;
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
            cboWorkTime.Text = workTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
        }

        private void changeInfo()
        {
            addInfo();
        }

        /// <summary>
        /// 返回班次名
        /// </summary>
        /// <param name="workStyle">工作制式名</param>
        /// <returns>班次名</returns>
        public static string workTime(string workStyle)
        {
            DataSet ds = WorkTimeBLL.returnWorkTime(workStyle);
            int hour = DateTime.Now.Hour;
            string workTime = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (hour > Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_FROM].ToString().Remove(2)) && hour <= Convert.ToInt32(ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_TO].ToString().Remove(2)))
                {
                    workTime = ds.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString();
                }
            }
            return workTime;
        }

        /// <summary>
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            //cboTeamName.Items.Clear();
            //DataSet ds = TeamBLL.selectTeamInfo();
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    cboTeamName.Items.Add(ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_NAME].ToString());
            //}
            cboTeamName.DataSource = null;

            DataSet ds = TeamBLL.selectTeamInfo();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboTeamName.DataSource = ds.Tables[0];
                cboTeamName.DisplayMember = TeamDbConstNames.TEAM_NAME;
                cboTeamName.ValueMember = TeamDbConstNames.ID;
                cboTeamName.SelectedIndex = -1;
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

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //巷道ID
            mineDataEntity.Tunnel.TunnelID = selectTunnelUserControl1.ITunnelId;
            //坐标X
            if (txtCoordinateX.Text == "")
            {
                mineDataEntity.CoordinateX = 0;
            }
            else
            {
                mineDataEntity.CoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            }
            //坐标Y
            if (txtCoordinateY.Text == "")
            {
                mineDataEntity.CoordinateY = 0;
            }
            else
            {
                mineDataEntity.CoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            }
            //坐标Z
            if (txtCoordinateZ.Text == "")
            {
                mineDataEntity.CoordinateZ = 0;
            }
            else
            {
                mineDataEntity.CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            }
            //提交日期
            mineDataEntity.Datetime = dtpDateTime.Value;
            //38制
            if (rbtn38.Checked)
            {
                mineDataEntity.WorkStyle = Const_MS.WORK_TIME_38;
            }
            //46制
            else
            {
                mineDataEntity.WorkStyle = Const_MS.WORK_TIME_46;
            }
            //班次
            mineDataEntity.WorkTime = cboWorkTime.Text;
            //队别
            mineDataEntity.TeamName = cboTeamName.Text;
            //填报人
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
            if (geologicStructure.WindowState != FormWindowState.Minimized)     //提交地质构造特有信息
            {
                bResult = submitGeologicStructure();
            }
            //关闭窗体
            if (bResult)
            {
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
            }
            else if (this.Text == new LibPanels(MineDataPanelName.Ventilation_Change).panelFormName)
            {
                bResult = VentilationBLL.updateVentilationInfo(viEntity);
            }
            return bResult;
        }

        /// <summary>
        /// 提交煤层赋存特有信息
        /// </summary>
        private bool submitC()
        {
            //共通实体转化为煤层赋存实体
            ceEntity = mineDataEntity.changeToCoalExistenceEntity();
            //是否层理紊乱
            ceEntity.IsLevelDisorder = coalExistence.coalExistenceEntity.IsLevelDisorder;
            //煤厚变化
            ceEntity.CoalThickChange = coalExistence.coalExistenceEntity.CoalThickChange;
            //软分层（构造煤）厚度
            ceEntity.TectonicCoalThick = coalExistence.coalExistenceEntity.TectonicCoalThick;
            //软分层（构造煤）层位是否发生变化
            ceEntity.IsLevelChange = coalExistence.coalExistenceEntity.IsLevelChange;
            //煤体破坏类型
            ceEntity.CoalDistoryLevel = coalExistence.coalExistenceEntity.CoalDistoryLevel;
            //是否煤层走向、倾角突然急剧变化
            ceEntity.IsTowardsChange = coalExistence.coalExistenceEntity.IsTowardsChange;
            //工作面煤层是否处于分叉、合层状态
            ceEntity.IsCoalMerge = coalExistence.coalExistenceEntity.IsCoalMerge;
            //煤层是否松软
            ceEntity.IsCoalSoft = coalExistence.coalExistenceEntity.IsCoalSoft;
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
            //共通实体转化为瓦斯实体
            gdEntity = mineDataEntity.ChangeToGasDataEntity();
            //瓦斯探头断电次数
            gdEntity.PowerFailure = gasData.gasDataEntity.PowerFailure;
            //吸钻预兆次数
            gdEntity.DrillTimes = gasData.gasDataEntity.DrillTimes;
            //瓦斯忽大忽小预兆次数
            gdEntity.GasTimes = gasData.gasDataEntity.GasTimes;
            //气温下降预兆次数
            gdEntity.TempDownTimes = gasData.gasDataEntity.TempDownTimes;
            //煤炮频繁预兆次数
            gdEntity.CoalBangTimes = gasData.gasDataEntity.CoalBangTimes;
            //喷孔次数
            gdEntity.CraterTimes = gasData.gasDataEntity.CraterTimes;
            //顶钻次数
            gdEntity.StoperTimes = gasData.gasDataEntity.StoperTimes;
            //瓦斯浓度
            gdEntity.GasThickness = gasData.gasDataEntity.GasThickness;
            bool bResult = false;
            //添加
            if (this.Text == new LibPanels(MineDataPanelName.GasData).panelFormName)
            {
                bResult = GasDataBLL.insertGasDataInfo(gdEntity);
            }
            //修改
            else if (this.Text == new LibPanels(MineDataPanelName.GasData_Change).panelFormName)
            {
                bResult = GasDataBLL.updateGasDataInfo(gdEntity);
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
            }
            else if (this.Text == new LibPanels(MineDataPanelName.UsualForecast_Change).panelFormName)
            {
                bResult = UsualForecastBLL.updateUsualForecastInfo(ufEntity);
            }
            return bResult;
        }

        /// <summary>
        /// 提交管理特有信息
        /// </summary>
        private bool submitM()
        {
            //共通实体转化为管理实体
            mEntity = mineDataEntity.changeToManagementEntity();
            //是否存在瓦斯异常不汇报
            mEntity.IsGasErrorNotReport = management.managementEntity.IsGasErrorNotReport;
            //是否存在工作面出现地质构造不汇报
            mEntity.IsWFNotReport = management.managementEntity.IsWFNotReport;
            //是否存在强化瓦斯措施执行不到位
            mEntity.IsStrgasNotDoWell = management.managementEntity.IsStrgasNotDoWell;
            //是否存在进回风巷隅角、尾巷管理不到位
            mEntity.IsRwmanagementNotDoWell = management.managementEntity.IsRwmanagementNotDoWell;
            //是否存在通风设施人为损坏
            mEntity.IsVFBrokenByPeople = management.managementEntity.IsVFBrokenByPeople;
            //是否存在甲烷传感器位置不当、误差大、调校超过规定
            mEntity.IsElementPlaceNotGood = management.managementEntity.IsElementPlaceNotGood;
            //是否存在瓦检员空漏假检
            mEntity.IsReporterFalseData = management.managementEntity.IsReporterFalseData;
            //钻孔未按设计施工次数
            mEntity.IsDrillWrongBuild = management.managementEntity.IsDrillWrongBuild;
            //钻孔施工不到位次数
            mEntity.IsDrillNotDoWell = management.managementEntity.IsDrillNotDoWell;
            //防突措施执行不到位次数
            mEntity.IsOPNotDoWell = management.managementEntity.IsOPNotDoWell;
            //防突异常情况未汇报次数
            mEntity.IsOPErrorNotReport = management.managementEntity.IsOPErrorNotReport;
            //是否存在局部通风机单回路供电或不能正常切换
            mEntity.IsPartWindSwitchError = management.managementEntity.IsPartWindSwitchError;
            //是否存在安全监测监控系统未及时安装
            mEntity.IsSafeCtrlUninstall = management.managementEntity.IsSafeCtrlUninstall;
            //是否存在监测监控停运
            mEntity.IsCtrlStop = management.managementEntity.IsCtrlStop;
            //是否存在不执行瓦斯治理措施、破坏通风设施
            mEntity.IsGasNotDowWell = management.managementEntity.IsGasNotDowWell;
            //是否高、突矿井工作面无专职瓦斯检查员
            mEntity.IsMineNoChecker = management.managementEntity.IsMineNoChecker;
            bool bResult = false;
            //添加
            if (this.Text == new LibPanels(MineDataPanelName.Management).panelFormName)
            {
                bResult = ManagementBLL.insertManagementInfo(mEntity);
                UpdateWarningDataMsg msg = new UpdateWarningDataMsg(Const.INVALID_ID, selectTunnelUserControl1.ITunnelId,
                    ManagementDbConstNames.TABLE_NAME, OPERATION_TYPE.ADD, mEntity.Datetime);
                this.MainForm.SendMsg2Server(msg);
            }
            //修改
            else if (this.Text == new LibPanels(MineDataPanelName.Management_Change).panelFormName)
            {
                bResult = ManagementBLL.updateManagementInfo(mEntity);
            }
            return bResult;
        }

        /// <summary>
        /// 提交地质构造特有信息
        /// </summary>
        /// <returns></returns>
        private bool submitGeologicStructure()
        {
            //minedata转地质构造实体
            geologicStructureEntity = mineDataEntity.changeToGeologicStructureEntity();
            //无计划揭露构造
            geologicStructureEntity.NoPlanStructure = geologicStructure.geoligicStructureEntity.NoPlanStructure;
            //过构造时措施无效
            geologicStructureEntity.PassedStructureRuleInvalid = geologicStructure.geoligicStructureEntity.PassedStructureRuleInvalid;
            //黄色预警措施无效
            geologicStructureEntity.YellowRuleInvalid = geologicStructure.geoligicStructureEntity.YellowRuleInvalid;
            //顶板破碎
            geologicStructureEntity.RoofBroken = geologicStructure.geoligicStructureEntity.RoofBroken;
            //煤层松软
            geologicStructureEntity.CoalSeamSoft = geologicStructure.geoligicStructureEntity.CoalSeamSoft;
            //工作面煤层处于分叉、合层状态
            geologicStructureEntity.CoalSeamBranch = geologicStructure.geoligicStructureEntity.CoalSeamBranch;
            //顶板条件发生变化
            geologicStructureEntity.RoofChange = geologicStructure.geoligicStructureEntity.RoofChange;
            //工作面夹矸突然变薄或消失
            geologicStructureEntity.GangueDisappear = geologicStructure.geoligicStructureEntity.GangueDisappear;
            //夹矸位置急剧变化
            geologicStructureEntity.GangueLocationChange = geologicStructure.geoligicStructureEntity.GangueLocationChange;
            bool bResult = false;
            //添加
            if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure).panelFormName)
            {
                bResult = GeologicStructureBLL.insertGeologicStructure(geologicStructureEntity);
            }
            //修改
            else if (this.Text == new LibPanels(MineDataPanelName.GeologicStructure_Change).panelFormName)
            {
                bResult = GeologicStructureBLL.updateGeologicStructure(geologicStructureEntity);
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
            if (selectTunnelUserControl1.ITunnelId == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //坐标X是否为数字
            if (txtCoordinateX.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateX, Const_GE.COORDINATE_X))
                {
                    return false;
                }
            }
            //坐标Y是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateY, Const_GE.COORDINATE_Y))
                {
                    return false;
                }
            }
            //坐标Z是否为数字
            if (txtCoordinateY.Text != "")
            {
                if (!Check.IsNumeric(txtCoordinateY, Const_GE.COORDINATE_Z))
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
            //选择38制
            if (rbtn38.Checked)
            {

                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_38);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }
            //选择46制
            else
            {
                cboWorkTime.Text = "";
                cboWorkTime.Items.Clear();
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_46);
                for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
                {
                    cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
                }
            }
        }

        /// <summary>
        /// 队别选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTeamName.SelectedIndex > 0)
            {
                this.bindTeamMember();
            }
        }

    }
}
