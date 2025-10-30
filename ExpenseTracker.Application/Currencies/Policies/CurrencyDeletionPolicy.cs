using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Currencies;
using Shared.Results;

namespace ExpenseTracker.Application.Currencies.Policies;

public class CurrencyDeletionPolicy(
    IUserRepository userRepository,
    IExpenseRecordRepository expenseRecordRepository)
    : ICurrencyDeletionPolicy
{
    public async Task<Result> CanDelete(Currency currency, CancellationToken ct)
    {
        bool isUsedByUsers = await userRepository.IsAnyByDefaultCurrency(currency.Id, ct);
        bool isUsedByRecords = await expenseRecordRepository.IsAnyByCurrency(currency.Id, ct);

        if (isUsedByUsers || isUsedByRecords)
        {
            return CurrencyErrors.IsUsed;
        }
        
        return Result.Success();
    }
}