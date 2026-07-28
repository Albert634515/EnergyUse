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

            getEnergyTypes();
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
                    OnPropertyChanged();
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
                    initRates();
                }
            }
        }

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
                    if (_selectedRate != null)
                    {
                        SelectedRateType = RateTypes
                            .FirstOrDefault(r => r.Id == (int)_selectedRate.RateTypeId);
                    }

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

        private string _alwaysCalculatedWithText = string.Empty;
        public string AlwaysCalculatedWithText
        {
            get => _alwaysCalculatedWithText;
            set { _alwaysCalculatedWithText = value; OnPropertyChanged(); }
        }

        private bool _isAlwaysCalculatedWithVisible;
        public bool IsAlwaysCalculatedWithVisible
        {
            get => _isAlwaysCalculatedWithVisible;
            set { _isAlwaysCalculatedWithVisible = value; OnPropertyChanged(); }
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

        private async void getEnergyTypes()
        {
            EnergyTypes.Clear();
            var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
            foreach (var e in list)
                EnergyTypes.Add(e);
        }

        private void getCostCategories(long energyTypeId)
        {
            CostCategories.Clear();
            var list = _controller.UnitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyTypeId).ToList();
            foreach (var c in list)
                CostCategories.Add(c);
        }

        private void getTariffGroups(CostCategory costCategory)
        {
            SelectedTariffGroup = null;
            TariffGroups.Clear();
            var list = _controller.UnitOfWork.TarifGroupRepo.GetAll().ToList();

            if (costCategory?.TariffGroup != null && costCategory.TariffGroup.Id > 0)
                list = list.Where(t => t.Id == costCategory.TariffGroup.Id).ToList();

            foreach (var t in list)
                TariffGroups.Add(t);

            setLabelAlwaysCalculatedWith(costCategory?.TariffGroup);
        }

        private void getRateTypes()
        {
            RateTypes.Clear();
            var list = Managers.SelectionItemList.GetRateTypeList();
            foreach (var r in list)
                RateTypes.Add(r);
        }

        #endregion

        #region Logic

        private async Task onEnergyTypeChangedAsync()
        {
            if (SelectedEnergyType != null)
            {
                SelectedCostCategory = null;
                getCostCategories(SelectedEnergyType.Id);
            }
            else
            {
                CostCategories.Clear();
                SelectedCostCategory = null;
            }

            if (SelectedCostCategory == null)
                initRates();
        }

        private async Task onCostCategoryChangedAsync()
        {
            if (SelectedCostCategory != null && SelectedEnergyType != null)
            {
                getTariffGroups(SelectedCostCategory);
                initRates();
            }
            else
            {
                TariffGroups.Clear();
                initRates();
            }
        }

        private void initRates()
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

            if (costCategory.TariffGroupId > 0)
                _controller.UnitOfWork.RateList =
                    _controller.UnitOfWork.RateRepo
                        .SelectByCostCategoryAndEnergyTypeAndTarifGroup(
                            costCategory.Id,
                            energyType.Id,
                            costCategory.TariffGroupId.Value)
                        .ToList();
            else if (tarifGroup.Id > 0)
                _controller.UnitOfWork.RateList =
                    _controller.UnitOfWork.RateRepo
                        .SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroup.Id)
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
                    vatPercentage));
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

        private void setLabelAlwaysCalculatedWith(TariffGroup? tarifGroup)
        {
            IsAlwaysCalculatedWithVisible = false;
            AlwaysCalculatedWithText = string.Empty;

            if (tarifGroup != null && tarifGroup.Id > 0)
            {
                AlwaysCalculatedWithText = $"Category is always calculated with tarif group: {tarifGroup.Description}";
                IsAlwaysCalculatedWithVisible = true;
            }
        }

        private void changeRateType()
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
                    var result = _dialogService.ShowYesNo(
                        "There are still staffel records for current rate, but type is no longer of type staffel. Do you want to delete the staffel records?",
                        "Staffel");

                    if (result)
                        _controller.UnitOfWork.StaffelRepo.DeleteByRateId(rate.Id);
                }
            }

            OnPropertyChanged(nameof(IsStaffel));
        }

        #endregion

        #region Commands Logic

        private void addRate()
        {
            if (!validateInput())
                return;

            if (SelectedCostCategory == null || SelectedEnergyType == null)
                return;

            var costCategory = SelectedCostCategory;
            var energyType = SelectedEnergyType;
            var tarifGroup = getCurrentTarifGroup();

            var entity = _controller.UnitOfWork.AddDefaultEntity(energyType.Id, costCategory.Id, tarifGroup.Id);

            Rates.Clear();
            foreach (var r in _controller.UnitOfWork.RateList)
                Rates.Add(r);

            SelectedRate = entity;
            _ = rebuildRateRowsAsync();
        }

        private void setRate()
        {
            if (SelectedRate != null)
            {
                if (!confirmNewRateFitsExistingRates())
                    return;

                SelectedRate.PriceChange = _controller.GetPriceChange(SelectedRate);
                _controller.UnitOfWork.Complete();
                _ = rebuildRateRowsAsync();
            }
        }

        private bool confirmNewRateFitsExistingRates()
        {
            if (SelectedRate == null || SelectedRate.Id > 0)
                return true;

            var newStartDate = SelectedRate.StartRate.Date;
            var newEndDate = SelectedRate.EndRate.Date;
            var existingRates = Rates
                .Where(rate => !ReferenceEquals(rate, SelectedRate))
                .OrderBy(rate => rate.StartRate)
                .ToList();

            var validationMessages = new List<string>();

            if (newEndDate < newStartDate)
            {
                validationMessages.Add("The end date is before the start date.");
            }
            else if (existingRates.Count > 0)
            {
                var overlappingRates = existingRates
                    .Where(rate =>
                        rate.StartRate.Date <= newEndDate &&
                        rate.EndRate.Date >= newStartDate)
                    .ToList();

                if (overlappingRates.Count > 0)
                {
                    var overlappingPeriods = string.Join(
                        Environment.NewLine,
                        overlappingRates.Select(rate =>
                            $"- {rate.StartRate:dd-MM-yyyy} through {rate.EndRate:dd-MM-yyyy}"));

                    validationMessages.Add(
                        $"The new rate overlaps the following existing period(s):{Environment.NewLine}" +
                        overlappingPeriods);
                }
                else
                {
                    var previousRate = existingRates
                        .Where(rate => rate.EndRate.Date < newStartDate)
                        .OrderByDescending(rate => rate.EndRate)
                        .FirstOrDefault();

                    var nextRate = existingRates
                        .Where(rate => rate.StartRate.Date > newEndDate)
                        .OrderBy(rate => rate.StartRate)
                        .FirstOrDefault();

                    if (previousRate != null)
                    {
                        var expectedStartDate = previousRate.EndRate.Date.AddDays(1);
                        if (newStartDate != expectedStartDate)
                        {
                            validationMessages.Add(
                                $"The previous rate ends on {previousRate.EndRate:dd-MM-yyyy}. " +
                                $"The expected start date is {expectedStartDate:dd-MM-yyyy}, " +
                                $"but {newStartDate:dd-MM-yyyy} was entered.");
                        }
                    }

                    if (nextRate != null)
                    {
                        var expectedEndDate = nextRate.StartRate.Date.AddDays(-1);
                        if (newEndDate != expectedEndDate)
                        {
                            validationMessages.Add(
                                $"The next rate starts on {nextRate.StartRate:dd-MM-yyyy}. " +
                                $"The expected end date is {expectedEndDate:dd-MM-yyyy}, " +
                                $"but {newEndDate:dd-MM-yyyy} was entered.");
                        }
                    }
                }
            }

            if (validationMessages.Count == 0)
                return true;

            var message =
                $"This new rate does not connect correctly to the existing rate periods.{Environment.NewLine}{Environment.NewLine}" +
                $"{string.Join(Environment.NewLine + Environment.NewLine, validationMessages)}{Environment.NewLine}{Environment.NewLine}" +
                "Do you want to save the rate anyway?";

            return _dialogService.ShowYesNo(message, "Rate period warning");
        }

        private void cancelRate()
        {
            _controller.UnitOfWork.CancelChanges();
            initRates();
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

        private void refreshRates()
        {
            if (!validateInput())
                return;

            initRates();
        }

        private void close()
        {
            _window.Close();
        }

        private bool validateInput()
        {
            if (SelectedCostCategory == null)
            {
                _dialogService.Show("Select a category", "Information");
                return false;
            }

            if (SelectedEnergyType == null)
            {
                _dialogService.Show("Select an energy type", "Information");
                return false;
            }

            return true;
        }

        private TariffGroup getCurrentTarifGroup()
        {
            var costCategory = SelectedCostCategory;
            var tarifGroup = costCategory?.TariffGroup;
            if (tarifGroup == null || tarifGroup.Id <= 0)
            {
                tarifGroup = SelectedTariffGroup ?? new TariffGroup();
            }
            return tarifGroup;
        }

        private bool CanModify() => SelectedEnergyType != null && SelectedCostCategory != null;

        #endregion
    }
}
