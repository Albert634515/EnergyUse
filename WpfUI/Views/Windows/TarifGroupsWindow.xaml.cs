using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class TarifGroupsWindow : Window
    {
        public TarifGroupsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Maak de dialogservice aan
            var dialogService = new DialogService();

            // Maak het viewmodel aan en geef de service door
            var vm = new TarifGroupsViewModel(dialogService);

            // Koppel de sluit-actie zodat het ViewModel het venster kan sluiten
            vm.CloseAction = () => this.Close();

            // Zet het viewmodel als DataContext
            DataContext = vm;
        }
    }
}