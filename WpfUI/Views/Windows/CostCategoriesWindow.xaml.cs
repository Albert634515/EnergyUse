using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows
{
    public partial class CostCategoriesWindow : Window
    {
        private readonly CostCategoriesViewModel _vm;

        public CostCategoriesWindow()
        {
            InitializeComponent();
            _vm = new CostCategoriesViewModel();
            DataContext = _vm;
        }
    }
}