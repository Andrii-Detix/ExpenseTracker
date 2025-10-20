namespace ExpenseTracker.Application.Abstractions.CQRS.Requests;

public interface IBaseQuery;

public interface IQuery : IBaseQuery;

public interface IQuery<TResponse> : IBaseQuery;