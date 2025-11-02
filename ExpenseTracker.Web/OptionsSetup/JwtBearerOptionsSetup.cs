using System.Text;
using ExpenseTracker.Infrastructure.Authentication;
using ExpenseTracker.Web.Auth.Errors;
using ExpenseTracker.Web.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Results;
using Shared.Results.Errors;

namespace ExpenseTracker.Web.OptionsSetup;

public class JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions) 
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };

        options.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = async context =>
            {
                context.NoResult();
                Error error = context.Exception switch
                {
                    SecurityTokenExpiredException => AuthErrors.ExpiredToken,
                    _ => AuthErrors.InvalidToken
                };
                
                await SetupResponse(error, context.HttpContext);
            },
            OnChallenge = async context =>
            {
                context.HandleResponse();
                await SetupResponse(AuthErrors.Unauthorized, context.HttpContext);
            }
        };
    }
 
    public void Configure(string? name, JwtBearerOptions options) => Configure(options);

    private static async Task SetupResponse(Error error, HttpContext context)
    {
        if (context.Response.HasStarted)
            return;
        
        Result errorResult = Result.Failure(error);
        await errorResult.ToProblemDetails()
            .ExecuteAsync(context);
    }
}