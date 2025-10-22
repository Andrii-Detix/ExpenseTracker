using ExpenseTracker.Application;
using ExpenseTracker.Persistence;
using ExpenseTracker.Web.Endpoints;
using ExpenseTracker.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence()
    .AddWeb();

var app = builder.Build();

app.MapUserEndpoints()
    .MapCategoryEndpoints()
    .MapExpenseRecordEndpoints()
    .MapHealthCheckEndpoints();

app.Run();