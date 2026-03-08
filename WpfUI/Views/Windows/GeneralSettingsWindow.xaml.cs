using System.Windows;

namespace WpfUI.Views.Windows
{
    /// <summary>
    /// Interaction logic for GeneralSettingsWindow.xaml
    /// </summary>
    public partial class GeneralSettingsWindow : Window
    {
        public GeneralSettingsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
