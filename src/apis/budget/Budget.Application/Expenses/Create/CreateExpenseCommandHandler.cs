namespace Budget.Application.Expenses.Create;

public sealed class CreateExpenseCommandHandler(IExpenseDbContext dbContext) : IRequestHandler<CreateExpenseCommand, Unit>
{
    public async Task<Unit> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(request.UserId);
        DateTime expenseDate = DateTime.Parse(request.Date).ToUniversalTime();
        Guid categoryId = Guid.Parse(request.CategoryId);
        Expense newExpense = new(Guid.NewGuid(), userId, request.Amount, request.Currency, expenseDate, request.Description, categoryId);

        await dbContext.AddExpense(newExpense);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
