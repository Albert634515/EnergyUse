using System.ComponentModel;

namespace EnergyUse.Common.Enums
{
    public enum RateType
    {
        [Description("Unknown")]
        UnKnown = 0,
        [Description("Fixed price")]
        FixedPrice = 1,
        [Description("Staffel")]
        Staffel = 2
    }
}