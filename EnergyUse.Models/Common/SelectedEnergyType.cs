namespace EnergyUse.Models.Common
{
    public class SelectedEnergyType
    {
        public Models.EnergyType EnergyType;
        public long TarifGroup = 0;
        public DateTime StartRange = DateTime.MinValue;
        public DateTime EndRange = DateTime.MinValue;
    }
}