using EnergyUse.Models;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class DataControl : UserControl, IRefreshable
    {
        public DataControlViewModel ViewModel => (DataControlViewModel)DataContext;

        public DataControl(Address address, EnergyType energyType)
        {
            InitializeComponent();
            DataContext = new DataControlViewModel(address, energyType, new DialogService());
        }

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            ViewModel.Refresh(address, energyType, addressChanged);
        }
    }
}