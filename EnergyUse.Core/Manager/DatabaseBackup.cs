using EnergyUse.Core.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnergyUse.Core.Manager;

public static class DatabaseBackup
{
    public static string Create(string sourceFile, string targetDirectory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(targetDirectory);
        var sourcePath = existingFile(sourceFile);
        var directory = Path.GetFullPath(targetDirectory);
        Directory.CreateDirectory(directory);
        var name = $"{Path.GetFileNameWithoutExtension(sourcePath)}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{Path.GetExtension(sourcePath)}";
        var destination = Path.Combine(directory, name);
        var staging = Path.Combine(directory, $".{Guid.NewGuid():N}.tmp");
        try
        {
            createSnapshot(sourcePath, staging);
            using (var snapshot = open(staging, SqliteOpenMode.ReadOnly))
                validate(snapshot);
            // Publish only a completed, validated backup; never overwrite an earlier backup.
            File.Move(staging, destination);
            return destination;
        }
        finally
        {
            deleteStaging(staging);
        }
    }

    public static string? Restore(string sourceFile, string destinationFile, string? backupDirectory = null)
    {
        var sourcePath = existingFile(sourceFile);
        var destinationPath = existingFile(destinationFile);
        if (string.Equals(sourcePath, destinationPath, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Select a backup other than the current database.");

        var staging = Path.Combine(Path.GetTempPath(), $"energy-restore-{Guid.NewGuid():N}.db");
        try
        {
            // Work from a private snapshot so validation and restoration use exactly the same data.
            createSnapshot(sourcePath, staging);
            using var snapshot = open(staging, SqliteOpenMode.ReadOnly);
            validate(snapshot);
            string? safetyBackup = null;
            if (backupDirectory != null)
                safetyBackup = Create(destinationPath, backupDirectory);

            using var destination = open(destinationPath, SqliteOpenMode.ReadWrite);
            using (var command = destination.CreateCommand())
            {
                command.CommandText = "PRAGMA synchronous = FULL";
                command.ExecuteNonQuery();
            }
            // SQLite holds a write transaction on the destination and rolls back a failed copy.
            // Do not replace files or delete WAL files underneath other open connections.
            snapshot.BackupDatabase(destination);
            return safetyBackup;
        }
        finally
        {
            deleteStaging(staging);
        }
    }

    private static void createSnapshot(string source, string staging)
    {
        using (new FileStream(staging, FileMode.CreateNew, FileAccess.Write, FileShare.None)) { }
        using var input = open(source, SqliteOpenMode.ReadOnly);
        using var output = open(staging, SqliteOpenMode.ReadWrite);
        input.BackupDatabase(output);
        // Backups must be portable single files, even when the source uses WAL mode.
        using var command = output.CreateCommand();
        command.CommandText = "PRAGMA journal_mode = DELETE";
        if (!string.Equals((string?)command.ExecuteScalar(), "delete", StringComparison.OrdinalIgnoreCase))
            throw new IOException("Could not create a standalone database backup.");
    }

    private static SqliteConnection open(string file, SqliteOpenMode mode)
    {
        var connection = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = file,
            Mode = mode,
            Pooling = false,
            DefaultTimeout = 5
        }.ToString());
        try
        {
            connection.Open();
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static string existingFile(string file)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(file);
        var path = Path.GetFullPath(file);
        if (!File.Exists(path))
            throw new FileNotFoundException("Database file does not exist.", path);
        return path;
    }

    private static void validate(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA integrity_check";
        using (var reader = command.ExecuteReader())
        {
            if (!reader.Read() || reader.GetString(0) != "ok" || reader.Read())
                throw new InvalidDataException("The database failed its integrity check.");
        }

        // Read model metadata without initializing or modifying the candidate database.
        using var model = new EnergyUseContext(connection.DataSource, initializeDatabase: false);
        foreach (var entity in model.Model.GetEntityTypes())
        {
            var table = entity.GetTableName()!;
            command.CommandText = $"PRAGMA table_info(\"{table.Replace("\"", "\"\"")}\")";
            var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var reader = command.ExecuteReader())
                while (reader.Read()) columns.Add(reader.GetString(1));

            // This table is added automatically when older EnergyUse databases are opened.
            if (table == "TariffGroupCostCategories" && columns.Count == 0)
                continue;
            var store = StoreObjectIdentifier.Table(table, entity.GetSchema());
            if (entity.GetProperties().Any(property => !columns.Contains(property.GetColumnName(store)!)))
                throw new InvalidDataException($"The file is not a compatible EnergyUse database: table '{table}' is missing or incomplete.");
        }

        command.CommandText = "PRAGMA foreign_key_check";
        using var foreignKeys = command.ExecuteReader();
        if (foreignKeys.Read())
            throw new InvalidDataException("The database contains invalid references between records.");
    }

    private static void deleteStaging(string path)
    {
        // Cleanup must not hide the original error or report a committed restore as a failure.
        foreach (var file in new[] { path, path + "-wal", path + "-shm", path + "-journal" })
        {
            try { File.Delete(file); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}
