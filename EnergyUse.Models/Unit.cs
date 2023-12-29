namespace EnergyUse.Models
{
    public partial class Unit
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EnergyType> EnergyTypes { get; set; }
        public virtual ICollection<CostCategory> CostCategories { get; set; }
    }
}
