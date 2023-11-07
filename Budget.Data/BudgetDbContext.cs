using Budget.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budget.Data;

public class BudgetDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public BudgetDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.Property(c => c.Name)
            .HasMaxLength(50);
            e.Property(c => c.Color)
            .HasMaxLength(9);
        });
    }
}