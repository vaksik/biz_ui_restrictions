using GraphQL.Types;
using Module.Gql.Federation;
using Service.Biz.UiRestrictions.GraphQL.Dto;

namespace Service.Biz.UiRestrictions.GraphQL.Types;

public class AccessRestrictionType : FederatedObjectGraphType<AccessRestrictionDto>
{
    public AccessRestrictionType()
    {
        Field(x => x.AccessRestrictionType,
            type: typeof(NonNullGraphType<AccessRestrictionTypeEnumType>));
        
        Field(x => x.Details,
            type: typeof(StringGraphType));
    }
}