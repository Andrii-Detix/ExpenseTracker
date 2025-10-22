using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Commands.CreateExpenseRecordCommands;

public record CreateExpenseRecordCommand(
    Guid UserId,
    Guid CategoryId,
    decimal Amount)
    : ICommand<Result<Guid>>;