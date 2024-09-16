using System.Globalization;
using EnergyUse.Core.Context;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager
{
    public class LibSettlementData
    {
        #region Properties

        private readonly EnergyUseContext _context;
        private static List<Models.Rate>? _rates;

        #endregion        

        public LibSettlementData(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);
        }

        public List<SettlementData> GetSettlementCost(long energyTypeId, List<PeriodicData> periodicDataList, Models.CostCategory costCategory, long tarifGroupId)
        {
            List<SettlementData> settlementDataList = new();

            if (costCategory.CalculationType.Id == 1 || costCategory.CalculationType.Id == 3)
            {
                DateTime startRange, endRange;
                decimal priceIncrease = 0;
                decimal lastRate = 0;
                var rateRepo = new Repositories.RepoRate(_context);
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

                    bool lastAvailableRateUsed = false;
                    bool lastAvailableVatRateUsed = false;
                    var rate = new Models.Rate();
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

                    //Ophalen tarieven
                    if (costCategory.EnergySubType.Id >= 5)
                    {
                        _rates = rateRepo.SelectByCostCategoryAndDate(energyTypeId, costCategory.Id, periodicData.ValueXDate, periodicData.ValueXDate, tarifGroupId).ToList();
                        rate = _rates.Where(x => periodicData.ValueXDate >= x.StartRate && periodicData.ValueXDate <= x.EndRate).FirstOrDefault();
                    }

                    if (rate == null)
                    {
                        rate = new Models.Rate();
                        rate = rateRepo.SelectLastRateByDate(energyTypeId, costCategory.Id, periodicData.ValueXDate, tarifGroupId);

                        if (rate != null && rate.EndRate < periodicData.ValueXDate)
                            lastAvailableRateUsed = true;
                        else
                            rate = new Models.Rate();
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
                            case 1:
                                //Normal
                                rateUsed = periodicData.RateNormal;
                                break;
                            case 2:
                                //low
                                rateUsed = periodicData.RateLow;
                                break;
                            case 3:
                                //return normal
                                if (periodicData.RateReturnNormal < 0)
                                    rateUsed = 0 - periodicData.RateReturnNormal;
                                else
                                    rateUsed = periodicData.RateReturnNormal;
                                break;
                            case 4:
                                //return low
                                if (periodicData.RateReturnNormal < 0)
                                    rateUsed = 0 - periodicData.RateReturnLow;
                                else
                                    rateUsed =  periodicData.RateReturnLow;
                                break;
                            case 5:
                                //Other
                                rateUsed = rate.RateValue;
                                break;
                            case 6:
                                rateUsed = rate.RateValue;
                                break;
                            case 7:
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
                            settlementData.ValueBaseConsumed += periodicData.ValueYReturnNormal;
                            break;
                        case 7:
                            //return cost low
                            settlementData.ValueBaseConsumed += periodicData.ValueYReturnLow;
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
            }

            return settlementDataList;
        }
    }
}