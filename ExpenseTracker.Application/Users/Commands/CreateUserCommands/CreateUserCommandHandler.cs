using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.CreateUserCommands;

public sealed class CreateUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken ct)
    {
        Result<User> userResult = User.Create(Guid.CreateVersion7(), command.Name);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        User user = userResult.Value!;
        
        await userRepository.Add(user, ct);

        return user.Id;
    }
}