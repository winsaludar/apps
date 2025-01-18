namespace Budget.Infrastructure.Expenses;

public sealed class ExpenseRepository(BudgetDbContext dbContext) : IExpenseRepository
{
    public async Task<List<ExpenseDto>> GetAllAsync(Guid userId)
    {
        return await dbContext.Expenses
            .Where(x => x.UserId == userId)
            .Select(x => new ExpenseDto(x.Id, x.UserId, x.Amount, x.Currency, x.Description, x.Date, x.Category.Id, x.Category.Name))
            .ToListAsync();
    }
}
