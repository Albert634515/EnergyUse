using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfUI.Managers;

namespace WpfUI.Views.Windows;

public partial class CalculatedUnitPriceWindow : Window
{
    private readonly CalculatedUnitPriceController _controller;
    private bool _busy;
    private bool _initializing = true;
    private EnergyType? _energyType;

    public CalculatedUnitPriceWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        _controller = new CalculatedUnitPriceController(Config.GetDbFileName());
        _controller.Initialize();
        Loaded += async (_, _) => await RunAsync(async () =>
        {
            EnergyTypes.ItemsSource = (await UnitOfWork.EnergyTypeRepo.GetAll()).ToList();
            EnergyTypes.SelectedIndex = 0;
            _energyType = EnergyTypes.SelectedItem as EnergyType;
            _initializing = false;
            await LoadPricesAsync();
        });
        Closing += Window_Closing;
        Closed += (_, _) => UnitOfWork.Dispose();
    }

    private EnergyUse.Core.UnitOfWork.CalculatedUnitPrice UnitOfWork => _controller.UnitOfWork!;

    private async Task RunAsync(Func<Task> action)
    {
        if (_busy) return;
        _busy = true;
        ContentPanel.IsEnabled = false;
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            _busy = false;
            ContentPanel.IsEnabled = true;
        }
    }

    private async Task LoadPricesAsync()
    {
        UnitOfWork.CalculatedUnitPrices = _energyType != null
            ? (await UnitOfWork.CalculatedUnitPriceRepo.SelectByEnergyType(
                _energyType.Id)).ToList()
            : new List<CalculatedUnitPrice>();
        UnitOfWork.SetListSorted();
        Prices.ItemsSource = UnitOfWork.CalculatedUnitPrices;
        Prices.SelectedItem = UnitOfWork.CalculatedUnitPrices.FirstOrDefault();
    }

    private bool CommitEdits()
    {
        return Prices.CommitEdit(DataGridEditingUnit.Cell, true)
            && Prices.CommitEdit(DataGridEditingUnit.Row, true);
    }

    private bool ConfirmDiscard()
    {
        if (!UnitOfWork.HasChanges()) return true;
        return MessageBox.Show(this, "Discard unsaved changes?", Title,
            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
    }

    private async void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_initializing || _busy) return;
        if (!CommitEdits() || !ConfirmDiscard())
        {
            _initializing = true;
            EnergyTypes.SelectedItem = _energyType;
            _initializing = false;
            return;
        }
        await RunAsync(async () =>
        {
            UnitOfWork.CancelChanges();
            _energyType = EnergyTypes.SelectedItem as EnergyType;
            await LoadPricesAsync();
        });
    }

    private async void Add_Click(object sender, RoutedEventArgs e)
    {
        if (!CommitEdits()) return;
        if (_energyType == null)
        {
            MessageBox.Show(this, "Select an energy type first.", Title);
            return;
        }
        await RunAsync(async () =>
        {
            var entity = await UnitOfWork.AddDefaultEntity(_energyType.Id);
            // Include pending additions when choosing the next year.
            entity.Year = Math.Max(entity.Year, UnitOfWork.CalculatedUnitPrices
                .Where(p => p != entity).Select(p => p.Year + 1).DefaultIfEmpty(entity.Year).Max());
            UnitOfWork.SetListSorted();
            Prices.ItemsSource = UnitOfWork.CalculatedUnitPrices;
            Prices.SelectedItem = entity;
            Prices.ScrollIntoView(entity);
        });
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        if (!CommitEdits()) return;
        await RunAsync(async () =>
        {
            UnitOfWork.Complete();
            await LoadPricesAsync();
        });
    }

    private async void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Prices.CancelEdit(DataGridEditingUnit.Cell);
        Prices.CancelEdit(DataGridEditingUnit.Row);
        await RunAsync(async () =>
        {
            UnitOfWork.CancelChanges();
            await LoadPricesAsync();
        });
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (!CommitEdits() || Prices.SelectedItem is not CalculatedUnitPrice entity) return;
        if (MessageBox.Show(this, "Are you sure you want to delete this unit price?", Title,
            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
        await RunAsync(() =>
        {
            UnitOfWork.Delete(entity);
            UnitOfWork.CalculatedUnitPrices.Remove(entity);
            Prices.Items.Refresh();
            Prices.SelectedItem = UnitOfWork.CalculatedUnitPrices.FirstOrDefault();
            return Task.CompletedTask;
        });
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        if (!CommitEdits() || !ConfirmDiscard()) return;
        await RunAsync(async () =>
        {
            UnitOfWork.CancelChanges();
            await LoadPricesAsync();
        });
    }

    private void Close_Click(object sender, RoutedEventArgs e) => Close();

    private void Window_Closing(object? sender, CancelEventArgs e)
    {
        e.Cancel = _busy || !CommitEdits() || !ConfirmDiscard();
    }
}
