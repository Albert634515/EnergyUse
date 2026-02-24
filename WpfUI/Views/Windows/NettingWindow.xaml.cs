using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class NettingWindow : Window
{
    public NettingWindow()
    {
        InitializeComponent();
        DataContext = new NettingViewModel(this);
    }
}