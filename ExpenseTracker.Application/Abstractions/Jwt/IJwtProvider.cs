using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Abstractions.Jwt;

public interface IJwtProvider
{
    string GenerateToken(User user);
}