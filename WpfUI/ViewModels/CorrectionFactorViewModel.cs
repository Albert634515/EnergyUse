using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfUI.Services;

namespace WpfUI.ViewModels;

public class CorrectionFactorViewModel : ViewModelBase
{
    private readonly CorrectionFactorController _controller;
    private readonly ISettingsService _settings;

    public ObservableCollection<EnergyType> EnergyTypes { get; set; } = new();
    public ObservableCollection<CorrectionFactor> CorrectionFactors { get; set; } = new();

    private EnergyType? _selectedEnergyType;
    public EnergyType? SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            _selectedEnergyType = value;
            OnPropertyChanged();

            if (value != null)
                _settings.Save("LastCorrectionEnergyTypeId", value.Id.ToString());

            loadCorrectionFactors();
        }
    }

    private CorrectionFactor? _selectedCorrectionFactor;
    public CorrectionFactor? SelectedCorrectionFactor
    {
        get => _selectedCorrectionFactor;
        set { _selectedCorrectionFactor = value; OnPropertyChanged(); }
    }

    private string _statusMessage = string.Empty;
    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public event Action? CloseRequested;

    public CorrectionFactorViewModel(ISettingsService settings)
    {
        _settings = settings;

        _controller = new CorrectionFactorController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        AddCommand = new RelayCommand(_ => Add());
        SaveCommand = new RelayCommand(_ => Save());
        CancelCommand = new RelayCommand(_ => Cancel());
        DeleteCommand = new RelayCommand(_ => Delete());
        RefreshCommand = new RelayCommand(_ => Refresh());
        CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());

        _ = loadEnergyTypesAsync();
    }

    private async Task loadEnergyTypesAsync()
    {
        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyType>(list);
        OnPropertyChanged(nameof(EnergyTypes));

        restoreLastEnergyType();
    }

    private void restoreLastEnergyType()
    {
        var last = _settings.Get("LastCorrectionEnergyTypeId");

        if (long.TryParse(last, out long id))
        {
            SelectedEnergyType = EnergyTypes.FirstOrDefault(e => e.Id == id)
                                 ?? EnergyTypes.FirstOrDefault();
        }
        else
        {
            SelectedEnergyType = EnergyTypes.FirstOrDefault();
        }
    }

    private void loadCorrectionFactors()
    {
        if (SelectedEnergyType == null) return;

        var list = _controller.UnitOfWork.CorrectionFactorRepo
                                         .SelectByEnergyType(SelectedEnergyType.Id)
                                         .ToList();

        CorrectionFactors = new ObservableCollection<CorrectionFactor>(list);
        OnPropertyChanged(nameof(CorrectionFactors));

        // automatisch eerste record selecteren
        SelectedCorrectionFactor = CorrectionFactors.FirstOrDefault();
    }

    private void Add()
    {
        if (SelectedEnergyType == null) return;

        var entity = _controller.UnitOfWork.AddDefaultEntity(SelectedEnergyType.Id);
        CorrectionFactors.Add(entity);
        SelectedCorrectionFactor = entity;
    }

    private void Save()
    {
        _controller.UnitOfWork.Complete();
        StatusMessage = "Saved.";
        OnPropertyChanged(nameof(StatusMessage));
    }

    private void Cancel()
    {
        _controller.UnitOfWork.CancelChanges();
        loadCorrectionFactors();
    }

    private void Delete()
    {
        if (SelectedCorrectionFactor == null) return;

        _controller.UnitOfWork.Delete(SelectedCorrectionFactor);
        CorrectionFactors.Remove(SelectedCorrectionFactor);
    }

    private void Refresh() => loadCorrectionFactors();
}
