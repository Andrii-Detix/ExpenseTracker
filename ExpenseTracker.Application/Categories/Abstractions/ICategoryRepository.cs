using ExpenseTracker.Domain.Categories;

namespace ExpenseTracker.Application.Categories.Abstractions;

public interface ICategoryRepository
{
    public Task<Category?> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<Category>> GetAll(CancellationToken ct);
    public Task Add(Category category, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
}