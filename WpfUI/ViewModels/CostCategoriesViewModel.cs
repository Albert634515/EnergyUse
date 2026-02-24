using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Extensions;

namespace WpfUI.ViewModels
{
    public class CostCategoriesViewModel : ViewModelBase
    {
        private readonly CostcategoriesController _controller;

        public ObservableCollection<EnergyType> EnergyTypes { get; }
        public ObservableCollection<CostCategory> CostCategories { get; private set; }
        public ObservableCollection<Unit> Units { get; }
        public ObservableCollection<TariffGroup> TariffGroups { get; }
        public ObservableCollection<EnergySubType> EnergySubTypes { get; }
        public ObservableCollection<CalculationType> CalculationTypes { get; }

        private EnergyType _selectedEnergyType;
        public EnergyType SelectedEnergyType
        {
            get => _selectedEnergyType;
            set
            {
                _selectedEnergyType = value;
                OnPropertyChanged();
                LoadCostCategories();
            }
        }

        private CostCategory _selectedCostCategory;
        public CostCategory SelectedCostCategory
        {
            get => _selectedCostCategory;
            set
            {
                _selectedCostCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        // Wrapper property for ColorPicker
        public Color SelectedColor
        {
            get => SelectedCostCategory?.ToWpfColor() ?? Colors.White;
            set
            {
                if (SelectedCostCategory != null)
                {
                    SelectedCostCategory.FromWpfColor(value);
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

        public CostCategoriesViewModel()
        {
            _controller = new CostcategoriesController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            EnergyTypes = new ObservableCollection<EnergyType>((IEnumerable<EnergyType>)_controller.UnitOfWork.EnergyTypeRepo.GetAll());
            Units = new ObservableCollection<Unit>(_controller.UnitOfWork.UnitRepo.GetAll());
            TariffGroups = new ObservableCollection<TariffGroup>(_controller.UnitOfWork.TariffGroupRepo.GetAll());
            EnergySubTypes = new ObservableCollection<EnergySubType>(_controller.UnitOfWork.EnergySubTypeRepo.GetAll());
            CalculationTypes = new ObservableCollection<CalculationType>(_controller.UnitOfWork.CalculationTypeRepo.GetAll());

            AddCommand = new RelayCommand(_ => Add());
            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
            DeleteCommand = new RelayCommand(_ => Delete());
            RefreshCommand = new RelayCommand(_ => Refresh());
            CloseCommand = new RelayCommand(_ => Close());
        }

        private void LoadCostCategories()
        {
            if (SelectedEnergyType == null)
                return;

            var list = _controller.UnitOfWork.CostCategoryRepo
                                             .SelectByEnergyTypeId(SelectedEnergyType.Id)
                                             .ToList();

            CostCategories = new ObservableCollection<CostCategory>(list);
            OnPropertyChanged(nameof(CostCategories));
        }

        private void Add()
        {
            if (SelectedEnergyType == null)
            {
                StatusMessage = "Select an energy type first.";
                return;
            }

            var entity = _controller.UnitOfWork.AddDefaultEntity("New category", SelectedEnergyType.Id);
            CostCategories.Add(entity);
            SelectedCostCategory = entity;
        }

        private void Save()
        {
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully.";
        }

        private void Cancel()
        {
            _controller.UnitOfWork.CancelChanges();
            Refresh();
        }

        private void Delete()
        {
            if (SelectedCostCategory == null)
                return;

            _controller.UnitOfWork.Delete(SelectedCostCategory);
            CostCategories.Remove(SelectedCostCategory);
        }

        private void Refresh()
        {
            LoadCostCategories();
            StatusMessage = "Refreshed.";
        }

        private void Close()
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is Views.Windows.CostCategoriesWindow)?
                .Close();
        }
    }
}