using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Commands.CreateCategoryCommands;

public record CreateCategoryCommand(string Name) : ICommand<Result<Guid>>;