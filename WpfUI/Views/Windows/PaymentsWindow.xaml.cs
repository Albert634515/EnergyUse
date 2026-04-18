using System.ComponentModel;
using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class PaymentsWindow : Window
{
    public PaymentsWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ShowInTaskbar = false;

        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            var vm = new PaymentsViewModel(new SettingsService());
            vm.CloseRequested += () => Close();
            DataContext = vm;
        }
    }
}
