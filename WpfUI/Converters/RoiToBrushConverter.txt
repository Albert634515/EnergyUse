using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfUI.Converters
{
    public class RoiToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal roi)
            {
                if (roi < 0)
                    return Brushes.Red;

                if (roi > 0)
                    return Brushes.Green;

                return Brushes.White;
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}