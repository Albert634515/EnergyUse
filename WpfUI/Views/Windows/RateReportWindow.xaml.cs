using System.Windows;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for RateReportWindow.xaml
    /// </summary>
    public partial class RateReportWindow : Window
    {
        public RateReportWindow(Window owner)
        {
            InitializeComponent();

            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
