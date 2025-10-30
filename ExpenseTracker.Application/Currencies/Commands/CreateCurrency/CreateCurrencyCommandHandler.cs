using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Commands.CreateCurrency;

public sealed class CreateCurrencyCommandHandler(
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCurrencyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCurrencyCommand command, CancellationToken ct)
    {
        Result<Currency> currencyResult = Currency.Create(Guid.CreateVersion7(), command.Code, command.Name);
        if (currencyResult.IsFailure)
        {
            return currencyResult.Error;
        }

        Currency currency = currencyResult.Value!;

        if (!await currencyRepository.IsUniqueCode(currency.Code, ct))
        {
            return CurrencyErrors.AlreadyExistsWithCode(currency.Code.Value);
        }
        
        await currencyRepository.Add(currency, ct);
        await unitOfWork.SaveChangesAsync(ct);
        
        return currency.Id;
    }
}