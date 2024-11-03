namespace EnergyUse.Common.Extensions;

public static class StringExtentions
{
    public static bool IsNumeric(this string text)
    {
        bool isNum = Decimal.TryParse(Convert.ToString(text), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out decimal retNum);
        return isNum;
    }
}