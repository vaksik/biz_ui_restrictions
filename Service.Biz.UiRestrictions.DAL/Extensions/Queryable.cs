using System.Linq.Expressions;

namespace Service.Biz.UiRestrictions.DAL.Extensions;

public static class Queryable
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}