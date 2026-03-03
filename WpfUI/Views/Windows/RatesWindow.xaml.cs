using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class RatesWindow : Window
    {
        public RatesWindow()
        {
            InitializeComponent();
            DataContext = new RatesViewModel(this);
        }
    }
}