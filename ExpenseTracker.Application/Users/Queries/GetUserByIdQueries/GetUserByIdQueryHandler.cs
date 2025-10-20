using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        User? user = await userRepository.GetById(query.Id, ct);
        if (user is null)
        {
            return UserErrors.NotFound(query.Id);
        }

        return user;
    }
}