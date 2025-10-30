using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.SetDefaultCurrency;

public class SetDefaultCurrencyCommandHandler(
    IUserRepository userRepository,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SetDefaultCurrencyCommand, Result>
{
    public async Task<Result> Handle(SetDefaultCurrencyCommand command, CancellationToken ct)
    {
        User? user = await userRepository.GetById(command.UserId, ct);
        if (user is null)
        {
            return UserErrors.NotFound(command.UserId);
        }
        
        Currency? currency = await currencyRepository.GetById(command.CurrencyId, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(command.CurrencyId);
        }
        
        user.SetDefaultCurrency(command.CurrencyId);

        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}