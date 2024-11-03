namespace EnergyUse.Models.Common;

public class SettlementSubTotal
{
    public long EngergyTypeId { get; set; } = 0;
    public string SubTotalType { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal ValueBase { get; set; } = 0.0m;
    public decimal TotalVat { get; set; } = 0.0m;
    public decimal TotalValue { get; set; } = 0.0m;
}