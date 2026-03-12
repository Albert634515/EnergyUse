using EnergyUse.Models;
using EnergyUse.Models.Common;
using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class SettlementReportWindow : Window
{
    public SettlementReportWindow(Window owner, SettlementReportViewModel vm)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        DataContext = vm;

        vm.CloseRequested += result =>
        {
            DialogResult = result;
            Close();
        };
    }

    public static async Task<ParameterSelection?> ShowDialogAsync(
        Window owner,
        Address currentAddress,
        EnergyUse.Common.Enums.ReportType defaultReport)
    {
        // 1. ViewModel maken
        var vm = new SettlementReportViewModel();

        // 2. ViewModel initialiseren (BELANGRIJK!)
        await vm.InitializeAsync(currentAddress, defaultReport);

        // 3. Window maken met ViewModel
        var win = new SettlementReportWindow(owner, vm);

        // 4. Window tonen
        bool? result = win.ShowDialog();

        // 5. Parameters teruggeven
        return result == true ? vm.GetSelectedParameters() : null;
    }
}