namespace WinFormsEF.ucControls
{
    partial class ucDateSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDateSelection));
            bsEngeryTypes = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            cboEnergyType = new ComboBox();
            lblRange = new Label();
            dtpFrom = new DateTimePicker();
            dtpTill = new DateTimePicker();
            lblEnergyType = new Label();
            cmdRemove = new Button();
            cboTarifGroups = new ComboBox();
            bsTarifGroups = new BindingSource(components);
            label1 = new Label();
            bindingSource1 = new BindingSource(components);
            toolTip2 = new ToolTip(components);
            bindingSource2 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)bsEngeryTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).BeginInit();
            SuspendLayout();
            // 
            // bsEngeryTypes
            // 
            bsEngeryTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataSource = bsEngeryTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            // 
            // lblRange
            // 
            resources.ApplyResources(lblRange, "lblRange");
            lblRange.Name = "lblRange";
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            // 
            // dtpTill
            // 
            dtpTill.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpTill, "dtpTill");
            dtpTill.Name = "dtpTill";
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // cmdRemove
            // 
            cmdRemove.Image = WinFormsUI.Properties.Resources.remove_24x24;
            resources.ApplyResources(cmdRemove, "cmdRemove");
            cmdRemove.Name = "cmdRemove";
            toolTip2.SetToolTip(cmdRemove, resources.GetString("cmdRemove.ToolTip"));
            toolTip1.SetToolTip(cmdRemove, resources.GetString("cmdRemove.ToolTip1"));
            cmdRemove.UseVisualStyleBackColor = true;
            // 
            // cboTarifGroups
            // 
            cboTarifGroups.DataSource = bsTarifGroups;
            cboTarifGroups.DisplayMember = "Description";
            cboTarifGroups.FormattingEnabled = true;
            resources.ApplyResources(cboTarifGroups, "cboTarifGroups");
            cboTarifGroups.Name = "cboTarifGroups";
            cboTarifGroups.ValueMember = "Id";
            // 
            // bsTarifGroups
            // 
            bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // ucDateSelection
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cboEnergyType);
            Controls.Add(lblRange);
            Controls.Add(dtpFrom);
            Controls.Add(dtpTill);
            Controls.Add(lblEnergyType);
            Controls.Add(cboTarifGroups);
            Controls.Add(cmdRemove);
            Controls.Add(label1);
            Name = "ucDateSelection";
            ((System.ComponentModel.ISupportInitialize)bsEngeryTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bsEngeryTypes;
        private ToolTip toolTip1;
        private BindingSource bsTarifGroups;
        public ComboBox cboEnergyType;
        private BindingSource bindingSource1;
        private Label lblRange;
        public DateTimePicker dtpFrom;
        public DateTimePicker dtpTill;
        private Label lblEnergyType;
        private ToolTip toolTip2;
        internal Button cmdRemove;
        public ComboBox cboTarifGroups;
        private BindingSource bindingSource2;
        private Label label1;
    }
}
