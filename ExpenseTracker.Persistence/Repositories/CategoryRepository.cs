using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<Category?> GetById(Guid id, CancellationToken ct)
    {
        return await dbContext.Categories.FindAsync([id], ct);
    }

    public async Task<IEnumerable<Category>> GetAll(CancellationToken ct)
    {
        return await dbContext.Categories.ToArrayAsync(ct);
    }

    public async Task Add(Category category, CancellationToken ct)
    {
        await dbContext.Categories.AddAsync(category, ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        Category? category = await dbContext.Categories.FindAsync([id], ct);

        if (category is not null)
        {
            dbContext.Categories.Remove(category);
        }
    }
}