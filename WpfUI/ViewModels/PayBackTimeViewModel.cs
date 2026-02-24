using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfUI.ViewModels;
using EnergyUse.Core.Controllers;
using EnergyUse.Models.Common;
using EnergyUse.Models;

namespace WpfUI.ViewModels
{
    public class PayBackTimeViewModel : ViewModelBase
    {
        private readonly PayBackTimeController _controller;

        public PayBackTimeViewModel()
        {
            _controller = new PayBackTimeController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            CalculateCommand = new RelayCommand(async _ => await CalculateAsync(), _ => CanCalculate);
            CloseCommand = new RelayCommand(_ => CloseWindow());

            PayBackTimes = new ObservableCollection<PayBackTime>();

            LoadAddresses();
        }

        // ---------------------------------------------------------
        // DATA SOURCES
        // ---------------------------------------------------------

        public ObservableCollection<Address> Addresses { get; set; } = new();
        public ObservableCollection<EnergyType> EnergyTypes { get; set; } = new();
        public ObservableCollection<PayBackTime> PayBackTimes { get; }

        // ---------------------------------------------------------
        // FORM FIELDS
        // ---------------------------------------------------------

        private Address _selectedAddress;
        public Address SelectedAddress
        {
            get => _selectedAddress;
            set
            {
                if (SetProperty(ref _selectedAddress, value))
                {
                    LoadEnergyTypes();
                    LoadAddressSettings();
                }
            }
        }

        private EnergyType _selectedEnergyType;
        public EnergyType SelectedEnergyType
        {
            get => _selectedEnergyType;
            set => SetProperty(ref _selectedEnergyType, value);
        }

        private DateTime _purchaseDate = DateTime.Today;
        public DateTime PurchaseDate
        {
            get => _purchaseDate;
            set => SetProperty(ref _purchaseDate, value);
        }

        private int _maxYears = 5;
        public int MaxYears
        {
            get => _maxYears;
            set => SetProperty(ref _maxYears, value);
        }

        private decimal _purchaseAmount;
        public decimal PurchaseAmount
        {
            get => _purchaseAmount;
            set => SetProperty(ref _purchaseAmount, value);
        }

        private decimal _subsidyAmount;
        public decimal SubsidyAmount
        {
            get => _subsidyAmount;
            set => SetProperty(ref _subsidyAmount, value);
        }

        private decimal _qualityReduction;
        public decimal QualityReduction
        {
            get => _qualityReduction;
            set => SetProperty(ref _qualityReduction, value);
        }

        private decimal _totalCapacity;
        public decimal TotalCapacity
        {
            get => _totalCapacity;
            set => SetProperty(ref _totalCapacity, value);
        }

        private decimal _averageReturn;
        public decimal AverageReturn
        {
            get => _averageReturn;
            set => SetProperty(ref _averageReturn, value);
        }

