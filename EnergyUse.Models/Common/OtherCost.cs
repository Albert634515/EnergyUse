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
    public decimal PriceAdjustmentFactor { get; set; }
    public decimal CorrectionFactor { get; set; }
    public decimal MaxStaffel { get; set; }
    /// <summary>
    /// Exact quantity priced by this cost record when a usage period crosses a staffel boundary.
    /// </summary>
    public decimal? Quantity { get; set; }
}
