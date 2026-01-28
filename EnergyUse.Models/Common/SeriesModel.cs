using System.Drawing;

namespace EnergyUse.Models.Common
{
    public class SeriesModel
    {
        public string Name { get; set; } = string.Empty;
        public long EnergyTypeId { get; set; }
        public string SeriesKey { get; set; } = string.Empty;
        public List<DatePoint> Points { get; set; } = new();
        public Color Color { get; set; } = Color.Empty;        
        public int ScalesYAt { get; set; } = 0;
        public bool IsStacked { get; set; } = false;
        public bool IsLine { get; set; } = false;
    }
}