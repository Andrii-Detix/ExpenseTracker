using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.Persistence.UnitOfWorks;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}