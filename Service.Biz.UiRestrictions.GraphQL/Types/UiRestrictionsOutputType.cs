using GraphQL.Types;
using Module.Gql.Federation;
using Service.Biz.UiRestrictions.GraphQL.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Types;

public class UiRestrictionsOutputType : FederatedObjectGraphType<UiRestrictionsResponseDto>
{
    public UiRestrictionsOutputType()
    {
        
        Name = nameof(UiRestrictionsOutputType);
        Field(x => x.Restrictions,
            type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<RestrictionOutputType>>>));
       
    }
}