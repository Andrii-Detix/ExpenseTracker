using Shared.Results;

namespace ExpenseTracker.Domain.Users.ValueObjects.Passwords;

public record Password
{
    private const int MinLength = 8;
    private const int MaxLength = 64;
    
    private Password(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return PasswordErrors.IsEmpty;
        }
        
        password = password.Trim();

        if (password.Length < MinLength || password.Length > MaxLength)
        {
            return PasswordErrors.LengthOutOfRange(MinLength, MaxLength);
        }

        if (!password.Any(char.IsLetter))
        {
            return PasswordErrors.MissingLetter;
        }
        
        if (!password.Any(char.IsDigit))
        {
            return PasswordErrors.MissingDigit;
        }

        return new Password(password);
    }
}