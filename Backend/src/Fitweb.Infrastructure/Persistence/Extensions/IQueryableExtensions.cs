using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Extensions
{
    public static class IQueryableExtensions
    {
        //https://stackoverflow.com/questions/29084894/how-to-use-an-expressionfunc-to-set-a-nested-property
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }

            var parameter = Expression.Parameter(typeof(T), "x");

            var property = columnName.Split('.')
                                    .Aggregate<string, Expression>
                                    (parameter, (c, m) => Expression.Property(c, m));

            var lambda = Expression.Lambda(property, parameter);

            var methodName = isAscending ? "OrderBy" : "OrderByDescending";

            var methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                            new Type[] { source.ElementType, property.Type },
                            source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
