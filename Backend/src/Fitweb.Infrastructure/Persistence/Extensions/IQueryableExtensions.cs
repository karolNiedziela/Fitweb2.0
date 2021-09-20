using Microsoft.EntityFrameworkCore;
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
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string columnName, string comparison, object value)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }

            var parameter = Expression.Parameter(typeof(T), "x");

            var property = columnName.Split('.')
                                .Aggregate<string, Expression>
                                    (parameter, (c, m) => Expression.Property(c, m));

            var body = MakeComparison(property, comparison, value);

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            var quote = Expression.Quote(lambda);

            var methodCallExpression = Expression.Call(typeof(Queryable), "Where",
                            new Type[] { source.ElementType },
                            source.Expression, quote);

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        //https://stackoverflow.com/questions/29084894/how-to-use-an-expressionfunc-to-set-a-nested-property
        public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending)
        {
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

        public static async Task<IEnumerable<T>> ApplyPaging<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
        {
            return await queryable
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        private static Expression MakeComparison(Expression left, string comparison, object value)
        {
            var constant = Expression.Constant(value, left.Type);
            switch (comparison)
            {
                case "==":
                    return Expression.MakeBinary(ExpressionType.Equal, left, constant);
                case "!=":
                    return Expression.MakeBinary(ExpressionType.NotEqual, left, constant);
                case ">":
                    return Expression.MakeBinary(ExpressionType.GreaterThan, left, constant);
                case ">=":
                    return Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, left, constant);
                case "<":
                    return Expression.MakeBinary(ExpressionType.LessThan, left, constant);
                case "<=":
                    return Expression.MakeBinary(ExpressionType.LessThanOrEqual, left, constant);
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    if (value is string)
                        return Expression.Call(left, comparison, Type.EmptyTypes, Expression.Constant(value, typeof(string)));

                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
                default:
                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
            }
        }
    }

}
