using ExpenseTracker.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapHealthCheckEndpoints();

app.Run();