using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Currencies.ValueObjects.CurrencyNames;

public static class CurrencyNameErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "CurrencyName.IsEmpty", "The currency name cannot be empty.");
    
    public static Error LengthOutOfRange(int min, int max) => Error.Validation(
        "CurrencyName.LengthOutOfRange", $"The length of the currency name must be between {min} and {max}.");
    
    public static readonly Error ContainsInvalidCharacters = Error.Validation(
        "CurrencyName.ContainsInvalidCharacters", "The currency name must contain only letters, spaces, hyphens, apostrophes, parentheses and dots.");
}