namespace EnergyUse.Models
{
    public partial class TariffGroup
    {
        public TariffGroup()
        {
            AdditionalCategoryAndGroupInfos = new HashSet<AdditionalCategoryAndGroupInfo>();
            Addresses = new HashSet<Address>();
            CostCategories = new HashSet<CostCategory>();
            PreDefinedPeriodDates = new HashSet<PreDefinedPeriodDate>();
            Rates = new HashSet<Rate>();
        }

        public long Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AdditionalCategoryAndGroupInfo> AdditionalCategoryAndGroupInfos { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CostCategory> CostCategories { get; set; }
        public virtual ICollection<PreDefinedPeriodDate> PreDefinedPeriodDates { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<CalculatedUnitPrice> CalculatedUnitPrices { get; set; }
    }
}