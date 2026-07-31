using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Core.Interfaces;
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
    private const string CategorySettingKey = "ChartRatesPeriodCategories";
    private const string StartDateSettingKey = "ChartRatesPeriodStart";
    private const string EndDateSettingKey = "ChartRatesPeriodEnd";
    private readonly RatesChartService _service;
    private readonly EnergyUse.Core.UnitOfWork.Graphs _unitOfWork;
    private readonly ISettingsService _settings;
    private Rates? _chartRates;
    private bool _suppressChartUpdates;
    private bool _initialized;
    private int _chartUpdateVersion;

    public ChartRatesLiveChartsViewModel(Address address, EnergyType energyType, ISettingsService settings)
    {
        _service = new RatesChartService();
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Graphs(Config.GetDbFileName());
        _settings = settings;

        CurrentAddress = address;
        CurrentEnergyType = energyType;

        _fromDate = _settings.GetDate(StartDateSettingKey, new DateTime(DateTime.Now.AddYears(-4).Year, 1, 1));
        _tillDate = _settings.GetDate(EndDateSettingKey, new DateTime(DateTime.Now.Year, 12, 31));
        if (_fromDate > _tillDate)
            _tillDate = _fromDate;

        ResetCommand = new RelayCommand(_ => resetChart());
        ExportCommand = new RelayCommand(_ => exportChart());
    }

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        _suppressChartUpdates = true;
        await setCostCategoriesAsync();
        _initialized = true;
        _suppressChartUpdates = false;
        await setChartAsync();
    }

    public async Task RefreshAsync(Address address, EnergyType energyType)
    {
        var energyTypeChanged = CurrentEnergyType?.Id != energyType.Id;
        CurrentAddress = address;
        CurrentEnergyType = energyType;

        if (energyTypeChanged)
        {
            _suppressChartUpdates = true;
            await setCostCategoriesAsync();
            _suppressChartUpdates = false;
        }

        if (_initialized)
            await setChartAsync();
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
        set
        {
            if (SetProperty(ref _fromDate, value))
            {
                _settings.SaveDate(StartDateSettingKey, value);
                if (TillDate != default && value > TillDate)
                    runWithoutChartUpdate(() => TillDate = value);
                SetChart();
            }
        }
    }

    private DateTime _tillDate;
    public DateTime TillDate
    {
        get => _tillDate;
        set
        {
            if (SetProperty(ref _tillDate, value))
            {
                _settings.SaveDate(EndDateSettingKey, value);
                if (FromDate != default && value < FromDate)
                    runWithoutChartUpdate(() => FromDate = value);
                SetChart();
            }
        }
    }

    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value) && value) SetChart(); }
    }
    private bool _stRate = true;

    public bool ShowTypeUnit
    {
        get => _stUnit;
        set { if (SetProperty(ref _stUnit, value) && value) SetChart(); }
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
        if (_suppressChartUpdates || !_initialized)
            return;

        _ = setChartAsync();
    }

    private async Task setChartAsync()
    {
        if (CurrentAddress == null || CurrentEnergyType == null || FromDate > TillDate)
            return;

        var updateVersion = ++_chartUpdateVersion;

        var showType = ShowTypeRate ? ShowType.Rate : ShowType.Unit;

        var result = await _service.BuildChartAsync(
            CurrentAddress,
            CurrentEnergyType,
            SelectedCostCategories.ToList(),
            FromDate,
            TillDate,
            showType,
            ShowMonthlyDataPoints
        );

        if (updateVersion != _chartUpdateVersion)
            return;

        _chartRates = result.Chart;

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
        runWithoutChartUpdate(initDefaults);
        SetChart();
    }

    private void exportChart()
    {
        if (_chartRates != null)
            _service.ExportToExcel(CurrentEnergyType, _chartRates);
    }

    #endregion

    #region Helpers

    private async Task setCostCategoriesAsync()
    {
        CostCategories.Clear();
        CostCategoryOptions.Clear();

        var list = (await _unitOfWork.CostCategoryRepo
            .SelectByEnergyTypeId(CurrentEnergyType.Id))
            .ToList();

        var (selectedIds, fromLegacySetting) = getSavedCategoryIds(CurrentEnergyType.Id);
        foreach (var c in list)
        {
            CostCategories.Add(c);

            var option = new CostCategoryOption(c);
            option.IsSelected = selectedIds == null || selectedIds.Contains(c.Id);
            option.SelectionChanged += (_, _) => setSelectedCostCategories();
            CostCategoryOptions.Add(option);
        }

        if (fromLegacySetting && CostCategoryOptions.All(option => !option.IsSelected))
        {
            _updatingCostCategorySelection = true;
            foreach (var option in CostCategoryOptions)
                option.IsSelected = true;
            _updatingCostCategorySelection = false;
        }

        setSelectedCostCategories();
    }

    private void initDefaults()
    {
        FromDate = new DateTime(DateTime.Now.AddYears(-4).Year, 1, 1);
        TillDate = new DateTime(DateTime.Now.Year, 12, 31);

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
        saveSelectedCostCategories();
        SetChart();
    }

    private (HashSet<long>? Ids, bool FromLegacySetting) getSavedCategoryIds(long energyTypeId)
    {
        var value = _settings.Get($"{CategorySettingKey}{energyTypeId}");
        var fromLegacySetting = false;
        if (value == null)
        {
            value = _settings.Get(CategorySettingKey);
            fromLegacySetting = value != null;
        }
        if (value == null)
            return (null, false);

        var ids = value.Split(';', StringSplitOptions.RemoveEmptyEntries)
                       .Select(id => long.TryParse(id, out var parsedId) ? parsedId : 0)
                       .Where(id => id > 0)
                       .ToHashSet();
        return (ids, fromLegacySetting);
    }

    private void saveSelectedCostCategories()
    {
        if (CurrentEnergyType == null)
            return;

        var value = string.Join(';', SelectedCostCategories.Select(category => category.Id));
        if (!string.IsNullOrEmpty(value))
            value += ";";
        _settings.Save($"{CategorySettingKey}{CurrentEnergyType.Id}", value);
    }

    private void runWithoutChartUpdate(Action action)
    {
        var wasSuppressed = _suppressChartUpdates;
        _suppressChartUpdates = true;
        try
        {
            action();
        }
        finally
        {
            _suppressChartUpdates = wasSuppressed;
        }
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
