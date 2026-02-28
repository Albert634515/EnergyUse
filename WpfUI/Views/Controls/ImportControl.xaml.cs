using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WpfUI.Interfaces;
using WpfUI.ViewModels;

namespace WpfUI.Views.Controls
{
    public partial class ImportControl : UserControl
    {
        public ImportControlViewModel ViewModel => (ImportControlViewModel)DataContext;

        public ImportControl(Address address, EnergyType energyType)
        {
            InitializeComponent();

            DataContext = new ImportControlViewModel(
                address,
                energyType,
                App.Services.GetRequiredService<ISettingsService>(),
                App.Services.GetRequiredService<IDialogService>(),
                App.Services.GetRequiredService<IImportService>());
        }
    }
}