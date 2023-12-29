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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDateSelection));
            this.bsEngeryTypes = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bsTarifGroups = new System.Windows.Forms.BindingSource(this.components);
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lblRange = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTill = new System.Windows.Forms.DateTimePicker();
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cboTarifGroups = new System.Windows.Forms.ComboBox();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsEngeryTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTarifGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // bsEngeryTypes
            // 
            this.bsEngeryTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // bsTarifGroups
            // 
            this.bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // cboEnergyType
            // 
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.DataSource = this.bsEngeryTypes;
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            this.cboEnergyType.Name = "cboEnergyType";
            this.toolTip2.SetToolTip(this.cboEnergyType, resources.GetString("cboEnergyType.ToolTip"));
            this.toolTip1.SetToolTip(this.cboEnergyType, resources.GetString("cboEnergyType.ToolTip1"));
            this.cboEnergyType.ValueMember = "Id";
            // 
            // lblRange
            // 
            resources.ApplyResources(this.lblRange, "lblRange");
            this.lblRange.Name = "lblRange";
            this.toolTip1.SetToolTip(this.lblRange, resources.GetString("lblRange.ToolTip"));
            this.toolTip2.SetToolTip(this.lblRange, resources.GetString("lblRange.ToolTip1"));
            // 
            // dtpFrom
            // 
            resources.ApplyResources(this.dtpFrom, "dtpFrom");
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Name = "dtpFrom";
            this.toolTip1.SetToolTip(this.dtpFrom, resources.GetString("dtpFrom.ToolTip"));
            this.toolTip2.SetToolTip(this.dtpFrom, resources.GetString("dtpFrom.ToolTip1"));
            // 
            // dtpTill
            // 
            resources.ApplyResources(this.dtpTill, "dtpTill");
            this.dtpTill.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTill.Name = "dtpTill";
            this.toolTip1.SetToolTip(this.dtpTill, resources.GetString("dtpTill.ToolTip"));
            this.toolTip2.SetToolTip(this.dtpTill, resources.GetString("dtpTill.ToolTip1"));
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            this.toolTip1.SetToolTip(this.lblEnergyType, resources.GetString("lblEnergyType.ToolTip"));
            this.toolTip2.SetToolTip(this.lblEnergyType, resources.GetString("lblEnergyType.ToolTip1"));
            // 
            // cmdRemove
            // 
            resources.ApplyResources(this.cmdRemove, "cmdRemove");
            this.cmdRemove.Image = global::WinFormsUI.Properties.Resources.remove_24x24;
            this.cmdRemove.Name = "cmdRemove";
            this.toolTip1.SetToolTip(this.cmdRemove, resources.GetString("cmdRemove.ToolTip"));
            this.toolTip2.SetToolTip(this.cmdRemove, resources.GetString("cmdRemove.ToolTip1"));
            this.cmdRemove.UseVisualStyleBackColor = true;
            // 
            // cboTarifGroups
            // 
            resources.ApplyResources(this.cboTarifGroups, "cboTarifGroups");
            this.cboTarifGroups.DataSource = this.bsTarifGroups;
            this.cboTarifGroups.DisplayMember = "Description";
            this.cboTarifGroups.FormattingEnabled = true;
            this.cboTarifGroups.Name = "cboTarifGroups";
            this.toolTip2.SetToolTip(this.cboTarifGroups, resources.GetString("cboTarifGroups.ToolTip"));
            this.toolTip1.SetToolTip(this.cboTarifGroups, resources.GetString("cboTarifGroups.ToolTip1"));
            this.cboTarifGroups.ValueMember = "Id";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            this.toolTip2.SetToolTip(this.label1, resources.GetString("label1.ToolTip1"));
            // 
            // ucDateSelection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.dtpTill);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.cboTarifGroups);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.label1);
            this.Name = "ucDateSelection";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.toolTip2.SetToolTip(this, resources.GetString("$this.ToolTip1"));
            ((System.ComponentModel.ISupportInitialize)(this.bsEngeryTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTarifGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
