namespace LibCommonForm
{
    partial class UserGroupInformationInput
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
            this.labelLoginName = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this._txtGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._rtxtRemarks = new System.Windows.Forms.RichTextBox();
            this._dgrvdUserInfo = new System.Windows.Forms.DataGridView();
            this._gbxUserInformation = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._lstSelUserName = new System.Windows.Forms.ListBox();
            this._btnSelAdding = new System.Windows.Forms.Button();
            this._btnSelReduce = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdUserInfo)).BeginInit();
            this._gbxUserInformation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLoginName
            // 
            this.labelLoginName.AutoSize = true;
            this.labelLoginName.Location = new System.Drawing.Point(3, 21);
            this.labelLoginName.Name = "labelLoginName";
            this.labelLoginName.Size = new System.Drawing.Size(77, 12);
            this.labelLoginName.TabIndex = 0;
            this.labelLoginName.Text = "用户组名称：";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(298, 355);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(380, 355);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // _txtGroupName
            // 
            this._txtGroupName.Location = new System.Drawing.Point(86, 18);
            this._txtGroupName.MaxLength = 50;
            this._txtGroupName.Name = "_txtGroupName";
            this._txtGroupName.Size = new System.Drawing.Size(369, 21);
            this._txtGroupName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "备      注：";
            // 
            // _rtxtRemarks
            // 
            this._rtxtRemarks.Location = new System.Drawing.Point(86, 42);
            this._rtxtRemarks.Name = "_rtxtRemarks";
            this._rtxtRemarks.Size = new System.Drawing.Size(369, 55);
            this._rtxtRemarks.TabIndex = 1;
            this._rtxtRemarks.Text = "";
            // 
            // _dgrvdUserInfo
            // 
            this._dgrvdUserInfo.AllowUserToAddRows = false;
            this._dgrvdUserInfo.AllowUserToDeleteRows = false;
            this._dgrvdUserInfo.AllowUserToResizeColumns = false;
            this._dgrvdUserInfo.AllowUserToResizeRows = false;
            this._dgrvdUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgrvdUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgrvdUserInfo.Location = new System.Drawing.Point(3, 17);
            this._dgrvdUserInfo.Name = "_dgrvdUserInfo";
            this._dgrvdUserInfo.ReadOnly = true;
            this._dgrvdUserInfo.RowHeadersVisible = false;
            this._dgrvdUserInfo.RowTemplate.Height = 23;
            this._dgrvdUserInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgrvdUserInfo.Size = new System.Drawing.Size(284, 226);
            this._dgrvdUserInfo.TabIndex = 0;
            this._dgrvdUserInfo.SelectionChanged += new System.EventHandler(this._dgrvdUserInfo_SelectionChanged);
            // 
            // _gbxUserInformation
            // 
            this._gbxUserInformation.Controls.Add(this._dgrvdUserInfo);
            this._gbxUserInformation.Location = new System.Drawing.Point(5, 103);
            this._gbxUserInformation.Name = "_gbxUserInformation";
            this._gbxUserInformation.Size = new System.Drawing.Size(290, 246);
            this._gbxUserInformation.TabIndex = 4;
            this._gbxUserInformation.TabStop = false;
            this._gbxUserInformation.Text = "用户列表";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._lstSelUserName);
            this.groupBox1.Location = new System.Drawing.Point(345, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 246);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已选用户";
            // 
            // _lstSelUserName
            // 
            this._lstSelUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lstSelUserName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lstSelUserName.FormattingEnabled = true;
            this._lstSelUserName.ItemHeight = 14;
            this._lstSelUserName.Location = new System.Drawing.Point(3, 17);
            this._lstSelUserName.Name = "_lstSelUserName";
            this._lstSelUserName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._lstSelUserName.Size = new System.Drawing.Size(107, 226);
            this._lstSelUserName.TabIndex = 0;
            this._lstSelUserName.SelectedIndexChanged += new System.EventHandler(this._lstSelUserName_SelectedIndexChanged);
            // 
            // _btnSelAdding
            // 
            this._btnSelAdding.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnSelAdding.Location = new System.Drawing.Point(298, 144);
            this._btnSelAdding.Name = "_btnSelAdding";
            this._btnSelAdding.Size = new System.Drawing.Size(41, 31);
            this._btnSelAdding.TabIndex = 2;
            this._btnSelAdding.Text = ">>>";
            this._btnSelAdding.UseVisualStyleBackColor = true;
            this._btnSelAdding.Click += new System.EventHandler(this._btnSelAdding_Click);
            // 
            // _btnSelReduce
            // 
            this._btnSelReduce.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._btnSelReduce.Location = new System.Drawing.Point(298, 212);
            this._btnSelReduce.Name = "_btnSelReduce";
            this._btnSelReduce.Size = new System.Drawing.Size(41, 31);
            this._btnSelReduce.TabIndex = 3;
            this._btnSelReduce.Text = "<<<";
            this._btnSelReduce.UseVisualStyleBackColor = true;
            this._btnSelReduce.Click += new System.EventHandler(this._btnSelReduce_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(457, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "*";
            // 
            // UserGroupInformationInput
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(470, 387);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnSelReduce);
            this.Controls.Add(this._btnSelAdding);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._gbxUserInformation);
            this.Controls.Add(this._rtxtRemarks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtGroupName);
            this.Controls.Add(this.labelLoginName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserGroupInformationInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户组信息录入";
            this.Load += new System.EventHandler(this.UserGroupInformationInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdUserInfo)).EndInit();
            this._gbxUserInformation.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLoginName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox _txtGroupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox _rtxtRemarks;
        private System.Windows.Forms.DataGridView _dgrvdUserInfo;
        private System.Windows.Forms.GroupBox _gbxUserInformation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox _lstSelUserName;
        private System.Windows.Forms.Button _btnSelAdding;
        private System.Windows.Forms.Button _btnSelReduce;
        private System.Windows.Forms.Label label2;
    }
}