using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Categories.Commands.CreateCategoryCommands;
using ExpenseTracker.Application.Categories.Commands.DeleteCategoryByIdCommands;
using ExpenseTracker.Application.Categories.Queries.GetAllCategoriesQueries;
using ExpenseTracker.Application.Categories.Queries.GetCategoryByIdQueries;
using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace ExpenseTracker.Web.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/categories");
        
        group.AddCategory();
        group.DeleteCategoryById();
        group.GetCategoryById();
        group.GetAllCategories();

        return endpoints;
    }

    private static void AddCategory(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (
                [FromBody] CreateCategoryCommand command,
                ICommandHandler<CreateCategoryCommand, Result<Guid>> handler,
                CancellationToken ct) =>
            {
                Result<Guid> result = await handler.Handle(command, ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }

                Guid id = result.Value;
                return Results.Created($"/categories/{id}", new {Id = id});
            });
    }

    private static void DeleteCategoryById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                ICommandHandler<DeleteCategoryByIdCommand, Result> handler,
                CancellationToken ct) =>
            {
                Result result = await handler.Handle(new DeleteCategoryByIdCommand(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }

                return Results.Ok();
            });
    }

    private static void GetCategoryById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IQueryHandler<GetCategoryByIdQuery, Result<Category>> handler,
                CancellationToken ct) =>
            {
                Result<Category> result = await handler.Handle(new GetCategoryByIdQuery(id), ct);

                if (result.IsFailure)
                {
                    return result.ToProblemDetails();
                }
                
                return Results.Ok(result.Value);
            });
    }

    private static void GetAllCategories(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/",
            async (
                IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>> handler,
                CancellationToken ct) =>
            {
                IEnumerable<Category> result = await handler.Handle(new GetAllCategoriesQuery(), ct);
                
                return Results.Ok(result);
            });
    }
}