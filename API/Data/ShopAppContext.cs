using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ShopAppContext : DbContext
{
    public ShopAppContext(DbContextOptions<ShopAppContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=ShopApp.db");
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Collection)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CollectionId);
        
        modelBuilder.Entity<Photo>()
            .HasOne(p => p.Product)
            .WithMany(c => c.Photos)
            .HasForeignKey(p => p.ProductId);
    }
    

    
    
    
}
