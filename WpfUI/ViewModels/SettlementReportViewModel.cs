using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class SettlementReportViewModel : ViewModelBase
{
    private readonly SelectReportParametersController _controller;
    private readonly ISettingsService _settings;

    private bool _isInitializing = true;

    public event Action<bool>? CloseRequested;

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
            {
                if (!_isInitializing)
                    getEnergyTypeSelections();
            }
        }
    }
    private Address _selectedAddress;

    public PreDefinedPeriod SelectedPredefinedPeriod
    {
        get => _selectedPredefinedPeriod;
        set
        {
            if (SetProperty(ref _selectedPredefinedPeriod, value))
            {
                if (!_isInitializing)
                    applyPredefinedPeriod();
            }
        }
    }
    private PreDefinedPeriod _selectedPredefinedPeriod;

    public SelectionItem SelectedReportType
    {
        get => _selectedReportType;
        set
        {
            if (SetProperty(ref _selectedReportType, value))
            {
                if (!_isInitializing)
                    applyReportTypeSettings();
            }
        }
    }
    private SelectionItem _selectedReportType;

    public bool PredictMissingData { get; set; } = true;
    public bool ShowRates { get; set; } = true;

    public ICommand AddDateSelectionCommand { get; }
    public ICommand ClearPredefinedPeriodCommand { get; }
    public ICommand SelectCommand { get; }
    public ICommand CancelCommand { get; }

    public SettlementReportViewModel(ISettingsService settings)
    {
        _settings = settings;

        _controller = new SelectReportParametersController(WpfUI.Managers.Config.GetDbFileName());
        _controller.Initialize();

        AddDateSelectionCommand = new RelayCommand(_ => AddDateSelection());
        ClearPredefinedPeriodCommand = new RelayCommand(_ => clearPredefinedPeriod());
        SelectCommand = new RelayCommand(_ => Select());
        CancelCommand = new RelayCommand(_ => Cancel());

        setInitialData();
    }

    private async void setInitialData()
    {
        foreach (var a in await _controller.UnitOfWork.AddressRepo.GetAll())
            AddressList.Add(a);

        foreach (var p in _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll())
            PredefinedPeriods.Add(p);

        foreach (var r in EnergyUse.Core.Manager.LibSelectionItemList.GetReportTypeList())
            ReportTypes.Add(r);

        initializeSettings();

        _isInitializing = false;

        if (SelectedAddress != null)
            getEnergyTypeSelections();
    }

    private void initializeSettings()
    {
        SelectedAddress = AddressList.FirstOrDefault(a => a.Id.ToString() == _settings.Get("LastSelectedAddress"));
        SelectedPredefinedPeriod = PredefinedPeriods.FirstOrDefault(p => p.Id.ToString() == _settings.Get("LastPreSelectedPeriod"));
        SelectedReportType = ReportTypes.FirstOrDefault(r => r.Id.ToString() == _settings.Get("LastSelectedReportType"));
    }

    private void getEnergyTypeSelections()
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
                TarifGroupsList = _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList(),
                RemoveButtonVisible = DateSelections.Count > 0
            };

            vm.SetTarifGroups();
            vm.SetEnergyType(et.Id);
            vm.RemoveRequested += removeDateSelection;

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
            TarifGroupsList = _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList(),
            RemoveButtonVisible = true
        };

        vm.SetTarifGroups();
        vm.RemoveRequested += removeDateSelection;

        DateSelections.Add(vm);
    }

    private void removeDateSelection(DateSelectionViewModel vm)
    {
        DateSelections.Remove(vm);
    }

    private void clearPredefinedPeriod()
    {
        SelectedPredefinedPeriod = null;
    }

    private void applyPredefinedPeriod()
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

    private void applyReportTypeSettings()
    {
        if (SelectedReportType?.Description == "Rates")
            ShowRates = true;
    }

   
    public ParameterSelection GetSelectedParameters()
    {
        var parameterSelection = new ParameterSelection();

        // Algemene instellingen
        parameterSelection.StartRange = DateTime.Now.AddMonths(-6);
        parameterSelection.EndRange = parameterSelection.StartRange.AddMonths(6);
        parameterSelection.PredictMissingData = PredictMissingData;
        parameterSelection.ShowRates = ShowRates;

        // Report type
        if (SelectedReportType != null)
        {
            if (Enum.TryParse(SelectedReportType.Description, out EnergyUse.Common.Enums.ReportType reportType))
                parameterSelection.ReportType = reportType;
        }

        // Address
        if (SelectedAddress != null)
            parameterSelection.AddressId = SelectedAddress.Id;

        // Predefined period
        if (SelectedPredefinedPeriod != null)
            parameterSelection.PreSelectedPeriodId = SelectedPredefinedPeriod.Id;

        // DateSelections (EnergyTypes)
        foreach (var ds in DateSelections)
        {
            if (ds.SelectedEnergyType == null)
                continue;

            var selectedEnergyType = new SelectedEnergyType
            {
                EnergyType = ds.SelectedEnergyType,
                StartRange = ds.DateFrom ?? DateTime.Today,
                EndRange = ds.DateTill ?? DateTime.Today,
                TarifGroup = ds.SelectedTariffGroup?.Id ?? 0
            };

            parameterSelection.SelectedEnergyTypeList.Add(selectedEnergyType);
        }

        return parameterSelection;
    }

    public event Action<ParameterSelection>? ParametersSelected;

    private void Select()
    {
        _settings.Save("LastSelectedAddress", SelectedAddress?.Id.ToString() ?? "");
        _settings.Save("LastPreSelectedPeriod", SelectedPredefinedPeriod?.Id.ToString() ?? "");
        _settings.Save("LastSelectedReportType", SelectedReportType?.Id.ToString() ?? "");

        var parameters = GetSelectedParameters();

        ParametersSelected?.Invoke(parameters);
        CloseRequested?.Invoke(true);
    }

    private void Cancel()
    {
        CloseRequested?.Invoke(false);
    }
}