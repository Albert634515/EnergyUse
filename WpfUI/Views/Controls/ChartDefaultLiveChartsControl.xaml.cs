using EnergyUse.Models;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class ChartDefaultLiveChartsControl : UserControl, IRefreshable
    {
        public ChartDefaultLiveChartsViewModel ViewModel => (ChartDefaultLiveChartsViewModel)DataContext;

        public ChartDefaultLiveChartsControl(Address address, EnergyType energyType, IEnumerable<EnergyType> energyTypes)
        {
            InitializeComponent();

            var vm = new ChartDefaultLiveChartsViewModel(
                address,
                energyType,
                energyTypes,
                new SettingsService());

            DataContext = vm;

            // voorkomt infinite loops tijdens laden
            vm.MarkLoaded();
        }

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            ViewModel.CurrentAddress = address;
            ViewModel.CurrentEnergyType = energyType;
            ViewModel.SafeUpdateChart();
        }
    }
}