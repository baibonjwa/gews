namespace LibCommonForm
{
    partial class UserGroupManagementForAddUserIntoGroup
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
            this._dgrvdUserInfo = new System.Windows.Forms.DataGridView();
            this._dgrvdGroupInfo = new System.Windows.Forms.DataGridView();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCacle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._btnRefrsh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdUserInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdGroupInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // _dgrvdUserInfo
            // 
            this._dgrvdUserInfo.AllowUserToAddRows = false;
            this._dgrvdUserInfo.AllowUserToDeleteRows = false;
            this._dgrvdUserInfo.AllowUserToResizeColumns = false;
            this._dgrvdUserInfo.AllowUserToResizeRows = false;
            this._dgrvdUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgrvdUserInfo.Location = new System.Drawing.Point(12, 24);
            this._dgrvdUserInfo.Name = "_dgrvdUserInfo";
            this._dgrvdUserInfo.ReadOnly = true;
            this._dgrvdUserInfo.RowHeadersVisible = false;
            this._dgrvdUserInfo.RowTemplate.Height = 23;
            this._dgrvdUserInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgrvdUserInfo.Size = new System.Drawing.Size(477, 311);
            this._dgrvdUserInfo.TabIndex = 2;
            // 
            // _dgrvdGroupInfo
            // 
            this._dgrvdGroupInfo.AllowUserToAddRows = false;
            this._dgrvdGroupInfo.AllowUserToDeleteRows = false;
            this._dgrvdGroupInfo.AllowUserToResizeColumns = false;
            this._dgrvdGroupInfo.AllowUserToResizeRows = false;
            this._dgrvdGroupInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgrvdGroupInfo.Location = new System.Drawing.Point(495, 24);
            this._dgrvdGroupInfo.MultiSelect = false;
            this._dgrvdGroupInfo.Name = "_dgrvdGroupInfo";
            this._dgrvdGroupInfo.ReadOnly = true;
            this._dgrvdGroupInfo.RowHeadersVisible = false;
            this._dgrvdGroupInfo.RowTemplate.Height = 23;
            this._dgrvdGroupInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgrvdGroupInfo.Size = new System.Drawing.Size(276, 270);
            this._dgrvdGroupInfo.TabIndex = 3;
            // 
            // _btnOK
            // 
            this._btnOK.Location = new System.Drawing.Point(587, 301);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(89, 34);
            this._btnOK.TabIndex = 5;
            this._btnOK.Text = "确定";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
            // 
            // _btnCacle
            // 
            this._btnCacle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCacle.Location = new System.Drawing.Point(682, 301);
            this._btnCacle.Name = "_btnCacle";
            this._btnCacle.Size = new System.Drawing.Size(89, 34);
            this._btnCacle.TabIndex = 6;
            this._btnCacle.Text = "取消";
            this._btnCacle.UseVisualStyleBackColor = true;
            this._btnCacle.Click += new System.EventHandler(this._btnCacle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(493, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户组信息";
            // 
            // _btnRefrsh
            // 
            this._btnRefrsh.Location = new System.Drawing.Point(495, 301);
            this._btnRefrsh.Name = "_btnRefrsh";
            this._btnRefrsh.Size = new System.Drawing.Size(86, 34);
            this._btnRefrsh.TabIndex = 4;
            this._btnRefrsh.Text = "刷新";
            this._btnRefrsh.UseVisualStyleBackColor = true;
            this._btnRefrsh.Click += new System.EventHandler(this._btnRefrsh_Click);
            // 
            // UserGroupManagementForAddUserIntoGroup
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCacle;
            this.ClientSize = new System.Drawing.Size(783, 346);
            this.Controls.Add(this._btnRefrsh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnCacle);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._dgrvdGroupInfo);
            this.Controls.Add(this._dgrvdUserInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserGroupManagementForAddUserIntoGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户组添加用户";
            this.Load += new System.EventHandler(this.UserGroupManagementForAddUserIntoGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdUserInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgrvdGroupInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _dgrvdUserInfo;
        private System.Windows.Forms.DataGridView _dgrvdGroupInfo;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCacle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnRefrsh;
    }
}