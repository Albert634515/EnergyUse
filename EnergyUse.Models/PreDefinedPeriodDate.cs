namespace EnergyUse.Models;

public partial class PreDefinedPeriodDate
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public long? PreDefinedPeriodId { get; set; }
    public long? EnergyTypeId { get; set; }
    public long? TariffGroupId { get; set; }

    public virtual EnergyType EnergyType { get; set; }
    public virtual PreDefinedPeriod PreDefinedPeriod { get; set; }
    public virtual TariffGroup? TariffGroup { get; set; }
    
   
    //ReadOnly props

    public string EnergyTypeName
    {
        get
        {
            if (EnergyType == null)
                return string.Empty;
            else
                return EnergyType.Name;
        }
    }

    public string TarifGroupName
    {
        get
        {
            if (TariffGroup == null)
                return string.Empty;
            else
                return TariffGroup.Description;
        }
    }
}