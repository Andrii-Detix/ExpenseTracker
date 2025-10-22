using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.Categories.Errors;
using ExpenseTracker.Domain.Categories;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Commands.DeleteCategoryByIdCommands;

public sealed class DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<DeleteCategoryByIdCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryByIdCommand command, CancellationToken ct)
    {
        Category? category = await categoryRepository.GetById(command.Id, ct);
        if (category is null)
        {
            return CategoryErrors.NotFound(command.Id);
        }
        
        await categoryRepository.Delete(command.Id, ct);
        
        return Result.Success();
    }
}