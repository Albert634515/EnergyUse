using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for PayBackTimeWindow.xaml
    /// </summary>
    public partial class PayBackTimeWindow : Window
    {
        public PayBackTimeWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            DataContext = new PayBackTimeViewModel();
        }
    }
}