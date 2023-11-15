using Budget.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budget.Data;

public class BudgetDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

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

        modelBuilder.Entity<Transaction>(e =>
        {
            e.Property(t => t.Name)
            .HasMaxLength(50);
            
            e.Property(t => t.Description)
            .HasMaxLength(200);

            e.Property(t => t.TransactionDate)
             .HasColumnType("INTEGER")
             .HasConversion(
                v => v.Ticks,
                v => new DateTime(v));

            e.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .OnDelete(DeleteBehavior.Restrict);

            e.Property(t => t.Amount)
             .HasColumnType("decimal(10,2)");
        });
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite("Data source = teset.db");
    //}
}