using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Drawing;

namespace EnergyUse.Core.Graphs.LiveCharts
{
    public class Base
    {
        #region ChartProperties
                
        internal UnitOfWork.Graphs? _unitOfWork { get; set; }
        internal ParameterGraph _graphParameter { get; set; } = new();
        internal LibPeriodicDate _libPeriodicDate { get; set; }
        internal List<PeriodicData> _periodicDataList { get; set; } = new();
        internal List<ISeries> _serieslist { get; set; } = new();
        internal List<ICartesianAxis> _axisList { get; set; } = new();
        internal Dictionary<string, ObservableCollection<DateTimePoint>> _datePoints { get; set; } = [];

        #endregion

        #region AddPointAndSeries

        internal void AddColumnSeriesToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt, bool showStacked = false)
        {
            ISeries columnSerie;
            var serieName = GetSeriesKey(serieType, energyTypeId);

            if (_datePoints.ContainsKey(serieName) && _datePoints[serieName].Count > 0)
            {
                Color color = GetColor(serieType, energyTypeId);

                if (showStacked)
                {
                    int stackedGroupId = (int)LibGraphGeneral.GetChartGroup(serieType) + 100 + (int)energyTypeId;
                    columnSerie = General.GetStackedColumnDateSerie(serieType, energyTypeId, stackedGroupId, color, scalesYAt);
                }
                else
                    columnSerie = General.GetColumnDateSerie(serieType, color, scalesYAt);

                columnSerie.Values = _datePoints[serieName];
                _serieslist.Add(columnSerie);
            }
        }

