using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }        
    }
}