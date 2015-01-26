// ******************************************************************
// 概  述：井下数据管理信息添加修改
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
    public partial class ManagementInfoEntering : Form
    {
        #region ******变量声明******
        public Management managementEntity = new Management();
        #endregion ******变量声明******

        /// <summary>
        /// 构造方法
        /// </summary>
        public ManagementInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="managementEntity"></param>
        public void bindDefaultValue(Management managementEntity)
        {
            //是否存在瓦斯异常不汇报
            if (managementEntity.IsGasErrorNotReport == 1)
            {
                chkIsGasErrorNotReport.Checked = true;
            }
            else
            {
                chkIsGasErrorNotReport.Checked = false;
            }
            //是否存在工作面出现地质构造不汇报
            if (managementEntity.IsWfNotReport == 1)
            {
                chkIsWFNotReport.Checked = true;
            }
            else
            {
                chkIsWFNotReport.Checked = false;
            }
            //是否存在强化瓦斯措施执行不到位
            if (managementEntity.IsStrgasNotDoWell == 1)
            {
                chkIsStrgasNotDoWell.Checked = true;
            }
            else
            {
                chkIsStrgasNotDoWell.Checked = false;
            }
            //是否存在进回风巷隅角、尾巷管理不到位
            if (managementEntity.IsRwmanagementNotDoWell == 1)
            {
                chkIsRWManagementnotDoWell.Checked = true;
            }
            else
            {
                chkIsRWManagementnotDoWell.Checked = false;
            }
            //是否存在通风设施人为损坏
            if (managementEntity.IsVfBrokenByPeople == 1)
            {
                chkIsVFBrokenByPeople.Checked = true;
            }
            else
            {
                chkIsVFBrokenByPeople.Checked = false;
            }
            //是否存在甲烷传感器位置不当、误差大、调校超过规定
            if (managementEntity.IsElementPlaceNotGood == 1)
            {
                chkIsElementPlaceNotGood.Checked = true;
            }
            else
            {
                chkIsElementPlaceNotGood.Checked = false;
            }
            //是否存在瓦检员空漏假检
            if (managementEntity.IsReporterFalseData == 1)
            {
                chkIsReporterFalseData.Checked = true;
            }
            else
            {
                chkIsReporterFalseData.Checked = false;
            }
            //钻孔未按设计施工
            if (managementEntity.IsDrillWrongBuild == 1)
            {
                chkIsDrillWrongBuild.Checked = true;
            }
            else
            {
                chkIsDrillWrongBuild.Checked = false;
            }
            //钻孔施工不到位
            if (managementEntity.IsDrillNotDoWell == 1)
            {
                chkIsDrillNotDoWell.Checked = true;
            }
            else
            {
                chkIsDrillNotDoWell.Checked = false;
            }
            //防突措施执行不到位
            if (managementEntity.IsOpNotDoWell == 1)
            {
                chkIsOPNotDoWellTimes.Checked = true;
            }
            else
            {
                chkIsOPNotDoWellTimes.Checked = false;
            }
            //防突异常情况未汇报
            if (managementEntity.IsOpErrorNotReport == 1)
            {
                chkIsOPErrorNotReport.Checked = true;
            }
            else
            {
                chkIsOPErrorNotReport.Checked = false;
            }
            //是否存在局部通风机单回路供电或不能正常切换
            if (managementEntity.IsPartWindSwitchError == 1)
            {
                chkIsPartWindSwitchError.Checked = true;
            }
            else
            {
                chkIsPartWindSwitchError.Checked = false;
            }
            //是否存在安全监测监控系统未及时安装
            if (managementEntity.IsSafeCtrlUninstall == 1)
            {
                chkIsSafeCtrlUninstall.Checked = true;
            }
            else
            {
                chkIsSafeCtrlUninstall.Checked = false;
            }
            //是否存在监测监控停运
            if (managementEntity.IsCtrlStop == 1)
            {
                chkIsCtrlStop.Checked = true;
            }
            else
            {
                chkIsCtrlStop.Checked = false;
            }
            //是否存在不执行瓦斯治理措施、破坏通风设施
            if (managementEntity.IsGasNotDowWell == 1)
            {
                chkIsGasNotDoWell.Checked = true;
            }
            else
            {
                chkIsGasNotDoWell.Checked = false;
            }
            //是否高、突矿井工作面无专职瓦斯检查员
            if (managementEntity.IsMineNoChecker == 1)
            {
                chkIsMineNoChecker.Checked = true;
            }
            else
            {
                chkIsMineNoChecker.Checked = false;
            }
        }

        /// <summary>
        /// 是否存在瓦斯异常不汇报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsGasErrorNotReport_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsGasErrorNotReport = chkIsGasErrorNotReport.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在进回风巷隅角、尾巷管理不到位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsRWManagementnotDoWell_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsRwmanagementNotDoWell = chkIsRWManagementnotDoWell.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在瓦检员空漏假检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsReporterFalseData_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsReporterFalseData = chkIsReporterFalseData.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在局部通风机单回路供电或不能正常切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsPartWindSwitchError_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsPartWindSwitchError = chkIsPartWindSwitchError.Checked ? 1 : 0;
        }

        /// <summary>
        /// 钻孔未按设计施工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsDrillWrongBuild_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsDrillWrongBuild = chkIsDrillWrongBuild.Checked ? 1 : 0;
        }

        /// <summary>
        /// 防突措施执行不到位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsOPNotDoWellTimes_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsOpNotDoWell = chkIsOPNotDoWellTimes.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在工作面出现地质构造不汇报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsWFNotReport_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsWfNotReport = chkIsWFNotReport.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在通风设施人为损坏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsVFBrokenByPeople_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsVfBrokenByPeople = chkIsVFBrokenByPeople.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在安全监测监控系统未及时安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsSafeCtrlUninstall_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsSafeCtrlUninstall = chkIsSafeCtrlUninstall.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在不执行瓦斯治理措施、破坏通风设施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsGasNotDoWell_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsGasNotDowWell = chkIsGasNotDoWell.Checked ? 1 : 0;
        }

        /// <summary>
        /// 钻孔施工不到位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsDrillNotDoWell_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsDrillNotDoWell = chkIsDrillNotDoWell.Checked ? 1 : 0;
        }

        /// <summary>
        /// 防突异常情况未汇报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsOPErrorNotReport_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsOpErrorNotReport = chkIsOPErrorNotReport.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在强化瓦斯措施执行不到位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsStrgasNotDoWell_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsStrgasNotDoWell = chkIsStrgasNotDoWell.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在甲烷传感器位置不当、误差大、调校超过规定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsElementPlaceNotGood_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsElementPlaceNotGood = chkIsElementPlaceNotGood.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否存在监测监控停运
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsCtrlStop_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsCtrlStop = chkIsCtrlStop.Checked ? 1 : 0;
        }

        /// <summary>
        /// 是否高、突矿井工作面无专职瓦斯检查员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsMineNoChecker_CheckedChanged(object sender, EventArgs e)
        {
            managementEntity.IsMineNoChecker = chkIsMineNoChecker.Checked ? 1 : 0;
        }
    }
}
