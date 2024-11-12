namespace WinFormsEF.Views
{
    partial class frmSelectReportParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectReportParameters));
            bsAddresses = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            addButton = new Button();
            clearPeriodButton = new Button();
            statusStrip1 = new StatusStrip();
            lblAddress = new Label();
            predictMissingDataCheckBox = new CheckBox();
            cancelParameterSelectionButton = new Button();
            selectButton = new Button();
            preSelectedPeriodComboBox = new ComboBox();
            label1 = new Label();
            addressComboBox = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            reportComboBox = new ComboBox();
            bsReportTypes = new BindingSource(components);
            label2 = new Label();
            showRatesCheckBox = new CheckBox();
            parametersGroupBox = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsReportTypes).BeginInit();
            parametersGroupBox.SuspendLayout();
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
            // predictMissingDataCheckBox
            // 
            resources.ApplyResources(predictMissingDataCheckBox, "predictMissingDataCheckBox");
            predictMissingDataCheckBox.Checked = true;
            predictMissingDataCheckBox.CheckState = CheckState.Checked;
            predictMissingDataCheckBox.Name = "predictMissingDataCheckBox";
            predictMissingDataCheckBox.UseVisualStyleBackColor = true;
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
            // reportComboBox
            // 
            reportComboBox.DataSource = bsReportTypes;
            reportComboBox.DisplayMember = "Description";
            reportComboBox.FormattingEnabled = true;
            resources.ApplyResources(reportComboBox, "reportComboBox");
            reportComboBox.Name = "reportComboBox";
            reportComboBox.Tag = "SelectedReport";
            reportComboBox.ValueMember = "Id";
            reportComboBox.SelectedIndexChanged += reportComboBox_SelectedIndexChanged;
            // 
            // bsReportTypes
            // 
            bsReportTypes.DataSource = typeof(EnergyUse.Models.Common.SelectionItem);
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // showRatesCheckBox
            // 
            resources.ApplyResources(showRatesCheckBox, "showRatesCheckBox");
            showRatesCheckBox.Checked = true;
            showRatesCheckBox.CheckState = CheckState.Checked;
            showRatesCheckBox.Name = "showRatesCheckBox";
            showRatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // parametersGroupBox
            // 
            resources.ApplyResources(parametersGroupBox, "parametersGroupBox");
            parametersGroupBox.Controls.Add(reportComboBox);
            parametersGroupBox.Controls.Add(addressComboBox);
            parametersGroupBox.Controls.Add(clearPeriodButton);
            parametersGroupBox.Controls.Add(lblAddress);
            parametersGroupBox.Controls.Add(showRatesCheckBox);
            parametersGroupBox.Controls.Add(label2);
            parametersGroupBox.Controls.Add(predictMissingDataCheckBox);
            parametersGroupBox.Controls.Add(preSelectedPeriodComboBox);
            parametersGroupBox.Controls.Add(label1);
            parametersGroupBox.Name = "parametersGroupBox";
            parametersGroupBox.TabStop = false;
            // 
            // frmSelectReportParameters
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(parametersGroupBox);
            Controls.Add(addButton);
            Controls.Add(cancelParameterSelectionButton);
            Controls.Add(selectButton);
            Controls.Add(statusStrip1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSelectReportParameters";
            ShowIcon = false;
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsReportTypes).EndInit();
            parametersGroupBox.ResumeLayout(false);
            parametersGroupBox.PerformLayout();
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
        public CheckBox predictMissingDataCheckBox;
        private Button cancelParameterSelectionButton;
        private Button selectButton;
        private ComboBox preSelectedPeriodComboBox;
        private Label label1;
        public ComboBox addressComboBox;
        private Button clearPeriodButton;
        private ComboBox reportComboBox;
        private Label label2;
        private BindingSource bsReportTypes;
        public CheckBox showRatesCheckBox;
        private GroupBox parametersGroupBox;
    }
}