namespace LibCommonForm
{
    partial class DXSkinSetting
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
            this.cboSkinNames = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboSkinNames
            // 
            this.cboSkinNames.FormattingEnabled = true;
            this.cboSkinNames.Location = new System.Drawing.Point(103, 27);
            this.cboSkinNames.Name = "cboSkinNames";
            this.cboSkinNames.Size = new System.Drawing.Size(121, 20);
            this.cboSkinNames.TabIndex = 0;
            this.cboSkinNames.SelectedIndexChanged += new System.EventHandler(this.cboSkinNames_SelectedIndexChanged);
            // 
            // DXSkinSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cboSkinNames);
            this.Name = "DXSkinSetting";
            this.Text = "皮肤设置";
            this.Load += new System.EventHandler(this.DXSkinSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboSkinNames;
    }
}