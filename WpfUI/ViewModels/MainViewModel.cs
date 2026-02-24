using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
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

    public MainViewModel()
    {
        _controller = new MainController(Config.GetDbFileName());
        _controller.Initialize();

        LoadAddresses();

        // Toolbar + Menu commands
        ShowLeftDataCommand = new RelayCommand(_ => LoadDataControl());
        ShowLeftImportCommand = new RelayCommand(_ => LoadDataImport());
        ShowLeftUsageGraphCommand = new RelayCommand(_ => LoadChartUsageControl());
        ShowLeftCompareGraphCommand = new RelayCommand(_ => LoadChartCompare());
        ShowLeftRatesGraphCommand = new RelayCommand(_ => LoadRatesControl());

        PdfReportCommand = new RelayCommand(_ => ShowPdfReport());
        PaybackCommand = new RelayCommand(_ => ShowPayback());
        RecalculateCommand = new RelayCommand(_ => RecalculateCurrentSelection());

        RefreshCommand = new RelayCommand(_ => RefreshViews(false));
        CloseCommand = new RelayCommand(_ => Application.Current.Shutdown());
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
                LoadEnergyTypes();
                LoadDefaultViews();
                RefreshViews(true);
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
                LoadDefaultViews();
                RefreshViews(false);
            }
        }
    }

    private async void LoadAddresses()
    {
        var list = await _controller.GetAllAddresses();
        Addresses.Clear();
        foreach (var a in list)
            Addresses.Add(a);

        SelectedAddress = Addresses.FirstOrDefault(x => x.DefaultAddress == true)
                       ?? Addresses.FirstOrDefault();
    }

    private void LoadEnergyTypes()
    {
        EnergyTypes.Clear();

        if (SelectedAddress == null)
            return;

        var list = _controller.getEnergyTypesByAddressId(SelectedAddress.Id);
        foreach (var e in list)
            EnergyTypes.Add(e);

        SelectedEnergyType = EnergyTypes.FirstOrDefault(x => x.DefaultType == true)
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

    private void LoadDefaultViews()
    {
        if (SelectedAddress == null || SelectedEnergyType == null)
            return;

        // Left view
        LoadDataControl();

        // Right
        LoadChartUsageControl();
    }

    private void LoadDataControl()
    {
        LeftView = new DataControl(SelectedAddress, SelectedEnergyType);
    }

    private void LoadDataImport()
    {
       // LeftView = new ImportControl(SelectedAddress, SelectedEnergyType);
    }

    private void LoadChartUsageControl()
    {
        RightView = new ChartDefaultLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType,
            EnergyTypes.ToList()
        );
    }

    private void LoadChartCompare()
    {
        RightView = new ChartCompareLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType
        );
    }

    private void LoadRatesControl()
    {
        RightView = new ChartRatesLiveChartsControl(
            SelectedAddress,
            SelectedEnergyType
        );
    }

    private void RefreshViews(bool addressChanged)
    {
        if (LeftView is IRefreshable left)
            left.Refresh(SelectedAddress, SelectedEnergyType, addressChanged);

        if (RightView is IRefreshable right)
            right.Refresh(SelectedAddress, SelectedEnergyType, addressChanged);
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

    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    #endregion

    #region WinForms-equivalent Methods

    private void ShowPdfReport()
    {
        MessageBox.Show("PDF Report nog te implementeren in WPF");
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

    #endregion
}