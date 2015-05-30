using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommonControl
{
    public partial class SelectRuleDlg : Form
    {
        public int ruleId;
        public string ruleDescription;
        DataSet allRules;
        DataTable filteredRules;

        public SelectRuleDlg()
        {
            InitializeComponent();
        }

        private void SelectRuleDlg_Load(object sender, EventArgs e)
        {
            //this.dataGridView1.AutoGenerateColumns = false;
            allRules = LibBusiness.PreWarningRulesBLL.selectAllWarningRules();

            filteredRules = new DataTable();
            filteredRules.Columns.Add(new DataColumn("ID", typeof(int)));
            filteredRules.Columns.Add(new DataColumn("规则类别", typeof(string)));
            filteredRules.Columns.Add(new DataColumn("预警类型", typeof(string)));
            filteredRules.Columns.Add(new DataColumn("预警级别", typeof(string)));
            filteredRules.Columns.Add(new DataColumn("规则描述", typeof(string)));

            getAllRules();

            dataGridView1.DataSource = filteredRules;
            //dataGridView1.Columns[0].HeaderText = "ID";
            //dataGridView1.Columns[2].HeaderText = "规则类别";
            //dataGridView1.Columns[3].HeaderText = "预警类型";
            //dataGridView1.Columns[4].HeaderText = "预警级别";

            //// RULE_CODE
            //dataGridView1.Columns[1].Visible = false;
            //// SUITABLE_LOCATION
            //dataGridView1.Columns[5].Visible = false;
            //// INDICATOR_TYPE
            //dataGridView1.Columns[7].Visible = false;
            //// OPERATOR_TYPE
            //dataGridView1.Columns[8].Visible = false;
            //// MODIFY_DATE
            //dataGridView1.Columns[9].Visible = false;
            //// REMARKS
            //dataGridView1.Columns[10].Visible = false;
            //// BINDING_TABLE_NAME
            //dataGridView1.Columns[11].Visible = false;
            //// BINDING_COLUMN_NAME
            //dataGridView1.Columns[12].Visible = false;
            //// USE_TYPE
            //dataGridView1.Columns[13].Visible = false;
            //// BINDING_SINGLERULES
            //dataGridView1.Columns[14].Visible = false;

            this.cbbWarningLevel.SelectedIndex = 0;
            this.cbbWarningType.SelectedIndex = 0;
            this.cbbRuleType.SelectedIndex = 0;
        }

        // Rule Type comboBox selection changed.
        private void cbbRuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbWarningType.SelectedIndex <= 0 && cbbWarningLevel.SelectedIndex <= 0)
            {
                if (this.cbbRuleType.SelectedIndex > 0)
                    filterRulesByType(cbbRuleType.Text);
                else if (this.cbbRuleType.SelectedIndex == 0)
                    getAllRules();
            }
            else if (cbbWarningType.SelectedIndex > 0 && cbbWarningLevel.SelectedIndex <= 0)
            {
                if (this.cbbRuleType.SelectedIndex > 0)
                    filterRulesByRuleTypeAndWarningType(cbbRuleType.Text, cbbWarningType.Text);
                else if (this.cbbRuleType.SelectedIndex == 0)
                    filterRulesByWarningType(this.cbbWarningType.Text);
            }
            else if (cbbWarningType.SelectedIndex <= 0 && cbbWarningLevel.SelectedIndex > 0)
            {
                if (this.cbbRuleType.SelectedIndex > 0)
                    filterRulesByRuleTypeAndWarningLevel(cbbRuleType.Text, cbbWarningLevel.Text);
                else if (this.cbbRuleType.SelectedIndex == 0)
                    filterRulesByWarningLevel(this.cbbWarningLevel.Text);
            }
            else if (cbbWarningType.SelectedIndex > 0 && cbbWarningLevel.SelectedIndex > 0)
            {
                if (this.cbbRuleType.SelectedIndex > 0)
                    filterRules(cbbRuleType.Text, cbbWarningType.Text, cbbWarningLevel.Text);
                else if (this.cbbRuleType.SelectedIndex == 0)
                    filterRulesByWarningTypeAndWarningLevel(this.cbbWarningType.Text, this.cbbWarningLevel.Text);
            }
            dataGridView1.DataSource = filteredRules;
        }

        private void cbbWarningType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbRuleType.SelectedIndex <= 0 && cbbWarningLevel.SelectedIndex <= 0)
            {
                if (this.cbbWarningType.SelectedIndex > 0)
                    filterRulesByWarningType(cbbWarningType.Text);
                else if (this.cbbWarningType.SelectedIndex == 0)
                    getAllRules();
            }
            else if (cbbRuleType.SelectedIndex > 0 && cbbWarningLevel.SelectedIndex <= 0)
            {
                if (this.cbbWarningType.SelectedIndex > 0)
                    filterRulesByRuleTypeAndWarningType(cbbRuleType.Text, cbbWarningType.Text);
                else if (this.cbbWarningType.SelectedIndex == 0)
                    filterRulesByType(this.cbbRuleType.Text);
            }
            else if (cbbRuleType.SelectedIndex <= 0 && cbbWarningLevel.SelectedIndex > 0)
            {
                if (this.cbbWarningType.SelectedIndex > 0)
                    filterRulesByWarningTypeAndWarningLevel(cbbWarningType.Text, cbbWarningLevel.Text);
                else if (this.cbbWarningType.SelectedIndex == 0)
                    filterRulesByWarningLevel(this.cbbWarningLevel.Text);
            }
            else if (cbbRuleType.SelectedIndex > 0 && cbbWarningLevel.SelectedIndex > 0)
            {
                if (this.cbbWarningType.SelectedIndex > 0)
                    filterRules(cbbRuleType.Text, cbbWarningType.Text, cbbWarningLevel.Text);
                else if (this.cbbWarningType.SelectedIndex == 0)
                    filterRulesByRuleTypeAndWarningLevel(this.cbbRuleType.Text, this.cbbWarningLevel.Text);
            }
            dataGridView1.DataSource = filteredRules;
        }

        // Warning level combobox selection changed.
        private void cbbWarningLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbRuleType.SelectedIndex <= 0 && cbbWarningType.SelectedIndex <= 0)
            {
                if (this.cbbWarningLevel.SelectedIndex > 0)
                    filterRulesByWarningLevel(cbbWarningLevel.Text);
                else if (this.cbbWarningLevel.SelectedIndex == 0)
                    getAllRules();
            }
            else if (cbbRuleType.SelectedIndex > 0 && cbbWarningType.SelectedIndex <= 0)
            {
                if (this.cbbWarningLevel.SelectedIndex > 0)
                    filterRulesByRuleTypeAndWarningLevel(cbbRuleType.Text, cbbWarningLevel.Text);
                else if (this.cbbWarningLevel.SelectedIndex == 0)
                    filterRulesByType(this.cbbRuleType.Text);
            }
            else if (cbbRuleType.SelectedIndex <= 0 && cbbWarningType.SelectedIndex > 0)
            {
                if (this.cbbWarningLevel.SelectedIndex > 0)
                    filterRulesByWarningTypeAndWarningLevel(cbbWarningType.Text, cbbWarningLevel.Text);
                else if (this.cbbWarningLevel.SelectedIndex == 0)
                    filterRulesByWarningType(this.cbbWarningType.Text);
            }
            else if (cbbRuleType.SelectedIndex > 0 && cbbWarningType.SelectedIndex > 0)
            {
                if (this.cbbWarningLevel.SelectedIndex > 0)
                    filterRules(cbbRuleType.Text, cbbWarningType.Text, cbbWarningLevel.Text);
                else if (this.cbbWarningLevel.SelectedIndex == 0)
                    filterRulesByRuleTypeAndWarningType(this.cbbRuleType.Text, this.cbbWarningType.Text);
            }
            dataGridView1.DataSource = filteredRules;
        }

        public void getAllRules()
        {
            filteredRules.Clear();
            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
            }
        }

        private void filterRulesByType(string ruleType)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.RULE_TYPE].ToString() == ruleType)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        // Get all the rules with the specified warning type.
        private void filterRulesByWarningType(string warningType)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.WARNING_TYPE].ToString() == warningType)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        // Get all the rules with the specified warning level.
        private void filterRulesByWarningLevel(string warningLevel)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.WARNING_LEVEL].ToString() == warningLevel)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        private void filterRulesByRuleTypeAndWarningType(string ruleType, string warningType)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.RULE_TYPE].ToString() == ruleType &&
                    row[LibBusiness.PreWarningRulesDbConstNames.WARNING_TYPE].ToString() == warningType)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        private void filterRulesByRuleTypeAndWarningLevel(string ruleType, string warningLevel)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.RULE_TYPE].ToString() == ruleType &&
                    row[LibBusiness.PreWarningRulesDbConstNames.WARNING_LEVEL].ToString() == warningLevel)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        private void filterRulesByWarningTypeAndWarningLevel(string warningType, string warningLevel)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.WARNING_TYPE].ToString() == warningType &&
                    row[LibBusiness.PreWarningRulesDbConstNames.WARNING_LEVEL].ToString() == warningLevel)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        private void filterRules(string ruleType, string warningType, string warningLevel)
        {
            filteredRules.Clear();

            foreach (DataRow row in allRules.Tables[0].Rows)
            {
                if (row[LibBusiness.PreWarningRulesDbConstNames.RULE_TYPE].ToString() == ruleType &&
                    row[LibBusiness.PreWarningRulesDbConstNames.WARNING_TYPE].ToString() == warningType &&
                    row[LibBusiness.PreWarningRulesDbConstNames.WARNING_LEVEL].ToString() == warningLevel)
                {
                    //filteredRules.Rows.Add(row);
                    filteredRules.Rows.Add(row[0], row[2], row[3], row[4], row[6]);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // 关闭窗口
            this.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //}

            int index = e.RowIndex;
            this.ruleId = int.Parse(this.filteredRules.Rows[index]["ID"].ToString());
            this.ruleDescription = this.filteredRules.Rows[index]["规则描述"].ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
