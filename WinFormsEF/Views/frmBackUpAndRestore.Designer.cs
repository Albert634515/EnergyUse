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
            toolStrip1 = new ToolStrip();
            tsbCloseExport = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            cmdCreateBackup = new Button();
            cmdSelectExportFile = new Button();
            txtBackUpDir = new TextBox();
            lblBackupDirectory = new Label();
            lblBackUpBeforeRestore = new Label();
            chkBackUpBeforeRestore = new CheckBox();
            cmdRestoreBackUp = new Button();
            cmdSelectRestoreFile = new Button();
            txtRestoreFile = new TextBox();
            lblRestoreFile = new Label();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbCloseExport });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbCloseExport
            // 
            tsbCloseExport.Alignment = ToolStripItemAlignment.Right;
            tsbCloseExport.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbCloseExport, "tsbCloseExport");
            tsbCloseExport.Name = "tsbCloseExport";
            tsbCloseExport.Click += tsbCloseExport_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // cmdCreateBackup
            // 
            resources.ApplyResources(cmdCreateBackup, "cmdCreateBackup");
            cmdCreateBackup.Name = "cmdCreateBackup";
            cmdCreateBackup.UseVisualStyleBackColor = true;
            cmdCreateBackup.Click += cmdCreateBackup_Click;
            // 
            // cmdSelectExportFile
            // 
            cmdSelectExportFile.Image = WinFormsUI.Properties.Resources.open_file_folder_icon;
            resources.ApplyResources(cmdSelectExportFile, "cmdSelectExportFile");
            cmdSelectExportFile.Name = "cmdSelectExportFile";
            cmdSelectExportFile.UseVisualStyleBackColor = true;
            cmdSelectExportFile.Click += cmdSelectExportFile_Click;
            // 
            // txtBackUpDir
            // 
            resources.ApplyResources(txtBackUpDir, "txtBackUpDir");
            txtBackUpDir.Name = "txtBackUpDir";
            txtBackUpDir.Tag = "BackUpDir";
            // 
            // lblBackupDirectory
            // 
            resources.ApplyResources(lblBackupDirectory, "lblBackupDirectory");
            lblBackupDirectory.Name = "lblBackupDirectory";
            // 
            // lblBackUpBeforeRestore
            // 
            resources.ApplyResources(lblBackUpBeforeRestore, "lblBackUpBeforeRestore");
            lblBackUpBeforeRestore.Name = "lblBackUpBeforeRestore";
            // 
            // chkBackUpBeforeRestore
            // 
            resources.ApplyResources(chkBackUpBeforeRestore, "chkBackUpBeforeRestore");
            chkBackUpBeforeRestore.Checked = true;
            chkBackUpBeforeRestore.CheckState = CheckState.Checked;
            chkBackUpBeforeRestore.Name = "chkBackUpBeforeRestore";
            chkBackUpBeforeRestore.UseVisualStyleBackColor = true;
            // 
            // cmdRestoreBackUp
            // 
            resources.ApplyResources(cmdRestoreBackUp, "cmdRestoreBackUp");
            cmdRestoreBackUp.Name = "cmdRestoreBackUp";
            cmdRestoreBackUp.UseVisualStyleBackColor = true;
            cmdRestoreBackUp.Click += cmdRestoreBackUp_Click;
            // 
            // cmdSelectRestoreFile
            // 
            cmdSelectRestoreFile.Image = WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(cmdSelectRestoreFile, "cmdSelectRestoreFile");
            cmdSelectRestoreFile.Name = "cmdSelectRestoreFile";
            cmdSelectRestoreFile.UseVisualStyleBackColor = true;
            cmdSelectRestoreFile.Click += cmdSelectRestoreFile_Click;
            // 
            // txtRestoreFile
            // 
            resources.ApplyResources(txtRestoreFile, "txtRestoreFile");
            txtRestoreFile.Name = "txtRestoreFile";
            // 
            // lblRestoreFile
            // 
            resources.ApplyResources(lblRestoreFile, "lblRestoreFile");
            lblRestoreFile.Name = "lblRestoreFile";
            // 
            // frmBackUpAndRestore
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblBackUpBeforeRestore);
            Controls.Add(chkBackUpBeforeRestore);
            Controls.Add(cmdRestoreBackUp);
            Controls.Add(cmdSelectRestoreFile);
            Controls.Add(txtRestoreFile);
            Controls.Add(lblRestoreFile);
            Controls.Add(cmdCreateBackup);
            Controls.Add(cmdSelectExportFile);
            Controls.Add(txtBackUpDir);
            Controls.Add(lblBackupDirectory);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmBackUpAndRestore";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += frmBackUpAndRestore_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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