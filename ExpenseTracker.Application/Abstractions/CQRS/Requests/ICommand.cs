namespace ExpenseTracker.Application.Abstractions.CQRS.Requests;

public interface IBaseCommand;

public interface ICommand : IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;