using EnergyUse.Models.Common;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class Rates : Base
{
    public Rates(ParameterGraph graphParameter)
        : this(graphParameter, buildChart: true)
    {
    }

    private Rates(ParameterGraph graphParameter, bool buildChart)
    {
        _graphParameter = graphParameter;
        _unitOfWork = new UnitOfWork.Graphs(_graphParameter.DbName);

        if (buildChart)
            getChart().GetAwaiter().GetResult();
    }

    public static async Task<Rates> CreateAsync(ParameterGraph graphParameter)
    {
        var chart = new Rates(graphParameter, buildChart: false);
        await chart.getChart();
        return chart;
    }

    private async Task getChart()
    {
        try
        {
            ResetSeries();

            if (_graphParameter.EnergyTypeList != null && _graphParameter.EnergyTypeList.Count > 0)
            {
                if (_graphParameter.ShowType == Common.Enums.ShowType.Unit)
                    await getChartSeriesPerCostCategoryAndUnit(
                        _graphParameter.EnergyTypeList,
                        _graphParameter.From,
                        _graphParameter.Till,
                        _graphParameter.TarifGroupId);
                else
                    await getChartSeriesPerCostCategory(_graphParameter.EnergyTypeList);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region GetChartSeriesPerCostCategoryAndUnit

    private async Task getChartSeriesPerCostCategoryAndUnit(List<Models.EnergyType> energyTypes, DateTime startDate, DateTime endDate, Int64 tarifGroupId)
    {
        var typeCounter = -1;
        var rateCache = new Dictionary<(long EnergyTypeId, long CostCategoryId, long TariffGroupId), List<Models.Rate>>();

        async Task<List<Models.Rate>> getRates(long energyTypeId, long costCategoryId, long categoryTariffGroupId)
        {
            var key = (energyTypeId, costCategoryId, categoryTariffGroupId);
            if (!rateCache.TryGetValue(key, out var rates))
            {
                rates = (await _unitOfWork!.RateRepo.SelectByCostCategoryAndDate(
                    energyTypeId,
                    costCategoryId,
                    startDate,
                    endDate,
                    categoryTariffGroupId)).ToList();
                rateCache[key] = rates;
            }

            return rates;
        }

        static Models.Rate? getRateForDate(IEnumerable<Models.Rate> rates, DateTime date) =>
            rates.Where(rate => rate.StartRate.Date <= date.Date && rate.EndRate.Date >= date.Date)
                 .OrderByDescending(rate => rate.StartRate)
                 .FirstOrDefault();

        foreach (var energyType in energyTypes)
        {
            var energyTypeId = energyType.Id;
            typeCounter++;

            // use available categories as fallback
            List<Models.CostCategory> costCategoryList = _graphParameter.CostCategoryList ?? new List<Models.CostCategory>();
            DateTime chartStartDate = startDate.Day == 1
                ? startDate.Date
                : new DateTime(startDate.Year, startDate.Month, 1).AddMonths(1);
            DateTime chartEndDate = endDate;

            while (chartStartDate <= chartEndDate)
            {
                if (chartStartDate.Day == 1)
                {
                    //Loop selected categories
                    foreach (Models.CostCategory mainCostCategory in costCategoryList)
                    {
                        string unitName = $"{mainCostCategory.Name} {mainCostCategory.Unit}";
                        var categoryTariffGroupId = mainCostCategory.TariffGroup?.Id
                                                    ?? _graphParameter.Address?.TariffGroup?.Id
                                                    ?? tarifGroupId;

                        // Calculate per main category
                        var ratesEnum = await getRates(energyTypeId, mainCostCategory.Id, categoryTariffGroupId);
                        Models.Rate? rate = getRateForDate(ratesEnum, chartStartDate);
                        PeriodicData? periodicData;
                        if (rate != null)
                        {
                            periodicData = _periodicDataList.Where(x => x.ValueXString == unitName && x.ValueXDate == chartStartDate).FirstOrDefault();
                            if (periodicData == null)
                            {
                                periodicData = new PeriodicData
                                {
                                    ValueXString = unitName,
                                    ValueXDate = chartStartDate
                                };

                                _periodicDataList.Add(periodicData);
                            }

                            periodicData.ValueY += rate.RateValue;
                        }

                        if (mainCostCategory.EnergySubType != null && (mainCostCategory.EnergySubType.Id == 1 || mainCostCategory.EnergySubType.Id == 2))
                        {
                            foreach (var otherCostCategory in costCategoryList.Where(m => m.EnergySubType != null && m.EnergySubType.Id == 5))
                            {
                                unitName = $"{mainCostCategory.Name} {otherCostCategory.Unit}";

                                //Toevoegen sub cat
                                var tariffGroupIdForOther = otherCostCategory.TariffGroup?.Id
                                                            ?? _graphParameter.Address?.TariffGroup?.Id
                                                            ?? tarifGroupId;
                                var ratesEnum2 = await getRates(energyTypeId, otherCostCategory.Id, tariffGroupIdForOther);
                                rate = getRateForDate(ratesEnum2, chartStartDate);
                                if (rate != null)
                                {
                                    periodicData = _periodicDataList.Where(x => x.ValueXString == unitName && x.ValueXDate == chartStartDate).FirstOrDefault();
                                    if (periodicData == null)
                                    {
                                        periodicData = new PeriodicData
                                        {
                                            ValueXString = unitName,
                                            ValueXDate = chartStartDate
                                        };

                                        _periodicDataList.Add(periodicData);
                                    }

                                    periodicData.ValueY += rate.RateValue;
                                }
                            }
                        }
                    }
                }

                chartStartDate = chartStartDate.AddMonths(1);
            }

            // Creating a list of series (now core model SeriesModel)
            var typeList = GetTypeList();
            foreach (var item in typeList)
            {
                var series = new SeriesModel
                {
                    Name = item,
                    SeriesKey = item,
                    EnergyTypeId = energyTypeId,
                    Points = GetValueListY(item),
                    ScalesYAt = typeCounter,
                    IsLine = true
                };

                _serieslist.Add(series);
            }
        }
    }

    #endregion

    #region GetChartSeriesPerCostCategory

    private async Task getChartSeriesPerCostCategory(List<Models.EnergyType> energyTypes)
    {
        var typeCounter = -1;

        foreach (var energyType in energyTypes)
        {
            var energyTypeId = energyType.Id;
            typeCounter++;

            var costCategories = _graphParameter.CostCategoryList ?? Enumerable.Empty<Models.CostCategory>();

            foreach (Models.CostCategory costCategory in costCategories)
            {
                long tarifGroupId = 0;
                Models.Rate? lastRate = null;
                var address = _graphParameter.Address ?? new Models.Address();

                var tarifGroup = costCategory.TariffGroup ?? address.TariffGroup;
                if (tarifGroup != null)
                    tarifGroupId = tarifGroup.Id;

                var rateList = await _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyType.Id, costCategory.Id, _graphParameter.From, _graphParameter.Till, tarifGroupId);
                foreach (Models.Rate rate in rateList.OrderBy(rate => rate.StartRate))
                {
                    if (_graphParameter.ShowMonthlyDataPoints)
                    {
                        foreach (var pointDate in GetMonthlyPointDates(rate, _graphParameter.From, _graphParameter.Till))
                            addRateToList(costCategory.Name, rate.RateValue, pointDate);
                    }
                    else
                    {
                        addRateToList(costCategory.Name, rate.RateValue, rate.StartRate);
                    }

                    lastRate = rate;
                }

                if (!_graphParameter.ShowMonthlyDataPoints && lastRate != null && lastRate.Id > 0)
                    addRateToList(costCategory.Name, lastRate.RateValue, lastRate.EndRate);
            }

            // Creating a list of series
            var typeList = GetTypeList();
            foreach (var item in typeList)
            {
                var series = new SeriesModel
                {
                    Name = item,
                    SeriesKey = item,
                    EnergyTypeId = energyTypeId,
                    Points = GetValueListY2(item),
                    ScalesYAt = typeCounter,
                    IsLine = true
                };

                _serieslist.Add(series);
            }
        }
    }

    private void addRateToList(string costCategoryName, decimal rateValue, DateTime rateDate)
    {
        PeriodicData periodicData = new()
        {
            ValueXString = costCategoryName
        };
        periodicData.ValueY2 += (double)Math.Round(rateValue, 4);
        periodicData.ValueXDate = rateDate;

        if (_graphParameter != null)
        {
            if (periodicData.ValueXDate < _graphParameter.From)
                periodicData.ValueXDate = _graphParameter.From;
            if (periodicData.ValueXDate > _graphParameter.Till)
                periodicData.ValueXDate = _graphParameter.Till;
        }

        _periodicDataList.Add(periodicData);
    }

    private static IEnumerable<DateTime> GetMonthlyPointDates(Models.Rate rate, DateTime from, DateTime till)
    {
        var start = rate.StartRate.Date < from.Date ? from.Date : rate.StartRate.Date;
        var end = rate.EndRate.Date > till.Date ? till.Date : rate.EndRate.Date;
        if (start > end)
            yield break;

        yield return start;

        var monthStart = new DateTime(start.Year, start.Month, 1).AddMonths(1);
        while (monthStart < end)
        {
            yield return monthStart;
            monthStart = monthStart.AddMonths(1);
        }

        if (end != start)
            yield return end;
    }

    private List<DatePoint> GetValueListY(string itemType)
    {
        var dateList = _periodicDataList.OrderBy(o => o.ValueXDate).Select(x => x.ValueXDate).Distinct().ToList();
        var valueList = new List<DatePoint>();
        foreach (DateTime day in dateList)
        {
            foreach (PeriodicData periodicData2 in _periodicDataList.Where(c => c.ValueXDate == day && c.ValueXString == itemType))
            {
                var value = periodicData2.ValueY;
                var dataPoint = new DatePoint(day, (double?)value ?? double.NaN);
                valueList.Add(dataPoint);
            }
        }

        return valueList;
    }

    private List<DatePoint> GetValueListY2(string itemType)
    {
        var dateList = _periodicDataList.OrderBy(o => o.ValueXDate).Select(x => x.ValueXDate).Distinct().ToList();
        var valueList = new List<DatePoint>();
        foreach (DateTime day in dateList)
        {
            foreach (PeriodicData periodicData2 in _periodicDataList.Where(c => c.ValueXDate == day && c.ValueXString == itemType))
            {
                var value = periodicData2.ValueY2;
                var dataPoint = new DatePoint(day, (double?)value ?? double.NaN);
                valueList.Add(dataPoint);
            }
        }

        return valueList;
    }

    #endregion
}
