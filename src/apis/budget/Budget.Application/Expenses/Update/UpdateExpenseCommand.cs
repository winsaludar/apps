namespace Budget.Application.Expenses.Update;

public sealed record UpdateExpenseCommand(Guid ExpenseId, string UserId, decimal Amount, string Currency, string Date, string Description, string CategoryId) : IRequest<Unit> { }
