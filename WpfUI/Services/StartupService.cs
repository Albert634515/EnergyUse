using System.IO;
using System.Windows;
using WpfUI.Managers;

namespace WpfUI.Services
{
    public class StartupService
    {
        public void Initialize()
        {
            ValidateDatabase();
        }

        private void ValidateDatabase()
        {
            var sourceDb = Config.GetDbFileName();

            if (string.IsNullOrWhiteSpace(sourceDb))
            {
                RunSetup();
                return;
            }

            if (!File.Exists(sourceDb))
            {
                var message = Languages.GetResourceString(
                    "MainErrorDbNotExist",
                    "Current selected database in the config does not exist or is not accessible, the database needs to be set up before this program can be used."
                );

                MessageBox.Show(message, "Database Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                RunSetup();
                return;
            }

            // Re-check after setup
            sourceDb = Config.GetDbFileName();
            if (string.IsNullOrWhiteSpace(sourceDb) || !File.Exists(sourceDb))
            {
                var message = Languages.GetResourceString(
                    "MainErrorDbNotSetup",
                    "Database not set up or not accessible, application will be closed."
                );

                MessageBox.Show(message, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void RunSetup()
        {
            // Hier open je jouw setup window
            var setupWindow = new Views.Windows.SetupNewFileWindow();
            setupWindow.ShowDialog();
        }
    }
}