using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Commands.CreateCurrency;

public record CreateCurrencyCommand(string Code, string Name) : ICommand<Result<Guid>>;