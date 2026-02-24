using EnergyUse.Common.Enums;
using System.Windows.Input;
using WpfUI.ViewModels;
using EnergyUse.Models;

public class DateSelectionViewModel : ViewModelBase
{
    private List<EnergyType> _energyTypeList;
    public List<EnergyType> EnergyTypeList
    {
        get => _energyTypeList;
        set => SetProperty(ref _energyTypeList, value);
    }

    private List<TariffGroup> _tarifGroupsList;
    public List<TariffGroup> TarifGroupsList
    {
        get => _tarifGroupsList;
        set => SetProperty(ref _tarifGroupsList, value);
    }

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
        RemoveCommand = new RelayCommand(_ => OnRemove());
    }

    private void OnRemove()
    {
        // Eventueel callback naar parent
    }

    public void SetTarifGroups()
    {
        if (TarifGroupsList == null)
            TarifGroupsList = new List<TariffGroup>();

        TarifGroupsList = TarifGroupsList
            .Where(w => w.TypeId == (int)TariffGroupType.EnergyCosts)
            .ToList();

        SelectedTariffGroup = null;
    }

    public void SetEnergyTypes()
    {
        if (EnergyTypeList == null)
            EnergyTypeList = new List<EnergyType>();

        SelectedEnergyType = null;
    }

    public void SetDefaultEnergyType()
    {
        SelectedEnergyType = EnergyTypeList?
            .FirstOrDefault(x => x.DefaultType == true);
    }

    public void SetEnergyType(long id)
    {
        SelectedEnergyType = EnergyTypeList?
            .FirstOrDefault(x => x.Id == id);
    }

    public void SetTarifGroup(long id)
    {
        SelectedTariffGroup = TarifGroupsList?
            .FirstOrDefault(x => x.Id == id &&
                                 x.TypeId == (int)TariffGroupType.EnergyCosts);
    }
}