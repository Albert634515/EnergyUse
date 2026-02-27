using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class SettlementReportViewModel : ViewModelBase
{
    private readonly SelectReportParametersController _controller;

    public ObservableCollection<Address> AddressList { get; } = new();
    public ObservableCollection<PreDefinedPeriod> PredefinedPeriods { get; } = new();
    public ObservableCollection<SelectionItem> ReportTypes { get; } = new();
    public ObservableCollection<DateSelectionViewModel> DateSelections { get; } = new();

    public Address SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            if (SetProperty(ref _selectedAddress, value))
                setEnergyTypeSelections();
        }
    }
    private Address _selectedAddress;

    public PreDefinedPeriod SelectedPredefinedPeriod
    {
        get => _selectedPredefinedPeriod;
        set
        {
            if (SetProperty(ref _selectedPredefinedPeriod, value))
                ApplyPredefinedPeriod();
        }
    }
    private PreDefinedPeriod _selectedPredefinedPeriod;

    public SelectionItem SelectedReportType
    {
        get => _selectedReportType;
        set
        {
            if (SetProperty(ref _selectedReportType, value))
                ApplyReportTypeSettings();
        }
    }
    private SelectionItem _selectedReportType;

    public bool PredictMissingData { get; set; } = true;
    public bool ShowRates { get; set; } = true;

    public ICommand AddDateSelectionCommand { get; }
    public ICommand SelectCommand { get; }
    public ICommand CancelCommand { get; }

    public SettlementReportViewModel()
    {
        _controller = new SelectReportParametersController(WpfUI.Managers.Config.GetDbFileName());
        _controller.Initialize();

        AddDateSelectionCommand = new RelayCommand(_ => AddDateSelection());
        SelectCommand = new RelayCommand(_ => Select());
        CancelCommand = new RelayCommand(_ => Cancel());

        setInitialData();
    }

    private async void setInitialData()
    {
        var addresses = await _controller.UnitOfWork.AddressRepo.GetAll();
        foreach (var a in addresses)
            AddressList.Add(a);

        foreach (var p in _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll())
            PredefinedPeriods.Add(p);

        foreach (var r in EnergyUse.Core.Manager.LibSelectionItemList.GetReportTypeList())
            ReportTypes.Add(r);
    }

    private void setEnergyTypeSelections()
    {
        DateSelections.Clear();

        if (SelectedAddress == null)
            return;

        var energyTypes = _controller.UnitOfWork.EnergyTypeRepo
            .SelectByAddressId(SelectedAddress.Id)
            .ToList();

        foreach (var et in energyTypes)
        {
            var vm = new DateSelectionViewModel
            {
                EnergyTypeList = energyTypes,
                TarifGroupsList = _controller.UnitOfWork.TarifGroupRepo.GetAll().ToList()
            };

            vm.SetTarifGroups();
            vm.SetEnergyType(et.Id);
            vm.RemoveButtonVisible = DateSelections.Count > 0;

            DateSelections.Add(vm);
        }
    }

    private void AddDateSelection()
    {
        if (SelectedAddress == null)
            return;

        var vm = new DateSelectionViewModel
        {
            EnergyTypeList = _controller.UnitOfWork.EnergyTypeRepo
                .SelectByAddressId(SelectedAddress.Id).ToList(),
            TarifGroupsList = _controller.UnitOfWork.TarifGroupRepo.GetAll().ToList(),
            RemoveButtonVisible = true
        };

        vm.SetTarifGroups();
        DateSelections.Add(vm);
    }

    private void ApplyPredefinedPeriod()
    {
        if (SelectedPredefinedPeriod == null)
            return;

        var dates = _controller.UnitOfWork.PreDefinedPeriodDateRepo
            .GetByPeriodId(SelectedPredefinedPeriod.Id)
            .ToList();

        for (int i = 0; i < dates.Count && i < DateSelections.Count; i++)
        {
            var vm = DateSelections[i];
            vm.DateFrom = dates[i].StartDate;
            vm.DateTill = dates[i].EndDate;
            vm.SetEnergyType(dates[i].EnergyType.Id);
            vm.SetTarifGroup(dates[i].TariffGroup?.Id ?? 0);
        }
    }

    private void ApplyReportTypeSettings()
    {
        if (SelectedReportType == null)
            return;

        if (SelectedReportType.Description == "Rates")
        {
            ShowRates = true;
        }
    }

    private void Select()
    {
        // Validatie + sluiten via event of callback
    }

    private void Cancel()
    {
        // Sluiten via event of callback
    }
}