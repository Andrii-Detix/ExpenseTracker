using Shared.Results.Errors;

namespace ExpenseTracker.Application.ExpenseRecords.Errors;

public static class ExpenseRecordErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "ExpenseRecord.NotFound", $"Expense record with id {id} was not found.");
}