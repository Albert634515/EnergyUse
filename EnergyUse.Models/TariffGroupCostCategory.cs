namespace EnergyUse.Models;

public class TariffGroupCostCategory
{
    public long TariffGroupId { get; set; }
    public long CostCategoryId { get; set; }
    public TariffGroup TariffGroup { get; set; } = null!;
    public CostCategory CostCategory { get; set; } = null!;
}