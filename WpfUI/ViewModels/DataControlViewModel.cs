using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfUI.Interfaces;
using WpfUI.Managers;

namespace WpfUI.ViewModels
{
    public class DataControlViewModel : ViewModelBase, IRefreshable
    {
        private readonly EnergyUse.Core.UnitOfWork.MeterReading _unitOfWork;
        private readonly IDialogService _dialogService;

        public DataControlViewModel(Address address, EnergyType energyType, IDialogService dialogService)
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(Config.GetDbFileName());
            _dialogService = dialogService;

            CurrentAddress = address;
            CurrentEnergyType = energyType;

            // Default filter range (zoals WinForms)
            FromDate = DateTime.Now.AddDays(-365);
            TillDate = DateTime.Now;

            AccumulativeYes = true;

            AddCommand = new RelayCommand(_ => addReading());
            EditCommand = new RelayCommand(_ => editReading());
            DeleteCommand = new RelayCommand(_ => deleteReading());
            RefreshCommand = new RelayCommand(_ => SetData());

            SetData();
        }

        #region Properties

        private Address _currentAddress;
        public Address CurrentAddress
        {
            get => _currentAddress;
            set => SetProperty(ref _currentAddress, value);
        }

        private EnergyType _currentEnergyType;
        public EnergyType CurrentEnergyType
        {
            get => _currentEnergyType;
            set => SetProperty(ref _currentEnergyType, value);
        }

        public ObservableCollection<MeterReading> MeterReadings { get; } = new();

        private MeterReading _selectedReading;
        public MeterReading SelectedReading
        {
            get => _selectedReading;
            set => SetProperty(ref _selectedReading, value);
        }

        private DateTime _fromDate;
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (SetProperty(ref _fromDate, value))
                    SetData();
            }
        }

        private DateTime _tillDate;
        public DateTime TillDate
        {
            get => _tillDate;
            set
            {
                if (SetProperty(ref _tillDate, value))
                    SetData();
            }
        }

        private bool? _accYes;
        public bool? AccumulativeYes
        {
            get => _accYes;
            set
            {
                if (SetProperty(ref _accYes, value))
                    SetData();
            }
        }

        public bool? AccumulativeNo
        {
            get => !_accYes;
            set
            {
                if (value == true)
                {
                    AccumulativeYes = false;
                    SetData();
                }
            }
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        #endregion

        #region Methods

        public void SetData()
        {
            MeterReadings.Clear();

            if (CurrentAddress == null || CurrentEnergyType == null)
                return;

            var list = _unitOfWork.MeterReadingRepo
                .SelectByRange(FromDate, TillDate, CurrentEnergyType.Id, CurrentAddress.Id)
                .OrderByDescending(x => x.RegistrationDate)
                .ToList();

            foreach (var item in list)
                MeterReadings.Add(item);
        }

        private void addReading()
        {
            _dialogService.Show("Add meter reading (WPF edit window nog te koppelen)", "Information");
        }

        private void editReading()
        {
            if (SelectedReading == null)
                return;

            _dialogService.Show("Edit meter reading (WPF edit window nog te koppelen)", "Information");
        }

        private void deleteReading()
        {
            if (SelectedReading == null)
                return;

            var msg = "Are you sure you want to delete this meter reading?";
            if (_dialogService.ShowYesNo(msg, "Delete"))
            {
                _unitOfWork.Delete(SelectedReading);
                SetData();
            }
        }

        #endregion

        #region IRefreshable

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            CurrentAddress = address;
            CurrentEnergyType = energyType;
            SetData();
        }

        #endregion
    }
}