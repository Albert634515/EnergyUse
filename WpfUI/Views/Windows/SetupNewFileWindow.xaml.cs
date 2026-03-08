using System.Windows;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class SetupNewFileWindow : Window
    {
        public SetupNewFileWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var vm = new SetupNewFileViewModel(new DialogService());
            DataContext = vm;

            vm.RequestClose += () => this.Close();
        }
    }
}