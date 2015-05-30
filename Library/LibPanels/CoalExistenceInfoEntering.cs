// ******************************************************************
// 概  述：井下数据煤层赋存信息添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class CoalExistenceInfoEntering : Form
    {
        #region ******变量声明******
        public CoalExistence coalExistenceEntity = new CoalExistence();
        double tmpDouble = 0;
        #endregion ******变量声明******

        /// <summary>
        /// 构造方法
        /// </summary>
        public CoalExistenceInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否层理紊乱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLevelDisorderY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLevelDisorderY.Checked)
            {
                coalExistenceEntity.IsLevelDisorder = 1;
            }
        }

        /// <summary>
        /// 是否层理紊乱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLevelDisorderN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLevelDisorderN.Checked)
            {
                coalExistenceEntity.IsLevelDisorder = 0;
            }
        }

        /// <summary>
        /// 是否软分层（构造煤）层位是否发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLevelChangeY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLevelChangeY.Checked)
            {
                coalExistenceEntity.IsLevelChange = 1;
            }
        }

        /// <summary>
        /// 是否软分层（构造煤）层位是否发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnLevelChangeN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLevelChangeN.Checked)
            {
                coalExistenceEntity.IsLevelChange = 0;
            }
        }

        /// <summary>
        /// 煤厚变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCoalThickChange_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtCoalThickChange.Text, out tmpDouble))
            {
                coalExistenceEntity.CoalThickChange = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 煤厚变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTectonicCoalThick_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtTectonicCoalThick.Text, out tmpDouble))
            {
                coalExistenceEntity.TectonicCoalThick = tmpDouble;
                tmpDouble = 0;
            }
        }

        /// <summary>
        /// 煤体破坏类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbmCoalDistoryLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            coalExistenceEntity.CoalDistoryLevel = cbmCoalDistoryLevel.Text;
        }

        /// <summary>
        /// 是否煤层走向、倾角突然急剧变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnTowardsChangeY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTowardsChangeY.Checked)
                coalExistenceEntity.IsTowardsChange = 1;
        }

        /// <summary>
        /// 是否煤层走向、倾角突然急剧变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnTowardsChangeN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTowardsChangeN.Checked)
                coalExistenceEntity.IsTowardsChange = 0;
        }

        /// <summary>
        /// 调用修改时绑定默认数据
        /// </summary>
        public void bindDefaultValue(CoalExistence coalExistenceEntity)
        {
            //是否层理紊乱
            if (coalExistenceEntity.IsLevelDisorder == 1)
            {
                rbtnLevelDisorderY.Checked = true;
            }
            else
            {
                rbtnLevelChangeN.Checked = true;
            }
            //是否软分层（构造煤）层位是否发生变化
            if (coalExistenceEntity.IsLevelChange == 1)
            {
                rbtnLevelChangeY.Checked = true;
            }
            else
            {
                rbtnLevelChangeN.Checked = true;
            }
            //是否煤层走向、倾角突然急剧变化
            if (coalExistenceEntity.IsTowardsChange == 1)
            {
                rbtnTowardsChangeY.Checked = true;
            }
            else
            {
                rbtnTowardsChangeN.Checked = true;
            }
            //煤厚变化
            txtCoalThickChange.Text = Convert.ToString(coalExistenceEntity.CoalThickChange);
            //软分层（构造煤）厚度
            txtTectonicCoalThick.Text = Convert.ToString(coalExistenceEntity.TectonicCoalThick);
            //煤体破坏类型
            cbmCoalDistoryLevel.Text = coalExistenceEntity.CoalDistoryLevel;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        public bool check()
        {
            if (!Check.IsNumeric(txtCoalThickChange, Const_GE.COAL_THICK_CHANGE))
            {
                return false;
            }
            if (!Check.IsNumeric(txtTectonicCoalThick, Const_GE.TECTONIC_COAL_THICK))
            {
                return false;
            }
            return true;
        }
    }
}
