namespace EnergyUse.Models
{
    public class AvgMeterRate
    {
        public EnergyType EnergyType { get; set; } = new();
        public long AddressId { get; set; } = 0;
        public int Month { get; set; } = 0;
        public int Day { get; set; } = 0;
        public decimal AvgLow { get; set; } = 0;
        public decimal AvgNormal { get; set; } = 0;
        public decimal AvgReturnDeliveryLow { get; set; } = 0;
        public decimal AvgReturnDeliveryNormal { get; set; } = 0;        
    }
}
