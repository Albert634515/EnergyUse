namespace EnergyUse.Core.Interfaces;

public interface IDialogService
{
    void Show(string message, string title);
    bool ShowYesNo(string message, string title);

    string? OpenFile(string filter, string title);
    string? SaveFile(string filter, string title);

    string? OpenFolder();    
}