namespace EnergyUse.Models;

public partial class Unit
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<EnergyType> EnergyTypes { get; set; }
    public virtual ICollection<CostCategory> CostCategories { get; set; }
}