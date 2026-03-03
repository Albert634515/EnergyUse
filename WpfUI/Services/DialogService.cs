using EnergyUse.Core.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace WpfUI.Services;

public class DialogService : IDialogService
{
    public void Show(string message, string title)
    {
        MessageBox.Show(message, title);
    }

    public bool ShowYesNo(string message, string title)
    {
        return MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }

    public bool WarningUnsavedChanges()
    {
        var message = Managers.Languages.GetResourceString(
            "UnsavedChanges",
            "There are unsaved changes are you sure you want to close this form?");

        var title = Managers.Languages.GetResourceString(
            "UnsavedChangesTitle",
            "Unsaved changes");

        // WinForms had: return true when user chooses NO
        // WPF equivalent:
        return ShowYesNo(message, title);
    }


    public string? OpenFile(string filter, string title)
    {
        var dialog = new OpenFileDialog
        {
            Filter = filter,
            Title = title
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
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

    public string? OpenFolder()
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        dialog.ShowNewFolderButton = true;

        return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK ? dialog.SelectedPath : null;
    }
}