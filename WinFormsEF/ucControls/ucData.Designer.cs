namespace WinFormsEF.ucControls
{
    partial class ucData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucData));
            toolTip1 = new ToolTip(components);
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbEdit = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            lblTill = new Label();
            dtpTill = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            lblFrom = new Label();
            lbl = new Label();
            pnAccumulative = new Panel();
            rbAccumulativeNo = new RadioButton();
            rbAccumulativeYes = new RadioButton();
            dgMeterReadings = new DataGridView();
            RegistrationDate = new DataGridViewTextBoxColumn();
            RateNormal = new DataGridViewTextBoxColumn();
            RateLow = new DataGridViewTextBoxColumn();
            ReturnDeliveryNormal = new DataGridViewTextBoxColumn();
            ReturnDeliveryLow = new DataGridViewTextBoxColumn();
            DeltaNormal = new DataGridViewTextBoxColumn();
            DeltaLow = new DataGridViewTextBoxColumn();
            ReturnDeliveryDeltaNormal = new DataGridViewTextBoxColumn();
            ReturnDeliveryDeltaLow = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bsMeterReadings = new BindingSource(components);
            bsEnergyTypes = new BindingSource(components);
            toolStrip1.SuspendLayout();
            pnAccumulative.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgMeterReadings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsMeterReadings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbEdit, tsbDelete, tsbRefresh });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAdd
            // 
            tsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAdd, "tsbAdd");
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Click += tsbAdd_Click;
            // 
            // tsbEdit
            // 
            tsbEdit.Image = WinFormsUI.Properties.Resources.editing_24x24;
            resources.ApplyResources(tsbEdit, "tsbEdit");
            tsbEdit.Name = "tsbEdit";
            tsbEdit.Click += tsbEdit_Click;
            // 
            // tsbDelete
            // 
            tsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDelete, "tsbDelete");
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Click += tsbDelete_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefresh, "tsbRefresh");
            tsbRefresh.Name = "tsbRefresh";
            tsbRefresh.Click += tsbRefresh_Click;
            // 
            // lblTill
            // 
            resources.ApplyResources(lblTill, "lblTill");
            lblTill.Name = "lblTill";
            // 
            // dtpTill
            // 
            dtpTill.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpTill, "dtpTill");
            dtpTill.Name = "dtpTill";
            dtpTill.ValueChanged += dtpTill_ValueChanged;
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ValueChanged += dtpFrom_ValueChanged;
            // 
            // lblFrom
            // 
            resources.ApplyResources(lblFrom, "lblFrom");
            lblFrom.Name = "lblFrom";
            // 
            // lbl
            // 
            resources.ApplyResources(lbl, "lbl");
            lbl.Name = "lbl";
            // 
            // pnAccumulative
            // 
            pnAccumulative.Controls.Add(rbAccumulativeNo);
            pnAccumulative.Controls.Add(rbAccumulativeYes);
            resources.ApplyResources(pnAccumulative, "pnAccumulative");
            pnAccumulative.Name = "pnAccumulative";
            // 
            // rbAccumulativeNo
            // 
            resources.ApplyResources(rbAccumulativeNo, "rbAccumulativeNo");
            rbAccumulativeNo.Checked = true;
            rbAccumulativeNo.Name = "rbAccumulativeNo";
            rbAccumulativeNo.TabStop = true;
            rbAccumulativeNo.UseVisualStyleBackColor = true;
            rbAccumulativeNo.CheckedChanged += rbAccumulativeNo_CheckedChanged;
            // 
            // rbAccumulativeYes
            // 
            resources.ApplyResources(rbAccumulativeYes, "rbAccumulativeYes");
            rbAccumulativeYes.Name = "rbAccumulativeYes";
            rbAccumulativeYes.UseVisualStyleBackColor = true;
            rbAccumulativeYes.CheckedChanged += rbAccumulativeYes_CheckedChanged;
            // 
            // dgMeterReadings
            // 
            dgMeterReadings.AllowUserToAddRows = false;
            dgMeterReadings.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgMeterReadings, "dgMeterReadings");
            dgMeterReadings.AutoGenerateColumns = false;
            dgMeterReadings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgMeterReadings.Columns.AddRange(new DataGridViewColumn[] { RegistrationDate, RateNormal, RateLow, ReturnDeliveryNormal, ReturnDeliveryLow, DeltaNormal, DeltaLow, ReturnDeliveryDeltaNormal, ReturnDeliveryDeltaLow, idDataGridViewTextBoxColumn });
            dgMeterReadings.DataSource = bsMeterReadings;
            dgMeterReadings.Name = "dgMeterReadings";
            dgMeterReadings.RowTemplate.Height = 24;
            dgMeterReadings.CellClick += dgvMeterReadings_CellClick;
            dgMeterReadings.ColumnWidthChanged += dgvMeterReadings_ColumnWidthChanged;
            dgMeterReadings.RowsAdded += dgvMeterReadings_RowsAdded;
            dgMeterReadings.Scroll += dgvMeterReadings_Scroll;
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
            // RateLow
            // 
            RateLow.DataPropertyName = "RateLow";
            resources.ApplyResources(RateLow, "RateLow");
            RateLow.Name = "RateLow";
            // 
            // ReturnDeliveryNormal
            // 
            ReturnDeliveryNormal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ReturnDeliveryNormal.DataPropertyName = "ReturnDeliveryNormal";
            resources.ApplyResources(ReturnDeliveryNormal, "ReturnDeliveryNormal");
            ReturnDeliveryNormal.Name = "ReturnDeliveryNormal";
            // 
            // ReturnDeliveryLow
            // 
            ReturnDeliveryLow.DataPropertyName = "ReturnDeliveryLow";
            resources.ApplyResources(ReturnDeliveryLow, "ReturnDeliveryLow");
            ReturnDeliveryLow.Name = "ReturnDeliveryLow";
            // 
            // DeltaNormal
            // 
            DeltaNormal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DeltaNormal.DataPropertyName = "DeltaNormal";
            resources.ApplyResources(DeltaNormal, "DeltaNormal");
            DeltaNormal.Name = "DeltaNormal";
            // 
            // DeltaLow
            // 
            DeltaLow.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DeltaLow.DataPropertyName = "DeltaLow";
            resources.ApplyResources(DeltaLow, "DeltaLow");
            DeltaLow.Name = "DeltaLow";
            // 
            // ReturnDeliveryDeltaNormal
            // 
            ReturnDeliveryDeltaNormal.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ReturnDeliveryDeltaNormal.DataPropertyName = "ReturnDeliveryDeltaNormal";
            resources.ApplyResources(ReturnDeliveryDeltaNormal, "ReturnDeliveryDeltaNormal");
            ReturnDeliveryDeltaNormal.Name = "ReturnDeliveryDeltaNormal";
            // 
            // ReturnDeliveryDeltaLow
            // 
            ReturnDeliveryDeltaLow.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ReturnDeliveryDeltaLow.DataPropertyName = "ReturnDeliveryDeltaLow";
            resources.ApplyResources(ReturnDeliveryDeltaLow, "ReturnDeliveryDeltaLow");
            ReturnDeliveryDeltaLow.Name = "ReturnDeliveryDeltaLow";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // bsMeterReadings
            // 
            bsMeterReadings.DataSource = typeof(EnergyUse.Models.MeterReading);
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // ucData
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgMeterReadings);
            Controls.Add(lblTill);
            Controls.Add(dtpTill);
            Controls.Add(dtpFrom);
            Controls.Add(lblFrom);
            Controls.Add(lbl);
            Controls.Add(pnAccumulative);
            Controls.Add(toolStrip1);
            Name = "ucData";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            pnAccumulative.ResumeLayout(false);
            pnAccumulative.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgMeterReadings).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsMeterReadings).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbEdit;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbRefresh;
        private BindingSource bsMeterReadings;
        private BindingSource bsEnergyTypes;
        private Label lblTill;
        private DateTimePicker dtpTill;
        private DateTimePicker dtpFrom;
        private Label lblFrom;
        private Label lbl;
        private Panel pnAccumulative;
        private RadioButton rbAccumulativeNo;
        private RadioButton rbAccumulativeYes;
        private DataGridView dgMeterReadings;
        private DataGridViewTextBoxColumn RegistrationDate;
        private DataGridViewTextBoxColumn RateNormal;
        private DataGridViewTextBoxColumn RateLow;
        private DataGridViewTextBoxColumn ReturnDeliveryNormal;
        private DataGridViewTextBoxColumn ReturnDeliveryLow;
        private DataGridViewTextBoxColumn DeltaNormal;
        private DataGridViewTextBoxColumn DeltaLow;
        private DataGridViewTextBoxColumn ReturnDeliveryDeltaNormal;
        private DataGridViewTextBoxColumn ReturnDeliveryDeltaLow;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}
