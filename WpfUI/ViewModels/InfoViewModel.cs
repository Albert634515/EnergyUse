using EnergyUse.Models.Common;
using System.Collections.ObjectModel;

namespace WpfUI.ViewModels;

public class InfoWindowViewModel : ViewModelBase
{
    public ObservableCollection<InfoReferenceItem> IconReferences { get; }
    public ObservableCollection<InfoReferenceItem> PluginReferences { get; }

    public InfoWindowViewModel()
    {
        IconReferences = new ObservableCollection<InfoReferenceItem>(GetIconReferences());
        PluginReferences = new ObservableCollection<InfoReferenceItem>(GetPluginReferences());
    }

    private IEnumerable<InfoReferenceItem> GetIconReferences() =>
        new List<InfoReferenceItem>
        {
            new("Icons from FlatIcon.com", "https://www.flaticon.com"),
            new("Icons made by Pixel perfect", "https://www.flaticon.com/packs/basic-ui-30/")
        };

    private IEnumerable<InfoReferenceItem> GetPluginReferences() =>
        new List<InfoReferenceItem>
        {
            new("EpPlus", "https://www.epplussoftware.com/"),
            new("iText7", "https://itextpdf.com/"),
            new("LiveCharts", "https://lvcharts.com/"),
            new("Microsoft EntityFrameworkCore", "https://docs.microsoft.com/en-us/ef/core/")
        };
}