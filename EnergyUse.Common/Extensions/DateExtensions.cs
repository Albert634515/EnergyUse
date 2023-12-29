using System.Globalization;

namespace EnergyUse.Common.Extensions
{
    public static class DateExtension
    {
        public static int GetWeekNumber(this DateTime date)
        {
            return GetWeekNumber(date, CultureInfo.CurrentCulture);
        }

        public static int GetWeekNumber(this DateTime date, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(date,
                culture.DateTimeFormat.CalendarWeekRule,
                culture.DateTimeFormat.FirstDayOfWeek);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static string YearWeekFormat(this DateTime dt)
        {
            return $"{dt.Year} {dt.GetWeekNumber()}";
        }
    }
}