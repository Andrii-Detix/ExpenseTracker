using Shared.Results;
using Shared.Results.Errors;

namespace ExpenseTracker.Web.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            detail: result.Error.Serialize(),
            type: GetType(result.Error.Type));

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,
            };

        static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "Validation",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Server Failure"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8",
                _ => "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
            };
    }
}