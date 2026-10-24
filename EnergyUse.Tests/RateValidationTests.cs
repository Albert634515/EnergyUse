using EnergyUse.Core.Controllers;
using EnergyUse.Models;
using Xunit;

namespace EnergyUse.Tests;

public class RateValidationTests
{
    [Fact]
    public void ExistingOverlappingRecordsWithDifferentPricesBothHaveErrors()
    {
        var first = rate(156, new DateTime(2025, 1, 1), new DateTime(2025, 11, 30));
        var second = rate(82, new DateTime(2025, 10, 1), new DateTime(2025, 10, 30));
        first.RateValue = 0.09462m;
        second.RateValue = 0.12782m;
        var errors = RateController.ValidateRates([first, second]);
        Assert.Equal(2, errors.Count);
        Assert.Contains("82", errors[first]);
        Assert.Contains("156", errors[second]);
    }

    [Fact]
    public void OverlapElsewhereInTheListIsDetectedEvenWhenSelectedRecordIsValid()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 3, 31));
        var second = rate(2, new DateTime(2025, 3, 1), new DateTime(2025, 3, 31));
        var selected = rate(3, new DateTime(2025, 4, 1), new DateTime(2025, 5, 31));
        var errors = RateController.ValidateRates([first, second, selected]);
        Assert.Equal(2, errors.Count);
        Assert.False(errors.ContainsKey(selected));
    }

    [Fact]
    public void AdjacentInclusivePeriodsAreValidButSharedBoundaryIsOverlap()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31));
        var second = rate(2, new DateTime(2025, 2, 1), new DateTime(2025, 2, 28));
        Assert.Empty(RateController.ValidateRates([first, second]));
        second.StartRate = first.EndRate;
        Assert.Equal(2, RateController.ValidateRates([first, second]).Count);
    }

    [Fact]
    public void GapsMarkBothAdjacentRows()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31));
        var second = rate(2, new DateTime(2025, 3, 1), new DateTime(2025, 3, 31));
        var errors = RateController.ValidateRates([first, second]);
        Assert.Contains("01-02-2025 through 28-02-2025", errors[first]);
        Assert.Equal(errors[first], errors[second]);
    }

    [Fact]
    public void NestedOverlapDoesNotCreateFalseGap()
    {
        var covering = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 6, 30));
        var nested = rate(2, new DateTime(2025, 2, 1), new DateTime(2025, 2, 28));
        var following = rate(3, new DateTime(2025, 7, 1), new DateTime(2025, 7, 31));
        var errors = RateController.ValidateRates([covering, nested, following]);
        Assert.False(errors.ContainsKey(following));
        Assert.All(errors.Values, error => Assert.DoesNotContain("Missing", error));
    }

    [Fact]
    public void DifferentCategoryEnergyOrTariffGroupIsNotAnOverlap()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 12, 31));
        var second = rate(2, first.StartRate, first.EndRate);
        second.TariffGroupId = 2;
        var third = rate(3, first.StartRate, first.EndRate);
        third.CostCategoryId = 2;
        var fourth = rate(4, first.StartRate, first.EndRate);
        fourth.EnergyTypeId = 2;
        Assert.Empty(RateController.ValidateRates([first, second, third, fourth]));
    }

    [Fact]
    public void InvalidDatesAndMissingRequiredSelectionsAreMarked()
    {
        var invalid = rate(1, new DateTime(2025, 2, 1), new DateTime(2025, 1, 1));
        invalid.TariffGroupId = null;
        invalid.RateTypeId = 0;
        var error = Assert.Single(RateController.ValidateRates([invalid])).Value;
        Assert.Contains("end date is before", error);
        Assert.Contains("tariff group", error);
        Assert.Contains("rate type", error);
    }

    [Fact]
    public void CorrectingDatesClearsPreviousValidationErrors()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 11, 30));
        var second = rate(2, new DateTime(2025, 10, 1), new DateTime(2025, 12, 31));
        Assert.Equal(2, RateController.ValidateRates([first, second]).Count);
        first.EndRate = second.StartRate.AddDays(-1);
        Assert.Empty(RateController.ValidateRates([first, second]));
    }

    [Fact]
    public void DuplicateReferenceDoesNotOverlapItselfAndZeroOrNegativePricesAreAllowed()
    {
        var first = rate(1, new DateTime(2025, 1, 1), new DateTime(2025, 1, 31));
        first.RateValue = 0m;
        var second = rate(2, new DateTime(2025, 2, 1), new DateTime(2025, 2, 28));
        second.RateValue = -0.2m;
        Assert.Empty(RateController.ValidateRates([first, first, second]));
    }

    private static Rate rate(long id, DateTime start, DateTime end) => new()
    {
        Id = id, EnergyTypeId = 1, CostCategoryId = 1, TariffGroupId = 1,
        RateTypeId = 1, StartRate = start, EndRate = end
    };
}
