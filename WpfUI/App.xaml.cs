using EnergyUse.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;
using WpfUI.Views.Windows;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            // Registraties
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IImportService, ImportService>();

            services.AddTransient<SettlementReportViewModel>();

            Services = services.BuildServiceProvider();
        }

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
