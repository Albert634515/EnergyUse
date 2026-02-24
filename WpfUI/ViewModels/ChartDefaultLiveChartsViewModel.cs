using EnergyUse.Common.Enums;
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

namespace WpfUI.ViewModels;

public class ChartDefaultLiveChartsViewModel : ViewModelBase
{
    private readonly DefaultChartService _service;

    public ChartDefaultLiveChartsViewModel(Address address,
                                           EnergyType energyType,
                                           IEnumerable<EnergyType> energyTypes)
    {
        _service = new DefaultChartService();

        CurrentAddress = address;
        CurrentEnergyType = energyType;
        EnergyTypes = new ObservableCollection<EnergyType>(energyTypes ?? Enumerable.Empty<EnergyType>());

        LoadPeriodTypes();
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

    public ObservableCollection<EnergyType> EnergyTypes { get; }

    private EnergyType _selectedCompareEnergyType;
    public EnergyType SelectedCompareEnergyType
    {
        get => _selectedCompareEnergyType;
        set { if (SetProperty(ref _selectedCompareEnergyType, value)) UpdateChart(); }
    }

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

    #region Period / Options

    public ObservableCollection<SelectionItem> PeriodTypes { get; } = new();

    private SelectionItem _selectedPeriodType;
    public SelectionItem SelectedPeriodType
    {
        get => _selectedPeriodType;
        set { if (SetProperty(ref _selectedPeriodType, value)) UpdateChart(); }
    }

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

    private bool _predictMissingData = true;
    public bool PredictMissingData
    {
        get => _predictMissingData;
        set { if (SetProperty(ref _predictMissingData, value)) UpdateChart(); }
    }

    private bool _showStacked = true;
    public bool ShowStacked
    {
        get => _showStacked;
        set { if (SetProperty(ref _showStacked, value)) UpdateChart(); }
    }

    private bool _showAverage = true;
    public bool ShowAverage
    {
        get => _showAverage;
        set { if (SetProperty(ref _showAverage, value)) UpdateChart(); }
    }

    public bool ShowByCategory
    {
        get => _sbCat;
        set { if (SetProperty(ref _sbCat, value)) UpdateChart(); }
    }
    private bool _sbCat = true;

    public bool ShowBySubCategory
    {
        get => _sbSub;
        set { if (SetProperty(ref _sbSub, value)) UpdateChart(); }
    }
    private bool _sbSub;

    public bool ShowByTotal
    {
        get => _sbTot;
        set { if (SetProperty(ref _sbTot, value)) UpdateChart(); }
    }
    private bool _sbTot;

    public bool ShowTypeRate
    {
        get => _stRate;
        set { if (SetProperty(ref _stRate, value)) UpdateChart(); }
    }
    private bool _stRate = true;

    public bool ShowTypeValue
    {
        get => _stValue;
        set { if (SetProperty(ref _stValue, value)) UpdateChart(); }
    }
    private bool _stValue;

    public bool ShowTypeEfficiency
    {
        get => _stEff;
        set { if (SetProperty(ref _stEff, value)) UpdateChart(); }
    }
    private bool _stEff;

    public bool IsEfficiencyVisible => CurrentEnergyType?.HasEnergyReturn ?? false;

    #endregion

    #region Chart

    public ObservableCollection<ISeries> ChartSeries { get; } = new();
    public ObservableCollection<Axis> XAxes { get; } = new();
    public ObservableCollection<Axis> YAxes { get; } = new();

    public void UpdateChart()
    {
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
        InitDefaults();
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

    private void InitDefaults()
    {
        FromDate = DateTime.Now.AddMonths(-12);
        TillDate = DateTime.Now;

        SelectedPeriodType = PeriodTypes.FirstOrDefault(p => p.Key == "MONTH")
                             ?? PeriodTypes.FirstOrDefault();
    }

    #endregion
}