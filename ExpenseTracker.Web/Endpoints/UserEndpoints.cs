using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Commands.CreateUserCommands;
using ExpenseTracker.Application.Users.Commands.DeleteUserByIdCommands;
using ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;
using ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Web.Extensions;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/users");
        
        group.AddUser();
        group.DeleteUserById();
        group.GetUserById();
        group.GetAllUsers();
        
        return endpoints;
    }

    private static void AddUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
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

                Guid id = result.Value;
                return Results.Created($"/users/{id}", new {Id = id});
            });
    }

    private static void DeleteUserById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{id:guid}",
            async (
                Guid id, 
                ICommandHandler<DeleteUserByIdCommand, Result> handler,
                CancellationToken ct) =>
            {
                Result result = await handler.Handle(new DeleteUserByIdCommand(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }

                return Results.Ok();
            });
    }

    private static void GetUserById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IQueryHandler<GetUserByIdQuery, Result<User>> handler,
                CancellationToken ct) =>
            {
                Result<User> result = await handler.Handle(new GetUserByIdQuery(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }

    private static void GetAllUsers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            async (
                IQueryHandler<GetAllUsersQuery, IEnumerable<User>> handler,
                CancellationToken ct) =>
            {
                IEnumerable<User> result = await handler.Handle(new GetAllUsersQuery(), ct);
                
                return Results.Ok(result);
            });
    }
}