using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager;

public class LibSelectionItemList
{
    public static List<SelectionItem> GetRateTypeList()
    {
        var rateList = new List<SelectionItem>();

        foreach (Common.Enums.RateType rateType in Enum.GetValues(typeof(Common.Enums.RateType)))
        {           
            var item = new SelectionItem((int)rateType, rateType.ToString(), rateType.GetDescription());
            rateList.Add(item);
        }

        return rateList;
    }

    public static List<SelectionItem> GetPeriodList()
    {
        var periodList = new List<SelectionItem>();

        foreach (Common.Enums.Period periodType in Enum.GetValues(typeof(Common.Enums.Period)))
        {
            if (periodType != Period.SettlementDay && periodType != Period.Unknown)
            {
                periodList.Add(new Models.Common.SelectionItem((int)periodType, periodType.ToString(), periodType.GetDescription()));
            }
        }

        return periodList;
    }

    public static List<SelectionItem> GetTariffGroupTypeList()
    {
        var values = Enum.GetValues(typeof(TariffGroupType));
        var items = new List<SelectionItem>();

        foreach (TariffGroupType tariff in Enum.GetValues(typeof(TariffGroupType)))
        {
            items.Add(new SelectionItem((int)tariff, tariff.ToString(), tariff.GetDescription()));
        }

        return items;
    }

    public static List<SelectionItem> GetReportTypeList()
    {
        var values = Enum.GetValues(typeof(ReportType));
        var items = new List<SelectionItem>();

        foreach (ReportType report in Enum.GetValues(typeof(ReportType)))
        {
            if (report == ReportType.None) continue;

            items.Add(new SelectionItem((int)report, report.ToString(), report.GetDescription()));
        }

        return items;
    }
}