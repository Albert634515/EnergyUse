using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using EnergyUse.Models;

namespace WpfUI.Converters
{
    public class MeterReadingToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MeterReading reading)
            {
                if (reading.Id == null)
                    return Brushes.LightGreen;   // Nieuw record

                return Brushes.Azure;            // Bestaand record
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}