namespace EnergyUse.Models
{
    public partial class Staffel
    {
        public long Id { get; set; }
        public long RateId { get; set; }
        public decimal ValueFrom { get; set; }
        public decimal ValueTill { get; set; }
        public decimal StaffelValue { get; set; }

        public virtual Rate Rate { get; set; }
    }
}