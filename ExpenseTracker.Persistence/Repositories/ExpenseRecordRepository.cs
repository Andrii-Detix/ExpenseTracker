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
        IQueryable<ExpenseRecord> query = dbContext.ExpenseRecords;

        if (filterParams.UserId is not null)
        {
            query = query.Where(x => x.UserId == filterParams.UserId);
        }

        if (filterParams.CategoryId is not null)
        {
            query = query.Where(x => x.CategoryId == filterParams.CategoryId);
        }
        
        return await query.ToArrayAsync(ct);
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

    public async Task<bool> IsAnyByCurrency(Guid currencyId, CancellationToken ct)
    {
        return await dbContext.ExpenseRecords.AnyAsync(er => er.CurrencyId == currencyId, ct);
    }
}