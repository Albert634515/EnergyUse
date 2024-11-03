namespace EnergyUse.Models.Common;

public class OtherCost
{
    public long RateId { get; set; }
    public long VatRateId { get; set; }
    public decimal VatTarif { get; set; }
    public long CostCategoryId { get; set; }
    public bool LastAvailableRateUsed { get; set; }
    public bool LastAvailableVatRateUsed { get; set; }
    public decimal Rate { get; set; }
    public decimal CorrectionFactor { get; set; }
    public decimal MaxStaffel { get; set; }
}