using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class NettingWindow : Window
{
    public NettingWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;

        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        DataContext = new NettingViewModel(this);
    }
}