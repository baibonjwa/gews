// ******************************************************************
// 概  述：井下数据瓦斯信息添加修改
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
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class GasInfoEntering : Form
    {
        #region ******变量声明******
        public GasDataEntity gasDataEntity = new GasDataEntity();
        double tmpDouble = 0;
        #endregion ******变量声明******

        /// <summary>
        /// 构造方法
        /// </summary>
        public GasInfoEntering()
        {
            InitializeComponent();

            // 添加默认值
            this.txtCoalBangTimes.Text = "0";
            this.txtCraterTimes.Text = "0";
            this.txtDrillTimes.Text = "0";
            this.txtGasThickness.Text = "0";
            this.txtGasTimes.Text = "0";
            this.txtPowerFailure.Text = "0";
            this.txtStoperTimes.Text = "0";
            this.txtTempDownTimes.Text = "0";
        }

        /// <summary>
        /// 瓦斯探头断电次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPowerFailure_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtPowerFailure.Text, out tmpDouble))
            {
                gasDataEntity.PowerFailure = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 吸钻预兆次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDrillTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtDrillTimes.Text, out tmpDouble))
            {
                gasDataEntity.DrillTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 瓦斯忽大忽小预兆次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGasTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtGasTimes.Text, out tmpDouble))
            {
                gasDataEntity.GasTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 气温下降预兆次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTempDownTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtTempDownTimes.Text, out tmpDouble))
            {
                gasDataEntity.TempDownTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 煤炮频繁预兆次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCoalBangTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtCoalBangTimes.Text, out tmpDouble))
            {
                gasDataEntity.CoalBangTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 喷孔次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCraterTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtCraterTimes.Text, out tmpDouble))
            {
                gasDataEntity.CraterTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 顶钻次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStoperTimes_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtStoperTimes.Text, out tmpDouble))
            {
                gasDataEntity.StoperTimes = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 瓦斯浓度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGasThickness_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtGasThickness.Text, out tmpDouble))
            {
                gasDataEntity.GasThickness = tmpDouble;
                tmpDouble = 0;
            }
        }
        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="gasDataEntity"></param>
        public void bindDefaultValue(GasDataEntity gasDataEntity)
        {
            //瓦斯探头断电次数
            txtPowerFailure.Text = gasDataEntity.PowerFailure.ToString();
            //吸钻预兆次数
            txtDrillTimes.Text = gasDataEntity.DrillTimes.ToString();
            //瓦斯忽大忽小预兆次数
            txtGasTimes.Text = gasDataEntity.GasTimes.ToString();
            //气温下降预兆次数
            txtTempDownTimes.Text = gasDataEntity.TempDownTimes.ToString();
            //煤炮频繁预兆次数
            txtCoalBangTimes.Text = gasDataEntity.CoalBangTimes.ToString();
            //喷孔次数
            txtCraterTimes.Text = gasDataEntity.CraterTimes.ToString();
            //顶钻次数
            txtStoperTimes.Text = gasDataEntity.StoperTimes.ToString();
            //瓦斯浓度
            txtGasThickness.Text = gasDataEntity.GasThickness.ToString();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        public bool check()
        {
            //瓦斯探头断电次数数字验证
            if (!Check.IsNumeric(txtPowerFailure, Const_OP.POWER_FAILURE))
            {
                return false;
            }
            //吸钻预兆次数数字验证
            if (!Check.IsNumeric(txtDrillTimes, Const_OP.DRILL_TIMES))
            {
                return false;
            }
            //瓦斯忽大忽小预兆次数数字验证
            if (!Check.IsNumeric(txtGasTimes, Const_OP.GAS_TIMES))
            {
                return false;
            }
            //气温下降预兆次数数字验证
            if (!Check.IsNumeric(txtTempDownTimes, Const_OP.TEMP_DOWN_TIMES))
            {
                return false;
            }
            //煤炮频繁预兆次数数字验证
            if (!Check.IsNumeric(txtCoalBangTimes, Const_OP.COAL_BANG_TIMES))
            {
                return false;
            }
            //喷孔次数数字验证
            if (!Check.IsNumeric(txtCraterTimes, Const_OP.CRATER_TIMES))
            {
                return false;
            }
            //顶钻次数数字验证
            if (!Check.IsNumeric(txtStoperTimes, Const_OP.STOPER_TIMES))
            {
                return false;
            }
            return true;
        }

        
    }
}
