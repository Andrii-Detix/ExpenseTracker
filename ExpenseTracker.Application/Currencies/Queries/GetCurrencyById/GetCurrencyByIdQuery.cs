using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Queries.GetCurrencyById;

public record GetCurrencyByIdQuery(Guid Id) : IQuery<Result<Currency>>;