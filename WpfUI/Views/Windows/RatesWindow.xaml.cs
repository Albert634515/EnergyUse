using System.Windows;
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
            DataContext = new RatesViewModel(this);
        }
    }
}