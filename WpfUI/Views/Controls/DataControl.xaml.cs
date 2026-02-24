using System.Windows.Controls;
using EnergyUse.Models;
using WpfUI.Interfaces;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class DataControl : UserControl, IRefreshable
    {
        public DataControlViewModel ViewModel => (DataControlViewModel)DataContext;

        public DataControl(Address address, EnergyType energyType)
        {
            InitializeComponent();
            DataContext = new DataControlViewModel(address, energyType);
        }

        public void Refresh(Address address, EnergyType energyType, bool addressChanged)
        {
            ViewModel.Refresh(address, energyType, addressChanged);
        }
    }
}