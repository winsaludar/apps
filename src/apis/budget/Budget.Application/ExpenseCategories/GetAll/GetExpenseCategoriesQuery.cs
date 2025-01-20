namespace Budget.Application.ExpenseCategories.GetAll;

public sealed record GetExpenseCategoriesQuery() : IRequest<List<ExpenseCategorySummaryDto>> { }
