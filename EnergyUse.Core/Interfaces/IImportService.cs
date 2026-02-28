using EnergyUse.Core.Interfaces;
using EnergyUse.Models;

public interface IImportService
{
    Task<List<MeterReading>> ImportAsync(string fileName,
                                        Address address,
                                        EnergyType energyType,
                                        Meter selectedMeter,
                                        EnergyUse.Core.UnitOfWork.Import uow);
}