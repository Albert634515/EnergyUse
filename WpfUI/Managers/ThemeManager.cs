using System.Windows;

public static class ThemeManager
{
    public static void SetTheme(bool dark)
    {
        var app = Application.Current;

        app.Resources.MergedDictionaries.Clear();

        app.Resources.MergedDictionaries.Add(
            new ResourceDictionary { Source = new Uri("/Styles/Fluent/FluentColors.xaml", UriKind.Relative) });

        app.Resources.MergedDictionaries.Add(
            new ResourceDictionary { Source = new Uri("/Styles/Fluent/FluentControls.xaml", UriKind.Relative) });

        if (dark)
        {
            app.Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri("/Styles/Fluent/FluentDark.xaml", UriKind.Relative) });
        }
    }
}