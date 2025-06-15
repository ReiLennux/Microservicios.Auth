using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  }

        public DbSet<CartDetails> CartDetails { get; set; }

        public DbSet<CartHeader> CartHeader { get; set; }
    }
}
