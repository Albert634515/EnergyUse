namespace EnergyUse.Models.Common
{
    public class RateTaxInfo
    {
        public int RateTaxId { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal RateIncTax { get; set; } = 0;
        public bool IsTaxIncluded { get; set; } = false;
    }
}
