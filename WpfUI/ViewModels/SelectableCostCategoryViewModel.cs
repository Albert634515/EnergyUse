using EnergyUse.Models;

namespace WpfUI.ViewModels;

public class SelectableCostCategoryViewModel : ViewModelBase
{
    private readonly Action<bool> _changed;
    public CostCategory Category { get; }
    public string Name { get; }
    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            _changed(value);
            OnPropertyChanged();
        }
    }

    public SelectableCostCategoryViewModel(CostCategory category, string name, bool selected, Action<bool> changed)
    {
        Category = category;
        Name = name;
        _isSelected = selected;
        _changed = changed;
    }
}