        internal void AddLineSeriesToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt)
        {
              var serieName = GetSeriesKey(serieType, energyTypeId);

            if (_datePoints.ContainsKey(serieName) && _datePoints[serieName].Count > 0)
            {
                var lineserie = General.GetLineDateSerie(serieType, scalesYAt);
                lineserie.Values = _datePoints[serieName];
                _serieslist.Add(lineserie);
            }
        }

        internal void AddLineSeriesAvgToList(ChartSeriesType serieType, long energyTypeId, int scalesYAt)
        {
            var serieName = GetSeriesKey(serieType, energyTypeId);

            if (_datePoints.ContainsKey(serieName) && _datePoints[serieName].Count > 0)
            {
                Color color = GetColor(serieType, energyTypeId);
                var lineserie = General.GetLineSerieAvgDate(serieType, color, scalesYAt);
                lineserie.Values = _datePoints[serieName];
                _serieslist.Add(lineserie);
            }
        }

        internal void AddDataPoint(ChartSeriesType seriesType, long energyTypeId, ShowType showType, PeriodicData periodicData, Period periodType = Period.Unknown, int roundingDigit = 2)
        {
            DateTimePoint? ratePeriod;
            string seriesKey = GetSeriesKey(seriesType, energyTypeId);

            if (showType == ShowType.Rate)
                ratePeriod = GetRateByPeriod(periodicData, seriesType, roundingDigit);
            else if (showType == ShowType.Value)
                ratePeriod = GetValueByPeriod(periodicData, seriesType, roundingDigit);
            else if (showType == ShowType.Efficiency)
                ratePeriod = GetEfficiencyByPeriod(periodicData, seriesType, _graphParameter.Address.TotalCapacity, roundingDigit);
            else if (showType == ShowType.AvgRate)
                ratePeriod = GetAvgRateByPeriod(periodicData, seriesType, periodType, roundingDigit);
            else if (showType == ShowType.AvgValue)
                ratePeriod = GetAvgValueByPeriod(periodicData, seriesType, periodType, roundingDigit);
            else
                throw new Exception("Unknown show type");

            if (ratePeriod != null)
            {
                if (seriesType.ToString().ToLower().Contains("return"))
                    ratePeriod.Value = 0 - ratePeriod.Value;

                var periodExits = _datePoints[seriesKey].Where(x => x.DateTime.Date == periodicData.ValueXDate).FirstOrDefault();
                if (periodExits != null && ratePeriod != null)
                    periodExits.Value += Math.Round((double)ratePeriod.Value, roundingDigit);
                else
                    _datePoints[seriesKey].Add(ratePeriod);
            }
        }

        #endregion

        #region GetPeriodData

        internal DateTimePoint GetEfficiencyByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, decimal totalCapacity, int roundingDigit = 2)
        {
            double rate = double.NaN;

            switch (chartSeriesType)
            {
                case ChartSeriesType.ReturnLowPredicted:
                    if (periodicData.IsPredicted == true)
                        rate = (double)_libPeriodicDate.GetEfficiency(periodicData, SubEnergyType.ReturnLow, totalCapacity);
                    break;
                case ChartSeriesType.ReturnNormalPredicted:
                    if (periodicData.IsPredicted == true)
                        rate = (double)_libPeriodicDate.GetEfficiency(periodicData, SubEnergyType.ReturnNormal, totalCapacity);
                    break;
                case ChartSeriesType.ReturnLow:
                    if (periodicData.IsPredicted == false)
                        rate = (double)_libPeriodicDate.GetEfficiency(periodicData, SubEnergyType.ReturnLow, totalCapacity);
                    break;
                case ChartSeriesType.ReturnNormal:
                    if (periodicData.IsPredicted == false)
                        rate = (double)_libPeriodicDate.GetEfficiency(periodicData, SubEnergyType.ReturnNormal, totalCapacity);
                    break;
                default:
                    break;
            }

            return new DateTimePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
        }

        internal DateTimePoint GetValueByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, int roundingDigit = 2)
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
                case ChartSeriesType.AvgLow:
                    break;
                case ChartSeriesType.AvgValueLow:
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
                case ChartSeriesType.AvgNormal:
                    break;
                case ChartSeriesType.AvgValueNormal:
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
                case ChartSeriesType.ReturnAvgLowDelivery:
                    break;
                case ChartSeriesType.ReturnNormal:
                    rate = (double)periodicData.ValueYMonetaryReturnNormal;
                    break;
                case ChartSeriesType.ReturnAvgNormalDelivery:
                    break;
                case ChartSeriesType.Consumed:
                    break;
                case ChartSeriesType.ConsumedPredicted:
                    break;
                case ChartSeriesType.Produced:
                    break;
                case ChartSeriesType.ProducedPredicted:
                    break;
                case ChartSeriesType.Total:
                    break;
                case ChartSeriesType.TotalPredicted:
                    break;
                default:
                    break;
            }

            return new DateTimePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
        }

        internal DateTimePoint GetRateByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, int digitRounding = 2)
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
                case ChartSeriesType.AvgLow:
                    break;
                case ChartSeriesType.AvgValueLow:
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
                case ChartSeriesType.AvgNormal:
                    break;
                case ChartSeriesType.AvgValueNormal:
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
                case ChartSeriesType.ReturnAvgLowDelivery:
                    break;
                case ChartSeriesType.ReturnNormal:
                    if (periodicData.IsPredicted == false)
                        rate = (double)periodicData.ValueYReturnNormal;
                    break;
                case ChartSeriesType.ReturnAvgNormalDelivery:
                    break;
                case ChartSeriesType.Consumed:
                    break;
                case ChartSeriesType.ConsumedPredicted:
                    break;
                case ChartSeriesType.Produced:
                    break;
                case ChartSeriesType.ProducedPredicted:
                    break;
                case ChartSeriesType.Total:
                    if (periodicData.IsPredicted == false)
                        rate = (double)(Math.Round(periodicData.ValueYLow + periodicData.ValueYNormal, digitRounding) - Math.Round(periodicData.ValueYReturnLow + periodicData.ValueYReturnNormal, digitRounding));
                    break;
                case ChartSeriesType.TotalPredicted:
                    break;
                default:
                    break;
            }

            return new DateTimePoint(periodicData.ValueXDate, Math.Round(rate, digitRounding));
        }

        /// <summary>
        /// Calculate average rate by period
        /// </summary>
        /// <remarks>Take into account incomplete periode as this will suppress avg too much</remarks>
        /// <param name="periodicData"></param>
        /// <param name="chartSeriesType"></param>
        /// <param name="periodType"></param>
        /// <returns></returns>
        internal DateTimePoint? GetAvgRateByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, Period periodType, int roundingDigit = 2)
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

            return new DateTimePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
        }

        internal DateTimePoint? GetAvgValueByPeriod(PeriodicData periodicData, ChartSeriesType chartSeriesType, Period periodType, int roundingDigit = 2)
        {
            double rate = double.NaN;
            //Take into account incomplete periode as this will suppress avg too much
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

            return new DateTimePoint(periodicData.ValueXDate, Math.Round(rate, roundingDigit));
        }

        #endregion

        #region BaseMethods

        public string GetSeriesKey(ChartSeriesType seriesType, long energyId)
        {
            return $"{seriesType}_{energyId}";
        }

        public List<ISeries> GetSeries()
        {
            return _serieslist;
        }

        public void AddAxis(ICartesianAxis axis)
        {
            _axisList.Add(axis);
        }

        public ICartesianAxis GetAxis(string name)
        {
            return new Axis
            {
                Name = name,
                NameTextSize = 14,
                NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                Padding = new LiveChartsCore.Drawing.Padding(20, 0, 0, 0),
                TextSize = 12,
                DrawTicksPath = true,
                ShowSeparatorLines = false,
                Position = LiveChartsCore.Measure.AxisPosition.End
            };
        }

        public List<ICartesianAxis> GetAxisLists()
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
            _serieslist = new ObservableCollection<ISeries>().ToList();
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

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
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
}