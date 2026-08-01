using System.Globalization;
using System.Windows.Data;

namespace WpfUI.Converters
{
    public class DecimalInputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is decimal decimalValue
                ? decimalValue.ToString(CultureInfo.CurrentCulture)
                : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string text)
                return Binding.DoNothing;

            var normalizedText = text.Trim()
                .Replace('\u2212', '-')
                .Replace('\uFE63', '-')
                .Replace('\uFF0D', '-');

            if (normalizedText.Length == 0
                || normalizedText == "-"
                || normalizedText.EndsWith(',')
                || normalizedText.EndsWith('.'))
                return Binding.DoNothing;

            normalizedText = normalizeDecimalSeparator(normalizedText);

            return decimal.TryParse(
                normalizedText,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out var decimalValue)
                    ? decimalValue
                    : Binding.DoNothing;
        }

        private static string normalizeDecimalSeparator(string text)
        {
            var lastComma = text.LastIndexOf(',');
            var lastPoint = text.LastIndexOf('.');
            var decimalSeparatorIndex = Math.Max(lastComma, lastPoint);

            if (decimalSeparatorIndex < 0)
                return text;

            var integerPart = text[..decimalSeparatorIndex]
                .Replace(",", string.Empty)
                .Replace(".", string.Empty);
            var fractionalPart = text[(decimalSeparatorIndex + 1)..]
                .Replace(",", string.Empty)
                .Replace(".", string.Empty);

            if (integerPart.Length == 0)
                integerPart = "0";
            else if (integerPart == "-")
                integerPart = "-0";

            return integerPart
                + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                + fractionalPart;
        }
    }
}
