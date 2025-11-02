using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
using ExpenseTracker.Domain.Users.ValueObjects.UserNames;

namespace ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;

public record UserResponse(    
    Guid Id,
    UserLogin Login,
    UserName Name,
    Guid DefaultCurrencyId);