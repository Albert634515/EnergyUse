using WpfUI.ViewModels;
using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class SetupNewFileViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private SetupNewFileController _controller;

    public event Action RequestClose;

    public SetupNewFileViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        NextCommand = new RelayCommand(_ => Next(), _ => SelectedTabIndex < 3);
        PreviousCommand = new RelayCommand(_ => Previous(), _ => SelectedTabIndex > 0);
        SelectFileCommand = new RelayCommand(_ => SelectFile());
        SelectDirectoryCommand = new RelayCommand(_ => SelectDirectory());
        CreateDbCommand = new RelayCommand(_ =>
            {
                if (CreateDatabase())
                    RequestClose?.Invoke();
            },
            _ => true
        );

        Address = new Address();

        EnergyTypes = new ObservableCollection<SelectableEnergyType>();
        foreach (var t in new[]
        {
            new EnergyType { Name = "Elektriciteit" },
            new EnergyType { Name = "Gas" },
            new EnergyType { Name = "Warmtepomp" },
            new EnergyType { Name = "Zonnepanelen" }
        })
        {
            var selectable = new SelectableEnergyType(t);
            selectable.PropertyChanged += (_, __) => UpdateSummary();
            EnergyTypes.Add(selectable);
        }

        UpdateSummary();
        StatusMessage = "Klaar om te beginnen.";
    }

    // -----------------------------
    // Wizard navigation
    // -----------------------------
    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set { _selectedTabIndex = value; OnPropertyChanged(); }
    }

    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }

    private void Next() => SelectedTabIndex++;
    private void Previous() => SelectedTabIndex--;

    // -----------------------------
    // File selection
    // -----------------------------
    public ICommand SelectFileCommand { get; }
    public ICommand SelectDirectoryCommand { get; }

    private string _newFilePath;
    public string NewFilePath
    {
        get => _newFilePath;
        set { _newFilePath = value; OnPropertyChanged(); UpdateSummary(); }
    }

    private string _targetDirectory;
    public string TargetDirectory
    {
        get => _targetDirectory;
        set { _targetDirectory = value; OnPropertyChanged(); UpdateSummary(); }
    }

    private bool _isNewFile = true;
    public bool IsNewFile
    {
        get => _isNewFile;
        set
        {
            _isNewFile = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsExistingFile));
            UpdateSummary();
        }
    }

    public bool IsExistingFile
    {
        get => !_isNewFile;
        set
        {
            IsNewFile = !value;
            OnPropertyChanged();
            UpdateSummary();
        }
    }

    private void SelectFile()
    {
        var file = _dialogService.SaveFile("Database file (*.db)|*.db", "Choose database file");

        if (!string.IsNullOrEmpty(file))
            NewFilePath = file;
    }

    private void SelectDirectory()
    {
        var folder = _dialogService.OpenFolder();
        if (!string.IsNullOrEmpty(folder))
            TargetDirectory = folder;
    }

    // -----------------------------
    // Create DB
    // -----------------------------
    public ICommand CreateDbCommand { get; }

    private bool CreateDatabase()
    {
        StatusMessage = "Database is being created...";

        var targetFile = Path.Combine(TargetDirectory ?? "", Path.GetFileName(NewFilePath) ?? "");
        var currentFile = Managers.Config.GetDbFileName()?.Trim();

        if (!ValidateNewSetup())
            return false;

        try
        {
            var folder = Path.GetDirectoryName(targetFile);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (IsNewFile)
            {
                Managers.Config.SetDbFileName(targetFile);
                _controller = new SetupNewFileController(targetFile);
                _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(targetFile);

                SetAsDefaultFile(targetFile);

                SetupNewAddress();
                SetupNewMeters();

                if (AddBaseData)
                    SetupCostCategory();
            }

            if (!IsNewFile)
                SetAsDefaultFile(targetFile);
            else if (!SetAsDefaultFileOption && !string.IsNullOrWhiteSpace(currentFile))
                SetAsDefaultFile(currentFile);
            else
                SetAsDefaultFile(targetFile);

            UpdateSummary();
            StatusMessage = "Database succesvol created!";
            return true;
        }
        catch (Exception ex)
        {
            StatusMessage = "Error during creating of database: " + ex.Message;
            return false;
        }
    }

    private bool ValidateNewSetup()
    {
        var targetFile = Path.Combine(TargetDirectory ?? "", Path.GetFileName(NewFilePath) ?? "");

        if (IsNewFile && string.IsNullOrWhiteSpace(targetFile))
        {
            StatusMessage = "Select a filename";
            return false;
        }

        if (IsNewFile && File.Exists(targetFile))
        {
            if (!_dialogService.ShowYesNo("Bestand bestaat al. Overschrijven?", "Overschrijven?"))
                return false;
        }

        if (IsExistingFile && string.IsNullOrWhiteSpace(targetFile))
        {
            StatusMessage = "Selecteer een bestaand bestand.";
            return false;
        }

        if (IsExistingFile && !File.Exists(targetFile))
        {
            if (!_dialogService.ShowYesNo("Bestand bestaat niet. Doorgaan?", "Niet gevonden"))
                return false;
        }

        return true;
    }

    private void SetAsDefaultFile(string file)
    {
        if (!File.Exists(file))
            _dialogService.Show($"Bestand {file} bestaat niet of is niet toegankelijk.", "Fout");
        else
            Managers.Config.SetDbFileName(file);
    }

    private void SetupNewAddress()
    {
        var address = Address;

        var defaultTariffGroup = _controller.UnitOfWork.TarifGroupRepo
            .SelectById((long)TariffGroupType.EnergyCosts);

        var generalTariffGroup = _controller.UnitOfWork.TarifGroupRepo
            .SelectById((long)TariffGroupType.GeneralCosts);

        address.DefaultTariffGroupId = defaultTariffGroup?.Id ?? 0;
        address.GeneralTariffGroupId = generalTariffGroup?.Id ?? 0;
        address.DefaultAddress = true;

        _controller.UnitOfWork.AddressRepo.Add(address);
        _controller.UnitOfWork.Complete();
    }

    private void SetupNewMeters()
    {
        var addressId = Address.Id;

        foreach (var et in EnergyTypes.Where(e => e.IsSelected).Select(e => e.EnergyType))
            SetupMeter(et, addressId);
    }

    private void SetupMeter(EnergyType type, long addressId)
    {
        var meter = new Meter
        {
            Description = $"{type.Name} meter",
            Number = $"Meter {type.Name}",
            AddressId = addressId,
            EnergyTypeId = type.Id
        };

        _controller.UnitOfWork.MeterRepo.Add(meter);
        _controller.UnitOfWork.Complete();
    }

    private void SetupCostCategory()
    {
        var categories = GetListOfNewCostCategories();
        int id = 1;

        foreach (var c in categories)
        {
            c.Id = id++;
            _controller.UnitOfWork.CostCategoriesRepo.Add(c);
        }

        _controller.UnitOfWork.Complete();
    }

    private List<CostCategory> GetListOfNewCostCategories()
    {
        // jouw bestaande code blijft hier staan
        return new List<CostCategory>();
    }

    // -----------------------------
    // Options
    // -----------------------------
    private bool _addBaseData = true;
    public bool AddBaseData
    {
        get => _addBaseData;
        set { _addBaseData = value; OnPropertyChanged(); UpdateSummary(); }
    }

    private bool _setAsDefaultFile = true;
    public bool SetAsDefaultFileOption
    {
        get => _setAsDefaultFile;
        set { _setAsDefaultFile = value; OnPropertyChanged(); UpdateSummary(); }
    }

    // -----------------------------
    // Address
    // -----------------------------
    private Address _address;
    public Address Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged();
            UpdateSummary();
        }
    }

    // -----------------------------
    // Energy types
    // -----------------------------
    public ObservableCollection<SelectableEnergyType> EnergyTypes { get; }

    // -----------------------------
    // Summary
    // -----------------------------
    private string _summaryText;
    public string SummaryText
    {
        get => _summaryText;
        set { _summaryText = value; OnPropertyChanged(); }
    }

    private void UpdateSummary()
    {
        var selectedEnergyTypes =
            (EnergyTypes ?? Enumerable.Empty<SelectableEnergyType>())
            .Where(e => e.IsSelected)
            .Select(e => e.EnergyType.Name);

        SummaryText =
            $"Bestand: {NewFilePath}\n" +
            $"Doelmap: {TargetDirectory}\n" +
            $"Adres: {Address?.Street} {Address?.HouseNumber}, {Address?.PostalCode} {Address?.City}\n" +
            $"Zonnepanelen: {(Address?.SolarPanelsAvailable == true ? "Ja" : "Nee")}\n" +
            $"Energietypen: {string.Join(", ", selectedEnergyTypes)}\n" +
            $"Basisgegevens toevoegen: {(AddBaseData ? "Ja" : "Nee")}\n" +
            $"Instellen als standaardbestand: {(SetAsDefaultFileOption ? "Ja" : "Nee")}";
    }

    // -----------------------------
    // Status
    // -----------------------------
    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }
}