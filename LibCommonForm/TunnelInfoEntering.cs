// ******************************************************************
// 概  述：巷道信息录入
// 作成者：宋英杰
// 作成日：2013/11/28
// 版本号：1.0
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
using LibCommon;
using LibEntity;
using LibCommonControl;

namespace LibCommonForm
{
    public partial class TunnelInfoEntering : BaseForm
    {
        /// <summary>
        /// 添加
        /// </summary>
        public TunnelInfoEntering(MainFrm frm)
        {
            this.MainForm = frm;

            InitializeComponent();
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            this.Text = Const_GM.TUNNEL_INFO_ADD;
            this.bindLithology();
            this.bindCoalLayer();

        }

        public TunnelInfoEntering(int[] array, MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            arr = array;
            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            //绑定围岩类型
            this.bindLithology();
            //绑定煤层
            this.bindCoalLayer();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="tunnelID"></param>
        public TunnelInfoEntering(int tunnelID, int[] array, MainFrm frm)
        {
            this.MainForm = frm;
            arr = array;
            tunnelEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelID);
            InitializeComponent();
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_CHANGE);
            this.Text = Const_GM.TUNNEL_INFO_CHANGE;
            this.bindLithology();
            this.bindCoalLayer();
            //bindInfo(tunnelID);

        }

