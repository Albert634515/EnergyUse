using EnergyUse.Common.Enums;
using EnergyUse.Models;
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
    private readonly CompareChartService _service;

    public Address? CurrentAddress { get; }
    public EnergyType? CurrentEnergyType { get; }

    private ObservableCollection<PeriodicData> _exportData = new();

    // ---------------------------------------------------------
    // MEMORY FOR DAY/WEEK/MONTH SELECTIONS
    // ---------------------------------------------------------
    private int _lastSelectedDay = 1;
    private int _lastSelectedWeek = 1;      // ← jouw keuze A
    private int _lastSelectedMonth = DateTime.Now.Month;

    public ChartCompareLiveChartsViewModel(Address address, EnergyType energyType, ILanguageService languageService)
    {
        CurrentAddress = address;
        CurrentEnergyType = energyType;

        _service = new CompareChartService(languageService);

        setPeriodTypes(languageService);
        setYears();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart(), _ => _exportData.Any());

        ResetChart();
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
                updateNumberList();   // ← BELANGRIJK

                ApplyPeriodChange();
                UpdateChart();
            }
        }
    }

    private void ApplyPeriodChange()
    {
        switch (PeriodKey)
        {
            case "DAY":
                SelectedNumber = _lastSelectedMonth;
                updateDayList();
                SelectedDay = _lastSelectedDay;
                break;

            case "WEEK":
                if (_lastSelectedWeek <= 0)
                    _lastSelectedWeek = 1;   // ← jouw keuze A
                SelectedNumber = _lastSelectedWeek;
                SelectedDay = 0;
                break;

            case "MONTH":
                SelectedNumber = _lastSelectedMonth;
                SelectedDay = 0;
                break;

            case "YEAR":
                SelectedNumber = 0;
                SelectedDay = 0;
                break;
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
        set { if (SetProperty(ref _startYear, value)) UpdateChart(); }
    }

    private int _endYear;
    public int EndYear
    {
        get => _endYear;
        set { if (SetProperty(ref _endYear, value)) UpdateChart(); }
    }

    private int _selectedNumber;
    public int SelectedNumber
    {
        get => _selectedNumber;
        set
        {
            if (SetProperty(ref _selectedNumber, value))
            {
                if (PeriodKey == "DAY")
                {
                    _lastSelectedMonth = value;
                    updateDayList();
                }

                if (PeriodKey == "WEEK")
                {
                    if (value <= 0)
                        value = 1;   // ← nooit week 0
                    _lastSelectedWeek = value;
                }

                if (PeriodKey == "MONTH")
                    _lastSelectedMonth = value;

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

                UpdateChart();
            }
        }
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
        set { if (SetProperty(ref _sbCat, value)) UpdateChart(); }
    }

    private bool _sbSub;
    public bool ShowBySubCategory
    {
        get => _sbSub;
        set { if (SetProperty(ref _sbSub, value)) UpdateChart(); }
    }

    private bool _sbTot;
    public bool ShowByTotal
    {
        get => _sbTot;
        set { if (SetProperty(ref _sbTot, value)) UpdateChart(); }
    }

    private bool _stRate = true;
    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value)) UpdateChart(); }
    }

    private bool _stValue;
    public bool ShowTypeValue
    {
        get => _stValue;
        set { if (SetProperty(ref _stValue, value)) UpdateChart(); }
    }

    private bool _stEff;
    public bool ShowTypeEfficiency
    {
        get => _stEff;
        set { if (SetProperty(ref _stEff, value)) UpdateChart(); }
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
        if (SelectedPeriodType == null || CurrentAddress == null || CurrentEnergyType == null)
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
            GetShowType());

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

    private void ResetChart()
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
            SelectedPeriodType = PeriodTypes.FirstOrDefault();

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