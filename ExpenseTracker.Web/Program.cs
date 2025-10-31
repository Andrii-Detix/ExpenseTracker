using ExpenseTracker.Application;
using ExpenseTracker.Persistence;
using ExpenseTracker.Web.Endpoints;
using ExpenseTracker.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddWeb();

var app = builder.Build();

app.ApplyMigrations();

app.UseExceptionHandler();

app.MapUserEndpoints()
    .MapCurrencyEndpoints()
    .MapCategoryEndpoints()
    .MapExpenseRecordEndpoints()
    .MapHealthCheckEndpoints();

app.Run();