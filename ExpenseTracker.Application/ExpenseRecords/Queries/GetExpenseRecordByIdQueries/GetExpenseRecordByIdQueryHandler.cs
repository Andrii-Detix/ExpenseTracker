using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Errors;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordByIdQueries;

public sealed class GetExpenseRecordByIdQueryHandler(IExpenseRecordRepository  expenseRecordRepository)
    : IQueryHandler<GetExpenseRecordByIdQuery, Result<ExpenseRecord>>
{
    public async Task<Result<ExpenseRecord>> Handle(GetExpenseRecordByIdQuery query, CancellationToken ct)
    {
        ExpenseRecord? expenseRecord = await expenseRecordRepository.GetById(query.Id, ct);
        if (expenseRecord is null)
        {
            return ExpenseRecordErrors.NotFound(query.Id);
        }

        return expenseRecord;
    }
}