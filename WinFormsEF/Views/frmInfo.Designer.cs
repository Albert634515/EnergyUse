namespace WinFormsEF.Views
{
    partial class frmInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
            this.pbEnergyUseLogo = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.chkHideInfoFormOnStart = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnergyUseLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbEnergyUseLogo
            // 
            this.pbEnergyUseLogo.Image = global::WinFormsUI.Properties.Resources.EnergyUse;
            resources.ApplyResources(this.pbEnergyUseLogo, "pbEnergyUseLogo");
            this.pbEnergyUseLogo.Name = "pbEnergyUseLogo";
            this.pbEnergyUseLogo.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // chkHideInfoFormOnStart
            // 
            resources.ApplyResources(this.chkHideInfoFormOnStart, "chkHideInfoFormOnStart");
            this.chkHideInfoFormOnStart.Name = "chkHideInfoFormOnStart";
            this.chkHideInfoFormOnStart.Tag = "HideInfoFormOnStart";
            this.chkHideInfoFormOnStart.UseVisualStyleBackColor = true;
            this.chkHideInfoFormOnStart.CheckedChanged += new System.EventHandler(this.chkHideInfoFormOnStart_CheckedChanged);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbEnergyUseLogo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkHideInfoFormOnStart);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmInfo";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbEnergyUseLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pbEnergyUseLogo;
        private StatusStrip statusStrip1;
        private CheckBox chkHideInfoFormOnStart;
        private Label lblInfo;
        private Button cmdClose;
    }
}