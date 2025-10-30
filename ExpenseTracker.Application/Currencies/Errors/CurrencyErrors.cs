using Shared.Results.Errors;

namespace ExpenseTracker.Application.Currencies.Errors;

public static class CurrencyErrors
{
    public static Error AlreadyExistsWithCode(string code) => Error.Conflict(
        "Currency.AlreadyExistsWithCode", $"The currency with code \"{code}\" already exists.");
    
    public static Error NotFoundById(Guid id) => Error.NotFound(
        "Currency.NotFoundById", $"The currency with id {id} was not found.");

    public static Error IsUsed = Error.Conflict(
        "Currency.IsUsed", "Currency is used by other entities.");
}