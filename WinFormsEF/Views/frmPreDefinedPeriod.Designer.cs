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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreDefinedPeriod));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddPredefinedPeriods = new System.Windows.Forms.ToolStripButton();
            this.tsbSavePredefinedPeriod = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDeletePredefinedPeriods = new System.Windows.Forms.ToolStripButton();
            this.tsbRefeshPredefinedPeriods = new System.Windows.Forms.ToolStripButton();
            this.tbsClosePredefinedPeriods = new System.Windows.Forms.ToolStripButton();
            this.bsPreDefinedPeriod = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgPeriods = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.ucDatePredefined1 = new WinFormsEF.ucControls.ucDatePredefined();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPreDefinedPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriods)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddPredefinedPeriods,
            this.tsbSavePredefinedPeriod,
            this.tbsCancel,
            this.tsbDeletePredefinedPeriods,
            this.tsbRefeshPredefinedPeriods,
            this.tbsClosePredefinedPeriods});
            this.toolStrip1.Name = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // tsbAddPredefinedPeriods
            // 
            resources.ApplyResources(this.tsbAddPredefinedPeriods, "tsbAddPredefinedPeriods");
            this.tsbAddPredefinedPeriods.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            this.tsbAddPredefinedPeriods.Name = "tsbAddPredefinedPeriods";
            this.tsbAddPredefinedPeriods.Click += new System.EventHandler(this.tsbAddPredefinedPeriods_Click);
            // 
            // tsbSavePredefinedPeriod
            // 
            resources.ApplyResources(this.tsbSavePredefinedPeriod, "tsbSavePredefinedPeriod");
            this.tsbSavePredefinedPeriod.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.tsbSavePredefinedPeriod.Name = "tsbSavePredefinedPeriod";
            this.tsbSavePredefinedPeriod.Click += new System.EventHandler(this.tsbSavePredefinedPeriod_Click);
            // 
            // tbsCancel
            // 
            resources.ApplyResources(this.tbsCancel, "tbsCancel");
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDeletePredefinedPeriods
            // 
            resources.ApplyResources(this.tsbDeletePredefinedPeriods, "tsbDeletePredefinedPeriods");
            this.tsbDeletePredefinedPeriods.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            this.tsbDeletePredefinedPeriods.Name = "tsbDeletePredefinedPeriods";
            this.tsbDeletePredefinedPeriods.Click += new System.EventHandler(this.tsbDeletePredefinedPeriods_Click);
            // 
            // tsbRefeshPredefinedPeriods
            // 
            resources.ApplyResources(this.tsbRefeshPredefinedPeriods, "tsbRefeshPredefinedPeriods");
            this.tsbRefeshPredefinedPeriods.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            this.tsbRefeshPredefinedPeriods.Name = "tsbRefeshPredefinedPeriods";
            this.tsbRefeshPredefinedPeriods.Click += new System.EventHandler(this.tsbRefeshPredefinedPeriods_Click);
            // 
            // tbsClosePredefinedPeriods
            // 
            resources.ApplyResources(this.tbsClosePredefinedPeriods, "tbsClosePredefinedPeriods");
            this.tbsClosePredefinedPeriods.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbsClosePredefinedPeriods.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.tbsClosePredefinedPeriods.Name = "tbsClosePredefinedPeriods";
            this.tbsClosePredefinedPeriods.Click += new System.EventHandler(this.tbsClosePredefinedPeriods_Click);
            // 
            // bsPreDefinedPeriod
            // 
            this.bsPreDefinedPeriod.DataSource = typeof(EnergyUse.Models.PreDefinedPeriod);
            this.bsPreDefinedPeriod.CurrentChanged += new System.EventHandler(this.bsPreDefinedPeriod_CurrentChanged);
            // 
            // dgPeriods
            // 
            resources.ApplyResources(this.dgPeriods, "dgPeriods");
            this.dgPeriods.AllowUserToAddRows = false;
            this.dgPeriods.AllowUserToDeleteRows = false;
            this.dgPeriods.AutoGenerateColumns = false;
            this.dgPeriods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPeriods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.dgPeriods.DataSource = this.bsPreDefinedPeriod;
            this.dgPeriods.Name = "dgPeriods";
            this.dgPeriods.RowTemplate.Height = 29;
            this.toolTip1.SetToolTip(this.dgPeriods, resources.GetString("dgPeriods.ToolTip"));
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // ucDatePredefined1
            // 
            resources.ApplyResources(this.ucDatePredefined1, "ucDatePredefined1");
            this.ucDatePredefined1.Name = "ucDatePredefined1";
            this.toolTip1.SetToolTip(this.ucDatePredefined1, resources.GetString("ucDatePredefined1.ToolTip"));
            // 
            // frmPreDefinedPeriod
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucDatePredefined1);
            this.Controls.Add(this.dgPeriods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmPreDefinedPeriod";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreDefinedPeriod_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPreDefinedPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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