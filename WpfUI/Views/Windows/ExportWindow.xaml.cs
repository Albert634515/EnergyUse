using System.Windows;

namespace EnergyUse.Wpf.Views
{
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();
            DataContext = new ExportViewModel();
        }
    }
}