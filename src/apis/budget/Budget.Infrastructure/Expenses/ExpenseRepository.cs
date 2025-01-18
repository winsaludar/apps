namespace Budget.Infrastructure.Expenses;

public sealed class ExpenseRepository(BudgetDbContext dbContext) : IExpenseRepository
{
    public async Task<List<ExpenseSummaryDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Expenses
            .Where(x => x.UserId == userId)
            .Select(x => new ExpenseSummaryDto(x.Id, x.UserId, x.Amount, x.Currency, x.Description, x.Date, x.Category.Id, x.Category.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<ExpenseDetailDto?> GetByIdAsync(Guid userId, Guid expenseId, CancellationToken cancellationToken)
    {
        return await dbContext.Expenses
            .Where(x => x.UserId == userId && x.Id == expenseId)
            .Select(x => new ExpenseDetailDto(
                x.Id, 
                x.UserId, 
                x.Amount, 
                x.Currency, 
                x.Date, 
                x.Description, 
                x.CreatedBy, 
                x.CreatedAt, 
                x.Category.Id, 
                x.Category.Name, 
                x.Category.Description, 
                x.UpdatedBy, 
                x.UpdatedAt, 
                (x.Category.ParentCategory != null) ? x.Category.ParentCategory.Name  : null))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
