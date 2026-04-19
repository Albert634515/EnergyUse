using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class CostCategoriesWindow : Window
    {
        private readonly CostCategoriesViewModel _vm;

        public CostCategoriesWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var settingsService = new SettingsService();
            _vm = new CostCategoriesViewModel(settingsService);
            DataContext = _vm;
        }
    }
}
