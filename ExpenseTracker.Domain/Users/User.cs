using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users.ValueObjects.UserNames;
using Shared.Results;

namespace ExpenseTracker.Domain.Users;

public class User : BaseEntity
{
    private User(Guid id, UserName name) 
        : base(id)
    {
        Name = name;
    }
    
    public UserName Name { get; }

    public static Result<User> Create(Guid id, string name)
    {
        Result<UserName> nameResult = UserName.Create(name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new User(id, nameResult.Value!);
    }
}