namespace EnergyUse.Models.Common;

public class ParameterCalcPeriod
{
    public int PeriodId { get; set; } = 0;
    public DateTime PeriodStart { get; set; } = DateTime.MinValue;
    public Models.Address Address { get; set; } = new Models.Address();
    public Models.EnergyType EnergyType { get; set; } = new Models.EnergyType();
    public decimal InitialInvestment { get; set; } = 0;
    public decimal QualityReductionSolarPanels { get; set; } = 0;
    public decimal TotalCapacitySolarPanels { get; set; } = 0;
    public decimal AverageReturn { get; set; } = 0;
}