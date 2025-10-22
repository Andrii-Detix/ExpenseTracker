using Shared.Results.Errors;

namespace ExpenseTracker.Domain.Categories.ValueObjects.CategoryNames;

public static class CategoryNameErrors
{
    public static readonly Error IsEmpty = Error.Validation(
        "CategoryName.IsEmpty", "Category name cannot be empty.");
}