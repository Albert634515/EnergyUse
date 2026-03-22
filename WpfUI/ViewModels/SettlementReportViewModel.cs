using System.Collections.ObjectModel;
using System.Windows.Input;
using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using EnergyUse.Core.Interfaces;

namespace WpfUI.ViewModels;

public class SettlementReportViewModel : ViewModelBase
{
    private readonly SelectReportParametersController _controller;
    private readonly ISettingsService _settings;

    public ObservableCollection<DateSelectionViewModel> DateSelections { get; } = new();

    public IList<Address> AddressList { get; private set; } = new List<Address>();
    public IList<PreDefinedPeriod> PredefinedPeriods { get; private set; } = new List<PreDefinedPeriod>();
    public IList<SelectionItem> ReportTypes { get; private set; } = new List<SelectionItem>();

    private Address? _selectedAddress;
    public Address? SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            if (!SetProperty(ref _selectedAddress, value))
                return;

            if (value != null)
            {
                // Adres onthouden
                _settings.Save("LastSelectedAddress", value.Id.ToString());

                // Zoals in je oorspronkelijke code:
                setDateSelectionsForAddress(value.Id);
                setLastSelectedPeriod(value.Id);
            }

            OnPropertyChanged(nameof(IsValid));
        }
    }

    private PreDefinedPeriod? _selectedPredefinedPeriod;
    public PreDefinedPeriod? SelectedPredefinedPeriod
    {
        get => _selectedPredefinedPeriod;
        set
        {
            if (SetProperty(ref _selectedPredefinedPeriod, value) && value != null)
                applyPredefinedPeriod(value);

            OnPropertyChanged(nameof(IsValid));
        }
    }

    private SelectionItem? _selectedReportType;
    public SelectionItem? SelectedReportType
    {
        get => _selectedReportType;
        set
        {
            if (!SetProperty(ref _selectedReportType, value))
                return;

            if (value?.Description == null)
                return;

            var report = Enum.Parse<EnergyUse.Common.Enums.ReportType>(value.Description);

            switch (report)
            {
                case EnergyUse.Common.Enums.ReportType.Rates:
                    ShowRates = true;
                    ShowRatesVisible = false;
                    break;

                case EnergyUse.Common.Enums.ReportType.SettlementCompact:
                    ShowRates = false;
                    ShowRatesVisible = true;
                    break;

                default:
                    ShowRates = true;
                    ShowRatesVisible = true;
                    break;
            }

            OnPropertyChanged(nameof(ShowRates));
            OnPropertyChanged(nameof(ShowRatesVisible));
            OnPropertyChanged(nameof(IsValid));
        }
    }

    private bool _predictMissingData = true;
    public bool PredictMissingData
    {
        get => _predictMissingData;
        set { SetProperty(ref _predictMissingData, value); OnPropertyChanged(nameof(IsValid)); }
    }

    private bool _showRates = true;
    public bool ShowRates
    {
        get => _showRates;
        set { SetProperty(ref _showRates, value); OnPropertyChanged(nameof(IsValid)); }
    }

    private bool _showRatesVisible = true;
    public bool ShowRatesVisible
    {
        get => _showRatesVisible;
        set { SetProperty(ref _showRatesVisible, value); }
    }

    public bool IsValid =>
        SelectedAddress != null &&
        SelectedReportType != null &&
        DateSelections.Any() &&
        DateSelections.All(d => d.IsValid());

    public ICommand AddDateSelectionCommand { get; }
    public ICommand ClearPredefinedPeriodCommand { get; }
    public ICommand SelectCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool>? CloseRequested;

    public SettlementReportViewModel(ISettingsService settings)
    {
        _settings = settings;

        _controller = new SelectReportParametersController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        AddDateSelectionCommand = new RelayCommand(_ =>
        {
            if (SelectedAddress != null)
                addDateSelectionForAddress(SelectedAddress.Id);
        });

        ClearPredefinedPeriodCommand = new RelayCommand(_ => SelectedPredefinedPeriod = null);
        SelectCommand = new RelayCommand(_ => OnSelect(), _ => IsValid);
        CancelCommand = new RelayCommand(_ => OnCancel());
    }

    public async Task InitializeAsync(Address? currentAddress, EnergyUse.Common.Enums.ReportType defaultReport)
    {
        AddressList = (await _controller.UnitOfWork.AddressRepo.GetAll()).ToList();
        OnPropertyChanged(nameof(AddressList));

        // Eerst predefined periods laden
        PredefinedPeriods = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll().ToList();
        OnPropertyChanged(nameof(PredefinedPeriods));

        // Dan report types
        ReportTypes = EnergyUse.Core.Manager.LibSelectionItemList.GetReportTypeList();
        OnPropertyChanged(nameof(ReportTypes));

        // Laatst geselecteerde adres ophalen
        var saved = _settings.Get("LastSelectedAddress");
        Address? last = null;

        if (saved != null && long.TryParse(saved, out long id))
            last = AddressList.FirstOrDefault(a => a.Id == id);

        // Pas nu SelectedAddress zetten (roept setDateSelections + setLastSelectedPeriod aan)
        SelectedAddress = last
                        ?? currentAddress
                        ?? AddressList.FirstOrDefault(a => a.DefaultAddress == true)
                        ?? AddressList.FirstOrDefault();

        SelectedReportType = ReportTypes.FirstOrDefault(r => r.Description == defaultReport.ToString());

        OnPropertyChanged(nameof(IsValid));
    }

    private void setLastSelectedPeriod(long addressId)
    {
        var key = $"{addressId}_LastPreSelectedPeriod";
        var saved = _settings.Get(key);

        if (saved != null && long.TryParse(saved, out long periodId))
            SelectedPredefinedPeriod = PredefinedPeriods.FirstOrDefault(p => p.Id == periodId);
    }

    private void setDateSelectionsForAddress(long addressId)
    {
        DateSelections.Clear();

        if (addressId == 0)
        {
            OnPropertyChanged(nameof(IsValid));
            return;
        }

        int count = getDateSelectionCount(addressId);

        for (int i = 0; i < count; i++)
            addDateSelectionForAddress(addressId);

        OnPropertyChanged(nameof(IsValid));
    }

    private int getDateSelectionCount(long addressId)
    {
        var key = $"NumberOfEnergyTypesOnReport_A{addressId}";
        var saved = _settings.Get(key);

        if (saved != null && int.TryParse(saved, out int count))
            return count;

        var energyTypes = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
        int newCount = energyTypes.Count;

        _settings.Save(key, newCount.ToString());
        return newCount;
    }

    private void addDateSelectionForAddress(long addressId)
    {
        var vm = new DateSelectionViewModel(() => OnPropertyChanged(nameof(IsValid)))
        {
            EnergyTypeList = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList(),
            TarifGroupsList = _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList()
        };

        vm.SetTarifGroups();
        vm.SetDefaultEnergyType();

        vm.RemoveButtonVisible = DateSelections.Count > 0;
        vm.RemoveCommand = new RelayCommand(_ => removeDateSelection(vm));

        DateSelections.Add(vm);

        if (DateSelections.Count == 1)
            vm.RemoveButtonVisible = false;

        OnPropertyChanged(nameof(IsValid));
    }

    private void removeDateSelection(DateSelectionViewModel vm)
    {
        DateSelections.Remove(vm);

        if (DateSelections.Count == 1)
            DateSelections[0].RemoveButtonVisible = false;

        OnPropertyChanged(nameof(IsValid));
    }

    private void applyPredefinedPeriod(PreDefinedPeriod period)
    {
        if (SelectedAddress == null)
            return;

        var list = _controller.UnitOfWork.PreDefinedPeriodDateRepo.GetByPeriodId(period.Id).ToList();

        while (DateSelections.Count < list.Count)
            addDateSelectionForAddress(SelectedAddress.Id);

        for (int i = 0; i < list.Count; i++)
        {
            var d = list[i];
            var vm = DateSelections[i];

            vm.DateFrom = d.StartDate;
            vm.DateTill = d.EndDate;

            vm.SetEnergyType(d.EnergyType.Id);

            if (d.TariffGroup != null)
                vm.SetTarifGroup(d.TariffGroup.Id);
        }

        OnPropertyChanged(nameof(IsValid));
    }

    public ParameterSelection GetSelectedParameters()
    {
        var result = new ParameterSelection
        {
            AddressId = SelectedAddress?.Id ?? 0,
            PredictMissingData = PredictMissingData,
            ShowRates = ShowRates,
            ReportType = Enum.Parse<EnergyUse.Common.Enums.ReportType>(SelectedReportType?.Description ?? "None")
        };

        if (SelectedPredefinedPeriod != null)
        {
            result.PreSelectedPeriodId = SelectedPredefinedPeriod.Id;

            var key = $"{SelectedAddress?.Id}_LastPreSelectedPeriod";
            _settings.Save(key, SelectedPredefinedPeriod.Id.ToString());
        }

        foreach (var vm in DateSelections)
        {
            if (!vm.IsValid())
                continue;

            result.SelectedEnergyTypeList.Add(new SelectedEnergyType
            {
                EnergyType = vm.SelectedEnergyType!,
                StartRange = vm.DateFrom ?? DateTime.Now,
                EndRange = vm.DateTill ?? DateTime.Now,
                TarifGroup = vm.SelectedTariffGroup?.Id ?? 0
            });
        }

        return result;
    }

    private void OnSelect() => CloseRequested?.Invoke(true);
    private void OnCancel() => CloseRequested?.Invoke(false);
}