namespace Budget.Application.ExpenseCategories.GetById;

public sealed record GetExpenseCategoryByIdQuery(Guid Id) : IRequest<ExpenseCategoryDetailDto> { }
