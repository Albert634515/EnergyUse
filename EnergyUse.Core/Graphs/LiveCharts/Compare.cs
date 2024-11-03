using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using System.Drawing;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class Compare : Base
{
    public Compare(ParameterGraph graphParameter)
    {
        _graphParameter = graphParameter;
        _unitOfWork = new UnitOfWork.Graphs(graphParameter.DbName);
        _libPeriodicDate = new(_graphParameter.DbName);

        loadChart();
    }

    private void loadChart()
    {
        if (_graphParameter.YearEnd <= 0 || _graphParameter.YearStart <= 0)
            return;
        if (_graphParameter.PeriodType == Period.Unknown)
            return;

        try
        {
            if (_graphParameter.Address != null && _graphParameter.Address.TariffGroup != null)
                _graphParameter.TarifGroupId = _graphParameter.Address.TariffGroup.Id;

            if (_graphParameter.EnergyTypeList != null && _graphParameter.EnergyTypeList.Count > 0 && _graphParameter.Address != null)
            {
                ResetSeries();

                if (_graphParameter.ShowBy == ShowBy.Category)
                    GetChartSeriesPerPeriod();
                else if (_graphParameter.ShowBy == ShowBy.SubCategory)
                    GetChartSeriesPerPeriodBySubCategory();
                else if (_graphParameter.ShowBy == ShowBy.Total)
                    GetChartSeriesPerPeriodByTotal();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region GetChartSeriesPerPeriod

    private void GetChartSeriesPerPeriod()
    {
        ResetDataPoints();
        var typeCounter = -1;

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            ParameterPeriod parameterPeriod = GetParameterPeriod(energyType, true);
            _periodicDataList = _libPeriodicDate.GetRange(parameterPeriod);
            typeCounter++;

            foreach (PeriodicData periodicData in _periodicDataList)
            {
                if (_graphParameter.ShowType != ShowType.Efficiency)
                    AddDataPoint(ChartSeriesType.GrossValue, energyType.Id, _graphParameter.ShowType, periodicData);

                if (periodicData.IsPredicted == false && _graphParameter.ShowType != ShowType.Efficiency)
                {
                    AddDataPoint(ChartSeriesType.Low, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.Normal, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnLow, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnNormal, energyType.Id, _graphParameter.ShowType, periodicData);
                }
                else if (periodicData.IsPredicted == true && _graphParameter.ShowType != ShowType.Efficiency)
                {
                    AddDataPoint(ChartSeriesType.LowPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.NormalPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnLowPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnNormalPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                }
                else if (periodicData.IsPredicted == false && _graphParameter.ShowType == ShowType.Efficiency)
                {
                    AddDataPoint(ChartSeriesType.ReturnLow, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnNormal, energyType.Id, _graphParameter.ShowType, periodicData);
                }
                else if (periodicData.IsPredicted == true && _graphParameter.ShowType == ShowType.Efficiency)
                {
                    AddDataPoint(ChartSeriesType.ReturnLowPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                    AddDataPoint(ChartSeriesType.ReturnNormalPredicted, energyType.Id, _graphParameter.ShowType, periodicData);
                }
            }                

            if (_graphParameter.ShowType != ShowType.Efficiency)
            {
                AddColumnSeriesToList(ChartSeriesType.Normal, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                if (HasPredictedData())
                    AddColumnSeriesToList(ChartSeriesType.NormalPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);

                if (energyType != null && energyType.HasNormalAndLow)
                {
                    AddColumnSeriesToList(ChartSeriesType.Low, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                    if (HasPredictedData())
                        AddColumnSeriesToList(ChartSeriesType.LowPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                }
            }

            if (energyType != null && energyType.HasEnergyReturn)
            {
                AddColumnSeriesToList(ChartSeriesType.ReturnLow, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                AddColumnSeriesToList(ChartSeriesType.ReturnNormal, energyType.Id, typeCounter, _graphParameter.ShowStacked);

                if (HasPredictedData())
                {
                    AddColumnSeriesToList(ChartSeriesType.ReturnLowPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                    AddColumnSeriesToList(ChartSeriesType.ReturnNormalPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                }
            }

            if (energyType != null && _graphParameter.ShowType != ShowType.Efficiency)
                AddLineSeriesToList(ChartSeriesType.GrossValue, energyType.Id, typeCounter);
        }
    }

    private void ResetDataPoints()
    {
        _datePoints = new Dictionary<string, ObservableCollection<DateTimePoint>>();

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            var energyTypeId = energyType.Id;

            _datePoints.Add(GetSeriesKey(ChartSeriesType.GrossValue, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.Low, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.LowPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());

            _datePoints.Add(GetSeriesKey(ChartSeriesType.Normal, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.NormalPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());

            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnLow, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnLowPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnAvgLowDelivery, energyTypeId), new ObservableCollection<DateTimePoint>());

            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnNormal, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnNormalPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ReturnAvgNormalDelivery, energyTypeId), new ObservableCollection<DateTimePoint>());
        }
    }

    #endregion

    #region GetChartSeriesPerPeriodBySubCategory

    private void GetChartSeriesPerPeriodBySubCategory()
    {
        ResetDataPointsSubCategory();
        var typeCounter = -1;

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            ParameterPeriod parameterPeriod = GetParameterPeriod(energyType, true);
            _periodicDataList = _libPeriodicDate.GetRange(parameterPeriod);
            typeCounter++;

            var energyTypeId = energyType.Id;
            var consumedKey = GetSeriesKey(ChartSeriesType.Consumed, energyTypeId);
            var consumedPredictedKey = GetSeriesKey(ChartSeriesType.ConsumedPredicted, energyTypeId);
            var producedKey = GetSeriesKey(ChartSeriesType.Produced, energyTypeId);
            var producedPredictedKey = GetSeriesKey(ChartSeriesType.ProducedPredicted, energyTypeId);
            var grossKey = GetSeriesKey(ChartSeriesType.GrossValue, energyTypeId);

            foreach (PeriodicData periodicData in _periodicDataList)
            {
                if (_graphParameter.ShowType == ShowType.Rate && periodicData.IsPredicted == false)
                {
                    _datePoints[consumedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, 2))));
                    _datePoints[producedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, 2))));
                    _datePoints[grossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Rate && periodicData.IsPredicted == true)
                {
                    _datePoints[consumedPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, 2))));
                    _datePoints[producedPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, 2))));
                    _datePoints[grossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Value && periodicData.IsPredicted == false)
                {
                    _datePoints[consumedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryLow + periodicData.ValueYMonetaryNormal, 2))));
                    _datePoints[producedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryReturnLow + periodicData.ValueYMonetaryReturnNormal, 2))));
                    _datePoints[grossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueMonetaryY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Value && periodicData.IsPredicted == true)
                {
                    _datePoints[consumedPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryLow + periodicData.ValueYMonetaryNormal, 2))));
                    _datePoints[producedPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryReturnLow + periodicData.ValueYMonetaryReturnNormal, 2))));
                    _datePoints[grossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueMonetaryY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Efficiency && periodicData.IsPredicted == false)
                {
                    _datePoints[producedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)_libPeriodicDate.GetEfficiencyTotal(periodicData, _graphParameter.Address.TotalCapacity)));
                }
                else if (_graphParameter.ShowType == ShowType.Efficiency && periodicData.IsPredicted == true)
                {
                    _datePoints[producedPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)_libPeriodicDate.GetEfficiencyTotal(periodicData, _graphParameter.Address.TotalCapacity)));
                }
            }

            if (_graphParameter.ShowType != ShowType.Efficiency)
            {
                AddColumnSeriesToList(ChartSeriesType.Consumed, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                if (HasPredictedData())
                    AddColumnSeriesToList(ChartSeriesType.ConsumedPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
            }
            if (energyType != null && energyType.HasEnergyReturn)
            {
                AddColumnSeriesToList(ChartSeriesType.Produced, energyType.Id, typeCounter, _graphParameter.ShowStacked);
                if (HasPredictedData())
                    AddColumnSeriesToList(ChartSeriesType.ProducedPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
            }

            if (energyType != null && _graphParameter.ShowType != ShowType.Efficiency)
                AddLineSeriesToList(ChartSeriesType.GrossValue, energyType.Id, typeCounter);
        }
    }

    private void ResetDataPointsSubCategory()
    {
        _datePoints = new Dictionary<string, ObservableCollection<DateTimePoint>>();

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            var energyTypeId = energyType.Id;

            _datePoints.Add(GetSeriesKey(ChartSeriesType.GrossValue, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.Consumed, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ConsumedPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.Produced, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.ProducedPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());
        }
    }

    #endregion

    #region GetChartSeriesPerPeriodByTotal

    private void GetChartSeriesPerPeriodByTotal()
    {
        resetDataPointsTotal();
        var typeCounter = -1;

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            ParameterPeriod parameterPeriod = GetParameterPeriod(energyType, true);
            _periodicDataList = _libPeriodicDate.GetRange(parameterPeriod);
            typeCounter++;

            var energyTypeId = energyType.Id;
            var totalKey = GetSeriesKey(ChartSeriesType.Total, energyTypeId);
            var totalGrossKey = GetSeriesKey(ChartSeriesType.GrossValue, energyTypeId);
            var totalPredictedKey = GetSeriesKey(ChartSeriesType.TotalPredicted, energyTypeId);

            foreach (PeriodicData periodicData in _periodicDataList)
            {
                if (_graphParameter.ShowType == ShowType.Rate && periodicData.IsPredicted == false)
                {
                    _datePoints[totalKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, 2) - Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, 2))));
                    _datePoints[totalGrossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Rate && periodicData.IsPredicted == true)
                {
                    _datePoints[totalPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, 2) - Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, 2))));
                    _datePoints[totalGrossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Value && periodicData.IsPredicted == false)
                {
                    _datePoints[totalKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryLow + periodicData.ValueYMonetaryNormal, 2) - Math.Round(periodicData.ValueYMonetaryReturnLow + periodicData.ValueYMonetaryReturnNormal, 2))));
                    _datePoints[totalGrossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueMonetaryY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Value && periodicData.IsPredicted == true)
                {
                    _datePoints[totalPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)(Math.Round(periodicData.ValueYMonetaryLow + periodicData.ValueYMonetaryNormal, 2) - Math.Round(periodicData.ValueYMonetaryReturnLow + periodicData.ValueYMonetaryReturnNormal, 2))));
                    _datePoints[totalGrossKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)Math.Round(periodicData.ValueMonetaryY, 2)));
                }
                else if (_graphParameter.ShowType == ShowType.Efficiency && periodicData.IsPredicted == false)
                {
                    _datePoints[totalKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)_libPeriodicDate.GetEfficiencyTotal(periodicData, _graphParameter.Address.TotalCapacity)));
                }
                else if (_graphParameter.ShowType == ShowType.Efficiency && periodicData.IsPredicted == true)
                {
                    _datePoints[totalPredictedKey].Add(new DateTimePoint(periodicData.ValueXDate, (double)_libPeriodicDate.GetEfficiencyTotal(periodicData, _graphParameter.Address.TotalCapacity)));
                }
            }

            AddColumnSeriesToList(ChartSeriesType.Total, energyType.Id, typeCounter, _graphParameter.ShowStacked);
            AddColumnSeriesToList(ChartSeriesType.TotalPredicted, energyType.Id, typeCounter, _graphParameter.ShowStacked);
            if (_graphParameter.ShowType != ShowType.Efficiency)
                AddLineSeriesToList(ChartSeriesType.GrossValue, energyType.Id, typeCounter);
        }
    }

    private void resetDataPointsTotal()
    {
        _datePoints = new Dictionary<string, ObservableCollection<DateTimePoint>>();

        foreach (var energyType in _graphParameter.EnergyTypeList)
        {
            var energyTypeId = energyType.Id;

            _datePoints.Add(GetSeriesKey(ChartSeriesType.GrossValue, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.Total, energyTypeId), new ObservableCollection<DateTimePoint>());
            _datePoints.Add(GetSeriesKey(ChartSeriesType.TotalPredicted, energyTypeId), new ObservableCollection<DateTimePoint>());
        }
    }

    #endregion

    #region General

    public Dictionary<string, ResultLabel> GetResultLabelsPerPeriod(Models.EnergyType energyType)
    {
        var libSettings = new LibSettings(_graphParameter.DbName);
        Dictionary<string, ResultLabel> keyValuePairs = new();
        bool hasSolarPanels = false;

        if (_graphParameter.Address != null)
            hasSolarPanels = _graphParameter.Address.SolarPanelsAvailable;

        ResultLabel resultLabel = new();
        resultLabel.LabelVisibility = true;
        resultLabel.LabelBackColor = libSettings.GetChartColor($"Color{SubEnergyType.Normal}{energyType.Id}");
        resultLabel.LabelForeColor = Color.White;
        resultLabel.LabelText = $"Delivery: {Math.Round(_periodicDataList.Sum(x => x.ValueYLow + x.ValueYNormal), 2)}";
        keyValuePairs.Add("Consumption", resultLabel);

        if (energyType.HasEnergyReturn && hasSolarPanels)
        {
            resultLabel = new();
            resultLabel.LabelVisibility = true;
            resultLabel.LabelBackColor = libSettings.GetChartColor(energyType, SubEnergyType.ReturnNormal);
            resultLabel.LabelText = $"Return delivery: {Math.Round(_periodicDataList.Sum(x => x.ValueYReturnLow + x.ValueYReturnNormal), 2)}";
            keyValuePairs.Add("Production", resultLabel);

            resultLabel = new();
            resultLabel.LabelVisibility = true;
            resultLabel.LabelBackColor = libSettings.GetChartColor("GrossValue");
            resultLabel.LabelText = $"Netto: {Math.Round(_periodicDataList.Sum(x => x.ValueY), 2)}";
            keyValuePairs.Add("Netto", resultLabel);
        }
        else
        {
            resultLabel = new();
            keyValuePairs.Add("Production", resultLabel);
            resultLabel = new();
            keyValuePairs.Add("Netto", resultLabel);
        }

        return keyValuePairs;
    }

    #endregion
}