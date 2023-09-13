using GraphQL.Types;
using Service.Biz.UiRestrictions.GraphQL.Filters;

namespace Service.Biz.UiRestrictions.GraphQL.Types;

public class FeatureFilterInputType :  InputObjectGraphType<FeatureFilter>
{
    public FeatureFilterInputType()
    {
        Field<ListGraphType<NonNullGraphType<PlaziusFeatureEnumType>>>("features");
    }
}