namespace Budget.Application.Expenses.GetById;

public sealed record ExpenseDetailDto(
    Guid Id, 
    Guid UserId,
    decimal Amount,
    string Currency,
    DateTime Date,
    string Description,
    Guid CreatedBy,
    DateTime CreatedAt,
    Guid CategoryId,
    string CategoryName,
    string CategoryDescription,
    Guid? UpdatedBy = null,
    DateTime? UpdatedAt = null,
    string? ParentCategoryName = null);
