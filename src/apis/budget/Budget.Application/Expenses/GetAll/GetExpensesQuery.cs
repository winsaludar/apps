namespace Budget.Application.Expenses.GetAll;

public sealed record GetExpensesQuery(Guid UserId) : IRequest<List<ExpenseSummaryDto>> { }
