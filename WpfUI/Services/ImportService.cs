using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using System.Globalization;
using System.IO;
using WpfUI.Managers;

namespace WpfUI.Services
{
    public class ImportService : IImportService
    {
        private readonly LibSettings _settings;
        private readonly string _db;

        public ImportService()
        {
            // Haal de databasebestandsnaam zelf op
            _db = Config.GetDbFileName();
            _settings = new LibSettings(_db);
        }

        public async Task<List<MeterReading>> ImportAsync(
            string fileName,
            Address address,
            EnergyType energyType,
            Meter selectedMeter,
            EnergyUse.Core.UnitOfWork.Import uow)
        {
            var libEpplus = new LibEpplus(_db);
            var imported = libEpplus.ImportFromCsvFile(fileName, energyType, selectedMeter);

            if (imported.Count == 0)
                return new List<MeterReading>();

            imported = imported
                .OrderByDescending(x => x.RegistrationDate)
                .GroupBy(x => x.RegistrationDate.Date)
                .Select(g => g.First())
                .ToList();

            var meterList = (await uow.MeterRepo
                .SelectByAddressAndEnergyType(address.Id, energyType.Id))
                .OrderByDescending(x => x.ActiveFrom)
                .ToList();
            var meterByDate = new Dictionary<DateTime, Meter>();

            foreach (var importedReading in imported)
            {
                var registrationDate = importedReading.RegistrationDate.Date;
                var matchingMeters = meterList
                    .Where(m => m.ActiveFrom.Date <= registrationDate
                             && (m.ActiveTill == null || m.ActiveTill.Value.Date >= registrationDate))
                    .ToList();

                if (matchingMeters.Count == 0)
                    throw new InvalidDataException(getMissingMeterMessage(meterList, energyType, registrationDate));

                if (matchingMeters.Count > 1)
                {
                    throw new InvalidDataException(
                        $"Multiple meters are active for {energyType.Name} on {registrationDate:yyyy-MM-dd}. " +
                        "Correct the meter periods before importing data.");
                }

                meterByDate[registrationDate] = matchingMeters[0];
            }

            var minDate = imported.Min(x => x.RegistrationDate);
            var maxDate = imported.Max(x => x.RegistrationDate);

            uow.meterReadings = (await uow.MeterReadingRepo
                .SelectByRange(minDate.AddDays(-7), maxDate, energyType.Id, address.Id))
                .ToList();

            var libMeterReading = new LibMeterReading(_db);
            var firstImportedReading = imported.MinBy(x => x.RegistrationDate)!;
            MeterReading? lastReading = await uow.MeterReadingRepo.SelectLastRowFromDate(
                firstImportedReading.RegistrationDate,
                energyType.Id,
                address.Id);

            foreach (var importedReading in imported.OrderBy(x => x.RegistrationDate))
            {
                var meter = meterByDate[importedReading.RegistrationDate.Date];

                var existing = (await uow.MeterReadingRepo
                    .SelectByExists(importedReading.RegistrationDate.Date, energyType.Id, meter.Id))
                    .FirstOrDefault();

                if (importedReading.RegistrationDate.Date == meter.ActiveFrom.Date ||
                    lastReading?.MeterId != meter.Id)
                {
                    lastReading = null;
                }

                if (existing == null)
                {
                    var newReading = new MeterReading
                    {
                        Id = null,
                        EnergyTypeId = energyType.Id,
                        MeterId = meter.Id,
                        RegistrationDate = importedReading.RegistrationDate.Date,
                        WeekNo = ISOWeek.GetWeekOfYear(importedReading.RegistrationDate),
                        RateNormal = importedReading.RateNormal,
                        RateLow = importedReading.RateLow,
                        ReturnDeliveryLow = importedReading.ReturnDeliveryLow,
                        ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal
                    };

                    libMeterReading.CalculateDiff(ref newReading, lastReading);
                    lastReading = newReading;

                    uow.meterReadings.Add(newReading);
                }
                else
                {
                    existing = uow.meterReadings.First(x => x.Id == existing.Id);

                    existing.RateNormal = importedReading.RateNormal;
                    existing.RateLow = importedReading.RateLow;
                    existing.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
                    existing.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;

                    libMeterReading.CalculateDiff(ref existing, lastReading);
                    lastReading = existing;
                }
            }

            return uow.meterReadings
                .OrderByDescending(x => x.RegistrationDate)
                .ToList();
        }

        private static string getMissingMeterMessage(List<Meter> meters, EnergyType energyType, DateTime registrationDate)
        {
            var previousMeter = meters
                .Where(meter => meter.ActiveFrom.Date <= registrationDate)
                .OrderByDescending(meter => meter.ActiveFrom)
                .FirstOrDefault();

            if (previousMeter?.ActiveTill is DateTime activeTill && activeTill.Date < registrationDate)
            {
                return $"Meter '{previousMeter.Description}' was closed on {activeTill:yyyy-MM-dd}. " +
                       $"No meter is available for {energyType.Name} on {registrationDate:yyyy-MM-dd}.";
            }

            return $"No meter is available for {energyType.Name} on {registrationDate:yyyy-MM-dd}.";
        }
    }
}
