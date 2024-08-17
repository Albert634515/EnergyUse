namespace EnergyUse.Models
{
    public partial class Rate
    {
        public long Id { get; set; }
        public DateTime StartRate { get; set; }
        public DateTime EndRate { get; set; }
        public decimal RateValue { get; set; } = 0;
        public decimal ExpectedPriceChange { get; set; } = 0;
        public decimal PriceChange { get; set; } = 0;
        public string? Description { get; set; } = "";
        public int RateTypeId { get; set; }

        public long? CostCategoryId { get; set; }
        public long? EnergyTypeId { get; set; }
        public long? TariffGroupId { get; set; }

        public virtual CostCategory CostCategory { get; set; }
        public virtual EnergyType EnergyType { get; set; }
        public virtual TariffGroup TariffGroup { get; set; }

        public virtual ICollection<Staffel> Staffels { get; set; }

        // Read only props
        public string EnergyTypeDescription => EnergyType.Name;
        public string TarifGroupDescription => TariffGroup.Description;
        public string CostCategoryDescription => CostCategory.Name;
        public decimal FutureRate => ExpectedPriceChange == 0 ? RateValue : (1 + (ExpectedPriceChange /  100)) * RateValue;
    }
}