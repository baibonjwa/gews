namespace LibCommonControl
{
    partial class FarpointFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNotFitColor = new System.Windows.Forms.Label();
            this.btnNotFitColor = new System.Windows.Forms.Button();
            this.lblFitColor = new System.Windows.Forms.Label();
            this.btnFitColor = new System.Windows.Forms.Button();
            this.chkHideUnfiltered = new System.Windows.Forms.CheckBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.clrDlg = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // lblNotFitColor
            // 
            this.lblNotFitColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNotFitColor.AutoSize = true;
            this.lblNotFitColor.Location = new System.Drawing.Point(267, 5);
            this.lblNotFitColor.Name = "lblNotFitColor";
            this.lblNotFitColor.Size = new System.Drawing.Size(113, 12);
            this.lblNotFitColor.TabIndex = 35;
            this.lblNotFitColor.Text = "不符合条件规则颜色";
            // 
            // btnNotFitColor
            // 
            this.btnNotFitColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNotFitColor.BackColor = System.Drawing.Color.White;
            this.btnNotFitColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotFitColor.Location = new System.Drawing.Point(386, 5);
            this.btnNotFitColor.Name = "btnNotFitColor";
            this.btnNotFitColor.Size = new System.Drawing.Size(12, 12);
            this.btnNotFitColor.TabIndex = 2;
            this.btnNotFitColor.Text = "btnNotFitColor";
            this.btnNotFitColor.UseVisualStyleBackColor = false;
            this.btnNotFitColor.Click += new System.EventHandler(this.btnNotFitColor_Click);
            // 
            // lblFitColor
            // 
            this.lblFitColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFitColor.AutoSize = true;
            this.lblFitColor.Location = new System.Drawing.Point(142, 5);
            this.lblFitColor.Name = "lblFitColor";
            this.lblFitColor.Size = new System.Drawing.Size(101, 12);
            this.lblFitColor.TabIndex = 36;
            this.lblFitColor.Text = "符合条件规则颜色";
            // 
            // btnFitColor
            // 
            this.btnFitColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFitColor.BackColor = System.Drawing.Color.LightGreen;
            this.btnFitColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitColor.Location = new System.Drawing.Point(249, 5);
            this.btnFitColor.Name = "btnFitColor";
            this.btnFitColor.Size = new System.Drawing.Size(12, 12);
            this.btnFitColor.TabIndex = 1;
            this.btnFitColor.Text = "btnFitColor";
            this.btnFitColor.UseVisualStyleBackColor = false;
            this.btnFitColor.Click += new System.EventHandler(this.btnFitColor_Click);
            // 
            // chkHideUnfiltered
            // 
            this.chkHideUnfiltered.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkHideUnfiltered.AutoSize = true;
            this.chkHideUnfiltered.Checked = true;
            this.chkHideUnfiltered.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideUnfiltered.Location = new System.Drawing.Point(4, 4);
            this.chkHideUnfiltered.Name = "chkHideUnfiltered";
            this.chkHideUnfiltered.Size = new System.Drawing.Size(132, 16);
            this.chkHideUnfiltered.TabIndex = 0;
            this.chkHideUnfiltered.Text = "隐藏不符合条件规则";
            this.chkHideUnfiltered.UseVisualStyleBackColor = true;
            this.chkHideUnfiltered.CheckedChanged += new System.EventHandler(this.chkHideUnfiltered_CheckedChanged);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.Location = new System.Drawing.Point(404, 0);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(91, 23);
            this.btnClearFilter.TabIndex = 3;
            this.btnClearFilter.Text = "清空过滤条件";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // FarpointFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.lblNotFitColor);
            this.Controls.Add(this.btnNotFitColor);
            this.Controls.Add(this.lblFitColor);
            this.Controls.Add(this.btnFitColor);
            this.Controls.Add(this.chkHideUnfiltered);
            this.Name = "FarpointFilter";
            this.Size = new System.Drawing.Size(499, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNotFitColor;
        private System.Windows.Forms.Button btnNotFitColor;
        private System.Windows.Forms.Label lblFitColor;
        private System.Windows.Forms.Button btnFitColor;
        private System.Windows.Forms.CheckBox chkHideUnfiltered;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.ColorDialog clrDlg;
    }
}
