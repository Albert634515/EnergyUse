using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfUI.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value is bool b && b;

            // Optional invert
            if (parameter?.ToString() == "Invert")
                flag = !flag;

            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Visibility → bool wordt in jouw UI nooit gebruikt
            return Binding.DoNothing;
        }
    }
}