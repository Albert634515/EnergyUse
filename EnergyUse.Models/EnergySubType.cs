namespace EnergyUse.Models;

public partial class EnergySubType
{
    public EnergySubType()
    {
        CostCategories = new HashSet<CostCategory>();
    }

    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<CostCategory> CostCategories { get; set; }
}