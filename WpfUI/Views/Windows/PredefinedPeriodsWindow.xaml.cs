using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class PredefinedPeriodsWindow : Window
{
    public PredefinedPeriodsWindow()
    {
        InitializeComponent();
        DataContext = new PredefinedPeriodsViewModel();
    }
}