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
                SelectedPeriodId = value?.Id ?? 0;

                DatePredefinedVM.SetDates(SelectedPeriodId);

                OnPropertyChanged();
            }
        }

        private long _selectedPeriodId;
        public long SelectedPeriodId
        {
            get => _selectedPeriodId;
            set { _selectedPeriodId = value; OnPropertyChanged(); }
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
            SaveCommand = new RelayCommand(_ => savePeriod());
            CancelCommand = new RelayCommand(_ => cancelPeriod());
            DeleteCommand = new RelayCommand(_ => deletePeriod());
            RefreshCommand = new RelayCommand(_ => refreshPeriods());
            CloseCommand = new RelayCommand(_ => _window.Close());

            getPeriods();
        }

        private void getPeriods()
        {
            var list = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll().ToList();
            PredefinedPeriods = new ObservableCollection<EnergyUse.Models.PreDefinedPeriod>(list);
            OnPropertyChanged(nameof(PredefinedPeriods));

            if (PredefinedPeriods.Any())
                SelectedPeriod = PredefinedPeriods.LastOrDefault();
        }

        private void addPeriod()
        {
            var entity = _controller.UnitOfWork.AddDefaultEntity("New period");
            getPeriods();
            SelectedPeriod = entity;
        }

        private void savePeriod()
        {
            DatePredefinedVM.SaveDates();
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully";
            getPeriods();
        }

        private void cancelPeriod()
        {
            _controller.UnitOfWork.CancelChanges();
            DatePredefinedVM.SetDates(SelectedPeriodId);
            StatusMessage = "Cancelled successfully";
            getPeriods();
        }

        private void deletePeriod()
        {
            if (SelectedPeriod != null)
            {
                if (SelectedPeriod.Id > 0)
                    DatePredefinedVM.DeleteByPeriodId(SelectedPeriod.Id);

                _controller.UnitOfWork.Delete(SelectedPeriod);
                StatusMessage = "Period deleted";
                getPeriods();
            }
        }

        private void refreshPeriods()
        {
            getPeriods();            
            DatePredefinedVM.SetDates(SelectedPeriodId);
            StatusMessage = "Refreshed";
        }
    }
}