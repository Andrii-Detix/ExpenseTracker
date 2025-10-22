using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Categories;
using Shared.Results;

namespace ExpenseTracker.Application.Categories.Queries.GetCategoryByIdQueries;

public record GetCategoryByIdQuery(Guid Id) : IQuery<Result<Category>>;