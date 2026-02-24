using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Common.Libs;
using EnergyUse.Core.Context;
using EnergyUse.Core.UnitOfWork;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System.Globalization;

namespace EnergyUse.Core.Manager;

public class LibPeriodicDate
{
    #region Properties

    private readonly EnergyUseContext _context;
    private readonly string _dbFileName;
    private Repositories.RepoAvgMeterRate _avgRepo;
    private Repositories.RepoSettings _settingsRepo;
    private Repositories.RepoMeterReading _meterReadingRepo;
    private Repositories.RepoVatTarif _vatTarifRepo;
    private Repositories.RepoCostCategories _costCategoriesRepo;
    private Repositories.RepoRate _rateRepo;
    private Repositories.RepoStaffel _staffelRepo;
    private Repositories.RepoNetting _nettingRepo;

    private List<Models.MeterReading> _meterReading;
    private List<PeriodicDataPerDay> _periodicDataList = new();

    private ParameterPeriod _parameterPeriod;
    private AvgMeterRate? _avgGeneral;
    private decimal _avgCorrectionPercentage = 1;
    private decimal _avgCorrectionPercentageReturn = 1;
    private Models.MeterReading _lastrow;
    private List<Models.Rate> _rates = new();
    private List<Models.Common.PeriodStaffel> _periodStaffelList = new List<Models.Common.PeriodStaffel>();

    #endregion

    public LibPeriodicDate(string dbFileName)
    {
        _dbFileName = dbFileName;
        _context = new EnergyUseContext(dbFileName);

        _avgRepo = new Repositories.RepoAvgMeterRate(_context);
        _settingsRepo = new Repositories.RepoSettings(_context);
        _meterReadingRepo = new Repositories.RepoMeterReading(_context);
        _vatTarifRepo = new Repositories.RepoVatTarif(_context);
        _costCategoriesRepo = new Repositories.RepoCostCategories(_context);
        _rateRepo = new Repositories.RepoRate(_context);
        _staffelRepo = new Repositories.RepoStaffel(_context);
        _nettingRepo = new Repositories.RepoNetting(_context);
    }

    public async Task<List<PeriodicData>> GetRangeAsync(ParameterPeriod parameterPeriod)
    {
        _parameterPeriod = parameterPeriod;
        _meterReading = new();
        _periodicDataList = new();
        _lastrow = getLastRow();

        setAvgCorrectionFactor();
        setCorrectionFactorData();
        setNettingData();

        await getPeriodicDataAsync();

        return getData();
    }

    private Models.MeterReading getLastRow()
    {
        var lastRow = _meterReadingRepo.SelectLastRow(_parameterPeriod.EnergyType.Id, _parameterPeriod.AddressId);
        if (lastRow == null)
        {
            lastRow = new Models.MeterReading
            {
                RegistrationDate = DateTime.MinValue
            };
        }

        return lastRow;
    }

    private async Task getPeriodicDataAsync()
    {
        //Retrieve data per day
        _meterReading = _meterReadingRepo.SelectByRange(_parameterPeriod.StartRange, _parameterPeriod.EndRange, _parameterPeriod.EnergyType.Id, _parameterPeriod.AddressId, _parameterPeriod.Month, _parameterPeriod.Week, _parameterPeriod.Day).ToList();

        convertReadingToPeriodicData();

        //Retrieve missing data
        await addMissingData();

        //Splitsen data if missing
        splitMissingPeriods();

        // Add rate values
        addRateToData();

        // Add addital cost data
        addCost();
    }