        Tunnel tunnelEntity = new Tunnel();
        int workingFaceID = 0;
        int[] arr;
        int formHeight = 0;
        private void addTunnelInfo()
        {
            // 验证
            if (!this.check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            if (PanelForTunnelEntering.IWorkingFaceId != 0)
            {
                workingFaceID = PanelForTunnelEntering.IWorkingFaceId;
            }
            else
            {
                workingFaceID = arr[3];
            }
            //创建巷道实体
            Tunnel tunnelEntity = new Tunnel();

            //巷道名称
            tunnelEntity.TunnelName = this.txtTunnelName.Text;
            //支护方式
            tunnelEntity.TunnelSupportPattern = this.cboSupportPattern.Text;
            //围岩类型
            if (this.cboLithology.Text == "")
            {
                this.cboLithology.SelectedIndex = -1;
            }
            tunnelEntity.TunnelLithologyID = Convert.ToInt32(this.cboLithology.SelectedValue);
            //断面类型
            tunnelEntity.TunnelSectionType = this.cboFaultageType.Text;
            //断面参数
            string tunnelParam = "";
            if (this.txtParam1.Visible == true)
            {
                tunnelParam += this.txtParam1.Text + ",";
            }
            if (this.txtParam2.Visible == true)
            {
                tunnelParam += this.txtParam2.Text + ",";
            }
            if (this.txtParam3.Visible == true)
            {
                tunnelParam += this.txtParam3.Text + ",";
            }
            if (this.cbotxtParam3.Visible == true)
            {
                tunnelParam += this.cbotxtParam3.Text + ",";
            }
            if (this.txtParam4.Visible == true)
            {
                tunnelParam += this.txtParam4.Text + ",";
            }
            if (this.txtParam5.Visible == true)
            {
                tunnelParam += txtParam5.Text + ",";
            }
            if (tunnelParam != "")
            {
                tunnelEntity.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (this.txtDesignLength.Text != "")
            {
                tunnelEntity.TunnelDesignLength = Convert.ToInt32(this.txtDesignLength.Text);
            }
            //巷道类型
            tunnelEntity.TunnelType = TunnelTypeEnum.OTHER;
            //煤巷岩巷
            tunnelEntity.CoalOrStone = this.cboCoalOrStone.Text;
            //绑定煤层
            tunnelEntity.CoalLayerID = Convert.ToInt32(this.cboCoalLayer.SelectedValue);
            //通过煤层ID获取煤层名称方法

            tunnelEntity.WorkingFace = BasicInfoManager.getInstance().getWorkingFaceById(workingFaceID);

            tunnelEntity.BindingID = IDGenerator.NewBindingID();

            tunnelEntity.TunnelWid = 5;
            //巷道信息登录
            bool bResult = TunnelInfoBLL.insertTunnelInfo(tunnelEntity);
            if (bResult)
            {
                Alert.alert("提交成功！");
            }
            else
            {
                Alert.alert("提交失败！");
                return;
            }
        }

        private void updateTunnelInfo()
        {
            if (!check())
            {
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;
            tunnelEntity.WorkingFace.WorkingFaceID = PanelForTunnelEntering.IWorkingFaceId;
            //巷道名称
            tunnelEntity.TunnelName = this.txtTunnelName.Text;
            //支护方式
            tunnelEntity.TunnelSupportPattern = this.cboSupportPattern.Text;
            //围岩类型
            if (this.cboLithology.Text == "")
            {
                this.cboLithology.SelectedIndex = -1;
            }
            tunnelEntity.TunnelLithologyID = Convert.ToInt32(this.cboLithology.SelectedValue);

            //断面类型
            tunnelEntity.TunnelSectionType = this.cboFaultageType.Text;
            //断面参数
            string tunnelParam = "";
            if (this.txtParam1.Visible == true)
            {
                tunnelParam += this.txtParam1.Text + ",";
            }
            if (this.txtParam2.Visible == true)
            {
                tunnelParam += this.txtParam2.Text + ",";
            }
            if (this.txtParam3.Visible == true)
            {
                tunnelParam += this.txtParam3.Text + ",";
            }
            if (this.cbotxtParam3.Visible == true)
            {
                tunnelParam += this.cbotxtParam3.Text + ",";
            }
            if (this.txtParam4.Visible == true)
            {
                tunnelParam += this.txtParam4.Text + ",";
            }
            if (this.txtParam5.Visible == true)
            {
                tunnelParam += txtParam5.Text + ",";
            }
            if (tunnelParam.Length > 0)
            {
                tunnelEntity.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (this.txtDesignLength.Text != "")
            {
                tunnelEntity.TunnelDesignLength = Convert.ToInt32(this.txtDesignLength.Text);
            }
            //煤巷岩巷
            if (cboCoalOrStone.Text != "")
            {
                tunnelEntity.CoalOrStone = cboCoalOrStone.Text;
            }
            if (cboCoalLayer.Text != "")
            {
                tunnelEntity.CoalLayerID = Convert.ToInt32(this.cboCoalLayer.SelectedValue);
            }
            //巷道信息登录

            tunnelEntity.TunnelWid = 5;
            bool bResult = TunnelInfoBLL.updateTunnelInfo(tunnelEntity);
            if (bResult)
            {
                Alert.alert("提交成功！");
            }
            else
            {
                Alert.alert("提交失败！");
                return;
            }
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Text == Const_GM.TUNNEL_INFO_ADD)
            {
                addTunnelInfo();
            }
            if (this.Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                updateTunnelInfo();
            }

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            if (PanelForTunnelEntering.IWorkingFaceId == 0)
            {
                Alert.alert("请选择巷道所在工作面信息");
                return false;
            }
            // 判断巷道名称是否入力
            if (Validator.IsEmpty(this.txtTunnelName.Text))
            {
                this.txtTunnelName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("巷道名称不能为空！");
                this.txtTunnelName.Focus();
                return false;
            }
            else
            {
                this.txtTunnelName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //巷道名称是否包含特殊字符
            //if (!Check.checkSpecialCharacters(txtTunnelName,"巷道名称"))
            //{
            //    return false;
            //}
            //判断设计长度是否为数字
            if (this.txtDesignLength.Text != "")
            {
                if (!Check.IsNumeric(txtDesignLength, "设计长度"))
                {
                    return false;
                }
            }

            //验证通过
            return true;
        }

        private void cboFaultageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.showParam();
        }

        //改变窗体大小
        private void changeFormSize(Object gbx)
        {
            if (gbx == null)
            {
                this.Height = formHeight;
            }
            else if (((GroupBox)gbx).Visible == true)
            {
                this.Height = formHeight + ((GroupBox)gbx).Height;
            }

        }

        //控制显示框体
        private void showParam()
        {
            //初使化是否显示
            this.gbxSquare.Visible = false;
            this.gbxSemicircle.Visible = false;
            this.gbxLadderShape.Visible = false;
            this.gbxArc.Visible = false;
            this.gbxThreePoint.Visible = false;
            this.gbxOther.Visible = false;
            this.txtParam1.Visible = false;
            this.txtParam2.Visible = false;
            this.txtParam3.Visible = false;
            this.txtParam4.Visible = false;
            this.txtParam5.Visible = false;
            this.cbotxtParam3.Visible = false;
            if (this.cboFaultageType.Text == "矩形")
            {

                this.gbxSquare.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                changeFormSize(gbxSquare);
            }
            if (this.cboFaultageType.Text == "梯形")
            {

                this.gbxLadderShape.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                this.txtParam3.Visible = true;
                this.txtParam4.Visible = true;
                this.txtParam5.Visible = true;
                changeFormSize(gbxLadderShape);
            }
            if (this.cboFaultageType.Text == "半圆拱")
            {

                this.gbxSemicircle.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                changeFormSize(gbxSemicircle);

            }
            if (this.cboFaultageType.Text == "三心拱")
            {

                this.gbxThreePoint.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                this.cbotxtParam3.Visible = true;
                changeFormSize(gbxThreePoint);
            }
            if (this.cboFaultageType.Text == "圆形")
            {

                this.gbxArc.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                this.txtParam3.Visible = true;
                changeFormSize(gbxArc);
            }
            if (this.cboFaultageType.Text == "其他")
            {

                this.gbxOther.Visible = true;
                this.txtParam1.Visible = true;
                this.txtParam2.Visible = true;
                this.txtParam3.Visible = true;
                this.txtParam4.Visible = true;
                this.txtParam5.Visible = true;
                changeFormSize(gbxOther);
            }
        }

        private void cboFaultageType_TextChanged(object sender, EventArgs e)
        {
            if (this.cboFaultageType.Text == "")
            {
                this.changeFormSize(null);
                this.gbxSquare.Visible = false;
                this.gbxSemicircle.Visible = false;
                this.gbxLadderShape.Visible = false;
                this.gbxArc.Visible = false;
                this.gbxThreePoint.Visible = false;
                this.gbxOther.Visible = false;
                this.txtParam1.Visible = false;
                this.txtParam2.Visible = false;
                this.txtParam3.Visible = false;
                this.txtParam4.Visible = false;
                this.txtParam5.Visible = false;
                this.cbotxtParam3.Visible = false;
            }
        }
        /// <summary>
        /// 绑定围岩类型
        /// </summary>
        private void bindLithology()
        {
            DataSet ds = LithologyBLL.selectAllLithologyInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.cboLithology.Items.Add(ds.Tables[0].Rows[i]["LITHOLOGY_NAME"].ToString());
                }
                // 设置数据源
                cboLithology.DataSource = ds.Tables[0];
                // 设置显示字段
                this.cboLithology.DisplayMember = LithologyDbConstNames.LITHOLOGY_NAME;
                // 设置隐藏字段
                this.cboLithology.ValueMember = LithologyDbConstNames.LITHOLOGY_ID;

            }
            cboLithology.SelectedIndex = -1;

        }

        private void bindCoalLayer()
        {
            DataSet dsCoalLayer = CoalSeamsBLL.selectAllCoalSeamsInfo();
            this.cboCoalLayer.DataSource = dsCoalLayer.Tables[0];
            this.cboCoalLayer.DisplayMember = CoalSeamsDbConstNames.COAL_SEAMS_NAME;
            this.cboCoalLayer.ValueMember = CoalSeamsDbConstNames.COAL_SEAMS_ID;
            this.cboCoalLayer.SelectedIndex = -1;
        }

        private void setTunnelType()
        {
            cboCoalOrStone.Text = cboCoalOrStone.Items[0].ToString();
        }

        private void TunnelInfoEntering_Load(object sender, EventArgs e)
        {
            formHeight = this.Height;
            this.cboLithology.Text = "";
            changeFormSize(null);

            PanelForTunnelEntering panelForTunnelEnteringForm;
            if (this.Text == Const_GM.TUNNEL_INFO_ADD)
            {
                panelForTunnelEnteringForm = new PanelForTunnelEntering(this.MainForm);
            }
            else
            {
                panelForTunnelEnteringForm = new PanelForTunnelEntering(arr, this.MainForm);
            }
            panelForTunnelEnteringForm.MdiParent = this;
            this.panel2.Controls.Add(panelForTunnelEnteringForm);
            panelForTunnelEnteringForm.WindowState = FormWindowState.Maximized;
            panelForTunnelEnteringForm.Show();
            panelForTunnelEnteringForm.Activate();
            this.ActiveControl = txtTunnelName;
            txtTunnelName.Focus();

            if (cboFaultageType.Text != "")
            {
                showParam();
            }

            if (this.Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                bindInfo(tunnelEntity.TunnelId);
            }



        }

        private void bindInfo(int tunnelID)
        {
            Tunnel tEntity = BasicInfoManager.getInstance().getTunnelByID(tunnelID);
            tunnelEntity = tEntity;
            txtTunnelName.Text = tunnelEntity.TunnelName;
            cboSupportPattern.Text = tunnelEntity.TunnelSupportPattern;
            string lithology = "";
            if (tunnelEntity.TunnelLithologyID != 0)
            {
                lithology = LithologyBLL.selectLithologyInfoByLithologyId(tunnelEntity.TunnelLithologyID).Tables[0].Rows[0][LithologyDbConstNames.LITHOLOGY_NAME].ToString();
            }
            cboLithology.Text = lithology;
            cboFaultageType.Text = tunnelEntity.TunnelSectionType;

            txtDesignLength.Text = tunnelEntity.TunnelDesignLength.ToString();
            string[] param = new string[] { "", "", "", "", "" };
            if (tunnelEntity.TunnelParam != null)
            {
                string[] paramOld = tunnelEntity.TunnelParam.Split(',');
                for (int i = 0; i < paramOld.Length; i++)
                {
                    param[i] = paramOld[i];
                }
            }
            txtParam1.Text = param[0];
            txtParam2.Text = param[1];
            txtParam3.Text = param[2];
            txtParam4.Text = param[3];
            txtParam5.Text = param[4];

            cboCoalOrStone.Text = tunnelEntity.CoalOrStone;
            if (cboCoalOrStone.Text == Const_GM.COAL_TUNNEL)
            {
                cboCoalLayer.Visible = true;
                cboCoalLayer.Text = BasicInfoManager.getInstance().getCoalSeamNameById(tunnelEntity.CoalLayerID);
            }
            else
            {
                cboCoalLayer.Visible = false;
            }
        }

        private void cboLithology_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLithology.Text == "煤层")
            {
                this.cboCoalOrStone.Text = "煤巷";
            }
            else
            {
                this.cboCoalOrStone.Text = "";
            }
        }

        private void cboCoalOrStone_TextChanged(object sender, EventArgs e)
        {
            if (cboCoalOrStone.Text == "煤巷")
            {
                lblCoalLayer.Visible = true;
                cboCoalLayer.Visible = true;
            }
            else
            {
                cboCoalLayer.Text = "";
                lblCoalLayer.Visible = false;
                cboCoalLayer.Visible = false;
            }
        }
    }
}