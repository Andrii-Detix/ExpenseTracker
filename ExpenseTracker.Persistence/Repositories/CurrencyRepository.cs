using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Currencies.ValueObjects.Codes;
using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Repositories;

public class CurrencyRepository(AppDbContext dbContext) : ICurrencyRepository
{
    public async Task<Currency?> GetById(Guid id, CancellationToken ct)
    {
        return await dbContext.Currencies.FindAsync([id], ct);
    }

    public async Task<IEnumerable<Currency>> GetAll(CancellationToken ct)
    {
        return await dbContext.Currencies.ToArrayAsync(ct);
    }

    public async Task Add(Currency currency, CancellationToken ct)
    {
        await dbContext.Currencies.AddAsync(currency, ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        Currency? currency = await dbContext.Currencies.FindAsync([id], ct);

        if (currency is not null)
        {
            dbContext.Currencies.Remove(currency);
        }
    }

    public async Task<bool> IsUniqueCode(CurrencyCode code, CancellationToken ct)
    {
        return await dbContext.Currencies.AnyAsync(c => c.Code == code, ct);
    }
}