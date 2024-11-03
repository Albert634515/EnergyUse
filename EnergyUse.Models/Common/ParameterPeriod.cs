using EnergyUse.Common.Enums;

namespace EnergyUse.Models.Common;

public class ParameterPeriod
{
    public Models.EnergyType EnergyType { get; set; } = new();
    public long AddressId { get; set; } = 0;
    public DateTime StartRange { get; set; } = DateTime.MinValue;
    public DateTime EndRange  { get; set; } = DateTime.MinValue;
    public ShowType ShowType { get; set; } = ShowType.Unknown;
    public Period PeriodType { get; set; } = Period.Unknown;
    public bool PredictMissingData { get; set; } = false;
    public long TarifGroupId { get; set; } = 0;
    public decimal QuantityReduction { get; set; } = 0;
    public bool SetMiddleOfYear { get; set; } = false;
    public int Month { get; set; } = 0;
    public int Week  { get; set; } = 0;
    public int Day { get; set; } = 0;
}