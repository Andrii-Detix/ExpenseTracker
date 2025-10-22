using System.Collections.Concurrent;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Persistence.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = [];
    
    public Task<User?> GetById(Guid id, CancellationToken ct)
    {
        bool exists = _users.TryGetValue(id, out var user);
        
        return Task.FromResult(exists ? user : null);
    }

    public Task<IEnumerable<User>> GetAll(CancellationToken ct)
    {
        return Task.FromResult<IEnumerable<User>>(_users.Values);
    }

    public Task Add(User user, CancellationToken ct)
    {
        _users.TryAdd(user.Id, user);
        
        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken ct)
    {
        _users.TryRemove(id, out _);
        
        return Task.CompletedTask;
    }
}