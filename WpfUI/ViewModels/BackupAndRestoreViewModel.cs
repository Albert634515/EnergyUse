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
            SourceFile = _controller.GetSourceDbFile();

            setInitialSettings();

            SelectBackupDirCommand = new RelayCommand(_ => selectBackupDir());
            CreateBackupCommand = new RelayCommand(_ => createBackup());
            SelectRestoreFileCommand = new RelayCommand(_ => selectRestoreFile());
            RestoreBackupCommand = new RelayCommand(_ => restoreBackup());
        }

        #region Properties

        private string _backupDirectory = string.Empty;
        public string BackupDirectory
        {
            get => _backupDirectory;
            set { _backupDirectory = value; OnPropertyChanged(); }
        }

        public string SourceFile { get; }

        private string _restoreFile = string.Empty;
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

        private void setInitialSettings()
        {
            var setting = _controller.GetSettingBackUpDir("BackUpDir");
            if (!string.IsNullOrWhiteSpace(setting))
                BackupDirectory = setting;
        }

        private void selectBackupDir()
        {
            using var dlg = new FolderBrowserDialog();
            var setting = _controller.GetSettingBackUpDir("BackUpDir");

            if (!string.IsNullOrWhiteSpace(setting))
                dlg.SelectedPath = setting;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                BackupDirectory = dlg.SelectedPath;
                _controller.SetSetting("BackUpDir", dlg.SelectedPath);
            }
        }

        private void createBackup()
        {
            if (string.IsNullOrWhiteSpace(BackupDirectory))
            {
                _dialogService.Show("No backup directory selected", "Information");
                return;
            }

            try
            {
                var file = _controller.CreateBackUpFile(BackupDirectory, SourceFile);
                _dialogService.Show($"Back-up gemaakt:\n{file}", "Back-up");
            }
            catch (Exception ex)
            {
                _dialogService.Show($"Back-up maken is mislukt.\n{ex.Message}", "Back-up");
            }
        }

        private void selectRestoreFile()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "SQLite databases|*.db;*.sqlite;*.sqlite3|Alle bestanden|*.*",
                Title = WpfUI.Managers.Languages.GetResourceString("BackUpAndRestoreRestoreFile", "Restore db file")
            };

            var setting = _controller.GetSettingBackUpDir("RestoreDirectory");
            if (!string.IsNullOrWhiteSpace(setting))
                dlg.InitialDirectory = setting;

            if (dlg.ShowDialog() == true)
                RestoreFile = dlg.FileName;
        }

        private void restoreBackup()
        {
            if (string.IsNullOrWhiteSpace(RestoreFile))
            {
                _dialogService.Show("No restore file selected", "No restore file");
                return;
            }

            if (!File.Exists(RestoreFile))
            {
                _dialogService.Show("Restore file does not exist", "Information");
                return;
            }

            if (!_dialogService.ShowYesNo(
                "Herstel vervangt alle opgeslagen gegevens in de huidige database. " +
                "Niet-opgeslagen wijzigingen gaan verloren. Na herstel sluit de app; start deze daarna opnieuw.\n\nDoorgaan?",
                "Back-up herstellen"))
                return;

            try
            {
                var safetyBackup = _controller.RestoreBackUpFile(RestoreFile, BackupBeforeRestore, BackupDirectory);
                var message = "De database is hersteld. Start de app opnieuw.";
                if (safetyBackup != null)
                    message += $"\nBack-up van de vorige gegevens:\n{safetyBackup}";
                _dialogService.Show(message, "Herstel voltooid");
            }
            catch (Exception ex)
            {
                _dialogService.Show($"Herstel is mislukt.\n{ex.Message}", "Back-up herstellen");
                return;
            }

            System.Windows.Application.Current.Shutdown();
        }

        #endregion
    }
}
