using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

/// <summary>
/// Interaction logic for AddressesWindow.xaml
/// </summary>
public partial class AddressesWindow : Window
{
    public AddressesWindow()
    {
        InitializeComponent();
        Loaded += Addresses_Loaded;
    }

    private void Addresses_Loaded(object sender, RoutedEventArgs e)
    {
        if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            DataContext = new AddressesViewModel();
    }
}
