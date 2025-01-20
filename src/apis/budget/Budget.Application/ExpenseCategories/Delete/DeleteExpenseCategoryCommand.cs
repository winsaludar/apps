namespace Budget.Application.ExpenseCategories.Delete;

public sealed record DeleteExpenseCategoryCommand(Guid Id) : IRequest<Unit> { }
