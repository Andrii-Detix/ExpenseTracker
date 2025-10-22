using ExpenseTracker.Web.HealthCheck;

namespace ExpenseTracker.Web.Endpoints;

public static class HealthCheckEndpoints
{
    public static IEndpointRouteBuilder MapHealthCheckEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.HealthCheckEndpoint();
        
        return endpoints;
    }

    private static void HealthCheckEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/healthcheck", () =>
        {
            object result = new
            {
                Status = nameof(HealthStatus.Healthy),
                Time = DateTime.UtcNow
            };
            
            return Results.Ok(result);
        });
    }
}