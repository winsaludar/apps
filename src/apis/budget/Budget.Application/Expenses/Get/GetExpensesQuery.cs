namespace Budget.Application.Expenses.Get;

public sealed record GetExpensesQuery(Guid UserId) : IRequest<List<Expense>> { }
