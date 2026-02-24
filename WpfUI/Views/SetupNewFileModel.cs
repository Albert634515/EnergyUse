using EnergyUse.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class SetupNewFileModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private Address _address = new Address();
    public Address Address
    {
        get => _address;
        set { _address = value; OnPropertyChanged(); }
    }

    private bool _addBaseData = true;
    public bool AddBaseData
    {
        get => _addBaseData;
        set { _addBaseData = value; OnPropertyChanged(); }
    }

    private bool _setAsDefaultFile = true;
    public bool SetAsDefaultFile
    {
        get => _setAsDefaultFile;
        set { _setAsDefaultFile = value; OnPropertyChanged(); }
    }
}