namespace Budget.Application.ExpenseCategories;

public interface IExpenseCategoryDbContext
{
    Task AddExpenseCategoryAsync(ExpenseCategory expenseCategory);
    Task UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory);
    Task DeleteExpenseCategoryAsync(Guid expenseCategoryId);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
