using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Users.ValueObjects.PasswordHashes;

public static class PasswordHashErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "PasswordHash.IsEmpty", "The password hash cannot be empty.");
}