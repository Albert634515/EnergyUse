namespace WinFormsEF.ucControls
{
    partial class ucImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucImport));
            toolTip1 = new ToolTip(components);
            CmdSelectImportFile = new Button();
            TxtImportFile = new TextBox();
            LblImportFile = new Label();
            DgImportRows = new DataGridView();
            RegistrationDate = new DataGridViewTextBoxColumn();
            RateNormal = new DataGridViewTextBoxColumn();
            DeltaNormal = new DataGridViewTextBoxColumn();
            RateLow = new DataGridViewTextBoxColumn();
            DeltaLow = new DataGridViewTextBoxColumn();
            ReturnDeliveryLow = new DataGridViewTextBoxColumn();
            ReturnDeliveryDeltaLow = new DataGridViewTextBoxColumn();
            ReturnDeliveryNormal = new DataGridViewTextBoxColumn();
            ReturnDeliveryDeltaNormal = new DataGridViewTextBoxColumn();
            RecordId = new DataGridViewTextBoxColumn();
            bsMeterReading = new BindingSource(components);
            ToolStrip1 = new ToolStrip();
            TsbImport = new ToolStripButton();
            TsbRecalculate = new ToolStripButton();
            TbsSave = new ToolStripButton();
            bsEnergyTypes = new BindingSource(components);
            bsAddresses = new BindingSource(components);
            LblMeter = new Label();
            CboMeters = new ComboBox();
            bsMeter = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)DgImportRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsMeterReading).BeginInit();
            ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsMeter).BeginInit();
            SuspendLayout();
            // 
            // CmdSelectImportFile
            // 
            CmdSelectImportFile.Image = WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(CmdSelectImportFile, "CmdSelectImportFile");
            CmdSelectImportFile.Name = "CmdSelectImportFile";
            CmdSelectImportFile.UseVisualStyleBackColor = true;
            CmdSelectImportFile.Click += cmdSelectImportFile_Click;
            // 
            // TxtImportFile
            // 
            resources.ApplyResources(TxtImportFile, "TxtImportFile");
            TxtImportFile.Name = "TxtImportFile";
            TxtImportFile.Tag = "LastImportFile";
            // 
            // LblImportFile
            // 
            resources.ApplyResources(LblImportFile, "LblImportFile");
            LblImportFile.Name = "LblImportFile";
            // 
            // DgImportRows
            // 
            DgImportRows.AllowUserToAddRows = false;
            DgImportRows.AllowUserToDeleteRows = false;
            resources.ApplyResources(DgImportRows, "DgImportRows");
            DgImportRows.AutoGenerateColumns = false;
            DgImportRows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgImportRows.Columns.AddRange(new DataGridViewColumn[] { RegistrationDate, RateNormal, DeltaNormal, RateLow, DeltaLow, ReturnDeliveryLow, ReturnDeliveryDeltaLow, ReturnDeliveryNormal, ReturnDeliveryDeltaNormal, RecordId });
            DgImportRows.DataSource = bsMeterReading;
            DgImportRows.Name = "DgImportRows";
            DgImportRows.CellFormatting += DgvImportRows_CellFormatting;
            // 
            // RegistrationDate
            // 
            RegistrationDate.DataPropertyName = "RegistrationDate";
            resources.ApplyResources(RegistrationDate, "RegistrationDate");
            RegistrationDate.Name = "RegistrationDate";
            // 
            // RateNormal
            // 
            RateNormal.DataPropertyName = "RateNormal";
            resources.ApplyResources(RateNormal, "RateNormal");
            RateNormal.Name = "RateNormal";
            // 
            // DeltaNormal
            // 
            DeltaNormal.DataPropertyName = "DeltaNormal";
            resources.ApplyResources(DeltaNormal, "DeltaNormal");
            DeltaNormal.Name = "DeltaNormal";
            // 
            // RateLow
            // 
            RateLow.DataPropertyName = "RateLow";
            resources.ApplyResources(RateLow, "RateLow");
            RateLow.Name = "RateLow";
            // 
            // DeltaLow
            // 
            DeltaLow.DataPropertyName = "DeltaLow";
            resources.ApplyResources(DeltaLow, "DeltaLow");
            DeltaLow.Name = "DeltaLow";
            // 
            // ReturnDeliveryLow
            // 
            ReturnDeliveryLow.DataPropertyName = "ReturnDeliveryLow";
            resources.ApplyResources(ReturnDeliveryLow, "ReturnDeliveryLow");
            ReturnDeliveryLow.Name = "ReturnDeliveryLow";
            // 
            // ReturnDeliveryDeltaLow
            // 
            ReturnDeliveryDeltaLow.DataPropertyName = "ReturnDeliveryDeltaLow";
            resources.ApplyResources(ReturnDeliveryDeltaLow, "ReturnDeliveryDeltaLow");
            ReturnDeliveryDeltaLow.Name = "ReturnDeliveryDeltaLow";
            // 
            // ReturnDeliveryNormal
            // 
            ReturnDeliveryNormal.DataPropertyName = "ReturnDeliveryNormal";
            resources.ApplyResources(ReturnDeliveryNormal, "ReturnDeliveryNormal");
            ReturnDeliveryNormal.Name = "ReturnDeliveryNormal";
            // 
            // ReturnDeliveryDeltaNormal
            // 
            ReturnDeliveryDeltaNormal.DataPropertyName = "ReturnDeliveryDeltaNormal";
            resources.ApplyResources(ReturnDeliveryDeltaNormal, "ReturnDeliveryDeltaNormal");
            ReturnDeliveryDeltaNormal.Name = "ReturnDeliveryDeltaNormal";
            // 
            // RecordId
            // 
            RecordId.DataPropertyName = "Id";
            resources.ApplyResources(RecordId, "RecordId");
            RecordId.Name = "RecordId";
            // 
            // bsMeterReading
            // 
            bsMeterReading.DataSource = typeof(EnergyUse.Models.MeterReading);
            // 
            // ToolStrip1
            // 
            ToolStrip1.ImageScalingSize = new Size(20, 20);
            ToolStrip1.Items.AddRange(new ToolStripItem[] { TsbImport, TsbRecalculate, TbsSave });
            resources.ApplyResources(ToolStrip1, "ToolStrip1");
            ToolStrip1.Name = "ToolStrip1";
            // 
            // TsbImport
            // 
            TsbImport.Image = WinFormsUI.Properties.Resources.download_24x24;
            resources.ApplyResources(TsbImport, "TsbImport");
            TsbImport.Name = "TsbImport";
            TsbImport.Click += tsbImport_Click;
            // 
            // TsbRecalculate
            // 
            TsbRecalculate.Image = WinFormsUI.Properties.Resources.settings;
            resources.ApplyResources(TsbRecalculate, "TsbRecalculate");
            TsbRecalculate.Name = "TsbRecalculate";
            TsbRecalculate.Click += tsbRecalculate_Click;
            // 
            // TbsSave
            // 
            TbsSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(TbsSave, "TbsSave");
            TbsSave.Name = "TbsSave";
            TbsSave.Click += tbsSave_Click;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // LblMeter
            // 
            resources.ApplyResources(LblMeter, "LblMeter");
            LblMeter.Name = "LblMeter";
            // 
            // CboMeters
            // 
            CboMeters.DataSource = bsMeter;
            CboMeters.DisplayMember = "Description";
            CboMeters.FormattingEnabled = true;
            resources.ApplyResources(CboMeters, "CboMeters");
            CboMeters.Name = "CboMeters";
            CboMeters.ValueMember = "Id";
            // 
            // bsMeter
            // 
            bsMeter.DataSource = typeof(EnergyUse.Models.Meter);
            // 
            // ucImport
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ToolStrip1);
            Controls.Add(DgImportRows);
            Controls.Add(LblMeter);
            Controls.Add(CboMeters);
            Controls.Add(CmdSelectImportFile);
            Controls.Add(TxtImportFile);
            Controls.Add(LblImportFile);
            Name = "ucImport";
            ((System.ComponentModel.ISupportInitialize)DgImportRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsMeterReading).EndInit();
            ToolStrip1.ResumeLayout(false);
            ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsMeter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip1;
        private BindingSource bsEnergyTypes;
        private BindingSource bsMeterReading;
        private BindingSource bsAddresses;
        private Button CmdSelectImportFile;
        private TextBox TxtImportFile;
        private Label LblImportFile;
        private DataGridView DgImportRows;
        private ToolStrip ToolStrip1;
        private ToolStripButton TsbImport;
        private ToolStripButton TsbRecalculate;
        private ToolStripButton TbsSave;
        private DataGridViewTextBoxColumn RegistrationDate;
        private DataGridViewTextBoxColumn RateNormal;
        private DataGridViewTextBoxColumn DeltaNormal;
        private DataGridViewTextBoxColumn RateLow;
        private DataGridViewTextBoxColumn DeltaLow;
        private DataGridViewTextBoxColumn ReturnDeliveryLow;
        private DataGridViewTextBoxColumn ReturnDeliveryDeltaLow;
        private DataGridViewTextBoxColumn ReturnDeliveryNormal;
        private DataGridViewTextBoxColumn ReturnDeliveryDeltaNormal;
        private DataGridViewTextBoxColumn RecordId;
        private Label LblMeter;
        private ComboBox CboMeters;
        private BindingSource bsMeter;
    }
}
