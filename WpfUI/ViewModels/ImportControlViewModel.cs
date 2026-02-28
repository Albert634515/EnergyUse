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

    public ImportControlViewModel(
        Address address,
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

        ImportCommand = new RelayCommand(_ => LoadData());
        RecalculateCommand = new RelayCommand(_ => Recalculate());
        SaveCommand = new RelayCommand(_ => SaveImport());
        BrowseFileCommand = new RelayCommand(_ => BrowseFile());

        ResetSelection(address, energyType);
    }

    public Address CurrentAddress { get; set; }
    public EnergyType CurrentEnergyType { get; set; }

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

    public ICommand ImportCommand { get; }
    public ICommand RecalculateCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand BrowseFileCommand { get; }

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

        var list = await _uow.MeterRepo
            .SelectByAddressAndEnergyType(CurrentAddress.Id, CurrentEnergyType.Id);

        foreach (var m in list)
            Meters.Add(m);

        SelectedMeter = Meters.FirstOrDefault(x => x.Active) ?? Meters.FirstOrDefault();
    }

    private void LoadLastUsedFileAndAutoImport()
    {
        if (SelectedMeter == null)
            return;

        ImportFile = _settings.GetLastUsedImportFile(GetKey()) ?? "";

        if (!string.IsNullOrWhiteSpace(ImportFile))
            LoadData();
    }

    public async void LoadData()
    {
        if (!ValidateImport())
            return;

        var result = await _importService.ImportAsync(
            ImportFile,
            CurrentAddress,
            CurrentEnergyType,
            SelectedMeter,
            _uow);

        MeterReadings.Clear();
        foreach (var r in result)
            MeterReadings.Add(r);
    }

    private bool ValidateImport()
    {
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

    private void Recalculate()
    {
        if (_uow.meterReadings.Count == 0)
            return;

        var lib = new LibMeterReading(Config.GetDbFileName());

        lib.RecalculateReadingsDiffPreviousDay(
            _uow.meterReadings.Min(m => m.RegistrationDate),
            _uow.meterReadings.Max(m => m.RegistrationDate),
            CurrentEnergyType.Id,
            CurrentAddress.Id);

        MeterReadings.Clear();
        foreach (var r in _uow.meterReadings.OrderByDescending(x => x.RegistrationDate))
            MeterReadings.Add(r);
    }

    private void SaveImport()
    {
        foreach (var r in _uow.meterReadings.Where(x => x.Id == null))
        {
            _uow.MeterReadingRepo.Add(r);
            _uow.Complete();
        }

        _dialog.Show("Data has been saved", "Import");
    }

    private void BrowseFile()
    {
        var file = _dialog.OpenFile(
            "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
            "Select import file");

        if (file != null)
        {
            ImportFile = file;
            _settings.SaveLastUsedImportFile(GetKey(), file);
            LoadData();
        }
    }

    private string GetKey() =>
        SelectedMeter == null
            ? "LastImportFile"
            : $"{SelectedMeter.Id}LastImportFile";
}