        // ---------------------------------------------------------
        // PROGRESS UI
        // ---------------------------------------------------------

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private string _progressText;
        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText, value);
        }

        private bool _isProgressVisible;
        public bool IsProgressVisible
        {
            get => _isProgressVisible;
            set => SetProperty(ref _isProgressVisible, value);
        }

        // ---------------------------------------------------------
        // COMMANDS
        // ---------------------------------------------------------

        public ICommand CalculateCommand { get; }
        public ICommand CloseCommand { get; }

        private bool CanCalculate =>
            SelectedAddress != null &&
            SelectedEnergyType != null &&
            MaxYears > 0 &&
            PurchaseAmount >= 0 &&
            SubsidyAmount >= 0;

        // ---------------------------------------------------------
        // INITIAL LOAD
        // ---------------------------------------------------------

        private async void LoadAddresses()
        {
            var list = (await _controller.UnitOfWork.AddressRepo.GetAll()).ToList();
            Addresses.Clear();
            foreach (var a in list)
                Addresses.Add(a);

            SelectedAddress = list.FirstOrDefault(x => x.DefaultAddress == true) ?? list.FirstOrDefault();
        }

        private void LoadEnergyTypes()
        {
            if (SelectedAddress == null)
                return;

            var list = _controller.UnitOfWork.EnergyTypeRepo
                .SelectByAddressId(SelectedAddress.Id)
                .Where(x => x.HasEnergyReturn)
                .ToList();

            EnergyTypes.Clear();
            foreach (var e in list)
                EnergyTypes.Add(e);

            SelectedEnergyType = EnergyTypes.FirstOrDefault();
        }

        private void LoadAddressSettings()
        {
            if (SelectedAddress == null)
                return;

            TotalCapacity = SelectedAddress.TotalCapacity;

            // Load settings per address
            QualityReduction = Managers.SettingsWpf.GetSettingDecimal($"QualityReductionSolarPanels_A{SelectedAddress.Id}", 0);
            PurchaseAmount = Managers.SettingsWpf.GetSettingDecimal($"PurchaseAmount_A{SelectedAddress.Id}", 0);
            SubsidyAmount = Managers.SettingsWpf.GetSettingDecimal($"SubsidyAmount_A{SelectedAddress.Id}", 0);
            PurchaseDate = Managers.SettingsWpf.GetDate($"PurchaseDate_A{SelectedAddress.Id}", DateTime.Today);
        }

        // ---------------------------------------------------------
        // CALCULATION
        // ---------------------------------------------------------

        private async Task CalculateAsync()
        {
            if (!await ValidateInputAsync())
                return;

            PayBackTimes.Clear();
            IsProgressVisible = true;
            ProgressValue = 0;

            decimal initialInvestment = PurchaseAmount - SubsidyAmount;
            decimal lastRoi = -Math.Abs(initialInvestment);
            DateTime lastPeriodStart = PurchaseDate;

            for (int i = 1; i <= MaxYears; i++)
            {
                ProgressText = $"Processing {i}/{MaxYears}";
                ProgressValue = (int)((double)i / MaxYears * 100);

                var parameters = new ParameterCalcPeriod
                {
                    PeriodId = i,
                    PeriodStart = lastPeriodStart,
                    Address = SelectedAddress,
                    EnergyType = SelectedEnergyType,
                    InitialInvestment = initialInvestment,
                    QualityReductionSolarPanels = QualityReduction,
                    TotalCapacitySolarPanels = TotalCapacity,
                    AverageReturn = AverageReturn
                };

                var result = await _controller.CalculatePayBackPeriodAsync(parameters);

                result.ReturnOnInvestmentTotal = lastRoi + result.ReturnOnInvestment;
                lastRoi = result.ReturnOnInvestmentTotal;

                PayBackTimes.Add(result);

                lastPeriodStart = result.EndPeriod.AddDays(1);
            }

            ProgressText = "Done";
            await Task.Delay(300);
            IsProgressVisible = false;
        }

        // ---------------------------------------------------------
        // VALIDATION
        // ---------------------------------------------------------

        private async Task<bool> ValidateInputAsync()
        {
            if (SelectedAddress == null)
            {
                MessageBox.Show("Select an address");
                return false;
            }

            if (PurchaseAmount < 0)
            {
                MessageBox.Show("Purchase amount must be >= 0");
                return false;
            }

            // Validate price per unit for each year
            for (int year = PurchaseDate.Year; year <= MaxYears; year++)
            {
                var defaultTariffGroupId = SelectedAddress.DefaultTariffGroupId ?? 0;
                var price = await _controller.GetPricePerUnitPerYear(year, defaultTariffGroupId, SelectedEnergyType.Id);

                if (price < 0)
                {
                    MessageBox.Show($"No price per unit for year {year}");
                    return false;
                }
            }

            return true;
        }

        // ---------------------------------------------------------
        // CLOSE WINDOW
        // ---------------------------------------------------------

        private void CloseWindow()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this)
                ?.Close();
        }
    }
}