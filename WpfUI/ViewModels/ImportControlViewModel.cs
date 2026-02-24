using EnergyUse.Core.Manager;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WpfUI.Interfaces;
using WpfUI.Managers;

namespace WpfUI.ViewModels
{
    public class ImportControlViewModel : ViewModelBase, IRefreshable
    {
        private readonly EnergyUse.Core.UnitOfWork.Import _unitOfWork;

        public ImportControlViewModel(Address address, EnergyType energyType)
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Import(Config.GetDbFileName());

            CurrentAddress = address;
            CurrentEnergyType = energyType;

            MeterReadings = new ObservableCollection<MeterReading>();
            Meters = new ObservableCollection<Meter>();

            ImportCommand = new RelayCommand(_ => LoadData());
            RecalculateCommand = new RelayCommand(_ => Recalculate());
            SaveCommand = new RelayCommand(_ => SaveImport());
            BrowseFileCommand = new RelayCommand(_ => BrowseFile());

            ResetSelection(address, energyType);
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

        private Meter _selectedMeter;
        public Meter SelectedMeter
        {
            get => _selectedMeter;
            set
            {
                if (SetProperty(ref _selectedMeter, value))
                    LoadLastUsedFileAndAutoImport();
            }
        }

        private string _importFile;
        public string ImportFile
        {
            get => _importFile;
            set => SetProperty(ref _importFile, value);
        }

        public ObservableCollection<MeterReading> MeterReadings { get; }
        public ObservableCollection<Meter> Meters { get; }

        #endregion

        #region Commands

        public ICommand ImportCommand { get; }
        public ICommand RecalculateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand BrowseFileCommand { get; }

        #endregion

        #region Methods

        public void ResetSelection(Address address, EnergyType energyType)
        {
            CurrentAddress = address;
            CurrentEnergyType = energyType;

            MeterReadings.Clear();

            LoadMeters();
            LoadLastUsedFileAndAutoImport();
        }

        private async void LoadMeters()
        {
            Meters.Clear();

            if (CurrentAddress == null || CurrentEnergyType == null)
                return;

            var list = await _unitOfWork.MeterRepo
                .SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id);

            foreach (var m in list)
                Meters.Add(m);

            SelectedMeter = Meters.FirstOrDefault(x => x.Active) ?? Meters.FirstOrDefault();
        }

        private void LoadLastUsedFileAndAutoImport()
        {
            if (SelectedMeter == null)
                return;

            var libSettings = new LibSettings(Config.GetDbFileName());
            ImportFile = libSettings.GetLastUsedImportFile(GetLastImportFileKey());

            if (!string.IsNullOrWhiteSpace(ImportFile))
                LoadData();
        }

        public void LoadData()
        {
            if (!ValidateImport())
                return;

            ImportCurrentFile();
        }

        private bool ValidateImport()
        {
            if (string.IsNullOrWhiteSpace(ImportFile))
            {
                MessageBox.Show("Please select a file to import");
                return false;
            }

            if (!File.Exists(ImportFile))
            {
                MessageBox.Show("Selected file does not exist");
                return false;
            }

            if (CurrentEnergyType == null)
            {
                MessageBox.Show("Please select an energy type");
                return false;
            }

            if (SelectedMeter == null)
            {
                MessageBox.Show("Please select a meter");
                return false;
            }

            return true;
        }

        private async void ImportCurrentFile()
        {
            if (CurrentEnergyType == null || SelectedMeter == null)
                return;

            var libEpplus = new LibEpplus(Config.GetDbFileName());
            var imported = libEpplus.ImportFromCsvFile(ImportFile, CurrentEnergyType, SelectedMeter);

            if (imported.Count == 0)
                return;

            // Remove duplicates
            imported = imported
                .OrderByDescending(x => x.RegistrationDate)
                .GroupBy(x => x.RegistrationDate.Date)
                .Select(g => g.First())
                .ToList();

            // Load existing data
            var minDate = imported.Min(x => x.RegistrationDate);
            var maxDate = imported.Max(x => x.RegistrationDate);

            _unitOfWork.meterReadings = _unitOfWork.MeterReadingRepo
                .SelectByRange(minDate.AddDays(-7), maxDate, CurrentEnergyType.Id, CurrentAddress.Id)
                .ToList();

            var meterList = (await _unitOfWork.MeterRepo
                .SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id))
                .OrderByDescending(x => x.ActiveFrom)
                .ToList();

