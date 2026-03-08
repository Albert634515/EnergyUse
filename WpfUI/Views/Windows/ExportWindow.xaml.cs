using System.Windows;

namespace WpfUI.Views.Windows
{
    public partial class ExportWindow : Window
    {
        public ExportWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            DataContext = new ExportViewModel();
        }
    }
}