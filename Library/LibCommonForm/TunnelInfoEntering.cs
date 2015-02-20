// ******************************************************************
// 概  述：巷道信息录入
// 作成者：宋英杰
// 作成日：2013/11/28
// 版本号：1.0
// ******************************************************************

using System;
using System.Data;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibCommonForm
{
    public partial class TunnelInfoEntering : BaseForm
    {
        private readonly int[] arr;
        private int formHeight;
        private Tunnel tunnelEntity = new Tunnel();
        private int workingFaceID;

        /// <summary>
        ///     添加
        /// </summary>
        public TunnelInfoEntering()
        {

            InitializeComponent();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            Text = Const_GM.TUNNEL_INFO_ADD;
            DataBindUtil.LoadLithology(cboLithology);
            bindCoalLayer();
        }

        public TunnelInfoEntering(int[] array)
        {
            InitializeComponent();

            arr = array;
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            //绑定围岩类型
            DataBindUtil.LoadLithology(cboLithology);
            //绑定煤层
            bindCoalLayer();
        }

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name="tunnelID"></param>
        public TunnelInfoEntering(Tunnel tunnel)
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_CHANGE);
            Text = Const_GM.TUNNEL_INFO_CHANGE;
            DataBindUtil.LoadLithology(cboLithology);
            bindCoalLayer();
            //bindInfo(tunnelID);
        }

        private void addTunnelInfo()
        {
            // 验证
            if (!check())
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
            var tunnelEntity = new Tunnel();

            //巷道名称
            tunnelEntity.TunnelName = txtTunnelName.Text;
            //支护方式
            tunnelEntity.TunnelSupportPattern = cboSupportPattern.Text;
            //围岩类型
            if (cboLithology.Text == "")
            {
                cboLithology.SelectedIndex = -1;
            }
            tunnelEntity.Lithology.LithologyId = Convert.ToInt32(cboLithology.SelectedValue);
            //断面类型
            tunnelEntity.TunnelSectionType = cboFaultageType.Text;
            //断面参数
            string tunnelParam = "";
            if (txtParam1.Visible)
            {
                tunnelParam += txtParam1.Text + ",";
            }
            if (txtParam2.Visible)
            {
                tunnelParam += txtParam2.Text + ",";
            }
            if (txtParam3.Visible)
            {
                tunnelParam += txtParam3.Text + ",";
            }
            if (cbotxtParam3.Visible)
            {
                tunnelParam += cbotxtParam3.Text + ",";
            }
            if (txtParam4.Visible)
            {
                tunnelParam += txtParam4.Text + ",";
            }
            if (txtParam5.Visible)
            {
                tunnelParam += txtParam5.Text + ",";
            }
            if (tunnelParam != "")
            {
                tunnelEntity.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnelEntity.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            //巷道类型
            tunnelEntity.TunnelType = TunnelTypeEnum.OTHER;
            //煤巷岩巷
            tunnelEntity.CoalOrStone = cboCoalOrStone.Text;
            //绑定煤层
            tunnelEntity.CoalSeams = CoalSeams.Find(cboCoalLayer.SelectedValue);
            //通过煤层ID获取煤层名称方法

            tunnelEntity.WorkingFace = WorkingFace.Find(workingFaceID);

            tunnelEntity.BindingId = IDGenerator.NewBindingID();

            tunnelEntity.TunnelWid = 5;
            //巷道信息登录

            tunnelEntity.Save();
            Alert.alert("提交成功！");
        }

        private void updateTunnelInfo()
        {
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            tunnelEntity.WorkingFace.WorkingFaceId = PanelForTunnelEntering.IWorkingFaceId;
            //巷道名称
            tunnelEntity.TunnelName = txtTunnelName.Text;
            //支护方式
            tunnelEntity.TunnelSupportPattern = cboSupportPattern.Text;
            //围岩类型
            if (cboLithology.Text == "")
            {
                cboLithology.SelectedIndex = -1;
            }
            tunnelEntity.Lithology.LithologyId = Convert.ToInt32(cboLithology.SelectedValue);

            //断面类型
            tunnelEntity.TunnelSectionType = cboFaultageType.Text;
            //断面参数
            string tunnelParam = "";
            if (txtParam1.Visible)
            {
                tunnelParam += txtParam1.Text + ",";
            }
            if (txtParam2.Visible)
            {
                tunnelParam += txtParam2.Text + ",";
            }
            if (txtParam3.Visible)
            {
                tunnelParam += txtParam3.Text + ",";
            }
            if (cbotxtParam3.Visible)
            {
                tunnelParam += cbotxtParam3.Text + ",";
            }
            if (txtParam4.Visible)
            {
                tunnelParam += txtParam4.Text + ",";
            }
            if (txtParam5.Visible)
            {
                tunnelParam += txtParam5.Text + ",";
            }
            if (tunnelParam.Length > 0)
            {
                tunnelEntity.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnelEntity.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            //煤巷岩巷
            if (cboCoalOrStone.Text != "")
            {
                tunnelEntity.CoalOrStone = cboCoalOrStone.Text;
            }
            if (cboCoalLayer.Text != "")
            {
                tunnelEntity.CoalSeams = CoalSeams.Find(Convert.ToInt32(cboCoalLayer.SelectedValue));
            }
            //巷道信息登录

            tunnelEntity.TunnelWid = 5;
            tunnelEntity.Save();
            Alert.alert("提交成功！");
        }


        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Text == Const_GM.TUNNEL_INFO_ADD)
            {
                addTunnelInfo();
            }
            if (Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                updateTunnelInfo();
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
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
            if (Validator.IsEmpty(txtTunnelName.Text))
            {
                txtTunnelName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("巷道名称不能为空！");
                txtTunnelName.Focus();
                return false;
            }
            txtTunnelName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            //巷道名称是否包含特殊字符
            //if (!Check.checkSpecialCharacters(txtTunnelName,"巷道名称"))
            //{
            //    return false;
            //}
            //判断设计长度是否为数字
            if (txtDesignLength.Text != "")
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
            showParam();
        }

        //改变窗体大小
        private void changeFormSize(Object gbx)
        {
            if (gbx == null)
            {
                Height = formHeight;
            }
            else if (((GroupBox)gbx).Visible)
            {
                Height = formHeight + ((GroupBox)gbx).Height;
            }
        }

        //控制显示框体
        private void showParam()
        {
            //初使化是否显示
            gbxSquare.Visible = false;
            gbxSemicircle.Visible = false;
            gbxLadderShape.Visible = false;
            gbxArc.Visible = false;
            gbxThreePoint.Visible = false;
            gbxOther.Visible = false;
            txtParam1.Visible = false;
            txtParam2.Visible = false;
            txtParam3.Visible = false;
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            cbotxtParam3.Visible = false;
            if (cboFaultageType.Text == "矩形")
            {
                gbxSquare.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                changeFormSize(gbxSquare);
            }
            if (cboFaultageType.Text == "梯形")
            {
                gbxLadderShape.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                txtParam4.Visible = true;
                txtParam5.Visible = true;
                changeFormSize(gbxLadderShape);
            }
            if (cboFaultageType.Text == "半圆拱")
            {
                gbxSemicircle.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                changeFormSize(gbxSemicircle);
            }
            if (cboFaultageType.Text == "三心拱")
            {
                gbxThreePoint.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                cbotxtParam3.Visible = true;
                changeFormSize(gbxThreePoint);
            }
            if (cboFaultageType.Text == "圆形")
            {
                gbxArc.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                changeFormSize(gbxArc);
            }
            if (cboFaultageType.Text == "其他")
            {
                gbxOther.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                txtParam4.Visible = true;
                txtParam5.Visible = true;
                changeFormSize(gbxOther);
            }
        }

        private void cboFaultageType_TextChanged(object sender, EventArgs e)
        {
            if (cboFaultageType.Text == "")
            {
                changeFormSize(null);
                gbxSquare.Visible = false;
                gbxSemicircle.Visible = false;
                gbxLadderShape.Visible = false;
                gbxArc.Visible = false;
                gbxThreePoint.Visible = false;
                gbxOther.Visible = false;
                txtParam1.Visible = false;
                txtParam2.Visible = false;
                txtParam3.Visible = false;
                txtParam4.Visible = false;
                txtParam5.Visible = false;
                cbotxtParam3.Visible = false;
            }
        }

        private void bindCoalLayer()
        {
            DataSet dsCoalLayer = CoalSeamsBLL.selectAllCoalSeamsInfo();
            cboCoalLayer.DataSource = dsCoalLayer.Tables[0];
            cboCoalLayer.DisplayMember = CoalSeamsDbConstNames.COAL_SEAMS_NAME;
            cboCoalLayer.ValueMember = CoalSeamsDbConstNames.COAL_SEAMS_ID;
            cboCoalLayer.SelectedIndex = -1;
        }

        private void setTunnelType()
        {
            cboCoalOrStone.Text = cboCoalOrStone.Items[0].ToString();
        }

        private void TunnelInfoEntering_Load(object sender, EventArgs e)
        {
            formHeight = Height;
            cboLithology.Text = "";
            changeFormSize(null);

            PanelForTunnelEntering panelForTunnelEnteringForm;
            if (Text == Const_GM.TUNNEL_INFO_ADD)
            {
                panelForTunnelEnteringForm = new PanelForTunnelEntering(MainForm);
            }
            else
            {
                panelForTunnelEnteringForm = new PanelForTunnelEntering(arr, MainForm);
            }
            panelForTunnelEnteringForm.MdiParent = this;
            panel2.Controls.Add(panelForTunnelEnteringForm);
            panelForTunnelEnteringForm.WindowState = FormWindowState.Maximized;
            panelForTunnelEnteringForm.Show();
            panelForTunnelEnteringForm.Activate();
            ActiveControl = txtTunnelName;
            txtTunnelName.Focus();

            if (cboFaultageType.Text != "")
            {
                showParam();
            }

            if (Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                bindInfo(tunnelEntity.TunnelId);
            }
        }

        private void bindInfo(int tunnelID)
        {
            Tunnel tEntity = Tunnel.Find(tunnelID);
            tunnelEntity = tEntity;
            txtTunnelName.Text = tunnelEntity.TunnelName;
            cboSupportPattern.Text = tunnelEntity.TunnelSupportPattern;
            string lithology = "";
            if (tunnelEntity.Lithology.LithologyId != 0)
            {
                lithology = Lithology.Find(tunnelEntity.Lithology).LithologyName;
            }
            cboLithology.Text = lithology;
            cboFaultageType.Text = tunnelEntity.TunnelSectionType;

            txtDesignLength.Text = tunnelEntity.TunnelDesignLength.ToString();
            string[] param = { "", "", "", "", "" };
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
                cboCoalLayer.Text = tunnelEntity.CoalSeams.CoalSeamsName;
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
                cboCoalOrStone.Text = "煤巷";
            }
            else
            {
                cboCoalOrStone.Text = "";
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