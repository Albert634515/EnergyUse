using EnergyUse.Models;

namespace WpfUI.ViewModels;

public sealed class RateGridRowViewModel : ViewModelBase
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
    private string _validationError = string.Empty;
    public string ValidationError
    {
        get => _validationError;
        set
        {
            if (SetProperty(ref _validationError, value))
                OnPropertyChanged(nameof(HasValidationError));
        }
    }
    public bool HasValidationError => !string.IsNullOrWhiteSpace(ValidationError);

    public void RefreshValidation(string error)
    {
        ValidationError = error;
        OnPropertyChanged(nameof(StartRate));
        OnPropertyChanged(nameof(EndRate));
        OnPropertyChanged(nameof(RateValue));
        OnPropertyChanged(nameof(ExpectedPriceChange));
        OnPropertyChanged(nameof(PriceChange));
        OnPropertyChanged(nameof(Description));
    }
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
