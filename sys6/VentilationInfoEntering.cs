// ******************************************************************
// 概  述：井下数据通风信息添加修改
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
using LibEntity;

namespace UnderTerminal
{
    public partial class VentilationInfoEntering : Form
    {
        #region ******变量声明******
        public VentilationInfo ventilationInfoEntity = new VentilationInfo();
        double tmpDouble = 0;
        #endregion ******变量声明******

        /// <summary>
        /// 构造方法
        /// </summary>
        public VentilationInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否有无风区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNoWindY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNoWindY.Checked)
            {
                this.ventilationInfoEntity.IsNoWindArea = 1;
            }
        }

        /// <summary>
        /// 是否有无风区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNoWindN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNoWindN.Checked)
            {
                this.ventilationInfoEntity.IsNoWindArea = 0;
            }
        }

        /// <summary>
        /// 是否有微风区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLightWindY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLightWindY.Checked)
            {
                this.ventilationInfoEntity.IsLightWindArea = 1;
            }
        }

        /// <summary>
        /// 是否有微风区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLightWindN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLightWindN.Checked)
            {
                this.ventilationInfoEntity.IsLightWindArea = 0;
            }
        }

        /// <summary>
        /// 是否有风流反向区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnReturnWindY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReturnWindY.Checked)
            {
                this.ventilationInfoEntity.IsReturnWindArea = 1;
            }
        }

        /// <summary>
        /// 是否有风流反向区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnReturnWindN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReturnWindN.Checked)
            {
                this.ventilationInfoEntity.IsReturnWindArea = 0;
            }
        }

        /// <summary>
        /// 是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnFollowRuleY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFollowRuleY.Checked)
            {
                this.ventilationInfoEntity.IsFollowRule = 1;
            }
        }

        /// <summary>
        /// 是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnFollowRuleN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFollowRuleN.Checked)
            {
                this.ventilationInfoEntity.IsFollowRule = 0;
            }
        }

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="ventilationInfoEntity">通风实体</param>
        public void bindDefaultValue(VentilationInfo ventilationInfoEntity)
        {
            //是否有无风区域
            if (ventilationInfoEntity.IsNoWindArea == 1)
            {
                rbtnNoWindY.Checked = true;
            }
            else
            {
                rbtnNoWindN.Checked = true;
            }
            //是否有微风区域
            if (ventilationInfoEntity.IsLightWindArea == 1)
            {
                rbtnLightWindY.Checked = true;
            }
            else
            {
                rbtnLightWindN.Checked = true;
            }
            //是否有风流反向区域
            if (ventilationInfoEntity.IsReturnWindArea == 1)
            {
                rbtnReturnWindY.Checked = true;
            }
            else
            {
                rbtnReturnWindN.Checked = true;
            }

            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
            if (ventilationInfoEntity.IsFollowRule == 1)
            {
                rbtnFollowRuleY.Checked = true;
            }
            else
            {
                rbtnFollowRuleN.Checked = true;
            }
        }

        private void txtFaultageArea_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtFaultageArea.Text, out tmpDouble))
            {
                this.ventilationInfoEntity.FaultageArea = tmpDouble;
                tmpDouble = 0;
            }
        }

        private void txtAirFlow_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtAirFlow.Text, out tmpDouble))
            {
                this.ventilationInfoEntity.AirFlow = tmpDouble;
                tmpDouble = 0;
            }
        }
    }
}
