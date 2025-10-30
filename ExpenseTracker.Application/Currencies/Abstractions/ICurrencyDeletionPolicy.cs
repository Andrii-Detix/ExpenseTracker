using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Abstractions;

public interface ICurrencyDeletionPolicy
{
    Task<Result> CanDelete(Currency currency, CancellationToken ct);
}