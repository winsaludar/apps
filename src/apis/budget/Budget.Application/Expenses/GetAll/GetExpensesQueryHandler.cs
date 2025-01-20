namespace Budget.Application.Expenses.GetAll;

public sealed class GetExpensesQueryHandler(IExpenseRepository repository) : IRequestHandler<GetExpensesQuery, List<ExpenseSummaryDto>>
{
    public async Task<List<ExpenseSummaryDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        List<ExpenseSummaryDto> expenses = await repository.GetAllAsync(request.UserId, cancellationToken);
        return expenses;
    }
}
