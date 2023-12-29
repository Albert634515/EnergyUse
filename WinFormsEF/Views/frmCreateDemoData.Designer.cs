namespace WinFormsEF.Views
{
    partial class frmCreateDemoData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateDemoData));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdCreateDemoData = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaxRandomRange = new System.Windows.Forms.TextBox();
            this.lblMinRangePerc = new System.Windows.Forms.Label();
            this.txtMinRandomRange = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRandomizerHelp = new System.Windows.Forms.Label();
            this.lblEndDay = new System.Windows.Forms.Label();
            this.dtpEndDay = new System.Windows.Forms.DateTimePicker();
            this.lblStartDay = new System.Windows.Forms.Label();
            this.dtpStartDay = new System.Windows.Forms.DateTimePicker();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTargetAddress = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSourceAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // cmdCreateDemoData
            // 
            resources.ApplyResources(this.cmdCreateDemoData, "cmdCreateDemoData");
            this.cmdCreateDemoData.Name = "cmdCreateDemoData";
            this.toolTip1.SetToolTip(this.cmdCreateDemoData, resources.GetString("cmdCreateDemoData.ToolTip"));
            this.cmdCreateDemoData.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtMaxRandomRange
            // 
            resources.ApplyResources(this.txtMaxRandomRange, "txtMaxRandomRange");
            this.txtMaxRandomRange.Name = "txtMaxRandomRange";
            // 
            // lblMinRangePerc
            // 
            resources.ApplyResources(this.lblMinRangePerc, "lblMinRangePerc");
            this.lblMinRangePerc.Name = "lblMinRangePerc";
            // 
            // txtMinRandomRange
            // 
            resources.ApplyResources(this.txtMinRandomRange, "txtMinRandomRange");
            this.txtMinRandomRange.Name = "txtMinRandomRange";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblRandomizerHelp
            // 
            resources.ApplyResources(this.lblRandomizerHelp, "lblRandomizerHelp");
            this.lblRandomizerHelp.Name = "lblRandomizerHelp";
            // 
            // lblEndDay
            // 
            resources.ApplyResources(this.lblEndDay, "lblEndDay");
            this.lblEndDay.Name = "lblEndDay";
            // 
            // dtpEndDay
            // 
            this.dtpEndDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpEndDay, "dtpEndDay");
            this.dtpEndDay.Name = "dtpEndDay";
            this.dtpEndDay.Value = new System.DateTime(2022, 1, 15, 0, 0, 0, 0);
            // 
            // lblStartDay
            // 
            resources.ApplyResources(this.lblStartDay, "lblStartDay");
            this.lblStartDay.Name = "lblStartDay";
            // 
            // dtpStartDay
            // 
            this.dtpStartDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpStartDay, "dtpStartDay");
            this.dtpStartDay.Name = "dtpStartDay";
            this.dtpStartDay.Value = new System.DateTime(2022, 1, 15, 0, 0, 0, 0);
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboTargetAddress
            // 
            this.cboTargetAddress.FormattingEnabled = true;
            resources.ApplyResources(this.cboTargetAddress, "cboTargetAddress");
            this.cboTargetAddress.Name = "cboTargetAddress";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cboSourceAddress
            // 
            this.cboSourceAddress.FormattingEnabled = true;
            resources.ApplyResources(this.cboSourceAddress, "cboSourceAddress");
            this.cboSourceAddress.Name = "cboSourceAddress";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // frmCreateDemoData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdCreateDemoData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMaxRandomRange);
            this.Controls.Add(this.lblMinRangePerc);
            this.Controls.Add(this.txtMinRandomRange);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRandomizerHelp);
            this.Controls.Add(this.lblEndDay);
            this.Controls.Add(this.dtpEndDay);
            this.Controls.Add(this.lblStartDay);
            this.Controls.Add(this.dtpStartDay);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboTargetAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSourceAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCreateDemoData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbClose;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private Label label7;
        private Label label6;
        private TextBox txtMaxRandomRange;
        private Label lblMinRangePerc;
        private TextBox txtMinRandomRange;
        private Label label5;
        private Label lblRandomizerHelp;
        private Label lblEndDay;
        private DateTimePicker dtpEndDay;
        private Label lblStartDay;
        private DateTimePicker dtpStartDay;
        private ComboBox cboEnergyType;
        private Label label4;
        private Label label3;
        private ComboBox cboTargetAddress;
        private Label label2;
        private ComboBox cboSourceAddress;
        private Label label1;
        private Button cmdCreateDemoData;
    }
}