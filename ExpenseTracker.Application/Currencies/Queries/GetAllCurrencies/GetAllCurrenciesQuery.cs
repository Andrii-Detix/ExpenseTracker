using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Queries.GetAllCurrencies;

public record GetAllCurrenciesQuery : IQuery<Result<IEnumerable<Currency>>>;