using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.Categories.Errors;
using ExpenseTracker.Domain.Categories;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Queries.GetCategoryByIdQueries;

public sealed class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryByIdQuery, Result<Category>>
{
    public async Task<Result<Category>> Handle(GetCategoryByIdQuery query, CancellationToken ct)
    {
        Category? category = await categoryRepository.GetById(query.Id, ct);
        if (category is null)
        {
            return CategoryErrors.NotFound(query.Id);
        }
        
        return category;
    }
}