using EnergyUse.Common.Enums;
using EnergyUse.Models;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class DateSelectionViewModel : ViewModelBase
{
    private readonly Action? _onChangedCallback;

    public DateSelectionViewModel(Action? onChangedCallback = null)
    {
        _onChangedCallback = onChangedCallback;
        RemoveButtonVisible = false;
    }

    private void NotifyChanged()
    {
        _onChangedCallback?.Invoke();
    }

    private IList<EnergyType> _energyTypeList = new List<EnergyType>();
    public IList<EnergyType> EnergyTypeList
    {
        get => _energyTypeList;
        set { SetProperty(ref _energyTypeList, value); NotifyChanged(); }
    }

    private IList<TariffGroup> _tarifGroupsList = new List<TariffGroup>();
    public IList<TariffGroup> TarifGroupsList
    {
        get => _tarifGroupsList;
        set { SetProperty(ref _tarifGroupsList, value); NotifyChanged(); }
    }

    private EnergyType? _selectedEnergyType;
    public EnergyType? SelectedEnergyType
    {
        get => _selectedEnergyType;
        set { SetProperty(ref _selectedEnergyType, value); NotifyChanged(); }
    }

    private TariffGroup? _selectedTariffGroup;
    public TariffGroup? SelectedTariffGroup
    {
        get => _selectedTariffGroup;
        set { SetProperty(ref _selectedTariffGroup, value); NotifyChanged(); }
    }

    private DateTime? _dateFrom;
    public DateTime? DateFrom
    {
        get => _dateFrom;
        set { SetProperty(ref _dateFrom, value); NotifyChanged(); }
    }

    private DateTime? _dateTill;
    public DateTime? DateTill
    {
        get => _dateTill;
        set { SetProperty(ref _dateTill, value); NotifyChanged(); }
    }

    private bool _removeButtonVisible;
    public bool RemoveButtonVisible
    {
        get => _removeButtonVisible;
        set { SetProperty(ref _removeButtonVisible, value); NotifyChanged(); }
    }

    public ICommand? RemoveCommand { get; set; }

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

    public bool IsValid()
    {
        if (SelectedEnergyType == null)
            return false;

        if (DateFrom == null || DateTill == null)
            return false;

        if (DateFrom > DateTill)
            return false;

        return true;
    }
}