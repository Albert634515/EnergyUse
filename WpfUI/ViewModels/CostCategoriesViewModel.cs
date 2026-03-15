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

        public ObservableCollection<EnergyType> EnergyTypes { get; private set; } = [];

        public ObservableCollection<CostCategory> CostCategories { get; private set; } = [];

        public ObservableCollection<Unit> Units { get; }
        public ObservableCollection<TariffGroup> TariffGroups { get; }
        public ObservableCollection<EnergySubType> EnergySubTypes { get; }
        public ObservableCollection<CalculationType> CalculationTypes { get; }

        private EnergyType? _selectedEnergyType;
        public EnergyType? SelectedEnergyType
        {
            get => _selectedEnergyType;
            set
            {
                _selectedEnergyType = value;
                OnPropertyChanged();
                setCostCategories();
            }
        }

        private CostCategory? _selectedCostCategory;
        public CostCategory? SelectedCostCategory
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

        public CostCategoriesViewModel()
        {
            _controller = new CostcategoriesController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            _ = setEnergyTypesAsync();
            
            Units = new ObservableCollection<Unit>(_controller.UnitOfWork.UnitRepo.GetAll());
            TariffGroups = new ObservableCollection<TariffGroup>(_controller.UnitOfWork.TariffGroupRepo.GetAll());
            EnergySubTypes = new ObservableCollection<EnergySubType>(_controller.UnitOfWork.EnergySubTypeRepo.GetAll());
            CalculationTypes = new ObservableCollection<CalculationType>(_controller.UnitOfWork.CalculationTypeRepo.GetAll());

            AddCommand = new RelayCommand(_ => add());
            SaveCommand = new RelayCommand(_ => save());
            CancelCommand = new RelayCommand(_ => cancel());
            DeleteCommand = new RelayCommand(_ => delete());
            RefreshCommand = new RelayCommand(_ => refresh());
            CloseCommand = new RelayCommand(_ => Close());
        }

        public async Task setEnergyTypesAsync()
        {
            var energyTypes = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();
            EnergyTypes = new ObservableCollection<EnergyType>(energyTypes);
        }

        private void setCostCategories()
        {
            if (SelectedEnergyType == null)
                return;

            var list = _controller.UnitOfWork.CostCategoryRepo
                                             .SelectByEnergyTypeId(SelectedEnergyType.Id)
                                             .ToList();

            CostCategories = new ObservableCollection<CostCategory>(list);
            OnPropertyChanged(nameof(CostCategories));
        }

        private void add()
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

        private void save()
        {
            _controller.UnitOfWork.Complete();
            StatusMessage = "Saved successfully.";
        }

        private void cancel()
        {
            _controller.UnitOfWork.CancelChanges();
            refresh();
            StatusMessage = "Cancelled successfully";
        }

        private void delete()
        {
            if (SelectedCostCategory == null)
                return;

            _controller.UnitOfWork.Delete(SelectedCostCategory);
            CostCategories.Remove(SelectedCostCategory);
            StatusMessage = "Cost category deleted";
        }

        private void refresh()
        {
            setCostCategories();
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