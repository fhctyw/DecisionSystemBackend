using DecisionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionSystem.Data
{
    public class DecisionSystemDbContext : DbContext
    {
        public DecisionSystemDbContext(DbContextOptions<DecisionSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Analyst> Analyst { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        //public DbSet<ProductPlacement> ProductPlacements { get; set; }
        public DbSet<Cart> Cart { get; set; }
        //public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<ProductManager> ProductManager { get; set; }
    }

}
