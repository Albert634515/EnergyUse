using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfUI.Models;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class CompareChartService
    {
        private readonly DialogService _dialogService;
        private readonly SelectionItemService _selectionItemService;

        public CompareChartService(ILanguageService languageService)
        {
            _dialogService = new DialogService();
            _selectionItemService = new SelectionItemService(languageService);
        }

        // ---------------------------------------------------------
        // MAIN ENTRY
        // ---------------------------------------------------------
        public CompareChartResult BuildChart(
            Address address,
            EnergyType energyType,
            SelectionItem periodTypeItem,
            int startYear,
            int endYear,
            int number,
            int day,
            bool predictMissing,
            bool showStacked,
            ShowBy showBy,
            ShowType showType)
        {
            var result = new CompareChartResult
            {
                Series = new List<ISeries>(),
                XAxes = new List<Axis>(),
                YAxes = new List<Axis>()
            };

            if (address == null || energyType == null || periodTypeItem == null)
                return result;

            var period = (periodTypeItem.Key ?? "").ToUpperInvariant() switch
            {
                "DAY" => Period.Day,
                "WEEK" => Period.Week,
                "MONTH" => Period.Month,
                "YEAR" => Period.Year,
                _ => Period.Unknown
            };

            if (period == Period.Unknown)
                return result;

            var graphParameter = new ParameterGraph
            {
                Address = address,
                EnergyTypeList = new() { energyType },
                DbName = Managers.Config.GetDbFileName(),
                ShowStacked = showStacked,
                PredictMissingData = predictMissing,
                YearStart = startYear,
                YearEnd = endYear,
                PeriodType = period,
                ShowBy = showBy,
                ShowType = showType,
                From = new DateTime(startYear, 1, 1),
                Till = new DateTime(endYear, 12, 31)
            };

            if (address.TariffGroup != null)
                graphParameter.TarifGroupId = address.TariffGroup.Id;

            if (period == Period.Day || period == Period.Month)
                graphParameter.Month = number;

            if (period == Period.Week)
                graphParameter.Week = number;

            if (day > 0)
                graphParameter.Day = day;

            var compare = new Compare(graphParameter);
            var seriesModels = compare.GetSeries() ?? new List<SeriesModel>();

            switch (period)
            {
                case Period.Year:
                    BuildYearChart(result, seriesModels, startYear, endYear, showStacked);
                    break;

                case Period.Month:
                    BuildMonthChart(result, seriesModels, startYear, endYear, number, showStacked, predictMissing);
                    break;

                case Period.Week:
                    BuildWeekChart(result, seriesModels, startYear, endYear, number, showStacked, predictMissing);
                    break;

                case Period.Day:
                    BuildDayChart(result, seriesModels, startYear, endYear, number, day, showStacked, predictMissing);
                    break;
            }

            result.YAxes.Add(new Axis { MinLimit = 0 });

            result.Labels = compare.GetResultLabelsPerPeriod(energyType);
            result.ExportData = compare.GetDataList();

            return result;
        }

        // ---------------------------------------------------------
        // YEAR (categorical per jaar)
        // ---------------------------------------------------------
        private void BuildYearChart(CompareChartResult result, List<SeriesModel> models, int startYear, int endYear, bool showStacked)
        {
            var years = Enumerable.Range(startYear, endYear - startYear + 1).ToList();

            result.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = years.Select(y => y.ToString()).ToArray(),
                    Position = AxisPosition.Start
                }
            };

            var yearIndex = years.Select((y, i) => new { y, i })
                                 .ToDictionary(x => x.y, x => (double)x.i);

            result.Series = ConvertToCategorical(models, dp => yearIndex[dp.DateTime.Year], showStacked);
        }

        // ---------------------------------------------------------
        // MONTH (som per jaar voor gekozen maand, categorical)
        // ---------------------------------------------------------
        private void BuildMonthChart(CompareChartResult result, List<SeriesModel> models, int startYear, int endYear, int month, bool showStacked, bool predictMissing)
        {
            var years = Enumerable.Range(startYear, endYear - startYear + 1).ToList();

            result.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = years.Select(y => $"{y}-{month:00}").ToArray(),
                    Position = AxisPosition.Start
                }
            };

            var yearIndex = years.Select((y, i) => new { y, i })
                                 .ToDictionary(x => x.y, x => (double)x.i);

            result.Series = ConvertToCategoricalMonth(models, years, month, yearIndex, showStacked, predictMissing);
        }

        // ---------------------------------------------------------
        // WEEK (som per jaar voor gekozen week, categorical)
        // ---------------------------------------------------------
        private void BuildWeekChart(CompareChartResult result, List<SeriesModel> models, int startYear, int endYear, int week, bool showStacked, bool predictMissing)
        {
            var years = Enumerable.Range(startYear, endYear - startYear + 1).ToList();

            result.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = years.Select(y => $"{y}-W{week:00}").ToArray(),
                    Position = AxisPosition.Start
                }
            };

            var yearIndex = years.Select((y, i) => new { y, i })
                                 .ToDictionary(x => x.y, x => (double)x.i);

            result.Series = ConvertToCategoricalWeek(models, years, week, yearIndex, showStacked, predictMissing);
        }

        // ---------------------------------------------------------
        // DAY (1 punt per jaar voor gekozen dag, categorical)
        // ---------------------------------------------------------
        private void BuildDayChart(CompareChartResult result, List<SeriesModel> models, int startYear, int endYear, int month, int day, bool showStacked, bool predictMissing)
        {
            var dates = Enumerable.Range(startYear, endYear - startYear + 1)
                                  .Select(y => SafeDate(y, month, day))
                                  .Where(d => d != DateTime.MinValue)
                                  .ToList();

            result.XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = dates.Select(d => d.ToString("dd/MM/yyyy")).ToArray(),
                    Position = AxisPosition.Start
                }
            };

            var indexMap = dates.Select((d, i) => new { d, i })
                                .ToDictionary(x => x.d, x => (double)x.i);

            result.Series = ConvertToCategoricalDay(models, dates, indexMap, showStacked, predictMissing);
        }

        // ---------------------------------------------------------
        // GENERIEKE HELPERS
        // ---------------------------------------------------------
        private List<ISeries> ConvertToCategorical(List<SeriesModel> models, Func<DatePoint, double> xSelector, bool showStacked)
        {
            var list = new List<ISeries>();

            foreach (var sm in models)
            {
                var points = sm.Points.Select(dp => new ObservablePoint(xSelector(dp), dp.Value)).ToList();

                if (sm.IsLine)
                {
                    list.Add(new LineSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        LineSmoothness = 0
                    });
                }
                else if (sm.IsStacked && showStacked)
                {
                    list.Add(new StackedColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        StackGroup = 0
                    });
                }
                else
                {
                    list.Add(new ColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name
                    });
                }
            }

            return list;
        }

        // MONTH: som per jaar voor gekozen maand
        private List<ISeries> ConvertToCategoricalMonth(
            List<SeriesModel> models,
            List<int> years,
            int month,
            Dictionary<int, double> yearIndex,
            bool showStacked,
            bool predictMissing)
        {
            var list = new List<ISeries>();

            foreach (var sm in models)
            {
                var grouped = sm.Points
                    .GroupBy(p => new { p.DateTime.Year, p.DateTime.Month })
                    .ToDictionary(g => (g.Key.Year, g.Key.Month), g => g.Sum(p => p.Value));

                var points = new List<ObservablePoint>();

                foreach (var y in years)
                {
                    var key = (y, month);
                    if (grouped.TryGetValue(key, out var val))
                    {
                        points.Add(new ObservablePoint(yearIndex[y], val));
                    }
                    else if (predictMissing)
                    {
                        points.Add(new ObservablePoint(yearIndex[y], 0));
                    }
                }

                if (sm.IsLine)
                {
                    list.Add(new LineSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        LineSmoothness = 0
                    });
                }
                else if (sm.IsStacked && showStacked)
                {
                    list.Add(new StackedColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        StackGroup = 0
                    });
                }
                else
                {
                    list.Add(new ColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name
                    });
                }
            }

            return list;
        }

        // WEEK: som per jaar voor gekozen week
        private List<ISeries> ConvertToCategoricalWeek(
            List<SeriesModel> models,
            List<int> years,
            int week,
            Dictionary<int, double> yearIndex,
            bool showStacked,
            bool predictMissing)
        {
            var list = new List<ISeries>();

            foreach (var sm in models)
            {
                var grouped = sm.Points
                    .GroupBy(p => new { p.DateTime.Year, Week = p.DateTime.GetWeekNumber() })
                    .ToDictionary(g => (g.Key.Year, g.Key.Week), g => g.Sum(p => p.Value));

                var points = new List<ObservablePoint>();

                foreach (var y in years)
                {
                    var key = (y, week);
                    if (grouped.TryGetValue(key, out var val))
                    {
                        points.Add(new ObservablePoint(yearIndex[y], val));
                    }
                    else if (predictMissing)
                    {
                        points.Add(new ObservablePoint(yearIndex[y], 0));
                    }
                }

                if (sm.IsLine)
                {
                    list.Add(new LineSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        LineSmoothness = 0
                    });
                }
                else if (sm.IsStacked && showStacked)
                {
                    list.Add(new StackedColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        StackGroup = 0
                    });
                }
                else
                {
                    list.Add(new ColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name
                    });
                }
            }

            return list;
        }

        // DAY: 1 punt per jaar voor gekozen dag
        private List<ISeries> ConvertToCategoricalDay(
            List<SeriesModel> models,
            List<DateTime> targetDates,
            Dictionary<DateTime, double> indexMap,
            bool showStacked,
            bool predictMissing)
        {
            var list = new List<ISeries>();

            foreach (var sm in models)
            {
                var grouped = sm.Points
                    .GroupBy(p => p.DateTime.Date)
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.Value));

                var points = new List<ObservablePoint>();

                foreach (var dt in targetDates)
                {
                    if (grouped.TryGetValue(dt, out var val))
                        points.Add(new ObservablePoint(indexMap[dt], val));
                    else if (predictMissing)
                        points.Add(new ObservablePoint(indexMap[dt], 0));
                }

                if (sm.IsLine)
                {
                    list.Add(new LineSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        LineSmoothness = 0
                    });
                }
                else if (sm.IsStacked && showStacked)
                {
                    list.Add(new StackedColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        StackGroup = 0
                    });
                }
                else
                {
                    list.Add(new ColumnSeries<ObservablePoint>
                    {
                        Values = points,
                        Name = sm.Name
                    });
                }
            }

            return list;
        }

        private static DateTime SafeDate(int year, int month, int day)
        {
            try { return new DateTime(year, month, day); }
            catch { return DateTime.MinValue; }
        }

        public void ExportToExcel(List<PeriodicData> exportData, EnergyType energyType)
        {
            if (exportData == null || exportData.Count == 0)
                return;

            var fileName = _dialogService.GetExportFileName("ChartDefault", energyType);
            if (!string.IsNullOrWhiteSpace(fileName))
                LibExport.ExportCompareChartToExcel(fileName, energyType, exportData);
        }
    }
}