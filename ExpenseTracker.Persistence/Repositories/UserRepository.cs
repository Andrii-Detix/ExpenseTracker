using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
using ExpenseTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetById(Guid id, CancellationToken ct)
    {
        return await dbContext.Users.FindAsync([id], ct);
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken ct)
    {
        return await dbContext.Users.ToArrayAsync(ct);
    }

    public async Task Add(User user, CancellationToken ct)
    {
        await dbContext.Users.AddAsync(user, ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        User? user = await dbContext.Users.FindAsync([id], ct);

        if (user is not null)
        {
            dbContext.Users.Remove(user);
        }
    }

    public async Task<bool> IsUniqueByLogin(UserLogin login, CancellationToken ct)
    {
        return !await dbContext.Users.AnyAsync(u => u.Login == login, ct);
    }

    public Task<bool> IsAnyByDefaultCurrency(Guid defaultCurrencyId, CancellationToken ct)
    {
        return dbContext.Users.AnyAsync(u => u.DefaultCurrencyId == defaultCurrencyId, ct);
    }
}