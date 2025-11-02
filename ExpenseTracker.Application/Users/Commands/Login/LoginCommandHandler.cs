using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Jwt;
using ExpenseTracker.Application.Abstractions.Security;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.ValueObjects.Passwords;
using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.Login;

public sealed class LoginCommandHandler(
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IUserRepository userRepository)
    : ICommandHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken ct)
    {
        Result<UserLogin> loginResult = UserLogin.Create(command.Login);
        Result<Password> passwordResult = Password.Create(command.Password);
        if (loginResult.IsFailure || passwordResult.IsFailure)
        {
            return UserErrors.InvalidCredentials;
        }

        UserLogin login = loginResult.Value!;
        User? user = await userRepository.GetByLogin(login, ct);
        if (user is null)
        {
            return UserErrors.InvalidCredentials;
        }

        Password password = passwordResult.Value!;
        bool isPasswordValid = passwordHasher.Verify(password.Value, user.PasswordHash.Value);
        if (!isPasswordValid)
        {
            return UserErrors.InvalidCredentials;
        }

        string jwtToken = jwtProvider.GenerateToken(user);
        
        return jwtToken;
    }
}