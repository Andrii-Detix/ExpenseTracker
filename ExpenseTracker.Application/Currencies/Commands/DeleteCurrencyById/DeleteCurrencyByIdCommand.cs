using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Commands.DeleteCurrencyById;

public record DeleteCurrencyByIdCommand(Guid Id) : ICommand<Result>;