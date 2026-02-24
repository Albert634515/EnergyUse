using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class MetersWindow : Window
{
    public MetersWindow()
    {
        InitializeComponent();
        DataContext = new MetersViewModel(this);
    }
}