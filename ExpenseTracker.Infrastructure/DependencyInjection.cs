using ExpenseTracker.Application.Abstractions.Jwt;
using ExpenseTracker.Application.Abstractions.Security;
using ExpenseTracker.Infrastructure.Authentication;
using ExpenseTracker.Infrastructure.Security.PasswordHashers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        return services;
    }
}