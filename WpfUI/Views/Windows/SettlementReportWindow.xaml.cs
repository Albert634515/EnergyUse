using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class SettlementReportWindow : Window
{
    public SettlementReportWindow(Window owner, SettlementReportViewModel vm)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        MaxHeight = SystemParameters.WorkArea.Height;

        DataContext = vm;

        vm.CloseRequested += result =>
        {
            DialogResult = result;
            Close();
        };
    }

    public static async Task<ParameterSelection?> ShowDialogAsync(Window owner,
                                                                  Address currentAddress,
                                                                  EnergyUse.Common.Enums.ReportType defaultReport)
    {
        // 1. SettingsService maken
        ISettingsService settings = new SettingsService();

        // 2. ViewModel maken met dependency
        var vm = new SettlementReportViewModel(settings);

        // 3. ViewModel initialiseren
        await vm.InitializeAsync(currentAddress, defaultReport);

        // 4. Window maken met ViewModel
        var win = new SettlementReportWindow(owner, vm);

        // 5. Window tonen
        bool? result = win.ShowDialog();

        // 6. Parameters teruggeven
        return result == true ? vm.GetSelectedParameters() : null;
    }
}
