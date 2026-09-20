using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using WpfUI.Managers;
using WpfUI.ViewModels;

public class ImportControlViewModel : ViewModelBase
{
    private readonly EnergyUse.Core.UnitOfWork.Import _uow;
    private readonly ISettingsService _settings;
    private readonly IDialogService _dialog;
    private readonly IImportService _importService;

    public ImportControlViewModel(Address address,
                                    EnergyType energyType,
                                    ISettingsService settings,
                                    IDialogService dialog,
                                    IImportService importService)
    {
        _uow = new EnergyUse.Core.UnitOfWork.Import(Config.GetDbFileName());
        _settings = settings;
        _dialog = dialog;
        _importService = importService;

        CurrentAddress = address;
        CurrentEnergyType = energyType;

        MeterReadings = new ObservableCollection<MeterReading>();
        Meters = new ObservableCollection<Meter>();

        ImportCommand = new RelayCommand(_ => SetData());
        RecalculateCommand = new RelayCommand(_ => recalculate());
        SaveCommand = new RelayCommand(_ => saveImport());
        BrowseFileCommand = new RelayCommand(_ => browseFile());

        ResetSelection(address, energyType);
    }

    public Address? CurrentAddress { get; set; }
    public EnergyType? CurrentEnergyType { get; set; }

    private Meter? _selectedMeter;
    public Meter? SelectedMeter
    {
        get => _selectedMeter;
        set
        {
            if (SetProperty(ref _selectedMeter, value))
            {
                _settings.Save("LastSelectedMeter", value?.Id.ToString() ?? "");
                clearImportPreview();
                loadSelectedMeterData();
            }
        }
    }

    private string _importFile = String.Empty;
    public string ImportFile
    {
        get => _importFile;
        set => SetProperty(ref _importFile, value);
    }

    public ObservableCollection<MeterReading> MeterReadings { get; }
    public ObservableCollection<Meter> Meters { get; }

    public ICommand ImportCommand { get; }
    public ICommand RecalculateCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand BrowseFileCommand { get; }

    public void ResetSelection(Address address, EnergyType energyType)
    {
        CurrentAddress = address;
        CurrentEnergyType = energyType;

        _selectedMeter = null;
        OnPropertyChanged(nameof(SelectedMeter));
        clearImportPreview();
        ImportFile = string.Empty;
        setMeters();
    }

    private async void setMeters()
    {
        Meters.Clear();
        var savedId = _settings.Get("LastSelectedMeter");

        if (CurrentAddress == null || CurrentEnergyType == null)
            return;

        var list = await _uow.MeterRepo.SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id);

        foreach (var m in list)
            Meters.Add(m);

