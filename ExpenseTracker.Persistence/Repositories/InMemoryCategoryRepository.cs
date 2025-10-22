using System.Collections.Concurrent;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Domain.Categories;

namespace ExpenseTracker.Persistence.Repositories;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly ConcurrentDictionary<Guid, Category> _categories = [];
    
    public Task<Category?> GetById(Guid id, CancellationToken ct)
    {
        bool exists = _categories.TryGetValue(id, out var category);
        
        return Task.FromResult(exists ? category : null);
    }

    public Task<IEnumerable<Category>> GetAll(CancellationToken ct)
    {
        return Task.FromResult<IEnumerable<Category>>(_categories.Values);
    }

    public Task Add(Category category, CancellationToken ct)
    {
        _categories.TryAdd(category.Id, category);
        
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken ct)
    {
        _categories.TryRemove(id, out _);
        
        return Task.CompletedTask;
    }
}