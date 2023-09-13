using Module.Gql.Federation;

namespace Service.Biz.UiRestrictions.GraphQL;

public class UiRestrictionsSchema : FederatedSchema<UiRestrictionsFederationEntityType, UiRestrictionsQuery>
{
    public UiRestrictionsSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        
    }
}