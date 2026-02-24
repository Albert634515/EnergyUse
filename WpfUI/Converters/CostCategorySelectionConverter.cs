using EnergyUse.Models;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfUI.Converters
{
    public class CostCategorySelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value = SelectedCostCategories
            var selected = value as IEnumerable<CostCategory>;
            if (selected == null)
                return false;

            // parameter = CheckBox zelf (via x:Reference)
            var checkBox = parameter as CheckBox;
            var current = checkBox?.DataContext as CostCategory;

            if (current == null)
                return false;

            return selected.Any(c => c.Id == current.Id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}