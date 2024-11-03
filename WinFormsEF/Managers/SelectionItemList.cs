using EnergyUse.Models.Common;

namespace WinFormsEF.Managers;

public class SelectionItemList
{
    public static List<SelectionItem> GetRateTypeList()
    {
        var list = EnergyUse.Core.Manager.LibSelectionItemList.GetRateTypeList();

        // Add translationss
        foreach (var item in list)
            item.Description = Languages.GetResourceString(item.Key, item.Description);

        return list;
    }

    public static List<SelectionItem> GetPeriodList()
    {
        var list = EnergyUse.Core.Manager.LibSelectionItemList.GetPeriodList();

        // Add translationss
        foreach (var item in list)
            item.Description = Languages.GetResourceString(item.Key, item.Key);

        return list;
    }
}