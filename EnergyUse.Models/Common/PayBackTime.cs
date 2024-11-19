namespace EnergyUse.Models.Common;

public class PayBackTime
{
    public int PeriodId { get; set; } = 0;
    public DateTime StartPeriod { get; set; } = DateTime.MinValue;
    public DateTime EndPeriod { get; set; } = DateTime.MinValue;
    public decimal MonetaryValueProduced { get; set; } = 0.0m;
    public decimal ValueProduced { get; set; } = 0.0m;
    public decimal EstimateDirectUsed { get; set; } = 0.0m;
    public decimal ValueProducedEstimateDirectUsed { get; set; } = 0.0m;
    public decimal MonetaryValueConsumed { get; set; } = 0.0m;
    public decimal ValueConsumed { get; set; } = 0.0m;
    public decimal MonetaryValueProducedAndConsumed { get; set; } = 0.0m;

    public decimal OtherCostProduced { get; set; } = 0.0m;
    public decimal OtherCostConsumed { get; set; } = 0.0m;

    public int NettingLimit { get; set; } = 0;
    public decimal ReturnOnInvestment { get; set; } = 0.0m;
    public decimal ReturnOnInvestmentTotal { get; set; } = 0.0m;
    public decimal Return { get; set; } = 0.0m;
}