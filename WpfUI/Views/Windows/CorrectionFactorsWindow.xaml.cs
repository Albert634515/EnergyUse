using System.Windows;
using System.ComponentModel;
using WpfUI.ViewModels;
using WpfUI.Services;

namespace WpfUI.Views.Windows;

public partial class CorrectionFactorsWindow : Window
{
    public CorrectionFactorsWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ShowInTaskbar = false;

        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            var vm = new CorrectionFactorViewModel(new SettingsService());
            vm.CloseRequested += () => Close();
            DataContext = vm;
        }
    }
}