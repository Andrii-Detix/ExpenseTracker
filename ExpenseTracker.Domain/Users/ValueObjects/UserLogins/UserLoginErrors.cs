using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Users.ValueObjects.UserLogins;

public static class UserLoginErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "UserLogin.IsEmpty", "The user login cannot be empty.");
    
    public static Error LengthOutOfRange(int min, int max) => Error.Validation(
        "UserLogin.LengthOutOfRange", $"The length of the user login must be between {min} and {max}.");
    
    public static readonly Error InvalidLoginFormat = Error.Validation(
        "UserLogin.InvalidLoginFormat", "The user login can only contain English letters, digits, and underscores."); 
}