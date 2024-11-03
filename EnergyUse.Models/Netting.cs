namespace EnergyUse.Models;

public partial class Netting
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Rate { get; set; }
    public long? EnergyTypeId { get; set; }

    public virtual EnergyType EnergyType { get; set; }

    // Readonly props
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
}