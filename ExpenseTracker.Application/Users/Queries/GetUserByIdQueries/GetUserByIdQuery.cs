using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;

public record GetUserByIdQuery(Guid Id) : IQuery<Result<GetUserByIdResponse>>;