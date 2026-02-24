using System.Windows;
using WpfApp.ViewModels;
using WpfUI.Services;

namespace WpfUI.Views.Windows;

public partial class BackUpAndRestoreWindow : Window
{
    public BackUpAndRestoreWindow()
    {
        InitializeComponent();
        DataContext = new BackupAndRestoreViewModel(new DialogService());
    }
}