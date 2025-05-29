using System.Globalization;
using EnergyUse.Core.Context;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager;

public class LibSettlementData
{
    #region Properties

    private readonly EnergyUseContext _context;
    private static List<Models.Rate>? _rates;

    private static Repositories.RepoRate _rateRepo;
    private static Repositories.RepoStaffel _rateStaffelRepo;

    #endregion        

    public LibSettlementData(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);
        _rateRepo = new Repositories.RepoRate(_context);
        _rateStaffelRepo = new Repositories.RepoStaffel(_context);
    }

    public List<SettlementData> GetSettlementCost(long energyTypeId, List<PeriodicData> periodicDataList, Models.CostCategory costCategory, long tarifGroupId)
    {
        List<SettlementData> settlementDataList = new();

        if (costCategory.CalculationType.Id != 1 && costCategory.CalculationType.Id != 3)
            return settlementDataList;

        DateTime startRange, endRange;
        decimal priceIncrease = 0;
        decimal lastRate = 0;
        var vatTarifRepo = new Repositories.RepoVatTarif(_context);

        //Loop data
        foreach (PeriodicData periodicData in periodicDataList)
        {
            startRange = DateTime.ParseExact(periodicData.ValueX.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            endRange = DateTime.ParseExact(periodicData.ValueX.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

            if (costCategory.Start > startRange)
                continue;

            if (costCategory.End < endRange)
                continue;

            bool lastAvailableVatRateUsed = false;

            Models.VatTarif? vatTarif = null;
            var vat = 0.0m;

            if (costCategory.CalculateVat)
            {
                vatTarif = vatTarifRepo.GetByCostCategoryIdAndDate(costCategory.Id, periodicData.ValueXDate);

                if (vatTarif == null)
                {
                    vatTarif = vatTarifRepo.GetLastTarif(costCategory.Id, periodicData.ValueXDate);
                    lastAvailableVatRateUsed = (vatTarif != null);
                }

                if (vatTarif is not null)
                    vat = vatTarif.Tarif;
            }

            //Get tarif rate
            (var rate, bool lastAvailableRateUsed) = getRate(energyTypeId, costCategory, periodicData, tarifGroupId);

            Models.Staffel? staffel = null;
            if (rate.RateTypeId == 2)
            {
                // Get staffel, TODO could be multiple staffel if streshold is exceeded
                staffel = getStaffel(rate.Id, periodicData.ValueX);

                // For now overrule rate value with staffel value
                if (staffel != null)
                    rate.RateValue = staffel.StaffelValue;
            }

            SettlementData? settlementData = null;
            var rateUsed = 0.0m;
            if (lastAvailableRateUsed)
            {
                rateUsed = rate.RateValue;
                if (rate != null && rate.ExpectedPriceChange != 0)
                {
                    priceIncrease = 1 + (rate.ExpectedPriceChange / 100);
                    rateUsed = Math.Round(rate.RateValue * priceIncrease, 4);
                }
            }
            else
            {
                switch (costCategory.EnergySubType.Id)
                {
                    case 1: //Normal
                        rateUsed = periodicData.RateNormal;
                        break;
                    case 2: //low
                        rateUsed = periodicData.RateLow;
                        break;
                    case 3: //return normal
                        rateUsed = periodicData.RateReturnNormal < 0 ? 0 - periodicData.RateReturnNormal : periodicData.RateReturnNormal;
                        break;
                    case 4: //return low
                        rateUsed = periodicData.RateReturnNormal < 0 ? 0 - periodicData.RateReturnLow : periodicData.RateReturnLow;
                        break;
                    case 5: //Other
                    case 6: // Return cost normal
                    case 7: // Return low normal
                        rateUsed = rate.RateValue;
                        break;
                }
            }

            //Als er nog geen sd record is eerste aanmaken
            // Zoek laatste record, kijk of tarief gelijk is anders nieuwe maken en toevoegen

            settlementData = settlementDataList.LastOrDefault(x => x.CorrectionFactor == periodicData.CorrectionFactor
                                                                 && x.LastAvailableRateUsed == lastAvailableRateUsed
                                                                 && x.LastAvailableVatRateUsed == lastAvailableVatRateUsed
                                                                 && x.DataPredicted == periodicData.IsPredicted
                                                                 && x.VatTarif == vat
                                                                 && x.Rate == rateUsed);

            if (settlementData is null || settlementData.Rate != lastRate)
            {
                settlementData = new SettlementData();
                settlementData.Description = costCategory.Name.Trim();
                settlementData.Rate = rateUsed;
                settlementData.StartDate = periodicData.ValueXDate;
                settlementData.EndDate = periodicData.ValueXDate;
                settlementData.CorrectionFactor = periodicData.CorrectionFactor;
                settlementData.LastAvailableRateUsed = lastAvailableRateUsed;
                settlementData.PriceIncrease = priceIncrease;
                settlementData.DataPredicted = periodicData.IsPredicted;
                settlementData.VatTarif = vat;
                settlementData.LastAvailableVatRateUsed = lastAvailableVatRateUsed;
                settlementData.MaxStaffelRange += periodicData.ValueX;

                settlementDataList.Add(settlementData);
            }
            else
                settlementData.EndDate = periodicData.ValueXDate;

            switch (costCategory.EnergySubType.Id)
            {
                case 1:
                    //Normal
                    settlementData.ValueBaseConsumed += periodicData.ValueYNormal;
                    break;
                case 2:
                    //low
                    settlementData.ValueBaseConsumed += periodicData.ValueYLow;
                    break;
                case 3:
                    //return normal
                    settlementData.ValueBaseConsumed -= periodicData.ValueYReturnNormal;
                    break;
                case 4:
                    //return low
                    settlementData.ValueBaseConsumed -= periodicData.ValueYReturnLow;
                    break;
                case 5:
                    //Other

                    // CalculationType== 1: Per unit
                    if (costCategory.CalculationType.Id == 1)
                    {
                        settlementData.ValueBaseConsumed += (periodicData.ValueYNormal + periodicData.ValueYLow);
                        if ((periodicData.NettingValueYReturnNormal + periodicData.NettingValueYReturnLow) != 0)
                            settlementData.ValueBaseProduced += 1 - (periodicData.NettingValueYReturnNormal + periodicData.NettingValueYReturnLow);
                    }
                    else
                    {
                        settlementData.ValueBaseConsumed += 0;
                        settlementData.ValueBaseProduced += 0;
                    }
                    break;
                case 6:
                    //return cost normal
                    settlementData.ValueBaseConsumed += 0 - periodicData.ValueYReturnNormal;
                    break;
                case 7:
                    //return cost low
                    settlementData.ValueBaseConsumed += 0 - periodicData.ValueYReturnLow;
                    break;
            }

            if (costCategory.CalculationType.Id == 3)
            {
                settlementData.Value = 0;
                settlementData.ValueBaseConsumed = 0;
            }

            lastRate = settlementData.Rate;
            settlementData.Value = settlementData.ValueBase * settlementData.Rate;
            if (costCategory.CanNotBeNegative && settlementData.Value < 0)
                settlementData.Value = 0;
        }

        if (costCategory.CalculationType.Id == 3)
        {
            int dayDiff;

            foreach (SettlementData settlementData2 in settlementDataList)
            {
                dayDiff = (settlementData2.EndDate - settlementData2.StartDate).Days + 1;
                dayDiff = (dayDiff == 0) ? 1 : dayDiff;

                settlementData2.ValueBaseConsumed += dayDiff;
                settlementData2.Value += (settlementData2.Rate * dayDiff);
            }
        }

        return settlementDataList;
    }

    private Tuple<Models.Rate, bool> getRate(long energyTypeId, Models.CostCategory costCategory, PeriodicData periodicData, long tarifGroupId)
    {
        var rate = new Models.Rate();
        var lastAvailableRateUsed = false;

        if (costCategory.EnergySubType.Id >= 5)
        {
            _rates = _rateRepo.SelectByCostCategoryAndDate(energyTypeId, costCategory.Id, periodicData.ValueXDate, periodicData.ValueXDate, tarifGroupId).ToList();
            rate = _rates.Where(x => periodicData.ValueXDate >= x.StartRate && periodicData.ValueXDate <= x.EndRate).FirstOrDefault();

            if (rate == null)
            {
                rate = new Models.Rate();
                rate = _rateRepo.SelectLastRateByDate(energyTypeId, costCategory.Id, periodicData.ValueXDate, tarifGroupId);

                if (rate != null && rate.EndRate < periodicData.ValueXDate)
                    lastAvailableRateUsed = true;
                else
                    rate = new Models.Rate();
            }
        }

        return new Tuple<Models.Rate, bool>(rate, lastAvailableRateUsed);
    }

    private Models.Staffel? getStaffel(long rateId, long maxRange)
    {
        return _rateStaffelRepo.SelectByRateIdAndRange(rateId, maxRange).FirstOrDefault();
    }
}