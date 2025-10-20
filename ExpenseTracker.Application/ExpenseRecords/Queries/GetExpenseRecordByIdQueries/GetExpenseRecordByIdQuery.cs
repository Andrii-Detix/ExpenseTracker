using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordByIdQueries;

public record GetExpenseRecordByIdQuery(Guid Id) : IQuery<Result<ExpenseRecord>>;