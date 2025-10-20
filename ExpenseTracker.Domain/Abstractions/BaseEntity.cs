namespace ExpenseTracker.Domain.Abstractions;

public abstract class BaseEntity(Guid id)
{
    public Guid Id { get; } = id;
}