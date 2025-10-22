using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;

public sealed class GetAllUsersQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        IEnumerable<User> users = await userRepository.GetAll(ct);
        
        return users;
    }
}