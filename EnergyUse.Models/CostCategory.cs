namespace EnergyUse.Models;

public partial class CostCategory
{
    public CostCategory()
    {
        AdditionalCategoryAndGroupInfos = new HashSet<AdditionalCategoryAndGroupInfo>();
        CostTypes = new HashSet<CostType>();
        Rates = new HashSet<Rate>();
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool CalculateVat { get; set; }
    public bool CanNotBeNegative { get; set; }
    public bool NotCalculateOverReturn { get; set; }

    public long? CalculationTypeId { get; set; }
    public long? EnergyTypeId { get; set; }
    public long? EnergySubTypeId { get; set; }
    public string? UnitId { get; set; }
    public long? TariffGroupId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public virtual Unit Unit { get; set; }
    public virtual CalculationType CalculationType { get; set; }
    public virtual EnergySubType EnergySubType { get; set; }
    public virtual EnergyType EnergyType { get; set; }
    public virtual TariffGroup? TariffGroup { get; set; }
    public virtual ICollection<CostType> CostTypes { get; set; }
    public virtual ICollection<Rate> Rates { get; set; }
    public virtual ICollection<VatTarif> VatTarifs { get; set; }
    public virtual ICollection<AdditionalCategoryAndGroupInfo>? AdditionalCategoryAndGroupInfos { get; set; }

    // Readonly props
    public string UnitName
    {
        get
        {
            if (Unit == null)
                return string.Empty;
            else
                return Unit.Description;
        }
    }
}