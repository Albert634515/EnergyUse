namespace EnergyUse.Models.Common;

public class PriceRate
{
    public decimal Rate { get; set; }
    public decimal PriceAdjustmentFactor { get; set; }
    public long RateId { get; set; }
    public long RateTypeId { get; set; }
    public bool LastRateUsed { get; set; }
    public DateTime StartRate { get; set; } = DateTime.MinValue;
    public DateTime EndRate { get; set; } = DateTime.MinValue;
}
