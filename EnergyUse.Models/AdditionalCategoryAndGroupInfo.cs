namespace EnergyUse.Models;

public partial class AdditionalCategoryAndGroupInfo
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public long? EnergyTypeId { get; set; }
    public long? CostCategoryId { get; set; }
    public long? TariffGroupId { get; set; }


    public virtual CostCategory? CostCategory { get; set; }
    public virtual EnergyType? EnergyType { get; set; }
    public virtual TariffGroup? TariffGroup { get; set; }
}