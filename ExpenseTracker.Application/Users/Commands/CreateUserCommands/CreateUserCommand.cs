using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.CreateUserCommands;

public record CreateUserCommand(string Name, Guid DefaultCurrencyId) : ICommand<Result<Guid>>;