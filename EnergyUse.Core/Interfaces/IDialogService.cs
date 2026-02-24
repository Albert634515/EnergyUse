namespace EnergyUse.Core.Interfaces;

public interface IDialogService
{
    string? OpenFolder();
    bool ShowYesNo(string message, string title);
    void Show(string message, string title);
    string? SaveFile(string filter, string title);
}