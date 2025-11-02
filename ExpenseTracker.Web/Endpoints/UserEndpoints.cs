using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Commands.DeleteUserByIdCommands;
using ExpenseTracker.Application.Users.Commands.SetDefaultCurrency;
using ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;
using ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;
using ExpenseTracker.Web.Contracts.Users;
using ExpenseTracker.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/users");
        
        group.SetDefaultCurrency();
        group.DeleteUserById();
        group.GetUserById();
        group.GetAllUsers();
        
        return endpoints;
    }
    
    private static void SetDefaultCurrency(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(
            "/{id:guid}/currency",
            async (
                Guid id,
                [FromBody] SetDefaultCurrencyDto dto,
                ICommandHandler<SetDefaultCurrencyCommand, Result> handler,
                CancellationToken ct) =>
            {
                SetDefaultCurrencyCommand command = new SetDefaultCurrencyCommand(id, dto.CurrencyId);
                Result result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok();
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
                IQueryHandler<GetUserByIdQuery, Result<GetUserByIdResponse>> handler,
                CancellationToken ct) =>
            {
                Result<GetUserByIdResponse> result = await handler.Handle(new GetUserByIdQuery(id), ct);

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
                IQueryHandler<GetAllUsersQuery, IEnumerable<UserResponse>> handler,
                CancellationToken ct) =>
            {
                IEnumerable<UserResponse> result = await handler.Handle(new GetAllUsersQuery(), ct);
                
                return Results.Ok(result);
            });
    }
}