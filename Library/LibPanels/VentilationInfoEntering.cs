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

namespace LibPanels
{
    public partial class VentilationInfoEntering : Form
    {
        #region ******变量声明******
        public Ventilation VentilationEntity = new Ventilation();
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
                this.VentilationEntity.IsNoWindArea = 1;
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
                this.VentilationEntity.IsNoWindArea = 0;
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
                this.VentilationEntity.IsLightWindArea = 1;
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
                this.VentilationEntity.IsLightWindArea = 0;
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
                this.VentilationEntity.IsReturnWindArea = 1;
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
                this.VentilationEntity.IsReturnWindArea = 0;
            }
        }

        /// <summary>
        /// 是否通风断面小于设计断面的2/3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnSmallY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSmallY.Checked)
            {
                this.VentilationEntity.IsSmall = 1;
            }
        }

        /// <summary>
        /// 是否通风断面小于设计断面的2/3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnSmallN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSmallN.Checked)
            {
                this.VentilationEntity.IsSmall = 0;
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
                this.VentilationEntity.IsFollowRule = 1;
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
                this.VentilationEntity.IsFollowRule = 0;
            }
        }

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="VentilationEntity">通风实体</param>
        public void bindDefaultValue(Ventilation VentilationEntity)
        {
            //是否有无风区域
            if (VentilationEntity.IsNoWindArea == 1)
            {
                rbtnNoWindY.Checked = true;
            }
            else
            {
                rbtnNoWindN.Checked = true;
            }
            //是否有微风区域
            if (VentilationEntity.IsLightWindArea == 1)
            {
                rbtnLightWindY.Checked = true;
            }
            else
            {
                rbtnLightWindN.Checked = true;
            }
            //是否有风流反向区域
            if (VentilationEntity.IsReturnWindArea == 1)
            {
                rbtnReturnWindY.Checked = true;
            }
            else
            {
                rbtnReturnWindN.Checked = true;
            }
            //是否通风断面小于设计断面的2/3
            if (VentilationEntity.IsSmall == 1)
            {
                rbtnSmallY.Checked = true;
            }
            else
            {
                rbtnSmallN.Checked = true;
            }
            //是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符
            if (VentilationEntity.IsFollowRule == 1)
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
                this.VentilationEntity.FaultageArea = tmpDouble;
                tmpDouble = 0;
            }
        }

        private void txtAirFlow_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtAirFlow.Text, out tmpDouble))
            {
                this.VentilationEntity.AirFlow = tmpDouble;
                tmpDouble = 0;
            }
        }
    }
}