    /// <summary>
    /// Set cost for each day in selection
    /// </summary>
    private async void addCost()
    {
        List<OtherCost> otherCostList = new();
        var costCategoryList = _costCategoriesRepo.SelectByEnergyTypeAndRange(_parameterPeriod.EnergyType.Id, _parameterPeriod.StartRange, _parameterPeriod.EndRange);
        var libPriceRate = new LibPriceRate(_dbFileName);

        foreach (var costCategory in costCategoryList)
        {
            if (costCategory.CalculationType.Id == 1 || costCategory.CalculationType.Id == 3)
            {
                //Loop data
                foreach (PeriodicDataPerDay periodicData in _periodicDataList)
                {
                    var periodicDate = periodicData.ValueX;
                    var tarifGroupId = _parameterPeriod.TarifGroupId;
                    if (costCategory.TariffGroupId.HasValue && costCategory.TariffGroupId > 0)
                        tarifGroupId = costCategory.TariffGroupId.Value;

                    if (costCategory.Start > periodicDate)
                        continue;

                    if (costCategory.End < periodicDate)
                        continue;

                    bool lastAvailableVatRateUsed = false;

                    Models.VatTarif? vatTarif = null;
                    var vat = 0.0m;

                    if (costCategory.CalculateVat)
                    {
                        vatTarif = await _vatTarifRepo.GetByCostCategoryIdAndDate(costCategory.Id, periodicDate);

                        if (vatTarif == null)
                        {
                            vatTarif = await _vatTarifRepo.GetLastTarif(costCategory.Id, periodicDate);
                            lastAvailableVatRateUsed = (vatTarif != null);
                        }

                        if (vatTarif is not null)
                            vat = vatTarif.Tarif;
                    }

                    PriceRate? priceRate = getPriceRate(libPriceRate, costCategory, periodicData, tarifGroupId);

                    if (priceRate != null && priceRate.RateTypeId == 2)
                    {
                        //Calculate staffel
                        // - Set Initial staffel value cache for previouse period
                        // - Set staffel cache
                        // - Set staffel value
                        setInitialStaffel(priceRate.RateId, costCategory.EnergySubTypeId.GetValueOrDefault());
                        var staffels = getStaffel(priceRate.RateId, periodicData, costCategory.EnergySubTypeId.GetValueOrDefault());

                        // For now overrule rate value with staffel value
                        if (staffels != null && staffels.Count > 0)
                        {
                            // Convert staffel to other costs
                        }
                    }

                    //If there is no cost record create it
                    // Zoek laatste record, kijk of tarief gelijk is anders nieuwe maken en toevoegen
                    OtherCost otherCost = new()
                    {
                        CostCategoryId = costCategory.Id,
                        CorrectionFactor = periodicData.CorrectionFactor,
                        LastAvailableRateUsed = lastAvailableVatRateUsed,
                        LastAvailableVatRateUsed = lastAvailableVatRateUsed
                    };

                    if (priceRate is not null)
                    {
                        otherCost.RateId = priceRate.RateId;
                        otherCost.Rate = priceRate.Rate;
                    }

                    if (vatTarif is not null)
                    {
                        otherCost.VatTarif = vatTarif.Tarif;
                        otherCost.VatRateId = vatTarif.Id;
                    }

                    periodicData.OtherCosts.Add(otherCost);
                }
            }
        }
    }

