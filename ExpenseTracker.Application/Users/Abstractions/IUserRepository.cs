using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.Abstractions;

public interface IUserRepository
{
    public Task<User?> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<User>> GetAll(CancellationToken ct);
    public Task Add(User user, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
}