using EnergyUse.Common.Extensions;

namespace EnergyUse.Models
{
    public partial class MeterReading
    {
        public long? Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int WeekNo { get; set; }
        public decimal RateNormal { get; set; }
        public decimal DeltaNormal { get; set; }
        public decimal RateLow { get; set; }
        public decimal DeltaLow { get; set; }
        public decimal ReturnDeliveryLow { get; set; }
        public decimal ReturnDeliveryDeltaLow { get; set; }
        public decimal ReturnDeliveryNormal { get; set; }
        public decimal ReturnDeliveryDeltaNormal { get; set; }

        public long? MeterId { get; set; }
        public long? EnergyTypeId { get; set; }

        public virtual EnergyType EnergyType { get; set; }

        public virtual Meter Meter { get; set; }

        public int Week => RegistrationDate.GetWeekNumber();
    }
}