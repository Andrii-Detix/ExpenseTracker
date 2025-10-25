using ExpenseTracker.Application.Abstractions.CQRS.Handlers;
using ExpenseTracker.Application.Abstractions.Persistence;
using ExpenseTracker.Application.Categories.Abstractions;
using ExpenseTracker.Application.Categories.Errors;
using ExpenseTracker.Domain.Categories;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Commands.DeleteCategoryByIdCommands;

public sealed class DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
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
        await unitOfWork.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}