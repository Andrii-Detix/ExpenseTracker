namespace ExpenseTracker.Application.Abstractions.Persistence;

public interface IUnitOfWork 
{
    Task SaveChangesAsync(CancellationToken ct = default);
}