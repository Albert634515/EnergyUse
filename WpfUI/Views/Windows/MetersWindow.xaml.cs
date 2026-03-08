using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class MetersWindow : Window
{
    public MetersWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;

        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        DataContext = new MetersViewModel(this);
    }
}