    private PriceRate? getPriceRate(LibPriceRate libPriceRate, Models.CostCategory costCategory, PeriodicDataPerDay periodicData, long tarifGroupId)
    {
        PriceRate? priceRate = null;
        switch (costCategory.EnergySubType.Id)
        {
            case 1:
                //Normal
                priceRate = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicData.ValueX, SubEnergyType.Normal, tarifGroupId);
                break;
            case 2:
                //low
                priceRate = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicData.ValueX, SubEnergyType.Low, tarifGroupId);
                break;
            case 3:
                //return normal
                priceRate = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicData.ValueX, SubEnergyType.ReturnNormal, tarifGroupId);
                break;
            case 4:
                //return low
                priceRate = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicData.ValueX, SubEnergyType.ReturnLow, tarifGroupId);
                break;
            case 5:
                //Other
                priceRate = libPriceRate.GetRate(_parameterPeriod.EnergyType.Id, costCategory, periodicData, tarifGroupId);
                break;
            case 6:
                // Return cost normal
                priceRate = libPriceRate.GetRate(_parameterPeriod.EnergyType.Id, costCategory, periodicData, tarifGroupId);
                break;
            case 7:
                // Return low normal
                priceRate = libPriceRate.GetRate(_parameterPeriod.EnergyType.Id, costCategory, periodicData, tarifGroupId);
                break;
        }

        return priceRate;
    }

    /// <summary>
    /// Return data per period
    /// </summary>
    /// <returns>List of data</returns>
    private List<PeriodicData> getData()
    {
        List<PeriodicData>? periodicDataList = new();

        foreach (PeriodicDataPerDay periodicDataPerDay in _periodicDataList)
        {
            var labels = GetLabels(periodicDataPerDay);

            PeriodicData? periodicData;
            if (_parameterPeriod.PeriodType == Period.Day || _parameterPeriod.PeriodType == Period.SettlementDay)
                periodicData = periodicDataList.FirstOrDefault(x => x.ValueXDate == periodicDataPerDay.ValueX);
            else if (_parameterPeriod.PeriodType == Period.Month)
                periodicData = periodicDataList.FirstOrDefault(x => x.ValueXString == labels.Item3 && x.IsPredicted == periodicDataPerDay.IsPredicted);
            else
                periodicData = periodicDataList.FirstOrDefault(x => x.ValueX == labels.Item1 && x.IsPredicted == periodicDataPerDay.IsPredicted);

            if (periodicData is null)
            {
                periodicData = new PeriodicData
                {
                    PeriodType = _parameterPeriod.PeriodType,
                    ValueX = labels.Item1,
                    ValueXDate = labels.Item2,
                    ValueXString = labels.Item3,
                    IsPredicted = periodicDataPerDay.IsPredicted,

                    // Correction factor should not be added to graph data
                    CorrectionFactor = 0
                };
                if (_parameterPeriod.PeriodType == Period.SettlementDay && periodicDataPerDay.CorrectionFactor != 1)
                    periodicData.CorrectionFactor = periodicDataPerDay.CorrectionFactor;

                periodicDataList.Add(periodicData);
            }

            //periodicData.ValueXDate = periodicDataPerDay.ValueX;
            periodicData.ValueXWeek = periodicDataPerDay.ValueWeek;
            periodicData.ValueYLow += periodicDataPerDay.ValueYLow;
            periodicData.ValueYNormal += periodicDataPerDay.ValueYNormal;
            periodicData.ValueYReturnLow += periodicDataPerDay.ValueYReturnLow;
            periodicData.ValueYReturnNormal += periodicDataPerDay.ValueYReturnNormal;

            //Calculate value left after netting/salderen
            periodicData.NettingValueYReturnLow += periodicDataPerDay.NettingValueYReturnLow;
            periodicData.NettingValueYReturnNormal += periodicDataPerDay.NettingValueYReturnNormal;

            periodicData.ValueY += periodicDataPerDay.ValueY;

            periodicData.RateLow += periodicDataPerDay.RateLow;
            periodicData.ValueYMonetaryLow += periodicDataPerDay.ValueYMonetaryLow;

            periodicData.RateNormal += periodicDataPerDay.RateNormal;
            periodicData.ValueYMonetaryNormal += periodicDataPerDay.ValueYMonetaryNormal;

            periodicData.RateReturnLow += periodicDataPerDay.RateReturnLow;
            periodicData.ValueYMonetaryReturnLow += periodicDataPerDay.ValueYMonetaryReturnLow;

            periodicData.RateReturnNormal += periodicDataPerDay.RateReturnNormal;
            periodicData.ValueYMonetaryReturnNormal += periodicDataPerDay.ValueYMonetaryReturnNormal;

            periodicData.ValueMonetaryY += periodicDataPerDay.ValueMonetaryY;

            periodicData.OtherCosts.AddRange(periodicDataPerDay.OtherCosts);
        }

        return periodicDataList;
    }

    /// <summary>
    /// Pre fill staffel cache
    /// - set data for period before current range
    /// </summary>
    /// <param name="rateId"></param>
    private void setInitialStaffel(long rateId, long energySubTypeId)
    {
        // Check if staffel is already set
        if (_periodStaffelList.Where(w => w.RateId == rateId).Any())
            return;

        var priceRate = _rateRepo.SelectById(rateId);
        var staffels = _staffelRepo.SelectByRateId(rateId);
        _periodStaffelList = new List<Models.Common.PeriodStaffel>();

        foreach (var staffel in staffels)
        {
            // set staffels for current period in staffel cache
            var periodStaffel = new Models.Common.PeriodStaffel();
            periodStaffel.RateId = rateId;
            periodStaffel.StartRange = priceRate.StartRate;
            periodStaffel.EndRange = priceRate.EndRate;
            periodStaffel.EnergySubTypeId = energySubTypeId;
            _periodStaffelList.Add(periodStaffel);
        }

        // Prefill staffel values with values for period before current range
        // Start range is priceRate.start - End range is _parameterPeriod.startRange
        DateTime currentDate = priceRate.StartRate;
        if (_parameterPeriod.StartRange > priceRate.StartRate)
        {
            // Get period date for range to fill intial staffels
            var meterReadings = _meterReadingRepo.SelectByRange(priceRate.StartRate, _parameterPeriod.StartRange, _parameterPeriod.EnergyType.Id, _parameterPeriod.AddressId, _parameterPeriod.Month, _parameterPeriod.Week, _parameterPeriod.Day).ToList();

            // Get period date for range to fill intial staffels
            decimal leftOver = -1;
            foreach (var meaterReading in meterReadings)
            {
                do
                {
                    leftOver = getInitialStaffel(rateId, meaterReading, energySubTypeId);
                } while (leftOver == 0);
            }
        }
    }

    private decimal getInitialStaffel(long rateId, Models.MeterReading meterReading, long energySubTypeId)
    {
        decimal leftOver = 0;

        // Look up staffel
        var periodStaffel = _periodStaffelList.Where(w => w.RateId == rateId
                                                       && w.EndRange <= meterReading.RegistrationDate
                                                       && w.StartRange >= meterReading.RegistrationDate
                                                       && w.Value <= w.MaxLevel
                                                       && w.EnergySubTypeId == energySubTypeId).FirstOrDefault();
        if (periodStaffel != null)
        {
            decimal value = 0;
            switch (energySubTypeId)
            {
                case 1:
                    //Normal
                    value = meterReading.DeltaNormal;
                    break;
                case 2:
                    //low
                    value = meterReading.DeltaLow;
                    break;
                case 3:
                    //return normal
                    value = meterReading.ReturnDeliveryDeltaNormal;
                    break;
                case 4:
                    //return low
                    value = meterReading.ReturnDeliveryDeltaLow;
                    break;
                case 5:
                    //Other
                    value = meterReading.DeltaNormal + meterReading.DeltaLow + meterReading.ReturnDeliveryDeltaNormal + meterReading.ReturnDeliveryDeltaLow;
                    break;
                case 6:
                    // Return cost normal
                    value = meterReading.ReturnDeliveryDeltaNormal;
                    break;
                case 7:
                    // Return low normal
                    value = meterReading.ReturnDeliveryDeltaLow;
                    break;
            }

            if (periodStaffel.MaxLevel > periodStaffel.Value + value)
            {
                leftOver = periodStaffel.MaxLevel - periodStaffel.Value;
                periodStaffel.Value = periodStaffel.MaxLevel;

                var newPeriodStaffel = new Models.Common.PeriodStaffel();
                newPeriodStaffel.RateId = periodStaffel.RateId;
                newPeriodStaffel.StartRange = periodStaffel.EndRange;
                newPeriodStaffel.EndRange = meterReading.RegistrationDate;
                newPeriodStaffel.Value = leftOver;
                _periodStaffelList.Add(newPeriodStaffel);
            }
            else
            {
                periodStaffel.Value += value;
                leftOver = 0;
            }
        }

        return leftOver;
    }

    private List<Models.Staffel> getStaffel(long rateId, PeriodicDataPerDay pdPerD, long energySubTypeId)
    {
        var staffels = new List<Models.Staffel>();

        // Look up staffel
        var periodStaffel = _periodStaffelList.Where(w => w.RateId == rateId
                                                       && w.EndRange <= pdPerD.ValueX
                                                       && w.StartRange >= pdPerD.ValueX
                                                       && w.Value <= w.MaxLevel
                                                       && w.EnergySubTypeId == energySubTypeId).FirstOrDefault();
        if (periodStaffel != null)
        {
            decimal value = 0;
            switch (energySubTypeId)
            {
                case 1:
                    //Normal
                    value = pdPerD.ValueYNormal;
                    break;
                case 2:
                    //low
                    value = pdPerD.ValueYLow;
                    break;
                case 3:
                    //return normal
                    value = pdPerD.ValueYReturnNormal;
                    break;
                case 4:
                    //return low
                    value = pdPerD.ValueYReturnLow;
                    break;
                case 5:
                    //Other
                    value = pdPerD.ValueYNormal + pdPerD.ValueYLow + pdPerD.ValueYReturnNormal + pdPerD.ValueYReturnLow;
                    break;
                case 6:
                    // Return cost normal
                    value = pdPerD.ValueYReturnNormal;
                    break;
                case 7:
                    // Return low normal
                    value = pdPerD.ValueYReturnLow;
                    break;
            }
        }

        return staffels;

    }

    private Tuple<int, DateTime, string> GetLabels(PeriodicDataPerDay periodicDataPerDay)
    {
        DateTime labelXDate;
        string labelXString;
        int labelX;

        if (_parameterPeriod.PeriodType == Period.Day && _parameterPeriod.Day == 0)
        {
            labelX = periodicDataPerDay.ValueX.Day;
            labelXDate = periodicDataPerDay.ValueX;
            labelXString = "";
        }
        else if (_parameterPeriod.PeriodType == Period.Day && _parameterPeriod.Day > 0 && _parameterPeriod.SetMiddleOfYear == false)
        {
            labelX = int.Parse(periodicDataPerDay.ValueX.ToString("yyyydd"));
            labelXDate = periodicDataPerDay.ValueX;
            labelXString = periodicDataPerDay.ValueX.ToString("dd yyyy");
        }
        else if (_parameterPeriod.PeriodType == Period.Week && _parameterPeriod.SetMiddleOfYear == false)
        {
            labelX = int.Parse($"{periodicDataPerDay.ValueX:yyyy}{periodicDataPerDay.ValueWeek.ToString().PadLeft(2, '0')}");
            labelXString = $"{periodicDataPerDay.ValueX:yyyy}{periodicDataPerDay.ValueWeek.ToString().PadLeft(2, '0')}";
            labelXDate = periodicDataPerDay.ValueX.StartOfWeek(DayOfWeek.Monday).AddDays(3);
        }
        else if (_parameterPeriod.PeriodType == Period.Month && _parameterPeriod.SetMiddleOfYear == false)
        {
            labelX = int.Parse(periodicDataPerDay.ValueX.ToString("yyyyMM"));
            labelXString = periodicDataPerDay.ValueX.ToString("MMM yy");
            labelXDate = new DateTime(periodicDataPerDay.ValueX.Year, periodicDataPerDay.ValueX.Month, 16);
        }
        else if (_parameterPeriod.PeriodType == Period.Year || _parameterPeriod.SetMiddleOfYear == true)
        {
            labelX = periodicDataPerDay.ValueX.Year;
            labelXString = periodicDataPerDay.ValueX.ToString("yyyy");
            labelXDate = new DateTime(periodicDataPerDay.ValueX.Year, 7, 15);
        }
        else if (_parameterPeriod.PeriodType == Period.SettlementDay)
        {
            labelX = int.Parse(periodicDataPerDay.ValueX.Date.ToString("yyyyMMdd"));
            labelXString = periodicDataPerDay.ValueX.ToString("yyyyMMdd");
            labelXDate = periodicDataPerDay.ValueX;
        }
        else
        {
            throw new Exception("Unknown period type");
        }

        return new Tuple<int, DateTime, string>(labelX, labelXDate, labelXString);
    }

    private void addRateToData()
    {
        var libPriceRate = new LibPriceRate(_dbFileName);

        foreach (PeriodicDataPerDay periodicDataPerDay in _periodicDataList)
        {
            var nettingUsed = false;
            // Check if there is netting is used
            var nettingPerc = _nettingRepo.SelectByEnergyTypeAndDate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX);
            if (nettingPerc != null && nettingPerc.Rate != 0)
                nettingUsed = true;

            if (_parameterPeriod.ShowType == ShowType.Value)
            {
                if (_parameterPeriod.EnergyType.HasNormalAndLow)
                {
                    periodicDataPerDay.RateLow = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.Low, _parameterPeriod.TarifGroupId).Rate;
                    periodicDataPerDay.ValueYMonetaryLow = periodicDataPerDay.ValueYLow * periodicDataPerDay.RateLow;
                }

                periodicDataPerDay.RateNormal = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.Normal, _parameterPeriod.TarifGroupId).Rate;
                periodicDataPerDay.ValueYMonetaryNormal = periodicDataPerDay.ValueYNormal * periodicDataPerDay.RateNormal;                              

                if (_parameterPeriod.EnergyType.HasEnergyReturn)
                {
                    if (_parameterPeriod.EnergyType.HasNormalAndLow)
                    {
                        if (nettingUsed)
                        {
                            // Take into account netting
                            periodicDataPerDay.RateReturnLow = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.Low, _parameterPeriod.TarifGroupId).Rate;
                            periodicDataPerDay.ValueYMonetaryReturnLow = periodicDataPerDay.NettingValueYReturnLow * periodicDataPerDay.RateReturnLow;

                            var rateReturnLow = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.ReturnLow, _parameterPeriod.TarifGroupId).Rate;
                            periodicDataPerDay.ValueYMonetaryReturnLow = (periodicDataPerDay.ValueYReturnLow - periodicDataPerDay.NettingValueYReturnLow) * rateReturnLow;
                        }
                        else
                        {
                            periodicDataPerDay.RateReturnLow = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.Low, _parameterPeriod.TarifGroupId).Rate;
                            periodicDataPerDay.ValueYMonetaryReturnLow = periodicDataPerDay.ValueYReturnLow * periodicDataPerDay.RateReturnLow;
                        }
                    }

                    if (nettingUsed)
                    {
                        // Take into account netting
                        periodicDataPerDay.RateReturnNormal = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.Normal, _parameterPeriod.TarifGroupId).Rate;
                        periodicDataPerDay.ValueYMonetaryReturnNormal = periodicDataPerDay.NettingValueYReturnNormal * periodicDataPerDay.RateReturnNormal;

                        var rateReturnNormal = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.ReturnNormal, _parameterPeriod.TarifGroupId).Rate;
                        periodicDataPerDay.ValueYMonetaryReturnNormal = (periodicDataPerDay.ValueYReturnNormal - periodicDataPerDay.NettingValueYReturnNormal) * rateReturnNormal;
                    }
                    else
                    {
                        periodicDataPerDay.RateReturnNormal = libPriceRate.GetCalculatedRate(_parameterPeriod.EnergyType.Id, periodicDataPerDay.ValueX, SubEnergyType.ReturnNormal, _parameterPeriod.TarifGroupId).Rate;
                        periodicDataPerDay.ValueYMonetaryReturnNormal = periodicDataPerDay.ValueYReturnNormal * periodicDataPerDay.RateReturnNormal;
                    }
                }

                periodicDataPerDay.ValueMonetaryY = periodicDataPerDay.ValueYMonetaryLow + periodicDataPerDay.ValueYMonetaryNormal + periodicDataPerDay.ValueYMonetaryReturnNormal + periodicDataPerDay.ValueYMonetaryReturnNormal;
            }
        }
    }

    private async Task addMissingData()
    {
        AvgMeterRate? avgMeterRate = null;
        PeriodicDataPerDay periodicDataDay;

        // Add dataprediction for missing data
        if (_parameterPeriod.PredictMissingData)
        {
            removeLastEmptyDay();

            DateTime lastDate;
            if (_periodicDataList.Count > 0)
                lastDate = _periodicDataList.Max(x => x.ValueX);
            else
                lastDate = _parameterPeriod.StartRange;

            if (lastDate < _parameterPeriod.EndRange)
            {
                List<AvgMeterRate> avgByPeriodList = await getAvgByPeriodList(_parameterPeriod.EnergyType.Id, _parameterPeriod.AddressId);

                for (var day = lastDate.AddDays(1); day.Date <= _parameterPeriod.EndRange; day = day.AddDays(1))
                {
                    // Check if day is missing
                    var missingDay = _periodicDataList.Where(x => x.ValueX.Date == day.Date).FirstOrDefault();
                    if (missingDay != null)
                        continue;

                    if (_parameterPeriod.Month > 0 && day.Month != _parameterPeriod.Month)
                        continue;
                    if (_parameterPeriod.Week > 0 && day.GetWeekNumber() != _parameterPeriod.Week)
                        continue;
                    if (_parameterPeriod.Day > 0 && !(day.Month == _parameterPeriod.Month && day.Day == _parameterPeriod.Day))
                        continue;

                    if (day.Date != _lastrow.RegistrationDate.Date)
                        avgMeterRate = avgByPeriodList.Where(x => x.EnergyTypeId == _parameterPeriod.EnergyType.Id && x.Day == day.Day && x.Month == day.Month).FirstOrDefault();

                    // If not avg is found try general avg
                    if (avgMeterRate == null)
                        avgMeterRate = getAvgGeneral();

                    if (avgMeterRate != null)
                    {
                        periodicDataDay = new PeriodicDataPerDay();
                        periodicDataDay.ValueX = day;
                        periodicDataDay.ValueWeek = day.GetWeekNumber();
                        periodicDataDay.IsPredicted = true;
                        periodicDataDay.CorrectionFactor = getCorrectionFactor(periodicDataDay.ValueX);

                        periodicDataDay.ValueYLow = avgMeterRate.AvgLow * periodicDataDay.CorrectionFactor;
                        periodicDataDay.ValueYNormal = avgMeterRate.AvgNormal * periodicDataDay.CorrectionFactor;
                        periodicDataDay.ValueYReturnLow = avgMeterRate.AvgReturnDeliveryLow * periodicDataDay.CorrectionFactor;
                        periodicDataDay.ValueYReturnNormal = avgMeterRate.AvgReturnDeliveryNormal * periodicDataDay.CorrectionFactor;

                        if (_parameterPeriod.QuantityReduction != 0)
                        {
                            periodicDataDay.ValueYReturnLow *= _parameterPeriod.QuantityReduction;
                            periodicDataDay.ValueYReturnNormal *= _parameterPeriod.QuantityReduction;
                        }

                        //Round predicted data
                        periodicDataDay.ValueYLow = Math.Round(periodicDataDay.ValueYLow, 0);
                        periodicDataDay.ValueYNormal = Math.Round(periodicDataDay.ValueYNormal, 0);
                        periodicDataDay.ValueYReturnLow = Math.Round(periodicDataDay.ValueYReturnLow, 0);
                        periodicDataDay.ValueYReturnNormal = Math.Round(periodicDataDay.ValueYReturnNormal, 0);
                        periodicDataDay.ValueY = periodicDataDay.ValueYLow + periodicDataDay.ValueYNormal - periodicDataDay.ValueYReturnLow - periodicDataDay.ValueYReturnNormal;

                        periodicDataDay.NettingPercentage = getNettingPerc(periodicDataDay.ValueX);
                        periodicDataDay.NettingValueYReturnLow = periodicDataDay.ValueYReturnLow * periodicDataDay.NettingPercentage;
                        periodicDataDay.NettingValueYReturnNormal += periodicDataDay.ValueYReturnNormal * periodicDataDay.NettingPercentage;

                        _periodicDataList.Add(periodicDataDay);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Last data in data set usualy does not have any calculated data
    /// Data is calculate from last day minus previous day
    /// </summary>
    private void removeLastEmptyDay()
    {
        if (_periodicDataList != null && _periodicDataList.Count > 0)
        {
            _lastrow = _meterReadingRepo.SelectLastRow(_parameterPeriod.EnergyType.Id, _parameterPeriod.AddressId);
            if (_lastrow == null)
                return;

            PeriodicDataPerDay lastSelectedDate = _periodicDataList.OrderByDescending(o => o.ValueX).FirstOrDefault();

            if (_lastrow.RegistrationDate.Date == lastSelectedDate.ValueX.Date && lastSelectedDate != null && lastSelectedDate.ValueY == 0)
                _periodicDataList.Remove(lastSelectedDate);
        }
    }

    private async Task<List<AvgMeterRate>> getAvgByPeriodList(long energyTypeId, long addressId)
    {
        var setting = _settingsRepo.GetByKey("UseAllDataForAvg");
        if (setting != null && setting.KeyValue == "Yes")
        {
            return (await _avgRepo.SelectByAddressAndEnergyTypePerPeriod(energyTypeId, addressId)).ToList();
        }
        else
        {
            // Calculate averageFrom
            var defaultFromValue = DateTime.Now.AddYears(-2);
            setting = _settingsRepo.GetByKey("AvgDateFromDate");
            if (setting != null && !string.IsNullOrWhiteSpace(setting.KeyValue))
            {
                defaultFromValue = DateTime.ParseExact(setting.KeyValue, "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            return (await _avgRepo.SelectByAddressAndEnergyTypePerPeriodFromDate(energyTypeId, addressId, defaultFromValue)).ToList();
        }
    }

    private AvgMeterRate? getAvgGeneral()
    {
        if (_avgGeneral == null)
        {
            // Await the asynchronous method to resolve the Task and get the result
            _avgGeneral = _avgRepo.SelectGeneralAvgByAddressAndEnergyType(
                _parameterPeriod.EnergyType.Id,
                _parameterPeriod.AddressId,
                _avgCorrectionPercentage,
                _avgCorrectionPercentageReturn
            ).Result; // Use .Result to get the result of the Task synchronously
        }

        return _avgGeneral;
    }

    private void convertReadingToPeriodicData()
    {
        PeriodicDataPerDay periodicDataDay;
        Repositories.RepoNetting? nettingRepo = new(_context);
        foreach (var meterReading in _meterReading.Where(x => x.RegistrationDate >= _parameterPeriod.StartRange))
        {
            periodicDataDay = new PeriodicDataPerDay();
            periodicDataDay.ValueX = meterReading.RegistrationDate;
            periodicDataDay.ValueWeek = meterReading.Week;
            periodicDataDay.IsPredicted = false;
            periodicDataDay.CorrectionFactor = getCorrectionFactor(periodicDataDay.ValueX);
            periodicDataDay.ValueYLow = meterReading.DeltaLow * periodicDataDay.CorrectionFactor;
            periodicDataDay.ValueYNormal = meterReading.DeltaNormal * periodicDataDay.CorrectionFactor;
            periodicDataDay.ValueYReturnLow = meterReading.ReturnDeliveryDeltaLow * periodicDataDay.CorrectionFactor;
            periodicDataDay.ValueYReturnNormal = meterReading.ReturnDeliveryDeltaNormal * periodicDataDay.CorrectionFactor;
            periodicDataDay.ValueY = periodicDataDay.ValueYLow + periodicDataDay.ValueYNormal - periodicDataDay.ValueYReturnLow - periodicDataDay.ValueYReturnNormal;

            periodicDataDay.NettingPercentage = getNettingPerc(periodicDataDay.ValueX);
            periodicDataDay.NettingValueYReturnLow = periodicDataDay.ValueYReturnLow * periodicDataDay.NettingPercentage;
            periodicDataDay.NettingValueYReturnNormal += periodicDataDay.ValueYReturnNormal * periodicDataDay.NettingPercentage;

            _periodicDataList.Add(periodicDataDay);
        }
    }

    private void splitMissingPeriods()
    {
        if (_periodicDataList.Count > 0)
        {
            PeriodicDataPerDay currentDay = _periodicDataList[_periodicDataList.Count - 1];
            var missingDataList = new List<PeriodicDataPerDay>();

            if (_periodicDataList.Count == 1)
            {
                var nextDay = new PeriodicDataPerDay();
                nextDay.ValueX = _parameterPeriod.StartRange;
                nextDay.ValueWeek = _parameterPeriod.StartRange.GetWeekNumber();
                nextDay.IsPredicted = currentDay.IsPredicted;
                missingDataList.AddRange(getMissingRange(currentDay, nextDay, _parameterPeriod.Month, _parameterPeriod.Week, _parameterPeriod.Day));
            }
            else
            {
                //Split data for missing periods
                for (int i = _periodicDataList.Count - 1; i > 0; i += -1)
                {
                    PeriodicDataPerDay nextDay = _periodicDataList[i - 1];

                    var newDays = getMissingRange(currentDay, nextDay, _parameterPeriod.Month, _parameterPeriod.Week, _parameterPeriod.Day);
                    if (newDays != null && newDays.Count > 0)
                    {
                        _periodicDataList.Remove(currentDay);
                        missingDataList.AddRange(newDays);
                    }

                    currentDay = nextDay;
                }
            }

            _periodicDataList.AddRange(missingDataList);
        }
    }

    private List<PeriodicDataPerDay> getMissingRange(PeriodicDataPerDay currentDay, PeriodicDataPerDay nextDay, int month = 0, int week = 0, int dayNo = 0)
    {
        var missingDataList = new List<PeriodicDataPerDay>();

        var dayDiff = (currentDay.ValueX.Date - nextDay.ValueX.Date).Days;
        if (dayDiff > 1)
        {
            for (int y = 0; y < dayDiff; y++)
            {
                var newDay = new PeriodicDataPerDay();
                newDay.ValueX = currentDay.ValueX.AddDays(-y);
                newDay.ValueWeek = currentDay.ValueWeek;
                newDay.IsPredicted = currentDay.IsPredicted;
                newDay.CorrectionFactor = currentDay.CorrectionFactor;
                newDay.ValueYLow = currentDay.ValueYLow / dayDiff;
                newDay.ValueYNormal = currentDay.ValueYNormal / dayDiff;
                newDay.ValueYReturnLow = currentDay.ValueYReturnLow / dayDiff;
                newDay.ValueYReturnNormal = currentDay.ValueYReturnNormal / dayDiff;
                newDay.ValueY = newDay.ValueYLow + newDay.ValueYNormal - newDay.ValueYReturnLow - newDay.ValueYReturnNormal;

                //Skip if specicific period is requested
                if (month > 0 && newDay.ValueX.Month != month)
                    continue;
                if (week > 0 && newDay.ValueX.GetWeekNumber() != week)
                    continue;
                if (dayNo > 0 && !(newDay.ValueX.Month == month && newDay.ValueX.Day == dayNo))
                    continue;

                missingDataList.Add(newDay);
            }
        }

        return missingDataList;
    }

    private List<Models.CorrectionFactor> _correctionFactors;
    private void setCorrectionFactorData()
    {
        var correctionFactorRepo = new Repositories.RepoCorrectionFactor(_context);
        _correctionFactors = correctionFactorRepo.SelectByRange(_parameterPeriod.StartRange, _parameterPeriod.EndRange, _parameterPeriod.EnergyType.Id).ToList();
    }

    private decimal getCorrectionFactor(DateTime day)
    {
        decimal correction = 1;

        if (_correctionFactors.Count > 0)
        {
            var correctionFactor = _correctionFactors.FirstOrDefault(x => x.EnergyType.Id == _parameterPeriod.EnergyType.Id && x.StartFactor <= day && x.EndFactor >= day);
            if (correctionFactor != null)
            {
                correction = correctionFactor.Factor;
            }
        }

        return correction;
    }

    /// <summary>
    /// Set correction factor to correct avg to correct season effect of season data not available
    /// </summary>
    private void setAvgCorrectionFactor()
    {
        var avgCorrectionPerc = _settingsRepo.GetByKey("AvgCorrectionPercentage");
        if (avgCorrectionPerc != null && avgCorrectionPerc.KeyValue.IsNumeric())
        {
            decimal.TryParse(avgCorrectionPerc.KeyValue, out _avgCorrectionPercentage);
            if (_avgCorrectionPercentage == 0)
                _avgCorrectionPercentage = 1;
        }

        var avgCorrectionPercReturn = _settingsRepo.GetByKey("AvgCorrectionPercentageReturn");
        if (avgCorrectionPercReturn != null && avgCorrectionPercReturn.KeyValue.IsNumeric())
        {
            decimal.TryParse(avgCorrectionPercReturn.KeyValue, out _avgCorrectionPercentageReturn);
            if (_avgCorrectionPercentageReturn == 0)
                _avgCorrectionPercentageReturn = 1;
        }
    }

    #region Netting

    private List<Models.Netting> _nettingList;
    private async Task setNettingData()
    {
        Repositories.RepoNetting? nettingRepo = new(_context);
        _nettingList = (await nettingRepo.SelectByEnergyType(_parameterPeriod.EnergyType.Id)).ToList();
    }

    private decimal getNettingPerc(DateTime day)
    {
        var netting = _nettingList.Where(x => x.StartDate.Date <= day.Date && x.EndDate.Date >= day.Date).FirstOrDefault();
        decimal nettingPerc = 1;
        if (netting != null)
            nettingPerc = (netting.Rate / 100);

        return nettingPerc;
    }

    #endregion

    #region Efficiency

    public decimal GetEfficiencyTotal(PeriodicData periodicData, decimal totalCapacity)
    {
        int capacityDivider = getCapacityDivider(periodicData);

        decimal efficiency = 0;
        decimal totalProduced = periodicData.ValueYReturnNormal + periodicData.ValueYReturnLow;

        if (totalProduced != 0)
            efficiency = totalProduced / (totalCapacity / capacityDivider) * 100;

        return Math.Round(efficiency, 2);
    }

    public decimal GetEfficiency(PeriodicData periodicData, SubEnergyType subEnergyType, decimal totalCapacity)
    {
        decimal efficiency = 0;
        int capacityDivider = getCapacityDivider(periodicData);

        switch (subEnergyType)
        {
            case SubEnergyType.ReturnNormal:
                if (periodicData.ValueYReturnNormal != 0 && totalCapacity > 0)
                    efficiency = periodicData.ValueYReturnNormal / (totalCapacity / capacityDivider) * 100;
                break;
            case SubEnergyType.ReturnLow:
                if (periodicData.ValueYReturnLow != 0 && totalCapacity > 0)
                    efficiency = periodicData.ValueYReturnLow / (totalCapacity / capacityDivider) * 100;
                break;
            default:
                break;
        }
        return Math.Round(efficiency, 2);
    }

    private int getCapacityDivider(PeriodicData periodicData)
    {
        int capacityDivider = periodicData.PeriodType switch
        {
            Period.Day => LibDatetime.GetDaysInYear(periodicData.ValueXDate),
            Period.Week => LibDatetime.GetWeeksInYear(int.Parse(periodicData.ValueXString.Substring(0, 4))),
            Period.Month => 12,
            Period.Year => 1,
            _ => 1,
        };

        return capacityDivider;
    }

    #endregion
}