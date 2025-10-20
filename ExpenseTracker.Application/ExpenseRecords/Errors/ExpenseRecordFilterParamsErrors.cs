using Shared.Results.Errors;

namespace ExpenseTracker.Application.ExpenseRecords.Errors;

public static class ExpenseRecordFilterParamsErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "ExpenseRecordFilterParams.IsEmpty", "Expense record filter params cannot be empty.");
}