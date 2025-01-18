namespace Budget.Application.Expenses.GetById;

public sealed record GetExpenseByIdQuery(Guid UserId, Guid ExpenseId) : IRequest<ExpenseDetailDto> { }
