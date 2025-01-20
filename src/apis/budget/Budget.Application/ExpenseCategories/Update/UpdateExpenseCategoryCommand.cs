namespace Budget.Application.ExpenseCategories.Update;

public sealed record UpdateExpenseCategoryCommand(Guid ExpenseCategoryId, Guid UserId, string Name, string Description, string? ParentCategoryId = null) : IRequest<Unit>{ }
