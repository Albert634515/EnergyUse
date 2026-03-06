using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using SkiaSharp;
using WpfUI.Models;
using EnergyUse.Common.Enums;

namespace WpfUI.ViewModels;

public class ChartCompareLiveChartsViewModel : ViewModelBase
{
    private readonly CompareChartService _service;

    // ✔ Deze twee horen bij jouw project
    public Address CurrentAddress { get; set; }
    public EnergyType CurrentEnergyType { get; set; }

    // ✔ Constructor met 2 parameters (zoals jouw project verwacht)
    public ChartCompareLiveChartsViewModel(Address address, EnergyType energyType)
    {
        CurrentAddress = address;
        CurrentEnergyType = energyType;

        _service = new CompareChartService();

        LoadPeriodTypes();
        LoadYears();

        ResetCommand = new RelayCommand(_ => ResetChart());
        ExportCommand = new RelayCommand(_ => ExportChart());

        PredictMissingData = true;
        ShowStacked = true;
        ShowByCategory = true;
        ShowTypeRate = true;

        ResetChart();
    }

    #region Labels

    public ChartLabel ConsumptionLabel { get; } = new();
    public ChartLabel ProductionLabel { get; } = new();
    public ChartLabel NettoLabel { get; } = new();

    #endregion

    #region Selecties

    public ObservableCollection<SelectionItem> PeriodTypes { get; } = new();
    public ObservableCollection<int> Years { get; } = new();
    public ObservableCollection<int> NumberList { get; } = new();
    public ObservableCollection<int> DayList { get; } = new();

    private SelectionItem _selectedPeriodType;
    public SelectionItem SelectedPeriodType
    {
        get => _selectedPeriodType;
        set
        {
            if (SetProperty(ref _selectedPeriodType, value))
            {
                UpdateNumberList();
                OnPropertyChanged(nameof(NumberLabel));
                OnPropertyChanged(nameof(IsDayVisible));
                UpdateChart();
            }
        }
    }

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
                if (SelectedPeriodType?.Key == "DAY")
                    UpdateDayList();
                UpdateChart();
            }
        }
    }

    private int _selectedDay;
    public int SelectedDay
    {
        get => _selectedDay;
        set { if (SetProperty(ref _selectedDay, value)) UpdateChart(); }
    }

    private bool _predict;
    public bool PredictMissingData
    {
        get => _predict;
        set { if (SetProperty(ref _predict, value)) UpdateChart(); }
    }

    private bool _stacked;
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

        // Labels (placeholder)
        ConsumptionLabel.Visible = true;
        ConsumptionLabel.Text = "Delivery: 12279";
        ConsumptionLabel.BackColor = Brushes.Orange;
        ConsumptionLabel.ForeColor = Brushes.Black;

        ProductionLabel.Visible = true;
        ProductionLabel.Text = "Return: 11490";
        ProductionLabel.BackColor = Brushes.LightGreen;
        ProductionLabel.ForeColor = Brushes.Black;

        NettoLabel.Visible = true;
        NettoLabel.Text = "Net: 789";
        NettoLabel.BackColor = Brushes.LightBlue;
        NettoLabel.ForeColor = Brushes.Black;
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
        ShowByCategory = true;
        ShowBySubCategory = false;
        ShowByTotal = false;
        ShowTypeRate = true;
        ShowTypeValue = false;
        ShowTypeEfficiency = false;

        if (PeriodTypes.Any())
            SelectedPeriodType = PeriodTypes.First();

        UpdateChart();
    }

    private void ExportChart()
    {
        _service.ExportToExcel();
    }

    #endregion

    #region Helpers

    private void LoadPeriodTypes()
    {
        PeriodTypes.Add(new SelectionItem(1, "DAY", "Day"));
        PeriodTypes.Add(new SelectionItem(2, "WEEK", "Week"));
        PeriodTypes.Add(new SelectionItem(3, "MONTH", "Month"));
        PeriodTypes.Add(new SelectionItem(4, "YEAR", "Year"));
    }

    private void LoadYears()
    {
        for (int y = 2020; y <= DateTime.Now.Year; y++)
            Years.Add(y);

        StartYear = DateTime.Now.Year - 1;
        EndYear = DateTime.Now.Year;
    }

    private void UpdateNumberList()
    {
        NumberList.Clear();
        DayList.Clear();

        switch (SelectedPeriodType?.Key)
        {
            case "MONTH":
                for (int i = 1; i <= 12; i++) NumberList.Add(i);
                SelectedNumber = DateTime.Now.Month;
                break;

            case "WEEK":
                for (int i = 1; i <= 53; i++) NumberList.Add(i);
                SelectedNumber = 1;
                break;

            case "DAY":
                for (int i = 1; i <= 12; i++) NumberList.Add(i);
                SelectedNumber = DateTime.Now.Month;
                UpdateDayList();
                SelectedDay = DateTime.Now.Day;
                break;
        }
    }

    private void UpdateDayList()
    {
        DayList.Clear();
        int days = DateTime.DaysInMonth(DateTime.Now.Year, SelectedNumber);
        for (int d = 1; d <= days; d++) DayList.Add(d);

        if (!DayList.Contains(SelectedDay))
            SelectedDay = DayList.FirstOrDefault();
    }

    #endregion
}