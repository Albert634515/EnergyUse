using EnergyUse.Core.Controllers;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace WinFormsEF.Views;

public partial class frmBackUpAndRestore : Form
{
    #region FormProperties

    private BackUpAndRestoreController _controller;

    #endregion

    #region InitForm

    public frmBackUpAndRestore()
    {
        _controller = new BackUpAndRestoreController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
    }

    #endregion

    #region Events

    private void frmBackUpAndRestore_Load(object sender, EventArgs e)
    {
        var settingValue = _controller.getSettingBackUpDir(txtBackUpDir.Tag.ToString());
        if (!string.IsNullOrWhiteSpace(settingValue))
            txtBackUpDir.Text = settingValue;
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
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        var setting = _controller.getSettingBackUpDir("BackUpDirectory");
        if (! string.IsNullOrWhiteSpace(setting))
            fbd.SelectedPath = setting;

        if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        {
            txtBackUpDir.Text = fbd.SelectedPath;
            _controller.SaveSetting(txtBackUpDir.Tag.ToString(), fbd.SelectedPath);
        }
    }

    private void cmdCreateBackup_Click(object sender, EventArgs e)
    {
        createBackUpFile(this, txtBackUpDir.Text.Trim());
    }

    private void cmdSelectRestoreFile_Click(object sender, EventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var setting = _controller.getSettingBackUpDir("RestoreDirectory");
            if (!string.IsNullOrWhiteSpace(setting))
                openFileDialog1.InitialDirectory = setting;

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
            createBackUpFile(this, txtBackUpDir.Text.Trim());

        restoreBackUpFile(this, txtRestoreFile.Text);
    }

    #endregion

    #region Methods

    private void createBackUpFile(IWin32Window owner, string backupDir)
    {
        if (string.IsNullOrWhiteSpace(backupDir))
            throw new Exception("No back-up directory");

        var targetPath = backupDir;
        var sourceFile = _controller.GetSourceDbFile();
        string message;

        if (string.IsNullOrWhiteSpace(targetPath))
        {
            message = Managers.Languages.GetResourceString("BackUpAndRestoreSelectFile", "Please first select an export directory");
            MessageBox.Show(owner, message);
            return;
        }

        _controller.CreateBackUpFile(targetPath, sourceFile);

        message = Managers.Languages.GetResourceString("BackUpAndRestoreCreated", "Back-up created");
        MessageBox.Show(this, message);
    }

    private void restoreBackUpFile(IWin32Window owner, string restoreFile)
    {
        if (string.IsNullOrWhiteSpace(restoreFile))
        {
            var message = Managers.Languages.GetResourceString("BackUpAndRestoreNoRestoreFileSelected", "No restore file selected");
            MessageBox.Show(owner, message);
            return;
        }

        if (!File.Exists(restoreFile))
        {
            var message = Managers.Languages.GetResourceString("BackUpAndRestoreNoRestoreFileExist", "No restore file does not exist");
            MessageBox.Show(owner, message);
            return;
        }

        //Todo restore file
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
    }

    #endregion
}