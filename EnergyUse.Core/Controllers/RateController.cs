using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class RateController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Rate UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public RateController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Rate(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion

    #region Methods

    public async Task<Dictionary<Models.Rate, string>> GetRateValidationErrorsAsync(IEnumerable<Models.Rate> editedRates)
    {
        var rates = editedRates.Distinct().ToList();
        var groups = rates.GroupBy(rate => (rate.EnergyTypeId, rate.CostCategoryId, rate.TariffGroupId))
            .Select(group => group.Key).ToList();
        foreach (var group in groups)
        {
            var storedRates = await UnitOfWork.RateRepo.SelectByCostCategoryAndEnergyTypeAndTarifGroup(
                group.CostCategoryId ?? 0, group.EnergyTypeId ?? 0, group.TariffGroupId ?? 0);
            foreach (var storedRate in storedRates)
                if (!rates.Any(rate => ReferenceEquals(rate, storedRate)
                    || (rate.Id > 0 && rate.Id == storedRate.Id)))
                    rates.Add(storedRate);
        }
        var errors = ValidateRates(rates);
        foreach (var rate in editedRates.Distinct())
        {
            var allowedGroups = await UnitOfWork.CostCategoryRepo.GetAllowedTariffGroupIds(rate.CostCategoryId ?? 0);
            if (!allowedGroups.Contains(rate.TariffGroupId ?? 0))
            {
                var message = "This cost category is not allowed for the selected tariff group.";
                errors[rate] = errors.TryGetValue(rate, out var existing) ? existing + Environment.NewLine + message : message;
            }
        }
        return errors;
    }

    public static Dictionary<Models.Rate, string> ValidateRates(IEnumerable<Models.Rate> rates)
    {
        var errors = new Dictionary<Models.Rate, List<string>>();
        void addError(Models.Rate rate, string message)
        {
            if (!errors.TryGetValue(rate, out var messages))
                errors[rate] = messages = new List<string>();
            messages.Add(message);
        }

        var rateList = rates.Distinct().ToList();
        foreach (var rate in rateList)
        {
            if (rate.StartRate == DateTime.MinValue || rate.EndRate == DateTime.MinValue)
                addError(rate, "A start and end date are required.");
            else if (rate.EndRate.Date < rate.StartRate.Date)
                addError(rate, "The end date is before the start date.");
            if (rate.EnergyTypeId is null or <= 0 || rate.CostCategoryId is null or <= 0
                || rate.TariffGroupId is null or <= 0)
                addError(rate, "An energy type, cost category and tariff group are required.");
            if (rate.RateTypeId is not 1 and not 2)
                addError(rate, "Select a valid rate type.");
        }

        foreach (var group in rateList.GroupBy(rate => (rate.EnergyTypeId, rate.CostCategoryId, rate.TariffGroupId)))
        {
            var ordered = group.Where(rate => rate.StartRate != DateTime.MinValue
                    && rate.EndRate != DateTime.MinValue && rate.EndRate.Date >= rate.StartRate.Date)
                .OrderBy(rate => rate.StartRate.Date).ToList();
            for (var index = 0; index < ordered.Count; index++)
            {
                var rate = ordered[index];
                foreach (var other in ordered.Skip(index + 1).TakeWhile(other => other.StartRate.Date <= rate.EndRate.Date))
                {
                    addError(rate, $"Overlaps rate {other.Id}: {other.StartRate:dd-MM-yyyy} through {other.EndRate:dd-MM-yyyy}.");
                    addError(other, $"Overlaps rate {rate.Id}: {rate.StartRate:dd-MM-yyyy} through {rate.EndRate:dd-MM-yyyy}.");
                }
            }

            Models.Rate? previous = null;
            foreach (var rate in ordered)
            {
                if (previous != null && (rate.StartRate.Date - previous.EndRate.Date).Days > 1)
                {
                    var message = $"Missing rate period: {previous.EndRate.AddDays(1):dd-MM-yyyy} through {rate.StartRate.AddDays(-1):dd-MM-yyyy}.";
                    addError(previous, message);
                    addError(rate, message);
                }
                if (previous == null || rate.EndRate.Date > previous.EndRate.Date)
                    previous = rate;
            }
        }
        return errors.ToDictionary(item => item.Key, item => string.Join(Environment.NewLine, item.Value));
    }

    public async Task<decimal> GetPriceChange(EnergyUse.Models.Rate rate)
    {
        decimal priceChange = 0;

        if (UnitOfWork?.RateRepo == null)
        {
            throw new InvalidOperationException("UnitOfWork or RateRepo is not initialized.");
        }

        if (!rate.EnergyTypeId.HasValue
            || !rate.CostCategoryId.HasValue
            || !rate.TariffGroupId.HasValue)
        {
            return priceChange;
        }

        var previousRate = await UnitOfWork.RateRepo.SelectLastRateByDate(
            rate.EnergyTypeId.Value,
            rate.CostCategoryId.Value,
            rate.StartRate.AddDays(-1),
            rate.TariffGroupId.Value);
        if (previousRate != null && previousRate.Id > 0 && previousRate.RateValue != 0)
            priceChange = Math.Round(((rate.RateValue - previousRate.RateValue) / previousRate.RateValue) * 100, 2);

        return priceChange;
    }

    public async Task<EnergyUse.Models.Common.RateTaxInfo> GetRateIncExTax(Models.CostCategory costCategory, Models.Rate rate)
    {
        var rateTaxInfo = new EnergyUse.Models.Common.RateTaxInfo();

        if (UnitOfWork?.RateRepo == null)
        {
            throw new InvalidOperationException("UnitOfWork or RateRepo is not initialized.");
        }

        if (rate == null || costCategory == null)
            return rateTaxInfo;

        var vatTarif = await UnitOfWork.RepoVatTarif.GetByCostCategoryIdAndDate(costCategory.Id, rate.StartRate);
        if (vatTarif == null)
            return rateTaxInfo;            

        if (costCategory.CalculateVat == true)
        {
            //Rate price is ex taxes
            rateTaxInfo.Tax = rate.RateValue * vatTarif.Tarif / 100;                
        }
        else
        {
            //Rate price is inc. taxes
            rateTaxInfo.Tax = rate.RateValue / (1 + vatTarif.Tarif / 100);
        }

        rateTaxInfo.Tax = Math.Round(rateTaxInfo.Tax, 4);
        rateTaxInfo.IsTaxIncluded = costCategory.CalculateVat;
        rateTaxInfo.RateIncTax = rate.RateValue + rateTaxInfo.Tax;

        return rateTaxInfo;
    }

    #endregion
}
