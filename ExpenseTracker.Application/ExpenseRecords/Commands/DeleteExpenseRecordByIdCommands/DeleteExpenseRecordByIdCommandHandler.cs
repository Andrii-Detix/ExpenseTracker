using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Errors;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Commands.DeleteExpenseRecordByIdCommands;

public sealed class DeleteExpenseRecordByIdCommandHandler(
    IExpenseRecordRepository expenseRecordRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteExpenseRecordByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteExpenseRecordByIdCommand command, CancellationToken ct)
    {
        ExpenseRecord? expenseRecord = await expenseRecordRepository.GetById(command.Id, ct);
        if (expenseRecord is null)
        {
            return ExpenseRecordErrors.NotFound(command.Id);
        }
        
        await expenseRecordRepository.Delete(command.Id, ct);
        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}