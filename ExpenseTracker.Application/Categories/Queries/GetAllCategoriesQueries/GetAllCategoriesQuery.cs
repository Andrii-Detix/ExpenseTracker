using ExpenseTracker.Application.Abstractions.CQRS.Requests;
using ExpenseTracker.Domain.Categories;

namespace ExpenseTracker.Application.Categories.Queries.GetAllCategoriesQueries;

public record GetAllCategoriesQuery() : IQuery<IEnumerable<Category>>;