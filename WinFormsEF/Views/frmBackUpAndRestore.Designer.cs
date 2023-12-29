namespace WinFormsEF.Views
{
    partial class frmBackUpAndRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackUpAndRestore));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdCreateBackup = new System.Windows.Forms.Button();
            this.cmdSelectExportFile = new System.Windows.Forms.Button();
            this.txtBackUpDir = new System.Windows.Forms.TextBox();
            this.lblBackupDirectory = new System.Windows.Forms.Label();
            this.lblBackUpBeforeRestore = new System.Windows.Forms.Label();
            this.chkBackUpBeforeRestore = new System.Windows.Forms.CheckBox();
            this.cmdRestoreBackUp = new System.Windows.Forms.Button();
            this.cmdSelectRestoreFile = new System.Windows.Forms.Button();
            this.txtRestoreFile = new System.Windows.Forms.TextBox();
            this.lblRestoreFile = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseExport});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbCloseExport
            // 
            resources.ApplyResources(this.tsbCloseExport, "tsbCloseExport");
            this.tsbCloseExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCloseExport.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.tsbCloseExport.Name = "tsbCloseExport";
            this.tsbCloseExport.Click += new System.EventHandler(this.tsbCloseExport_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Name = "statusStrip1";
            // 
            // cmdCreateBackup
            // 
            resources.ApplyResources(this.cmdCreateBackup, "cmdCreateBackup");
            this.cmdCreateBackup.Name = "cmdCreateBackup";
            this.cmdCreateBackup.UseVisualStyleBackColor = true;
            this.cmdCreateBackup.Click += new System.EventHandler(this.cmdCreateBackup_Click);
            // 
            // cmdSelectExportFile
            // 
            resources.ApplyResources(this.cmdSelectExportFile, "cmdSelectExportFile");
            this.cmdSelectExportFile.Image = global::WinFormsUI.Properties.Resources.open_file_folder_icon;
            this.cmdSelectExportFile.Name = "cmdSelectExportFile";
            this.cmdSelectExportFile.UseVisualStyleBackColor = true;
            this.cmdSelectExportFile.Click += new System.EventHandler(this.cmdSelectExportFile_Click);
            // 
            // txtBackUpDir
            // 
            resources.ApplyResources(this.txtBackUpDir, "txtBackUpDir");
            this.txtBackUpDir.Name = "txtBackUpDir";
            this.txtBackUpDir.Tag = "BackUpDir";
            // 
            // lblBackupDirectory
            // 
            resources.ApplyResources(this.lblBackupDirectory, "lblBackupDirectory");
            this.lblBackupDirectory.Name = "lblBackupDirectory";
            // 
            // lblBackUpBeforeRestore
            // 
            resources.ApplyResources(this.lblBackUpBeforeRestore, "lblBackUpBeforeRestore");
            this.lblBackUpBeforeRestore.Name = "lblBackUpBeforeRestore";
            // 
            // chkBackUpBeforeRestore
            // 
            resources.ApplyResources(this.chkBackUpBeforeRestore, "chkBackUpBeforeRestore");
            this.chkBackUpBeforeRestore.Checked = true;
            this.chkBackUpBeforeRestore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackUpBeforeRestore.Name = "chkBackUpBeforeRestore";
            this.chkBackUpBeforeRestore.UseVisualStyleBackColor = true;
            // 
            // cmdRestoreBackUp
            // 
            resources.ApplyResources(this.cmdRestoreBackUp, "cmdRestoreBackUp");
            this.cmdRestoreBackUp.Name = "cmdRestoreBackUp";
            this.cmdRestoreBackUp.UseVisualStyleBackColor = true;
            this.cmdRestoreBackUp.Click += new System.EventHandler(this.cmdRestoreBackUp_Click);
            // 
            // cmdSelectRestoreFile
            // 
            resources.ApplyResources(this.cmdSelectRestoreFile, "cmdSelectRestoreFile");
            this.cmdSelectRestoreFile.Image = global::WinFormsUI.Properties.Resources.open_file_icon;
            this.cmdSelectRestoreFile.Name = "cmdSelectRestoreFile";
            this.cmdSelectRestoreFile.UseVisualStyleBackColor = true;
            this.cmdSelectRestoreFile.Click += new System.EventHandler(this.cmdSelectRestoreFile_Click);
            // 
            // txtRestoreFile
            // 
            resources.ApplyResources(this.txtRestoreFile, "txtRestoreFile");
            this.txtRestoreFile.Name = "txtRestoreFile";
            // 
            // lblRestoreFile
            // 
            resources.ApplyResources(this.lblRestoreFile, "lblRestoreFile");
            this.lblRestoreFile.Name = "lblRestoreFile";
            // 
            // frmBackUpAndRestore
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBackUpBeforeRestore);
            this.Controls.Add(this.chkBackUpBeforeRestore);
            this.Controls.Add(this.cmdRestoreBackUp);
            this.Controls.Add(this.cmdSelectRestoreFile);
            this.Controls.Add(this.txtRestoreFile);
            this.Controls.Add(this.lblRestoreFile);
            this.Controls.Add(this.cmdCreateBackup);
            this.Controls.Add(this.cmdSelectExportFile);
            this.Controls.Add(this.txtBackUpDir);
            this.Controls.Add(this.lblBackupDirectory);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmBackUpAndRestore";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmBackUpAndRestore_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbCloseExport;
        private StatusStrip statusStrip1;
        private Button cmdCreateBackup;
        private Button cmdSelectExportFile;
        private TextBox txtBackUpDir;
        private Label lblBackupDirectory;
        private Label lblBackUpBeforeRestore;
        private CheckBox chkBackUpBeforeRestore;
        private Button cmdRestoreBackUp;
        private Button cmdSelectRestoreFile;
        private TextBox txtRestoreFile;
        private Label lblRestoreFile;
    }
}