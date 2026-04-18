using System.Windows;
using WpfUI.ViewModels;
using WpfUI.Services;

namespace WpfUI.Views.Windows;

public partial class PaymentsWindow : Window
{
    public PaymentsWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            DataContext = new PaymentsViewModel(new SettingsService());
    }
}