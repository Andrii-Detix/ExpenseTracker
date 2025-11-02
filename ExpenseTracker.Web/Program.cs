using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Persistence;
using ExpenseTracker.Web.Endpoints;
using ExpenseTracker.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure()
    .AddWeb();

var app = builder.Build();

app.UseExceptionHandler();

app.ApplyMigrations();

app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints()
    .MapAuthEndpoints()
    .MapCurrencyEndpoints()
    .MapCategoryEndpoints()
    .MapExpenseRecordEndpoints()
    .MapHealthCheckEndpoints();

await app.SeedData();

app.Run();