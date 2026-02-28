using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using System.Globalization;
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

            var minDate = imported.Min(x => x.RegistrationDate);
            var maxDate = imported.Max(x => x.RegistrationDate);

            uow.meterReadings = uow.MeterReadingRepo
                .SelectByRange(minDate.AddDays(-7), maxDate, energyType.Id, address.Id)
                .ToList();

            var meterList = (await uow.MeterRepo
                .SelectByAddressAndEnergyType(address.Id, energyType.Id))
                .OrderByDescending(x => x.ActiveFrom)
                .ToList();

            var libMeterReading = new LibMeterReading(_db);
            MeterReading lastReading = null;

            foreach (var importedReading in imported.OrderBy(x => x.RegistrationDate))
            {
                var meter = meterList
                    .Where(m => m.ActiveFrom.Date <= importedReading.RegistrationDate.Date)
                    .OrderBy(m => m.ActiveFrom)
                    .LastOrDefault();

                var existing = uow.MeterReadingRepo
                    .SelectByExists(importedReading.RegistrationDate.Date, importedReading.EnergyType.Id, meter.Id)
                    .FirstOrDefault();

                if (importedReading.RegistrationDate.Date == meter.ActiveFrom.Date)
                {
                    if (existing != null && existing.Id != null)
                        lastReading = new MeterReading();
                    else
                        existing = null;
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
    }
}