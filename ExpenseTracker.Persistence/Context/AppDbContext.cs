using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ExpenseRecord> ExpenseRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseRecordConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}