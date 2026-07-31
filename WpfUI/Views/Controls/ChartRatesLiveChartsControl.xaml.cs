using EnergyUse.Models;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    /// <summary>
    /// Interaction logic for ChartRatesLiveCharts.xaml
    /// </summary>
    public partial class ChartRatesLiveChartsControl : UserControl, IRefreshable
    {
        private bool _initialized;
        public ChartRatesLiveChartsViewModel ViewModel => (ChartRatesLiveChartsViewModel)DataContext;

        public ChartRatesLiveChartsControl(Address address, EnergyType energyType)
        {
            InitializeComponent();
            DataContext = new ChartRatesLiveChartsViewModel(address, energyType, new SettingsService());
            Loaded += async (_, _) =>
            {
                if (_initialized)
                    return;

                _initialized = true;
                await ViewModel.InitializeAsync();
            };
        }

        public async void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            await ViewModel.RefreshAsync(address, energyType);
        }
    }
}
