using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EnergyUse.Core.Controllers;
using WpfUI.Managers;

namespace WpfUI.ViewModels;

public class MetersViewModel : ViewModelBase
{
    private readonly Window _window;
    private readonly MeterController _controller;

    public ObservableCollection<EnergyUse.Models.Meter> Meters { get; set; } = new();
    public ObservableCollection<EnergyUse.Models.EnergyType> EnergyTypes { get; set; } = new();
    public ObservableCollection<EnergyUse.Models.Address> Addresses { get; set; } = new();

    private EnergyUse.Models.Meter _selectedMeter;
    public EnergyUse.Models.Meter SelectedMeter
    {
        get => _selectedMeter;
        set { _selectedMeter = value; OnPropertyChanged(); }
    }

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public MetersViewModel(Window window)
    {
        _window = window;

        _controller = new MeterController(Config.GetDbFileName());
        _controller.Initialize();

        AddCommand = new RelayCommand(_ => addMeter());
        SaveCommand = new RelayCommand(_ => saveMeter());
        CancelCommand = new RelayCommand(_ => cancelMeter());
        DeleteCommand = new RelayCommand(_ => deleteMeter());
        RefreshCommand = new RelayCommand(async _ => await getMeters());
        CloseCommand = new RelayCommand(_ => _window.Close());

        _ = initializeAsync();
    }

    private async Task initializeAsync()
    {
        await getCombosAsync();
        await getMeters();
    }

    private async Task getCombosAsync()
    {
        var energyTypes = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyUse.Models.EnergyType>(energyTypes);
        OnPropertyChanged(nameof(EnergyTypes));

        var addresses = await _controller.UnitOfWork.AddressRepo.GetAll();
        Addresses = new ObservableCollection<EnergyUse.Models.Address>(addresses);
        OnPropertyChanged(nameof(Addresses));
    }

    public async Task getMeters()
    {
        var list = await _controller.UnitOfWork.MeterRepo.GetAll();
        Meters = new ObservableCollection<EnergyUse.Models.Meter>(list);
        OnPropertyChanged(nameof(Meters));
    }

    private void addMeter()
    {
        var entity = _controller.UnitOfWork.AddDefaultEntity("New meter");
        _ = getMeters();
        SelectedMeter = entity;
    }

    private void saveMeter()
    {
        if (SelectedMeter != null)
            _controller.UnitOfWork.Complete();

        _ = getMeters();
    }

    private void cancelMeter()
    {
        _controller.UnitOfWork.CancelChanges();
        _ = getMeters();
    }

    private void deleteMeter()
    {
        if (SelectedMeter != null)
        {
            _controller.UnitOfWork.Delete(SelectedMeter);
            _ = getMeters();
        }
    }
}