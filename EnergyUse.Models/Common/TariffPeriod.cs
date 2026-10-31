namespace EnergyUse.Models.Common;

public record TariffPeriod(DateTime StartDate, DateTime EndDate, long TariffGroupId, string TariffGroupName);
