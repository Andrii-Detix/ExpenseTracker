using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Repositories;

public class ExpenseRecordRepository(AppDbContext dbContext) : IExpenseRecordRepository
{
    public async Task<ExpenseRecord?> GetById(Guid id, CancellationToken ct)
    {
        return await dbContext.ExpenseRecords.FindAsync([id], ct);
    }

    public async Task<IEnumerable<ExpenseRecord>> GetAllByFilter(ExpenseRecordFilterParams filterParams, CancellationToken ct)
    {
        return await dbContext.ExpenseRecords.ToArrayAsync(ct);
    }

    public async Task Add(ExpenseRecord expenseRecord, CancellationToken ct)
    {
        await dbContext.ExpenseRecords.AddAsync(expenseRecord, ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        ExpenseRecord? expenseRecord = await dbContext.ExpenseRecords.FindAsync([id], ct);

        if (expenseRecord is not null)
        {
            dbContext.ExpenseRecords.Remove(expenseRecord);
        }
    }
}