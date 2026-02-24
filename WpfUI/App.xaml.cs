using System.Windows;
using WpfUI.Services;
using WpfUI.Views.Windows;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var startup = new StartupService();
            startup.Initialize();

            var main = new MainWindow();
            main.Show();
        }
    }
}
