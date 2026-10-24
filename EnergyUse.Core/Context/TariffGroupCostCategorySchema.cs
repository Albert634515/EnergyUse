using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnergyUse.Core.Context;

internal static class TariffGroupCostCategorySchema
{
    public static void EnsureCreated(EnergyUseContext context, bool newDatabase)
    {
        context.Database.OpenConnection();
        try
        {
            using var transaction = context.Database.BeginTransaction();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.Transaction = transaction.GetDbTransaction();
            command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'TariffGroupCostCategories'";
            var exists = Convert.ToInt64(command.ExecuteScalar()) > 0;
            if (!exists)
            {
                context.Database.ExecuteSqlRaw("""
                    CREATE TABLE TariffGroupCostCategories (
                        TariffGroupId INTEGER NOT NULL,
                        CostCategoryId INTEGER NOT NULL,
                        PRIMARY KEY (TariffGroupId, CostCategoryId),
                        FOREIGN KEY (TariffGroupId) REFERENCES TariffGroups (Id) ON DELETE CASCADE,
                        FOREIGN KEY (CostCategoryId) REFERENCES CostCategories (Id) ON DELETE CASCADE
                    );
                    CREATE INDEX IX_TariffGroupCostCategories_CostCategoryId ON TariffGroupCostCategories (CostCategoryId);
                    """);
            }
            if (!exists || newDatabase)
            {
                context.Database.ExecuteSqlRaw("""
                    INSERT OR IGNORE INTO TariffGroupCostCategories (TariffGroupId, CostCategoryId)
                    SELECT tg.Id, cc.Id FROM TariffGroups tg
                    JOIN CostCategories cc ON cc.TariffGroupTypeId = tg.TypeId
                    UNION
                    SELECT r.TariffGroupId, r.CostCategoryId FROM Rates r
                    JOIN TariffGroups tg ON tg.Id = r.TariffGroupId
                    JOIN CostCategories cc ON cc.Id = r.CostCategoryId;
                    """);
            }
            transaction.Commit();
        }
        finally { context.Database.CloseConnection(); }
    }
}