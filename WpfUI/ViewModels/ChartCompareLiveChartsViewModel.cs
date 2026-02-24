using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using WpfUI.Models;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class ChartCompareLiveChartsViewModel : ViewModelBase
{
    private readonly CompareChartService _service;

    public ChartCompareLiveChartsViewModel(Address address, EnergyType energyType)
    {
        _service = new CompareChartService();

        CurrentAddress = address;
        CurrentEnergyType = energyType;

        LoadPeriodTypes();
        LoadYears();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart());

        ResetChart();
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

    #region Labels

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

    #endregion

    #region Properties

    public ObservableCollection<SelectionItem> PeriodTypes { get; } = new();
    public ObservableCollection<int> Years { get; } = new();
    public ObservableCollection<int> NumberList { get; } = new();
    public ObservableCollection<int> DayList { get; } = new();

    private SelectionItem _selectedPeriodType;
    public SelectionItem SelectedPeriodType
    {
        get => _selectedPeriodType;
        set { if (SetProperty(ref _selectedPeriodType, value)) { UpdateNumberList(); UpdateChart(); } }
    }

    public int StartYear { get => _startYear; set { if (SetProperty(ref _startYear, value)) UpdateChart(); } }
    private int _startYear;

    public int EndYear { get => _endYear; set { if (SetProperty(ref _endYear, value)) UpdateChart(); } }
    private int _endYear;

    public int SelectedNumber { get => _selectedNumber; set { if (SetProperty(ref _selectedNumber, value)) UpdateChart(); } }
    private int _selectedNumber;

    public int SelectedDay { get => _selectedDay; set { if (SetProperty(ref _selectedDay, value)) UpdateChart(); } }
    private int _selectedDay;

    public bool PredictMissingData { get => _predict; set { if (SetProperty(ref _predict, value)) UpdateChart(); } }
    private bool _predict;

    public bool ShowStacked { get => _stacked; set { if (SetProperty(ref _stacked, value)) UpdateChart(); } }
    private bool _stacked;

    public bool ShowByCategory { get => _sbCat; set { if (SetProperty(ref _sbCat, value)) UpdateChart(); } }
    private bool _sbCat = true;

    public bool ShowBySubCategory { get => _sbSub; set { if (SetProperty(ref _sbSub, value)) UpdateChart(); } }
    private bool _sbSub;

    public bool ShowByTotal { get => _sbTot; set { if (SetProperty(ref _sbTot, value)) UpdateChart(); } }
    private bool _sbTot;

    public bool ShowTypeRate { get => _stRate; set { if (SetProperty(ref _stRate, value)) UpdateChart(); } }
    private bool _stRate = true;

    public bool ShowTypeValue { get => _stValue; set { if (SetProperty(ref _stValue, value)) UpdateChart(); } }
    private bool _stValue;

    public bool ShowTypeEfficiency { get => _stEff; set { if (SetProperty(ref _stEff, value)) UpdateChart(); } }
    private bool _stEff;

    public bool IsEfficiencyVisible => CurrentEnergyType?.HasEnergyReturn ?? false;

    public string NumberLabel => SelectedPeriodType?.Key switch
    {
        "DAY" => "Month",
        "WEEK" => "Week",
        "MONTH" => "Month",
        _ => ""
    };

    public bool IsDayVisible => SelectedPeriodType?.Key == "DAY";

    #endregion

    #region Chart

    public ObservableCollection<ISeries> ChartSeries { get; } = new();
    public ObservableCollection<Axis> XAxes { get; } = new();
    public ObservableCollection<Axis> YAxes { get; } = new();

    public void UpdateChart()
    {
        if (SelectedPeriodType == null)
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
            GetShowType()
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

        UpdateLabels(result.Labels);
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

    #endregion

    #region Commands

    public ICommand ResetCommand { get; }
    public ICommand ExportCommand { get; }

    private void ResetChart()
    {
        PredictMissingData = true;
        ShowStacked = true;
        UpdateChart();
    }

    private void ExportChart()
    {
        _service.ExportToExcel(CurrentEnergyType);
    }

    #endregion

    #region Helpers

    private void LoadPeriodTypes()
    {
        foreach (var p in LibSelectionItemList.GetPeriodList())
            PeriodTypes.Add(p);
    }

    private void LoadYears()
    {
        for (int y = 2000; y <= DateTime.Now.Year; y++)
            Years.Add(y);

        StartYear = DateTime.Now.Year - 5;
        EndYear = DateTime.Now.Year;
    }

    private void UpdateNumberList()
    {
        NumberList.Clear();

        if (SelectedPeriodType.Key == "MONTH")
            for (int i = 1; i <= 12; i++) NumberList.Add(i);

        if (SelectedPeriodType.Key == "WEEK")
            for (int i = 1; i <= 53; i++) NumberList.Add(i);

        if (SelectedPeriodType.Key == "DAY")
        {
            for (int i = 1; i <= 12; i++) NumberList.Add(i);
            UpdateDayList();
        }
    }

    private void UpdateDayList()
    {
        DayList.Clear();
        int days = DateTime.DaysInMonth(DateTime.Now.Year, SelectedNumber);
        for (int d = 1; d <= days; d++) DayList.Add(d);
    }

    #endregion
}