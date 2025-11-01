using Shared.Results.Errors;

namespace ExpenseTracker.Application.Users.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "User.NotFound", $"User with id {id} was not found.");

    public static Error AlreadyExistsByLogin(string login) => Error.Conflict(
        "User.AlreadyExistsByLogin", $"The user with login '{login}' already exists.");
}