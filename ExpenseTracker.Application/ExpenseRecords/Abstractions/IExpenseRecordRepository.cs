using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Domain.ExpenseRecords;

namespace ExpenseTracker.Application.ExpenseRecords.Abstractions;

public interface IExpenseRecordRepository
{
    public Task<ExpenseRecord?> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<ExpenseRecord>> GetAllByFilter(ExpenseRecordFilterParams filterParams, CancellationToken ct);
    public Task Add(ExpenseRecord expenseRecord, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
}