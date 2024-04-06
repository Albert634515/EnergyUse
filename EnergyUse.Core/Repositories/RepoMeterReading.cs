using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoMeterReading : RepoGeneral<Models.MeterReading>
    {
        private readonly EnergyUseContext _context;

        public RepoMeterReading(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Models.MeterReading> SelectByExists(DateTime registrationDate, long energyTypeId, long meterId)
        {
            return _context.MeterReadings
                    .Include(e => e.EnergyType)
                    .Include(t => t.Meter)
                    .Include(a => a.Meter.Address)
                    .Where(n => n.Meter.Id == meterId 
                             && n.EnergyType.Id == energyTypeId
                             && n.RegistrationDate.Date == registrationDate.Date);
        }


        public IEnumerable<Models.MeterReading> SelectByRange(DateTime startRange, DateTime endRange, long energyTypeId, long addressId, int month = 0, int week = 0, int day = 0)
        {
            var meterReadingList = _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId 
                         && n.Meter.Address.Id == addressId 
                         && n.RegistrationDate.Date >= startRange.Date 
                         && n.RegistrationDate.Date <= endRange.Date)
                .OrderBy(o => o.RegistrationDate);

            if (month > 0 && day == 0)
                meterReadingList = (IOrderedQueryable<Models.MeterReading>)meterReadingList.Where(m => m.RegistrationDate.Month == month);
            if (week > 0)
            {
                meterReadingList = (IOrderedQueryable<Models.MeterReading>)meterReadingList.Where(m => m.WeekNo == week);
            }
            if (day > 0)
                meterReadingList = (IOrderedQueryable<Models.MeterReading>)meterReadingList.Where(m => m.RegistrationDate.DayOfYear == day);

            return meterReadingList;
        }

        public IEnumerable<Models.MeterReading> SelectByEnergyIdAndAddressId(long energyTypeId, long addressId)
        {
            return _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Meter.Address.Id == addressId)
                .OrderBy(o => o.RegistrationDate);
        }

        public Models.MeterReading? SelectFirstRow(long energyTypeId, long addressId)
        {
            return _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Meter.Address.Id == addressId)
                .OrderBy(o => o.RegistrationDate)
                .FirstOrDefault();
        }

        public Models.MeterReading? SelectLastRow(long energyTypeId, long addressId)
        {
            return _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Meter.Address.Id == addressId)
                .OrderByDescending(o => o.RegistrationDate)
                .FirstOrDefault();
        }

        public Models.MeterReading? SelectLastRowFromDate(DateTime registrationDate, long energyTypeId, long addressId)
        {
            return _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Meter.Address.Id == addressId && n.RegistrationDate.Date < registrationDate.Date)
                .OrderByDescending(o => o.RegistrationDate)
                .FirstOrDefault();
        }

        public Models.MeterReading? SelectRow(DateTime registrationDate, long energyTypeId, long addressId)
        {
            return _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(t => t.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Meter.Address.Id == addressId && n.RegistrationDate.Date <= registrationDate.Date)
                .OrderByDescending(o => o.RegistrationDate)
                .FirstOrDefault();
        }

        public long? GetLastId()
        {
           return _context.MeterReadings
                          .Include(e => e.EnergyType)
                          .Include(t => t.Meter)
                          .Include(a => a.Meter.Address)
                          .Max(o => o.Id);
        }

        public Models.MeterReading GetDefaultReading(EnergyUse.Models.EnergyType energyType, EnergyUse.Models.Meter meter)
        {
            var defaultMeterReading = _context.MeterReadings
                          .Include(e => e.EnergyType)
                          .Include(m => m.Meter)
                          .Include(a => a.Meter.Address)
                          .Where(n => n.EnergyType.Id == energyType.Id && n.Meter.Id == meter.Id)
                          .FirstOrDefault();

            if (defaultMeterReading == null)
            {
                defaultMeterReading = new Models.MeterReading();
                defaultMeterReading.EnergyType = energyType;
                defaultMeterReading.Meter = new Models.Meter();
                defaultMeterReading.Meter = meter;
            }

            return defaultMeterReading;
        }

    }
}