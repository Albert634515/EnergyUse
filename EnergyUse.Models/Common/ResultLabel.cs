using System.Drawing;

namespace EnergyUse.Models.Common;

public class ResultLabel
{
    public Color LabelForeColor { get; set; } = Color.White;
    public Color LabelBackColor { get; set; } = Color.Black;
    public string LabelText { get; set; } = "";
    public bool LabelVisibility { get; set; } = false;
    public int Left { get; set; } = 0;
}