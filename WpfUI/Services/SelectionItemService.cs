using System.Collections.Generic;
using EnergyUse.Models.Common;

namespace WpfUI.Services
{
    public class SelectionItemService
    {
        private readonly ILanguageService _languageService;

        public SelectionItemService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public List<SelectionItem> GetRateTypeList()
        {
            var list = EnergyUse.Core.Manager.LibSelectionItemList.GetRateTypeList();

            foreach (var item in list)
                item.Description = _languageService.Translate(item.Key, item.Description);

            return list;
        }

        public List<SelectionItem> GetPeriodList()
        {
            var list = EnergyUse.Core.Manager.LibSelectionItemList.GetPeriodList();

            foreach (var item in list)
                item.Description = _languageService.Translate(item.Key, item.Key);

            return list;
        }

        public List<SelectionItem> GetTariffGroupTypeList()
        {
            var list = EnergyUse.Core.Manager.LibSelectionItemList.GetTariffGroupTypeList();

            foreach (var item in list)
                item.Description = _languageService.Translate(item.Key, item.Key);

            return list;
        }

        public List<SelectionItem> GetReportTypeList()
        {
            var list = EnergyUse.Core.Manager.LibSelectionItemList.GetReportTypeList();

            foreach (var item in list)
                item.Description = _languageService.Translate(item.Key, item.Key);

            return list;
        }
    }
}