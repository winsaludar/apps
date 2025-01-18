namespace Budget.Application.Expenses.Create;

public sealed record CreateExpenseCommand(string UserId, decimal Amount, string Currency, string Date, string Description, string CategoryId) : IRequest<Unit> { }
