using Shared.Results.Errors;

namespace ExpenseTracker.Application.Categories.Errors;

public static class CategoryErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Category.NotFound", $"Category with id {id} was not found.");
}