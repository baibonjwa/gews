using System;
using System.Globalization;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using NHibernate.Engine;

namespace LibCommonForm
{
    public partial class TunnelInfoEntering : BaseForm
    {
        private int _formHeight;
        private Tunnel Tunnel { get; set; }

        /// <summary>
        ///     添加
        /// </summary>
        public TunnelInfoEntering()
        {
            InitializeComponent();
            //设置窗体格式
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_ADD);
            Text = Const_GM.TUNNEL_INFO_ADD;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        ///     修改
        /// </summary>
        public TunnelInfoEntering(Tunnel tunnel)
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_INFO_CHANGE);
            Text = Const_GM.TUNNEL_INFO_CHANGE;
            Tunnel = tunnel;
        }

        private void TunnelInfoEntering_Load(object sender, EventArgs e)
        {
            _formHeight = Height;
            ChangeFormSize(null);

            DataBindUtil.LoadLithology(cboLithology, "煤层");
            DataBindUtil.LoadCoalSeamsName(cboCoalLayer);

            if (Text == Const_GM.TUNNEL_INFO_ADD)
            {
                selectWorkingFaceControl1.LoadData();
            }
            else
            {
                selectWorkingFaceControl1.LoadData(Tunnel.WorkingFace);
                txtTunnelName.Text = Tunnel.TunnelName;
                cboSupportPattern.Text = Tunnel.TunnelSupportPattern;
                cboSupportPattern.Text = Tunnel.TunnelSupportPattern;
                cboLithology.SelectedValue = Tunnel.Lithology.LithologyId;
                cboFaultageType.Text = Tunnel.TunnelSectionType;

                txtDesignLength.Text = Tunnel.TunnelDesignLength.ToString(CultureInfo.InvariantCulture);
                string[] param = { "", "", "", "", "" };
                if (Tunnel.TunnelParam != null)
                {
                    string[] paramOld = Tunnel.TunnelParam.Split(',');
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

                cboCoalOrStone.Text = Tunnel.CoalOrStone;
                if (cboCoalOrStone.Text == Const_GM.COAL_TUNNEL)
                {
                    cboCoalLayer.Visible = true;
                    cboCoalLayer.Text = Tunnel.CoalSeams.CoalSeamsName;
                }
                else
                {
                    cboCoalLayer.Visible = false;
                }
            }

            if (cboFaultageType.Text != "")
            {
                ShowParam();
            }
        }

        private void AddTunnelInfo()
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            //创建巷道实体
            var tunnel = new Tunnel
            {
                TunnelName = txtTunnelName.Text,
                TunnelSupportPattern = cboSupportPattern.Text,
                WorkingFace = selectWorkingFaceControl1.SelectedWorkingFace,
                Lithology = cboLithology.SelectedItem == null ? null : (Lithology)cboLithology.SelectedItem,
                TunnelSectionType = cboFaultageType.Text,
                TunnelType = TunnelTypeEnum.OTHER,
                CoalOrStone = cboCoalOrStone.Text,
                CoalSeams = CoalSeams.Find(cboCoalLayer.SelectedValue),
                BindingId = IDGenerator.NewBindingID(),
                TunnelWid = 5
            };
            //巷道类型
            //煤巷岩巷
            //绑定煤层
            //通过煤层ID获取煤层名称方法

            //断面类型
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
                tunnel.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (txtDesignLength.Text != "")
            {
                tunnel.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                tunnel.TunnelDesignArea = Convert.ToInt32(txtDesignLength.Text);
            }
            //巷道信息登录

            tunnel.Save();
            Alert.alert("提交成功！");
        }

        private void UpdateTunnelInfo()
        {
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
            Tunnel.WorkingFace = selectWorkingFaceControl1.SelectedWorkingFace;
            //巷道名称
            Tunnel.TunnelName = txtTunnelName.Text;
            //支护方式
            Tunnel.TunnelSupportPattern = cboSupportPattern.Text;
            //围岩类型
            Tunnel.Lithology = (Lithology)cboLithology.SelectedItem;

            Tunnel.TunnelWid = 5;
            //断面类型
            Tunnel.TunnelSectionType = cboFaultageType.Text;
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
                Tunnel.TunnelParam = tunnelParam.Remove(tunnelParam.Length - 1);
            }
            //设计长度
            if (txtDesignLength.Text != "")
            {
                Tunnel.TunnelDesignLength = Convert.ToInt32(txtDesignLength.Text);
            }
            if (txtDesignArea.Text != "")
            {
                Tunnel.TunnelDesignArea = Convert.ToInt32(txtDesignLength.Text);
            }
            //煤巷岩巷
            if (cboCoalOrStone.Text != "")
            {
                Tunnel.CoalOrStone = cboCoalOrStone.Text;
            }
            if (cboCoalLayer.Text != "")
            {
                Tunnel.CoalSeams = CoalSeams.Find(Convert.ToInt32(cboCoalLayer.SelectedValue));
            }
            //巷道信息登录

            Tunnel.Save();
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
                AddTunnelInfo();
            }
            if (Text == Const_GM.TUNNEL_INFO_CHANGE)
            {
                UpdateTunnelInfo();
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
        private bool Check()
        {
            if (selectWorkingFaceControl1.SelectedWorkingFace == null)
            {
                Alert.alert("请选择巷道所在工作面信息");
                return false;
            }
            // 判断巷道名称是否入力
            if (String.IsNullOrWhiteSpace(txtTunnelName.Text))
            {
                txtTunnelName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("巷道名称不能为空！");
                txtTunnelName.Focus();
                return false;
            }
            txtTunnelName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            if (String.IsNullOrWhiteSpace(txtDesignLength.Text))
            {
                if (!LibCommon.Check.IsNumeric(txtDesignLength, "设计长度"))
                {
                    return false;
                }
            }

            //验证通过
            return true;
        }

        private void cboFaultageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowParam();
        }

        //改变窗体大小
        private void ChangeFormSize(Object gbx)
        {
            if (gbx == null)
            {
                Height = _formHeight;
            }
            else if (((GroupBox)gbx).Visible)
            {
                Height = _formHeight + ((GroupBox)gbx).Height;
            }
        }

        //控制显示框体
        private void ShowParam()
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
            if (cboFaultageType.Text == @"矩形")
            {
                gbxSquare.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                ChangeFormSize(gbxSquare);
            }
            if (cboFaultageType.Text == @"梯形")
            {
                gbxLadderShape.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                txtParam4.Visible = true;
                txtParam5.Visible = true;
                ChangeFormSize(gbxLadderShape);
            }
            if (cboFaultageType.Text == @"半圆拱")
            {
                gbxSemicircle.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                ChangeFormSize(gbxSemicircle);
            }
            if (cboFaultageType.Text == @"三心拱")
            {
                gbxThreePoint.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                cbotxtParam3.Visible = true;
                ChangeFormSize(gbxThreePoint);
            }
            if (cboFaultageType.Text == @"圆形")
            {
                gbxArc.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                ChangeFormSize(gbxArc);
            }
            if (cboFaultageType.Text == @"其他")
            {
                gbxOther.Visible = true;
                txtParam1.Visible = true;
                txtParam2.Visible = true;
                txtParam3.Visible = true;
                txtParam4.Visible = true;
                txtParam5.Visible = true;
                ChangeFormSize(gbxOther);
            }
        }

        private void cboFaultageType_TextChanged(object sender, EventArgs e)
        {
            if (cboFaultageType.Text == "")
            {
                ChangeFormSize(null);
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

        private void cboLithology_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCoalOrStone.Text = cboLithology.Text == @"煤层" ? "煤巷" : "岩巷";
        }

        private void cboCoalOrStone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCoalOrStone.Text == @"煤巷")
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