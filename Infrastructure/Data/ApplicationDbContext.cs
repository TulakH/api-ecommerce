namespace Infrastructure.Data;

using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Category> Categories { get; set; } = null!;
	public DbSet<Order> Orders { get; set; } = null!;
	public DbSet<OrderItem> OrderItems { get; set; } = null!;
	public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId);

            entity.Property(p => p.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd()
            .Metadata
            .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(p => p.UpdatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
           entity.HasKey(o => o.Id);
           //TODO : client FK

            entity.Property(o => o.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.HasOne(i => i.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(p => p.OrderId);

            entity.HasOne(i => i.Product)
                  .WithMany(p => p.OrderItems)
                  .HasForeignKey(i => i.OrderId);
        });
    }
}