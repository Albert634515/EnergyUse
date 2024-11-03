using System;
using System.Globalization;

namespace EnergyUse.Common.Libs;

public class LibDatetime
{
    public static int GetDaysInYear(DateTime date)
    {
        int daysInYear;
        DateTime startDate, endDate;

        startDate = new DateTime(date.Year, 1, 1);
        endDate = new DateTime(date.Year, 12, 31);
        daysInYear = (int)endDate.Subtract(startDate).TotalDays;

        return daysInYear;
    }

    public static int GetWeeksInYear(int year)
    {
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        DateTime date1 = new DateTime(year, 12, 31);
        Calendar cal = dfi.Calendar;

        return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
    }
}