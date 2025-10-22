using Shared.Results;

namespace ExpenseTracker.Domain.Users.ValueObjects.UserNames;

public record UserName
{
    private const int MinLength = 2;
    private const int MaxLength = 100;
    private static readonly char[] AvailableCharacters = ['-', '\''];
    
    private UserName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<UserName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return UserNameErrors.IsEmpty;
        }
        
        value = value.Trim();

        if (value.Length is < MinLength or > MaxLength)
        {
            return UserNameErrors.LengthOutOfRange(MinLength, MaxLength);
        }
        
        if (!ConsistsOfAvailableCharacters(value))
        {
            return UserNameErrors.InvalidFormat;
        }

        if (!char.IsUpper(value[0]))
        {
            return UserNameErrors.MustStartWithCapital;
        }

        return new UserName(value);
    }
    
    private static bool ConsistsOfAvailableCharacters(string value)
    {
        return value.All(c => char.IsLetter(c) || AvailableCharacters.Contains(c));
    }
}