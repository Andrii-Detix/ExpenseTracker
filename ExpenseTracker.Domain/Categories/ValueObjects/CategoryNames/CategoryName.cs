using Shared.Results;

namespace ExpenseTracker.Domain.Categories.ValueObjects.CategoryNames;

public record CategoryName
{
    private CategoryName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<CategoryName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return CategoryNameErrors.IsEmpty;
        }

        return new CategoryName(name.Trim());
    }
}