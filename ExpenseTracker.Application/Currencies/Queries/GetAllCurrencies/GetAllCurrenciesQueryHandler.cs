using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Queries.GetAllCurrencies;

public sealed class GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
    : IQueryHandler<GetAllCurrenciesQuery, Result<IEnumerable<Currency>>>
{
    public async Task<Result<IEnumerable<Currency>>> Handle(GetAllCurrenciesQuery query, CancellationToken ct)
    {
        IEnumerable<Currency> currencies = await currencyRepository.GetAll(ct);

        return Result<IEnumerable<Currency>>.Success(currencies);
    }
}