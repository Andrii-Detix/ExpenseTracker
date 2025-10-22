using ExpenseTracker.Application.Abstractions.CQRS.Requests;

namespace ExpenseTracker.Application.Abstractions.CQRS.Handlers;

public interface IQueryHandler<in TQuery>
    where TQuery : IQuery
{
    Task Handle(TQuery query, CancellationToken ct);
}

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken ct);
}