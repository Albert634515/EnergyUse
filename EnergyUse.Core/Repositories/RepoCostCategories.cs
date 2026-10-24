using EnergyUse.Core.Context;
using EnergyUse.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoCostCategories : RepoGeneral<Models.CostCategory>
{
    private readonly EnergyUseContext _context;

    public RepoCostCategories(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.CostCategory?> Get(long costCategoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .FirstOrDefaultAsync(w => w.Id == costCategoryId, cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CostCategory>> SelectByEnergyTypeAndUntit(long energyTypeId, string unitId, CancellationToken cancellationToken = default)
    {
        var categoryList = await _context.Set<Models.Rate>()
                                         .Where(w => w.EnergyTypeId == energyTypeId)
                                         .Select(s => s.CostCategory.Id)
                                         .ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);

        if (categoryList.Count == 0)
            categoryList.Add(0);

        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Include(u => u.Unit)
                       .Where(w => w.EnergyType.Id == energyTypeId && w.Unit.Id == unitId && categoryList.Contains(w.Id))
                       .OrderBy(o => o.SortOrder)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CostCategory>> SelectByEnergyType(long energytypeid, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Where(w => w.EnergyType.Id == energytypeid)
                       .OrderBy(o => o.EnergySubType.Id).ThenBy(t => t.SortOrder)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<Models.CostCategory?> SelectByEnergyTypeAndSubType(long energyTypeId, Common.Enums.SubEnergyType subEnergyType, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Where(w => w.EnergyType.Id == energyTypeId && w.EnergySubType.Id == (int)subEnergyType)
                       .OrderBy(o => o.SortOrder)
                       .FirstOrDefaultAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CostCategory>> SelectByEnergyTypeId(long energyTypeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Where(w => w.EnergyType.Id == energyTypeId)
                       .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CostCategory>> SelectByEnergyTypeAndRange(long energyTypeId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Where(w => w.EnergyType.Id == energyTypeId
                                && (w.Start == null || w.Start.Value.Date <= endDate.Date)
                                && (w.End == null || w.End.Value.Date >= startDate.Date))
                       .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CostCategory>> SelectByEnergyTypeIdVatCalculated(long energyTypeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CostCategory>()
                       .Include(e => e.EnergyType)
                       .Include(s => s.EnergySubType)
                       .Include(c => c.CalculationType)
                       .Where(w => w.EnergyTypeId == energyTypeId && w.CalculateVat)
                       .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public Models.CostCategory GetDefault(long energyTypeId)
    {
        Models.CostCategory costCategory = new();
        costCategory.Id = 0;
        costCategory.EnergySubType = new Models.EnergySubType();
        costCategory.EnergyType = new Models.EnergyType();
        costCategory.EnergyType.Id = (int)energyTypeId;

        return costCategory;
    }

    public async Task<List<Models.CostCategory>> SelectByTariffGroup(long tariffGroupId, long? energyTypeId = null)
    {
        return await _context.CostCategories
            .Where(category => (!energyTypeId.HasValue || category.EnergyTypeId == energyTypeId)
                && _context.TariffGroupCostCategories.Any(link => link.TariffGroupId == tariffGroupId
                    && link.CostCategoryId == category.Id))
            .OrderBy(category => category.SortOrder).ThenBy(category => category.Name)
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<long>> GetAllowedTariffGroupIds(long costCategoryId)
    {
        return await _context.TariffGroupCostCategories.Where(link => link.CostCategoryId == costCategoryId)
            .Select(link => link.TariffGroupId).ToListAsync().ConfigureAwait(false);
    }

    #region Mappers

    public async Task<List<SettlementData>> MapCostCategories(List<PeriodicData> periodicDataList, CancellationToken cancellationToken = default)
    {
        List<SettlementData> settlementDatas = new();
        SettlementData? settlementData = null;

        foreach (var periodicData in periodicDataList)
        {
            foreach (var otherCost in periodicData.OtherCosts)
            {
                var costCategory = await Get(otherCost.CostCategoryId, cancellationToken).ConfigureAwait(false);

                if (costCategory is null)
                    throw new Exception($"Cost category {otherCost.CostCategoryId} not found");

                var calculationTypeId = costCategory.CalculationTypeId;
                var categoryRate = otherCost.Rate;

                settlementData = settlementDatas.LastOrDefault(x => x.CorrectionFactor == periodicData.CorrectionFactor
                                                     && x.LastAvailableRateUsed == otherCost.LastAvailableRateUsed
                                                     && x.LastAvailableVatRateUsed == otherCost.LastAvailableVatRateUsed
                                                     && x.DataPredicted == periodicData.IsPredicted
                                                     && x.VatTarif == otherCost.VatTarif
                                                     && x.RateId == otherCost.RateId
                                                     && x.Rate == categoryRate
                                                     && x.CostCategory.Id == otherCost.CostCategoryId
                                                     && (x.EndDate.Date == periodicData.ValueXDate.Date
                                                         || x.EndDate.Date.AddDays(1) == periodicData.ValueXDate.Date));

                if (settlementData is null)
                {
                    // Look up cost category for extra info
                    settlementData = new SettlementData
                    {
                        Description = costCategory.Name,
                        CostCategory = costCategory,
                        CorrectionFactor = periodicData.CorrectionFactor,
                        LastAvailableRateUsed = otherCost.LastAvailableRateUsed,
                        LastAvailableVatRateUsed = otherCost.LastAvailableVatRateUsed,
                        PriceAdjustmentFactor = otherCost.PriceAdjustmentFactor,
                        DataPredicted = periodicData.IsPredicted,
                        VatTarif = otherCost.VatTarif,
                        RateId = otherCost.RateId,
                        Rate = categoryRate,
                        StartDate = periodicData.ValueXDate,
                        EndDate = periodicData.ValueXDate
                    };

                    settlementDatas.Add(settlementData);
                }
                else
                {
                    settlementData.EndDate = periodicData.ValueXDate;
                }

                if (otherCost.Quantity is not null)
                {
                    if (costCategory.EnergySubType.Id is 3 or 4 or 6 or 7)
                        settlementData.ValueBaseProduced -= otherCost.Quantity.Value;
                    else
                        settlementData.ValueBaseConsumed += otherCost.Quantity.Value;
                }
                else switch (costCategory.EnergySubType.Id)
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
                        settlementData.ValueBaseProduced -= periodicData.ValueYReturnNormal;
                        break;
                    case 4:
                        //return low
                        settlementData.ValueBaseProduced -= periodicData.ValueYReturnLow;
                        break;
                    case 5:
                        //Other

                        if (calculationTypeId == 1)
                        {
                            // CalculationType== 1: Per unit
                            settlementData.ValueBaseConsumed += (periodicData.ValueYNormal + periodicData.ValueYLow);
                            settlementData.ValueBaseProduced -= periodicData.NettingValueYReturnNormal + periodicData.NettingValueYReturnLow;
                        }
                        else if (calculationTypeId == 3)
                        {
                            // CalculationType== 3: Per day
                        }
                        else
                        {
                            settlementData.ValueBaseConsumed += 0;
                            settlementData.ValueBaseProduced += 0;
                        }
                        break;
                    case 6:
                        //return cost normal
                        settlementData.ValueBaseProduced += 0 - periodicData.ValueYReturnNormal;
                        break;
                    case 7:
                        //return cost low
                        settlementData.ValueBaseProduced += 0 - periodicData.ValueYReturnLow;
                        break;
                }

                settlementData.Value = calculationTypeId == 2
                    ? settlementData.ValueBase * settlementData.Rate / 100m
                    : settlementData.ValueBase * settlementData.Rate;
                if (costCategory.CanNotBeNegative && settlementData.Value < 0)
                    settlementData.Value = 0;
            }
        }

        // Round once after accumulating each complete line. VAT uses its rounded net amount.
        foreach (var line in settlementDatas)
        {
            line.Value = Math.Round(line.Value, 2, MidpointRounding.AwayFromZero);
            line.VatAmount = line.CostCategory.CalculateVat
                ? Math.Round(line.Value * line.VatTarif / 100m, 2, MidpointRounding.AwayFromZero)
                : 0m;
        }

        settlementDatas = settlementDatas.OrderBy(o => o.CostCategory.SortOrder).ToList();

        return settlementDatas;
    }

    #endregion

}
