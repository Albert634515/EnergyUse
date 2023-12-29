namespace EnergyUse.Models
{
    public partial class VatTarif
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Tarif { get; set; }

        public long? CostCategoryId { get; set; }
        public virtual CostCategory? CostCategory { get; set; }
    }
}
