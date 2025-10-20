using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.ExpenseRecords.Commands.CreateExpenseRecordCommands;
using ExpenseTracker.Application.ExpenseRecords.Commands.DeleteExpenseRecordByIdCommands;
using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordByIdQueries;
using ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordsByFilterQueries;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Web.Extensions;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class ExpenseRecordEndpoints
{
    public static IEndpointRouteBuilder MapExpenseRecordEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/records");

        group.AddRecord();
        group.DeleteRecordById();
        group.GetRecordById();
        group.GetRecordsByFilter();
        
        return endpoints;
    }

    private static void AddRecord(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (
                CreateExpenseRecordCommand command,
                ICommandHandler<CreateExpenseRecordCommand, Result<Guid>> handler,
                CancellationToken ct) =>
            {
                Result<Guid> result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                Guid id = result.Value;
                return Results.Created($"/records/{id}", new {Id = id});
            });
    }

    private static void DeleteRecordById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                ICommandHandler<DeleteExpenseRecordByIdCommand, Result> handler,
                CancellationToken ct) =>
            {
                Result result = await handler.Handle(new DeleteExpenseRecordByIdCommand(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok();
            });
    }

    private static void GetRecordById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IQueryHandler<GetExpenseRecordByIdQuery, Result<ExpenseRecord>> handler,
                CancellationToken ct) =>
            {
                Result<ExpenseRecord> result = await handler.Handle(new GetExpenseRecordByIdQuery(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }

    private static void GetRecordsByFilter(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            async (
                [AsParameters] ExpenseRecordFilterParams filterParams,
                IQueryHandler<GetExpenseRecordsByFilterQuery, Result<IEnumerable<ExpenseRecord>>> handler,
                CancellationToken ct) =>
            {
                var result = await handler.Handle(new GetExpenseRecordsByFilterQuery(filterParams), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }
}