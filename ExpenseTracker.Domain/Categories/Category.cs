using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Categories.ValueObjects.CategoryNames;
using Shared.Results;

namespace ExpenseTracker.Domain.Categories;

public class Category : BaseEntity
{
    private Category(Guid id, CategoryName name)
        : base(id)
    {
        Name = name;
    }
    
    public CategoryName Name { get; }

    public static Result<Category> Create(Guid id, string name)
    {
        Result<CategoryName> nameResult = CategoryName.Create(name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new Category(id, nameResult.Value!);
    }
}