        if (int.TryParse(savedId, out int id))
        {
            SelectedMeter = Meters.FirstOrDefault(m => m.Id == id)
                                                    ?? Meters.FirstOrDefault(x => x.Active)
                                                    ?? Meters.FirstOrDefault();
        }
        else
        {
            SelectedMeter = Meters.FirstOrDefault(x => x.Active)
                                                  ?? Meters.FirstOrDefault();
        }
    }

    private async void loadSelectedMeterData()
    {
        if (SelectedMeter == null)
        {
            ImportFile = string.Empty;
            return;
        }

        var selectedMeter = SelectedMeter;
        ImportFile = _settings.GetLastUsedImportFile(getKey()) ?? "";

        try
        {
            var readings = (await _uow.MeterReadingRepo.SelectByMeterId(selectedMeter.Id)).ToList();

            if (SelectedMeter?.Id != selectedMeter.Id)
                return;

            _uow.meterReadings = readings;
            await mergeCsvDataForSelectedMeter(selectedMeter);
            MeterReadings.Clear();
            foreach (var reading in _uow.meterReadings.OrderByDescending(reading => reading.RegistrationDate))
                MeterReadings.Add(reading);
        }
        catch (Exception ex)
        {
            _dialog.Show(ex.Message, "Import");
        }
    }

    private void clearImportPreview()
    {
        _uow.CancelChanges();
        _uow.meterReadings = new List<MeterReading>();
        MeterReadings.Clear();
    }

    private async Task mergeCsvDataForSelectedMeter(Meter selectedMeter)
    {
        if (CurrentEnergyType == null || string.IsNullOrWhiteSpace(ImportFile) || !File.Exists(ImportFile))
            return;

        var libEpplus = new LibEpplus(Config.GetDbFileName());
        var importedReadings = libEpplus.ImportFromCsvFile(ImportFile, CurrentEnergyType, selectedMeter)
            .OrderByDescending(reading => reading.RegistrationDate)
            .GroupBy(reading => reading.RegistrationDate.Date)
            .Select(group => group.First())
            .Where(reading => reading.RegistrationDate.Date >= selectedMeter.ActiveFrom.Date
                           && (selectedMeter.ActiveTill == null ||
                               reading.RegistrationDate.Date <= selectedMeter.ActiveTill.Value.Date))
            .OrderBy(reading => reading.RegistrationDate)
            .ToList();

        foreach (var importedReading in importedReadings)
        {
            var existingReading = _uow.meterReadings.FirstOrDefault(reading =>
                reading.MeterId == selectedMeter.Id &&
                reading.RegistrationDate.Date == importedReading.RegistrationDate.Date);

            if (existingReading == null)
            {
                importedReading.Id = null;
                importedReading.MeterId = selectedMeter.Id;
                importedReading.EnergyTypeId = CurrentEnergyType.Id;
                importedReading.WeekNo = System.Globalization.ISOWeek.GetWeekOfYear(importedReading.RegistrationDate);
                _uow.meterReadings.Add(importedReading);
                continue;
            }

            existingReading.RateNormal = importedReading.RateNormal;
            existingReading.RateLow = importedReading.RateLow;
            existingReading.ReturnDeliveryLow = importedReading.ReturnDeliveryLow;
            existingReading.ReturnDeliveryNormal = importedReading.ReturnDeliveryNormal;
            existingReading.WeekNo = System.Globalization.ISOWeek.GetWeekOfYear(existingReading.RegistrationDate);
        }

        if (_uow.meterReadings.Count > 0)
        {
            var libMeterReading = new LibMeterReading(Config.GetDbFileName());
            _uow.meterReadings = await libMeterReading
                .RecalculateReadingsDiffPreviousDay(_uow.meterReadings);
        }
    }

    public async void SetData()
    {
        if (!validateImport())
            return;

        var selectedMeter = SelectedMeter!;

        try
        {
            var result = await _importService.ImportAsync(ImportFile,
                                                            CurrentAddress!,
                                                            CurrentEnergyType!,
                                                            selectedMeter,
                                                            _uow);

            if (SelectedMeter?.Id != selectedMeter.Id)
                return;

            MeterReadings.Clear();
            foreach (var r in result.Where(reading => reading.MeterId == selectedMeter.Id))
                MeterReadings.Add(r);
        }
        catch (Exception ex)
        {
            _dialog.Show(ex.Message, "Import");
        }
    }

    private bool validateImport()
    {
        if (CurrentAddress == null)
        {
            _dialog.Show("Please select an address", "Import");
            return false;
        }

        if (string.IsNullOrWhiteSpace(ImportFile))
        {
            _dialog.Show("Please select a file to import", "Import");
            return false;
        }

        if (!File.Exists(ImportFile))
        {
            _dialog.Show("Selected file does not exist", "Import");
            return false;
        }

        if (CurrentEnergyType == null)
        {
            _dialog.Show("Please select an energy type", "Import");
            return false;
        }

        if (SelectedMeter == null)
        {
            _dialog.Show("Please select a meter", "Import");
            return false;
        }

        return true;
    }

    private async void recalculate()
    {
        if (_uow.meterReadings == null || _uow.meterReadings.Count == 0 || CurrentEnergyType == null || CurrentAddress == null)
            return;

        var lib = new LibMeterReading(Config.GetDbFileName());

        _uow.meterReadings = await lib.RecalculateReadingsDiffPreviousDay(_uow.meterReadings);

        MeterReadings.Clear();
        foreach (var r in _uow.meterReadings.OrderByDescending(x => x.RegistrationDate))
            MeterReadings.Add(r);
    }

    private void saveImport()
    {
        if (_uow.meterReadings == null || _uow.meterReadings.Count == 0)
            return;

        foreach (var r in _uow.meterReadings.Where(x => x.Id == null))
            _uow.MeterReadingRepo.Add(r);

        if (_uow.HasChanges())
            _uow.Complete();

        _dialog.Show("Data has been saved", "Import");
    }

    private void browseFile()
    {
        var file = _dialog.OpenFile(
            "CSV files (*.csv)|*.csv",
            "Select import file");

        if (file != null)
        {
            ImportFile = file;
            _settings.SaveLastUsedImportFile(getKey(), file);
            SetData();
        }
    }

    private string getKey() =>
        SelectedMeter == null
            ? "LastImportFile"
            : $"{SelectedMeter.Id}LastImportFile";
}
