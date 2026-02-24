using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using System.IO;
using System.Windows.Forms;
using WpfUI.ViewModels;

namespace WpfApp.ViewModels
{
    public class BackupAndRestoreViewModel : ViewModelBase
    {
        private readonly BackUpAndRestoreController _controller;
        private readonly IDialogService _dialogService;

        public BackupAndRestoreViewModel(IDialogService dialogService)
        {
            _controller = new BackUpAndRestoreController(WpfUI.Managers.Config.GetDbFileName());
            _controller.Initialize();
            _dialogService = dialogService;

            LoadInitialSettings();

            SelectBackupDirCommand = new RelayCommand(_ => SelectBackupDir());
            CreateBackupCommand = new RelayCommand(_ => CreateBackup());
            SelectRestoreFileCommand = new RelayCommand(_ => SelectRestoreFile());
            RestoreBackupCommand = new RelayCommand(_ => RestoreBackup());
        }

        #region Properties

        private string _backupDirectory;
        public string BackupDirectory
        {
            get => _backupDirectory;
            set { _backupDirectory = value; OnPropertyChanged(); }
        }

        private string _restoreFile;
        public string RestoreFile
        {
            get => _restoreFile;
            set { _restoreFile = value; OnPropertyChanged(); }
        }

        private bool _backupBeforeRestore = true;
        public bool BackupBeforeRestore
        {
            get => _backupBeforeRestore;
            set { _backupBeforeRestore = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public RelayCommand SelectBackupDirCommand { get; }
        public RelayCommand CreateBackupCommand { get; }
        public RelayCommand SelectRestoreFileCommand { get; }
        public RelayCommand RestoreBackupCommand { get; }

        #endregion

        #region Methods

        private void LoadInitialSettings()
        {
            var setting = _controller.getSettingBackUpDir("BackUpDir");
            if (!string.IsNullOrWhiteSpace(setting))
                BackupDirectory = setting;
        }

        private void SelectBackupDir()
        {
            var dlg = new FolderBrowserDialog();
            var setting = _controller.getSettingBackUpDir("BackUpDirectory");

            if (!string.IsNullOrWhiteSpace(setting))
                dlg.SelectedPath = setting;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                BackupDirectory = dlg.SelectedPath;
                _controller.SaveSetting("BackUpDir", dlg.SelectedPath);
            }
        }

        private void CreateBackup()
        {
            if (string.IsNullOrWhiteSpace(BackupDirectory))
            {
                System.Windows.MessageBox.Show("No backup directory selected");
                return;
            }

            var sourceFile = _controller.GetSourceDbFile();
            _controller.CreateBackUpFile(BackupDirectory, sourceFile);

            _dialogService.Show("Backup created", "Backup created");
        }

        private void SelectRestoreFile()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Database files|*.db",
                Title = WpfUI.Managers.Languages.GetResourceString("BackUpAndRestoreRestoreFile", "Restore db file")
            };

            var setting = _controller.getSettingBackUpDir("RestoreDirectory");
            if (!string.IsNullOrWhiteSpace(setting))
                dlg.InitialDirectory = setting;

            if (dlg.ShowDialog() == true)
                RestoreFile = dlg.FileName;
        }

        private void RestoreBackup()
        {
            if (BackupBeforeRestore)
                CreateBackup();

            if (string.IsNullOrWhiteSpace(RestoreFile))
            {
                _dialogService.Show("No restore file selected", "No restore file");
                return;
            }

            if (!File.Exists(RestoreFile))
            {
                System.Windows.MessageBox.Show("Restore file does not exist");
                return;
            }

            // TODO: implement restore logic
        }

        #endregion
    }
}