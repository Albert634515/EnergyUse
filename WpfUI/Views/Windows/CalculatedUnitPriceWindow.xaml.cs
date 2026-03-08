using System.Windows;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for CalculatedUnitPriceWindow.xaml
    /// </summary>
    public partial class CalculatedUnitPriceWindow : Window
    {
        public CalculatedUnitPriceWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
