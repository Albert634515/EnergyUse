using EnergyUse.Models.Common;
using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class SettlementReportWindow : Window
    {
        public ParameterSelection? SelectedParameters { get; private set; }

        public SettlementReportWindow(SettlementReportViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var vm = (SettlementReportViewModel)DataContext;

            vm.CloseRequested += result =>
            {
                if (result)
                    SelectedParameters = vm.GetSelectedParameters();

                DialogResult = result;   // ✔️ nu is het venster volledig actief
                Close();
            };
        }
    }
}