namespace Budget.Application.Expenses;

public interface IExpenseRepository
{
    Task<List<ExpenseDto>> GetAllAsync(Guid userId);
}
