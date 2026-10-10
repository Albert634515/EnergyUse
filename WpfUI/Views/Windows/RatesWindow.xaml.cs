using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class RatesWindow : Window
    {
        public RatesWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var viewModel = new RatesViewModel(this, new DialogService());
            DataContext = viewModel;
            Loaded += async (_, _) => await viewModel.InitializeAsync();
        }
    }
}
