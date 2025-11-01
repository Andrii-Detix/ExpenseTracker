using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Categories.Commands.CreateCategoryCommands;
using ExpenseTracker.Application.Categories.Commands.DeleteCategoryByIdCommands;
using ExpenseTracker.Application.Categories.Queries.GetAllCategoriesQueries;
using ExpenseTracker.Application.Categories.Queries.GetCategoryByIdQueries;
using ExpenseTracker.Application.Currencies.Abstractions;
using ExpenseTracker.Application.Currencies.Commands.CreateCurrency;
using ExpenseTracker.Application.Currencies.Commands.DeleteCurrencyById;
using ExpenseTracker.Application.Currencies.Policies;
using ExpenseTracker.Application.Currencies.Queries.GetAllCurrencies;
using ExpenseTracker.Application.Currencies.Queries.GetCurrencyById;
using ExpenseTracker.Application.ExpenseRecords.Commands.CreateExpenseRecordCommands;
using ExpenseTracker.Application.ExpenseRecords.Commands.DeleteExpenseRecordByIdCommands;
using ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordByIdQueries;
using ExpenseTracker.Application.ExpenseRecords.Queries.GetExpenseRecordsByFilterQueries;
using ExpenseTracker.Application.Users.Commands.CreateUserCommands;
using ExpenseTracker.Application.Users.Commands.DeleteUserByIdCommands;
using ExpenseTracker.Application.Users.Commands.Login;
using ExpenseTracker.Application.Users.Commands.SetDefaultCurrency;
using ExpenseTracker.Application.Users.Queries.GetAllUsersQueries;
using ExpenseTracker.Application.Users.Queries.GetUserByIdQueries;
using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Shared.Results;

namespace ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateUserCommand, Result<Guid>>, CreateUserCommandHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Result<string>>, LoginCommandHandler>();
        services.AddScoped<ICommandHandler<SetDefaultCurrencyCommand, Result>, SetDefaultCurrencyCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteUserByIdCommand, Result>, DeleteUserByIdCommandHandler>();
        services.AddScoped<IQueryHandler<GetUserByIdQuery, Result<User>>, GetUserByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllUsersQuery, IEnumerable<User>>, GetAllUsersQueryHandler>();
        
        services.AddScoped<ICurrencyDeletionPolicy, CurrencyDeletionPolicy>();
        services.AddScoped<ICommandHandler<CreateCurrencyCommand, Result<Guid>>, CreateCurrencyCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteCurrencyByIdCommand, Result>, DeleteCurrencyByIdCommandHandler>();
        services.AddScoped<IQueryHandler<GetCurrencyByIdQuery, Result<Currency>>, GetCurrencyByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllCurrenciesQuery, Result<IEnumerable<Currency>>>, GetAllCurrenciesQueryHandler>();
        
        services.AddScoped<ICommandHandler<CreateCategoryCommand, Result<Guid>>, CreateCategoryCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteCategoryByIdCommand, Result>, DeleteCategoryByIdCommandHandler>();
        services.AddScoped<IQueryHandler<GetCategoryByIdQuery, Result<Category>>, GetCategoryByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>, GetAllCategoriesQueryHandler>();

        services.AddScoped<ICommandHandler<CreateExpenseRecordCommand, Result<Guid>>, CreateExpenseRecordCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteExpenseRecordByIdCommand, Result>, DeleteExpenseRecordByIdCommandHandler>();
        services.AddScoped<IQueryHandler<GetExpenseRecordByIdQuery, Result<ExpenseRecord>>, GetExpenseRecordByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetExpenseRecordsByFilterQuery, Result<IEnumerable<ExpenseRecord>>>, GetExpenseRecordsByFilterQueryHandler>();
        
        return services;
    }
}