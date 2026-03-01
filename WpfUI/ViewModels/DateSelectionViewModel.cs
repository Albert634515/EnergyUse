using EnergyUse.Common.Enums;
using EnergyUse.Models;
using System.Windows.Input;
using WpfUI.ViewModels;

public class DateSelectionViewModel : ViewModelBase
{
    public event Action<DateSelectionViewModel>? RemoveRequested;

    public List<EnergyType> EnergyTypeList { get; set; } = new();
    public List<TariffGroup> TarifGroupsList { get; set; } = new();

    private EnergyType _selectedEnergyType;
    public EnergyType SelectedEnergyType
    {
        get => _selectedEnergyType;
        set => SetProperty(ref _selectedEnergyType, value);
    }

    private TariffGroup _selectedTariffGroup;
    public TariffGroup SelectedTariffGroup
    {
        get => _selectedTariffGroup;
        set => SetProperty(ref _selectedTariffGroup, value);
    }

    private DateTime? _dateFrom = DateTime.Today;
    public DateTime? DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }

    private DateTime? _dateTill = DateTime.Today;
    public DateTime? DateTill
    {
        get => _dateTill;
        set => SetProperty(ref _dateTill, value);
    }

    private bool _removeButtonVisible;
    public bool RemoveButtonVisible
    {
        get => _removeButtonVisible;
        set => SetProperty(ref _removeButtonVisible, value);
    }

    public ICommand RemoveCommand { get; }

    public DateSelectionViewModel()
    {
        RemoveCommand = new RelayCommand(_ => RemoveRequested?.Invoke(this));
    }

    public void SetTarifGroups()
    {
        TarifGroupsList = TarifGroupsList
            .Where(w => w.TypeId == (int)TariffGroupType.EnergyCosts)
            .ToList();
    }

    public void SetDefaultEnergyType()
    {
        SelectedEnergyType = EnergyTypeList.FirstOrDefault(x => x.DefaultType);
    }

    public void SetEnergyType(long id)
    {
        SelectedEnergyType = EnergyTypeList.FirstOrDefault(x => x.Id == id);
    }

    public void SetTarifGroup(long id)
    {
        SelectedTariffGroup = TarifGroupsList.FirstOrDefault(x => x.Id == id);
    }
}