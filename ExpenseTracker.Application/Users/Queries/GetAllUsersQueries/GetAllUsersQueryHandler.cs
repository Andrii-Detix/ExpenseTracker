using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Users;

namespace ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;

public sealed class GetAllUsersQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        IEnumerable<UserResponse> users = (await userRepository
            .GetAll(ct))
            .Select(u => new UserResponse(
                    u.Id, 
                    u.Login, 
                    u.Name, 
                    u.DefaultCurrencyId));
        
        return users;
    }
}