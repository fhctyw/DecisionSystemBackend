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
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductsCategories> ProductsCategories { get; set; }
        public DbSet<ProductRating> ProductRating { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseItem> PurchaseItem { get; set; }
        public DbSet<ProductManager> ProductManager { get; set; }
        public DbSet<ProductAnalys> ProductAnalys { get; set; }
        public DbSet<ArchiveProductsAnalys> ArchiveProductsAnalys { get; set; }
        public DbSet<AnalysReport> AnalysReport { get; set; }
        public DbSet<DecisionSystem.Models.AnalysFactor> AnalysFactor { get; set; } = default!;
    }
}

