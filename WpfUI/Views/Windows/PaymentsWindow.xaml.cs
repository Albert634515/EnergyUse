using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

/// <summary>
/// Interaction logic for PaymentsWindow.xaml
/// </summary>
public partial class PaymentsWindow : Window
{
    public PaymentsWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            DataContext = new PaymentsViewModel();
    }
}