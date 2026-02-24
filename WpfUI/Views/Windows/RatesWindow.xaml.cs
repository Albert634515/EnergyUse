using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

/// <summary>
/// Interaction logic for EnergyRatesWindow.xaml
/// </summary>
public partial class EnergyRatesWindow : Window
{
    public EnergyRatesWindow()
    {
        InitializeComponent();
        DataContext = new RatesViewModel(this);
    }
}
