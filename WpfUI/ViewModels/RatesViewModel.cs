using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Input;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class RatesViewModel : ViewModelBase
{
    private readonly RateController _controller;
    private readonly Window _window;
    private readonly SelectionItemService _selectionService;
    private readonly ILanguageService _languageService;

    public ObservableCollection<EnergyType> EnergyTypes { get; } = new();
    public ObservableCollection<CostCategory> CostCategories { get; } = new();
    public ObservableCollection<TariffGroup> TariffGroups { get; } = new();
    public ObservableCollection<Rate> Rates { get; } = new();
    public ObservableCollection<EnergyUse.Models.Common.SelectionItem> RateTypes { get; } = new();

    private EnergyType _selectedEnergyType;
    public EnergyType SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            if (_selectedEnergyType != value)
            {
                _selectedEnergyType = value;
                OnPropertyChanged(nameof(SelectedEnergyType));
                _ = OnEnergyTypeChangedAsync();
            }
        }
    }

    private CostCategory _selectedCostCategory;
    public CostCategory SelectedCostCategory
    {
        get => _selectedCostCategory;
        set
        {
            if (_selectedCostCategory != value)
            {
                _selectedCostCategory = value;
                OnPropertyChanged(nameof(SelectedCostCategory));
                _ = OnCostCategoryChangedAsync();
            }
        }
    }

    private TariffGroup _selectedTariffGroup;
    public TariffGroup SelectedTariffGroup
    {
        get => _selectedTariffGroup;
        set
        {
            if (_selectedTariffGroup != value)
            {
                _selectedTariffGroup = value;
                OnPropertyChanged(nameof(SelectedTariffGroup));
                _ = InitAdditionalCategoryAndGroupInfoAsync();
                InitRates();
            }
        }
    }

    private Rate _selectedRate;
    public Rate SelectedRate
    {
        get => _selectedRate;
        set
        {
            if (_selectedRate != value)
            {
                _selectedRate = value;
                OnPropertyChanged(nameof(SelectedRate));
                _ = SetRateIncExLabelAsync();
            }
        }
    }

    private EnergyUse.Models.Common.SelectionItem _selectedRateType;
    public EnergyUse.Models.Common.SelectionItem SelectedRateType
    {
        get => _selectedRateType;
        set
        {
            if (_selectedRateType != value)
            {
                _selectedRateType = value;
                OnPropertyChanged(nameof(SelectedRateType));
                ChangeRateType();
                OnPropertyChanged(nameof(IsStaffel));
            }
        }
    }

    private string _rateTaxInfoText = "_";
    public string RateTaxInfoText
    {
        get => _rateTaxInfoText;
        set { _rateTaxInfoText = value; OnPropertyChanged(nameof(RateTaxInfoText)); }
    }

    private string _alwaysCalculatedWithText;
    public string AlwaysCalculatedWithText
    {
        get => _alwaysCalculatedWithText;
        set { _alwaysCalculatedWithText = value; OnPropertyChanged(nameof(AlwaysCalculatedWithText)); }
    }

    private bool _isAlwaysCalculatedWithVisible;
    public bool IsAlwaysCalculatedWithVisible
    {
        get => _isAlwaysCalculatedWithVisible;
        set { _isAlwaysCalculatedWithVisible = value; OnPropertyChanged(nameof(IsAlwaysCalculatedWithVisible)); }
    }

    public bool IsStaffel =>
        SelectedRateType != null && (RateType)SelectedRateType.Id == RateType.Staffel;

    public ICommand AddRateCommand { get; }
    public ICommand SaveRateCommand { get; }
    public ICommand CancelRateCommand { get; }
    public ICommand DeleteRateCommand { get; }
    public ICommand RefreshRatesCommand { get; }
    public ICommand CloseCommand { get; }

    public RatesViewModel(Window window)
    {
        _window = window;
        _controller = new RateController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        _languageService = new LanguageService();
        _selectionService = new SelectionItemService(_languageService);

        AddRateCommand = new RelayCommand(_ => AddRate(), _ => CanModify());
        SaveRateCommand = new RelayCommand(_ => SaveRate(), _ => true);
        CancelRateCommand = new RelayCommand(_ => CancelRate(), _ => true);
        DeleteRateCommand = new RelayCommand(_ => DeleteRate(), _ => SelectedRate != null);
        RefreshRatesCommand = new RelayCommand(_ => RefreshRates(), _ => true);
        CloseCommand = new RelayCommand(_ => Close(), _ => true);

        LoadEnergyTypes();
        LoadRateTypes();
    }

    private async void LoadEnergyTypes()
    {
        EnergyTypes.Clear();
        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        foreach (var e in list)
            EnergyTypes.Add(e);
    }

    private void LoadCostCategories(long energyTypeId)
    {
        CostCategories.Clear();
        var list = _controller.UnitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyTypeId).ToList();
        foreach (var c in list)
            CostCategories.Add(c);
    }

    private void LoadTariffGroups(CostCategory costCategory)
    {
        TariffGroups.Clear();
        var list = _controller.UnitOfWork.TarifGroupRepo.GetAll().ToList();
        if (costCategory?.TariffGroup != null && costCategory.TariffGroup.Id > 0)
            list = list.Where(t => t.Id == costCategory.TariffGroup.Id).ToList();

        foreach (var t in list)
            TariffGroups.Add(t);

        SetLabelAlwaysCalculatedWith(costCategory?.TariffGroup);
    }

    private void LoadRateTypes()
    {
        RateTypes.Clear();
        var list = _selectionService.GetRateTypeList();
        foreach (var r in list)
            RateTypes.Add(r);
    }

    private async Task OnEnergyTypeChangedAsync()
    {
        if (SelectedEnergyType != null)
            LoadCostCategories(SelectedEnergyType.Id);
        else
            CostCategories.Clear();

        await InitAdditionalCategoryAndGroupInfoAsync();
        InitRates();
    }

    private async Task OnCostCategoryChangedAsync()
    {
        if (SelectedCostCategory != null && SelectedEnergyType != null)
        {
            LoadTariffGroups(SelectedCostCategory);
            await InitAdditionalCategoryAndGroupInfoAsync();
            InitRates();
        }
    }

    private void InitRates()
    {
        Rates.Clear();

        var costCategory = SelectedCostCategory;
        var energyType = SelectedEnergyType;
        var tarifGroup = SelectedTariffGroup;

        _controller.UnitOfWork.RateList = new System.Collections.Generic.List<Rate>();

        if (costCategory != null && costCategory.TariffGroup != null && costCategory.TariffGroup.Id > 0)
            _controller.UnitOfWork.RateList =
                _controller.UnitOfWork.RateRepo
                    .SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, costCategory.TariffGroup.Id)
                    .ToList();
        else if (costCategory != null && tarifGroup != null && tarifGroup.Id > 0)
            _controller.UnitOfWork.RateList =
                _controller.UnitOfWork.RateRepo
                    .SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroup.Id)
                    .ToList();

        _controller.UnitOfWork.SetListSorted();

        foreach (var r in _controller.UnitOfWork.RateList)
            Rates.Add(r);

        SelectedRate = Rates.FirstOrDefault();
    }

    private async Task InitAdditionalCategoryAndGroupInfoAsync()
    {
        var costCategory = SelectedCostCategory;
        var energyType = SelectedEnergyType;
        var tarifGroup = SelectedTariffGroup;

        if (energyType != null && energyType.Id > 0
            && costCategory != null && costCategory.Id > 0
            && tarifGroup != null && tarifGroup.Id > 0)
        {
            var entity = await _controller.UnitOfWork.AdditionalCategoryAndGroupInfoRepo
                .SelectByPrimaryKey(energyType.Id, costCategory.Id, tarifGroup.Id);

            if (entity == null)
            {
                entity = new AdditionalCategoryAndGroupInfo
                {
                    EnergyType = energyType,
                    CostCategory = costCategory,
                    TariffGroup = tarifGroup
                };
                _controller.UnitOfWork.AdditionalCategoryAndGroupInfoRepo.Add(entity);
            }
        }
    }

    private async Task SetRateIncExLabelAsync()
    {
        RateTaxInfoText = "_";

        if (SelectedRate == null || SelectedCostCategory == null)
            return;

        var rateTaxInfo = await _controller.GetRateIncExTax(SelectedCostCategory, SelectedRate);
        if (rateTaxInfo != null && SelectedCostCategory.CalculateVat)
            RateTaxInfoText = rateTaxInfo.RateIncTax.ToString();
    }

    private void SetLabelAlwaysCalculatedWith(TariffGroup tarifGroup)
    {
        IsAlwaysCalculatedWithVisible = false;
        AlwaysCalculatedWithText = string.Empty;

        if (tarifGroup != null && tarifGroup.Id > 0)
        {
            var message = _languageService.Translate(
                "RatesAlwaysCalculatedWith",
                "Category is always calculated with tarif group: %s");
            message = message.Replace("%s", tarifGroup.Description);

            IsAlwaysCalculatedWithVisible = true;
            AlwaysCalculatedWithText = message;
        }
    }

    private void ChangeRateType()
    {
        if (SelectedRate == null || SelectedRateType == null)
            return;

        var rateType = (RateType)SelectedRateType.Id;
        var rate = SelectedRate;

        if (rateType != RateType.Staffel && rate != null)
        {
            var staffelList = _controller.UnitOfWork.StaffelRepo.SelectByRateId(rate.Id);
            if (staffelList != null && staffelList.Any())
            {
                var result = MessageBox.Show(
                    "There are still staffel records for current rate, but type is no longer of type staffel. Do you want to delete the staffel records?",
                    "Staffel",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    _controller.UnitOfWork.StaffelRepo.DeleteByRateId(rate.Id);
            }
        }

        OnPropertyChanged(nameof(IsStaffel));
    }

    private void AddRate()
    {
        if (!ValidateInput())
            return;

        var costCategory = SelectedCostCategory;
        var energyType = SelectedEnergyType;
        var tarifGroup = GetCurrentTarifGroup();

        var entity = _controller.UnitOfWork.AddDefaultEntity(energyType.Id, costCategory.Id, tarifGroup.Id);

        Rates.Clear();
        foreach (var r in _controller.UnitOfWork.RateList)
            Rates.Add(r);

        SelectedRate = entity;
    }

    private void SaveRate()
    {
        if (SelectedRate != null)
        {
            SelectedRate.PriceChange = _controller.GetPriceChange(SelectedRate);
            _controller.UnitOfWork.Complete();
        }
    }

    private void CancelRate()
    {
        _controller.UnitOfWork.CancelChanges();
        InitRates();
    }

    private TariffGroup GetCurrentTarifGroup()
    {
        var costCategory = SelectedCostCategory;
        var tarifGroup = costCategory?.TariffGroup;
        if (tarifGroup == null || tarifGroup.Id <= 0)
        {
            tarifGroup = SelectedTariffGroup ?? new TariffGroup();
        }
        return tarifGroup;
    }

    private void DeleteRate()
    {
        if (SelectedRate == null)
            return;

        var message = _languageService.Translate("RatesAskDelete", "Are you sure you want to delete this rate?");
        var message2 = _languageService.Translate("DeleteTitle", "Delete?");
        if (MessageBox.Show(message, message2, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            _controller.UnitOfWork.Delete(SelectedRate);

            Rates.Clear();
            foreach (var r in _controller.UnitOfWork.RateList)
                Rates.Add(r);

            SelectedRate = Rates.FirstOrDefault();
        }
    }

    private void RefreshRates()
    {
        if (!ValidateInput())
            return;

        InitRates();
    }

    private void Close()
    {
        _window.Close();
    }

    private bool ValidateInput()
    {
        if (SelectedCostCategory == null)
        {
            var message = _languageService.Translate("SelectCategory", "Select a category");
            MessageBox.Show(message);
            return false;
        }

        if (SelectedEnergyType == null)
        {
            var message = _languageService.Translate("SelectEnergyType", "Select an energy type");
            MessageBox.Show(message);
            return false;
        }

        return true;
    }

    private bool CanModify() => SelectedEnergyType != null && SelectedCostCategory != null;
}