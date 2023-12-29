namespace WinFormsEF.ucControls
{
    partial class ucDatePredefined
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDatePredefined));
            toolStrip1 = new ToolStrip();
            tsbAddPredefinedPeriods = new ToolStripButton();
            tsbSavePredefinedPeriod = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDeletePredefinedPeriods = new ToolStripButton();
            tsbRefeshPredefinedPeriods = new ToolStripButton();
            bsPreDefinedPeriodDates = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            dgPredefinedPeriodDates = new DataGridView();
            gbPeriodDate = new GroupBox();
            cboTarifGroup = new ComboBox();
            bsTarifGroups = new BindingSource(components);
            label2 = new Label();
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            label1 = new Label();
            lblEndDate = new Label();
            dtEndDate = new DateTimePicker();
            lblStartDate = new Label();
            dtStartDate = new DateTimePicker();
            startDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            energyTypeNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tarifGroupNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriodDates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgPredefinedPeriodDates).BeginInit();
            gbPeriodDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAddPredefinedPeriods, tsbSavePredefinedPeriod, tbsCancel, tsbDeletePredefinedPeriods, tsbRefeshPredefinedPeriods });
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
            // bsPreDefinedPeriodDates
            // 
            bsPreDefinedPeriodDates.DataSource = typeof(EnergyUse.Models.PreDefinedPeriodDate);
            // 
            // dgPredefinedPeriodDates
            // 
            dgPredefinedPeriodDates.AllowUserToAddRows = false;
            dgPredefinedPeriodDates.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgPredefinedPeriodDates, "dgPredefinedPeriodDates");
            dgPredefinedPeriodDates.AutoGenerateColumns = false;
            dgPredefinedPeriodDates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgPredefinedPeriodDates.Columns.AddRange(new DataGridViewColumn[] { startDateDataGridViewTextBoxColumn, endDateDataGridViewTextBoxColumn, energyTypeNameDataGridViewTextBoxColumn, tarifGroupNameDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            dgPredefinedPeriodDates.DataSource = bsPreDefinedPeriodDates;
            dgPredefinedPeriodDates.Name = "dgPredefinedPeriodDates";
            dgPredefinedPeriodDates.RowTemplate.Height = 29;
            // 
            // gbPeriodDate
            // 
            resources.ApplyResources(gbPeriodDate, "gbPeriodDate");
            gbPeriodDate.Controls.Add(cboTarifGroup);
            gbPeriodDate.Controls.Add(label2);
            gbPeriodDate.Controls.Add(cboEnergyType);
            gbPeriodDate.Controls.Add(label1);
            gbPeriodDate.Controls.Add(lblEndDate);
            gbPeriodDate.Controls.Add(dtEndDate);
            gbPeriodDate.Controls.Add(lblStartDate);
            gbPeriodDate.Controls.Add(dtStartDate);
            gbPeriodDate.Name = "gbPeriodDate";
            gbPeriodDate.TabStop = false;
            // 
            // cboTarifGroup
            // 
            cboTarifGroup.DataBindings.Add(new Binding("SelectedValue", bsPreDefinedPeriodDates, "TariffGroupId", true));
            cboTarifGroup.DataSource = bsTarifGroups;
            cboTarifGroup.DisplayMember = "Description";
            cboTarifGroup.FormattingEnabled = true;
            resources.ApplyResources(cboTarifGroup, "cboTarifGroup");
            cboTarifGroup.Name = "cboTarifGroup";
            cboTarifGroup.ValueMember = "Id";
            // 
            // bsTarifGroups
            // 
            bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataBindings.Add(new Binding("SelectedValue", bsPreDefinedPeriodDates, "EnergyTypeId", true));
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // lblEndDate
            // 
            resources.ApplyResources(lblEndDate, "lblEndDate");
            lblEndDate.Name = "lblEndDate";
            // 
            // dtEndDate
            // 
            dtEndDate.DataBindings.Add(new Binding("Text", bsPreDefinedPeriodDates, "EndDate", true));
            dtEndDate.DataBindings.Add(new Binding("Value", bsPreDefinedPeriodDates, "EndDate", true));
            dtEndDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtEndDate, "dtEndDate");
            dtEndDate.Name = "dtEndDate";
            // 
            // lblStartDate
            // 
            resources.ApplyResources(lblStartDate, "lblStartDate");
            lblStartDate.Name = "lblStartDate";
            // 
            // dtStartDate
            // 
            dtStartDate.DataBindings.Add(new Binding("Value", bsPreDefinedPeriodDates, "StartDate", true));
            dtStartDate.DataBindings.Add(new Binding("Text", bsPreDefinedPeriodDates, "StartDate", true));
            dtStartDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtStartDate, "dtStartDate");
            dtStartDate.Name = "dtStartDate";
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
            // energyTypeNameDataGridViewTextBoxColumn
            // 
            energyTypeNameDataGridViewTextBoxColumn.DataPropertyName = "EnergyTypeName";
            resources.ApplyResources(energyTypeNameDataGridViewTextBoxColumn, "energyTypeNameDataGridViewTextBoxColumn");
            energyTypeNameDataGridViewTextBoxColumn.Name = "energyTypeNameDataGridViewTextBoxColumn";
            energyTypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifGroupNameDataGridViewTextBoxColumn
            // 
            tarifGroupNameDataGridViewTextBoxColumn.DataPropertyName = "TarifGroupName";
            resources.ApplyResources(tarifGroupNameDataGridViewTextBoxColumn, "tarifGroupNameDataGridViewTextBoxColumn");
            tarifGroupNameDataGridViewTextBoxColumn.Name = "tarifGroupNameDataGridViewTextBoxColumn";
            tarifGroupNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // ucDatePredefined
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbPeriodDate);
            Controls.Add(dgPredefinedPeriodDates);
            Controls.Add(toolStrip1);
            Name = "ucDatePredefined";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriodDates).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgPredefinedPeriodDates).EndInit();
            gbPeriodDate.ResumeLayout(false);
            gbPeriodDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAddPredefinedPeriods;
        private ToolStripButton tsbSavePredefinedPeriod;
        private ToolStripButton tsbDeletePredefinedPeriods;
        private ToolStripButton tsbRefeshPredefinedPeriods;
        private BindingSource bsPreDefinedPeriodDates;
        private ToolTip toolTip1;
        private BindingSource bsEnergyTypes;
        private DataGridView dgPredefinedPeriodDates;
        private GroupBox gbPeriodDate;
        private Label lblEndDate;
        private DateTimePicker dtEndDate;
        private Label lblStartDate;
        private DateTimePicker dtStartDate;
        private BindingSource bsTarifGroups;
        private Label label1;
        private ComboBox cboEnergyType;
        private ComboBox cboTarifGroup;
        private Label label2;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn startDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tarifGroupNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}
