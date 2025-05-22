using Microsoft.EntityFrameworkCore;

namespace Products.API.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamPageHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));


            double total = await queryable.CountAsync();
            // asignar el total de registros
            httpContext.Response.Headers.Append("cantidad-total-registros", total.ToString());


        }
    }
}
