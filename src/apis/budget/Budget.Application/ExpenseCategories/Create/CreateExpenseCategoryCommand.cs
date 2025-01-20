namespace Budget.Application.ExpenseCategories.Create;

public sealed record CreateExpenseCategoryCommand(Guid UserId, string Name, string Description, string? ParentCategoryId = null) : IRequest<Guid> { }
