namespace EnergyUse.Models.Common;

public class AxisModel
{
    public string Name { get; set; } = string.Empty;
    public int Position { get; set; }
    public double TextSize { get; set; }
    public int PaddingLeft { get; set; }
    public int PaddingTop { get; set; }
    public bool ShowSeparatorLines { get; set; }
}