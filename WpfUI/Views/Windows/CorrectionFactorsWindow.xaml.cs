using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

/// <summary>
/// Interaction logic for CorrectionFactorsWindow.xaml
/// </summary>
public partial class CorrectionFactorsWindow : Window
{
    public CorrectionFactorsWindow()
    {
        InitializeComponent();
        if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            DataContext = new CorrectionFactorViewModel();
    }
}
