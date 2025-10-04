using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnergyUse.Core.Repositories;

public class RepoAvgMeterRate : RepoGeneral<Models.AvgMeterRate>
{
    private readonly EnergyUseContext _context;

    public RepoAvgMeterRate(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectAllByEnergyTypeId(long energyTypeId, long addressId, int month = 0, int week = 0, int day = 0)
    {
        if (month > 0 && day == 0)
            return await SelectByAddressAndEnergyTypePerMonth(energyTypeId, addressId, month);
        else if (week > 0)
            return await SelectByAddressAndEnergyTypePerWeek(energyTypeId, addressId, day);
        else if (day > 0)
            return await SelectByAddressAndEnergyTypePerDay(energyTypeId, addressId, month, day);
        else
            return await SelectByAddressAndEnergyType(energyTypeId, addressId);
    }

    /// <summary>
    /// Calculate total average with correction factor
    /// </summary>
    /// <param name="energyTypeId">Type of energy id</param>
    /// <param name="addressId">Address id</param>
    /// <param name="correction">Correction of consumed energy</param>
    /// <param name="correctionReturn">Correction over return energy</param>
    /// <returns></returns>
    public async Task<Models.AvgMeterRate?> SelectGeneralAvgByAddressAndEnergyType(long energyTypeId, long addressId, decimal correction, decimal correctionReturn)
    {
        var meterReadings = await _context.MeterReadings
            .AsNoTracking()
            .Include(m => m.Meter)
            .Include(a => a.Meter.Address)
            .Where(n => n.EnergyTypeId == energyTypeId && n.Meter.AddressId == addressId)
            .ToListAsync();

        var avgList = meterReadings
            .GroupBy(g => new { EnergyTypeId = g.EnergyTypeId, AddressId = g.Meter.Address.Id })
            .Select(x => new Models.AvgMeterRate
            {
                AddressId = x.Key.AddressId,
                EnergyTypeId = x.Key.EnergyTypeId ?? 0,
                AvgLow = x.Average(t => t.DeltaLow) * correction,
                AvgNormal = x.Average(t => t.DeltaNormal) * correction,
                AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow) * correctionReturn,
                AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal) * correctionReturn
            })
            .OrderBy(o => o.Month)
            .ThenBy(o => o.Day)
            .FirstOrDefault();

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyType(long energyTypeId, long addressId)
    {
        var avgList = await _context.MeterReadings
                                    .AsNoTracking()
                                    .Include(m => m.Meter)
                                    .Include(a => a.Meter.Address)
                                    .Where(n => n.EnergyTypeId == energyTypeId && n.Meter.AddressId == addressId)            
                                    .GroupBy(g => new { EnergyTypeId = g.EnergyTypeId, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Date.Month, Day = g.RegistrationDate.Date.Day })
                                    .Select(x => new Models.AvgMeterRate
                                    {
                                        AddressId = x.Key.AddressId,
                                        EnergyTypeId = x.Key.EnergyTypeId ?? 0,
                                        Month = x.Key.Month,
                                        Day = x.Key.Day,
                                        AvgLow = x.Average(t => t.DeltaLow),
                                        AvgNormal = x.Average(t => t.DeltaNormal),
                                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                                    })
                                    .OrderBy(o => o.Month)
                                    .ThenBy(o => o.Day)
                                    .ToListAsync();

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyTypePerMonth(long energyTypeId, long addressId, int month)
    {
        var avgList = await _context.MeterReadings
                                    .AsNoTracking()
                                    .Include(a => a.Meter)
                                    .Include(a => a.Meter.Address)
                                    .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.RegistrationDate.Date.Month == month)            
                                    .GroupBy(g => new { EnergyTypeId = g.EnergyType.Id, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month })
                                    .Select(x => new Models.AvgMeterRate
                                    {
                                        AddressId = x.Key.AddressId,
                                        EnergyTypeId = x.Key.EnergyTypeId,
                                        Month = x.Key.Month,
                                        AvgLow = x.Average(t => t.DeltaLow),
                                        AvgNormal = x.Average(t => t.DeltaNormal),
                                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                                    })
                                    .OrderBy(o => o.Month)
                                    .ThenBy(o => o.Day)
                                    .ToListAsync();

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyTypePerWeek(long energyTypeId, long addressId, int weekNo)
    {
        var avgList = await _context.MeterReadings
                                    .AsNoTracking()
                                    .Include(a => a.Meter)
                                    .Include(a => a.Meter.Address)
                                    .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.WeekNo == weekNo)
                                    .GroupBy(g => new { EnergyTypeId = g.EnergyTypeId, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Day })
                                    .Select(x => new Models.AvgMeterRate
                                    {
                                        AddressId = x.Key.AddressId,
                                        EnergyTypeId = x.Key.EnergyTypeId ?? 0,
                                        Month = x.Key.Month,
                                        Day = x.Key.Day,
                                        AvgLow = x.Average(t => t.DeltaLow),
                                        AvgNormal = x.Average(t => t.DeltaNormal),
                                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                                    })
                                    .OrderBy(o => o.Month)
                                    .ThenBy(o => o.Day)
                                    .ToListAsync();

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyTypePerDay(long energyTypeId, long addressId, int month, int day)
    {
        var avgList = await _context.MeterReadings
                                    .AsNoTracking()
                                    .Include(a => a.Meter)
                                    .Include(a => a.Meter.Address)
                                    .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.RegistrationDate.Date.Month == month && w.RegistrationDate.Date.Day == day)
                                    .GroupBy(g => new { EnergyTypeId = g.EnergyType.Id, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Date.Day })
                                    .Select(x => new Models.AvgMeterRate
                                    {
                                        AddressId = x.Key.AddressId,
                                        EnergyTypeId = x.Key.EnergyTypeId,
                                        Month = x.Key.Month,
                                        Day = x.Key.Day,
                                        AvgLow = x.Average(t => t.DeltaLow),
                                        AvgNormal = x.Average(t => t.DeltaNormal),
                                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                                    })
                                    .OrderBy(o => o.Month)
                                    .ThenBy(o => o.Day)
                                    .ToListAsync(); 

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyTypePerPeriod(long energyTypeId, long addressId)
    {
        var avgList = await _context.MeterReadings
                    .AsNoTracking()
                    .Include(a => a.Meter)
                    .Include(a => a.Meter.Address)
                    .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId)
                    .GroupBy(g => new { EnergyTypeId = g.EnergyType.Id, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Date.Day })
                    .Select(x => new Models.AvgMeterRate
                    {
                        AddressId = x.Key.AddressId,
                        EnergyTypeId = x.Key.EnergyTypeId,
                        Month = x.Key.Month,
                        Day = x.Key.Day,
                        AvgLow = x.Average(t => t.DeltaLow),
                        AvgNormal = x.Average(t => t.DeltaNormal),
                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                    })
                    .OrderBy(o => o.Month)
                    .ThenBy(o => o.Day)
                    .ToListAsync();

        return avgList;
    }

    public async Task<IEnumerable<Models.AvgMeterRate>> SelectByAddressAndEnergyTypePerPeriodFromDate(long energyTypeId, long addressId, DateTime fromDate)
    {
        var avgList = await _context.MeterReadings
                    .AsNoTracking()
                    .Include(a => a.Meter)
                    .Include(a => a.Meter.Address)
                    .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.RegistrationDate.Date >= fromDate.Date)
                    .GroupBy(g => new { EnergyTypeId = g.EnergyType.Id, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Date.Day })
                    .Select(x => new Models.AvgMeterRate
                    {
                        AddressId = x.Key.AddressId,
                        EnergyTypeId = x.Key.EnergyTypeId,
                        Month = x.Key.Month,
                        Day = x.Key.Day,
                        AvgLow = x.Average(t => t.DeltaLow),
                        AvgNormal = x.Average(t => t.DeltaNormal),
                        AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                        AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                    })
                    .OrderBy(o => o.Month)
                    .ThenBy(o => o.Day)
                    .ToListAsync();

        return avgList;
    }
}