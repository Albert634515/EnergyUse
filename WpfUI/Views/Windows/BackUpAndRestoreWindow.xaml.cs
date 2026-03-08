using System.Windows;
using WpfApp.ViewModels;
using WpfUI.Services;

namespace WpfUI.Views.Windows;

public partial class BackUpAndRestoreWindow : Window
{
    public BackUpAndRestoreWindow(Window owner)
    {
        InitializeComponent();
        Owner = owner;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;

        DataContext = new BackupAndRestoreViewModel(new DialogService());
    }
}