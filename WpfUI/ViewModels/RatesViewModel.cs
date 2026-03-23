using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;
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

        public ObservableCollection<EnergyType> EnergyTypes { get; } = new();
        public ObservableCollection<CostCategory> CostCategories { get; } = new();
        public ObservableCollection<TariffGroup> TariffGroups { get; } = new();
        public ObservableCollection<Rate> Rates { get; } = new();
        public ObservableCollection<EnergyUse.Models.Common.SelectionItem> RateTypes { get; } = new();

        // ⭐ Staffel ViewModel geïntegreerd
        public StaffelViewModel StaffelVM { get; } = new();

        public RatesViewModel(Window window)
        {
            _window = window;
            _controller = new RateController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            AddRateCommand = new RelayCommand(_ => addRate(), _ => CanModify());
            SaveRateCommand = new RelayCommand(_ => saveRate(), _ => true);
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
                    _ = OnEnergyTypeChangedAsync();
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
                    _ = OnCostCategoryChangedAsync();
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

                    _ = setRateIncExLabelAsync();

                    // ⭐ Staffel laden
                    StaffelVM.LoadStaffels(_selectedRate?.Id ?? 0);
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

        private async Task OnEnergyTypeChangedAsync()
        {
            if (SelectedEnergyType != null)
                getCostCategories(SelectedEnergyType.Id);
            else
                CostCategories.Clear();

            initRates();
        }

        private async Task OnCostCategoryChangedAsync()
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
            Rates.Clear();

            if (SelectedEnergyType == null || SelectedCostCategory == null)
                return;

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

            if (Rates.Any())
                SelectedRate = Rates.FirstOrDefault();
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

        #endregion

        #region Commands Logic

        private void addRate()
        {
            if (!ValidateInput())
                return;

            if (SelectedCostCategory == null || SelectedEnergyType == null)
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

        private void saveRate()
        {
            if (SelectedRate != null)
            {
                SelectedRate.PriceChange = _controller.GetPriceChange(SelectedRate);
                _controller.UnitOfWork.Complete();
            }
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
            if (MessageBox.Show(message, message2, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _controller.UnitOfWork.Delete(SelectedRate);

                Rates.Clear();
                foreach (var r in _controller.UnitOfWork.RateList)
                    Rates.Add(r);

                SelectedRate = Rates.FirstOrDefault();
            }
        }

        private void refreshRates()
        {
            if (!ValidateInput())
                return;

            initRates();
        }

        private void close()
        {
            _window.Close();
        }

        private bool ValidateInput()
        {
            if (SelectedCostCategory == null)
            {
                MessageBox.Show("Select a category");
                return false;
            }

            if (SelectedEnergyType == null)
            {
                MessageBox.Show("Select an energy type");
                return false;
            }

            return true;
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

        private bool CanModify() => SelectedEnergyType != null && SelectedCostCategory != null;

        #endregion
    }
}