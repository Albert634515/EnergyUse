using EnergyUse.Core.Context;
using Xunit;

namespace EnergyUse.Tests;

public class CalculatedUnitPriceTests : IDisposable
{
    private readonly string _path = Path.Combine(Path.GetTempPath(), $"energy-unit-price-{Guid.NewGuid():N}.db");

    [Fact]
    public async Task AddEditCancelAndDeletePersistCorrectly()
    {
        using var work = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_path);
        var energyType = (await work.EnergyTypeRepo.GetAll()).First();
        var price = await work.AddDefaultEntity(energyType.Id);
        Assert.Null(price.TariffGroupId);
        price.Price = 0.31415m;
        work.Complete();

        price.Price = 0.99m;
        work.CancelChanges();
        Assert.Equal(0.31415m, price.Price);

        var next = await work.AddDefaultEntity(energyType.Id);
        Assert.Equal(price.Year + 1, next.Year);
        work.CancelChanges();
        Assert.Single(await work.CalculatedUnitPriceRepo.SelectByEnergyType(energyType.Id));

        work.Delete(price);
        work.CancelChanges();
        Assert.Single(await work.CalculatedUnitPriceRepo.SelectByEnergyType(energyType.Id));

        work.Delete(price);
        work.Complete();
        using var verify = new EnergyUseContext(_path);
        Assert.Empty(verify.Set<EnergyUse.Models.CalculatedUnitPrice>());
    }

    [Fact]
    public async Task DeletingUnsavedRowDoesNotAffectOtherPendingRows()
    {
        using var work = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_path);
        var energyType = (await work.EnergyTypeRepo.GetAll()).First();
        var keep = await work.AddDefaultEntity(energyType.Id);
        keep.Price = 0.25m;
        var remove = await work.AddDefaultEntity(energyType.Id);
        work.Delete(remove);
        work.Complete();

        using var verify = new EnergyUseContext(_path);
        Assert.Equal(0.25m, Assert.Single(verify.Set<EnergyUse.Models.CalculatedUnitPrice>()).Price);
    }

    [Fact]
    public async Task YearlyPricesAndAverageAreIndependentOfTariffGroup()
    {
        using var work = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_path);
        var price = await work.AddDefaultEntity(1);
        price.Year = 2024;
        price.Price = .3m;
        var next = await work.AddDefaultEntity(1);
        next.Year = 2025;
        next.Price = .5m;
        work.Complete();
        Assert.Equal(.3m, (await work.CalculatedUnitPriceRepo.GetByYear(2024, 1, 999))!.Price);
        Assert.Equal(.4m, await work.CalculatedUnitPriceRepo.GetByAverage(1, 999));
        Assert.Empty(await work.CalculatedUnitPriceRepo.SelectByEnergyType(2));
        Assert.Equal(-1m, await work.CalculatedUnitPriceRepo.GetByAverage(2));
    }

    [Fact]
    public async Task ConflictingLegacyPricesRequireCorrectionAndEqualDuplicatesCountOnce()
    {
        using var work = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_path);
        var first = await work.AddDefaultEntity(1);
        first.Year = 2024;
        first.Price = .3m;
        var duplicate = await work.AddDefaultEntity(1);
        duplicate.Year = 2024;
        duplicate.Price = .6m;
        var next = await work.AddDefaultEntity(1);
        next.Year = 2025;
        next.Price = .5m;
        work.Complete();
        await Assert.ThrowsAsync<EnergyUse.Core.Manager.RateValidationException>(
            () => work.CalculatedUnitPriceRepo.GetByYear(2024, 1));
        await Assert.ThrowsAsync<EnergyUse.Core.Manager.RateValidationException>(
            () => work.CalculatedUnitPriceRepo.GetByAverage(1));
        duplicate.Price = .3m;
        work.Complete();
        Assert.Equal(.4m, await work.CalculatedUnitPriceRepo.GetByAverage(1));
    }

    public void Dispose()
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        File.Delete(_path);
    }
}
