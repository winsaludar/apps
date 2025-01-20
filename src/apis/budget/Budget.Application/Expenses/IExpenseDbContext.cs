namespace Budget.Application.Expenses;

public interface IExpenseDbContext
{
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Guid expenseId, Guid userId);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
