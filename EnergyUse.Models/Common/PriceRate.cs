namespace EnergyUse.Models.Common;

public class PriceRate
{
    public decimal Rate { get; set; }
    public decimal Increase { get; set; }
    public long RateId { get; set; }
    public long RateTypeId { get; set; }
    public bool LastRateUsed { get; set; }
}