using Shared.Results.Errors;

namespace ExpenseTracker.Web.Auth.Errors;

public static class AuthErrors
{
    public static readonly Error ExpiredToken = Error.Unauthorized(
        "Auth.ExpiredToken", "Provided token has expired.");
    
    public static readonly Error InvalidToken = Error.Unauthorized(
        "Auth.InvalidToken", "Provided token is invalid.");
    
    public static readonly Error Unauthorized = Error.Unauthorized(
        "Auth.Unauthorized", "You are not authorized to access this resource.");
}