using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Users.ValueObjects.PasswordHashes;
using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
using ExpenseTracker.Domain.Users.ValueObjects.UserNames;
using Shared.Results;

namespace ExpenseTracker.Domain.Users;

public class User : BaseEntity
{
    private User(Guid id, UserLogin login, PasswordHash passwordHash, UserName name, Guid defaultCurrencyId) 
        : base(id)
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        DefaultCurrencyId = defaultCurrencyId;
    }
    
    private User() { }
    
    public UserLogin Login { get; }
    public PasswordHash PasswordHash { get; }
    public UserName Name { get; }
    public Guid DefaultCurrencyId { get; private set; }

    public static Result<User> Create(Guid id, string login, string passwordHash, string name, Guid defaultCurrencyId)
    {
        Result<UserLogin> loginResult = UserLogin.Create(login);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }
        
        Result<PasswordHash> passwordHashResult = PasswordHash.Create(passwordHash);
        if (passwordHashResult.IsFailure)
        {
            return passwordHashResult.Error;
        }
        
        Result<UserName> nameResult = UserName.Create(name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new User(id, loginResult.Value!, passwordHashResult.Value!, nameResult.Value!, defaultCurrencyId);
    }

    public void SetDefaultCurrency(Guid defaultCurrencyId)
    {
        DefaultCurrencyId = defaultCurrencyId;
    }
}