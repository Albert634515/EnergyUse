using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfUI.ViewModels
{
    public class DatePredefinedViewModel : ViewModelBase
    {
        private readonly PreDefinedPeriodController _controller;

        public ObservableCollection<PreDefinedPeriodDate> Dates { get; private set; } = new();
        public ObservableCollection<EnergyType> EnergyTypes { get; private set; } = new();
        public ObservableCollection<TariffGroup> TariffGroups { get; private set; } = new();

        private PreDefinedPeriodDate? _selectedDate;
        public PreDefinedPeriodDate? SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private long _currentPeriodId;
        public long CurrentPeriodId
        {
            get => _currentPeriodId;
            set
            {
                _currentPeriodId = value;
                OnPropertyChanged();
                SetDates(value);
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public DatePredefinedViewModel(PreDefinedPeriodController controller)
        {
            _controller = controller;

            AddCommand = new RelayCommand(_ => addDate());
            SaveCommand = new RelayCommand(_ => SaveDates());
            CancelCommand = new RelayCommand(_ => CancelDates());
            DeleteCommand = new RelayCommand(_ => deleteDate());
            RefreshCommand = new RelayCommand(_ => SetDates(_currentPeriodId));

            setTariffGroups();
            _ = setEnergyTypesAsync();
        }

        public async void SetDates(long periodId)
        {
            _currentPeriodId = periodId;

            if (periodId <= 0)
            {
                Dates.Clear();
                return;
            }

            var list = (await _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.GetByPeriodId(periodId))
                .OrderByDescending(date => date.EndDate)
                .ThenByDescending(date => date.StartDate)
                .ThenByDescending(date => date.Id)
                .ToList();

            Dates = new ObservableCollection<PreDefinedPeriodDate>(list);
            OnPropertyChanged(nameof(Dates));

            if (Dates.Any())
                SelectedDate = Dates.FirstOrDefault();
        }

        private void addDate()
        {
            if (_currentPeriodId <= 0)
                return;

            var previousDate = Dates
                .OrderByDescending(date => date.EndDate)
                .ThenByDescending(date => date.StartDate)
                .FirstOrDefault();

            var entity = new PreDefinedPeriodDate
            {
                PreDefinedPeriodId = _currentPeriodId,
                StartDate = previousDate?.StartDate.AddYears(1) ?? DateTime.Now.Date,
                EndDate = previousDate?.EndDate.AddYears(1) ?? DateTime.Now.Date.AddYears(1),
                EnergyTypeId = previousDate?.EnergyTypeId,
                TariffGroupId = previousDate?.TariffGroupId,
                EnergyType = previousDate?.EnergyType,
                TariffGroup = previousDate?.TariffGroup
            };

            _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.Add(entity);
            Dates.Insert(0, entity);
            SelectedDate = entity;
        }

        public void SaveDates()
        {
            _controller.UnitOfWorkPd.Complete();
            StatusCallback?.Invoke("Dates saved");
        }

        public void CancelDates()
        {
            _controller.UnitOfWorkPd.CancelChanges();
            SetDates(_currentPeriodId);
            StatusCallback?.Invoke("Changes cancelled");
        }

        private void deleteDate()
        {
            if (SelectedDate == null)
                return;

            if (SelectedDate.Id > 0)
                _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.Remove(SelectedDate);

            Dates.Remove(SelectedDate);
            StatusCallback?.Invoke("Date deleted");
        }

        public async void DeleteByPeriodId(long periodId)
        {
            var datesToDelete = (await _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo
                .GetByPeriodId(periodId))
                .ToList();

            if (datesToDelete.Count == 0)
                return;

            foreach (var date in datesToDelete)
            {
                _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.Remove(date);
            }

            _controller.UnitOfWorkPd.Complete();

            SetDates(periodId);
            StatusCallback?.Invoke("Dates deleted");
        }

        private async void setTariffGroups()
        {
            var list = (await _controller.UnitOfWorkPd.TarifGroupRepo.GetAll()).ToList();
            TariffGroups = new ObservableCollection<TariffGroup>(list);
            OnPropertyChanged(nameof(TariffGroups));
        }

        private async Task setEnergyTypesAsync()
        {
            var list = await _controller.UnitOfWorkPd.EnergyTypeRepo.GetAll();
            EnergyTypes = new ObservableCollection<EnergyType>(list.ToList());
            OnPropertyChanged(nameof(EnergyTypes));
        }

        public Action<string>? StatusCallback { get; set; }
    }
}
