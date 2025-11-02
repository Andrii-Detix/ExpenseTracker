using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.Login;

public record LoginCommand(string Login, string Password) : ICommand<Result<string>>;