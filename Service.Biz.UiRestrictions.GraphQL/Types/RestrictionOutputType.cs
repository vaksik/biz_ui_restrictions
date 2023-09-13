using GraphQL.Types;
using Module.Gql.Federation;
using Service.Biz.UiRestrictions.GraphQL.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Types;

public class RestrictionOutputType : FederatedObjectGraphType<UiRestrictionDto>
{
    public RestrictionOutputType()
    {
        Field(x => x.AccessType,
            type: typeof(NonNullGraphType<AccessTypeEnumType>));
        Field(x => x.AccessRestrictions,
            type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<AccessRestrictionType>>>));
        Field(x => x.Feature,
            type: typeof(NonNullGraphType<PlaziusFeatureEnumType>));
    }
}