using EnergyUse.Core.Controllers;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using WpfUI.ViewModels;

public class ExportViewModel : ViewModelBase
{
    private readonly ExportController _controller;

    public ExportViewModel()
    {
        _controller = new ExportController(WpfUI.Managers.Config.GetDbFileName());
        _controller.Initialize();

        CloseCommand = new RelayCommand(_ => CloseWindow());
        SelectExportFileCommand = new RelayCommand(_ => SelectExportFile());
        ExportCommand = new RelayCommand(_ => Export());

        LoadAddresses();
        LoadDefaultExportFile();
    }

    // -------------------------------------------------------
    // Properties
    // -------------------------------------------------------

    private ObservableCollection<EnergyUse.Models.Address> _addresses = new();
    public ObservableCollection<EnergyUse.Models.Address> Addresses
    {
        get => _addresses;
        set => SetProperty(ref _addresses, value);
    }

    private ObservableCollection<EnergyUse.Models.EnergyType> _energyTypes = new();
    public ObservableCollection<EnergyUse.Models.EnergyType> EnergyTypes
    {
        get => _energyTypes;
        set => SetProperty(ref _energyTypes, value);
    }

    private EnergyUse.Models.Address? _selectedAddress;
    public EnergyUse.Models.Address? SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            if (SetProperty(ref _selectedAddress, value))
            {
                LoadEnergyTypes();
                LoadDefaultExportFile();
            }
        }
    }

    private EnergyUse.Models.EnergyType? _selectedEnergyType;
    public EnergyUse.Models.EnergyType? SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            if (SetProperty(ref _selectedEnergyType, value))
                LoadDefaultExportFile();
        }
    }

    private string _exportFileName = string.Empty;
    public string ExportFileName
    {
        get => _exportFileName;
        set => SetProperty(ref _exportFileName, value);
    }

    private bool? _exportAll = true;
    public bool? ExportAll
    {
        get => _exportAll;
        set
        {
            if (SetProperty(ref _exportAll, value))
                OnPropertyChanged(nameof(ExportSelectedRange));
        }
    }

    public bool? ExportSelectedRange
    {
        get => !_exportAll;
        set
        {
            ExportAll = !(value ?? false);
            OnPropertyChanged();
        }
    }

    private DateTime _dateFrom = DateTime.Today;
    public DateTime DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }

    private DateTime _dateTill = DateTime.Today;
    public DateTime DateTill
    {
        get => _dateTill;
        set => SetProperty(ref _dateTill, value);
    }

    private string _statusMessage = string.Empty;
    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    // -------------------------------------------------------
    // Commands
    // -------------------------------------------------------

    public RelayCommand CloseCommand { get; }
    public RelayCommand SelectExportFileCommand { get; }
    public RelayCommand ExportCommand { get; }

    // -------------------------------------------------------
    // Methods
    // -------------------------------------------------------

    private void LoadAddresses()
    {
        var list = _controller.UnitOfWork.AddressRepo.GetAll();
        Addresses = new ObservableCollection<EnergyUse.Models.Address>(list);

        SelectedAddress = list.FirstOrDefault(x => x.DefaultAddress == true);
    }

    private void LoadEnergyTypes()
    {
        EnergyTypes.Clear();

        if (SelectedAddress == null)
            return;

        var list = _controller.UnitOfWork.EnergyTypeRepo
            .SelectByAddressId(SelectedAddress.Id)
            .ToList();

        EnergyTypes = new ObservableCollection<EnergyUse.Models.EnergyType>(list);

        SelectedEnergyType = list.FirstOrDefault(x => x.DefaultType);
    }

    private void LoadDefaultExportFile()
    {
        string dir = GetInitialExportDirectory();
        string file = GetInitialExportFileName();

        ExportFileName = Path.Combine(dir, file);
    }

    private string GetInitialExportDirectory()
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(WpfUI.Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting("ExportDirectory");

        return setting != null && setting.Id > 0
            ? setting.KeyValue
            : string.Empty;
    }

    private string GetInitialExportFileName()
    {
        if (SelectedEnergyType != null)
            return $"ExcelSheet_{DateTime.Now:dd-MM-yyyy}_{SelectedEnergyType.Name}.xlsx";

        return $"ExcelSheet_{DateTime.Now:dd-MM-yyyy}.xlsx";
    }

    private void SelectExportFile()
    {
        var dlg = new SaveFileDialog
        {
            InitialDirectory = GetInitialExportDirectory(),
            Filter = "Excel files|*.xlsx|All files|*.*",
            FileName = GetInitialExportFileName()
        };

        if (dlg.ShowDialog() == true)
            ExportFileName = dlg.FileName;
    }

    private void Export()
    {
        StatusMessage = "Export completed.";
    }

    private void CloseWindow()
    {
        Application.Current.Windows
            .OfType<Window>()
            .SingleOrDefault(w => w.DataContext == this)?
            .Close();
    }
}