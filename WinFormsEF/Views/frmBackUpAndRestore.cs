namespace WinFormsEF.Views
{
    public partial class frmBackUpAndRestore : Form
    {
        #region FormProperties

        #endregion

        #region InitForm

        public frmBackUpAndRestore()
        {
            InitializeComponent();
            setBaseFormSettings();
        }

        #endregion

        #region Events

        private void frmBackUpAndRestore_Load(object sender, EventArgs e)
        {
            getSettingBackUpDir();
        }

        #endregion

        #region Toolbar

        private void tsbCloseExport_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region ButtonEvents

        private void cmdSelectExportFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetSetting("BackUpDirectory");
            if (setting != null && setting.Id > 0)
            {
                folderDialog.SelectedPath = setting.KeyValue;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
                txtBackUpDir.Text = folderDialog.SelectedPath;
        }

        private void cmdCreateBackup_Click(object sender, EventArgs e)
        {
            createBackUpFile();
        }

        private void cmdSelectRestoreFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                var setting = libSettings.GetSetting("RestoreDirectory");
                if (setting != null && setting.Id > 0)
                {
                    openFileDialog1.InitialDirectory = setting.KeyValue;
                }

                openFileDialog1.Title = Managers.Languages.GetResourceString("BackUpAndRestoreRestoreFile", "Restore db file");
                openFileDialog1.Filter = "Database files|*.db";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtRestoreFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdRestoreBackUp_Click(object sender, EventArgs e)
        {
            if (chkBackUpBeforeRestore.Checked)
                createBackUpFile();

            restoreBackUpFile();
        }

        #endregion

        #region Methods

        private void createBackUpFile()
        {
            var targetPath = txtBackUpDir.Text.Trim();
            var sourceFile = getSourceDbFile();
            var message = "";

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                message = Managers.Languages.GetResourceString("BackUpAndRestoreSelectFile", "Please first select an export directory");
                MessageBox.Show(this, message);
                return;
            }

            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            var fileName = $"EnergyUse_{DateTime.Now.ToString("yyyyMMddHHmmss")}.db";
            var destFile = Path.Combine(targetPath, fileName);
            File.Copy(sourceFile, destFile, true);

            message = Managers.Languages.GetResourceString("BackUpAndRestoreCreated", "Back-up created");
            MessageBox.Show(this, message);
        }

        private void restoreBackUpFile()
        {
            if (string.IsNullOrWhiteSpace(txtRestoreFile.Text))
            {
                var message = Managers.Languages.GetResourceString("BackUpAndRestoreNoRestoreFileSelected", "No restore file selected");
                MessageBox.Show(this, message);
                return;
            }

            if (!File.Exists(txtRestoreFile.Text))
            {
                var message = Managers.Languages.GetResourceString("BackUpAndRestoreNoRestoreFileExist", "No restore file does not exist");
                MessageBox.Show(this, message);
                return;
            }
        }

        private void getSettingBackUpDir()
        {
            string settingValue;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetSetting(txtBackUpDir.Tag.ToString());
            if ((setting == null || (setting != null && string.IsNullOrWhiteSpace(setting.KeyValue))) || !Directory.Exists(setting.KeyValue))
                settingValue = getDefaultBackUpDir();
            else
                settingValue = setting.KeyValue;

            txtBackUpDir.Text = settingValue;
        }

        private string getSourceDbFile()
        {
            string dbFile;

            dbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "EnergyUse.db");

            return dbFile;
        }

        private string getDefaultBackUpDir()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackUp");
        }

        private void setBaseFormSettings()
        {
            Managers.Settings.SetBaseFormSettings(this);
        }

        #endregion
    }
}