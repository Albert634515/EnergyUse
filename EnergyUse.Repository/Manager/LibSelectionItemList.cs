using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;

namespace EnergyUse.Core.Manager
{
    public class LibSelectionItemList
    {
        public static List<Models.Common.SelectionItem> GetRateList()
        {
            var rateList = new List<Models.Common.SelectionItem>();

            foreach (Common.Enums.RateType rateType in Enum.GetValues(typeof(Common.Enums.RateType)))
            {
                var item = new Models.Common.SelectionItem();
                item.Id = (int)rateType;
                item.Key = rateType.ToString();                
                item.Description = rateType.GetDescription();
                rateList.Add(item);
            }

            return rateList;
        }

        public static List<Models.Common.SelectionItem> GetPeriodList()
        {
            var periodList = new List<Models.Common.SelectionItem>();

            foreach (Common.Enums.Period periodType in Enum.GetValues(typeof(Common.Enums.Period)))
            {
                if (periodType != Period.SettlementDay && periodType != Period.Unknown)
                {
                    var item = new Models.Common.SelectionItem();
                    item.Id = (int)periodType;
                    item.Key = periodType.ToString();
                    item.Description = periodType.GetDescription();
                    periodList.Add(item);
                }
            }

            return periodList;
        }
    }
}