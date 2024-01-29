namespace WinFormsEF.Views
{
    partial class frmNetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNetting));
            toolStrip1 = new ToolStrip();
            tsbAddNetting = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDeleteNetting = new ToolStripButton();
            tsbRefreshNetting = new ToolStripButton();
            tsbCloseNetting = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            dgNetting = new DataGridView();
            bsNetting = new BindingSource(components);
            cboEnergyType = new ComboBox();
            lblEnergyType = new Label();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            startDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            rateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            energyTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgNetting).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsNetting).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAddNetting, tsbSave, tbsCancel, tsbDeleteNetting, tsbRefreshNetting, tsbCloseNetting });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAddNetting
            // 
            tsbAddNetting.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAddNetting, "tsbAddNetting");
            tsbAddNetting.Name = "tsbAddNetting";
            tsbAddNetting.Click += tsbAddNetting_Click;
            // 
            // tsbSave
            // 
            tsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(tsbSave, "tsbSave");
            tsbSave.Name = "tsbSave";
            tsbSave.Click += tsbSave_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(tbsCancel, "tbsCancel");
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDeleteNetting
            // 
            tsbDeleteNetting.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDeleteNetting, "tsbDeleteNetting");
            tsbDeleteNetting.Name = "tsbDeleteNetting";
            tsbDeleteNetting.Click += tsbDeleteNetting_Click;
            // 
            // tsbRefreshNetting
            // 
            tsbRefreshNetting.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefreshNetting, "tsbRefreshNetting");
            tsbRefreshNetting.Name = "tsbRefreshNetting";
            tsbRefreshNetting.Click += tsbRefreshNetting_Click;
            // 
            // tsbCloseNetting
            // 
            tsbCloseNetting.Alignment = ToolStripItemAlignment.Right;
            tsbCloseNetting.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbCloseNetting, "tsbCloseNetting");
            tsbCloseNetting.Name = "tsbCloseNetting";
            tsbCloseNetting.Click += tsbCloseNetting_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // dgNetting
            // 
            dgNetting.AllowUserToAddRows = false;
            dgNetting.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgNetting, "dgNetting");
            dgNetting.AutoGenerateColumns = false;
            dgNetting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgNetting.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, startDateDataGridViewTextBoxColumn, endDateDataGridViewTextBoxColumn, rateDataGridViewTextBoxColumn, energyTypeDataGridViewTextBoxColumn });
            dgNetting.DataSource = bsNetting;
            dgNetting.Name = "dgNetting";
            // 
            // bsNetting
            // 
            bsNetting.DataSource = typeof(EnergyUse.Models.Netting);
            // 
            // cboEnergyType
            // 
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            cboEnergyType.SelectedIndexChanged += cboEnergyType_SelectedIndexChanged;
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // startDateDataGridViewTextBoxColumn
            // 
            startDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate";
            resources.ApplyResources(startDateDataGridViewTextBoxColumn, "startDateDataGridViewTextBoxColumn");
            startDateDataGridViewTextBoxColumn.Name = "startDateDataGridViewTextBoxColumn";
            // 
            // endDateDataGridViewTextBoxColumn
            // 
            endDateDataGridViewTextBoxColumn.DataPropertyName = "EndDate";
            resources.ApplyResources(endDateDataGridViewTextBoxColumn, "endDateDataGridViewTextBoxColumn");
            endDateDataGridViewTextBoxColumn.Name = "endDateDataGridViewTextBoxColumn";
            // 
            // rateDataGridViewTextBoxColumn
            // 
            rateDataGridViewTextBoxColumn.DataPropertyName = "Rate";
            resources.ApplyResources(rateDataGridViewTextBoxColumn, "rateDataGridViewTextBoxColumn");
            rateDataGridViewTextBoxColumn.Name = "rateDataGridViewTextBoxColumn";
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            // 
            // frmNetting
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cboEnergyType);
            Controls.Add(lblEnergyType);
            Controls.Add(dgNetting);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmNetting";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmNetting_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgNetting).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsNetting).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAddNetting;
        private ToolStripButton tsbDeleteNetting;
        private ToolStripButton tsbRefreshNetting;
        private ToolStripButton tsbCloseNetting;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsNetting;
        private ToolStripButton tsbSave;
        private DataGridView dgNetting;
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeDataGridViewTextBoxColumn;
    }
}