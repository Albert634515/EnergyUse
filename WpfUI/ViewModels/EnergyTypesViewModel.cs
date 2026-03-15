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
            }
        }

        public Color SelectedColor
        {
            get => SelectedEnergyType?.ToWpfColor() ?? Colors.Black;
            set
            {
                if (SelectedEnergyType != null)
                {
                    SelectedEnergyType.FromWpfColor(value);
                    OnPropertyChanged();
                }
            }
        }

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

            EnergyTypes = new ObservableCollection<EnergyType>(_controller.UnitOfWork.EnergyTypeRepo.GetAll());
            Units = new ObservableCollection<Unit>(_controller.UnitOfWork.UnitRepo.GetAll());

            AddCommand = new RelayCommand(_ => Add());
            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
            DeleteCommand = new RelayCommand(_ => Delete());
            RefreshCommand = new RelayCommand(_ => Refresh());
            CloseCommand = new RelayCommand(_ => Close());
        }

        private void Add()
        {
            var entity = _controller.UnitOfWork.AddDefaultEntity("New energy type");
            EnergyTypes.Add(entity);
            SelectedEnergyType = entity;
        }

        private void Save()
        {
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully";
        }

        private void Cancel()
        {
            _controller.UnitOfWork.CancelChanges();
            StatusMessage = "Cancelled successfully";
            Refresh();
        }

        private void Delete()
        {
            if (SelectedEnergyType == null)
                return;

            _controller.UnitOfWork.Delete(SelectedEnergyType);
            EnergyTypes.Remove(SelectedEnergyType);
            StatusMessage = "Energy type deleted";
        }

        private void Refresh()
        {
            EnergyTypes = new ObservableCollection<EnergyType>(_controller.UnitOfWork.EnergyTypeRepo.GetAll());
            OnPropertyChanged(nameof(EnergyTypes));
        }

        private void Close()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is Views.Windows.EnergyTypesWindow)?
                .Close();
        }
    }
}