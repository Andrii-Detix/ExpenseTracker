using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Currencies.ValueObjects.Codes;

namespace ExpenseTracker.Application.Currencies.Abstractions;

public interface ICurrencyRepository
{
    public Task<Currency?> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<Currency>> GetAll(CancellationToken ct);
    public Task Add(Currency currency, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
    public Task<bool> IsUniqueCode(CurrencyCode code, CancellationToken ct);
}