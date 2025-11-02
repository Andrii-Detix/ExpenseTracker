using ExpenseTracker.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ExpenseTracker.Web.Extensions;

public static class AuthExtensions
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();
        
        services.ConfigureOptions<JwtOptions>();
        services.ConfigureOptions<JwtBearerOptions>();
    }
}