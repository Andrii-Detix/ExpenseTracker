using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Users.ValueObjects.UserNames;

public static class UserNameErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "UserName.Empty", "The user name cannot be empty.");
    
    public static Error LengthOutOfRange(int min, int max) => Error.Validation(
        "UserName.LengthOutOfRange", $"The length of the user name must be between {min} and {max}.");

    public static readonly Error MustStartWithCapital = Error.Validation(
        "UserName.MustStartWithCapital", "The user name must start with capital letter.");
    
    public static readonly Error InvalidFormat = Error.Validation(
        "UserName.InvalidFormat", "The user name must contain only letters and symbols \"-\", \"'\"");
}