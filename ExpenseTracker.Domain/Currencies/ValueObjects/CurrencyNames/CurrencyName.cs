using Shared.Results;

namespace ExpenseTracker.Domain.Currencies.ValueObjects.CurrencyNames;

public record CurrencyName
{
    private const int MinLength = 2;
    private const int MaxLength = 100;
    private const string AllowedChars = " -'().";
    
    private CurrencyName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<CurrencyName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return CurrencyNameErrors.IsEmpty;
        }

        name = name.Trim();
        
        if (name.Length < MinLength || name.Length > MaxLength)
        {
            return CurrencyNameErrors.LengthOutOfRange(MinLength, MaxLength);
        }
        
        if (!ContainsOnlyAllowedCharsOrLetters(name))
        {
            return CurrencyNameErrors.ContainsInvalidCharacters;
        }

        return new CurrencyName(name);
    }

    private static bool ContainsOnlyAllowedCharsOrLetters(string value)
    {
        return value.All(c => char.IsLetter(c) || AllowedChars.Contains(c));
    }
}