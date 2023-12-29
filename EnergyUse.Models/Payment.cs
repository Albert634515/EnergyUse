namespace EnergyUse.Models
{
    public partial class Payment
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Amount { get; set; }
        public long AddressId { get; set; }
        public long? PreDefinedPeriodId { get; set; }

        public virtual Address Address { get; set; }
        public virtual PreDefinedPeriod PreDefinedPeriod { get; set; }
    }
}
