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
using WpfUI.Managers;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.ViewModels;

public class ChartDefaultLiveChartsViewModel : ViewModelBase
{
    private readonly DefaultChartService _service;
    private readonly ISettingsService _settings;
    private bool _isLoaded;
    private bool _isLoadingSettings;

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

        setPeriodTypes();
        setSettings();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => exportChart());
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
    
    private Address? _currentAddress;
    public Address? CurrentAddress
    {
        get => _currentAddress;
        set { if (SetProperty(ref _currentAddress, value)) SafeUpdateChart(); }
    }

    private EnergyType? _currentEnergyType;
    public EnergyType? CurrentEnergyType
    {
        get => _currentEnergyType;
        set { if (SetProperty(ref _currentEnergyType, value)) SafeUpdateChart(); }
    }    

    // ---------------------------------------------------------
    // COMPARE WITH
    // ---------------------------------------------------------

    public ObservableCollection<EnergyType> EnergyTypes { get; }

    private EnergyType? _selectedCompareEnergyType;
    public EnergyType? SelectedCompareEnergyType
    {
        get => _selectedCompareEnergyType;
        set
        {
            if (SetProperty(ref _selectedCompareEnergyType, value))
            {
                setSetting(getCompareSettingKey(), value?.Id.ToString() ?? "");
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

    private void updateLabels(Dictionary<string, ResultLabel> labels)
    {
        applyLabel(labels, "Consumption", ConsumptionLabel);
        applyLabel(labels, "Production", ProductionLabel);
        applyLabel(labels, "Netto", NettoLabel);
    }

    private void applyLabel(Dictionary<string, ResultLabel> labels, string key, ChartLabel target)
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

    private SelectionItem? _selectedPeriodType;
    public SelectionItem? SelectedPeriodType
    {
        get => _selectedPeriodType;
        set
        {
            if (SetProperty(ref _selectedPeriodType, value))
            {
                setSetting("DefaultChartPeriodPeriodType", value?.Key ?? "");
                if (!_isLoadingSettings)
                    getPeriodSettings();
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
                setDate(getPeriodSettingKey("DefaultChartPeriodPeriodStart"), value);
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
                setDate(getPeriodSettingKey("DefaultChartPeriodPeriodEnd"), value);
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
                setSetting(getPeriodSettingKey("DefaultChartPeriodPredictMissing"), value.ToString());
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
                setSetting(getPeriodSettingKey("DefaultChartPeriodShowStacked"), value.ToString());
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
                setSetting(getPeriodSettingKey("DefaultChartPeriodShowAverage"), value.ToString());
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodShowBy"), "DefaultChartCategoryShowBy");
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodShowBy"), "DefaultChartSubCategoryShowBy");
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodShowBy"), "DefaultChartTotalsShowBy");
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodType"), "DefaultChartRateType");
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodType"), "DefaultChartValueType");
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
                if (value)
                    setSetting(getPeriodSettingKey("DefaultChartPeriodType"), "DefaultChartEfficiencyType");
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
            getShowBy(),
            getShowType()
        );

        ChartSeries = new ObservableCollection<ISeries>(result.Series);
        XAxes = new ObservableCollection<Axis>(result.XAxes);
        YAxes = new ObservableCollection<Axis>(result.YAxes);

        updateLabels(result.Labels);

        OnPropertyChanged(nameof(ChartSeries));
        OnPropertyChanged(nameof(XAxes));
        OnPropertyChanged(nameof(YAxes));
    }

    private ShowBy getShowBy() =>
        ShowByCategory ? ShowBy.Category :
        ShowBySubCategory ? ShowBy.SubCategory :
        ShowBy.Total;

    private ShowType getShowType() =>
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
        setSettings();
        SafeUpdateChart();
    }

    private void exportChart()
    {
        if (CurrentEnergyType == null)
            return;

        _service.ExportToExcel(CurrentEnergyType);
    }

    // ---------------------------------------------------------
    // SETTINGS
    // ---------------------------------------------------------

    private void setSettings()
    {
        _isLoadingSettings = true;
        getDefaults();
        getPeriodSettings();
        _isLoadingSettings = false;
    }

    private void getPeriodSettings()
    {
        var wasLoadingSettings = _isLoadingSettings;
        _isLoadingSettings = true;

        var defaultFrom = _settings.GetDate("DefaultChartPeriodPeriodStart", DateTime.Now.AddMonths(-12));
        var defaultTill = _settings.GetDate("DefaultChartPeriodPeriodEnd", DateTime.Now);
        FromDate = _settings.GetDate(getPeriodSettingKey("DefaultChartPeriodPeriodStart"), defaultFrom);
        TillDate = _settings.GetDate(getPeriodSettingKey("DefaultChartPeriodPeriodEnd"), defaultTill);

        ShowStacked = getPeriodBool("DefaultChartPeriodShowStacked", getBool("DefaultChartPeriodShowStacked", true));
        ShowAverage = getPeriodBool("DefaultChartPeriodShowAverage", getBool("DefaultChartPeriodShowAverage", true));
        PredictMissingData = getPeriodBool("DefaultChartPeriodPredictMissing", getBool("DefaultChartPeriodPredictMissing", true));

        var showBy = _settings.Get(getPeriodSettingKey("DefaultChartPeriodShowBy"));
        ShowByCategory = showBy == null ? getBool("DefaultChartCategoryShowBy", true) : showBy == "DefaultChartCategoryShowBy";
        ShowBySubCategory = showBy == "DefaultChartSubCategoryShowBy";
        ShowByTotal = showBy == "DefaultChartTotalsShowBy";

        var showType = _settings.Get(getPeriodSettingKey("DefaultChartPeriodType"));
        ShowTypeRate = showType == null ? getBool("DefaultChartRateType", true) : showType == "DefaultChartRateType";
        ShowTypeValue = showType == "DefaultChartValueType";
        ShowTypeEfficiency = showType == "DefaultChartEfficiencyType";

        var compareId = _settings.Get(getCompareSettingKey()) ?? _settings.Get("DefaultChartPeriodCompareWith");
        if (int.TryParse(compareId, out int id))
            SelectedCompareEnergyType = EnergyTypes.FirstOrDefault(e => e.Id == id);

        _isLoadingSettings = wasLoadingSettings;
    }

    private void getDefaults()
    {
        var key = _settings.Get("DefaultChartPeriodPeriodType") ?? "Year";
        SelectedPeriodType = PeriodTypes.FirstOrDefault(p => p.Key == key)
                             ?? PeriodTypes.FirstOrDefault();
    }

    private string getPeriodSettingKey(string key) =>
        $"{key}_{SelectedPeriodType?.Key.ToUpperInvariant()}";

    private string getCompareSettingKey() =>
        $"DefaultChartPeriodCompareWith{SelectedPeriodType?.Key.ToUpperInvariant()}";

    private bool getPeriodBool(string key, bool defaultValue)
    {
        var value = _settings.Get(getPeriodSettingKey(key));
        return value == null ? defaultValue : value.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    private void setSetting(string key, string value)
    {
        if (!_isLoadingSettings)
            _settings.Save(key, value);
    }

    private void setDate(string key, DateTime value)
    {
        if (!_isLoadingSettings)
            _settings.SaveDate(key, value);
    }

    private bool getBool(string key, bool defaultValue)
    {
        var v = _settings.Get(key);
        return v == null ? defaultValue : v.ToLower() == "true";
    }

    private void setPeriodTypes()
    {
        foreach (var p in SelectionItemList.GetPeriodList())
            PeriodTypes.Add(p);
    }
}
