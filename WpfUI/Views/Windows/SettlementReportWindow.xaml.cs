using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for SettlementReportWindow.xaml
    /// </summary>
    public partial class SettlementReportWindow : Window
    {
        public SettlementReportWindow()
        {
            InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                DataContext = new SettlementReportViewModel();
        }
    }
}
