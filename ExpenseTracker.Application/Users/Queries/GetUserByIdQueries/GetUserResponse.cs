using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
using ExpenseTracker.Domain.Users.ValueObjects.UserNames;

namespace ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;

public record GetUserByIdResponse(
    Guid Id,
    UserLogin Login,
    UserName Name,
    Guid DefaultCurrencyId);