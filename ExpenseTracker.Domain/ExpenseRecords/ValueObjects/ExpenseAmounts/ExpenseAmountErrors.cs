using Shared.Results.Errors;

namespace ExpenseTracker.Domain.ExpenseRecords.ValueObjects.ExpenseAmounts;

public static class ExpenseAmountErrors
{
    public static readonly Error IsNegative = Error.Validation(
        "ExpenseAmount.IsNegative", "Expense amount cannot be negative.");
}