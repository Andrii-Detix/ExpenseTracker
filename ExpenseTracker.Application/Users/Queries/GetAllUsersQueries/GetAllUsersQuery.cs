using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;

public record GetAllUsersQuery() : IQuery<IEnumerable<User>>;