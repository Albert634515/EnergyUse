using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class AddressesWindow : Window
    {
        private readonly AddressesViewModel _viewModel;

        public AddressesWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Inject SettingsService
            _viewModel = new AddressesViewModel(new SettingsService());
            _viewModel.CloseRequested += () => Close();

            DataContext = _viewModel;
        }
    }
}