namespace EnergyUse.Models;

public partial class PreDefinedPeriodDate : System.ComponentModel.INotifyPropertyChanged
{
    public long Id { get; set; }

    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        set => setProperty(ref _startDate, value);
    }

    private DateTime _endDate;
    public DateTime EndDate
    {
        get => _endDate;
        set => setProperty(ref _endDate, value);
    }

    public long? PreDefinedPeriodId { get; set; }
    public long? EnergyTypeId { get; set; }
    public long? TariffGroupId { get; set; }

    public virtual EnergyType? EnergyType { get; set; }
    public virtual PreDefinedPeriod? PreDefinedPeriod { get; set; }
    public virtual TariffGroup? TariffGroup { get; set; }
    
   
    //ReadOnly props

    public string EnergyTypeName
    {
        get
        {
            if (EnergyType == null)
                return string.Empty;
            else
                return EnergyType.Name;
        }
    }

    public string TarifGroupName
    {
        get
        {
            if (TariffGroup == null)
                return string.Empty;
            else
                return TariffGroup.Description;
        }
    }

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    private void setProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return;

        field = value;
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
