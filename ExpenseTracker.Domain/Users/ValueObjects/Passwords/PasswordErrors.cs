using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Users.ValueObjects.Passwords;

public static class PasswordErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "Passwords.IsEmpty", "The password cannot be empty."); 
    
    public static Error LengthOutOfRange(int min, int max) => Error.Validation(
        "Password.LengthOutOfRange", $"The length of the user login must be between {min} and {max}.");
    
    public static readonly Error MissingDigit = Error.Validation(
        "Password.MissingDigit", "The password must contain at least one digit.");

    public static readonly Error MissingLetter = Error.Validation(
        "Password.MissingLetter", "The password must contain at least one letter.");
}