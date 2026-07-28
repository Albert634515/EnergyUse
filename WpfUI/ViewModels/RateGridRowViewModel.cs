using EnergyUse.Models;

namespace WpfUI.ViewModels;

public sealed class RateGridRowViewModel
{
    public RateGridRowViewModel(Rate rate, bool storedPriceExcludesVat, decimal? vatPercentage)
    {
        Rate = rate;

        var futureRate = rate.FutureRate;
        if (storedPriceExcludesVat)
        {
            FutureRateExcludingVat = futureRate;

            if (vatPercentage.HasValue)
            {
                var vatFactor = 1 + (vatPercentage.Value / 100);
                RateIncludingVat = rate.RateValue * vatFactor;
                FutureRateIncludingVat = futureRate * vatFactor;
            }
        }
        else
        {
            RateIncludingVat = rate.RateValue;
            FutureRateIncludingVat = futureRate;

            if (vatPercentage.HasValue)
            {
                var vatFactor = 1 + (vatPercentage.Value / 100);
                FutureRateExcludingVat = futureRate / vatFactor;
            }
        }
    }

    public Rate Rate { get; }
    public DateTime StartRate => Rate.StartRate;
    public DateTime EndRate => Rate.EndRate;
    public decimal RateValue => Rate.RateValue;
    public decimal ExpectedPriceChange => Rate.ExpectedPriceChange;
    public decimal PriceChange => Rate.PriceChange;
    public string? Description => Rate.Description;
    public decimal? RateIncludingVat { get; }
    public decimal? FutureRateExcludingVat { get; }
    public decimal? FutureRateIncludingVat { get; }
}
