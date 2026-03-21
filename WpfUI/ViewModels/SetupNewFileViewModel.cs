using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Views.Controls;

namespace WpfUI.ViewModels;

public class SetupNewFileViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private SetupNewFileController? _controller;

    public event Action? RequestClose;

    public SetupNewFileViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        // Commands
        NextCommand = new RelayCommand(_ => NextStep(), _ => CanGoNext);
        PreviousCommand = new RelayCommand(_ => PreviousStep(), _ => CanGoPrevious);
        CreateDbCommand = new RelayCommand(_ => Finish(), _ => CanFinish);
        SelectFileCommand = new RelayCommand(_ => SelectFile());
        SelectDirectoryCommand = new RelayCommand(_ => SelectDirectory());

        // Load demo address
        Address = EnergyUse.Core.Manager.LibBaseData.GetDemoAddress(1);

        // Load energy types
        EnergyTypes = new ObservableCollection<SelectableEnergyTypeViewModel>();
        foreach (var t in EnergyUse.Core.Manager.LibBaseData.GetDefaultEnergyTypes(true, false))
            EnergyTypes.Add(new SelectableEnergyTypeViewModel(t));

        // Initial step
        CurrentStep = WizardStep.FileSelection;
        LoadPageForStep();

        UpdateStepVisibility();
        UpdateStepIndicator();
        UpdateNavigationVisibility();
        UpdateSummary();
    }

    // ---------------------------------------------------------
    // Wizard steps
    // ---------------------------------------------------------
    public enum WizardStep
    {
        FileSelection = 1,
        Address = 2,
        EnergyTypes = 3,
        Summary = 4
    }

    private WizardStep _currentStep;
    public WizardStep CurrentStep
    {
        get => _currentStep;
        set
        {
            _currentStep = value;
            OnPropertyChanged();
            LoadPageForStep();
            UpdateStepIndicator();
            UpdateNavigationVisibility();
        }
    }

    // ---------------------------------------------------------
    // Dynamic step visibility
    // ---------------------------------------------------------
    public Visibility Step2Visible => IsNewFile ? Visibility.Visible : Visibility.Collapsed;
    public Visibility Step3Visible => IsNewFile ? Visibility.Visible : Visibility.Collapsed;

    private void UpdateStepVisibility()
    {
        OnPropertyChanged(nameof(Step2Visible));
        OnPropertyChanged(nameof(Step3Visible));
    }

    // ---------------------------------------------------------
    // Step indicator colors
    // ---------------------------------------------------------
    public Brush Step1Background => CurrentStep == WizardStep.FileSelection ? Brushes.SteelBlue : Brushes.LightGray;
    public Brush Step2Background => CurrentStep == WizardStep.Address ? Brushes.SteelBlue : Brushes.LightGray;
    public Brush Step3Background => CurrentStep == WizardStep.EnergyTypes ? Brushes.SteelBlue : Brushes.LightGray;
    public Brush Step4Background => CurrentStep == WizardStep.Summary ? Brushes.SteelBlue : Brushes.LightGray;

    public Brush Step1Foreground => Brushes.White;
    public Brush Step2Foreground => Brushes.White;
    public Brush Step3Foreground => Brushes.White;
    public Brush Step4Foreground => Brushes.White;

    private void UpdateStepIndicator()
    {
        OnPropertyChanged(nameof(Step1Background));
        OnPropertyChanged(nameof(Step2Background));
        OnPropertyChanged(nameof(Step3Background));
        OnPropertyChanged(nameof(Step4Background));

        OnPropertyChanged(nameof(Step1Foreground));
        OnPropertyChanged(nameof(Step2Foreground));
        OnPropertyChanged(nameof(Step3Foreground));
        OnPropertyChanged(nameof(Step4Foreground));
    }

    // ---------------------------------------------------------
    // Wizard navigation
    // ---------------------------------------------------------
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand CreateDbCommand { get; }

    public bool CanGoNext => CurrentStep != WizardStep.Summary;
    public bool CanGoPrevious => CurrentStep != WizardStep.FileSelection;
    public bool CanFinish => CurrentStep == WizardStep.Summary;

    public Visibility PreviousVisible => CanGoPrevious ? Visibility.Visible : Visibility.Collapsed;
    public Visibility NextVisible => CanGoNext && !CanFinish ? Visibility.Visible : Visibility.Collapsed;
    public Visibility FinishVisible => CanFinish ? Visibility.Visible : Visibility.Collapsed;

    private void UpdateNavigationVisibility()
    {
        OnPropertyChanged(nameof(PreviousVisible));
        OnPropertyChanged(nameof(NextVisible));
        OnPropertyChanged(nameof(FinishVisible));
    }

    private void NextStep()
    {
        if (CurrentStep == WizardStep.FileSelection)
        {
            if (IsExistingFile)
            {
                CurrentStep = WizardStep.Summary;
                return;
            }

            CurrentStep = WizardStep.Address;
            return;
        }

        if (CurrentStep == WizardStep.Address)
        {
            CurrentStep = WizardStep.EnergyTypes;
            return;
        }

        if (CurrentStep == WizardStep.EnergyTypes)
        {
            CurrentStep = WizardStep.Summary;
            return;
        }
    }

    private void PreviousStep()
    {
        if (CurrentStep == WizardStep.Summary)
        {
            if (IsExistingFile)
            {
                CurrentStep = WizardStep.FileSelection;
                return;
            }

            CurrentStep = WizardStep.EnergyTypes;
            return;
        }

        if (CurrentStep == WizardStep.EnergyTypes)
        {
            CurrentStep = WizardStep.Address;
            return;
        }

        if (CurrentStep == WizardStep.Address)
        {
            CurrentStep = WizardStep.FileSelection;
            return;
        }
    }

    // ---------------------------------------------------------
    // Page loading
    // ---------------------------------------------------------
    private object? _currentPage;
    public object? CurrentPage
    {
        get => _currentPage;
        set { _currentPage = value; OnPropertyChanged(); }
    }

    private void LoadPageForStep()
    {
        CurrentPage = CurrentStep switch
        {
            WizardStep.FileSelection => new WizardPage1_FileSelection(),
            WizardStep.Address => new WizardPage2_Address(),
            WizardStep.EnergyTypes => new WizardPage3_EnergyTypes(),
            WizardStep.Summary => new WizardPage4_Summary(),
            _ => null
        };
    }

    // ---------------------------------------------------------
    // File selection
    // ---------------------------------------------------------
    public ICommand SelectFileCommand { get; }
    public ICommand SelectDirectoryCommand { get; }

    private string _newFilePath = string.Empty;
    public string NewFilePath
    {
        get => _newFilePath;
        set { _newFilePath = value; OnPropertyChanged(); UpdateSummary(); }
    }

    private string _targetDirectory = string.Empty;
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
            UpdateStepVisibility();
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
            UpdateStepVisibility();
            UpdateSummary();
        }
    }

    private void SelectFile()
    {
        var file = _dialogService.SaveFile("Database file (*.db)|*.db", "Kies databasebestand");
        if (!string.IsNullOrEmpty(file))
            NewFilePath = file;
    }

    private void SelectDirectory()
    {
        var folder = _dialogService.OpenFolder();
        if (!string.IsNullOrEmpty(folder))
            TargetDirectory = folder;
    }

    // ---------------------------------------------------------
    // Options
    // ---------------------------------------------------------
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

    // ---------------------------------------------------------
    // Address
    // ---------------------------------------------------------
    private Address? _address;
    public Address? Address
    {
        get => _address;
        set { _address = value; OnPropertyChanged(); UpdateSummary(); }
    }

    // ---------------------------------------------------------
    // Energy types
    // ---------------------------------------------------------
    public ObservableCollection<SelectableEnergyTypeViewModel> EnergyTypes { get; }

    // ---------------------------------------------------------
    // Summary
    // ---------------------------------------------------------
    private string _summaryText = string.Empty;
    public string SummaryText
    {
        get => _summaryText;
        set { _summaryText = value; OnPropertyChanged(); }
    }

    private void UpdateSummary()
    {
        var selectedEnergyTypes =
            (EnergyTypes ?? Enumerable.Empty<SelectableEnergyTypeViewModel>())
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

    // ---------------------------------------------------------
    // Status
    // ---------------------------------------------------------
    private string _statusMessage = string.Empty;
    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    // ---------------------------------------------------------
    // Create database (vervolg)
    // ---------------------------------------------------------
    private void Finish()
    {
        if (!CreateDatabase())
            return;

        RequestClose?.Invoke();
    }

    private bool CreateDatabase()
    {
        var targetFile = Path.Combine(TargetDirectory ?? "", Path.GetFileName(NewFilePath) ?? "");
        var currentFile = Managers.Config.GetDbFileName()?.Trim();

        if (!ValidateNewSetup())
            return false;

        try
        {
            var folder = Path.GetDirectoryName(targetFile);
            if (folder == null)
                return false;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (IsNewFile)
            {
                Managers.Config.SetDbFileName(targetFile);
                _controller = new SetupNewFileController(targetFile);
                _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(targetFile);

                setAsDefaultFile(targetFile);

                setupNewAddress();
                setupNewMeters();

                if (AddBaseData)
                    setupCostCategory();
            }

            if (!IsNewFile)
                setAsDefaultFile(targetFile);
            else if (!SetAsDefaultFileOption && !string.IsNullOrWhiteSpace(currentFile))
                setAsDefaultFile(currentFile);
            else
                setAsDefaultFile(targetFile);

            StatusMessage = "Database succesvol aangemaakt.";
            return true;
        }
        catch (Exception ex)
        {
            StatusMessage = "Fout bij aanmaken database: " + ex.Message;
            return false;
        }
    }

    private bool ValidateNewSetup()
    {
        var targetFile = Path.Combine(TargetDirectory ?? "", Path.GetFileName(NewFilePath) ?? "");

        if (IsNewFile && string.IsNullOrWhiteSpace(targetFile))
        {
            StatusMessage = "Selecteer een bestandsnaam.";
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

    private void setAsDefaultFile(string file)
    {
        if (!File.Exists(file))
            _dialogService.Show($"Bestand {file} bestaat niet of is niet toegankelijk.", "Fout");
        else
            Managers.Config.SetDbFileName(file);
    }

    private void setupNewAddress()
    {
        if (_controller == null || Address == null)
            return;

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

    private void setupNewMeters()
    {
        if (Address == null)
            return;

        var addressId = Address.Id;

        foreach (var et in EnergyTypes.Where(e => e.IsSelected).Select(e => e.EnergyType))
            setupMeter(et, addressId);
    }

    private void setupMeter(EnergyType type, long addressId)
    {
        if (_controller == null)
            return;

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

    // ---------------------------------------------------------
    // Cost categories (volledig)
    // ---------------------------------------------------------
    private void setupCostCategory()
    {
        if (_controller == null)
            return;

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
        if (_controller == null)
            return new List<CostCategory>();

        var costCategories = new List<CostCategory>();

        var calTypePU = _controller.UnitOfWork.CalculationTypeRepo.SelectByDescription("Per Unit");
        var calTypePd = _controller.UnitOfWork.CalculationTypeRepo.SelectByDescription("Per Day");
        var energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Normal");
        var energySubTypeOther = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Other");
        var tarifGroupDefault = _controller.UnitOfWork.TarifGroupRepo.SelectById((long)TariffGroupType.EnergyCosts);
        var tarifGroupGeneral = _controller.UnitOfWork.TarifGroupRepo.SelectById((long)TariffGroupType.GeneralCosts);

        // ELECTRICITY
        var electricity = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Electricity");
        if (electricity != null)
        {
            costCategories.Add(new CostCategory
            {
                Name = "Energy rate normal",
                SortOrder = 1,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = energySubType.Id,
                UnitId = "kWh",
                TariffGroupId = tarifGroupDefault.Id
            });

            var low = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Low");
            costCategories.Add(new CostCategory
            {
                Name = "Energy rate low",
                SortOrder = 2,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = low.Id,
                UnitId = "kWh",
                TariffGroupId = tarifGroupDefault.Id
            });

            var retNorm = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("ReturnNormal");
            costCategories.Add(new CostCategory
            {
                Name = "Return delivery normal rate",
                SortOrder = 3,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = retNorm.Id,
                UnitId = "kWh",
                TariffGroupId = tarifGroupDefault.Id
            });

            var retLow = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("ReturnLow");
            costCategories.Add(new CostCategory
            {
                Name = "Return delivery low rate",
                SortOrder = 4,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = retLow.Id,
                UnitId = "kWh",
                TariffGroupId = tarifGroupDefault.Id
            });

            costCategories.Add(new CostCategory
            {
                Name = "Delivery costs",
                SortOrder = 7,
                CalculationTypeId = calTypePd.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "Day",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Network costs",
                SortOrder = 8,
                CalculationTypeId = calTypePd.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "Day",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Reduction energy tax",
                SortOrder = 9,
                CalculationTypeId = calTypePd.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "Day",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Energy tax",
                SortOrder = 5,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = electricity.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "kWh",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true,
                CanNotBeNegative = true,
                NotCalculateOverReturn = true
            });
        }

        // GAS
        var gas = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Gas");
        if (gas != null)
        {
            costCategories.Add(new CostCategory
            {
                Name = "Energy rate gas",
                SortOrder = 2,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = gas.Id,
                EnergySubTypeId = energySubType.Id,
                UnitId = "m3",
                TariffGroupId = tarifGroupDefault.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Fixed delivery costs",
                SortOrder = 4,
                CalculationTypeId = calTypePd.Id,
                EnergyTypeId = gas.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "Day",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Network costs",
                SortOrder = 5,
                CalculationTypeId = calTypePd.Id,
                EnergyTypeId = gas.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "Day",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });

            costCategories.Add(new CostCategory
            {
                Name = "Energy tax",
                SortOrder = 3,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = gas.Id,
                EnergySubTypeId = energySubTypeOther.Id,
                UnitId = "m3",
                TariffGroupId = tarifGroupGeneral.Id,
                CalculateVat = true
            });
        }

        // WATER
        var water = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Water");
        if (water != null)
        {
            costCategories.Add(new CostCategory
            {
                Name = "Water rate",
                SortOrder = 3,
                CalculationTypeId = calTypePU.Id,
                EnergyTypeId = water.Id,
                EnergySubTypeId = energySubType.Id,
                UnitId = "m3",
                TariffGroupId = tarifGroupDefault.Id,
                CalculateVat = true
            });
        }

        return costCategories;
    }
}