using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.SetDefaultCurrency;

public record SetDefaultCurrencyCommand(Guid UserId, Guid CurrencyId) : ICommand<Result>;