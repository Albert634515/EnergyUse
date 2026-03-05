using EnergyUse.Common.Enums;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.ViewModels;

public class ChartDefaultLiveChartsViewModel : ViewModelBase
{
    private readonly DefaultChartService _service;
    private readonly ISettingsService _settings;
    private bool _isLoaded;

    public ChartDefaultLiveChartsViewModel(
        Address address,
        EnergyType energyType,
        IEnumerable<EnergyType> energyTypes,
        ISettingsService settings)
    {
        _service = new DefaultChartService();
        _settings = settings;

        CurrentAddress = address;
        CurrentEnergyType = energyType;
        EnergyTypes = new ObservableCollection<EnergyType>(energyTypes ?? Enumerable.Empty<EnergyType>());

        LoadPeriodTypes();
        LoadSettings();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart());
    }

    public void MarkLoaded()
    {
        _isLoaded = true;
        UpdateChart();
    }

    public void SafeUpdateChart()
    {
        if (_isLoaded)
            UpdateChart();
    }

    // ---------------------------------------------------------
    // ADDRESS + ENERGYTYPE
    // ---------------------------------------------------------

    public Address CurrentAddress
    {
        get => _currentAddress;
        set { if (SetProperty(ref _currentAddress, value)) SafeUpdateChart(); }
    }
    private Address _currentAddress;

    public EnergyType CurrentEnergyType
    {
        get => _currentEnergyType;
        set { if (SetProperty(ref _currentEnergyType, value)) SafeUpdateChart(); }
    }
    private EnergyType _currentEnergyType;

    // ---------------------------------------------------------
    // COMPARE WITH
    // ---------------------------------------------------------

    public ObservableCollection<EnergyType> EnergyTypes { get; }

    private EnergyType _selectedCompareEnergyType;
    public EnergyType SelectedCompareEnergyType
    {
        get => _selectedCompareEnergyType;
        set
        {
            if (SetProperty(ref _selectedCompareEnergyType, value))
            {
                _settings.Save("DefaultChartPeriodCompareWith", value?.Id.ToString() ?? "");
                SafeUpdateChart();
            }
        }
    }

    // ---------------------------------------------------------
    // LABELS
    // ---------------------------------------------------------

    public ChartLabel ConsumptionLabel { get; } = new();
    public ChartLabel ProductionLabel { get; } = new();
    public ChartLabel NettoLabel { get; } = new();

    private void UpdateLabels(Dictionary<string, ResultLabel> labels)
    {
        ApplyLabel(labels, "Consumption", ConsumptionLabel);
        ApplyLabel(labels, "Production", ProductionLabel);
        ApplyLabel(labels, "Netto", NettoLabel);
    }

    private void ApplyLabel(Dictionary<string, ResultLabel> labels, string key, ChartLabel target)
    {
        if (!labels.TryGetValue(key, out var src))
        {
            target.Visible = false;
            return;
        }

        target.Visible = src.LabelVisibility;
        target.Text = src.LabelText;

        target.ForeColor = new SolidColorBrush(Color.FromRgb(
            src.LabelForeColor.R,
            src.LabelForeColor.G,
            src.LabelForeColor.B));

        target.BackColor = new SolidColorBrush(Color.FromRgb(
            src.LabelBackColor.R,
            src.LabelBackColor.G,
            src.LabelBackColor.B));
    }

    // ---------------------------------------------------------
    // PERIOD
    // ---------------------------------------------------------

    public ObservableCollection<SelectionItem> PeriodTypes { get; } = new();

    private SelectionItem _selectedPeriodType;
    public SelectionItem SelectedPeriodType
    {
        get => _selectedPeriodType;
        set
        {
            if (SetProperty(ref _selectedPeriodType, value))
            {
                _settings.Save("DefaultChartPeriodPeriodType", value?.Key ?? "");
                SafeUpdateChart();
            }
        }
    }

    private DateTime _fromDate;
    public DateTime FromDate
    {
        get => _fromDate;
        set
        {
            if (SetProperty(ref _fromDate, value))
            {
                _settings.SaveDate("DefaultChartPeriodPeriodStart", value);
                SafeUpdateChart();
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
                _settings.SaveDate("DefaultChartPeriodPeriodEnd", value);
                SafeUpdateChart();
            }
        }
    }

    // ---------------------------------------------------------
    // CHECKBOXES
    // ---------------------------------------------------------

    public bool PredictMissingData
    {
        get => _predictMissingData;
        set
        {
            if (SetProperty(ref _predictMissingData, value))
            {
                _settings.Save("DefaultChartPeriodPredictMissing", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _predictMissingData;

    public bool ShowStacked
    {
        get => _showStacked;
        set
        {
            if (SetProperty(ref _showStacked, value))
            {
                _settings.Save("DefaultChartPeriodShowStacked", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _showStacked;

    public bool ShowAverage
    {
        get => _showAverage;
        set
        {
            if (SetProperty(ref _showAverage, value))
            {
                _settings.Save("DefaultChartPeriodShowAverage", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _showAverage;

    // ---------------------------------------------------------
    // SHOWBY
    // ---------------------------------------------------------

    public bool ShowByCategory
    {
        get => _sbCat;
        set
        {
            if (SetProperty(ref _sbCat, value))
            {
                _settings.Save("DefaultChartCategoryShowBy", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _sbCat;

    public bool ShowBySubCategory
    {
        get => _sbSub;
        set
        {
            if (SetProperty(ref _sbSub, value))
            {
                _settings.Save("DefaultChartSubCategoryShowBy", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _sbSub;

    public bool ShowByTotal
    {
        get => _sbTot;
        set
        {
            if (SetProperty(ref _sbTot, value))
            {
                _settings.Save("DefaultChartTotalsShowBy", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _sbTot;

    // ---------------------------------------------------------
    // SHOWTYPE
    // ---------------------------------------------------------

    public bool ShowTypeRate
    {
        get => _stRate;
        set
        {
            if (SetProperty(ref _stRate, value))
            {
                _settings.Save("DefaultChartRateType", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _stRate;

    public bool ShowTypeValue
    {
        get => _stValue;
        set
        {
            if (SetProperty(ref _stValue, value))
            {
                _settings.Save("DefaultChartValueType", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _stValue;

    public bool ShowTypeEfficiency
    {
        get => _stEff;
        set
        {
            if (SetProperty(ref _stEff, value))
            {
                _settings.Save("DefaultChartEfficiencyType", value.ToString());
                SafeUpdateChart();
            }
        }
    }
    private bool _stEff;

    public bool IsEfficiencyVisible => CurrentEnergyType?.HasEnergyReturn ?? false;

    // ---------------------------------------------------------
    // CHART
    // ---------------------------------------------------------

    public ObservableCollection<ISeries> ChartSeries { get; set; } = new();
    public ObservableCollection<Axis> XAxes { get; set; } = new();
    public ObservableCollection<Axis> YAxes { get; set; } = new();

    public void UpdateChart()
    {
        if (!_isLoaded) return;
        if (CurrentAddress == null || CurrentEnergyType == null || SelectedPeriodType == null)
            return;

        var period = LibGraphGeneral.GetPeriodType(SelectedPeriodType.Key);

        var energyTypes = new List<EnergyType> { CurrentEnergyType };
        if (SelectedCompareEnergyType != null && SelectedCompareEnergyType.Id != CurrentEnergyType.Id)
            energyTypes.Add(SelectedCompareEnergyType);

        var result = _service.BuildChart(
            CurrentAddress,
            energyTypes,
            period,
            FromDate,
            TillDate,
            ShowStacked,
            ShowAverage,
            PredictMissingData,
            GetShowBy(),
            GetShowType()
        );

        ChartSeries = new ObservableCollection<ISeries>(result.Series);
        XAxes = new ObservableCollection<Axis>(result.XAxes);
        YAxes = new ObservableCollection<Axis>(result.YAxes);

        UpdateLabels(result.Labels);

        OnPropertyChanged(nameof(ChartSeries));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    }

    private ShowBy GetShowBy() =>
        ShowByCategory ? ShowBy.Category :
        ShowBySubCategory ? ShowBy.SubCategory :
        ShowBy.Total;

    private ShowType GetShowType() =>
        ShowTypeRate ? ShowType.Rate :
        ShowTypeValue ? ShowType.Value :
        ShowTypeEfficiency ? ShowType.Efficiency :
        ShowType.Unknown;

    // ---------------------------------------------------------
    // COMMANDS
    // ---------------------------------------------------------

    public ICommand ResetCommand { get; }
    public ICommand ExportCommand { get; }

    private void ResetChart()
    {
        LoadDefaults();
        SafeUpdateChart();
    }

    private void ExportChart()
    {
        _service.ExportToExcel(CurrentEnergyType);
    }

    // ---------------------------------------------------------
    // SETTINGS
    // ---------------------------------------------------------

    private void LoadSettings()
    {
        LoadDefaults();

        ShowStacked = GetBool("DefaultChartPeriodShowStacked", true);
        ShowAverage = GetBool("DefaultChartPeriodShowAverage", true);
        PredictMissingData = GetBool("DefaultChartPeriodPredictMissing", true);

        ShowByCategory = GetBool("DefaultChartCategoryShowBy", true);
        ShowBySubCategory = GetBool("DefaultChartSubCategoryShowBy", false);
        ShowByTotal = GetBool("DefaultChartTotalsShowBy", false);

        ShowTypeRate = GetBool("DefaultChartRateType", true);
        ShowTypeValue = GetBool("DefaultChartValueType", false);
        ShowTypeEfficiency = GetBool("DefaultChartEfficiencyType", false);

        var compareId = _settings.Get("DefaultChartPeriodCompareWith");
        if (int.TryParse(compareId, out int id))
            SelectedCompareEnergyType = EnergyTypes.FirstOrDefault(e => e.Id == id);
    }

    private void LoadDefaults()
    {
        FromDate = _settings.GetDate("DefaultChartPeriodPeriodStart", DateTime.Now.AddMonths(-12));
        TillDate = _settings.GetDate("DefaultChartPeriodPeriodEnd", DateTime.Now);

        var key = _settings.Get("DefaultChartPeriodPeriodType") ?? "MONTH";
        SelectedPeriodType = PeriodTypes.FirstOrDefault(p => p.Key == key)
                             ?? PeriodTypes.FirstOrDefault();
    }

    private bool GetBool(string key, bool defaultValue)
    {
        var v = _settings.Get(key);
        return v == null ? defaultValue : v.ToLower() == "true";
    }

    // ---------------------------------------------------------
    // HELPERS
    // ---------------------------------------------------------

    private void LoadPeriodTypes()
    {
        foreach (var p in WpfUI.Managers.SelectionItemList.GetPeriodList())
            PeriodTypes.Add(p);
    }
}