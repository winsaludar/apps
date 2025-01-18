namespace Budget.Application.Expenses;

public interface IExpenseRepository
{
    Task<List<ExpenseSummaryDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ExpenseDetailDto?> GetByIdAsync(Guid userId, Guid expenseId, CancellationToken cancellationToken);
}
