using EnergyUse.Core.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace WpfUI.Services;

public class DialogService : IDialogService
{
    public string? OpenFolder()
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        dialog.ShowNewFolderButton = true;

        return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK ? dialog.SelectedPath : null;
    }

    public bool ShowYesNo(string message, string title)
    {
        return MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }

    public void Show(string message, string title)
    {
        MessageBox.Show(message, title);
    }

    public string? SaveFile(string filter, string title)
    {
        var dialog = new SaveFileDialog
        {
            Filter = filter,
            Title = title
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}