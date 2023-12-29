namespace WinFormsEF.Views
{
    partial class FrmSelectSettelementParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectSettelementParameters));
            bsAddresses = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            BtnAdd = new Button();
            statusStrip1 = new StatusStrip();
            lblAddress = new Label();
            ChkpredictMissingData = new CheckBox();
            ChkShowCalculatedData = new CheckBox();
            BtnCancel = new Button();
            BtnSelect = new Button();
            CboPreSelectedPeriods = new ComboBox();
            label1 = new Label();
            CboAddress = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // BtnAdd
            // 
            BtnAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(BtnAdd, "BtnAdd");
            BtnAdd.Name = "BtnAdd";
            toolTip1.SetToolTip(BtnAdd, resources.GetString("BtnAdd.ToolTip"));
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += cmdAdd_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            // 
            // ChkpredictMissingData
            // 
            resources.ApplyResources(ChkpredictMissingData, "ChkpredictMissingData");
            ChkpredictMissingData.Checked = true;
            ChkpredictMissingData.CheckState = CheckState.Checked;
            ChkpredictMissingData.Name = "ChkpredictMissingData";
            ChkpredictMissingData.UseVisualStyleBackColor = true;
            // 
            // ChkShowCalculatedData
            // 
            resources.ApplyResources(ChkShowCalculatedData, "ChkShowCalculatedData");
            ChkShowCalculatedData.Name = "ChkShowCalculatedData";
            ChkShowCalculatedData.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            resources.ApplyResources(BtnCancel, "BtnCancel");
            BtnCancel.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            BtnCancel.Name = "BtnCancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += cmdCancel_Click;
            // 
            // BtnSelect
            // 
            resources.ApplyResources(BtnSelect, "BtnSelect");
            BtnSelect.Name = "BtnSelect";
            BtnSelect.UseVisualStyleBackColor = true;
            BtnSelect.Click += cmdSelect_Click;
            // 
            // CboPreSelectedPeriods
            // 
            CboPreSelectedPeriods.FormattingEnabled = true;
            resources.ApplyResources(CboPreSelectedPeriods, "CboPreSelectedPeriods");
            CboPreSelectedPeriods.Name = "CboPreSelectedPeriods";
            CboPreSelectedPeriods.Tag = "LastPreSelectedPeriod";
            CboPreSelectedPeriods.SelectedIndexChanged += cboPreSelectedPeriods_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // CboAddress
            // 
            CboAddress.DataSource = bsAddresses;
            CboAddress.DisplayMember = "Description";
            CboAddress.FormattingEnabled = true;
            resources.ApplyResources(CboAddress, "CboAddress");
            CboAddress.Name = "CboAddress";
            CboAddress.ValueMember = "Id";
            CboAddress.SelectedIndexChanged += cmbAddress_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // FrmSelectSettelementParameters
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CboAddress);
            Controls.Add(BtnAdd);
            Controls.Add(lblAddress);
            Controls.Add(ChkpredictMissingData);
            Controls.Add(ChkShowCalculatedData);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSelect);
            Controls.Add(CboPreSelectedPeriods);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Name = "FrmSelectSettelementParameters";
            ShowIcon = false;
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bsAddresses;
        private ToolTip toolTip1;
        private StatusStrip statusStrip1;
        private BindingSource bsEnergyTypes;
        private Button BtnAdd;
        private Label lblAddress;
        public CheckBox ChkpredictMissingData;
        public CheckBox ChkShowCalculatedData;
        private Button BtnCancel;
        private Button BtnSelect;
        private ComboBox CboPreSelectedPeriods;
        private Label label1;
        public ComboBox CboAddress;
    }
}