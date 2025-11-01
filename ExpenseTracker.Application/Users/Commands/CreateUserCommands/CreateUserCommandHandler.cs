using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Abstractions.Security;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.ValueObjects.Passwords;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.CreateUserCommands;

public sealed class CreateUserCommandHandler(
    IPasswordHasher passwordHasher,
    IUserRepository userRepository,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken ct)
    {
        Currency? currency = await currencyRepository.GetById(command.DefaultCurrencyId, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(command.DefaultCurrencyId);
        }
        
        Result<Password> passwordResult = Password.Create(command.Password);
        if (passwordResult.IsFailure)
        {
            return passwordResult.Error;
        }

        Password password = passwordResult.Value!;
        string passwordHash = passwordHasher.Generate(password.Value);
        
        Result<User> userResult = User.Create(
            Guid.CreateVersion7(),
            command.Login,
            passwordHash,
            command.Name,
            command.DefaultCurrencyId);
        
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        User user = userResult.Value!;

        if (!await userRepository.IsUniqueByLogin(user.Login, ct))
        {
            return UserErrors.AlreadyExistsByLogin(user.Login.Value);
        }
        
        await userRepository.Add(user, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return user.Id;
    }
}