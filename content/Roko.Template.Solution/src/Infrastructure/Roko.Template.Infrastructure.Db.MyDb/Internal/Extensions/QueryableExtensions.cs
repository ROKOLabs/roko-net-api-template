namespace Roko.Template.Infrastructure.Db.MyDb.Internal.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class QueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }
    }
}
