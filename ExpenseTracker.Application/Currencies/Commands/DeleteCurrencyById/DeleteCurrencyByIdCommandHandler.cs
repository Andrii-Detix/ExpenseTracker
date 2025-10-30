using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Commands.DeleteCurrencyById;

public sealed class DeleteCurrencyByIdCommandHandler(
    ICurrencyDeletionPolicy deletionPolicy,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteCurrencyByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteCurrencyByIdCommand command, CancellationToken ct)
    {
        Currency? currency = await currencyRepository.GetById(command.Id, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(command.Id);
        }

        Result canDeleteResult = await deletionPolicy.CanDelete(currency, ct);
        if (canDeleteResult.IsFailure)
        {
            return canDeleteResult;
        }
        
        await currencyRepository.Delete(command.Id, ct);
        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}