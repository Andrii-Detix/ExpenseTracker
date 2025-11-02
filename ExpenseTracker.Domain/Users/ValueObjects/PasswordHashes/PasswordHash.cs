using Shared.Results;

namespace ExpenseTracker.Domain.Users.ValueObjects.PasswordHashes;

public record PasswordHash
{
    private PasswordHash(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<PasswordHash> Create(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return PasswordHashErrors.IsEmpty;
        }
        
        passwordHash = passwordHash.Trim();
        
        return new PasswordHash(passwordHash);
    }
}