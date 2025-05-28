using Microsoft.EntityFrameworkCore;
using Products.API.Models;
using System.Reflection.Emit;

namespace Products.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
