namespace LibPanels
{
    partial class VentilationInfoEntering
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
            this.gbxNoWindArea = new System.Windows.Forms.GroupBox();
            this.rbtnNoWindN = new System.Windows.Forms.RadioButton();
            this.rbtnNoWindY = new System.Windows.Forms.RadioButton();
            this.gbxLightWindArea = new System.Windows.Forms.GroupBox();
            this.rbtnLightWindN = new System.Windows.Forms.RadioButton();
            this.rbtnLightWindY = new System.Windows.Forms.RadioButton();
            this.gbxReturnWindArea = new System.Windows.Forms.GroupBox();
            this.rbtnReturnWindN = new System.Windows.Forms.RadioButton();
            this.rbtnReturnWindY = new System.Windows.Forms.RadioButton();
            this.gbxSmallThanSection = new System.Windows.Forms.GroupBox();
            this.rbtnSmallN = new System.Windows.Forms.RadioButton();
            this.rbtnSmallY = new System.Windows.Forms.RadioButton();
            this.gbxNotFollowRule = new System.Windows.Forms.GroupBox();
            this.rbtnFollowRuleN = new System.Windows.Forms.RadioButton();
            this.rbtnFollowRuleY = new System.Windows.Forms.RadioButton();
            this.lblFaultageArea = new System.Windows.Forms.Label();
            this.lblAirFlow = new System.Windows.Forms.Label();
            this.txtFaultageArea = new System.Windows.Forms.TextBox();
            this.txtAirFlow = new System.Windows.Forms.TextBox();
            this.gbxNoWindArea.SuspendLayout();
            this.gbxLightWindArea.SuspendLayout();
            this.gbxReturnWindArea.SuspendLayout();
            this.gbxSmallThanSection.SuspendLayout();
            this.gbxNotFollowRule.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxNoWindArea
            // 
            this.gbxNoWindArea.Controls.Add(this.rbtnNoWindN);
            this.gbxNoWindArea.Controls.Add(this.rbtnNoWindY);
            this.gbxNoWindArea.Location = new System.Drawing.Point(12, 12);
            this.gbxNoWindArea.Name = "gbxNoWindArea";
            this.gbxNoWindArea.Size = new System.Drawing.Size(135, 75);
            this.gbxNoWindArea.TabIndex = 24;
            this.gbxNoWindArea.TabStop = false;
            this.gbxNoWindArea.Text = "是否有停风区域";
            // 
            // rbtnNoWindN
            // 
            this.rbtnNoWindN.Checked = true;
            this.rbtnNoWindN.Location = new System.Drawing.Point(6, 45);
            this.rbtnNoWindN.Name = "rbtnNoWindN";
            this.rbtnNoWindN.Size = new System.Drawing.Size(35, 16);
            this.rbtnNoWindN.TabIndex = 1;
            this.rbtnNoWindN.TabStop = true;
            this.rbtnNoWindN.Text = "否";
            this.rbtnNoWindN.UseVisualStyleBackColor = true;
            this.rbtnNoWindN.CheckedChanged += new System.EventHandler(this.rbtnNoWindN_CheckedChanged);
            // 
            // rbtnNoWindY
            // 
            this.rbtnNoWindY.Location = new System.Drawing.Point(6, 20);
            this.rbtnNoWindY.Name = "rbtnNoWindY";
            this.rbtnNoWindY.Size = new System.Drawing.Size(35, 16);
            this.rbtnNoWindY.TabIndex = 0;
            this.rbtnNoWindY.Text = "是";
            this.rbtnNoWindY.UseVisualStyleBackColor = true;
            this.rbtnNoWindY.CheckedChanged += new System.EventHandler(this.rbtnNoWindY_CheckedChanged);
            // 
            // gbxLightWindArea
            // 
            this.gbxLightWindArea.Controls.Add(this.rbtnLightWindN);
            this.gbxLightWindArea.Controls.Add(this.rbtnLightWindY);
            this.gbxLightWindArea.Location = new System.Drawing.Point(153, 12);
            this.gbxLightWindArea.Name = "gbxLightWindArea";
            this.gbxLightWindArea.Size = new System.Drawing.Size(135, 75);
            this.gbxLightWindArea.TabIndex = 25;
            this.gbxLightWindArea.TabStop = false;
            this.gbxLightWindArea.Text = "是否有微风区域";
            // 
            // rbtnLightWindN
            // 
            this.rbtnLightWindN.Checked = true;
            this.rbtnLightWindN.Location = new System.Drawing.Point(6, 45);
            this.rbtnLightWindN.Name = "rbtnLightWindN";
            this.rbtnLightWindN.Size = new System.Drawing.Size(35, 16);
            this.rbtnLightWindN.TabIndex = 3;
            this.rbtnLightWindN.TabStop = true;
            this.rbtnLightWindN.Text = "否";
            this.rbtnLightWindN.UseVisualStyleBackColor = true;
            this.rbtnLightWindN.CheckedChanged += new System.EventHandler(this.rbtnLightWindN_CheckedChanged);
            // 
            // rbtnLightWindY
            // 
            this.rbtnLightWindY.Location = new System.Drawing.Point(6, 20);
            this.rbtnLightWindY.Name = "rbtnLightWindY";
            this.rbtnLightWindY.Size = new System.Drawing.Size(35, 16);
            this.rbtnLightWindY.TabIndex = 2;
            this.rbtnLightWindY.Text = "是";
            this.rbtnLightWindY.UseVisualStyleBackColor = true;
            this.rbtnLightWindY.CheckedChanged += new System.EventHandler(this.rbtnLightWindY_CheckedChanged);
            // 
            // gbxReturnWindArea
            // 
            this.gbxReturnWindArea.Controls.Add(this.rbtnReturnWindN);
            this.gbxReturnWindArea.Controls.Add(this.rbtnReturnWindY);
            this.gbxReturnWindArea.Location = new System.Drawing.Point(294, 12);
            this.gbxReturnWindArea.Name = "gbxReturnWindArea";
            this.gbxReturnWindArea.Size = new System.Drawing.Size(135, 75);
            this.gbxReturnWindArea.TabIndex = 26;
            this.gbxReturnWindArea.TabStop = false;
            this.gbxReturnWindArea.Text = "是否有风流反向区域";
            // 
            // rbtnReturnWindN
            // 
            this.rbtnReturnWindN.Checked = true;
            this.rbtnReturnWindN.Location = new System.Drawing.Point(6, 45);
            this.rbtnReturnWindN.Name = "rbtnReturnWindN";
            this.rbtnReturnWindN.Size = new System.Drawing.Size(35, 16);
            this.rbtnReturnWindN.TabIndex = 3;
            this.rbtnReturnWindN.TabStop = true;
            this.rbtnReturnWindN.Text = "否";
            this.rbtnReturnWindN.UseVisualStyleBackColor = true;
            this.rbtnReturnWindN.CheckedChanged += new System.EventHandler(this.rbtnReturnWindN_CheckedChanged);
            // 
            // rbtnReturnWindY
            // 
            this.rbtnReturnWindY.Location = new System.Drawing.Point(6, 20);
            this.rbtnReturnWindY.Name = "rbtnReturnWindY";
            this.rbtnReturnWindY.Size = new System.Drawing.Size(35, 16);
            this.rbtnReturnWindY.TabIndex = 2;
            this.rbtnReturnWindY.Text = "是";
            this.rbtnReturnWindY.UseVisualStyleBackColor = true;
            this.rbtnReturnWindY.CheckedChanged += new System.EventHandler(this.rbtnReturnWindY_CheckedChanged);
            // 
            // gbxSmallThanSection
            // 
            this.gbxSmallThanSection.Controls.Add(this.rbtnSmallN);
            this.gbxSmallThanSection.Controls.Add(this.rbtnSmallY);
            this.gbxSmallThanSection.Location = new System.Drawing.Point(12, 93);
            this.gbxSmallThanSection.Name = "gbxSmallThanSection";
            this.gbxSmallThanSection.Size = new System.Drawing.Size(135, 75);
            this.gbxSmallThanSection.TabIndex = 27;
            this.gbxSmallThanSection.TabStop = false;
            this.gbxSmallThanSection.Text = "是否通风断面小于设计断面的2/3";
            // 
            // rbtnSmallN
            // 
            this.rbtnSmallN.Checked = true;
            this.rbtnSmallN.Location = new System.Drawing.Point(6, 55);
            this.rbtnSmallN.Name = "rbtnSmallN";
            this.rbtnSmallN.Size = new System.Drawing.Size(35, 16);
            this.rbtnSmallN.TabIndex = 1;
            this.rbtnSmallN.TabStop = true;
            this.rbtnSmallN.Text = "否";
            this.rbtnSmallN.UseVisualStyleBackColor = true;
            this.rbtnSmallN.CheckedChanged += new System.EventHandler(this.rbtnSmallN_CheckedChanged);
            // 
            // rbtnSmallY
            // 
            this.rbtnSmallY.Location = new System.Drawing.Point(6, 30);
            this.rbtnSmallY.Name = "rbtnSmallY";
            this.rbtnSmallY.Size = new System.Drawing.Size(35, 16);
            this.rbtnSmallY.TabIndex = 0;
            this.rbtnSmallY.Text = "是";
            this.rbtnSmallY.UseVisualStyleBackColor = true;
            this.rbtnSmallY.CheckedChanged += new System.EventHandler(this.rbtnSmallY_CheckedChanged);
            // 
            // gbxNotFollowRule
            // 
            this.gbxNotFollowRule.Controls.Add(this.rbtnFollowRuleN);
            this.gbxNotFollowRule.Controls.Add(this.rbtnFollowRuleY);
            this.gbxNotFollowRule.Location = new System.Drawing.Point(153, 93);
            this.gbxNotFollowRule.Name = "gbxNotFollowRule";
            this.gbxNotFollowRule.Size = new System.Drawing.Size(276, 75);
            this.gbxNotFollowRule.TabIndex = 25;
            this.gbxNotFollowRule.TabStop = false;
            this.gbxNotFollowRule.Text = "是否工作面风量低于计划风量，风速与《煤矿安全规程》规定不符";
            // 
            // rbtnFollowRuleN
            // 
            this.rbtnFollowRuleN.Checked = true;
            this.rbtnFollowRuleN.Location = new System.Drawing.Point(6, 55);
            this.rbtnFollowRuleN.Name = "rbtnFollowRuleN";
            this.rbtnFollowRuleN.Size = new System.Drawing.Size(35, 16);
            this.rbtnFollowRuleN.TabIndex = 1;
            this.rbtnFollowRuleN.TabStop = true;
            this.rbtnFollowRuleN.Text = "否";
            this.rbtnFollowRuleN.UseVisualStyleBackColor = true;
            this.rbtnFollowRuleN.CheckedChanged += new System.EventHandler(this.rbtnFollowRuleN_CheckedChanged);
            // 
            // rbtnFollowRuleY
            // 
            this.rbtnFollowRuleY.Location = new System.Drawing.Point(6, 30);
            this.rbtnFollowRuleY.Name = "rbtnFollowRuleY";
            this.rbtnFollowRuleY.Size = new System.Drawing.Size(35, 16);
            this.rbtnFollowRuleY.TabIndex = 0;
            this.rbtnFollowRuleY.Text = "是";
            this.rbtnFollowRuleY.UseVisualStyleBackColor = true;
            this.rbtnFollowRuleY.CheckedChanged += new System.EventHandler(this.rbtnFollowRuleY_CheckedChanged);
            // 
            // lblFaultageArea
            // 
            this.lblFaultageArea.AutoSize = true;
            this.lblFaultageArea.Location = new System.Drawing.Point(20, 189);
            this.lblFaultageArea.Name = "lblFaultageArea";
            this.lblFaultageArea.Size = new System.Drawing.Size(53, 12);
            this.lblFaultageArea.TabIndex = 28;
            this.lblFaultageArea.Text = "通风断面";
            // 
            // lblAirFlow
            // 
            this.lblAirFlow.AutoSize = true;
            this.lblAirFlow.Location = new System.Drawing.Point(198, 189);
            this.lblAirFlow.Name = "lblAirFlow";
            this.lblAirFlow.Size = new System.Drawing.Size(29, 12);
            this.lblAirFlow.TabIndex = 29;
            this.lblAirFlow.Text = "风量";
            // 
            // txtFaultageArea
            // 
            this.txtFaultageArea.Location = new System.Drawing.Point(79, 186);
            this.txtFaultageArea.MaxLength = 18;
            this.txtFaultageArea.Name = "txtFaultageArea";
            this.txtFaultageArea.Size = new System.Drawing.Size(100, 21);
            this.txtFaultageArea.TabIndex = 30;
            this.txtFaultageArea.TextChanged += new System.EventHandler(this.txtFaultageArea_TextChanged);
            // 
            // txtAirFlow
            // 
            this.txtAirFlow.Location = new System.Drawing.Point(235, 186);
            this.txtAirFlow.MaxLength = 18;
            this.txtAirFlow.Name = "txtAirFlow";
            this.txtAirFlow.Size = new System.Drawing.Size(100, 21);
            this.txtAirFlow.TabIndex = 31;
            this.txtAirFlow.TextChanged += new System.EventHandler(this.txtAirFlow_TextChanged);
            // 
            // VentilationInfoEntering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 228);
            this.Controls.Add(this.txtAirFlow);
            this.Controls.Add(this.txtFaultageArea);
            this.Controls.Add(this.lblAirFlow);
            this.Controls.Add(this.lblFaultageArea);
            this.Controls.Add(this.gbxNotFollowRule);
            this.Controls.Add(this.gbxSmallThanSection);
            this.Controls.Add(this.gbxReturnWindArea);
            this.Controls.Add(this.gbxLightWindArea);
            this.Controls.Add(this.gbxNoWindArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VentilationInfoEntering";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通风信息";
            this.gbxNoWindArea.ResumeLayout(false);
            this.gbxLightWindArea.ResumeLayout(false);
            this.gbxReturnWindArea.ResumeLayout(false);
            this.gbxSmallThanSection.ResumeLayout(false);
            this.gbxNotFollowRule.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxNoWindArea;
        private System.Windows.Forms.RadioButton rbtnNoWindN;
        private System.Windows.Forms.RadioButton rbtnNoWindY;
        private System.Windows.Forms.GroupBox gbxLightWindArea;
        private System.Windows.Forms.RadioButton rbtnLightWindN;
        private System.Windows.Forms.RadioButton rbtnLightWindY;
        private System.Windows.Forms.GroupBox gbxReturnWindArea;
        private System.Windows.Forms.RadioButton rbtnReturnWindN;
        private System.Windows.Forms.RadioButton rbtnReturnWindY;
        private System.Windows.Forms.GroupBox gbxSmallThanSection;
        private System.Windows.Forms.RadioButton rbtnSmallN;
        private System.Windows.Forms.RadioButton rbtnSmallY;
        private System.Windows.Forms.GroupBox gbxNotFollowRule;
        private System.Windows.Forms.RadioButton rbtnFollowRuleN;
        private System.Windows.Forms.RadioButton rbtnFollowRuleY;
        private System.Windows.Forms.Label lblFaultageArea;
        private System.Windows.Forms.Label lblAirFlow;
        private System.Windows.Forms.TextBox txtFaultageArea;
        private System.Windows.Forms.TextBox txtAirFlow;

    }
}