using System.Text.RegularExpressions;
using Shared.Results;

namespace ExpenseTracker.Domain.Users.ValueObjects.UserLogins;

public record UserLogin
{
    private const int MinLength = 5;
    private const int MaxLength = 50;
    private static readonly Regex LoginPattern = new(@"^[a-zA-Z0-9_]+$"); 
    
    private UserLogin(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<UserLogin> Create(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            return UserLoginErrors.IsEmpty;
        }
        
        login = login.Trim();

        if (login.Length < MinLength || login.Length > MaxLength)
        {
            return UserLoginErrors.LengthOutOfRange(MinLength, MaxLength);
        }

        if (!LoginPattern.IsMatch(login))
        {
            return UserLoginErrors.InvalidLoginFormat;
        }
        
        return new UserLogin(login);
    }
}