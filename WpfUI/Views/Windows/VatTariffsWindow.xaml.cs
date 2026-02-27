using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for VatTariffsWindow.xaml
    /// </summary>
    public partial class VatTariffsWindow : Window
    {
        public VatTariffsWindow()
        {
            InitializeComponent();
            DataContext = new VatTarifsViewModel();
        }
    }
}
