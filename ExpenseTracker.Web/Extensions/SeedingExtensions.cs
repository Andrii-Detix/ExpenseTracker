using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Currencies.Commands.CreateCurrency;
using Shared.Results;

namespace ExpenseTracker.Web.Extensions;

public static class SeedingExtensions
{
    public static async Task SeedData(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;
        CancellationToken ct = CancellationToken.None;

        var createCurrencyHandler = serviceProvider.GetRequiredService<ICommandHandler<CreateCurrencyCommand, Result<Guid>>>();
        foreach (var createCurrencyCommand in Currencies)
        {
            await createCurrencyHandler.Handle(createCurrencyCommand, ct);
        }
    }

    private static CreateCurrencyCommand[] Currencies =>
    [
        new("EUR", "Euro"),
        new("USD", "US Dollar")
    ];
}