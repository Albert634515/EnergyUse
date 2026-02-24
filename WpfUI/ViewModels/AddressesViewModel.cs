using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class AddressesViewModel : ViewModelBase
{
    private readonly AddressController _controller;

    private ObservableCollection<Address> _addresses;
    public ObservableCollection<Address> Addresses
    {
        get => _addresses;
        set
        {
            _addresses = value;
            OnPropertyChanged();
        }
    }

    private Address _selectedAddress;
    public Address SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            _selectedAddress = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TariffGroup> GeneralTariffs { get; set; }
    public ObservableCollection<TariffGroup> EnergyTariffs { get; set; }

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public AddressesViewModel()
    {
        try
        {

            _controller = new AddressController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            getAddresses();
            getTariffGroups();

            AddCommand = new RelayCommand(_ => addAddress());
            SaveCommand = new RelayCommand(_ => saveAddress());
            DeleteCommand = new RelayCommand(_ => deleteAddress());
            RefreshCommand = new RelayCommand(_ => getAddresses());
            CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

    }

    public event Action CloseRequested;

    private async void getAddresses()
    {
        try
        {
            var list = await _controller.GetAllAdressesAsync();
            Addresses = new ObservableCollection<Address>(list);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading addresses:\n" + ex);
        }
    }

    private void getTariffGroups()
    {
        GeneralTariffs = new ObservableCollection<TariffGroup>(
            _controller.GetTariffGroups((int)EnergyUse.Common.Enums.TariffGroupType.GeneralCosts));

        EnergyTariffs = new ObservableCollection<TariffGroup>(
            _controller.GetTariffGroups((int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts));
    }

    private void addAddress()
    {
        var entity = _controller.AddDefaultEntity("New address");
        Addresses.Add(entity);
        SelectedAddress = entity;
    }

    private void saveAddress()
    {
        if (SelectedAddress == null)
            return;

        _controller.UnitOfWork.Complete();
    }

    private void deleteAddress()
    {
        if (SelectedAddress == null)
            return;

        _controller.UnitOfWork.Delete(SelectedAddress);
        Addresses.Remove(SelectedAddress);
    }
}