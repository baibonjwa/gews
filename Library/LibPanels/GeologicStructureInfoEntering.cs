// ******************************************************************
// 概  述：井下数据地质构造信息添加修改
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
    public partial class GeologicStructureInfoEntering : Form
    {
        #region ******变量声明******
        public GeologicStructure geoligicStructureEntity = new GeologicStructure();
        #endregion#region ******变量声明******

        /// <summary>
        /// 构造方法 
        /// </summary>
        public GeologicStructureInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 无计划揭露构造
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNoPlanStructure_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.NoPlanStructure = chkNoPlanStructure.Checked ? 1 : 0;
        }

        /// <summary>
        /// 过构造时措施无效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPassedStructureRuleInvalid_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.PassedStructureRuleInvalid = chkPassedStructureRuleInvalid.Checked ? 1 : 0;
        }

        /// <summary>
        /// 黄色预警措施无效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkYellowRuleInvalid_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.YellowRuleInvalid = chkYellowRuleInvalid.Checked ? 1 : 0;
        }

        /// <summary>
        /// 顶板破碎
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRoofBroken_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.RoofBroken = chkRoofBroken.Checked ? 1 : 0;
        }

        /// <summary>
        /// 煤层松软
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCoalSeamSoft_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.CoalSeamSoft = chkCoalSeamSoft.Checked ? 1 : 0;
        }

        /// <summary>
        /// 工作面煤层处于分叉、合层状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCoalSeamBranch_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.CoalSeamBranch = chkCoalSeamBranch.Checked ? 1 : 0;
        }

        /// <summary>
        /// 顶板条件发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRoofChange_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.RoofChange = chkRoofChange.Checked ? 1 : 0;
        }

        /// <summary>
        /// 工作面夹矸突然变薄或消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGangueDisappear_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.GangueDisappear = chkGangueDisappear.Checked ? 1 : 0;
        }

        /// <summary>
        /// 夹矸位置急剧变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGangueLocationChange_Click(object sender, EventArgs e)
        {
            geoligicStructureEntity.GangueLocationChange = chkGangueLocationChange.Checked ? 1 : 0;
        }

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="geologicStructureEntity"></param>
        public void bindDefaultValue(GeologicStructure geologicStructureEntity)
        {
            //无计划揭露构造
            chkNoPlanStructure.Checked = (geologicStructureEntity.NoPlanStructure == 1);
            //过构造时措施无效
            chkPassedStructureRuleInvalid.Checked = (geologicStructureEntity.PassedStructureRuleInvalid == 1);
            //黄色预警措施无效
            chkYellowRuleInvalid.Checked = (geologicStructureEntity.YellowRuleInvalid == 1);
            //顶板破碎
            chkRoofBroken.Checked = (geologicStructureEntity.RoofBroken == 1);
            //煤层松软
            chkCoalSeamSoft.Checked = (geologicStructureEntity.CoalSeamSoft == 1);
            //工作面煤层处于分叉、合层状态
            chkCoalSeamBranch.Checked = (geologicStructureEntity.CoalSeamBranch == 1);
            //顶板条件发生变化
            chkRoofChange.Checked = (geologicStructureEntity.RoofChange == 1);
            //工作面夹矸突然变薄或消失
            chkGangueDisappear.Checked = (geologicStructureEntity.GangueDisappear == 1);
            //夹矸位置急剧变化
            chkGangueLocationChange.Checked = (geologicStructureEntity.GangueLocationChange == 1);
        }
    }
}
