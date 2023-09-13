using GraphQL.Types;
using Service.Biz.UiRestrictions.GraphQL.Dto;
using Service.Biz.UiRestrictions.GraphQL.Filters;

namespace Service.Biz.UiRestrictions.GraphQL.Types;

public class ClientFilterInputType :  InputObjectGraphType<ClientFilter>
{
    public ClientFilterInputType()
    {
        Field<NonNullGraphType<GuidGraphType>>("id");
        Field<NonNullGraphType<ClientTypeEnumType>>("clientType");
        Field<ListGraphType<NonNullGraphType<GuidGraphType>>>("visitedOrganizations");
    }
}