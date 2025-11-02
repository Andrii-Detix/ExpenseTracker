using ExpenseTracker.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Web.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}