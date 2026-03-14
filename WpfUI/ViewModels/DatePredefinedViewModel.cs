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

        public void SetDates(long periodId)
        {
            _currentPeriodId = periodId;

            if (periodId <= 0)
            {
                Dates.Clear();
                return;
            }

            var list = _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.GetByPeriodId(periodId).ToList();

            Dates = new ObservableCollection<PreDefinedPeriodDate>(list);
            OnPropertyChanged(nameof(Dates));

            if (Dates.Any())
                SelectedDate = Dates.FirstOrDefault();
        }

        private void addDate()
        {
            if (_currentPeriodId <= 0)
                return;

            var entity = new PreDefinedPeriodDate
            {
                PreDefinedPeriodId = _currentPeriodId,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddYears(1)
            };

            _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo.Add(entity);
            Dates.Add(entity);
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

        public void DeleteByPeriodId(long periodId)
        {
            var datesToDelete = _controller.UnitOfWorkPd.PreDefinedPeriodDateRepo
                .GetByPeriodId(periodId)
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

        private void setTariffGroups()
        {
            var list = _controller.UnitOfWorkPd.TarifGroupRepo.GetAll().ToList();
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