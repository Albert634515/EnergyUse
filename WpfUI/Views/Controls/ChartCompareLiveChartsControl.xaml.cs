using EnergyUse.Models;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls;

/// <summary>
/// Interaction logic for ChartCompareLiveCharts.xaml
/// </summary>
public partial class ChartCompareLiveChartsControl : UserControl, IRefreshable
{
    public ChartCompareLiveChartsViewModel ViewModel => (ChartCompareLiveChartsViewModel)DataContext;

    public ChartCompareLiveChartsControl(Address address, EnergyType energyType)
    {
        InitializeComponent();
        DataContext = new ChartCompareLiveChartsViewModel(address, energyType);
    }

    public void Refresh(Address address, EnergyType energyType, bool addressChanged)
    {
        ViewModel.CurrentAddress = address;
        ViewModel.CurrentEnergyType = energyType;
        ViewModel.UpdateChart();
    }
}
