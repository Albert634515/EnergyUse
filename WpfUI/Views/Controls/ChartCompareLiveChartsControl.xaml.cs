using System.Windows.Controls;
using WpfUI.ViewModels;
using EnergyUse.Models;
using WpfUI.Services;

namespace WpfUI.Views.Controls
{
    public partial class ChartCompareLiveChartsControl : UserControl
    {
        public ChartCompareLiveChartsControl(
            Address address,
            EnergyType energyType,
            ILanguageService languageService)
        {
            InitializeComponent();

            // ViewModel injecteren
            DataContext = new ChartCompareLiveChartsViewModel(
                address,
                energyType,
                languageService);
        }
    }
}