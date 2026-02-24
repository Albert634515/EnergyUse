using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using WpfUI.Extensions;
using System.Windows.Media;

namespace WpfUI.ViewModels
{
    public class EnergyTypesViewModel : ViewModelBase
    {
        private readonly EnergyTypesController _controller;

        public ObservableCollection<EnergyType> EnergyTypes { get; private set; }
        public ObservableCollection<Unit> Units { get; }

        private EnergyType _selectedEnergyType;
        public EnergyType SelectedEnergyType
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

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }
        private string _statusMessage;

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
            StatusMessage = "Saved.";
        }

        private void Cancel()
        {
            _controller.UnitOfWork.CancelChanges();
            Refresh();
        }

        private void Delete()
        {
            if (SelectedEnergyType == null)
                return;

            _controller.UnitOfWork.Delete(SelectedEnergyType);
            EnergyTypes.Remove(SelectedEnergyType);
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