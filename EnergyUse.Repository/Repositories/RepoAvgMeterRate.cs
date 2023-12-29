using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoAvgMeterRate : RepoGeneral<Models.AvgMeterRate>
    {
        private readonly EnergyUseContext _context;

        public RepoAvgMeterRate(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Models.AvgMeterRate> SelectAllByEnergyTypeId(long energyTypeId, long addressId, int month = 0, int week = 0, int day = 0)
        {
            if (month > 0 && day == 0)
                return SelectByAddressAndEnergyTypePerMonth(energyTypeId, addressId, month);
            else if (week > 0)
                return SelectByAddressAndEnergyTypePerWeek(energyTypeId, addressId, day);
            else if (day > 0)
                return SelectByAddressAndEnergyTypePerDay(energyTypeId, addressId, month, day);
            else
                return SelectByAddressAndEnergyType(energyTypeId, addressId);
        }

        /// <summary>
        /// Calculate total average with correction factor
        /// </summary>
        /// <param name="energyTypeId">Type of energy id</param>
        /// <param name="addressId">Address id</param>
        /// <param name="correction">Correction of consumed energy</param>
        /// <param name="correctionReturn">Correction over return energy</param>
        /// <returns></returns>
        public Models.AvgMeterRate? SelectGeneralAvgByAddressAndEnergyType(long energyTypeId, long addressId, decimal correction, decimal correctionReturn)
        {
            var avgList = _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(m => m.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyTypeId == energyTypeId && n.Meter.AddressId == addressId).ToList()
                .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id })
                .Select(x => new Models.AvgMeterRate
                {
                    AddressId = x.Key.AddressId,
                    EnergyType = x.Key.EnergyType,
                    AvgLow = x.Average(t => t.DeltaLow) * correction,
                    AvgNormal = x.Average(t => t.DeltaNormal) * correction,
                    AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow) * correctionReturn,
                    AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal) * correctionReturn
                })
                .OrderBy(o => o.Month).ThenBy(o => o.Day).FirstOrDefault();

            return avgList;
        }

        public IEnumerable<Models.AvgMeterRate> SelectByAddressAndEnergyType(long energyTypeId, long addressId)
        {
            var avgList = _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(m => m.Meter)
                .Include(a => a.Meter.Address)
                .Where(n => n.EnergyTypeId == energyTypeId && n.Meter.AddressId == addressId).ToList()
                .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Date.Month, Day = g.RegistrationDate.Date.Day })
                .Select(x => new Models.AvgMeterRate
                {
                    AddressId = x.Key.AddressId,
                    EnergyType = x.Key.EnergyType,
                    Month = x.Key.Month,
                    Day = x.Key.Day,
                    AvgLow = x.Average(t => t.DeltaLow),
                    AvgNormal = x.Average(t => t.DeltaNormal),
                    AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                    AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                })
                .OrderBy(o => o.Month).ThenBy(o => o.Day);

            return avgList;
        }

        public IEnumerable<Models.AvgMeterRate> SelectByAddressAndEnergyTypePerMonth(long energyTypeId, long addressId, int month)
        {
            var avgList = _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(a => a.Meter)
                .Include(a => a.Meter.Address)
                .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.RegistrationDate.Date.Month == month).ToList()
                .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month })
                .Select(x => new Models.AvgMeterRate
                {
                    AddressId = x.Key.AddressId,
                    EnergyType = x.Key.EnergyType,
                    Month = x.Key.Month,
                    AvgLow = x.Average(t => t.DeltaLow),
                    AvgNormal = x.Average(t => t.DeltaNormal),
                    AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                    AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                })
                .OrderBy(o => o.Month).ThenBy(o => o.Day);

            return avgList;
        }

        public IEnumerable<Models.AvgMeterRate> SelectByAddressAndEnergyTypePerWeek(long energyTypeId, long addressId, int weekNo)
        {
            var avgList = _context.MeterReadings
                .Include(e => e.EnergyType)
                .Include(a => a.Meter)
                .Include(a => a.Meter.Address)
                .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.WeekNo == weekNo).ToList()
                .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Day })
                .Select(x => new Models.AvgMeterRate
                {
                    AddressId = x.Key.AddressId,
                    EnergyType = x.Key.EnergyType,
                    Month = x.Key.Month,
                    Day = x.Key.Day,
                    AvgLow = x.Average(t => t.DeltaLow),
                    AvgNormal = x.Average(t => t.DeltaNormal),
                    AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                    AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                })
                .OrderBy(o => o.Month).ThenBy(o => o.Day);

            return avgList;
        }

        public IEnumerable<Models.AvgMeterRate> SelectByAddressAndEnergyTypePerDay(long energyTypeId, long addressId, int month, int day)
        {
            var avgList = _context.MeterReadings
                        .Include(e => e.EnergyType)
                        .Include(a => a.Meter)
                        .Include(a => a.Meter.Address)
                        .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId && w.RegistrationDate.Date.Month == month && w.RegistrationDate.Date.Day == day).ToList()
                        .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Date.Day })
                        .Select(x => new Models.AvgMeterRate
                        {
                            AddressId = x.Key.AddressId,
                            EnergyType = x.Key.EnergyType,
                            Month = x.Key.Month,
                            Day = x.Key.Day,
                            AvgLow = x.Average(t => t.DeltaLow),
                            AvgNormal = x.Average(t => t.DeltaNormal),
                            AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                            AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                        })
                        .OrderBy(o => o.Month).ThenBy(o => o.Day);

            return avgList;
        }

        public IEnumerable<Models.AvgMeterRate> SelectByAddressAndEnergyTypePerPeriod(long energyTypeId, long addressId)
        {
            var avgList = _context.MeterReadings
                        .Include(e => e.EnergyType)
                        .Include(a => a.Meter)
                        .Include(a => a.Meter.Address)
                        .Where(w => w.EnergyType.Id == energyTypeId && w.Meter.Address.Id == addressId).ToList()
                        .GroupBy(g => new { EnergyType = g.EnergyType, AddressId = g.Meter.Address.Id, Month = g.RegistrationDate.Month, Day = g.RegistrationDate.Date.Day })
                        .Select(x => new Models.AvgMeterRate
                        {
                            AddressId = x.Key.AddressId,
                            EnergyType = x.Key.EnergyType,
                            Month = x.Key.Month,
                            Day = x.Key.Day,
                            AvgLow = x.Average(t => t.DeltaLow),
                            AvgNormal = x.Average(t => t.DeltaNormal),
                            AvgReturnDeliveryLow = x.Average(t => t.ReturnDeliveryDeltaLow),
                            AvgReturnDeliveryNormal = x.Average(t => t.ReturnDeliveryDeltaNormal)
                        })
                        .OrderBy(o => o.Month).ThenBy(o => o.Day);

            return avgList;
        }
    }
}
