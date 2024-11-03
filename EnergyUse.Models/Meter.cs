namespace EnergyUse.Models;

public partial class Meter
{
    public Meter()
    {
        MeterReadings = new HashSet<MeterReading>();
    }

    public long Id { get; set; }
    public string Description { get; set; } = "";
    public string Number { get; set; } = "";
    public long? EnergyTypeId { get; set; }
    public long? AddressId { get; set; }
    public bool Active { get; set; }
    public DateTime ActiveFrom { get; set; }

    public virtual Address Address { get; set; }
    public virtual EnergyType EnergyType { get; set; }
    public virtual ICollection<MeterReading> MeterReadings { get; set; }


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
}