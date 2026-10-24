namespace EnergyUse.Core.Manager;

public sealed class RateValidationException(string message) : InvalidOperationException(message);
