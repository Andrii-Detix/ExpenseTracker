using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Queries.GetCurrencyById;

public class GetCurrencyByIdQueryHandler(ICurrencyRepository currencyRepository)
    : IQueryHandler<GetCurrencyByIdQuery, Result<Currency>>
{
    public async Task<Result<Currency>> Handle(GetCurrencyByIdQuery query, CancellationToken ct)
    {
        Currency? currency = await currencyRepository.GetById(query.Id, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(query.Id);
        }

        return currency;
    }
}