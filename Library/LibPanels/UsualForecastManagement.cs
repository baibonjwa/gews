// ******************************************************************
// 概  述：井下数据日常预测信息管理
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
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class UsualForecastManagement : Form
    {
        public UsualForecastManagement()
        {
            InitializeComponent();

            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;
        }
        //***********************************


        private int _iRecordCount = 0;
        int rowsCount = 0;      //数据行数
        int checkCount = 0;     //选择行数
        DataSet dsAll = new DataSet();
        public static LibEntity.MineData mdEntity = new LibEntity.MineData();
        public static LibEntity.UsualForecast ufEntity = new LibEntity.UsualForecast();
        public static Tunnel te = new Tunnel();

        //***********************************

        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            this.fpGasData.Sheets[0].Rows.Default.Resizable = false;
            this.fpGasData.Sheets[0].Columns[0].Resizable = false;
            this.bindFpGasData();
        }

        private void FrmParent_EventHandler(object sender)
        {
            bindFpGasData();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpGasData()
        {
            clearFp();
            // ※分页必须
            _iRecordCount = UsualForecastBLL.selectUsualForecast().Tables[0].Rows.Count;

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();
            DataSet ds = UsualForecastBLL.selectUsualForecast(iStartIndex, iEndIndex);
            dsAll = ds;
            int searchCount = ds.Tables[0].Rows.Count;
            rowsCount = searchCount;

            if (searchCount > 0)
            {
                int rowDetailStartIndex = 4;
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                ckbxcell.ThreeState = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //巷道名称
                    //this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(Convert.ToInt32(ds.Tables[0].Rows[i]["TUNNEL_ID"])).TunnelName;
                    //坐标X
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.X].ToString();
                    //坐标Y
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.Y].ToString();
                    //坐标Z
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.Z].ToString();
                    //时间
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.DATETIME].ToString();
                    //是否有顶板下沉
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.IS_ROOF_DOWN].ToString();
                    //是否支架变形与折损
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.IS_SUPPORT_BROKEN].ToString();
                    //是否煤壁片帮
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.IS_COAL_WALL_DROP].ToString();
                    //是否局部冒顶
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.IS_PART_ROOF_FALL].ToString();
                    //是否顶板沿工作面煤壁切落（大冒顶）
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.IS_BIG_ROOF_FALL].ToString();
                    //工作制式
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.WORK_STYLE].ToString();
                    //班次
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.WORK_TIME].ToString();
                    //填报人
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = ds.Tables[0].Rows[i][UsualForecastDbConstNames.SUBMITTER].ToString();
                }
            }
            else
            {
                Alert.alert("无数据");
            }
            setButtenEnable();

        }
        /// <summary>
        /// 清空farpoint
        /// </summary>
        private void clearFp()
        {
            int searchCount = rowsCount;
            if (searchCount > 0)
            {
                int rowDetailStartIndex = 4;

                for (int i = 0; i < rowsCount; i++)
                {
                    int index = 0;
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, index].Text = "";

                    //巷道名称
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //坐标X
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //坐标Y
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //坐标Z
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //时间
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否有顶板下沉
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否支架变形与折损
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否煤壁片帮
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否局部冒顶
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否顶板沿工作面煤壁切落（大冒顶）
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否煤壁片帮
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否局部冒顶
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                    //是否顶板沿工作面煤壁切落（大冒顶）
                    this.fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, ++index].Text = "";
                }
                checkCount = 0;
            }
            chkSelAll.Checked = false;
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpGasEmissionData_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                if (fpChk.Checked)
                {
                    checkCount++;
                }
                else
                {
                    checkCount--;
                }
            }
            if (checkCount == rowsCount)
            {
                chkSelAll.Checked = true;
            }
            else
            {
                chkSelAll.Checked = false;
            }
            setButtenEnable();
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            if (checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }
            if (checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

        }

        private void setMineDataEntityValue()
        {
            int searchCount = rowsCount;
            int rowDetailStartIndex = 4;
            for (int i = 0; i < rowsCount; i++)
            {
                if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text == "True")
                {
                    te.TunnelId = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.TUNNEL_ID]);
                    //te = TunnelInfoBLL.selectTunnelInfoByTunnelID(te.Tunnel);

                    ufEntity.Id = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.ID]);
                    ufEntity.Tunnel.TunnelId = te.TunnelId;
                    mdEntity.CoordinateX = Convert.ToDouble(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.X]);
                    ufEntity.CoordinateY = Convert.ToDouble(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.Y]);
                    ufEntity.CoordinateZ = Convert.ToDouble(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.Z]);
                    ufEntity.WorkStyle = dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.WORK_STYLE].ToString();
                    ufEntity.WorkTime = dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.WORK_TIME].ToString();
                    ufEntity.TeamName = dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.TEAM_NAME].ToString();
                    ufEntity.Submitter = dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.SUBMITTER].ToString();
                    ufEntity.Datetime = Convert.ToDateTime(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.DATETIME]);
                    ufEntity.IsRoofDown = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.IS_ROOF_DOWN]);
                    ufEntity.IsSupportBroken = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.IS_SUPPORT_BROKEN]);
                    ufEntity.IsCoalWallDrop = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.IS_COAL_WALL_DROP]);
                    ufEntity.IsPartRoolFall = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.IS_PART_ROOF_FALL]);
                    ufEntity.IsBigRoofFall = Convert.ToInt32(dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.IS_BIG_ROOF_FALL]);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            int searchCount = rowsCount;
            if (searchCount > 0)
            {
                int rowDetailStartIndex = 4;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value == null)
                    {
                        fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = !((CheckBox)sender).Checked;
                    }
                    string tf = fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value.ToString();
                    if (chkSelAll.Checked)
                    {
                        if (tf == "False")
                        {
                            fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                            checkCount++;
                        }
                    }
                    //else
                    //{
                    //    if (checkCount == rowsCount)
                    //    {
                    //        if (tf == "True")
                    //        {
                    //            fpDayReportJJ.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text = "false";
                    //            checkCount--;
                    //        }
                    //    }
                    //}
                }
                if (!chkSelAll.Checked)
                {
                    if (checkCount == rowsCount)
                    {
                        for (int i = 0; i < rowsCount; i++)
                        {
                            string tf = fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Value.ToString();
                            if (tf == "True")
                            {
                                fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text = "false";
                                checkCount--;
                            }
                        }

                    }
                }
            }
            setButtenEnable();
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            MineData m = new MineData();
            m.Text = new LibPanels(MineDataPanelName.UsualForecast).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasData();
                this.dataPager1.btnLastPage_Click(sender, e);
            }
            else
            {
                bindFpGasData();
            }
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            setMineDataEntityValue();
            MineData m = new MineData(ufEntity);

            m.Text = new LibPanels(MineDataPanelName.UsualForecast_Change).panelFormName;
            if (DialogResult.OK == m.ShowDialog())
            {
                bindFpGasData();
            }
        }

        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(@"确定要删除所选内容吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                int searchCount = rowsCount;
                int rowDetailStartIndex = 4;
                bool bResult = false;
                int delCount = 0;
                for (int i = 0; i < rowsCount; i++)
                {
                    if (fpGasData.Sheets[0].Cells[rowDetailStartIndex + i, 0].Text == "True")
                    {
                        int id = (int)dsAll.Tables[0].Rows[i][UsualForecastDbConstNames.ID];
                        bResult = UsualForecastBLL.deleteUsualForecastInfo(id);
                        delCount++;
                    }
                }
                if (bResult)
                {
                    bindFpGasData();
                }
                else
                {
                    Alert.alert("删除失败");
                }
            }
            else
            {
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpGasData, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }
    }
}
