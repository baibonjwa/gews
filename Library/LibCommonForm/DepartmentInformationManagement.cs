// ******************************************************************
// 概   述：部门信息管理
// 作  者：秦  凯
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息:
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
using LibDatabase;
using LibXPorperty;
using LibEntity;
using System.Reflection;
using FarPoint.Win.Spread.CellType;
using LibBusiness;

namespace LibCommonForm
{
    public partial class DepartmentInformation : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentInformation()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibFormTitles.DEPARTMENT_MANMAGEMENT);
        }

        private void RefreshData()
        {
            var departments = Department.FindAll();
            gcDepartment.DataSource = departments;
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {

            var m = new DepartmentInformationInput();
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.alert("请选择要修改的信息");
                return;
            }
            var departmentEntering = new DepartmentInformationInput(((Department)gridView1.GetFocusedRow()));
            if (DialogResult.OK == departmentEntering.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var department in selectedIndex.Select(i => (Department)gridView1.GetRow(i)))
            {
                department.Delete();
            }
            RefreshData();
        }

        /// <summary>
        /// 窗体登陆时加载数据,设置按钮是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentInformation_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcDepartment.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcDepartment, "部门信息报表");
        }




    }
}
