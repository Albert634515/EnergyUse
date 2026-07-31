using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

            setCorrectionFactors();
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

        AddCommand = new RelayCommand(_ => add());
        SaveCommand = new RelayCommand(_ => save());
        CancelCommand = new RelayCommand(_ => cancel());
        DeleteCommand = new RelayCommand(_ => delete());
        RefreshCommand = new RelayCommand(_ => refresh());
        CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());

        _ = setEnergyTypesAsync();
    }

    private async Task setEnergyTypesAsync()
    {
        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyType>(list);
        OnPropertyChanged(nameof(EnergyTypes));

        setLastEnergyType();
    }

    private void setLastEnergyType()
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

    private async void setCorrectionFactors()
    {
        if (SelectedEnergyType == null) return;

        var list = (await _controller.UnitOfWork.CorrectionFactorRepo
                                         .SelectByEnergyType(SelectedEnergyType.Id))
                                         .ToList();

        CorrectionFactors = new ObservableCollection<CorrectionFactor>(list);
        OnPropertyChanged(nameof(CorrectionFactors));

        // automatisch eerste record selecteren
        SelectedCorrectionFactor = CorrectionFactors.FirstOrDefault();
    }

    private async void add()
    {
        if (SelectedEnergyType == null) return;

        var entity = await _controller.UnitOfWork.AddDefaultEntity(SelectedEnergyType.Id);
        CorrectionFactors.Add(entity);
        SelectedCorrectionFactor = entity;
    }

    private void save()
    {
        _controller.UnitOfWork.Complete();
        StatusMessage = "Saved.";
        OnPropertyChanged(nameof(StatusMessage));
    }

    private void cancel()
    {
        _controller.UnitOfWork.CancelChanges();
        setCorrectionFactors();
    }

    private void delete()
    {
        if (SelectedCorrectionFactor == null) return;

        _controller.UnitOfWork.Delete(SelectedCorrectionFactor);
        CorrectionFactors.Remove(SelectedCorrectionFactor);
    }

    private void refresh() => setCorrectionFactors();
}
