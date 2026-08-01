using EnergyUse.Core.Controllers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfUI.Managers;

namespace WpfUI.ViewModels
{
    public class PredefinedPeriodsViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly PreDefinedPeriodController _controller;

        public ObservableCollection<EnergyUse.Models.PreDefinedPeriod> PredefinedPeriods { get; set; } = new();

        private EnergyUse.Models.PreDefinedPeriod? _selectedPeriod;
        public EnergyUse.Models.PreDefinedPeriod? SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                _selectedPeriod = value;
                OnPropertyChanged();

                DatePredefinedVM.CurrentPeriodId = value?.Id ?? 0;
            }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public DatePredefinedViewModel DatePredefinedVM { get; }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand CloseCommand { get; }

        public PredefinedPeriodsViewModel(Window window)
        {
            _window = window;

            _controller = new PreDefinedPeriodController(Config.GetDbFileName());
            _controller.Initialize();

            DatePredefinedVM = new DatePredefinedViewModel(_controller);
            DatePredefinedVM.StatusCallback = msg => StatusMessage = msg;

            AddCommand = new RelayCommand(_ => addPeriod());
            SaveCommand = new RelayCommand(_ => setPeriod());
            CancelCommand = new RelayCommand(_ => cancelPeriod());
            DeleteCommand = new RelayCommand(_ => deletePeriod());
            RefreshCommand = new RelayCommand(_ => refreshPeriods());
            CloseCommand = new RelayCommand(_ => _window.Close());

            setPeriods();
        }

        private async void setPeriods()
        {
            var list = (await _controller.UnitOfWork.PreDefinedPeriodRepo.GetAllWithDates())
                .OrderByDescending(period => period.EndDate)
                .ThenByDescending(period => period.StartDate)
                .ThenByDescending(period => period.Id)
                .ToList();
            PredefinedPeriods = new ObservableCollection<EnergyUse.Models.PreDefinedPeriod>(list);
            OnPropertyChanged(nameof(PredefinedPeriods));

            if (PredefinedPeriods.Any())
                SelectedPeriod = PredefinedPeriods.FirstOrDefault();
        }

        private void addPeriod()
        {
            var entity = _controller.UnitOfWork.AddDefaultEntity("New period");
            PredefinedPeriods.Insert(0, entity);
            SelectedPeriod = entity;
        }

        private void setPeriod()
        {
            DatePredefinedVM.SaveDates();
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully";
            setPeriods();
        }

        private void cancelPeriod()
        {
            _controller.UnitOfWork.CancelChanges();
            StatusMessage = "Cancelled successfully";
            setPeriods();
        }

        private void deletePeriod()
        {
            if (SelectedPeriod != null)
            {
                if (SelectedPeriod.Id > 0)
                    DatePredefinedVM.DeleteByPeriodId(SelectedPeriod.Id);

                _controller.UnitOfWork.Delete(SelectedPeriod);
                StatusMessage = "Period deleted";
                setPeriods();
            }
        }

        private void refreshPeriods()
        {
            setPeriods();
            StatusMessage = "Refreshed";
        }
    }
}
