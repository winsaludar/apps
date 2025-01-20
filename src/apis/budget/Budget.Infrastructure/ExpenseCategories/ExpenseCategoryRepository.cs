namespace Budget.Infrastructure.ExpenseCategories;

public sealed class ExpenseCategoryRepository(BudgetDbContext dbContext) : IExpenseCategoryRepository
{
    public async Task<List<ExpenseCategorySummaryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.ExpensesCategories
            .Select(x => new ExpenseCategorySummaryDto(x.Id, x.Name, x.Description, x.ParentCategoryId))
            .ToListAsync(cancellationToken);
    }
}
