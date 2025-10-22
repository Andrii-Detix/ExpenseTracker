using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Application.ExpenseRecords.Errors;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordsByFilterQueries;

public sealed class GetExpenseRecordsByFilterQueryHandler(IExpenseRecordRepository expenseRecordRepository)
    : IQueryHandler<GetExpenseRecordsByFilterQuery, Result<IEnumerable<ExpenseRecord>>>
{
    public async Task<Result<IEnumerable<ExpenseRecord>>> Handle(GetExpenseRecordsByFilterQuery query, CancellationToken ct)
    {
        ExpenseRecordFilterParams filterParams = query.FilterParams;

        if (filterParams is null
            || filterParams.UserId is null && filterParams.CategoryId is null)
        {
            return ExpenseRecordFilterParamsErrors.IsEmpty;
        }
        
        IEnumerable<ExpenseRecord> expenseRecords = await expenseRecordRepository.GetAllByFilter(filterParams, ct);

        return Result<IEnumerable<ExpenseRecord>>.Success(expenseRecords);
    }
}