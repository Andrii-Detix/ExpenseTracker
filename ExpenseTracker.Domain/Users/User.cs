using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users.ValueObjects.UserNames;
using Shared.Results;

namespace ExpenseTracker.Domain.Users;

public class User : BaseEntity
{
    private User(Guid id, Guid defaultCurrencyId, UserName name) 
        : base(id)
    {
        DefaultCurrencyId = defaultCurrencyId;
        Name = name;
    }
    
    private User() { }
    
    public Guid DefaultCurrencyId { get; }
    public UserName Name { get; }

    public static Result<User> Create(Guid id, Guid defaultCurrencyId, string name)
    {
        Result<UserName> nameResult = UserName.Create(name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new User(id, defaultCurrencyId, nameResult.Value!);
    }
}