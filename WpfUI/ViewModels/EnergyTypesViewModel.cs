using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Extensions;

namespace WpfUI.ViewModels
{
    public class EnergyTypesViewModel : ViewModelBase
    {
        private readonly EnergyTypesController _controller;

        public ObservableCollection<EnergyType> EnergyTypes { get; private set; } = new ObservableCollection<EnergyType>();
        public ObservableCollection<Unit> Units { get; } = new ObservableCollection<Unit>();

        private EnergyType? _selectedEnergyType;
        public EnergyType? SelectedEnergyType
        {
            get => _selectedEnergyType;
            set
            {
                _selectedEnergyType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedColor));
                OnPropertyChanged(nameof(SelectedBrush));
            }
        }

        // -----------------------------
        // COLOR (WPF Color)
        // -----------------------------
        public Color SelectedColor
        {
            get => SelectedEnergyType?.ToWpfColor() ?? Colors.Black;
            set
            {
                if (SelectedEnergyType != null)
                {
                    SelectedEnergyType.FromWpfColor(value);
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedBrush));
                }
            }
        }

        // -----------------------------
        // BRUSH (voor XAML preview)
        // -----------------------------
        public Brush SelectedBrush => new SolidColorBrush(SelectedColor);

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

        public EnergyTypesViewModel()
        {
            _controller = new EnergyTypesController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            EnergyTypes = new ObservableCollection<EnergyType>(_controller.UnitOfWork.EnergyTypeRepo.GetAll().GetAwaiter().GetResult());
            Units = new ObservableCollection<Unit>(_controller.UnitOfWork.UnitRepo.GetAll().GetAwaiter().GetResult());

            AddCommand = new RelayCommand(_ => add());
            SaveCommand = new RelayCommand(_ => save());
            CancelCommand = new RelayCommand(_ => cancel());
            DeleteCommand = new RelayCommand(_ => delete());
            RefreshCommand = new RelayCommand(_ => refresh());
            CloseCommand = new RelayCommand(_ => close());

            // ⭐ Selecteer eerste record bij laden
            selectFirstRecord();
        }

        private void selectFirstRecord()
        {
            if (EnergyTypes.Any())
                SelectedEnergyType = EnergyTypes.First();
            else
                SelectedEnergyType = null;
        }

        private void add()
        {
            var entity = _controller.UnitOfWork.AddDefaultEntity("New energy type");
            EnergyTypes.Add(entity);
            SelectedEnergyType = entity;
        }

        private void save()
        {
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully";
        }

        private void cancel()
        {
            _controller.UnitOfWork.CancelChanges();
            StatusMessage = "Cancelled successfully";
            refresh();
        }

        private void delete()
        {
            if (SelectedEnergyType == null)
                return;

            _controller.UnitOfWork.Delete(SelectedEnergyType);
            EnergyTypes.Remove(SelectedEnergyType);
            StatusMessage = "Energy type deleted";

            selectFirstRecord();
        }

        private async void refresh()
        {
            EnergyTypes = new ObservableCollection<EnergyType>(await _controller.UnitOfWork.EnergyTypeRepo.GetAll());
            OnPropertyChanged(nameof(EnergyTypes));

            // ⭐ Selecteer eerste record na refresh
            selectFirstRecord();
        }

        private void close()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is Views.Windows.EnergyTypesWindow)?
                .Close();
        }
    }
}
