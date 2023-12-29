namespace EnergyUse.Models
{
    public partial class PreDefinedPeriod
    {
        public PreDefinedPeriod()
        {
            PreDefinedPeriodDates = new HashSet<PreDefinedPeriodDate>();
        }

        public long Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PreDefinedPeriodDate> PreDefinedPeriodDates { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}