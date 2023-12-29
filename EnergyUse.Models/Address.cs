namespace EnergyUse.Models
{
    public partial class Address
    {
        public Address() => Meters = new HashSet<Meter>();

        public long Id { get; set; }
        public string? Description { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public bool? DefaultAddress { get; set; }
        public bool SolarPanelsAvailable { get; set; }
        public long TotalCapacity { get; set; }

        public long? TariffGroupId { get; set; }

        public virtual TariffGroup? TariffGroup { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
