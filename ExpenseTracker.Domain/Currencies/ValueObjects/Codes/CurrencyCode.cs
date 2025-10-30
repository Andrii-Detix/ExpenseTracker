using Shared.Results;

namespace ExpenseTracker.Domain.Currencies.ValueObjects.Codes;

public record CurrencyCode
{
    private const int CodeLength = 3;
    
    private CurrencyCode(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<CurrencyCode> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return CurrencyCodeErrors.IsEmpty;
        }
        
        code = code.Trim().ToUpper();

        if (code.Length != CodeLength)
        {
            return CurrencyCodeErrors.InvalidLength(CodeLength);
        }

        if (!ContainsOnlyLetters(code))
        {
            return CurrencyCodeErrors.ContainsNonLetters;
        }

        return new CurrencyCode(code);
    }

    private static bool ContainsOnlyLetters(string value)
    {
        return value.All(char.IsLetter);
    }
}