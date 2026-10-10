using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoMeterReading : RepoGeneral<Models.MeterReading>
{
    private readonly EnergyUseContext _context;

    public RepoMeterReading(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    private IQueryable<Models.MeterReading> meterReadingsWithDetails()
    {
        return _context.MeterReadings
            .Include(reading => reading.EnergyType)
            .Include(reading => reading.Meter)
            .ThenInclude(meter => meter!.Address);
    }

    public async Task<IEnumerable<Models.MeterReading>> SelectByExists(DateTime registrationDate, long energyTypeId, long meterId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
                .AsNoTracking()
                .Where(n => n.MeterId == meterId
                         && n.EnergyTypeId == energyTypeId
                         && n.RegistrationDate.Date == registrationDate.Date)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.MeterReading>> SelectByMeterId(long meterId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(reading => reading.MeterId == meterId)
            .OrderBy(reading => reading.RegistrationDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }


    public async Task<IEnumerable<Models.MeterReading>> SelectByRange(DateTime startRange, DateTime endRange, long energyTypeId, long addressId, int month = 0, int week = 0, int day = 0, CancellationToken cancellationToken = default)
    {
        var meterReadingList = meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId
                     && n.RegistrationDate.Date >= startRange.Date
                     && n.RegistrationDate.Date <= endRange.Date);

        if (month > 0 && day == 0)
            meterReadingList = meterReadingList.Where(m => m.RegistrationDate.Month == month);
        if (week > 0)
            meterReadingList = meterReadingList.Where(m => m.WeekNo == week);
        if (day > 0)
            meterReadingList = meterReadingList.Where(m => m.RegistrationDate.DayOfYear == day);

        return await meterReadingList
            .OrderBy(o => o.RegistrationDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.MeterReading>> SelectByEnergyIdAndAddressId(long energyTypeId, long addressId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId)
            .OrderBy(o => o.RegistrationDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading?> SelectFirstRow(long energyTypeId, long addressId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId)
            .OrderBy(o => o.RegistrationDate)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading?> SelectLastRow(long energyTypeId, long addressId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId)
            .OrderByDescending(o => o.RegistrationDate)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading?> SelectLastRowFromDate(DateTime registrationDate, long energyTypeId, long addressId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId
                     && n.RegistrationDate.Date < registrationDate.Date)
            .OrderByDescending(o => o.RegistrationDate)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading?> SelectRow(DateTime registrationDate, long energyTypeId, long addressId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId
                     && n.RegistrationDate.Date <= registrationDate.Date)
            .OrderByDescending(o => o.RegistrationDate)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading?> SelectRow(DateTime registrationDate, long energyTypeId, long addressId, long meterId, CancellationToken cancellationToken = default)
    {
        return await meterReadingsWithDetails()
            .Where(n => n.EnergyTypeId == energyTypeId
                     && n.Meter != null
                     && n.Meter.AddressId == addressId
                     && n.MeterId == meterId
                     && n.RegistrationDate.Date <= registrationDate.Date)
            .OrderByDescending(o => o.RegistrationDate)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<long?> GetLastId(CancellationToken cancellationToken = default)
    {
        return await _context.MeterReadings
                       .AsNoTracking()
                       .MaxAsync(o => o.Id, cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<Models.MeterReading> GetDefaultReading(EnergyUse.Models.EnergyType energyType, EnergyUse.Models.Meter meter, CancellationToken cancellationToken = default)
    {
        var defaultMeterReading = await meterReadingsWithDetails()
                      .Where(n => n.EnergyTypeId == energyType.Id && n.MeterId == meter.Id)
                      .FirstOrDefaultAsync(cancellationToken)
                      .ConfigureAwait(false);

        if (defaultMeterReading == null)
        {
            defaultMeterReading = new Models.MeterReading
            {
                EnergyTypeId = energyType.Id,
                EnergyType = energyType,
                MeterId = meter.Id,
                Meter = meter
            };
        }

        return defaultMeterReading;
    }
}
