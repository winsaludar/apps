namespace Budget.Application.Expenses.Create;

public sealed record CreateExpenseCommand(Guid UserId, decimal Amount, string Currency, string Date, string Description, string CategoryId) : IRequest<Guid> { }
