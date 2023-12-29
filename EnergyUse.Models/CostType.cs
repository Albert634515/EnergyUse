namespace EnergyUse.Models
{
    public partial class CostType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CostCategoryId { get; set; }
        public virtual CostCategory CostCategory { get; set; }
    }
}