using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Currencies.Commands.CreateCurrency;
using ExpenseTracker.Application.Currencies.Commands.DeleteCurrencyById;
using ExpenseTracker.Application.Currencies.Queries.GetAllCurrencies;
using ExpenseTracker.Application.Currencies.Queries.GetCurrencyById;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Web.Extensions;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class CurrencyEndpoints
{
    public static IEndpointRouteBuilder MapCurrencyEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/currencies");
        
        group.AddCurrency();
        group.DeleteCurrencyById();
        group.GetCurrencyById();
        group.GetAllCurrencies();

        return endpoints;
    }

    private static void AddCurrency(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (
                ICommandHandler<CreateCurrencyCommand, Result<Guid>> handler,
                CreateCurrencyCommand command,
                CancellationToken ct) =>
            {
                Result<Guid> result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                Guid id = result.Value;
                return Results.Created($"/currencies/{id}", new {Id = id});
            });
    }

    private static void DeleteCurrencyById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{id:guid}",
            async (
                ICommandHandler<DeleteCurrencyByIdCommand, Result> handler, 
                Guid id,
                CancellationToken ct) =>
            {
                Result result = await handler.Handle(new DeleteCurrencyByIdCommand(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }

                return Results.Ok();
            });
    }

    private static void GetCurrencyById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{id:guid}",
            async (
                IQueryHandler<GetCurrencyByIdQuery, Result<Currency>> handler,
                Guid id,
                CancellationToken ct) =>
            {
                Result<Currency> result = await handler.Handle(new GetCurrencyByIdQuery(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }

    private static void GetAllCurrencies(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            async (
                IQueryHandler<GetAllCurrenciesQuery, Result<IEnumerable<Currency>>> handler,
                CancellationToken ct) =>
            {
                Result<IEnumerable<Currency>> result = await handler.Handle(new GetAllCurrenciesQuery(), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }
}