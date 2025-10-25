using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        dbContext.Database.Migrate();
    }
}