using EnergyUse.Models;

namespace WpfUI.ViewModels;

public class SelectableEnergyTypeViewModel : ViewModelBase
{
    public EnergyType EnergyType { get; }

    public SelectableEnergyTypeViewModel(EnergyType energyType)
    {
        EnergyType = energyType;
    }

    public string Name => EnergyType.Name;

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }
}