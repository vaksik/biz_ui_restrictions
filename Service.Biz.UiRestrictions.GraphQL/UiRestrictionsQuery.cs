using Api.Biz.Contracts.Http.Dto.Data.Accounts;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Module.Gql.Federation;
using Service.Biz.UiRestrictions.BLL.Dto;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Biz.UiRestrictions.BLL.Services.Access;
using Service.Biz.UiRestrictions.BLL.Services.Access.Enumerations;
using Service.Biz.UiRestrictions.BLL.Services.Access.Extensions;
using Service.Biz.UiRestrictions.DAL.Entities;
using Service.Biz.UiRestrictions.GraphQL.Dto;
using Service.Biz.UiRestrictions.GraphQL.Filters;
using Service.Biz.UiRestrictions.GraphQL.Mappers;
using Service.Biz.UiRestrictions.GraphQL.Types;

namespace Service.Biz.UiRestrictions.GraphQL;

public class UiRestrictionsQuery : FederatedQuery<UiRestrictionsFederationEntityType>
{
    private readonly IUserAccessService<Guid, OrganizationMainDataAccess> _userAccessService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFeatureRestrictionManagerService _featureRestrictionManagerService;

    public UiRestrictionsQuery(IUserAccessService<Guid, OrganizationMainDataAccess> userAccessService,
        IHttpContextAccessor httpContextAccessor, IFeatureRestrictionManagerService featureRestrictionManagerService)
    {
        _userAccessService = userAccessService;
        _httpContextAccessor = httpContextAccessor;
        _featureRestrictionManagerService = featureRestrictionManagerService;
        
        Field<NonNullGraphType<UiRestrictionsOutputType>>()
            .Name("getFeaturesAccess")//todo rename on getFeaturesRestrictions
            .Argument<NonNullGraphType<ClientFilterInputType>>(InputNamingConvention.Client)
            .Argument<NonNullGraphType<FeatureFilterInputType>>(InputNamingConvention.Filtering)
            .ResolveAsync(UiRestrictionsQueryResolver);
    }
    
    private async Task<object> UiRestrictionsQueryResolver(IResolveFieldContext<object> arg)
    {
        var client = arg.GetArgument<ClientFilter>(InputNamingConvention.Client);
        var featureFilter = arg.GetArgument<FeatureFilter>(InputNamingConvention.Filtering);
        var identity = _httpContextAccessor.HttpContext.User.Identity;
        // await _userAccessService.AssertAccessAsync(
        //     identity!,
        //     client.Id,
        //     new []{ OrganizationMainDataAccess.ViewSingle });
        var featureRestrictions = Enumerable.Empty<FeatureRestrictionDto>();
        
        // if (identity!.GetRole() == BizRolesDto.Admin)
        //     return featureRestrictions.ToDto();
        
        featureRestrictions = client.ClientType
            switch
            {
                TypeOfClient.Organization
                    =>
                    await _featureRestrictionManagerService.GetMergedOrganizationsRestrictionsAsync(null,
                        new[] { client.Id }, featureFilter.Features.ToArray()),
                TypeOfClient.Network =>
                    await _featureRestrictionManagerService.GetMergedOrganizationsRestrictionsAsync(client.Id,
                        client.VisitedOrganizations.ToArray(),
                        featureFilter.Features.ToArray()),
                _ => throw new ArgumentOutOfRangeException(nameof(client.ClientType), client.ClientType, null)
            };
        var dto = featureRestrictions.ToDto();
        return dto;
    }
}