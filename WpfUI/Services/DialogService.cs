using EnergyUse.Core.Interfaces;
using Microsoft.Win32;
using System;
using System.IO;
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
            "There are unsaved changes, are you sure you want to close this form?");

        var title = Managers.Languages.GetResourceString(
            "UnsavedChangesTitle",
            "Unsaved changes");

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

        return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK
            ? dialog.SelectedPath
            : null;
    }

    public string GetExportFileName(string chartType, EnergyUse.Models.EnergyType energyType)
    {
        var exportFileName = $"{chartType}_{DateTime.Now:yyyyMMddHHmmss}_{energyType.Name}.xlsx";

        var dialog = new SaveFileDialog
        {
            FileName = exportFileName,
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "Export chart to Excel"
        };

        if (dialog.ShowDialog() != true)
            return string.Empty;

        var fullPath = dialog.FileName;
        var directory = Path.GetDirectoryName(fullPath);

        if (string.IsNullOrWhiteSpace(directory))
            return string.Empty;

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Bestaat bestand al?
        if (File.Exists(fullPath))
        {
            var message = Managers.Languages.GetResourceString(
                "FileExistsOverWrite",
                "File already exists, do you want to overwrite?");

            var title = Managers.Languages.GetResourceString(
                "FileAlreadyExists",
                "File already exists");

            if (MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return string.Empty;

            File.Delete(fullPath);
        }

        return fullPath;
    }
}