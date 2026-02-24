using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class VatTarifsViewModel : ViewModelBase
{
    private readonly VatTariffController _controller;

    public VatTarifsViewModel()
    {
        _controller = new VatTariffController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        AddCommand = new RelayCommand(_ => Add());
        SaveCommand = new RelayCommand(_ => Save());
        CancelCommand = new RelayCommand(_ => Cancel());
        DeleteCommand = new RelayCommand(_ => Delete());
        RefreshCommand = new RelayCommand(_ => Refresh());
        CloseCommand = new RelayCommand(_ => CloseWindow());

        getEnergyTypes();
    }

    #region Collections

    public ObservableCollection<EnergyType> EnergyTypes { get; } = new();
    public ObservableCollection<CostCategory> CostCategories { get; } = new();
    public ObservableCollection<VatTarif> VatTariffs { get; } = new();

    #endregion

    #region Selected Items

    private EnergyType? _selectedEnergyType;
    public EnergyType? SelectedEnergyType
    {
        get => _selectedEnergyType;
        set
        {
            if (_selectedEnergyType != value)
            {
                _selectedEnergyType = value;
                OnPropertyChanged();
                LoadCostCategories();
                LoadVatTariffs();
            }
        }
    }

    private CostCategory? _selectedCostCategory;
    public CostCategory? SelectedCostCategory
    {
        get => _selectedCostCategory;
        set
        {
            if (_selectedCostCategory != value)
            {
                _selectedCostCategory = value;
                OnPropertyChanged();
                LoadVatTariffs();
            }
        }
    }

    private VatTarif? _selectedVatTariff;
    public VatTarif? SelectedVatTariff
    {
        get => _selectedVatTariff;
        set
        {
            if (_selectedVatTariff != value)
            {
                _selectedVatTariff = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Load Methods

    private async Task getEnergyTypes()
    {
        EnergyTypes.Clear();

        var list = await _controller.UnitOfWork.EnergyTypeRepo.GetAll();

        foreach (var et in list)
            EnergyTypes.Add(et);
    }

    private void LoadCostCategories()
    {
        CostCategories.Clear();

        if (SelectedEnergyType == null)
            return;

        var list = _controller.UnitOfWork.CostCategoryRepo
            .SelectByEnergyTypeIdVatCalculated(SelectedEnergyType.Id);

        foreach (var cc in list)
            CostCategories.Add(cc);
    }

    private async void LoadVatTariffs()
    {
        VatTariffs.Clear();

        if (SelectedCostCategory == null)
            return;

        var list = await _controller.UnitOfWork.VatTarifRepo
            .GetByCostCategoryId(SelectedCostCategory.Id);

        foreach (var vt in list)
            VatTariffs.Add(vt);
    }

    #endregion

    #region Commands

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    private void Add()
    {
        if (SelectedCostCategory == null)
            return;

        var entity = _controller.UnitOfWork.AddDefaultEntity(SelectedCostCategory.Id);
        VatTariffs.Add(entity);
        SelectedVatTariff = entity;
    }

    private void Save()
    {
        _controller.UnitOfWork.Complete();
        LoadVatTariffs();
    }

    private void Cancel()
    {
        _controller.UnitOfWork.CancelChanges();
        LoadVatTariffs();
    }

    private void Delete()
    {
        if (SelectedVatTariff == null)
            return;

        _controller.UnitOfWork.Delete(SelectedVatTariff);
        VatTariffs.Remove(SelectedVatTariff);
    }

    private void Refresh()
    {
        LoadVatTariffs();
    }

    private void CloseWindow()
    {
        var window = System.Windows.Application.Current.Windows
            .OfType<System.Windows.Window>()
            .FirstOrDefault(w => w.DataContext == this);

        window?.Close();
    }

    #endregion
}