using EnergyUse.Common.Enums;
using EnergyUse.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfUI.Converters;
using WpfUI.Managers;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class ChartRatesLiveChartsViewModel : ViewModelBase
{
    private readonly RatesChartService _service;
    private readonly EnergyUse.Core.UnitOfWork.Graphs _unitOfWork;

    public ChartRatesLiveChartsViewModel(Address address, EnergyType energyType)
    {
        _service = new RatesChartService();
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Graphs(Config.GetDbFileName());

        CurrentAddress = address;
        CurrentEnergyType = energyType;

        LoadCostCategories();
        InitDefaults();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart());

        UpdateChart();
    }

    #region External

    public Address CurrentAddress
    {
        get => _currentAddress;
        set => SetProperty(ref _currentAddress, value);
    }
    private Address _currentAddress;

    public EnergyType CurrentEnergyType
    {
        get => _currentEnergyType;
        set => SetProperty(ref _currentEnergyType, value);
    }
    private EnergyType _currentEnergyType;

    #endregion

    #region Properties

    public ObservableCollection<CostCategory> CostCategories { get; } = new();
    public ObservableCollection<CostCategory> SelectedCostCategories { get; } = new();

    private DateTime _fromDate;
    public DateTime FromDate
    {
        get => _fromDate;
        set { if (SetProperty(ref _fromDate, value)) UpdateChart(); }
    }

    private DateTime _tillDate;
    public DateTime TillDate
    {
        get => _tillDate;
        set { if (SetProperty(ref _tillDate, value)) UpdateChart(); }
    }

    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value)) UpdateChart(); }
    }
    private bool _stRate = true;

    public bool ShowTypeUnit
    {
        get => _stUnit;
        set { if (SetProperty(ref _stUnit, value)) UpdateChart(); }
    }
    private bool _stUnit;

    #endregion

    #region Chart

    public ObservableCollection<ISeries> ChartSeries { get; } = new();
    public ObservableCollection<Axis> XAxes { get; } = new();
    public ObservableCollection<Axis> YAxes { get; } = new();

    public void UpdateChart()
    {
        if (CurrentAddress == null || CurrentEnergyType == null)
            return;

        var showType = ShowTypeRate ? ShowType.Rate : ShowType.Unit;

        var result = _service.BuildChart(
            CurrentAddress,
            CurrentEnergyType,
            SelectedCostCategories,
            FromDate,
            TillDate,
            showType
        );

        ChartSeries.Clear();
        foreach (var s in result.Series)
            ChartSeries.Add(s);

        XAxes.Clear();
        foreach (var ax in result.XAxes)
            XAxes.Add(ax);

        YAxes.Clear();
        foreach (var ay in result.YAxes)
            YAxes.Add(ay);
    }

    #endregion

    #region Commands

    public ICommand ResetCommand { get; }
    public ICommand ExportCommand { get; }

    private void ResetChart()
    {
        InitDefaults();
        UpdateChart();
    }

    private void ExportChart()
    {
        // optional: implement Excel export
    }

    #endregion

    #region Helpers

    private void LoadCostCategories()
    {
        CostCategories.Clear();

        var list = _unitOfWork.CostCategoryRepo
            .SelectByEnergyTypeId(CurrentEnergyType.Id)
            .ToList();

        foreach (var c in list)
            CostCategories.Add(c);
    }

    private void InitDefaults()
    {
        FromDate = DateTime.Now.AddYears(-4);
        TillDate = DateTime.Now;

        SelectedCostCategories.Clear();
        foreach (var c in CostCategories)
            SelectedCostCategories.Add(c);
    }

    #endregion
}