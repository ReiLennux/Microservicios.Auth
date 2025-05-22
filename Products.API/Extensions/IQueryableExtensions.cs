using Products.API.Models.Dtos;

namespace Products.API.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginer<T>(this IQueryable<T> queryable, PagerDto pages)
        {
            return queryable
                .Skip((pages.Page - 1) * pages.RecordsPerPage)
                .Take(pages.RecordsPerPage);
        }
    }
}
