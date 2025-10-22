using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.DeleteUserByIdCommands;

public record DeleteUserByIdCommand(Guid Id) : ICommand<Result>;