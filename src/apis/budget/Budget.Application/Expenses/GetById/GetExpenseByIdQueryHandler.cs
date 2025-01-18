namespace Budget.Application.Expenses.GetById;

public sealed class GetExpenseByIdQueryHandler(IExpenseRepository repository) : IRequestHandler<GetExpenseByIdQuery, ExpenseDetailDto>
{
    public async Task<ExpenseDetailDto> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        ExpenseDetailDto expense = await repository.GetByIdAsync(request.UserId, request.ExpenseId, cancellationToken) 
            ?? throw new NotFoundException($"Expense with id: {request.ExpenseId} and user id: {request.UserId} not found");

        return expense;
    }
}
