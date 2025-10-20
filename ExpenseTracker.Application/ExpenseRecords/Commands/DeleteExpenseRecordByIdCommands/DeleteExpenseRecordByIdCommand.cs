using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Commands.DeleteExpenseRecordByIdCommands;

public record DeleteExpenseRecordByIdCommand(Guid Id) : ICommand<Result>;