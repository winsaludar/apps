namespace Budget.Application.Expenses.GetAll;

public sealed class GetExpensesQueryHandler(IExpenseRepository repository) : IRequestHandler<GetExpensesQuery, List<ExpenseDto>>
{
    public async Task<List<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        List<ExpenseDto> expenses = await repository.GetAllAsync(request.UserId);
        return expenses;
    }
}
