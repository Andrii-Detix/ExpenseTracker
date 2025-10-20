using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
        services.AddSingleton<IExpenseRecordRepository, InMemoryExpenseRecordRepository>();
        
        return services;
    }
}