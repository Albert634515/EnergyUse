namespace EnergyUse.Models.Common
{
    public class PeriodicDataPerDay
    {
        public DateTime ValueX { get; set; } = DateTime.MinValue;
        public int ValueWeek { get; set; } = 0;
        public bool IsPredicted { get; set; } = false;

        // Values of used energy
        public decimal ValueY { get; set; } = 0.0m;
        public decimal ValueYLow { get; set; } = 0.0m;
        public decimal ValueYNormal { get; set; } = 0.0m;
        public decimal ValueYReturnLow { get; set; } = 0.0m;
        public decimal ValueYReturnNormal { get; set; } = 0.0m;
        public decimal CorrectionFactor { get; set; } = 0.0m;

        // Values by rate
        public decimal RateLow { get; set; } = 0;
        public decimal RateNormal { get; set; } = 0;
        public decimal RateReturnLow { get; set; } = 0;
        public decimal RateReturnNormal { get; set; } = 0;

        public decimal ValueMonetaryY { get; set; } = 0;
        public decimal ValueYMonetaryLow { get; set; } = 0;
        public decimal ValueYMonetaryNormal { get; set; } = 0;
        public decimal ValueYMonetaryReturnLow { get; set; } = 0;
        public decimal ValueYMonetaryReturnNormal { get; set; } = 0;

        //Other values
        public decimal NettingPercentage { get; set; } = 0;
        public decimal NettingValueYReturnLow { get; set; } = 0;
        public decimal NettingValueYReturnNormal { get; set; } = 0;

    }
}