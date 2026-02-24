using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnergyUse.Core.Controllers;
using WpfUI.Managers;

namespace WpfUI.ViewModels;

public class NettingViewModel : ViewModelBase
{
    private readonly Window _window;
    private readonly NettingController _controller;

    public ObservableCollection<EnergyUse.Models.Netting> Nettings { get; set; } = new();
    public ObservableCollection<EnergyUse.Models.EnergyType> EnergyTypes { get; set; } = new();

    private long _selectedEnergyTypeId;
    public long SelectedEnergyTypeId
    {
        get => _selectedEnergyTypeId;
        set
        {
            _selectedEnergyTypeId = value;
            OnPropertyChanged();
            _ = refreshNetting();
        }
    }

    private EnergyUse.Models.Netting _selectedNetting;
    public EnergyUse.Models.Netting SelectedNetting
    {
        get => _selectedNetting;
        set { _selectedNetting = value; OnPropertyChanged(); }
    }

    // Commands
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public NettingViewModel(Window window)
    {
        _window = window;

        _controller = new NettingController(Config.GetDbFileName());
        _controller.Initialize();

        AddCommand = new RelayCommand(_ => addNetting());
        SaveCommand = new RelayCommand(_ => saveNetting());
        CancelCommand = new RelayCommand(_ => cancelNetting());
        DeleteCommand = new RelayCommand(_ => deleteNetting());
        RefreshCommand = new RelayCommand(async _ => await refreshNetting());
        CloseCommand = new RelayCommand(_ => _window.Close());

        _ = initializeAsync();
    }

    private async Task initializeAsync()
    {
        await loadEnergyTypes();
        await refreshNetting();
    }

    private async Task loadEnergyTypes()
    {
        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyUse.Models.EnergyType>(list);
        OnPropertyChanged(nameof(EnergyTypes));
    }

    private async Task refreshNetting()
    {
        if (SelectedEnergyTypeId == 0)
        {
            Nettings = new ObservableCollection<EnergyUse.Models.Netting>();
        }
        else
        {
            var list = await _controller.UnitOfWork.NettingRepo.SelectByEnergyType(SelectedEnergyTypeId);
            Nettings = new ObservableCollection<EnergyUse.Models.Netting>(list);
        }

        OnPropertyChanged(nameof(Nettings));
    }

    private void addNetting()
    {
        if (SelectedEnergyTypeId == 0)
            return;

        var entity = _controller.UnitOfWork.AddDefaultEntity(SelectedEnergyTypeId);
        _ = refreshNetting();
        SelectedNetting = entity;
    }

    private void saveNetting()
    {
        _controller.UnitOfWork.Complete();
        _ = refreshNetting();
    }

    private void cancelNetting()
    {
        _controller.UnitOfWork.CancelChanges();
        _ = refreshNetting();
    }

    private void deleteNetting()
    {
        if (SelectedNetting != null)
        {
            _controller.UnitOfWork.Delete(SelectedNetting);
            _ = refreshNetting();
        }
    }
}