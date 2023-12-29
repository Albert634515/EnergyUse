namespace EnergyUse.Models
{
    public partial class CalculatedUnitPrice
    {
        public long Id { get; set; }
        public long? EnergyTypeId { get; set; }
        public long? TariffGroupId { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }


        public virtual EnergyType? EnergyType { get; set; }
        public virtual TariffGroup? TariffGroup { get; set; }
    }
}