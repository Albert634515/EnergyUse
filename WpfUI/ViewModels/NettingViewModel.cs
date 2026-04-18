using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfUI.Managers;

namespace WpfUI.ViewModels;

public class NettingViewModel : ViewModelBase
{
    private readonly Window _window;
    private readonly NettingController _controller;
    private readonly ISettingsService _settings;

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

            if (value > 0)
                _settings.Save("LastNettingEnergyTypeId", value.ToString());

            _ = refreshNetting();
        }
    }

    private EnergyUse.Models.Netting? _selectedNetting;
    public EnergyUse.Models.Netting? SelectedNetting
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

    public NettingViewModel(Window window, ISettingsService settings)
    {
        _window = window;
        _settings = settings;

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
        await setEnergyTypes();
        restoreLastEnergyType();
        await refreshNetting();
    }

    private async Task setEnergyTypes()
    {
        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyUse.Models.EnergyType>(list);
        OnPropertyChanged(nameof(EnergyTypes));
    }

    private void restoreLastEnergyType()
    {
        var last = _settings.Get("LastNettingEnergyTypeId");

        if (long.TryParse(last, out long id))
        {
            if (EnergyTypes.Any(e => e.Id == id))
            {
                SelectedEnergyTypeId = id;
                return;
            }
        }

        // fallback: eerste type
        SelectedEnergyTypeId = EnergyTypes.FirstOrDefault()?.Id ?? 0;
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

        // automatisch eerste record selecteren
        SelectedNetting = Nettings.FirstOrDefault();
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
