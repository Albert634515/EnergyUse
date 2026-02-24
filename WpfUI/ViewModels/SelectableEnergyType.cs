using System.ComponentModel;
using System.Runtime.CompilerServices;
using EnergyUse.Models;

namespace WpfUI.ViewModels;

public class SelectableEnergyType : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public EnergyType EnergyType { get; }

    public SelectableEnergyType(EnergyType energyType)
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