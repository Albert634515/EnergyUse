using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for VatTarifsWindow.xaml
    /// </summary>
    public partial class VatTarifsWindow : Window
    {
        public VatTarifsWindow()
        {
            InitializeComponent();
            DataContext = new VatTarifsViewModel();
        }
    }
}
