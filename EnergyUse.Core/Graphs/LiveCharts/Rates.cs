using EnergyUse.Models.Common;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class Rates : Base
{
    public Rates(ParameterGraph graphParameter)
    {
        _graphParameter = graphParameter;
        _unitOfWork = new UnitOfWork.Graphs(_graphParameter.DbName);

        getChart();
    }

    private void getChart()
    {
        try
        {
            ResetSeries();

            if (_graphParameter.EnergyTypeList != null && _graphParameter.EnergyTypeList.Count > 0)
            {
                if (_graphParameter.ShowType == Common.Enums.ShowType.Unit)
                    getChartSeriesPerCostCategoryAndUnit(_graphParameter.EnergyTypeList, _graphParameter.From, _graphParameter.Till, 1);
                else
                    getChartSeriesPerCostCategory(_graphParameter.EnergyTypeList);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region GetChartSeriesPerCostCategoryAndUnit

    private void getChartSeriesPerCostCategoryAndUnit(List<Models.EnergyType> energyTypes, DateTime startDate, DateTime endDate, Int64 tarifGroupId)
    {
        var typeCounter = -1;

        foreach (var energyType in energyTypes)
        {
            var energyTypeId = energyType.Id;
            typeCounter++;

            // use available categories as fallback
            List<Models.CostCategory> costCategoryList = _graphParameter.CostCategoryList ?? new List<Models.CostCategory>();
            DateTime chartStartDate = startDate;
            DateTime chartEndDate = endDate;

            while (chartStartDate.AddDays(1) <= chartEndDate)
            {
                if (chartStartDate.Day == 1)
                {
                    //Loop selected categories
                    foreach (Models.CostCategory mainCostCategory in costCategoryList)
                    {
                        string unitName = $"{mainCostCategory.Name} {mainCostCategory.Unit}";
                        var tarifGroup = mainCostCategory.TariffGroup != null && mainCostCategory.TariffGroup.Id <= 0
                            ? mainCostCategory.TariffGroup
                            : _graphParameter.Address?.TariffGroup;
                        if (tarifGroup != null)
                            tarifGroupId = tarifGroup.Id;

                        // Calculate per main category
                        var ratesEnum = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyTypeId, mainCostCategory.Id, chartStartDate, chartStartDate, tarifGroupId) ?? Enumerable.Empty<Models.Rate>();
                        Models.Rate? rate = ratesEnum.FirstOrDefault();
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
                                var tariffGroupIdForOther = otherCostCategory.TariffGroup?.Id ?? 0;
                                var ratesEnum2 = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyTypeId, otherCostCategory.Id, chartStartDate, chartStartDate, (long)tariffGroupIdForOther) ?? Enumerable.Empty<Models.Rate>();
                                rate = ratesEnum2.FirstOrDefault();
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

                chartStartDate = chartStartDate.AddDays(1);
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
                    IsLine = true,
                };

                _serieslist.Add(series);
            }
        }
    }

    #endregion

    #region GetChartSeriesPerCostCategory

    private void getChartSeriesPerCostCategory(List<Models.EnergyType> energyTypes)
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

                var rateList = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyType.Id, costCategory.Id, _graphParameter.From, _graphParameter.Till, tarifGroupId) ?? Enumerable.Empty<Models.Rate>();
                foreach (Models.Rate rate in rateList)
                {
                    addRateToList(costCategory.Name, rate.RateValue, rate.StartRate);
                    lastRate = rate;
                }

                // Add last rate by enddate
                if (lastRate != null && lastRate.Id > 0)
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