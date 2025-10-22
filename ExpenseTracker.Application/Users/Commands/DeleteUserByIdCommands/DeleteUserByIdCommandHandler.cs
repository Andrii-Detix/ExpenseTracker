using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Users.Abstractions;
using ExpenseTracker.Application.Users.Errors;
using ExpenseTracker.Domain.Users;
using Shared.Results;

namespace ExpenseTracker.Application.Users.Commands.DeleteUserByIdCommands;

public sealed class DeleteUserByIdCommandHandler(IUserRepository userRepository)
    : ICommandHandler<DeleteUserByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteUserByIdCommand command, CancellationToken ct)
    {
        User? user = await userRepository.GetById(command.Id, ct);
        if (user is null)
        {
            return UserErrors.NotFound(command.Id);
        }
        
        await userRepository.Delete(command.Id, ct);
        
        return Result.Success();
    }
}