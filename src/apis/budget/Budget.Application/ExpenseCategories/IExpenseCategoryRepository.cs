namespace Budget.Application.ExpenseCategories;

public interface IExpenseCategoryRepository
{
    Task<List<ExpenseCategorySummaryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExpenseCategoryDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
