namespace EnergyUse.Models.Common;

public class PeriodStaffel
{
    public long RateId { get; set; } = 0;
    /// <summary>
    /// Start period of staffel
    /// </summary>
    public DateTime StartRange { get; set; } = DateTime.MinValue;
    /// <summary>
    /// End period of staffel
    /// </summary>
    public DateTime EndRange { get; set; } = DateTime.MinValue;
    /// <summary>
    /// Minimum level of the staffel
    /// </summary>
    public decimal MinLevel { get; set; } = 0;
    /// <summary>
    /// Maximum level of the staffel
    /// </summary>
    public decimal MaxLevel { get; set; } = 0;
    /// <summary>
    /// Value used for this level of the staffel
    /// </summary>
    public decimal Value { get; set; } = 0;
    public long EnergySubTypeId { get; set; } = 0;
}
