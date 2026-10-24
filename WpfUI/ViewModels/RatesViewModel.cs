using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels
{
    public class RatesViewModel : ViewModelBase
    {
        private readonly RateController _controller;
        private readonly Window _window;
        private readonly IDialogService _dialogService;

        public ObservableCollection<EnergyType> EnergyTypes { get; } = new();
        public ObservableCollection<CostCategory> CostCategories { get; } = new();
        public ObservableCollection<TariffGroup> TariffGroups { get; } = new();
        public ObservableCollection<Rate> Rates { get; } = new();
        public ObservableCollection<RateGridRowViewModel> RateRows { get; } = new();
        public ObservableCollection<EnergyUse.Models.Common.SelectionItem> RateTypes { get; } = new();

        // ⭐ Staffel ViewModel geïntegreerd
        public StaffelViewModel StaffelVM { get; } = new();

        public RatesViewModel(Window window, IDialogService dialogService)
        {
            _window = window;
            _dialogService = dialogService;
            _controller = new RateController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            AddRateCommand = new RelayCommand(_ => addRate(), _ => CanModify());
            SaveRateCommand = new RelayCommand(_ => setRate(), _ => true);
            CancelRateCommand = new RelayCommand(_ => cancelRate(), _ => true);
            DeleteRateCommand = new RelayCommand(_ => deleteRate(), _ => SelectedRate != null);
            RefreshRatesCommand = new RelayCommand(_ => refreshRates(), _ => true);
            CloseCommand = new RelayCommand(_ => close(), _ => true);

            getRateTypes();
        }

        #region Properties

        private EnergyType? _selectedEnergyType;
        public EnergyType? SelectedEnergyType
        {
            get => _selectedEnergyType;
            set
            {
                if (_selectedEnergyType != value)
                {
                    _selectedEnergyType = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSelectCostCategory));
                    _ = onEnergyTypeChangedAsync();
                }
            }
        }

        private CostCategory? _selectedCostCategory;
        public CostCategory? SelectedCostCategory
        {
            get => _selectedCostCategory;
            set
            {
                if (_selectedCostCategory != value)
                {
                    _selectedCostCategory = value;
                    _allowedRateTariffGroupIds = value == null ? new() : _controller.UnitOfWork.CostCategoryRepo
                        .GetAllowedTariffGroupIds(value.Id).GetAwaiter().GetResult().ToHashSet();
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ApplicableRateTariffGroups));
                    _ = onCostCategoryChangedAsync();
                }
            }
        }

        private TariffGroup? _selectedTariffGroup;
        public TariffGroup? SelectedTariffGroup
        {
            get => _selectedTariffGroup;
            set
            {
                if (_selectedTariffGroup != value)
                {
                    _selectedTariffGroup = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanSelectEnergyType));
                    _ = onTariffGroupChangedAsync();
                }
            }
        }

        public bool CanSelectEnergyType => SelectedTariffGroup != null;
        public bool CanSelectCostCategory => SelectedEnergyType != null;
        private HashSet<long> _allowedRateTariffGroupIds = new();
        public IEnumerable<TariffGroup> ApplicableRateTariffGroups =>
            TariffGroups.Where(group => _allowedRateTariffGroupIds.Contains(group.Id));

        private Rate? _selectedRate;
        public Rate? SelectedRate
        {
            get => _selectedRate;
            set
            {
                if (_selectedRate != value)
                {
                    _selectedRate = value;
                    OnPropertyChanged();

                    // ⭐ Koppel RateType aan RateTypes lijst
                    SelectedRateType = _selectedRate == null
                        ? null
                        : RateTypes.FirstOrDefault(r => r.Id == _selectedRate.RateTypeId);

                    var selectedRow = RateRows
                        .FirstOrDefault(row => ReferenceEquals(row.Rate, _selectedRate));
                    if (_selectedRateRow != selectedRow)
                    {
                        _selectedRateRow = selectedRow;
                        OnPropertyChanged(nameof(SelectedRateRow));
                    }

                    _ = setRateIncExLabelAsync();

                    // ⭐ Staffel laden
                    StaffelVM.GetStaffels(_selectedRate?.Id ?? 0);
                }
            }
        }

        private RateGridRowViewModel? _selectedRateRow;
        public RateGridRowViewModel? SelectedRateRow
        {
            get => _selectedRateRow;
            set
            {
                if (_selectedRateRow != value)
                {
                    _selectedRateRow = value;
                    OnPropertyChanged();
                    SelectedRate = value?.Rate;
                }
            }
        }

        private EnergyUse.Models.Common.SelectionItem? _selectedRateType;
        public EnergyUse.Models.Common.SelectionItem? SelectedRateType
        {
            get => _selectedRateType;
            set
            {
                if (_selectedRateType != value)
                {
                    _selectedRateType = value;
                    OnPropertyChanged();

                    // ⭐ Schrijf terug naar Rate
                    if (SelectedRate != null && value != null)
                        SelectedRate.RateTypeId = value.Id;

                    OnPropertyChanged(nameof(IsStaffel));
                }
            }
        }

        public bool IsStaffel =>
            SelectedRateType != null && (RateType)SelectedRateType.Id == RateType.Staffel;

        private string _rateTaxInfoText = "_";
        public string RateTaxInfoText
        {
            get => _rateTaxInfoText;
            set { _rateTaxInfoText = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand AddRateCommand { get; }
        public ICommand SaveRateCommand { get; }
        public ICommand CancelRateCommand { get; }
        public ICommand DeleteRateCommand { get; }
        public ICommand RefreshRatesCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion

        #region Initialization

        public async Task InitializeAsync()
        {
            if (TariffGroups.Count > 0)
                return;

            var tariffGroups = await _controller.UnitOfWork.TarifGroupRepo.GetAll();
            foreach (var tariffGroup in tariffGroups.OrderBy(group => group.Description, StringComparer.CurrentCultureIgnoreCase))
                TariffGroups.Add(tariffGroup);
        }

        private async Task getEnergyTypes(long tariffGroupId)
        {
            var costCategories = await _controller.UnitOfWork.CostCategoryRepo.SelectByTariffGroup(tariffGroupId);
            var applicableEnergyTypeIds = costCategories
                .Where(category => category.EnergyTypeId.HasValue)
                .Select(category => category.EnergyTypeId.GetValueOrDefault())
                .ToHashSet();

            var energyTypes = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
            if (SelectedTariffGroup?.Id != tariffGroupId)
                return;

            EnergyTypes.Clear();
            foreach (var energyType in energyTypes
                .Where(type => applicableEnergyTypeIds.Contains(type.Id))
                .OrderBy(type => type.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                EnergyTypes.Add(energyType);
            }
        }

        private async Task getCostCategories(long energyTypeId, long tariffGroupId)
        {
            var costCategories = await _controller.UnitOfWork.CostCategoryRepo
                .SelectByTariffGroup(tariffGroupId, energyTypeId);
            if (SelectedTariffGroup?.Id != tariffGroupId || SelectedEnergyType?.Id != energyTypeId)
                return;

            var allowedIds = costCategories.Select(category => category.Id).ToHashSet();
            foreach (var category in CostCategories.Where(category => !allowedIds.Contains(category.Id)).ToList())
                CostCategories.Remove(category);
            foreach (var category in costCategories.Where(category => CostCategories.All(existing => existing.Id != category.Id)))
            {
                CostCategories.Add(category);
            }

            var sortedCategories = CostCategories
                .OrderBy(category => category.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
            for (var index = 0; index < sortedCategories.Count; index++)
            {
                var currentIndex = CostCategories.IndexOf(sortedCategories[index]);
                if (currentIndex != index)
                    CostCategories.Move(currentIndex, index);
            }
        }

        public async Task RefreshCostCategoriesAsync()
        {
            if (SelectedTariffGroup != null && SelectedEnergyType != null)
                await getCostCategories(SelectedEnergyType.Id, SelectedTariffGroup.Id);
        }

        private void getRateTypes()
        {
            RateTypes.Clear();
            var list = Managers.SelectionItemList.GetRateTypeList();
            foreach (var r in list.OrderBy(item => item.Description, StringComparer.CurrentCultureIgnoreCase))
                RateTypes.Add(r);
        }

        #endregion

        #region Logic

        private async Task onTariffGroupChangedAsync()
        {
            SelectedEnergyType = null;
            EnergyTypes.Clear();
            SelectedCostCategory = null;
            CostCategories.Clear();

            if (SelectedTariffGroup != null)
                await getEnergyTypes(SelectedTariffGroup.Id);

            await initRates();
        }

        private async Task onEnergyTypeChangedAsync()
        {
            SelectedCostCategory = null;
            CostCategories.Clear();

            if (SelectedEnergyType != null && SelectedTariffGroup != null)
            {
                await getCostCategories(SelectedEnergyType.Id, SelectedTariffGroup.Id);
            }

            await initRates();
        }

        private async Task onCostCategoryChangedAsync()
        {
            await initRates();
        }

        private async Task initRates()
        {
            _rateRowsVersion++;
            SelectedRate = null;
            Rates.Clear();
            RateRows.Clear();

            if (SelectedEnergyType == null
                || SelectedCostCategory == null
                || SelectedTariffGroup == null)
                return;

            var costCategory = SelectedCostCategory;
            var energyType = SelectedEnergyType;
            var tarifGroup = SelectedTariffGroup;

            _controller.UnitOfWork.RateList = new System.Collections.Generic.List<Rate>();

            if (tarifGroup.Id > 0)
                _controller.UnitOfWork.RateList =
                    (await _controller.UnitOfWork.RateRepo
                        .SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroup.Id))
                        .ToList();

            _controller.UnitOfWork.SetListSorted();

            foreach (var r in _controller.UnitOfWork.RateList)
                Rates.Add(r);

            if (Rates.Any())
                SelectedRate = Rates.FirstOrDefault();

            _ = rebuildRateRowsAsync();
        }

        private int _rateRowsVersion;

        private async Task rebuildRateRowsAsync()
        {
            var version = ++_rateRowsVersion;
            var costCategory = SelectedCostCategory;
            var rates = Rates.ToList();
            var selectedRate = SelectedRate;

            RateRows.Clear();
            if (costCategory == null)
                return;

            var vatTariffs = await _controller.UnitOfWork.RepoVatTarif
                .GetByCostCategoryId(costCategory.Id);

            if (version != _rateRowsVersion || SelectedCostCategory?.Id != costCategory.Id)
                return;

            var validationErrors = RateController.ValidateRates(rates);
            foreach (var rate in rates)
            {
                var vatPercentage = vatTariffs
                    .FirstOrDefault(vat =>
                        vat.StartDate.Date <= rate.StartRate.Date &&
                        vat.EndDate.Date >= rate.StartRate.Date)
                    ?.Tarif;

                RateRows.Add(new RateGridRowViewModel(
                    rate,
                    costCategory.CalculateVat,
                    vatPercentage)
                {
                    ValidationError = validationErrors.GetValueOrDefault(rate, string.Empty)
                });
            }

            SelectedRateRow = RateRows
                .FirstOrDefault(row => ReferenceEquals(row.Rate, selectedRate));
        }

        private async Task setRateIncExLabelAsync()
        {
            RateTaxInfoText = "_";

            if (SelectedRate == null || SelectedCostCategory == null)
                return;

            var rateTaxInfo = await _controller.GetRateIncExTax(SelectedCostCategory, SelectedRate);
            if (rateTaxInfo != null && SelectedCostCategory.CalculateVat)
                RateTaxInfoText = rateTaxInfo.RateIncTax.ToString();
        }

        private async void changeRateType()
        {
            if (SelectedRate == null || SelectedRateType == null)
                return;

            var rateType = (RateType)SelectedRateType.Id;
            var rate = SelectedRate;

            if (rateType != RateType.Staffel && rate != null)
            {
                var staffelList = await _controller.UnitOfWork.StaffelRepo.SelectByRateId(rate.Id);
                if (staffelList != null && staffelList.Any())
                {
                    var result = _dialogService.ShowYesNo(
                        "There are still staffel records for current rate, but type is no longer of type staffel. Do you want to delete the staffel records?",
                        "Staffel");

                    if (result)
                        await _controller.UnitOfWork.StaffelRepo.DeleteByRateId(rate.Id);
                }
            }

            OnPropertyChanged(nameof(IsStaffel));
        }

        #endregion

        #region Commands Logic

        private async void addRate()
        {
            if (!validateInput())
                return;

            if (SelectedCostCategory == null || SelectedEnergyType == null)
                return;

            var costCategory = SelectedCostCategory;
            var energyType = SelectedEnergyType;
            var tarifGroup = getCurrentTarifGroup();

            var entity = await _controller.UnitOfWork.AddDefaultEntity(energyType.Id, costCategory.Id, tarifGroup.Id);

            Rates.Clear();
            foreach (var r in _controller.UnitOfWork.RateList)
                Rates.Add(r);

            SelectedRate = entity;
            _ = rebuildRateRowsAsync();
        }

        private async void setRate()
        {
            var selectedRate = SelectedRate;
            if (selectedRate != null)
            {
                if (!await confirmRateFitsExistingRatesAsync(selectedRate)
                    || !ReferenceEquals(SelectedRate, selectedRate))
                    return;

                selectedRate.PriceChange = await _controller.GetPriceChange(selectedRate);
                _controller.UnitOfWork.Complete();
                await initRates();
            }
        }

        private async Task<bool> confirmRateFitsExistingRatesAsync(Rate selectedRate)
        {
            var errors = await _controller.GetRateValidationErrorsAsync(Rates.Append(selectedRate));
            foreach (var row in RateRows)
                row.RefreshValidation(errors.GetValueOrDefault(row.Rate, string.Empty));

            if (errors.Count == 0)
                return true;

            var details = string.Join(Environment.NewLine + Environment.NewLine, errors.Select(item =>
                $"Rate {item.Key.Id}: {item.Key.StartRate:dd-MM-yyyy} through {item.Key.EndRate:dd-MM-yyyy}{Environment.NewLine}{item.Value}"));
            if (errors.Values.Any(error => error.Contains("This cost category is not allowed")))
            {
                _dialogService.Show(details, "Rate validation");
                return false;
            }
            return _dialogService.ShowYesNo(
                $"The red rows contain rate validation errors.{Environment.NewLine}{Environment.NewLine}" +
                $"{details}{Environment.NewLine}{Environment.NewLine}Do you want to save the rates anyway?",
                "Rate validation");
        }

        private void cancelRate()
        {
            _controller.UnitOfWork.CancelChanges();
            _ = initRates();
        }

        private void deleteRate()
        {
            if (SelectedRate == null)
                return;

            var message = "Are you sure you want to delete this rate?";
            var message2 = "Delete?";
            if (_dialogService.ShowYesNo(message, message2))
            {
                _controller.UnitOfWork.Delete(SelectedRate);

                Rates.Clear();
                foreach (var r in _controller.UnitOfWork.RateList)
                    Rates.Add(r);

                SelectedRate = Rates.FirstOrDefault();
                _ = rebuildRateRowsAsync();
            }
        }

        private async void refreshRates()
        {
            await RefreshCostCategoriesAsync();
            await initRates();
        }

        private void close()
        {
            _window.Close();
        }

        private bool validateInput()
        {
            if (SelectedTariffGroup == null)
            {
                _dialogService.Show("Select a tariff group", "Information");
                return false;
            }

            if (SelectedEnergyType == null)
            {
                _dialogService.Show("Select an energy type", "Information");
                return false;
            }

            if (SelectedCostCategory == null)
            {
                _dialogService.Show("Select a category", "Information");
                return false;
            }

            return true;
        }

        private TariffGroup getCurrentTarifGroup()
        {
            return SelectedTariffGroup ?? new TariffGroup();
        }

        private bool CanModify() => SelectedTariffGroup != null
            && SelectedEnergyType != null
            && SelectedCostCategory != null;

        #endregion
    }
}
