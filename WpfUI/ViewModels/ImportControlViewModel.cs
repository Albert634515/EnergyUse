using EnergyUse.Core.Import;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using WpfUI.Managers;
using WpfUI.ViewModels;

public class ImportControlViewModel : ViewModelBase
{
    private readonly ISettingsService _settings;
    private readonly IDialogService _dialog;
    private readonly IImportService _importService;
    private int _version;
    private ImportPreview? _preview;
    private bool _busy;

    public ImportControlViewModel(Address address, EnergyType energyType, ISettingsService settings,
        IDialogService dialog, IImportService importService)
    {
        _settings = settings;
        _dialog = dialog;
        _importService = importService;
        ImportCommand = new RelayCommand(async _ => await prepareAsync(), _ => !_busy);
        RecalculateCommand = ImportCommand;
        SaveCommand = new RelayCommand(async _ => await saveAsync(), _ => !_busy && _preview is { IsSaved: false } && _preview.UnresolvedCount == 0 && _preview.SelectedCount > 0);
        BrowseFileCommand = new RelayCommand(_ => browseFile(), _ => !_busy);
        ResetSelection(address, energyType);
    }

    public Address? CurrentAddress { get; private set; }
    public EnergyType? CurrentEnergyType { get; private set; }
    public ObservableCollection<MeterReading> MeterReadings { get; } = new();
    public ObservableCollection<Meter> Meters { get; } = new();
    public ObservableCollection<ImportPreviewRow> OverviewRows { get; } = new();
    public string ImportSummary => _preview?.Summary ?? "Load a CSV file to review new, changed and conflicting readings.";
    public string ImportHelp => _busy ? "Working…" : _preview?.IsSaved == true ? "Import saved." :
        "Stored and imported readings for this address and energy type. Green: new · yellow: changed · red: conflict. Choose a source row or Skip for each import conflict before saving.";
    public ICommand ImportCommand { get; }
    public ICommand RecalculateCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand BrowseFileCommand { get; }

    private Meter? _selectedMeter;
    public Meter? SelectedMeter
    {
        get => _selectedMeter;
        set
        {
            if (!SetProperty(ref _selectedMeter, value)) return;
            invalidatePreview();
            _settings.Save("LastSelectedMeter", value?.Id.ToString() ?? "");
            ImportFile = _settings.GetLastUsedImportFile(getKey()) ?? "";
            _ = loadStoredAsync();
        }
    }

    private string _importFile = "";
    public string ImportFile
    {
        get => _importFile;
        set
        {
            if (!SetProperty(ref _importFile, value)) return;
            invalidatePreview();
        }
    }

    public void ResetSelection(Address address, EnergyType energyType)
    {
        CurrentAddress = address;
        CurrentEnergyType = energyType;
        _selectedMeter = null;
        OnPropertyChanged(nameof(SelectedMeter));
        MeterReadings.Clear();
        Meters.Clear();
        invalidatePreview();
        ImportFile = "";
        _ = loadMetersAsync();
    }

    private async Task loadMetersAsync()
    {
        var address = CurrentAddress;
        var energyType = CurrentEnergyType;
        if (address == null || energyType == null) return;
        try
        {
            using var work = new EnergyUse.Core.UnitOfWork.Import(Config.GetDbFileName());
            var list = await work.MeterRepo.SelectByAddressAndEnergyType(address.Id, energyType.Id);
            if (address.Id != CurrentAddress?.Id || energyType.Id != CurrentEnergyType?.Id) return;
            Meters.Clear();
            foreach (var meter in list) Meters.Add(meter);
            long.TryParse(_settings.Get("LastSelectedMeter"), out var savedId);
            SelectedMeter = Meters.FirstOrDefault(m => m.Id == savedId) ?? Meters.FirstOrDefault(m => m.Active) ?? Meters.FirstOrDefault();
            if (SelectedMeter != null && File.Exists(ImportFile))
                await prepareAsync();
        }
        catch (Exception ex) { _dialog.Show(ex.Message, "Import"); }
    }

    private async Task loadStoredAsync()
    {
        var version = _version;
        var address = CurrentAddress;
        var energyType = CurrentEnergyType;
        if (address == null || energyType == null) return;
        try
        {
            using var work = new EnergyUse.Core.UnitOfWork.Import(Config.GetDbFileName());
            var readings = await work.MeterReadingRepo.SelectByEnergyIdAndAddressId(energyType.Id, address.Id);
            if (version != _version) return;
            MeterReadings.Clear();
            foreach (var reading in readings.OrderByDescending(row => row.RegistrationDate)) MeterReadings.Add(reading);
            rebuildOverview();
        }
        catch (Exception ex) { _dialog.Show(ex.Message, "Import"); }
    }

    private void invalidatePreview()
    {
        _version++;
        if (_preview != null)
            foreach (var row in _preview.Rows) row.PropertyChanged -= previewChanged;
        _preview = null;
        rebuildOverview();
        notifyPreview();
    }

    private void rebuildOverview()
    {
        OverviewRows.Clear();
        foreach (var row in ImportOverview.Merge(MeterReadings, _preview?.Rows ?? [])) OverviewRows.Add(row);
    }

    private void previewChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) => notifyPreview();
    private void notifyPreview()
    {
        OnPropertyChanged(nameof(ImportSummary));
        OnPropertyChanged(nameof(ImportHelp));
        CommandManager.InvalidateRequerySuggested();
    }

    public async void SetData() => await prepareAsync();

    private async Task prepareAsync()
    {
        if (_busy) return;
        invalidatePreview();
        if (CurrentAddress == null || CurrentEnergyType == null || SelectedMeter == null || !File.Exists(ImportFile))
        {
            _dialog.Show("Select an existing CSV file, address, energy type and meter.", "Import");
            return;
        }
        var version = _version;
        _busy = true;
        notifyPreview();
        try
        {
            var preview = await _importService.ImportAsync(ImportFile, CurrentAddress, CurrentEnergyType, SelectedMeter);
            if (version != _version) return;
            _preview = preview;
            foreach (var row in preview.Rows)
            {
                row.PropertyChanged += previewChanged;
            }
            rebuildOverview();
            await loadStoredAsync();
        }
        catch (Exception ex) { _dialog.Show(ex.Message, "Import"); }
        finally { _busy = false; notifyPreview(); }
    }

    private async Task saveAsync()
    {
        var preview = _preview;
        if (_busy || preview == null || preview.IsSaved || preview.UnresolvedCount > 0 || preview.SelectedCount == 0) return;
        if (!_dialog.ShowYesNo(preview.Summary + "\n\nSave these choices? Changed readings will be replaced; skipped rows will be kept.", "Save import")) return;
        _busy = true;
        notifyPreview();
        try
        {
            var count = await _importService.SaveAsync(preview);
            _dialog.Show($"{count} readings applied. Consumption differences have been recalculated.", "Import saved");
            invalidatePreview();
            await loadStoredAsync();
        }
        catch (Exception ex) { _dialog.Show($"Saving was aborted.\n{ex.Message}", "Import"); }
        finally { _busy = false; notifyPreview(); }
    }

    private void browseFile()
    {
        var file = _dialog.OpenFile("CSV files (*.csv)|*.csv", "Select import file");
        if (file == null) return;
        ImportFile = file;
        _settings.SaveLastUsedImportFile(getKey(), file);
        SetData();
    }

    private string getKey() => SelectedMeter == null ? "LastImportFile" : $"{SelectedMeter.Id}LastImportFile";
}
