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
            pbEnergyUseLogo = new PictureBox();
            statusStrip1 = new StatusStrip();
            chkHideInfoFormOnStart = new CheckBox();
            lblInfo = new Label();
            cmdClose = new Button();
            ((System.ComponentModel.ISupportInitialize)pbEnergyUseLogo).BeginInit();
            SuspendLayout();
            // 
            // pbEnergyUseLogo
            // 
            pbEnergyUseLogo.Image = WinFormsUI.Properties.Resources.EnergyUse;
            resources.ApplyResources(pbEnergyUseLogo, "pbEnergyUseLogo");
            pbEnergyUseLogo.Name = "pbEnergyUseLogo";
            pbEnergyUseLogo.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // chkHideInfoFormOnStart
            // 
            resources.ApplyResources(chkHideInfoFormOnStart, "chkHideInfoFormOnStart");
            chkHideInfoFormOnStart.Name = "chkHideInfoFormOnStart";
            chkHideInfoFormOnStart.Tag = "HideInfoFormOnStart";
            chkHideInfoFormOnStart.UseVisualStyleBackColor = true;
            chkHideInfoFormOnStart.CheckedChanged += chkHideInfoFormOnStart_CheckedChanged;
            // 
            // lblInfo
            // 
            resources.ApplyResources(lblInfo, "lblInfo");
            lblInfo.Name = "lblInfo";
            // 
            // cmdClose
            // 
            resources.ApplyResources(cmdClose, "cmdClose");
            cmdClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            cmdClose.Name = "cmdClose";
            cmdClose.UseVisualStyleBackColor = true;
            cmdClose.Click += cmdClose_Click;
            // 
            // frmInfo
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbEnergyUseLogo);
            Controls.Add(statusStrip1);
            Controls.Add(chkHideInfoFormOnStart);
            Controls.Add(lblInfo);
            Controls.Add(cmdClose);
            Name = "frmInfo";
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)pbEnergyUseLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbEnergyUseLogo;
        private StatusStrip statusStrip1;
        private CheckBox chkHideInfoFormOnStart;
        private Label lblInfo;
        private Button cmdClose;
    }
}