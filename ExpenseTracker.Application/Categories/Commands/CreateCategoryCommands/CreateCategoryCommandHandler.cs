using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Domain.Categories;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Commands.CreateCategoryCommands;

public sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand command, CancellationToken ct)
    {
        Result<Category> categoryResult = Category.Create(Guid.CreateVersion7(), command.Name);
        if (categoryResult.IsFailure)
        {
            return categoryResult.Error;
        }
        
        Category category = categoryResult.Value!;
        
        await categoryRepository.Add(category, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return category.Id;
    }
}