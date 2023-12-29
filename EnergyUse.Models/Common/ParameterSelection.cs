namespace EnergyUse.Models.Common
{
    public class ParameterSelection
    {
        public long AddressId = 0;
        public long PreSelectedPeriodId = 0;
        public bool ShowCalculatedData = false;
        public bool PredictMissingData = false;
        public DateTime StartRange = DateTime.MinValue;
        public DateTime EndRange = DateTime.MinValue;

        public List<SelectedEnergyType> SelectedEnergyTypeList = new List<SelectedEnergyType>();
    }
}