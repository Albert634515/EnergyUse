namespace WinFormsEF.Views
{
    partial class frmPreDefinedPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreDefinedPeriod));
            statusStrip1 = new StatusStrip();
            toolStrip1 = new ToolStrip();
            tsbAddPredefinedPeriods = new ToolStripButton();
            tsbSavePredefinedPeriod = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDeletePredefinedPeriods = new ToolStripButton();
            tsbRefeshPredefinedPeriods = new ToolStripButton();
            tbsClosePredefinedPeriods = new ToolStripButton();
            bsPreDefinedPeriod = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            dgPeriods = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            label1 = new Label();
            ucDatePredefined1 = new ucControls.ucDatePredefined();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriod).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgPeriods).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAddPredefinedPeriods, tsbSavePredefinedPeriod, tbsCancel, tsbDeletePredefinedPeriods, tsbRefeshPredefinedPeriods, tbsClosePredefinedPeriods });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAddPredefinedPeriods
            // 
            tsbAddPredefinedPeriods.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAddPredefinedPeriods, "tsbAddPredefinedPeriods");
            tsbAddPredefinedPeriods.Name = "tsbAddPredefinedPeriods";
            tsbAddPredefinedPeriods.Click += tsbAddPredefinedPeriods_Click;
            // 
            // tsbSavePredefinedPeriod
            // 
            tsbSavePredefinedPeriod.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(tsbSavePredefinedPeriod, "tsbSavePredefinedPeriod");
            tsbSavePredefinedPeriod.Name = "tsbSavePredefinedPeriod";
            tsbSavePredefinedPeriod.Click += tsbSavePredefinedPeriod_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(tbsCancel, "tbsCancel");
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDeletePredefinedPeriods
            // 
            tsbDeletePredefinedPeriods.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDeletePredefinedPeriods, "tsbDeletePredefinedPeriods");
            tsbDeletePredefinedPeriods.Name = "tsbDeletePredefinedPeriods";
            tsbDeletePredefinedPeriods.Click += tsbDeletePredefinedPeriods_Click;
            // 
            // tsbRefeshPredefinedPeriods
            // 
            tsbRefeshPredefinedPeriods.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefeshPredefinedPeriods, "tsbRefeshPredefinedPeriods");
            tsbRefeshPredefinedPeriods.Name = "tsbRefeshPredefinedPeriods";
            tsbRefeshPredefinedPeriods.Click += tsbRefeshPredefinedPeriods_Click;
            // 
            // tbsClosePredefinedPeriods
            // 
            tbsClosePredefinedPeriods.Alignment = ToolStripItemAlignment.Right;
            tbsClosePredefinedPeriods.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tbsClosePredefinedPeriods, "tbsClosePredefinedPeriods");
            tbsClosePredefinedPeriods.Name = "tbsClosePredefinedPeriods";
            tbsClosePredefinedPeriods.Click += tbsClosePredefinedPeriods_Click;
            // 
            // bsPreDefinedPeriod
            // 
            bsPreDefinedPeriod.DataSource = typeof(EnergyUse.Models.PreDefinedPeriod);
            bsPreDefinedPeriod.CurrentChanged += bsPreDefinedPeriod_CurrentChanged;
            // 
            // dgPeriods
            // 
            dgPeriods.AllowUserToAddRows = false;
            dgPeriods.AllowUserToDeleteRows = false;
            dgPeriods.AutoGenerateColumns = false;
            dgPeriods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgPeriods.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn });
            dgPeriods.DataSource = bsPreDefinedPeriod;
            resources.ApplyResources(dgPeriods, "dgPeriods");
            dgPeriods.Name = "dgPeriods";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // ucDatePredefined1
            // 
            resources.ApplyResources(ucDatePredefined1, "ucDatePredefined1");
            ucDatePredefined1.Name = "ucDatePredefined1";
            // 
            // frmPreDefinedPeriod
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucDatePredefined1);
            Controls.Add(dgPeriods);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmPreDefinedPeriod";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmPreDefinedPeriod_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriod).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgPeriods).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbAddPredefinedPeriods;
        private ToolStripButton tsbSavePredefinedPeriod;
        private ToolStripButton tsbDeletePredefinedPeriods;
        private ToolStripButton tsbRefeshPredefinedPeriods;
        private ToolStripButton tbsClosePredefinedPeriods;
        private BindingSource bsPreDefinedPeriod;
        private ToolTip toolTip1;
        private DataGridView dgPeriods;
        private Label label1;
        private ucControls.ucDatePredefined ucDatePredefined1;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}