            MeterReading lastReading = null;

            var libMeterReading = new LibMeterReading(Config.GetDbFileName());

            foreach (var importedReading in imported.OrderBy(x => x.RegistrationDate))
            {
                var meter = meterList
                    .Where(m => m.ActiveFrom.Date <= importedReading.RegistrationDate.Date)
                    .OrderBy(m => m.ActiveFrom)
                    .LastOrDefault();

                var existing = _unitOfWork.MeterReadingRepo
                    .SelectByExists(importedReading.RegistrationDate.Date, importedReading.EnergyType.Id, meter.Id)
                    .FirstOrDefault();

                if (existing == null)
                {
                    var newReading = new MeterReading
                    {
                        Id = null,
                        EnergyTypeId = CurrentEnergyType.Id,
                        MeterId = meter.Id,
                        RegistrationDate = importedReading.RegistrationDate.Date,
                        WeekNo = ISOWeek.GetWeekOfYear(importedReading.RegistrationDate),
                        RateNormal = importedReading.RateNormal,
                        RateLow = importedReading.RateLow,
                        ReturnDeliveryLow = importedReading.ReturnDeliveryLow,
                        ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal
                    };

                    libMeterReading.CalculateDiff(ref newReading, lastReading);
                    lastReading = newReading;

                    _unitOfWork.meterReadings.Add(newReading);
                }
                else
                {
                    existing = _unitOfWork.meterReadings.First(x => x.Id == existing.Id);

                    existing.RateNormal = importedReading.RateNormal;
                    existing.RateLow = importedReading.RateLow;
                    existing.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
                    existing.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;

                    libMeterReading.CalculateDiff(ref existing, lastReading);
                    lastReading = existing;
                }
            }

            MeterReadings.Clear();
            foreach (var r in _unitOfWork.meterReadings.OrderByDescending(x => x.RegistrationDate))
                MeterReadings.Add(r);
        }

        private void Recalculate()
        {
            if (_unitOfWork.meterReadings.Count == 0)
                return;

            var libMeterReading = new LibMeterReading(Config.GetDbFileName());

            libMeterReading.RecalculateReadingsDiffPreviousDay(
                _unitOfWork.meterReadings.Min(m => m.RegistrationDate),
                _unitOfWork.meterReadings.Max(m => m.RegistrationDate),
                CurrentEnergyType.Id,
                CurrentAddress.Id);

            MeterReadings.Clear();
            foreach (var r in _unitOfWork.meterReadings.OrderByDescending(x => x.RegistrationDate))
                MeterReadings.Add(r);
        }

        private void SaveImport()
        {
            foreach (var reading in _unitOfWork.meterReadings.Where(r => r.Id == null))
            {
                _unitOfWork.MeterReadingRepo.Add(reading);
                _unitOfWork.Complete();
            }

            MessageBox.Show("Data has been saved");
        }

        private void BrowseFile()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                ImportFile = dlg.FileName;

                var libSettings = new LibSettings(Config.GetDbFileName());
                libSettings.SetLastUsedImportFile(ImportFile, GetLastImportFileKey());

                LoadData();
            }
        }

        private string GetLastImportFileKey()
        {
            if (SelectedMeter == null)
                return "LastImportFile";

            return $"{SelectedMeter.Id}_LastImportFile";
        }

        #endregion

        #region IRefreshable

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            ResetSelection(address, energyType);
        }

        #endregion
    }
}