using ExpenseTracker.Application.Abstractions.CQRS.Requests;

namespace ExpenseTracker.Application.Abstractions.CQRS.Handlers;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken ct);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken ct);
}