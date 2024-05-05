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
            addButton = new Button();
            clearPeriodButton = new Button();
            statusStrip1 = new StatusStrip();
            lblAddress = new Label();
            ChkpredictMissingData = new CheckBox();
            ChkShowCalculatedData = new CheckBox();
            cancelParameterSelectionButton = new Button();
            selectButton = new Button();
            preSelectedPeriodComboBox = new ComboBox();
            label1 = new Label();
            addressComboBox = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // addButton
            // 
            addButton.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(addButton, "addButton");
            addButton.Name = "addButton";
            toolTip1.SetToolTip(addButton, resources.GetString("addButton.ToolTip"));
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += cmdAdd_Click;
            // 
            // clearPeriodButton
            // 
            clearPeriodButton.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(clearPeriodButton, "clearPeriodButton");
            clearPeriodButton.Name = "clearPeriodButton";
            toolTip1.SetToolTip(clearPeriodButton, resources.GetString("clearPeriodButton.ToolTip"));
            clearPeriodButton.UseVisualStyleBackColor = true;
            clearPeriodButton.Click += clearPeriodButton_Click;
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
            // cancelParameterSelectionButton
            // 
            resources.ApplyResources(cancelParameterSelectionButton, "cancelParameterSelectionButton");
            cancelParameterSelectionButton.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            cancelParameterSelectionButton.Name = "cancelParameterSelectionButton";
            cancelParameterSelectionButton.UseVisualStyleBackColor = true;
            cancelParameterSelectionButton.Click += cmdCancel_Click;
            // 
            // selectButton
            // 
            resources.ApplyResources(selectButton, "selectButton");
            selectButton.Name = "selectButton";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += cmdSelect_Click;
            // 
            // preSelectedPeriodComboBox
            // 
            preSelectedPeriodComboBox.FormattingEnabled = true;
            resources.ApplyResources(preSelectedPeriodComboBox, "preSelectedPeriodComboBox");
            preSelectedPeriodComboBox.Name = "preSelectedPeriodComboBox";
            preSelectedPeriodComboBox.Tag = "LastPreSelectedPeriod";
            preSelectedPeriodComboBox.SelectedIndexChanged += cboPreSelectedPeriods_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // addressComboBox
            // 
            addressComboBox.DataSource = bsAddresses;
            addressComboBox.DisplayMember = "Description";
            addressComboBox.FormattingEnabled = true;
            resources.ApplyResources(addressComboBox, "addressComboBox");
            addressComboBox.Name = "addressComboBox";
            addressComboBox.ValueMember = "Id";
            addressComboBox.SelectedIndexChanged += cmbAddress_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // FrmSelectSettelementParameters
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(clearPeriodButton);
            Controls.Add(addressComboBox);
            Controls.Add(addButton);
            Controls.Add(lblAddress);
            Controls.Add(ChkpredictMissingData);
            Controls.Add(ChkShowCalculatedData);
            Controls.Add(cancelParameterSelectionButton);
            Controls.Add(selectButton);
            Controls.Add(preSelectedPeriodComboBox);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            MaximizeBox = false;
            MinimizeBox = false;
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
        private Button addButton;
        private Label lblAddress;
        public CheckBox ChkpredictMissingData;
        public CheckBox ChkShowCalculatedData;
        private Button cancelParameterSelectionButton;
        private Button selectButton;
        private ComboBox preSelectedPeriodComboBox;
        private Label label1;
        public ComboBox addressComboBox;
        private Button clearPeriodButton;
    }
}