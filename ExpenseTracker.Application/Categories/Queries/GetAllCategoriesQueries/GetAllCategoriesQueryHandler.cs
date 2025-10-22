using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Domain.Categories;

namespace ExpenseTracker.Application.Categories.Queries.GetAllCategoriesQueries;

public sealed class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query, CancellationToken ct)
    {
        IEnumerable<Category> categories = await categoryRepository.GetAll(ct);
        
        return categories;
    }
}