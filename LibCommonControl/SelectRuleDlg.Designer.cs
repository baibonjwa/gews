namespace LibCommonControl
{
    partial class SelectRuleDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectRuleDlg));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbRuleType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbWarningType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbWarningLevel = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(783, 322);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "规则类别";
            // 
            // cbbRuleType
            // 
            this.cbbRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRuleType.FormattingEnabled = true;
            this.cbbRuleType.Items.AddRange(new object[] {
            "所有",
            "瓦斯",
            "煤层赋存",
            "地质构造",
            "通风",
            "管理因素",
            "其他"});
            this.cbbRuleType.Location = new System.Drawing.Point(98, 21);
            this.cbbRuleType.Name = "cbbRuleType";
            this.cbbRuleType.Size = new System.Drawing.Size(121, 20);
            this.cbbRuleType.TabIndex = 2;
            this.cbbRuleType.SelectedIndexChanged += new System.EventHandler(this.cbbRuleType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "预警类型";
            // 
            // cbbWarningType
            // 
            this.cbbWarningType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWarningType.FormattingEnabled = true;
            this.cbbWarningType.Items.AddRange(new object[] {
            "所有",
            "超限预警",
            "突出预警"});
            this.cbbWarningType.Location = new System.Drawing.Point(314, 22);
            this.cbbWarningType.Name = "cbbWarningType";
            this.cbbWarningType.Size = new System.Drawing.Size(121, 20);
            this.cbbWarningType.TabIndex = 2;
            this.cbbWarningType.SelectedIndexChanged += new System.EventHandler(this.cbbWarningType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(456, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "预警级别";
            // 
            // cbbWarningLevel
            // 
            this.cbbWarningLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWarningLevel.FormattingEnabled = true;
            this.cbbWarningLevel.Items.AddRange(new object[] {
            "所有",
            "黄色预警",
            "红色预警"});
            this.cbbWarningLevel.Location = new System.Drawing.Point(530, 24);
            this.cbbWarningLevel.Name = "cbbWarningLevel";
            this.cbbWarningLevel.Size = new System.Drawing.Size(121, 20);
            this.cbbWarningLevel.TabIndex = 2;
            this.cbbWarningLevel.SelectedIndexChanged += new System.EventHandler(this.cbbWarningLevel_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(705, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectRuleDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 395);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbbWarningLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbWarningType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbRuleType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectRuleDlg";
            this.Text = "选择规则";
            this.Load += new System.EventHandler(this.SelectRuleDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbRuleType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbWarningType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbWarningLevel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnCancel;
    }
}