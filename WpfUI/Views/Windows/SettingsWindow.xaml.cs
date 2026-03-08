using System.Windows;
using WpfUI.ViewModels;
using WpfUI.Services;

namespace WpfUI.Views.Windows
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            DataContext = new SettingsViewModel(
                new SettingsService(),
                new DialogService(),
                new LanguageService());
        }
    }
}