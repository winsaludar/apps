namespace Budget.Application.ExpenseCategories;

public interface IExpenseCategoryDbContext
{
    Task AddExpenseCategoryAsync(ExpenseCategory expenseCategory);
    Task UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
