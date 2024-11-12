namespace EnergyUse.Models.Common;

public class ParameterSelection
{
    public long AddressId { get; set; } = 0;
    public long PreSelectedPeriodId { get; set; } = 0;
    public bool PredictMissingData { get; set; } = false;
    public bool ShowRates { get; set; } = false;
    public DateTime StartRange { get; set; } = DateTime.MinValue;
    public DateTime EndRange { get; set; } = DateTime.MinValue;
    public EnergyUse.Common.Enums.ReportType ReportType { get; set; } = EnergyUse.Common.Enums.ReportType.None;

    public List<SelectedEnergyType> SelectedEnergyTypeList { get; set; } = new List<SelectedEnergyType>();
}