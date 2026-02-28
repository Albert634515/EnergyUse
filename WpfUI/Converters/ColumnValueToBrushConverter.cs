using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfUI.Converters
{
    public class ColumnValueToBrushConverter : IValueConverter
    {
        public Brush PositiveBrush { get; set; } = Brushes.Green;
        public Brush NegativeBrush { get; set; } = Brushes.Red;
        public Brush ZeroBrush { get; set; } = Brushes.White;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return ZeroBrush;

            try
            {
                // Ondersteunt decimal, double, int, long, string
                decimal number = System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);

                if (number > 0)
                    return PositiveBrush;

                if (number < 0)
                    return NegativeBrush;

                return ZeroBrush;
            }
            catch
            {
                // Als conversie mislukt → neutrale kleur
                return ZeroBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}