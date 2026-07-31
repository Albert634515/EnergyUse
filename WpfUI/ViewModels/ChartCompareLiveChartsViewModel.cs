using EnergyUse.Common.Enums;
using EnergyUse.Models;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Models;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class ChartCompareLiveChartsViewModel : ViewModelBase
{
    private const string PeriodTypeSettingKey = "ChartComparePeriodType";
    private const string StartYearSettingKey = "ChartCompareStartYear";
    private const string EndYearSettingKey = "ChartCompareEndYear";
    private const string NumberSettingKey = "ChartCompareNumbers";
    private const string DaySettingKey = "ChartCompareNumbers2";
    private readonly CompareChartService _service;
    private readonly ISettingsService _settings;
    private bool _suppressChartUpdates;
    private bool _isLoadingPeriodSettings;

    public Address? CurrentAddress { get; }
    public EnergyType? CurrentEnergyType { get; }

    private ObservableCollection<PeriodicData> _exportData = new();

    // ---------------------------------------------------------
    // MEMORY FOR DAY/WEEK/MONTH SELECTIONS
    // ---------------------------------------------------------
    private int _lastSelectedDay = 1;
    private int _lastSelectedWeek = 1;      // ← jouw keuze A
    private int _lastSelectedMonth = DateTime.Now.Month;

    public ChartCompareLiveChartsViewModel(
        Address address,
        EnergyType energyType,
        ILanguageService languageService,
        ISettingsService settings)
    {
        _suppressChartUpdates = true;

        CurrentAddress = address;
        CurrentEnergyType = energyType;
        _settings = settings;

        _service = new CompareChartService(languageService);

        setPeriodTypes(languageService);
        setYears();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart(), _ => _exportData.Any());

        ResetChart(restoreSavedPeriod: true);

        _suppressChartUpdates = false;
        UpdateChart();
    }

    // ---------------------------------------------------------
    // PERIOD KEY
    // ---------------------------------------------------------
    private string PeriodKey => (SelectedPeriodType?.Key ?? "").ToUpperInvariant();

    // ---------------------------------------------------------
    // LABELS
    // ---------------------------------------------------------
    public ChartLabel ConsumptionLabel { get; } = new();
    public ChartLabel ProductionLabel { get; } = new();
    public ChartLabel NettoLabel { get; } = new();

    private void UpdateLabels(CompareChartResult result)
    {
        void Apply(ChartLabel target, ResultLabel src)
        {
            target.Visible = src.LabelVisibility;
            target.Text = src.LabelText;
            target.BackColor = new SolidColorBrush(Color.FromArgb(src.LabelBackColor.A, src.LabelBackColor.R, src.LabelBackColor.G, src.LabelBackColor.B));
            target.ForeColor = new SolidColorBrush(Color.FromArgb(src.LabelForeColor.A, src.LabelForeColor.R, src.LabelForeColor.G, src.LabelForeColor.B));
        }

        if (result.Labels.TryGetValue("Consumption", out var c)) Apply(ConsumptionLabel, c);
        if (result.Labels.TryGetValue("Production", out var p)) Apply(ProductionLabel, p);
        if (result.Labels.TryGetValue("Netto", out var n)) Apply(NettoLabel, n);

        OnPropertyChanged(nameof(ConsumptionLabel));
        OnPropertyChanged(nameof(ProductionLabel));
        OnPropertyChanged(nameof(NettoLabel));
    }

    // ---------------------------------------------------------
    // SELECTIES
    // ---------------------------------------------------------
    public ObservableCollection<SelectionItem> PeriodTypes { get; } = new();
    public ObservableCollection<int> Years { get; } = new();
    public ObservableCollection<int> NumberList { get; } = new();
    public ObservableCollection<int> DayList { get; } = new();

    private SelectionItem? _selectedPeriodType;
    public SelectionItem? SelectedPeriodType
    {
        get => _selectedPeriodType;
        set
        {
            if (SetProperty(ref _selectedPeriodType, value))
            {
                if (value != null)
                    _settings.Save(PeriodTypeSettingKey, value.Key);

                var shouldUpdate = !_suppressChartUpdates;
                runWithoutChartUpdate(() =>
                {
                    updateNumberList();   // ← BELANGRIJK
                    ApplyPeriodChange();
                });

                if (shouldUpdate)
                    UpdateChart();
            }
        }
    }

    private void ApplyPeriodChange()
    {
        _isLoadingPeriodSettings = true;
        try
        {
            var savedStartYear = getSavedSelection(StartYearSettingKey, DateTime.Now.Year - 1, Years);
            var savedEndYear = getSavedSelection(EndYearSettingKey, DateTime.Now.Year, Years);
            if (savedStartYear > savedEndYear)
                savedEndYear = savedStartYear;

            StartYear = savedStartYear;
            EndYear = savedEndYear;

            switch (PeriodKey)
            {
                case "DAY":
                    SelectedNumber = getSavedSelection(NumberSettingKey, _lastSelectedMonth, NumberList);
                    updateDayList();
                    SelectedDay = getSavedSelection(DaySettingKey, _lastSelectedDay, DayList);
                    break;

                case "WEEK":
                    SelectedNumber = getSavedSelection(NumberSettingKey, _lastSelectedWeek, NumberList);
                    SelectedDay = 0;
                    break;

                case "MONTH":
                    SelectedNumber = getSavedSelection(NumberSettingKey, _lastSelectedMonth, NumberList);
                    SelectedDay = 0;
                    break;

                case "YEAR":
                    SelectedNumber = 0;
                    SelectedDay = 0;
                    break;
            }
        }
        finally
        {
            _isLoadingPeriodSettings = false;
        }

        OnPropertyChanged(nameof(IsNumberVisible));
        OnPropertyChanged(nameof(IsDayVisible));
        OnPropertyChanged(nameof(NumberLabel));
    }

    public bool IsNumberVisible => PeriodKey != "YEAR";
    public bool IsDayVisible => PeriodKey == "DAY";

    public string NumberLabel => PeriodKey switch
    {
        "DAY" => "Month",
        "WEEK" => "Week",
        "MONTH" => "Month",
        _ => ""
    };

    private int _startYear;
    public int StartYear
    {
        get => _startYear;
        set
        {
            if (SetProperty(ref _startYear, value))
            {
                savePeriodSelection(StartYearSettingKey, value);

                if (EndYear != 0 && value > EndYear)
                    runWithoutChartUpdate(() => EndYear = value);

                UpdateChart();
            }
        }
    }

    private int _endYear;
    public int EndYear
    {
        get => _endYear;
        set
        {
            if (SetProperty(ref _endYear, value))
            {
                savePeriodSelection(EndYearSettingKey, value);

                if (StartYear != 0 && value < StartYear)
                    runWithoutChartUpdate(() => StartYear = value);

                UpdateChart();
            }
        }
    }

    private int _selectedNumber;
    public int SelectedNumber
    {
        get => _selectedNumber;
        set
        {
            if (PeriodKey == "WEEK" && value <= 0)
                value = 1;

            if (SetProperty(ref _selectedNumber, value))
            {
                if (PeriodKey == "DAY")
                {
                    _lastSelectedMonth = value;
                    updateDayList();
                }

                if (PeriodKey == "WEEK")
                    _lastSelectedWeek = value;

                if (PeriodKey == "MONTH")
                    _lastSelectedMonth = value;

                savePeriodSelection(NumberSettingKey, value);
                UpdateChart();
            }
        }
    }

    private int _selectedDay;
    public int SelectedDay
    {
        get => _selectedDay;
        set
        {
            if (SetProperty(ref _selectedDay, value))
            {
                if (PeriodKey == "DAY")
                    _lastSelectedDay = value;

                savePeriodSelection(DaySettingKey, value);
                UpdateChart();
            }
        }
    }

    private int getSavedSelection(string settingKey, int defaultValue, IEnumerable<int> availableValues)
    {
        var savedValue = _settings.Get($"{settingKey}{PeriodKey}");
        return int.TryParse(savedValue, out var value) && availableValues.Contains(value)
            ? value
            : defaultValue;
    }

    private void savePeriodSelection(string settingKey, int value)
    {
        if (!_isLoadingPeriodSettings && !string.IsNullOrWhiteSpace(PeriodKey))
            _settings.Save($"{settingKey}{PeriodKey}", value.ToString());
    }

    private bool _predict = true;
    public bool PredictMissingData
    {
        get => _predict;
        set { if (SetProperty(ref _predict, value)) UpdateChart(); }
    }

    private bool _stacked = true;
    public bool ShowStacked
    {
        get => _stacked;
        set { if (SetProperty(ref _stacked, value)) UpdateChart(); }
    }

    private bool _sbCat = true;
    public bool ShowByCategory
    {
        get => _sbCat;
        set { if (SetProperty(ref _sbCat, value) && value) UpdateChart(); }
    }

    private bool _sbSub;
    public bool ShowBySubCategory
    {
        get => _sbSub;
        set { if (SetProperty(ref _sbSub, value) && value) UpdateChart(); }
    }

    private bool _sbTot;
    public bool ShowByTotal
    {
        get => _sbTot;
        set { if (SetProperty(ref _sbTot, value) && value) UpdateChart(); }
    }

    private bool _stRate = true;
    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value) && value) UpdateChart(); }
    }

    private bool _stValue;
    public bool ShowTypeValue
    {
        get => _stValue;
        set { if (SetProperty(ref _stValue, value) && value) UpdateChart(); }
    }

    private bool _stEff;
    public bool ShowTypeEfficiency
    {
        get => _stEff;
        set { if (SetProperty(ref _stEff, value) && value) UpdateChart(); }
    }

    public bool IsEfficiencyVisible => CurrentEnergyType?.HasEnergyReturn ?? false;

    // ---------------------------------------------------------
    // CHART
    // ---------------------------------------------------------
    public ObservableCollection<ISeries> ChartSeries { get; } = new();
    public ObservableCollection<Axis> XAxes { get; } = new();
    public ObservableCollection<Axis> YAxes { get; } = new();

    public void UpdateChart()
    {
        if (_suppressChartUpdates)
            return;

        if (SelectedPeriodType == null || CurrentAddress == null || CurrentEnergyType == null)
            return;

        var showType = GetShowType();
        // Bij een radiobutton-wissel zet WPF eerst de oude keuze uit en
        // daarna pas de nieuwe aan. Ververs niet in die tussentoestand.
        if (showType == ShowType.Unknown)
            return;

        var result = _service.BuildChart(
            CurrentAddress,
            CurrentEnergyType,
            SelectedPeriodType,
            StartYear,
            EndYear,
            SelectedNumber,
            SelectedDay,
            PredictMissingData,
            ShowStacked,
            GetShowBy(),
            showType);

        ChartSeries.Clear();
        foreach (var s in result.Series)
            ChartSeries.Add(s);

        XAxes.Clear();
        foreach (var ax in result.XAxes)
            XAxes.Add(ax);

        YAxes.Clear();
        foreach (var ay in result.YAxes)
            YAxes.Add(ay);

        _exportData = new ObservableCollection<PeriodicData>(result.ExportData);

        UpdateLabels(result);

        CommandManager.InvalidateRequerySuggested();
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

    private void ResetChart(bool restoreSavedPeriod = false)
    {
        var shouldUpdate = !_suppressChartUpdates;
        runWithoutChartUpdate(() =>
        {
            PredictMissingData = true;
            ShowStacked = true;
            ShowByCategory = true;
            ShowBySubCategory = false;
            ShowByTotal = false;
            ShowTypeRate = true;
            ShowTypeValue = false;
            ShowTypeEfficiency = false;

            if (PeriodTypes.Any())
            {
                var savedPeriod = restoreSavedPeriod ? _settings.Get(PeriodTypeSettingKey) : null;
                SelectedPeriodType = PeriodTypes.FirstOrDefault(x =>
                                         string.Equals(x.Key, savedPeriod, StringComparison.OrdinalIgnoreCase))
                                     ?? PeriodTypes.FirstOrDefault();
            }
        });

        if (shouldUpdate)
            UpdateChart();
    }

    private void ExportChart()
    {
        if (CurrentEnergyType == null) return;

        _service.ExportToExcel(_exportData.ToList(), CurrentEnergyType);
    }

    // ---------------------------------------------------------
    // HELPERS
    // ---------------------------------------------------------
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

    private void setPeriodTypes(ILanguageService languageService)
    {
        var service = new SelectionItemService(languageService);
        PeriodTypes.Clear();
        foreach (var item in service.GetPeriodList())
            PeriodTypes.Add(item);
    }

    private void setYears()
    {
        Years.Clear();
        for (int y = 2020; y <= DateTime.Now.Year; y++)
            Years.Add(y);

        StartYear = DateTime.Now.Year - 1;
        EndYear = DateTime.Now.Year;
    }

    private void updateNumberList()
    {
        NumberList.Clear();
        DayList.Clear();

        switch (PeriodKey)
        {
            case "DAY":
                for (int i = 1; i <= 12; i++)
                    NumberList.Add(i);
                break;

            case "WEEK":
                for (int i = 1; i <= 53; i++)
                    NumberList.Add(i);
                break;

            case "MONTH":
                for (int i = 1; i <= 12; i++)
                    NumberList.Add(i);
                break;

            case "YEAR":
                break;
        }
    }

    private void updateDayList()
    {
        DayList.Clear();
        if (SelectedNumber <= 0) return;

        int days = DateTime.DaysInMonth(DateTime.Now.Year, SelectedNumber);
        for (int d = 1; d <= days; d++)
            DayList.Add(d);

        if (!DayList.Contains(SelectedDay))
            SelectedDay = DayList.FirstOrDefault();
    }
}
