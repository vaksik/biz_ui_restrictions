using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Service.Biz.UiRestrictions.DAL;

public class DbManager : IDbManager
{
    private readonly IDbContextFactory<BizUiRestrictionsDataContext> contextFactory;

    public DbManager(IDbContextFactory<BizUiRestrictionsDataContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async Task<bool> MigrationAllUpAsync(CancellationToken cancellationToken = default)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync(cancellationToken);
        await dbContext.Database.MigrateAsync(cancellationToken);
        await using var conn = (NpgsqlConnection)dbContext.Database.GetDbConnection();
        await conn.OpenAsync(cancellationToken);
        await conn.ReloadTypesAsync();
        return true;
    }

    public async Task<bool> IsServerAvailableAsync(CancellationToken cancellationToken = default)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Database.CanConnectAsync(cancellationToken);
    }

    public async Task<bool> CreateDatabaseIfNotExistsAsync(CancellationToken cancellationToken = default)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync(cancellationToken);
        if (await dbContext.Database.CanConnectAsync(cancellationToken))
            return true;

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(dbContext.Database.GetConnectionString());
        var database = connectionStringBuilder.Database
                       ?? throw new InvalidOperationException("Database name not specified in connection string");

        connectionStringBuilder.Database = "postgres";
        await using var connection = new NpgsqlConnection(connectionStringBuilder.ToString());

        await connection.OpenAsync(cancellationToken);

        var command = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @datname", connection);
        command.Parameters.AddWithValue("datname", database.ToLower());
        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result == null)
            await new NpgsqlCommand($"CREATE DATABASE {database}", connection).ExecuteNonQueryAsync(cancellationToken);

        return true;
    }
}