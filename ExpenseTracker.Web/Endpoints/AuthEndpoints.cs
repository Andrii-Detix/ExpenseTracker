using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Commands.CreateUserCommands;
using ExpenseTracker.Application.Users.Commands.Login;
using ExpenseTracker.Web.Extensions;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.RegisterUser();
        endpoints.LoginUser();
        
        return endpoints;
    }
    
    private static void RegisterUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/register",
            async (
                CreateUserCommand command,
                ICommandHandler<CreateUserCommand, Result<Guid>> handler,
                CancellationToken ct) =>
            {
                Result<Guid> result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }

                return Results.Created();
            });
    }

    private static void LoginUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/login",
            async (
                LoginCommand command,
                ICommandHandler<LoginCommand, Result<string>> handler,
                CancellationToken ct) =>
            {
                Result<string> result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(new {Token = result.Value});
            });
    }
}