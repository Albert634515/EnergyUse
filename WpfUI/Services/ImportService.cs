using EnergyUse.Core.Import;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using WpfUI.Managers;

namespace WpfUI.Services;

public class ImportService : IImportService
{
    public Task<ImportPreview> ImportAsync(string fileName, Address address, EnergyType energyType, Meter selectedMeter)
    {
        var database = Config.GetDbFileName();
        var source = LibEpplus.ReadCsvFile(fileName, energyType, selectedMeter);
        return new MeterReadingImport(database).PrepareAsync(source, address.Id, energyType.Id);
    }

    public Task<int> SaveAsync(ImportPreview preview) => new MeterReadingImport(Config.GetDbFileName()).SaveAsync(preview);
}
