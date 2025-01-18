namespace Budget.Infrastructure.Expenses;

public sealed class ExpenseRepository : IExpenseRepository
{
    public async Task<List<ExpenseDto>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
