using Module.Gql.Federation;
using Service.Biz.UiRestrictions.GraphQL.Types;

namespace Service.Biz.UiRestrictions.GraphQL;

public class UiRestrictionsFederationEntityType : FederationEntityType
{
    public UiRestrictionsFederationEntityType()
    {
        Type<UiRestrictionsOutputType>();
    }
}