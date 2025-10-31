using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Repositories;
using ExpenseTracker.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseRecordRepository, ExpenseRecordRepository>();
        
        return services;
    }
}