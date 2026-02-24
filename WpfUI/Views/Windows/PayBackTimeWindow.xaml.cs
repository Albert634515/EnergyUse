using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for PayBackTimeWindow.xaml
    /// </summary>
    public partial class PayBackTimeWindow : Window
    {
        public PayBackTimeWindow()
        {
            InitializeComponent();
            DataContext = new PayBackTimeViewModel();
        }
    }
}