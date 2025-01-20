namespace Budget.Application.ExpenseCategories.GetById;

public record ExpenseCategoryDetailDto(
    Guid Id, 
    string Name, 
    string Description, 
    Guid CreatedBy, 
    DateTime CreatedAt, 
    Guid? UpdatedBy = null, 
    DateTime? UpdatedAt = null, 
    Guid? ParentCategoryId = 
    null, string? 
    ParentCategoryName = null);
