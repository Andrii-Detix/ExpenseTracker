using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Commands.DeleteCategoryByIdCommands;

public record DeleteCategoryByIdCommand(Guid Id) : ICommand<Result>;