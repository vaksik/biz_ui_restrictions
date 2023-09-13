namespace Service.Biz.UiRestrictions.DAL;

public interface IDbManager
{
    Task<bool> MigrationAllUpAsync(CancellationToken cancellationToken = default);

    Task<bool> IsServerAvailableAsync(CancellationToken cancellationToken = default);

    Task<bool> CreateDatabaseIfNotExistsAsync(CancellationToken cancellationToken = default);
}