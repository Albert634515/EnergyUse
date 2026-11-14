using EnergyUse.Core.Interfaces;
using EnergyUse.Models;

public interface IImportService
{
    Task<EnergyUse.Core.Import.ImportPreview> ImportAsync(string fileName,
                                        Address address,
                                        EnergyType energyType,
                                        Meter selectedMeter);
    Task<int> SaveAsync(EnergyUse.Core.Import.ImportPreview preview);
}
