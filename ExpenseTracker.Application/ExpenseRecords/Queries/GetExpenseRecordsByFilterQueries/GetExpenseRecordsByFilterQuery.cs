using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Application.ExpenseRecords.Dtos;
using ExpenseTracker.Domain.ExpenseRecords;
using Shared.Results;

namespace ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordsByFilterQueries;

public record GetExpenseRecordsByFilterQuery(ExpenseRecordFilterParams FilterParams) 
    : IQuery<Result<IEnumerable<ExpenseRecord>>>;