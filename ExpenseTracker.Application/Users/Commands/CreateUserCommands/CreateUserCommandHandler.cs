using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.CreateUserCommands;

public sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken ct)
    {
        Result<User> userResult = User.Create(Guid.CreateVersion7(), command.DefaultCurrencyId, command.Name);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        User user = userResult.Value!;
        
        Currency? currency = await currencyRepository.GetById(command.DefaultCurrencyId, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(command.DefaultCurrencyId);
        }
        
        await userRepository.Add(user, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return user.Id;
    }
}