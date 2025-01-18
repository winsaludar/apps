namespace Budget.Application.Abstractions;

public interface IBudgetDbContext
{
    Task AddExpense(Expense expense);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
