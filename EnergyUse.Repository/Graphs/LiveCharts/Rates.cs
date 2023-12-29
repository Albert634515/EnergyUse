using EnergyUse.Models.Common;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;

namespace EnergyUse.Core.Graphs.LiveCharts
{
    public class Rates : Base
    {
        public Rates(ParameterGraph graphParameter)
        {
            _graphParameter = graphParameter;
            _unitOfWork = new UnitOfWork.Graphs(_graphParameter.DbName);

            loadChart();
        }

        private void loadChart()
        {
            try
            {
                ResetSeries();

                if (_graphParameter.EnergyTypeList != null && _graphParameter.EnergyTypeList.Count > 0)
                {
                    if (_graphParameter.ShowType == Common.Enums.ShowType.Unit)
                        GetChartSeriesPerCostCategoryAndUnit(_graphParameter.EnergyTypeList, _graphParameter.From, _graphParameter.Till, 1);
                    else
                        GetChartSeriesPerCostCategory(_graphParameter.EnergyTypeList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region GetChartSeriesPerCostCategoryAndUnit

        private void GetChartSeriesPerCostCategoryAndUnit(List<Models.EnergyType> energyTypes, DateTime startDate, DateTime endDate, Int64 tarifGroupId)
        {
            var typeCounter = -1;

            foreach (var energyType in energyTypes)
            {
                var energyTypeId = energyType.Id;
                typeCounter++;

                List<Models.CostCategory> costCategoryList = [];
                DateTime chartStartDate = startDate;
                DateTime chartEndDate = endDate;

                while (chartStartDate.AddDays(1) <= chartEndDate)
                {
                    if (chartStartDate.Day == 1)
                    {
                        //Loop selected categories
                        foreach (Models.CostCategory mainCostCategory in _graphParameter.CostCategoryList)
                        {
                            string unitName = $"{mainCostCategory.Name} {mainCostCategory.Unit}";
                            var tarifGroup = mainCostCategory.TariffGroup != null && mainCostCategory.TariffGroup.Id <= 0 ? mainCostCategory.TariffGroup : _graphParameter.Address.TariffGroup;
                            if (tarifGroup != null)
                                tarifGroupId = tarifGroup.Id;

                            // Calculate per main category
                            Models.Rate? rate = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyTypeId, mainCostCategory.Id, chartStartDate, chartStartDate, tarifGroupId).FirstOrDefault();
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

                            if (mainCostCategory.EnergySubType.Id == 1 || mainCostCategory.EnergySubType.Id == 2)
                            {
                                foreach (var otherCostCategory in costCategoryList.Where(m => m.EnergySubType.Id == 5))
                                {
                                    unitName = $"{mainCostCategory.Name} {otherCostCategory.Unit}";

                                    //Toevoegen sub cat
                                    rate = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyTypeId, otherCostCategory.Id, chartStartDate, chartStartDate, (long)otherCostCategory.TariffGroup.Id).FirstOrDefault();
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

                // Creating a list of series
                var typeList = GetTypeList();
                foreach (var item in typeList)
                {
                    var ln = General.GetDefaultLineDateSeries(typeCounter, 2, false);
                    ln.Values = GetValueListY(item);
                    ln.Name = item;

                    _serieslist.Add(ln);
                }
            }
        }

        #endregion

        #region GetChartSeriesPerCostCategory

        private void GetChartSeriesPerCostCategory(List<Models.EnergyType> energyTypes)
        {
            var typeCounter = -1;

            foreach (var energyType in energyTypes)
            {
                var energyTypeId = energyType.Id;
                typeCounter++;

                foreach (Models.CostCategory costCategory in _graphParameter.CostCategoryList)
                {
                    long tarifGroupId = 0;
                    Models.Rate? lastRate = new();

                    var address = new Models.Address();
                    if (_graphParameter.Address != null)
                        address = _graphParameter.Address;

                    var tarifGroup = costCategory.TariffGroup ?? address.TariffGroup;
                    if (tarifGroup != null)
                        tarifGroupId = tarifGroup.Id;

                    var rateList = _unitOfWork.RateRepo.SelectByCostCategoryAndDate(energyType.Id, costCategory.Id, _graphParameter.From, _graphParameter.Till, tarifGroupId).ToList();
                    foreach (Models.Rate rate in rateList)
                    {
                        addRateToList(costCategory.Name, rate.RateValue, rate.StartRate);
                        lastRate = rate;
                    }

                    // Add last rate by enddate
                    if (lastRate != null && lastRate.Id > 0)
                        addRateToList(costCategory.Name, lastRate.RateValue, lastRate.StartRate);
                }

                // Creating a list of series
                var typeList = GetTypeList();
                foreach (var item in typeList)
                {
                    var ln = General.GetDefaultLineDateSeries(typeCounter, 1, false);
                    ln.Values = GetValueListY2(item);
                    ln.Name = item;

                    _serieslist.Add(ln);
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

            if (periodicData.ValueXDate < _graphParameter.From)
                periodicData.ValueXDate = _graphParameter.From;
            if (periodicData.ValueXDate > _graphParameter.Till)
                periodicData.ValueXDate = _graphParameter.Till;

            _periodicDataList.Add(periodicData);
        }

        private ObservableCollection<DateTimePoint> GetValueListY(string itemType)
        {
            var dateList = _periodicDataList.OrderBy(o => o.ValueXDate).Select(x => x.ValueXDate).Distinct().ToList();
            var valueList = new ObservableCollection<DateTimePoint>();
            foreach (DateTime day in dateList)
            {
                foreach (PeriodicData periodicData2 in _periodicDataList.Where(c => c.ValueXDate == day && c.ValueXString == itemType))
                {
                    var dataPoint = new DateTimePoint
                    {
                        DateTime = day,
                        Value = (double?)periodicData2.ValueY
                    };

                    valueList.Add(dataPoint);
                }
            }

            return valueList;
        }

        private ObservableCollection<DateTimePoint> GetValueListY2(string itemType)
        {
            var dateList = _periodicDataList.OrderBy(o => o.ValueXDate).Select(x => x.ValueXDate).Distinct().ToList();
            var valueList = new ObservableCollection<DateTimePoint>();
            foreach (DateTime day in dateList)
            {
                foreach (PeriodicData periodicData2 in _periodicDataList.Where(c => c.ValueXDate == day && c.ValueXString == itemType))
                {
                    var dataPoint = new DateTimePoint
                    {
                        DateTime = day,
                        Value = (double?)periodicData2.ValueY2
                    };

                    valueList.Add(dataPoint);
                }
            }

            return valueList;
        }

        #endregion
    }
}