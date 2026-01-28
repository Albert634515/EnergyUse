using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using System.Drawing;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class Base
{
    #region ChartProperties

    internal UnitOfWork.Graphs? _unitOfWork { get; set; }
    internal ParameterGraph _graphParameter { get; set; } = new();
    // LibPeriodicDate is created by derived classes (needs db name)
    internal LibPeriodicDate? _libPeriodicDate { get; set; }
    internal List<PeriodicData> _periodicDataList { get; set; } = new();

    // UI-agnostic models stored in EnergyUse.Models.Common
    internal List<SeriesModel> _serieslist { get; set; } = new();
    internal List<AxisModel> _axisList { get; set; } = new();
    internal Dictionary<string, List<DatePoint>> _datePoints { get; set; } = new Dictionary<string, List<DatePoint>>();

    #endregion

    #region AddPointAndSeries

    internal void AddColumnSeriesToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt, bool showStacked = false)
    {
        var serieName = GetSeriesKey(serieType, energyTypeId);

        if (_datePoints.TryGetValue(serieName, out var points) && points.Count > 0)
        {
            Color color = GetColor(serieType, energyTypeId);

            var series = new SeriesModel
            {
                Name = serieName,
                SeriesKey = serieName,
                EnergyTypeId = energyTypeId,
                Points = points.Select(dp => new DatePoint(dp.DateTime, dp.Value)).ToList(),
                Color = color,
                IsStacked = showStacked,
                ScalesYAt = scalesYAt,
                IsLine = false,
            };

            _serieslist.Add(series);
        }
    }

    internal void AddLineSeriesToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt)
    {
        var serieName = GetSeriesKey(serieType, energyTypeId);

        if (_datePoints.TryGetValue(serieName, out var points) && points.Count > 0)
        {
            var series = new SeriesModel
            {
                Name = serieName,
                SeriesKey = serieName,
                EnergyTypeId = energyTypeId,
                Points = points.Select(dp => new DatePoint(dp.DateTime, dp.Value)).ToList(),
                ScalesYAt = scalesYAt,
                IsLine = true,
            };
            _serieslist.Add(series);
        }
    }

    internal void AddLineSeriesAvgToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt)
    {
        var serieName = GetSeriesKey(serieType, energyTypeId);

        if (_datePoints.TryGetValue(serieName, out var points) && points.Count > 0)
        {
            Color color = GetColor(serieType, energyTypeId);
            var series = new SeriesModel
            {
                Name = serieName,
                SeriesKey = serieName,
                EnergyTypeId = energyTypeId,
                Points = points.Select(dp => new DatePoint(dp.DateTime, dp.Value)).ToList(),
                Color = color,
                ScalesYAt = scalesYAt,
                IsLine = true,
            };
            _serieslist.Add(series);
        }
    }

    internal void AddDataPoint(
        ChartSeriesType seriesType,
        long energyTypeId,
        ShowType showType,
        PeriodicData periodicData,
        Period periodType = Period.Unknown,
        int roundingDigit = 2)
    {
        DatePoint? ratePeriod = null;
        string seriesKey = GetSeriesKey(seriesType, energyTypeId);

        if (showType == ShowType.Rate)
            ratePeriod = GetRateByPeriod(periodicData, seriesType, roundingDigit);
        else if (showType == ShowType.Value)
            ratePeriod = GetValueByPeriod(periodicData, seriesType, roundingDigit);
        else if (showType == ShowType.Efficiency)
        {
            if (_libPeriodicDate == null) throw new InvalidOperationException("LibPeriodicDate not initialized");
            ratePeriod = GetEfficiencyByPeriod(periodicData, seriesType, _graphParameter.Address.TotalCapacity, roundingDigit);
        }
        else if (showType == ShowType.AvgRate)
            ratePeriod = GetAvgRateByPeriod(periodicData, seriesType, periodType, roundingDigit);
        else if (showType == ShowType.AvgValue)
            ratePeriod = GetAvgValueByPeriod(periodicData, seriesType, periodType, roundingDigit);
        else
            throw new Exception("Unknown show type");

        if (ratePeriod != null)
        {
            double value = ratePeriod.Value;
            if (seriesType.ToString().ToLower().Contains("return"))
                value = -value;

            if (!_datePoints.ContainsKey(seriesKey))
                _datePoints[seriesKey] = new List<DatePoint>();

            var list = _datePoints[seriesKey];
            int idx = list.FindIndex(x => x.DateTime.Date == periodicData.ValueXDate);
            if (idx >= 0)
            {
                var existing = list[idx];
                var updated = new DatePoint(existing.DateTime, Math.Round(existing.Value + Math.Round(value, roundingDigit), roundingDigit));
                list[idx] = updated;
            }
            else
            {
                var dp = new DatePoint(periodicData.ValueXDate, Math.Round(value, roundingDigit));
                list.Add(dp);
            }
        }
    }

    #endregion

    #region GetPeriodData

    internal DatePoint GetEfficiencyByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, decimal totalCapacity, int roundingDigit = 2)
    {
        double rate = double.NaN;

        switch (chartSeriesType)
        {
            case ChartSeriesType.ReturnLowPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)_libPeriodicDate!.GetEfficiency(periodicData, SubEnergyType.ReturnLow, totalCapacity);
                break;
            case ChartSeriesType.ReturnNormalPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)_libPeriodicDate!.GetEfficiency(periodicData, SubEnergyType.ReturnNormal, totalCapacity);
                break;
            case ChartSeriesType.ReturnLow:
                if (periodicData.IsPredicted == false)
                    rate = (double)_libPeriodicDate!.GetEfficiency(periodicData, SubEnergyType.ReturnLow, totalCapacity);
                break;
            case ChartSeriesType.ReturnNormal:
                if (periodicData.IsPredicted == false)
                    rate = (double)_libPeriodicDate!.GetEfficiency(periodicData, SubEnergyType.ReturnNormal, totalCapacity);
                break;
            default:
                break;
        }

        return new DatePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
    }

    internal DatePoint GetValueByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, int roundingDigit = 2)
    {
        double rate = double.NaN;

        switch (chartSeriesType)
        {
            case ChartSeriesType.GrossValue:
                rate = (double)periodicData.ValueMonetaryY;
                break;
            case ChartSeriesType.Low:
                rate = (double)periodicData.ValueYMonetaryLow;
                break;
            case ChartSeriesType.LowPredicted:
                rate = (double)periodicData.ValueYMonetaryLow;
                break;
            case ChartSeriesType.ReturnLowPredicted:
                rate = (double)periodicData.ValueYMonetaryReturnLow;
                break;
            case ChartSeriesType.Normal:
                rate = (double)periodicData.ValueYMonetaryNormal;
                break;
            case ChartSeriesType.NormalPredicted:
                rate = (double)periodicData.ValueYMonetaryNormal;
                break;
            case ChartSeriesType.ReturnNormalPredicted:
                rate = (double)periodicData.ValueYMonetaryReturnNormal;
                break;
            case ChartSeriesType.ReturnLow:
                rate = (double)periodicData.ValueYMonetaryReturnLow;
                break;
            case ChartSeriesType.ReturnNormal:
                rate = (double)periodicData.ValueYMonetaryReturnNormal;
                break;
            default:
                break;
        }

        return new DatePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
    }

    internal DatePoint GetRateByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, int digitRounding = 2)
    {
        double rate = double.NaN;

        switch (chartSeriesType)
        {
            case ChartSeriesType.GrossValue:
                rate = (double)periodicData.ValueY;
                break;
            case ChartSeriesType.Low:
                if (periodicData.IsPredicted == false)
                    rate = (double)periodicData.ValueYLow;
                break;
            case ChartSeriesType.LowPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)periodicData.ValueYLow;
                break;
            case ChartSeriesType.ReturnLowPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)periodicData.ValueYReturnLow;
                break;
            case ChartSeriesType.Normal:
                if (periodicData.IsPredicted == false)
                    rate = (double)periodicData.ValueYNormal;
                break;
            case ChartSeriesType.NormalPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)periodicData.ValueYNormal;
                break;
            case ChartSeriesType.ReturnNormalPredicted:
                if (periodicData.IsPredicted == true)
                    rate = (double)periodicData.ValueYReturnNormal;
                break;
            case ChartSeriesType.ReturnLow:
                if (periodicData.IsPredicted == false)
                    rate = (double)periodicData.ValueYReturnLow;
                break;
            case ChartSeriesType.ReturnNormal:
                if (periodicData.IsPredicted == false)
                    rate = (double)periodicData.ValueYReturnNormal;
                break;
            case ChartSeriesType.Total:
                if (periodicData.IsPredicted == false)
                    rate = (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, digitRounding) - Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, digitRounding));
                break;
            default:
                break;
        }

        return new DatePoint(periodicData.ValueXDate, Math.Round(rate, digitRounding));
    }

    internal DatePoint? GetAvgRateByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, Period periodType, int roundingDigit = 2)
    {
        double rate = double.NaN;
        DateTime maxDate = _periodicDataList.Max(x => x.ValueXDate);

        var maxAvgValue = LibGraphGeneral.GetMaxAvg(periodType, maxDate, _periodicDataList.Count);
        if (periodicData.ValueXDate < maxDate)
            maxAvgValue = periodicData.ValueX;

        var periodicDataList = _periodicDataList.Where(x => x.ValueX <= maxAvgValue).ToList();
        if (periodicDataList.Count == 0)
            return null;

        switch (chartSeriesType)
        {
            case ChartSeriesType.AvgLow:
                rate = (double)periodicDataList.Average(x => x.ValueYLow);
                break;
            case ChartSeriesType.AvgNormal:
                rate = (double)periodicDataList.Average(x => x.ValueYNormal);
                break;
            case ChartSeriesType.ReturnAvgLowDelivery:
                rate = (double)periodicDataList.Average(x => x.ValueYReturnLow);
                break;
            case ChartSeriesType.ReturnAvgNormalDelivery:
                rate = (double)periodicDataList.Average(x => x.ValueYReturnNormal);
                break;
            default:
                break;
        }

        return new DatePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
    }

    internal DatePoint? GetAvgValueByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, Period periodType, int roundingDigit = 2)
    {
        double rate = double.NaN;
        var maxAvgValue = LibGraphGeneral.GetMaxAvg(periodType, _graphParameter.Till, _periodicDataList.Count);
        var periodicDataList = _periodicDataList.Where(x => x.ValueX <= maxAvgValue).ToList();
        if (periodicDataList.Count == 0)
            return null;

        switch (chartSeriesType)
        {
            case ChartSeriesType.AvgLow:
                rate = (double)periodicDataList.Average(x => x.ValueYMonetaryLow);
                break;
            case ChartSeriesType.AvgNormal:
                rate = (double)periodicDataList.Average(x => x.ValueYMonetaryNormal);
                break;
            case ChartSeriesType.ReturnAvgLowDelivery:
                rate = (double)periodicDataList.Average(x => x.ValueYMonetaryReturnLow);
                break;
            case ChartSeriesType.ReturnAvgNormalDelivery:
                rate = (double)periodicDataList.Average(x => x.ValueYMonetaryReturnNormal);
                break;
            default:
                break;
        }

        return new DatePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
    }

    #endregion

    #region BaseMethods

    public string GetSeriesKey(ChartSeriesType seriesType, long energyId)
    {
        return $"{seriesType}_{energyId}";
    }

    // public API returns model series
    public List<SeriesModel> GetSeries()
    {
        return _serieslist;
    }

    public void AddAxis(AxisModel axis)
    {
        _axisList.Add(axis);
    }

    public AxisModel GetAxis(string name)
    {
        return new AxisModel
        {
            Name = name,
            Position = 0,
            TextSize = 14,
            PaddingLeft = 20,
            PaddingTop = 0,
            ShowSeparatorLines = false
        };
    }

    public List<AxisModel> GetAxisLists()
    {
        return _axisList;
    }

    public List<PeriodicData> GetDataList()
    {
        return _periodicDataList;
    }

    public bool HasPredictedData()
    {
        return _periodicDataList.Any(c => c.IsPredicted);
    }

    internal void ResetSeries()
    {
        _serieslist = new List<SeriesModel>();
    }

    internal List<string> GetTypeList()
    {
        return _periodicDataList.Select(x => x.ValueXString).Distinct().ToList();
    }

    internal ParameterPeriod GetParameterPeriod(Models.EnergyType energyType, bool compare = false)
    {
        ParameterPeriod parameterPeriod = new()
        {
            EnergyType = energyType,
            AddressId = _graphParameter.Address.Id,
            ShowType = _graphParameter.ShowType,
            PeriodType = _graphParameter.PeriodType,
            PredictMissingData = _graphParameter.PredictMissingData,
            TarifGroupId = _graphParameter.TarifGroupId,
            Month = _graphParameter.Month,
            Week = _graphParameter.Week,
            Day = _graphParameter.Day
        };

        if (compare == false)
        {
            parameterPeriod.StartRange = _graphParameter.From;
            parameterPeriod.EndRange = _graphParameter.Till;
            parameterPeriod.SetMiddleOfYear = false;
        }
        else
        {
            int daysInMonth = DateTime.DaysInMonth(_graphParameter.YearEnd, DateTime.Now.Month);
            parameterPeriod.StartRange = new DateTime(_graphParameter.YearStart, 1, 1);
            parameterPeriod.EndRange = new DateTime(_graphParameter.YearEnd, 12, daysInMonth);
            parameterPeriod.SetMiddleOfYear = true;
        }

        return parameterPeriod;
    }

    #endregion

    #region Color

    internal Color GetColor(ChartSeriesType chartSeriesType, long energyTypeId)
    {
        string colorKey = LibGraphGeneral.GetColorKey(chartSeriesType, energyTypeId);

        string chartSeriesName = chartSeriesType.ToString();
        bool isPredicted = chartSeriesName.Contains("Predicted");

        var libSettings = new LibSettings(_graphParameter.DbName);
        Color color = libSettings.GetChartColor(colorKey);

        if (isPredicted)
            color = ChangeColorBrightness(color, 40.45E-2f);

        return color;
    }

    internal static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = (float)color.R;
        float green = (float)color.G;
        float blue = (float)color.B;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
    }

    #endregion
}