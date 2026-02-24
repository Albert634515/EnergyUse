using EnergyUse.Models.Common;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUI.Managers;
using WpfUI.ViewModels;

namespace WpfUI.Views.Windows;

public partial class InfoWindow : Window
{
    public InfoWindow()
    {
        InitializeComponent();

        DataContext = new InfoWindowViewModel();

        chkHideInfoFormOnStart.IsChecked = Config.GetBool("HideInfoFormOnStart");
        chkHideInfoFormOnStart.Checked += (s, e) => Config.SetBool("HideInfoFormOnStart", true);
        chkHideInfoFormOnStart.Unchecked += (s, e) => Config.SetBool("HideInfoFormOnStart", false);

        BuildDynamicContent();
    }

    private void BuildDynamicContent()
    {
        var vm = (InfoWindowViewModel)DataContext;

        // ICONS
        AddTitle("Icons were used from:");
        AddReferenceList(vm.IconReferences);

        // SPACING
        stackContent.Children.Add(new Border { Height = 10, Background = Brushes.Transparent });

        // PLUGINS
        AddTitle("The following plugins were used:");
        AddReferenceList(vm.PluginReferences);
    }

    private void AddTitle(string text)
    {
        stackContent.Children.Add(new TextBlock
        {
            Text = text,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 10, 0, 5)
        });
    }

    private void AddReferenceList(IEnumerable<InfoReferenceItem> items)
    {
        foreach (var item in items)
        {
            var link = new TextBlock
            {
                Text = item.Name,
                Foreground = Brushes.RoyalBlue,
                TextDecorations = TextDecorations.Underline,
                Cursor = System.Windows.Input.Cursors.Hand,
                Tag = item.Url,
                Margin = new Thickness(0, 2, 0, 2)
            };

            link.MouseLeftButtonUp += (s, e) =>
            {
                string url = (string)((TextBlock)s).Tag;
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            };

            stackContent.Children.Add(link);
        }
    }

    private void cmdClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}