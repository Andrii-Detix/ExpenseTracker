using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;

namespace ExpenseTracker.Application.Users.Abstractions;

public interface IUserRepository
{
    public Task<User?> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<User>> GetAll(CancellationToken ct);
    public Task Add(User user, CancellationToken ct);
    public Task Delete(Guid id, CancellationToken ct);
    public Task<bool> IsUniqueByLogin(UserLogin login, CancellationToken ct);
    public Task<bool> IsAnyByDefaultCurrency(Guid defaultCurrencyId, CancellationToken ct);
}