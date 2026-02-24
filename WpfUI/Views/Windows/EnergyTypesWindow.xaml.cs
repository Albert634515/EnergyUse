using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for EnergyTypesWindow.xaml
    /// </summary>
    public partial class EnergyTypesWindow : Window
    {
        public EnergyTypesWindow()
        {
            InitializeComponent();
            DataContext = new EnergyTypesViewModel();
        }
    }
}
