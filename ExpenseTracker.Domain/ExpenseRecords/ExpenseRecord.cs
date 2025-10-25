using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.ExpenseRecords.ValueObjects.ExpenseAmounts;
using Shared.Results;

namespace ExpenseTracker.Domain.ExpenseRecords;

public class ExpenseRecord : BaseEntity
{
    private ExpenseRecord(
        Guid id,
        Guid userId,
        Guid categoryId,
        DateTime createdAt,
        ExpenseAmount amount)
        : base(id)
    {
        UserId = userId;
        CategoryId = categoryId;
        CreatedAt = createdAt;
        Amount = amount;
    }

    private ExpenseRecord() { }
    
    public Guid UserId { get; }
    public Guid CategoryId { get; }
    public DateTime CreatedAt { get; }
    public ExpenseAmount Amount { get; } = null!;

    public static Result<ExpenseRecord> Create(
        Guid id,
        Guid userId,
        Guid categoryId,
        DateTime createdAt,
        decimal amount)
    {
        Result<ExpenseAmount> amountResult = ExpenseAmount.Create(amount);
        if (amountResult.IsFailure)
        {
            return amountResult.Error;
        }

        return new ExpenseRecord(id, userId, categoryId, createdAt, amountResult.Value!);
    }
}