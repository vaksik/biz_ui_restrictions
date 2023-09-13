namespace Service.Biz.UiRestrictions.BLL.Services;

public interface IOrganizationService
{
    Task<IEnumerable<Guid>> GetOrganizationsWithProductAsync(Guid[]? networkIds, Guid[]? organizationIds,
        CancellationToken cancellationToken = default);
    Task UpdateOrganizationProductAsync(Guid organizationId, string productCode, Guid? networkId,
        CancellationToken cancellationToken = default);
}