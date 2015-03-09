using System.Windows.Forms;

namespace LibCommonForm
{
    partial class QueryConditions : Control
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this._dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.selectTunnelSimple1 = new SelectTunnelSimple();
            this.SuspendLayout();
            // 
            // _btnSearch
            // 
            this._btnSearch.Location = new System.Drawing.Point(727, 10);
            this._btnSearch.Name = "_btnSearch";
            this._btnSearch.Size = new System.Drawing.Size(75, 23);
            this._btnSearch.TabIndex = 42;
            this._btnSearch.Text = "查询数据";
            this._btnSearch.UseVisualStyleBackColor = true;
            this._btnSearch.Click += new System.EventHandler(this._btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "开始时间：";
            // 
            // _dateTimeEnd
            // 
            this._dateTimeEnd.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeEnd.Location = new System.Drawing.Point(532, 11);
            this._dateTimeEnd.Name = "_dateTimeEnd";
            this._dateTimeEnd.Size = new System.Drawing.Size(175, 21);
            this._dateTimeEnd.TabIndex = 39;
            this._dateTimeEnd.Value = new System.DateTime(2014, 5, 7, 23, 59, 59, 0);
            this._dateTimeEnd.ValueChanged += new System.EventHandler(this._dateTimeEnd_ValueChanged);
            // 
            // _dateTimeStart
            // 
            this._dateTimeStart.CustomFormat = "yyyy/MM/dd   HH:mm:ss";
            this._dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimeStart.Location = new System.Drawing.Point(280, 11);
            this._dateTimeStart.Name = "_dateTimeStart";
            this._dateTimeStart.Size = new System.Drawing.Size(175, 21);
            this._dateTimeStart.TabIndex = 38;
            this._dateTimeStart.Value = new System.DateTime(2014, 5, 7, 0, 0, 0, 0);
            this._dateTimeStart.ValueChanged += new System.EventHandler(this._dateTimeStart_ValueChanged);
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(3, 3);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.Size = new System.Drawing.Size(219, 38);
            this.selectTunnelSimple1.TabIndex = 37;
            // 
            // QueryConditions
            // 
            this.Controls.Add(this._btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dateTimeEnd);
            this.Controls.Add(this._dateTimeStart);
            this.Controls.Add(this.selectTunnelSimple1);
            this.Name = "QueryConditions";
            this.Size = new System.Drawing.Size(817, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _dateTimeEnd;
        private System.Windows.Forms.DateTimePicker _dateTimeStart;
        private SelectTunnelSimple selectTunnelSimple1;
    }
}
