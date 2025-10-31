using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.Categories.Errors;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Errors;
using ExpenseTracker.Application.ExpenseRecords.Abstractions;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Commands.CreateExpenseRecordCommands;

public sealed class CreateExpenseRecordCommandHandler(
    IExpenseRecordRepository  expenseRecordRepository,
    IUserRepository  userRepository,
    ICategoryRepository categoryRepository,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateExpenseRecordCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateExpenseRecordCommand command, CancellationToken ct)
    {
        User? user = await userRepository.GetById(command.UserId, ct);
        if (user is null)
        {
            return UserErrors.NotFound(command.UserId);
        }
        
        Category? category = await categoryRepository.GetById(command.CategoryId, ct);
        if (category is null)
        {
            return CategoryErrors.NotFound(command.CategoryId);
        }
        
        Guid currencyId = command.CurrencyId ?? user.DefaultCurrencyId;
        
        Currency? currency = await currencyRepository.GetById(currencyId, ct);
        if (currency is null)
        {
            return CurrencyErrors.NotFoundById(currencyId);
        }

        Result<ExpenseRecord> expenseRecordResult = ExpenseRecord.Create(
            Guid.CreateVersion7(),
            command.UserId,
            command.CategoryId,
            currencyId,
            DateTime.UtcNow,
            command.Amount);

        if (expenseRecordResult.IsFailure)
        {
            return expenseRecordResult.Error;
        }
        
        ExpenseRecord expenseRecord = expenseRecordResult.Value!;
        
        await expenseRecordRepository.Add(expenseRecord, ct);
        await unitOfWork.SaveChangesAsync(ct);
        
        return expenseRecord.Id;
    }
}