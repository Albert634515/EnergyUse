using EnergyUse.Common.Enums;

namespace EnergyUse.Models.Common
{
    public class PeriodicData
    {
        public Period PeriodType { get; set; } = Period.Unknown;

        public int ValueX { get; set; } = 0;
        public DateTime ValueXDate { get; set; } = DateTime.MinValue;
        public int ValueXWeek { get; set; } = 0;
        public string ValueXString { get; set; } = "";

        public decimal ValueY { get; set; } = 0;
        public double? ValueY2 { get; set; } = 0;
        public decimal ValueYLow { get; set; } = 0;
        public decimal ValueYNormal { get; set; } = 0;
        public decimal ValueYReturnLow { get; set; } = 0;
        public decimal ValueYReturnNormal { get; set; } = 0;

        public decimal NettingValueYReturnLow { get; set; } = 0;
        public decimal NettingValueYReturnNormal { get; set; } = 0;

        public decimal CorrectionFactor { get; set; } = 0;

        public decimal RateLow { get; set; } = 0;
        public decimal RateNormal { get; set; } = 0;
        public decimal RateReturnLow { get; set; } = 0;
        public decimal RateReturnNormal { get; set; } = 0;

        public decimal ValueMonetaryY { get; set; } = 0;
        public decimal ValueYMonetaryLow { get; set; } = 0;
        public decimal ValueYMonetaryNormal { get; set; } = 0;
        public decimal ValueYMonetaryReturnLow { get; set; } = 0;
        public decimal ValueYMonetaryReturnNormal { get; set; } = 0;

        public bool IsPredicted { get; set; } = false;
    }
}