using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Currencies.ValueObjects.Codes;

public static class CurrencyCodeErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "CurrencyCode.IsEmpty", "The currency code cannot be empty.");

    public static Error InvalidLength(int correctLength) => Error.Validation(
        "CurrencyCode.InvalidLength", $"The currency code length must be {correctLength}.");
    
    public static readonly Error ContainsNonLetters = Error.Validation(
        "CurrencyCode.ContainsNonLetters", "The currency code must contain only letters.");
}