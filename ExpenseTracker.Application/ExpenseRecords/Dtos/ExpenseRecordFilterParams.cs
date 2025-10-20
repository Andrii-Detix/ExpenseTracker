namespace ExpenseTracker.Application.ExpenseRecords.Dtos;

public record ExpenseRecordFilterParams(Guid? UserId = null, Guid? CategoryId = null);