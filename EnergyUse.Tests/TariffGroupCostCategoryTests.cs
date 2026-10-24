using EnergyUse.Core.Context;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Repositories;
using EnergyUse.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EnergyUse.Tests;

public class TariffGroupCostCategoryTests : IDisposable
{
    private readonly string _path = Path.Combine(Path.GetTempPath(), $"energy-categories-{Guid.NewGuid():N}.db");

    private long addCategory(EnergyUseContext context, int typeId = 2)
    {
        var category = new CostCategory { Name = "Category", EnergyTypeId = 1, TariffGroupTypeId = typeId };
        context.CostCategories.Add(category);
        context.SaveChanges();
        return category.Id;
    }

    [Fact]
    public async Task UpgradePreservesTypeChoicesAndExistingRatePairsOnlyOnce()
    {
        long categoryId;
        using (var context = new EnergyUseContext(_path))
        {
            categoryId = addCategory(context);
            context.Rates.Add(new Rate { CostCategoryId = categoryId, TariffGroupId = 1, EnergyTypeId = 1,
                StartRate = new DateTime(2026, 1, 1), EndRate = new DateTime(2026, 12, 31), RateTypeId = 1 });
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DROP TABLE TariffGroupCostCategories");
        }
        using (var context = new EnergyUseContext(_path))
        {
            Assert.Equal(2, context.TariffGroupCostCategories.Count());
            var repository = new RepoCostCategories(context);
            Assert.Equal(categoryId, Assert.Single(await repository.SelectByTariffGroup(1, 1)).Id);
            Assert.Empty(await repository.SelectByTariffGroup(1, 2));
            context.TariffGroupCostCategories.RemoveRange(context.TariffGroupCostCategories);
            context.SaveChanges();
        }
        using var reopened = new EnergyUseContext(_path);
        Assert.Empty(reopened.TariffGroupCostCategories);
    }

    [Fact]
    public async Task CheckboxesSupportNewGroupsSaveAndCancel()
    {
        long categoryId;
        using (var context = new EnergyUseContext(_path)) categoryId = addCategory(context);
        using var work = new EnergyUse.Core.UnitOfWork.TariffGroup(_path);
        var group = work.SetDefaultEntity("New group");
        work.SetCostCategoryAllowed(group, categoryId, true);
        work.Complete();
        Assert.True(work.IsCostCategoryAllowed(group, categoryId));
        work.SetCostCategoryAllowed(group, categoryId, false);
        work.SetCostCategoryAllowed(group, categoryId, true);
        Assert.False(work.HasChanges());
        work.SetCostCategoryAllowed(group, categoryId, false);
        Assert.True(work.HasChanges());
        work.CancelChanges();
        var reloaded = (await work.TariffGroupRepo.SelectById(group.Id))!;
        Assert.True(work.IsCostCategoryAllowed(reloaded, categoryId));
        work.SetCostCategoryAllowed(reloaded, categoryId, false);
        work.Complete();
        using var verify = new EnergyUseContext(_path);
        Assert.Empty(verify.TariffGroupCostCategories);
    }

    [Fact]
    public async Task OpenRateRepositoryReflectsCheckboxChangesSavedInAnotherWindow()
    {
        long categoryId;
        using (var setup = new EnergyUseContext(_path)) categoryId = addCategory(setup);
        using var groups = new EnergyUse.Core.UnitOfWork.TariffGroup(_path);
        var group = (await groups.TariffGroupRepo.SelectById(2))!;
        groups.SetCostCategoryAllowed(group, categoryId, true);
        groups.Complete();

        using var rateContext = new EnergyUseContext(_path);
        var repository = new RepoCostCategories(rateContext);
        Assert.Equal(categoryId, Assert.Single(await repository.SelectByTariffGroup(2, 1)).Id);

        groups.SetCostCategoryAllowed(group, categoryId, false);
        groups.Complete();
        Assert.Empty(await repository.SelectByTariffGroup(2, 1));
        using var reopened = new EnergyUseContext(_path);
        Assert.Empty(await new RepoCostCategories(reopened).SelectByTariffGroup(2, 1));
    }

    [Fact]
    public async Task RateValidationRejectsUnassignedGroup()
    {
        long categoryId;
        using (var context = new EnergyUseContext(_path)) categoryId = addCategory(context);
        var controller = new RateController(_path);
        using var work = controller.UnitOfWork;
        var rate = new Rate { CostCategoryId = categoryId, EnergyTypeId = 1, TariffGroupId = 2,
            StartRate = new DateTime(2026, 1, 1), EndRate = new DateTime(2026, 12, 31), RateTypeId = 1 };
        var errors = await controller.GetRateValidationErrorsAsync([rate]);
        Assert.Contains("not allowed", errors[rate]);
    }

    public void Dispose()
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        File.Delete(_path);
    }
}
