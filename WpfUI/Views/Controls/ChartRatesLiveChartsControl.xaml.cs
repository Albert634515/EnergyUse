using EnergyUse.Models;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    /// <summary>
    /// Interaction logic for ChartRatesLiveCharts.xaml
    /// </summary>
    public partial class ChartRatesLiveChartsControl : UserControl, IRefreshable
    {
        public ChartRatesLiveChartsViewModel ViewModel => (ChartRatesLiveChartsViewModel)DataContext;

        public ChartRatesLiveChartsControl(Address address, EnergyType energyType)
        {
            InitializeComponent();
            DataContext = new ChartRatesLiveChartsViewModel(address, energyType);
        }

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            ViewModel.CurrentAddress = address;
            ViewModel.CurrentEnergyType = energyType;
            ViewModel.UpdateChart();
        }
    }
}
