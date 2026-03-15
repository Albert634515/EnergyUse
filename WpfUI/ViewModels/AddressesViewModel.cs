using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class AddressesViewModel : ViewModelBase
{
    private readonly AddressController _controller;
    private readonly ISettingsService _settings;

    public ObservableCollection<Address> Addresses { get; set; } = new();
    public ObservableCollection<TariffGroup> GeneralTariffs { get; set; } = new();
    public ObservableCollection<TariffGroup> EnergyTariffs { get; set; } = new();

    private Address? _selectedAddress;
    public Address? SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            _selectedAddress = value;
            OnPropertyChanged();

            if (value != null)
                _settings.Save("LastSelectedAddressId", value.Id.ToString());
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

    public event Action? CloseRequested;

    public AddressesViewModel(ISettingsService settings)
    {
        _settings = settings;

        _controller = new AddressController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        getTariffs();
        getAddresses();

        AddCommand = new RelayCommand(_ => addAddress());
        SaveCommand = new RelayCommand(_ => saveAddress());
        CancelCommand = new RelayCommand(_ => cancel());
        DeleteCommand = new RelayCommand(_ => deleteAddress());
        RefreshCommand = new RelayCommand(_ => getAddresses());
        CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());
    }

    private async void getAddresses()
    {
        try
        {
            var list = await _controller.GetAllAdressesAsync();
            Addresses = new ObservableCollection<Address>(list);
            OnPropertyChanged(nameof(Addresses));

            // Restore last selected
            var savedId = _settings.Get("LastSelectedAddressId");
            if (int.TryParse(savedId, out int id))
                SelectedAddress = Addresses.FirstOrDefault(a => a.Id == id);

            if (SelectedAddress == null)
                SelectedAddress = Addresses.FirstOrDefault();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading addresses:\n" + ex);
        }
    }

    private void getTariffs()
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

    private void cancel()
    {
        _controller.UnitOfWork.CancelChanges();
    }

    private void deleteAddress()
    {
        if (SelectedAddress == null)
            return;

        _controller.UnitOfWork.Delete(SelectedAddress);
        Addresses.Remove(SelectedAddress);
        SelectedAddress = Addresses.FirstOrDefault();
    }
}