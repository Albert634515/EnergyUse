using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class CorrectionFactorViewModel : ViewModelBase
{
    private readonly CorrectionFactorController _controller;

    public ObservableCollection<EnergyType> EnergyTypes { get; set; }
    public ObservableCollection<CorrectionFactor> CorrectionFactors { get; set; }

    private EnergyType _selectedEnergyType;
    public EnergyType SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            _selectedEnergyType = value;
            OnPropertyChanged();
            getCorrectionFactors();
        }
    }

    private CorrectionFactor _selectedCorrectionFactor;
    public CorrectionFactor SelectedCorrectionFactor
    {
        get => _selectedCorrectionFactor;
        set { _selectedCorrectionFactor = value; OnPropertyChanged(); }
    }

    public string StatusMessage { get; set; }

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public CorrectionFactorViewModel()
    {
        // 🔥 Belangrijk: voorkom dat de designer database opent
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            return;

        _controller = new CorrectionFactorController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        EnergyTypes = new ObservableCollection<EnergyType>((IEnumerable<EnergyType>)_controller.UnitOfWork.EnergyTypeRepo.GetAll());

        AddCommand = new RelayCommand(_ => Add());
        SaveCommand = new RelayCommand(_ => Save());
        CancelCommand = new RelayCommand(_ => Cancel());
        DeleteCommand = new RelayCommand(_ => Delete());
        RefreshCommand = new RelayCommand(_ => Refresh());
        CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());
    }

    public event Action CloseRequested;

    private void getCorrectionFactors()
    {
        if (SelectedEnergyType == null) return;

        var list = _controller.UnitOfWork.CorrectionFactorRepo
            .SelectByEnergyType(SelectedEnergyType.Id)
            .ToList();

        CorrectionFactors = new ObservableCollection<CorrectionFactor>(list);
        OnPropertyChanged(nameof(CorrectionFactors));
    }

    private void Add()
    {
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
        getCorrectionFactors();
    }

    private void Delete()
    {
        if (SelectedCorrectionFactor == null) return;

        _controller.UnitOfWork.Delete(SelectedCorrectionFactor);
        CorrectionFactors.Remove(SelectedCorrectionFactor);
    }

    private void Refresh() => getCorrectionFactors();
}
