namespace Budget.Application.Expenses.Delete;

public sealed record DeleteExpenseCommand(Guid ExpenseId, Guid UserId) : IRequest<Unit> { }
