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

        public DataControlViewModel(Address address, EnergyType energyType)
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(Config.GetDbFileName());

            CurrentAddress = address;
            CurrentEnergyType = energyType;

            // Default filter range (zoals WinForms)
            FromDate = DateTime.Now.AddDays(-365);
            TillDate = DateTime.Now;

            AccumulativeYes = true;

            AddCommand = new RelayCommand(_ => AddReading());
            EditCommand = new RelayCommand(_ => EditReading());
            DeleteCommand = new RelayCommand(_ => DeleteReading());
            RefreshCommand = new RelayCommand(_ => LoadData());

            LoadData();
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
                    LoadData();
            }
        }

        private DateTime _tillDate;
        public DateTime TillDate
        {
            get => _tillDate;
            set
            {
                if (SetProperty(ref _tillDate, value))
                    LoadData();
            }
        }

        private bool? _accYes;
        public bool? AccumulativeYes
        {
            get => _accYes;
            set
            {
                if (SetProperty(ref _accYes, value))
                    LoadData();
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
                    LoadData();
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

        public void LoadData()
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

        private void AddReading()
        {
            MessageBox.Show("Add meter reading (WPF edit window nog te koppelen)");
        }

        private void EditReading()
        {
            if (SelectedReading == null)
                return;

            MessageBox.Show("Edit meter reading (WPF edit window nog te koppelen)");
        }

        private void DeleteReading()
        {
            if (SelectedReading == null)
                return;

            var msg = "Are you sure you want to delete this meter reading?";
            if (MessageBox.Show(msg, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _unitOfWork.Delete(SelectedReading);
                LoadData();
            }
        }

        #endregion

        #region IRefreshable

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            CurrentAddress = address;
            CurrentEnergyType = energyType;
            LoadData();
        }

        #endregion
    }
}