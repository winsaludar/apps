namespace Budget.Infrastructure.ExpenseCategories;

public sealed class ExpenseCategoryRepository(BudgetDbContext dbContext) : IExpenseCategoryRepository
{
    public async Task<List<ExpenseCategorySummaryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbContext.ExpensesCategories
        .Select(x => new ExpenseCategorySummaryDto(x.Id, x.Name, x.Description, x.ParentCategoryId))
        .ToListAsync(cancellationToken);

    public async Task<ExpenseCategoryDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.ExpensesCategories
            .Where(x => x.Id == id)
            .Select(x => new ExpenseCategoryDetailDto(
                x.Id,
                x.Name,
                x.Description,
                x.CreatedBy,
                x.CreatedAt,
                x.UpdatedBy,
                x.UpdatedAt,
                x.ParentCategoryId,
                (x.ParentCategory != null) ? x.ParentCategory.Name : null))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
