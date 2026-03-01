using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using WpfUI.Interfaces;
using WpfUI.Managers;
using WpfUI.Views.Controls;
using WpfUI.Views.Windows;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly MainController _controller;
    private readonly ISettingsService _settings;

    public MainViewModel()
    {
        _settings = new SettingsService();
        _controller = new MainController(Config.GetDbFileName());
        _controller.Initialize();

        setInitialSettings();
        setAddresses();
        setLastViews();

        // Toolbar + Menu commands
        ShowLeftDataCommand = new RelayCommand(_ => setDataControl());
        ShowLeftImportCommand = new RelayCommand(_ => setDataImport());
        ShowLeftUsageGraphCommand = new RelayCommand(_ => setChartUsageControl());
        ShowLeftCompareGraphCommand = new RelayCommand(_ => setChartCompare());
        ShowLeftRatesGraphCommand = new RelayCommand(_ => setRatesControl());

        PdfReportCommand = new RelayCommand(async _ => await OpenSettlementReport());
        PaybackCommand = new RelayCommand(_ => ShowPayback());
        RecalculateCommand = new RelayCommand(_ => RecalculateCurrentSelection());
        RecalculateAllCommand = new RelayCommand(_ => RecalculateAll());

        RefreshCommand = new RelayCommand(_ => refreshViews(false));
        CloseCommand = new RelayCommand(_ => Application.Current.Shutdown());

        // Settings menu
        //SettingsGeneralCommand = new RelayCommand(_ => new SettingsWindow().ShowDialog());
        SettingsEnergyTypesCommand = new RelayCommand(_ => new EnergyTypesWindow().ShowDialog());
        SettingsAddressesCommand = new RelayCommand(_ => new AddressesWindow().ShowDialog());
        SettingsMetersCommand = new RelayCommand(_ => new MetersWindow().ShowDialog());
        SettingsCostCategoriesCommand = new RelayCommand(_ => new CostCategoriesWindow().ShowDialog());
        //SettingsTariffGroupsCommand = new RelayCommand(_ => new TariffGroupsWindow().ShowDialog());
        //SettingsRatesCommand = new RelayCommand(_ => new RatesWindow().ShowDialog());
        SettingsPredefinedPeriodsCommand = new RelayCommand(_ => new PredefinedPeriodsWindow().ShowDialog());
        SettingsCorrectionFactorsCommand = new RelayCommand(_ => new CorrectionFactorsWindow().ShowDialog());
        SettingsNettingCommand = new RelayCommand(_ => new NettingWindow().ShowDialog());
        SettingsVatTariffsCommand = new RelayCommand(_ => new VatTariffsWindow().ShowDialog());
        SettingsCalculatedUnitPriceCommand = new RelayCommand(_ => new CalculatedUnitPriceWindow().ShowDialog());
        SettingsPaymentsCommand = new RelayCommand(_ => new PaymentsWindow().ShowDialog());

        // Import / Export
        BackupRestoreCommand = new RelayCommand(_ => new BackUpAndRestoreWindow().ShowDialog());
        //ImportMeterRatesCommand = new RelayCommand(_ => new ImportWindow().ShowDialog());
        //ExportRatesCommand = new RelayCommand(_ => new ExportWindow().ShowDialog());

        // Misc
        SetupNewFileCommand = new RelayCommand(_ => new SetupNewFileWindow().ShowDialog());
        //CreateDemoDataCommand = new RelayCommand(_ => new CreateDemoDataWindow().ShowDialog());
        InfoCommand = new RelayCommand(_ => new InfoWindow().ShowDialog());
    }

    #region Address + EnergyType

    public ObservableCollection<Address> Addresses { get; } = new();
    public ObservableCollection<EnergyType> EnergyTypes { get; } = new();

    private Address _selectedAddress;
    public Address SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            if (SetProperty(ref _selectedAddress, value))
            {
                _settings.Save("SelectedAddress", value?.Id.ToString() ?? "");
                setEnergyTypes();
                refreshViews(true);
            }
        }
    }

    private EnergyType _selectedEnergyType;
    public EnergyType SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            if (SetProperty(ref _selectedEnergyType, value))
            {
                _settings.Save("SelectedEnergyType", value?.Id.ToString() ?? "");
                refreshViews(false);
            }
        }
    }

    private async void setAddresses()
    {
        var list = await _controller.GetAllAddresses();
        Addresses.Clear();

        foreach (var a in list)
            Addresses.Add(a);

        var saved = _settings.Get("SelectedAddress");

        SelectedAddress =
            Addresses.FirstOrDefault(x => x.Id.ToString() == saved)
            ?? Addresses.FirstOrDefault(x => x.DefaultAddress == true)
            ?? Addresses.FirstOrDefault();
    }

    private void setEnergyTypes()
    {
        EnergyTypes.Clear();

        if (SelectedAddress == null)
            return;

        var list = _controller.getEnergyTypesByAddressId(SelectedAddress.Id);
        foreach (var e in list)
            EnergyTypes.Add(e);

        var saved = _settings.Get("SelectedEnergyType");
        SelectedEnergyType = EnergyTypes.FirstOrDefault(x => x.Id.ToString() == saved)
                          ?? EnergyTypes.FirstOrDefault(x => x.DefaultType)
                          ?? EnergyTypes.FirstOrDefault();
    }

    #endregion

    #region Views

    private object _leftView;
    public object LeftView
    {
        get => _leftView;
        set => SetProperty(ref _leftView, value);
    }

    private object _rightView;
    public object RightView
    {
        get => _rightView;
        set => SetProperty(ref _rightView, value);
    }

    private void SaveLeftView(string key) => _settings.Save("LastLeftView", key);
    private void SaveRightView(string key) => _settings.Save("LastRightView", key);

    private void setLastViews()
    {
        var left = _settings.Get("LastLeftView") ?? "ucData";
        var right = _settings.Get("LastRightView") ?? "ucChartDefaultLiveCharts";

        setLeftView(left);
        LoadRightView(right);
    }

    private void setLeftView(string key)
    {
        switch (key)
        {
            case "ucData":
                setDataControl();
                break;
            case "ucImport":
                setDataImport();
                break;
        }
    }

    private void LoadRightView(string key)
    {
        switch (key)
        {
            case "ucChartDefaultLiveCharts":
                setChartUsageControl();
                break;
            case "ucChartCompareLiveCharts":
                setChartCompare();
                break;
            case "ucChartRatesLiveCharts":
                setRatesControl();
                break;
        }
    }

    private void setDataControl()
    {
        LeftView = new DataControl(SelectedAddress, SelectedEnergyType);
        SaveLeftView("ucData");
    }

    private void setDataImport()
    {
        LeftView = new ImportControl(SelectedAddress, SelectedEnergyType);
        SaveLeftView("ucImport");
    }

    private void setChartUsageControl()
    {
        RightView = new ChartDefaultLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType,
            EnergyTypes.ToList()
        );
        SaveRightView("ucChartDefaultLiveCharts");
    }

    private void setChartCompare()
    {
        RightView = new ChartCompareLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType
        );
        SaveRightView("ucChartCompareLiveCharts");
    }

    private void setRatesControl()
    {
        RightView = new ChartRatesLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType
        );
        SaveRightView("ucChartRatesLiveCharts");
    }

    private void refreshViews(bool addressChanged)
    {
        if (LeftView is IRefreshable left)
            left.Refresh(SelectedAddress, SelectedEnergyType, addressChanged);

        if (RightView is IRefreshable right)
            right.Refresh(SelectedAddress, SelectedEnergyType, addressChanged);
    }

    #endregion

    #region Splitter

    private GridLength _leftWidth;
    public GridLength LeftWidth
    {
        get => _leftWidth;
        set
        {
            if (SetProperty(ref _leftWidth, value))
                _settings.Save("MainSplitter", value.Value.ToString());
        }
    }

    private void setInitialSettings()
    {
        var saved = _settings.Get("MainSplitter");
        if (double.TryParse(saved, out double width))
            LeftWidth = new GridLength(width, GridUnitType.Star);
        else
            LeftWidth = new GridLength(1, GridUnitType.Star);
    }

    #endregion

    #region Commands

    public ICommand ShowLeftDataCommand { get; }
    public ICommand ShowLeftImportCommand { get; }
    public ICommand ShowLeftUsageGraphCommand { get; }
    public ICommand ShowLeftCompareGraphCommand { get; }
    public ICommand ShowLeftRatesGraphCommand { get; }

    public ICommand PdfReportCommand { get; }
    public ICommand PaybackCommand { get; }
    public ICommand RecalculateCommand { get; }
    public ICommand RecalculateAllCommand { get; }

    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public ICommand SettingsGeneralCommand { get; }
    public ICommand SettingsEnergyTypesCommand { get; }
    public ICommand SettingsAddressesCommand { get; }
    public ICommand SettingsMetersCommand { get; }
    public ICommand SettingsCostCategoriesCommand { get; }
    public ICommand SettingsTariffGroupsCommand { get; }
    public ICommand SettingsRatesCommand { get; }
    public ICommand SettingsPredefinedPeriodsCommand { get; }
    public ICommand SettingsCorrectionFactorsCommand { get; }
    public ICommand SettingsNettingCommand { get; }
    public ICommand SettingsVatTariffsCommand { get; }
    public ICommand SettingsCalculatedUnitPriceCommand { get; }
    public ICommand SettingsPaymentsCommand { get; }

    public ICommand BackupRestoreCommand { get; }
    public ICommand ImportMeterRatesCommand { get; }
    public ICommand ExportRatesCommand { get; }

    public ICommand SetupNewFileCommand { get; }
    public ICommand CreateDemoDataCommand { get; }
    public ICommand InfoCommand { get; }

    #endregion

    #region Methods

    private async Task OpenSettlementReport()
    {
        var vm = App.Services.GetRequiredService<SettlementReportViewModel>();
        var win = new SettlementReportWindow(vm);

        if (win.ShowDialog() == true)
        {
            var parameters = win.SelectedParameters;
            if (parameters == null)
                return;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                var fileName = await _controller.GetSettlementPdfAsync(parameters);

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    Process.Start(new ProcessStartInfo(fileName)
                    {
                        UseShellExecute = true
                    });
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }

    private void ShowPayback()
    {
        var win = new PayBackTimeWindow();
        win.ShowDialog();
    }

    private void RecalculateCurrentSelection()
    {
        MessageBox.Show("Recalculate nog te implementeren in WPF");
    }

    private void RecalculateAll()
    {
        if (SelectedAddress == null || SelectedEnergyType == null)
            return;

        var msg = $"Wil je alle data opnieuw berekenen voor energietype '{SelectedEnergyType.Name}'?";
        if (MessageBox.Show(msg, "Alles herberekenen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            _controller.RecalculateReadingsDiffPreviousDay(
                DateTime.MinValue,
                DateTime.MinValue,
                SelectedEnergyType.Id,
                SelectedAddress.Id);

            refreshViews(false);
        }
    }

    #endregion
}