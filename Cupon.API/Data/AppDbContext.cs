using Coupon.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Coupon> Coupons { get; set; }

    }
}
