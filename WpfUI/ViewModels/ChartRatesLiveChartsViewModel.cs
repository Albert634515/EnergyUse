using EnergyUse.Common.Enums;
using EnergyUse.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        setCostCategories();
        initDefaults();

        ResetCommand = new RelayCommand(_ => resetChart());
        ExportCommand = new RelayCommand(_ => exportChart());

        SetChart();
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
    public ObservableCollection<CostCategoryOption> CostCategoryOptions { get; } = new();

    public string SelectedCostCategoriesText => $"{SelectedCostCategories.Count} geselecteerd";
    private bool _updatingCostCategorySelection;

    private DateTime _fromDate;
    public DateTime FromDate
    {
        get => _fromDate;
        set { if (SetProperty(ref _fromDate, value)) SetChart(); }
    }

    private DateTime _tillDate;
    public DateTime TillDate
    {
        get => _tillDate;
        set { if (SetProperty(ref _tillDate, value)) SetChart(); }
    }

    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value)) SetChart(); }
    }
    private bool _stRate = true;

    public bool ShowTypeUnit
    {
        get => _stUnit;
        set { if (SetProperty(ref _stUnit, value)) SetChart(); }
    }
    private bool _stUnit;

    public bool ShowMonthlyDataPoints
    {
        get => _showMonthlyDataPoints;
        set { if (SetProperty(ref _showMonthlyDataPoints, value)) SetChart(); }
    }
    private bool _showMonthlyDataPoints;

    #endregion

    #region Chart

    public ObservableCollection<ISeries> ChartSeries { get; } = new();
    public ObservableCollection<Axis> XAxes { get; } = new();
    public ObservableCollection<Axis> YAxes { get; } = new();

    public void SetChart()
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
            showType,
            ShowMonthlyDataPoints
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

    private void resetChart()
    {
        initDefaults();
        SetChart();
    }

    private void exportChart()
    {
        // optional: implement Excel export
    }

    #endregion

    #region Helpers

    private async void setCostCategories()
    {
        CostCategories.Clear();
        CostCategoryOptions.Clear();

        var list = (await _unitOfWork.CostCategoryRepo
            .SelectByEnergyTypeId(CurrentEnergyType.Id))
            .ToList();

        foreach (var c in list)
        {
            CostCategories.Add(c);

            var option = new CostCategoryOption(c);
            option.SelectionChanged += (_, _) => setSelectedCostCategories();
            CostCategoryOptions.Add(option);
        }
    }

    private void initDefaults()
    {
        FromDate = DateTime.Now.AddYears(-4);
        TillDate = DateTime.Now;

        _updatingCostCategorySelection = true;
        foreach (var option in CostCategoryOptions)
            option.IsSelected = true;
        _updatingCostCategorySelection = false;

        setSelectedCostCategories();
    }

    private void setSelectedCostCategories()
    {
        if (_updatingCostCategorySelection)
            return;

        SelectedCostCategories.Clear();
        foreach (var option in CostCategoryOptions.Where(option => option.IsSelected))
            SelectedCostCategories.Add(option.Category);

        OnPropertyChanged(nameof(SelectedCostCategoriesText));
        SetChart();
    }

    #endregion
}

public sealed class CostCategoryOption : ViewModelBase
{
    public CostCategoryOption(CostCategory category) => Category = category;

    public CostCategory Category { get; }
    public string Name => Category.Name;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (SetProperty(ref _isSelected, value))
                SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? SelectionChanged;
}
