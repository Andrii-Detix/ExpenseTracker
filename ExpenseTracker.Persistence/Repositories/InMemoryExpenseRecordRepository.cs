using System.Collections.Concurrent;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Domain.ExpenseRecords;

namespace ExpenseTracker.Persistence.Repositories;

public class InMemoryExpenseRecordRepository : IExpenseRecordRepository
{
    private readonly ConcurrentDictionary<Guid, ExpenseRecord> _expenseRecords = [];
    
    public Task<ExpenseRecord?> GetById(Guid id, CancellationToken ct)
    {
        bool exists = _expenseRecords.TryGetValue(id, out var expenseRecord);
        
        return Task.FromResult(exists ? expenseRecord : null);
    }

    public Task<IEnumerable<ExpenseRecord>> GetAllByFilter(ExpenseRecordFilterParams filterParams, CancellationToken ct)
    {
        IEnumerable<ExpenseRecord> query = _expenseRecords.Values.AsEnumerable();

        if (filterParams.UserId is not null)
        {
            query = query.Where(er => er.UserId == filterParams.UserId);
        }

        if (filterParams.CategoryId is not null)
        {
            query = query.Where(er => er.CategoryId == filterParams.CategoryId);
        }
        
        return Task.FromResult(query);
    }

    public Task Add(ExpenseRecord expenseRecord, CancellationToken ct)
    {
        _expenseRecords.TryAdd(expenseRecord.Id, expenseRecord);
        
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken ct)
    {
        _expenseRecords.TryRemove(id, out _);
        
        return Task.CompletedTask;
    }
}