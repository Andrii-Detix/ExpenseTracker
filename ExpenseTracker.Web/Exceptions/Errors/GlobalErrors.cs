using Shared.Results.Errors;

namespace ExpenseTracker.Web.Exceptions.Errors;

public static class GlobalErrors
{
    public static readonly Error Occured = Error.Failure(
        "Server.Error", "An error occured.");
}