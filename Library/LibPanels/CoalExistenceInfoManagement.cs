// ******************************************************************
// 概  述：井下数据煤层赋存信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Windows.Forms;
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibCommonControl;

namespace LibPanels
{
    public partial class CoalExistenceInfoManagement : BaseForm
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public CoalExistenceInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;

            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineDataManagement_Load(object sender, EventArgs e)
        {
            gcCoalExistence.DataSource = CoalExistence.FindAll();
        }

        /// <summary>
        ///  添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //MineData m = new MineData();
            var m = new MineDataSimple(MainForm)
            {
                Text = new LibPanels(MineDataPanelName.CoalExistence).panelFormName
            };
            if (DialogResult.OK == m.ShowDialog())
            {

            }
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {

            var m = new MineData(MainForm) { Text = new LibPanels(MineDataPanelName.CoalExistence_Change).panelFormName };

            if (DialogResult.OK == m.ShowDialog())
            {

            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnExit_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void bandedGridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "IsLevelDisorder" || e.Column.FieldName == "IsLevelChange" ||
                e.Column.FieldName == "IsTowardsChange" || e.Column.FieldName == "IsCoalMerge" ||
                e.Column.FieldName == "IsCoalSoft")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "否";
                        break;
                    case "1":
                        e.DisplayText = "是";
                        break;
                }
            }

        }
    }
}
