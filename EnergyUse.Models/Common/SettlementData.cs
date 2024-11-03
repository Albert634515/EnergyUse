namespace EnergyUse.Models.Common;

public class SettlementData
{
    /// <summary>
    /// Costcategory description
    /// </summary>
    public string Description { get; set; } = "";
    public CostCategory CostCategory { get; set; } = new();
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime EndDate { get; set; } = DateTime.MinValue;
    public decimal ValueBaseConsumed { get; set; } = 0.0m;
    public decimal ValueBaseProduced { get; set; } = 0.0m;
    public decimal ValueBase
    {
        get
        {
            return (ValueBaseConsumed + ValueBaseProduced);
        }
    }
    public decimal CorrectionFactor { get; set; } = 0.0m;
    public decimal Rate { get; set; } = 0.0m;
    public bool LastAvailableRateUsed { get; set; } = false;
    public decimal Value { get; set; } = 0.0m;
    public decimal VatTarif { get; set; } = 0.0m;
    public decimal VatAmount
    {
        get
        {
            return Value * (VatTarif / 100);
        }
    }
    public bool LastAvailableVatRateUsed { get; set; } = false;        
    public bool DataPredicted { get; set; } = false;
    public decimal PriceIncrease { get; set; } = 0;
    public Staffel? Staffel { get; set; } = null;
    public decimal MaxStaffelRange {  get; set; } = 0;
}