using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class PaymentsViewModel : ViewModelBase
{
    private readonly PaymentsController _controller;

    public PaymentsViewModel()
    {
        // Belangrijk: voorkom dat de designer database opent
        //if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        //    return;

        _controller = new PaymentsController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        setAddresses();
        setPeriods();

        AddCommand = new RelayCommand(_ => addPayment());
        SaveCommand = new RelayCommand(_ => savePayment());
        CancelCommand = new RelayCommand(_ => cancelPayment());
        DeleteCommand = new RelayCommand(_ => deletePayment());
        RefreshCommand = new RelayCommand(_ => refreshPayments());
        CloseCommand = new RelayCommand(_ => CloseRequested?.Invoke());
    }

    #region Collections

    public ObservableCollection<Address> Addresses { get; set; } = new();
    public ObservableCollection<PreDefinedPeriod> PreDefinedPeriods { get; set; } = new();
    public ObservableCollection<Payment> Payments { get; set; } = new();

    #endregion

    #region Selected Items

    private Address? _selectedAddress;
    public Address? SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            _selectedAddress = value;
            OnPropertyChanged();
            setPayments();
        }
    }

    private PreDefinedPeriod? _selectedPeriod;
    public PreDefinedPeriod? SelectedPeriod
    {
        get => _selectedPeriod;
        set
        {
            _selectedPeriod = value;
            OnPropertyChanged();
            setPayments();
        }
    }

    private Payment? _selectedPayment;
    public Payment? SelectedPayment
    {
        get => _selectedPayment;
        set { _selectedPayment = value; OnPropertyChanged(); }
    }

    #endregion

    #region Loaders

    private async void setAddresses()
    {
        var list = await _controller.UnitOfWork.AddressRepo.GetAll();
        Addresses.Clear();
        foreach (var a in list)
            Addresses.Add(a);

        if (Addresses.Any())
            SelectedAddress = Addresses.FirstOrDefault(x => x.DefaultAddress == true);
    }

    private void setPeriods()
    {
        var list = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll();
        PreDefinedPeriods.Clear();
        foreach (var p in list)
            PreDefinedPeriods.Add(p);
    }

    private void setPayments()
    {
        if (SelectedAddress == null || SelectedPeriod == null)
            return;

        Payments.Clear();

        var list = _controller.UnitOfWork.PaymentRepo.SelectByAddressAndPeriod(SelectedAddress.Id, SelectedPeriod.Id);

        foreach (var p in list)
            Payments.Add(p);
    }

    #endregion

    #region Commands

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CloseCommand { get; }

    public event Action? CloseRequested;

    private void addPayment()
    {
        if (SelectedAddress == null || SelectedPeriod == null)
            return;

        var entity = _controller.UnitOfWork.AddDefaultEntity(
            "",
            SelectedAddress.Id,
            SelectedPeriod.Id);

        Payments.Add(entity);
        SelectedPayment = entity;
    }

    private void savePayment()
    {
        _controller.UnitOfWork.Complete();
    }

    private void cancelPayment()
    {
        _controller.UnitOfWork.CancelChanges();
        setPayments();
    }

    private void deletePayment()
    {
        if (SelectedPayment == null)
            return;

        _controller.UnitOfWork.Delete(SelectedPayment);
        Payments.Remove(SelectedPayment);
    }

    private void refreshPayments()
    {
        setPayments();
    }

    #endregion
}