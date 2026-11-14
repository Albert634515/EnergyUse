using EnergyUse.Core.Context;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Manager;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EnergyUse.Tests;

public class DatabaseBackupTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), $"energy-backup-tests-{Guid.NewGuid():N}");
    private string database => Path.Combine(_directory, "current.db");
    private string backups => Path.Combine(_directory, "backups");

    public DatabaseBackupTests()
    {
        Directory.CreateDirectory(_directory);
        using var context = new EnergyUseContext(database);
        using var connection = open(database);
        execute(connection, "CREATE TABLE BackupTest (Value TEXT NOT NULL); INSERT INTO BackupTest VALUES ('original')");
    }

    [Fact]
    public void BackupIncludesCommittedWalDataAndDoesNotOverwriteEarlierBackups()
    {
        using var live = open(database);
        execute(live, "PRAGMA journal_mode=WAL; PRAGMA wal_autocheckpoint=0; UPDATE BackupTest SET Value='in WAL'");
        Assert.True(new FileInfo(database + "-wal").Length > 0);

        var first = DatabaseBackup.Create(database, backups);
        execute(live, "UPDATE BackupTest SET Value='newer'");
        var second = DatabaseBackup.Create(database, backups);

        Assert.NotEqual(first, second);
        Assert.Equal("in WAL", read(first));
        Assert.Equal("newer", read(second));
        Assert.Equal(2, Directory.GetFiles(backups).Length);
    }

    [Fact]
    public void RestoreReplacesDataWhileOtherConnectionsAreOpenAndKeepsSafetyBackup()
    {
        var backup = DatabaseBackup.Create(database, backups);
        using var live = open(database);
        execute(live, "PRAGMA journal_mode=WAL; UPDATE BackupTest SET Value='changed'");

        var safety = DatabaseBackup.Restore(backup, database, backups);

        Assert.NotNull(safety);
        Assert.Equal("changed", read(safety!));
        Assert.Equal("original", read(database));
        Assert.Equal("original", scalar(live, "SELECT Value FROM BackupTest"));
        Assert.Equal("original", read(backup));
    }

    [Fact]
    public void RestoreCanSkipSafetyBackup()
    {
        var backup = DatabaseBackup.Create(database, backups);
        using (var connection = open(database)) execute(connection, "UPDATE BackupTest SET Value='changed'");
        Assert.Null(DatabaseBackup.Restore(backup, database));
        Assert.Equal("original", read(database));
        Assert.Single(Directory.GetFiles(backups));
    }

    [Theory]
    [InlineData("text")]
    [InlineData("empty")]
    [InlineData("unrelated")]
    [InlineData("missing column")]
    [InlineData("invalid references")]
    public void InvalidRestoreLeavesCurrentDatabaseAndBackupDirectoryUntouched(string kind)
    {
        var invalid = Path.Combine(_directory, "invalid.db");
        if (kind == "text") File.WriteAllText(invalid, "not a database");
        else if (kind == "empty") File.WriteAllBytes(invalid, []);
        else if (kind == "unrelated")
        {
            using var connection = open(invalid);
            execute(connection, "CREATE TABLE SomethingElse (Id INTEGER)");
        }
        else
        {
            invalid = DatabaseBackup.Create(database, backups);
            using var connection = open(invalid);
            if (kind == "missing column") execute(connection, "ALTER TABLE Addresses DROP COLUMN City");
            else execute(connection, "PRAGMA foreign_keys=OFF; INSERT INTO Meters (Description, Number, ActiveFrom, EnergyTypeId, AddressId) VALUES ('Invalid', '1', '2026-01-01', 999, 999)");
        }

        var safetyDirectory = Path.Combine(_directory, "safety");
        Assert.ThrowsAny<Exception>(() => DatabaseBackup.Restore(invalid, database, safetyDirectory));
        Assert.Equal("original", read(database));
        Assert.False(Directory.Exists(safetyDirectory));
    }

    [Fact]
    public void FailedSafetyBackupAbortsRestore()
    {
        var backup = DatabaseBackup.Create(database, backups);
        using (var connection = open(database)) execute(connection, "UPDATE BackupTest SET Value='changed'");
        var notDirectory = Path.Combine(_directory, "file");
        File.WriteAllText(notDirectory, "occupied");

        Assert.ThrowsAny<IOException>(() => DatabaseBackup.Restore(backup, database, notDirectory));
        Assert.Equal("changed", read(database));
    }

    [Fact]
    public void LockedDestinationFailsWithoutChangingEitherDatabase()
    {
        var backup = DatabaseBackup.Create(database, backups);
        using var connection = open(database);
        execute(connection, "UPDATE BackupTest SET Value='changed'");
        using (var transaction = connection.BeginTransaction())
        {
            Assert.Throws<SqliteException>(() => DatabaseBackup.Restore(backup, database));
            transaction.Rollback();
        }
        Assert.Equal("changed", read(database));
        Assert.Equal("original", read(backup));
    }

    [Fact]
    public void MissingSourceIsNotCreatedAndSameFileRestoreIsRejected()
    {
        var missing = Path.Combine(_directory, "missing.db");
        Assert.Throws<FileNotFoundException>(() => DatabaseBackup.Create(missing, backups));
        Assert.False(File.Exists(missing));
        Assert.Throws<FileNotFoundException>(() => DatabaseBackup.Restore(missing, database));
        Assert.Throws<InvalidOperationException>(() => DatabaseBackup.Restore(database, database));
        Assert.Equal("original", read(database));
    }

    [Fact]
    public void InvalidBackupIsNotPublishedAndStagingIsRemoved()
    {
        var invalid = Path.Combine(_directory, "invalid.db");
        File.WriteAllText(invalid, "not SQLite");
        Assert.Throws<SqliteException>(() => DatabaseBackup.Create(invalid, backups));
        Assert.Empty(Directory.GetFiles(backups));
    }

    [Fact]
    public void LegacyDatabaseWithoutCategoryJoinTableCanBeRestoredAndUpgraded()
    {
        var backup = DatabaseBackup.Create(database, backups);
        using (var connection = open(backup)) execute(connection, "DROP TABLE TariffGroupCostCategories");
        DatabaseBackup.Restore(backup, database);
        using var upgraded = new EnergyUseContext(database);
        Assert.Empty(upgraded.TariffGroupCostCategories.ToList());
    }

    [Fact]
    public void ControllerRequiresBackupDirectoryWhenSafetyBackupIsRequested()
    {
        var controller = new BackUpAndRestoreController(database);
        var backup = controller.CreateBackUpFile(backups, database);
        Assert.Throws<ArgumentException>(() => controller.RestoreBackUpFile(backup, true, " "));
        Assert.Equal("original", read(database));
    }

    private static SqliteConnection open(string path)
    {
        var connection = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = path, Pooling = false, DefaultTimeout = 1
        }.ToString());
        connection.Open();
        return connection;
    }

    private static void execute(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private static string scalar(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return (string)command.ExecuteScalar()!;
    }

    private static string read(string path)
    {
        using var connection = open(path);
        return scalar(connection, "SELECT Value FROM BackupTest");
    }

    public void Dispose()
    {
        SqliteConnection.ClearAllPools();
        Directory.Delete(_directory, recursive: true);
    }
}
