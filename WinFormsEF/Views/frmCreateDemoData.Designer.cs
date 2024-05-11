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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateDemoData));
            toolStrip1 = new ToolStrip();
            tsbClose = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            cmdCreateDemoData = new Button();
            label7 = new Label();
            label6 = new Label();
            txtMaxRandomRange = new TextBox();
            lblMinRangePerc = new Label();
            txtMinRandomRange = new TextBox();
            label5 = new Label();
            lblRandomizerHelp = new Label();
            lblEndDay = new Label();
            dtpEndDay = new DateTimePicker();
            lblStartDay = new Label();
            dtpStartDay = new DateTimePicker();
            cboEnergyType = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            cboTargetAddress = new ComboBox();
            label2 = new Label();
            cboSourceAddress = new ComboBox();
            label1 = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbClose });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // cmdCreateDemoData
            // 
            resources.ApplyResources(cmdCreateDemoData, "cmdCreateDemoData");
            cmdCreateDemoData.Name = "cmdCreateDemoData";
            toolTip1.SetToolTip(cmdCreateDemoData, resources.GetString("cmdCreateDemoData.ToolTip"));
            cmdCreateDemoData.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // txtMaxRandomRange
            // 
            resources.ApplyResources(txtMaxRandomRange, "txtMaxRandomRange");
            txtMaxRandomRange.Name = "txtMaxRandomRange";
            // 
            // lblMinRangePerc
            // 
            resources.ApplyResources(lblMinRangePerc, "lblMinRangePerc");
            lblMinRangePerc.Name = "lblMinRangePerc";
            // 
            // txtMinRandomRange
            // 
            resources.ApplyResources(txtMinRandomRange, "txtMinRandomRange");
            txtMinRandomRange.Name = "txtMinRandomRange";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // lblRandomizerHelp
            // 
            resources.ApplyResources(lblRandomizerHelp, "lblRandomizerHelp");
            lblRandomizerHelp.Name = "lblRandomizerHelp";
            // 
            // lblEndDay
            // 
            resources.ApplyResources(lblEndDay, "lblEndDay");
            lblEndDay.Name = "lblEndDay";
            // 
            // dtpEndDay
            // 
            dtpEndDay.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpEndDay, "dtpEndDay");
            dtpEndDay.Name = "dtpEndDay";
            dtpEndDay.Value = new DateTime(2022, 1, 15, 0, 0, 0, 0);
            // 
            // lblStartDay
            // 
            resources.ApplyResources(lblStartDay, "lblStartDay");
            lblStartDay.Name = "lblStartDay";
            // 
            // dtpStartDay
            // 
            dtpStartDay.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpStartDay, "dtpStartDay");
            dtpStartDay.Name = "dtpStartDay";
            dtpStartDay.Value = new DateTime(2022, 1, 15, 0, 0, 0, 0);
            // 
            // cboEnergyType
            // 
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // cboTargetAddress
            // 
            cboTargetAddress.FormattingEnabled = true;
            resources.ApplyResources(cboTargetAddress, "cboTargetAddress");
            cboTargetAddress.Name = "cboTargetAddress";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // cboSourceAddress
            // 
            cboSourceAddress.FormattingEnabled = true;
            resources.ApplyResources(cboSourceAddress, "cboSourceAddress");
            cboSourceAddress.Name = "cboSourceAddress";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // frmCreateDemoData
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cmdCreateDemoData);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtMaxRandomRange);
            Controls.Add(lblMinRangePerc);
            Controls.Add(txtMinRandomRange);
            Controls.Add(label5);
            Controls.Add(lblRandomizerHelp);
            Controls.Add(lblEndDay);
            Controls.Add(dtpEndDay);
            Controls.Add(lblStartDay);
            Controls.Add(dtpStartDay);
            Controls.Add(cboEnergyType);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cboTargetAddress);
            Controls.Add(label2);
            Controls.Add(cboSourceAddress);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmCreateDemoData";
            ShowIcon = false;
            ShowInTaskbar = false;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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