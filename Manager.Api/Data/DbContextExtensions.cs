using Microsoft.EntityFrameworkCore;

namespace Manager.Api.Data
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable, DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var navigationProperties = entityType.GetNavigations().Select(e => e.Name);

            foreach (var navigationProperty in navigationProperties)
            {
                queryable = queryable.Include(navigationProperty);
            }

            return queryable;
        }
    }

}
