namespace Budget.Application.Expenses;

public interface IExpenseDbContext
{
    Task AddExpense(Expense expense);
    Task UpdateExpense(Expense expense);
    Task DeleteExpense(Guid expenseId, Guid userId);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
