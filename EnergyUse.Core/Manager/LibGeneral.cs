namespace EnergyUse.Core.Manager;

public class LibGeneral
{
    public static int MonthDiff(DateTime startDate, DateTime endDate)
    {
        return endDate.Month - startDate.Month;
    }
}