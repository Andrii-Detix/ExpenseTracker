using ExpenseTracker.Web.Exceptions.Errors;
using ExpenseTracker.Web.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Shared.Results;

namespace ExpenseTracker.Web.Exceptions.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problem = Result
            .Failure(GlobalErrors.Occured)
            .ToProblemDetails();

        await problem.ExecuteAsync(httpContext);
        
        return true;
    }
}