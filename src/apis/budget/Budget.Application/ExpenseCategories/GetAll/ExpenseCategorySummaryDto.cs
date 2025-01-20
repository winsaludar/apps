namespace Budget.Application.ExpenseCategories.GetAll;

public sealed record ExpenseCategorySummaryDto(Guid Id, string Name, string Description, Guid? ParentCategoryId = null);
