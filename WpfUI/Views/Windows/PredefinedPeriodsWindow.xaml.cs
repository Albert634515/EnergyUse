using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class PredefinedPeriodsWindow : Window
    {
        public PredefinedPeriodsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowInTaskbar = false;

            DataContext = new PredefinedPeriodsViewModel(this);
        }
    }
}
