using EnergyUse.Core.UnitOfWork;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfUI.Managers;

namespace WpfUI.ViewModels;

public class DatePredefinedViewModel : ViewModelBase
{
    private readonly PredefinedPeriodDate _unitOfWork;

    public ObservableCollection<EnergyUse.Models.PreDefinedPeriodDate> Dates { get; set; } = new();
    public ObservableCollection<EnergyUse.Models.EnergyType> EnergyTypes { get; set; } = new();
    public ObservableCollection<EnergyUse.Models.TariffGroup> TariffGroups { get; set; } = new();

    private EnergyUse.Models.PreDefinedPeriodDate _selectedDate;
    public EnergyUse.Models.PreDefinedPeriodDate SelectedDate
    {
        get => _selectedDate;
        set { _selectedDate = value; OnPropertyChanged(); }
    }

    private long _currentPeriodId;

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }

    public DatePredefinedViewModel()
    {
        _unitOfWork = new PredefinedPeriodDate(Config.GetDbFileName());

        AddCommand = new RelayCommand(_ => addDate());
        SaveCommand = new RelayCommand(_ => saveDate());
        CancelCommand = new RelayCommand(_ => cancelDate());
        DeleteCommand = new RelayCommand(_ => deleteDate());
        RefreshCommand = new RelayCommand(_ => LoadDates(_currentPeriodId));

        getTarifGroups();
        _= InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        getTarifGroups();                 // sync
        await getDataEnergyTypes();   // async
    }


    private void getTarifGroups()
    {
        var list = new ObservableCollection<EnergyUse.Models.TariffGroup>(_unitOfWork.TarifGroupRepo.GetAll().ToList());
        TariffGroups = list;

        OnPropertyChanged(nameof(TariffGroups));
    }

    private async Task getDataEnergyTypes()
    {
        var list = await _unitOfWork.EnergyTypeRepo.GetAll();
        EnergyTypes = new ObservableCollection<EnergyUse.Models.EnergyType>(list.ToList());

        OnPropertyChanged(nameof(EnergyTypes));
    }

    public void LoadDates(long periodId)
    {
        _currentPeriodId = periodId;

        if (periodId <= 0)
        {
            Dates.Clear();
            return;
        }

        var list = _unitOfWork.PreDefinedPeriodDateRepo.GetByPeriodId(periodId).ToList();
        Dates = new ObservableCollection<PreDefinedPeriodDate>(list);
        OnPropertyChanged(nameof(Dates));
    }

    private void addDate()
    {
        if (_currentPeriodId <= 0)
            return;

        var entity = new EnergyUse.Models.PreDefinedPeriodDate
        {
            PreDefinedPeriodId = _currentPeriodId,
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.AddYears(1)
        };

        _unitOfWork.PreDefinedPeriodDateRepo.Add(entity);
        Dates.Add(entity);
        SelectedDate = entity;
    }

    private void saveDate()
    {
        _unitOfWork.Complete();
    }

    private void cancelDate()
    {
        _unitOfWork.CancelChanges();
        LoadDates(_currentPeriodId);
    }

    private void deleteDate()
    {
        if (SelectedDate == null)
            return;

        if (SelectedDate.Id > 0)
            _unitOfWork.PreDefinedPeriodDateRepo.Remove(SelectedDate);

        Dates.Remove(SelectedDate);
    }
}